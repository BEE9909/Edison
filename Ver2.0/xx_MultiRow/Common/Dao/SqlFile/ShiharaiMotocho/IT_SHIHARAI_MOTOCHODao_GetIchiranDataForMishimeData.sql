﻿SELECT
    1 AS DENSHU_KBN,
    /*IF tyuusyutuKBN == 1*/
    UE.DENPYOU_DATE AS MEISAI_DATE,
    /*END*/
    /*IF tyuusyutuKBN == 2*/
    UE.SHIHARAI_DATE AS MEISAI_DATE,
    /*END*/
    dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU AS TORIHIKI_KBN,
    UE.UKEIRE_NUMBER AS DENPYOU_NUMBER,
    UE.GYOUSHA_CD AS GYOUSHA_CD,
    UE.GENBA_CD AS GENBA_CD,
    UE.GYOUSHA_NAME AS GYOUSHA_NAME,
    UE.GENBA_NAME AS GENBA_NAME,
    UD.HINMEI_CD AS HINMEI_CD,
    UE.RECEIPT_NUMBER AS SHIHARAI_NUMBER,
    UD.HINMEI_NAME AS HINMEI_NAME,
    '' AS SUURYOU_UNIT,
    UD.SUURYOU AS SUURYOU,
    dbo.M_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU,
    UD.TANKA AS TANKA,
    UE.SHIHARAI_ZEI_KEISAN_KBN_CD AS SHIHARAI_ZEI_KEISAN_KBN_CD,
    CASE
    WHEN UE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN
        '伝票毎'
    WHEN UE.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN
        '請求毎'
    WHEN UE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN
        '明細毎'
    ELSE
        ''
    END AS SHIHARAI_ZEI_KEISAN_KBN,
    (ISNULL(UD.KINGAKU, 0) + ISNULL(UD.HINMEI_KINGAKU, 0)) AS SHIHARAI_KINGAKU,
    (CASE WHEN ISNULL(UD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
        (CASE WHEN UE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
            (CASE UE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN UD.TAX_SOTO WHEN 2 THEN UD.TAX_UCHI ELSE 0 END)
        ELSE
            0 
        END)
    ELSE
        (CASE UD.HINMEI_ZEI_KBN_CD WHEN 1 THEN UD.HINMEI_TAX_SOTO WHEN 2 THEN UD.HINMEI_TAX_UCHI ELSE 0 END)
    END) AS SHOUHIZEI,
    (CASE WHEN ISNULL(UD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
        (CASE WHEN UE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
            (CASE WHEN UE.SHIHARAI_ZEI_KBN_CD = 1 THEN UD.TAX_SOTO ELSE 0 END)
        ELSE
            0 
        END)
    ELSE
        (CASE WHEN UD.HINMEI_ZEI_KBN_CD = 1 THEN UD.HINMEI_TAX_SOTO ELSE 0 END)
    END) AS SHOUHI_SOTO_ZEI,
    UE.SHIHARAI_ZEI_KBN_CD AS SHIHARAI_ZEI_KBN_CD,
    CASE
    WHEN UE.SHIHARAI_ZEI_KBN_CD = 1 THEN
        '外税'
    WHEN UE.SHIHARAI_ZEI_KBN_CD = 2 THEN
        '内税'
    WHEN UE.SHIHARAI_ZEI_KBN_CD = 3 THEN
        '非課税'
    ELSE
        ''
    END AS SHIHARAI_ZEI_KBN,
    NULL AS SHUKKIN_KINGAKU,
    NULL AS SASHIHIKI_ZANDAKA,
    UD.MEISAI_BIKOU AS MEISAI_BIKOU,
    UE.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
    dbo.M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME,
    (CASE WHEN UE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
        (CASE UE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN UE.SHIHARAI_TAX_SOTO WHEN 2 THEN UE.SHIHARAI_TAX_UCHI ELSE 0 END)
    ELSE
        0 
    END) AS DENPYOU_MAI_ZEI,
    (CASE WHEN UE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
        (CASE WHEN UE.SHIHARAI_ZEI_KBN_CD = 1 THEN UE.SHIHARAI_TAX_SOTO ELSE 0 END)
    ELSE
        0 
    END) AS DENPYOU_MAI_SOTO_ZEI,
    UE.SHIHARAI_ZEI_KEISAN_KBN_CD AS ZEI_KEISAN_KBN_CD,
    UE.SHIHARAI_ZEI_KBN_CD AS ZEI_KBN_CD,
    UE.SHIHARAI_SHOUHIZEI_RATE AS SHIHARAI_SHOUHIZEI_RATE,
    UD.HINMEI_ZEI_KBN_CD AS HINMEI_ZEI_KBN_CD
FROM
    dbo.T_UKEIRE_ENTRY AS UE
LEFT JOIN
    dbo.T_UKEIRE_DETAIL AS UD ON ((UE.SYSTEM_ID = UD.SYSTEM_ID) AND (UE.SEQ = UD.SEQ))
LEFT JOIN
    dbo.M_TORIHIKI_KBN ON UE.SHIHARAI_TORIHIKI_KBN_CD = dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_CD
LEFT JOIN
    dbo.M_UNIT ON UD.UNIT_CD = dbo.M_UNIT.UNIT_CD
LEFT JOIN
    dbo.M_TORIHIKISAKI ON UE.TORIHIKISAKI_CD = dbo.M_TORIHIKISAKI.TORIHIKISAKI_CD
LEFT JOIN
    dbo.M_TORIHIKISAKI_SHIHARAI AS MTSI ON UE.TORIHIKISAKI_CD = MTSI.TORIHIKISAKI_CD
WHERE
    (UD.DENPYOU_KBN_CD = 2) AND (UE.DELETE_FLG = 0) AND (UE.TAIRYUU_KBN = 0) 
    AND NOT EXISTS (
        SELECT 1 FROM T_SEISAN_DENPYOU AS SEIE
        INNER JOIN T_SEISAN_DETAIL SEIDE ON SEIE.SEISAN_NUMBER = SEIDE.SEISAN_NUMBER AND SEIDE.DELETE_FLG = '0'
            /*IF startDay != null*/AND SEIE.SEISAN_DATE < /*startDay*/ /*END*/
        WHERE
            SEIDE.DENPYOU_SHURUI_CD = 1
            AND SEIDE.DENPYOU_SYSTEM_ID = UD.SYSTEM_ID 
            AND SEIDE.DENPYOU_SEQ = UD.SEQ 
            AND SEIDE.DETAIL_SYSTEM_ID = UD.DETAIL_SYSTEM_ID
    )
/*IF startCD != '' && startCD != null*/AND UE.TORIHIKISAKI_CD >= /*startCD*//*END*/
/*IF endCD != '' && endCD != null*/AND UE.TORIHIKISAKI_CD <= /*endCD*//*END*/
/*IF tyuusyutuKBN == 1*/
/*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, UE.DENPYOU_DATE, 111), 120) < /*startDay*//*END*/
/*END*/
/*IF tyuusyutuKBN == 2*/
/*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, UE.SHIHARAI_DATE, 111), 120) < /*startDay*//*END*/
/*END*/
/*IF kakuteiKBN == 1*/AND UE.KAKUTEI_KBN = 1/*END*/
/*IF kakuteiKBN == 2*/AND UD.KAKUTEI_KBN = 1/*END*/
/*IF torihikiKBN == 2*/AND UE.SHIHARAI_TORIHIKI_KBN_CD = 1/*END*/
/*IF torihikiKBN == 1*/AND UE.SHIHARAI_TORIHIKI_KBN_CD = 2/*END*/
/*IF shimebi != '' && shimebi != null*/AND (MTSI.SHIMEBI1 = /*shimebi*/
OR MTSI.SHIMEBI2 = /*shimebi*/
OR MTSI.SHIMEBI3 = /*shimebi*/)/*END*/
UNION ALL (
    SELECT
        2 AS DENSHU_KBN,
        /*IF tyuusyutuKBN == 1*/
        SE.DENPYOU_DATE AS MEISAI_DATE,
        /*END*/
        /*IF tyuusyutuKBN == 2*/
        SE.SHIHARAI_DATE AS MEISAI_DATE,
        /*END*/
        dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU AS TORIHIKI_KBN,
        SE.SHUKKA_NUMBER AS DENPYOU_NUMBER,
        SE.GYOUSHA_CD AS GYOUSHA_CD,
        SE.GENBA_CD AS GENBA_CD,
        SE.GYOUSHA_NAME AS GYOUSHA_NAME,
        SE.GENBA_NAME AS GENBA_NAME,
        SD.HINMEI_CD AS HINMEI_CD,
        SE.RECEIPT_NUMBER AS SHIHARAI_NUMBER,
        SD.HINMEI_NAME AS HINMEI_NAME,
        '' AS SUURYOU_UNIT,
        SD.SUURYOU AS SUURYOU,
        dbo.M_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU,
        SD.TANKA AS TANKA,
        SE.SHIHARAI_ZEI_KEISAN_KBN_CD AS SHIHARAI_ZEI_KEISAN_KBN_CD,
        CASE
        WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN
            '伝票毎'
        WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN
            '請求毎'
        WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN
            '明細毎'
        ELSE
            ''
        END AS SHIHARAI_ZEI_KEISAN_KBN,
        (ISNULL(SD.KINGAKU, 0) + ISNULL(SD.HINMEI_KINGAKU, 0)) AS SHIHARAI_KINGAKU,
        (CASE WHEN ISNULL(SD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
            (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
                (CASE SE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN SD.TAX_SOTO WHEN 2 THEN SD.TAX_UCHI ELSE 0 END)
            ELSE
                0 
            END)
        ELSE
            (CASE SD.HINMEI_ZEI_KBN_CD WHEN 1 THEN SD.HINMEI_TAX_SOTO WHEN 2 THEN SD.HINMEI_TAX_UCHI ELSE 0 END)
        END) AS SHOUHIZEI,
        (CASE WHEN ISNULL(SD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
            (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
                (CASE WHEN SE.SHIHARAI_ZEI_KBN_CD = 1 THEN SD.TAX_SOTO ELSE 0 END)
            ELSE
                0 
            END)
        ELSE
            (CASE WHEN SD.HINMEI_ZEI_KBN_CD = 1 THEN SD.HINMEI_TAX_SOTO ELSE 0 END)
        END) AS SHOUHI_SOTO_ZEI,
        SE.SHIHARAI_ZEI_KBN_CD AS SHIHARAI_ZEI_KBN_CD,
        CASE
        WHEN SE.SHIHARAI_ZEI_KBN_CD = 1 THEN
            '外税'
        WHEN SE.SHIHARAI_ZEI_KBN_CD = 2 THEN
            '内税'
        WHEN SE.SHIHARAI_ZEI_KBN_CD = 3 THEN
            '非課税'
        ELSE
            ''
        END AS SHIHARAI_ZEI_KBN,
        NULL AS SHUKKIN_KINGAKU,
        NULL AS SASHIHIKI_ZANDAKA,
        SD.MEISAI_BIKOU AS MEISAI_BIKOU,
        SE.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
        dbo.M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME,
        (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
            (CASE SE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN SE.SHIHARAI_TAX_SOTO WHEN 2 THEN SE.SHIHARAI_TAX_UCHI ELSE 0 END)
        ELSE
            0 
        END) AS DENPYOU_MAI_ZEI,
        (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
            (CASE WHEN SE.SHIHARAI_ZEI_KBN_CD = 1 THEN SE.SHIHARAI_TAX_SOTO ELSE 0 END)
        ELSE
            0 
        END) AS DENPYOU_MAI_SOTO_ZEI,
        SE.SHIHARAI_ZEI_KEISAN_KBN_CD AS ZEI_KEISAN_KBN_CD,
        SE.SHIHARAI_ZEI_KBN_CD AS ZEI_KBN_CD,
        SE.SHIHARAI_SHOUHIZEI_RATE AS SHIHARAI_SHOUHIZEI_RATE,
        SD.HINMEI_ZEI_KBN_CD AS HINMEI_ZEI_KBN_CD
    FROM
        dbo.T_SHUKKA_ENTRY AS SE
    LEFT JOIN
        dbo.T_SHUKKA_DETAIL AS SD ON ((SE.SYSTEM_ID = SD.SYSTEM_ID) AND (SE.SEQ = SD.SEQ))
    LEFT JOIN
        dbo.M_TORIHIKI_KBN ON SE.SHIHARAI_TORIHIKI_KBN_CD = dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_CD
    LEFT JOIN
        dbo.M_UNIT ON SD.UNIT_CD = dbo.M_UNIT.UNIT_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI ON SE.TORIHIKISAKI_CD = dbo.M_TORIHIKISAKI.TORIHIKISAKI_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI_SHIHARAI AS MTSI ON SE.TORIHIKISAKI_CD = MTSI.TORIHIKISAKI_CD
    WHERE
        (SD.DENPYOU_KBN_CD = 2) AND (SE.DELETE_FLG = 0) AND (SE.TAIRYUU_KBN = 0) 
        AND (SE.KENSHU_MUST_KBN = 0 OR SE.KENSHU_MUST_KBN IS NULL)
        AND NOT EXISTS (
            SELECT 1 FROM T_SEISAN_DENPYOU AS SEIE
            INNER JOIN T_SEISAN_DETAIL SEIDE ON SEIE.SEISAN_NUMBER = SEIDE.SEISAN_NUMBER AND SEIDE.DELETE_FLG = '0'
                /*IF startDay != null*/AND SEIE.SEISAN_DATE < /*startDay*/ /*END*/
            WHERE
                SEIDE.DENPYOU_SHURUI_CD = 2
                AND SEIDE.DENPYOU_SYSTEM_ID = SD.SYSTEM_ID 
                AND SEIDE.DENPYOU_SEQ = SD.SEQ 
                AND SEIDE.DETAIL_SYSTEM_ID = SD.DETAIL_SYSTEM_ID
        )
    /*IF startCD != '' && startCD != null*/AND SE.TORIHIKISAKI_CD >= /*startCD*//*END*/
    /*IF endCD != '' && endCD != null*/AND SE.TORIHIKISAKI_CD <= /*endCD*//*END*/
    /*IF tyuusyutuKBN == 1*/
    /*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, SE.DENPYOU_DATE, 111), 120) < /*startDay*//*END*/
    /*END*/
    /*IF tyuusyutuKBN == 2*/
    /*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, SE.SHIHARAI_DATE, 111), 120) < /*startDay*//*END*/
    /*END*/
    /*IF kakuteiKBN == 1*/AND SE.KAKUTEI_KBN = 1/*END*/
    /*IF kakuteiKBN == 2*/AND SD.KAKUTEI_KBN = 1/*END*/
    /*IF torihikiKBN == 2*/AND SE.SHIHARAI_TORIHIKI_KBN_CD = 1/*END*/
    /*IF torihikiKBN == 1*/AND SE.SHIHARAI_TORIHIKI_KBN_CD = 2/*END*/
    /*IF shimebi != '' && shimebi != null*/AND (MTSI.SHIMEBI1 = /*shimebi*/
    OR MTSI.SHIMEBI2 = /*shimebi*/
    OR MTSI.SHIMEBI3 = /*shimebi*/)/*END*/)
UNION ALL (
    SELECT
        2 AS DENSHU_KBN,
        /*IF tyuusyutuKBN == 1*/
        SE.KENSHU_DATE AS MEISAI_DATE,
        /*END*/
        /*IF tyuusyutuKBN == 2*/
        SE.KENSHU_SHIHARAI_DATE AS MEISAI_DATE,
        /*END*/
        dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU AS TORIHIKI_KBN,
        SE.SHUKKA_NUMBER AS DENPYOU_NUMBER,
        SE.GYOUSHA_CD AS GYOUSHA_CD,
        SE.GENBA_CD AS GENBA_CD,
        SE.GYOUSHA_NAME AS GYOUSHA_NAME,
        SE.GENBA_NAME AS GENBA_NAME,
        SD.HINMEI_CD AS HINMEI_CD,
        SE.RECEIPT_NUMBER AS SHIHARAI_NUMBER,
        SD.HINMEI_NAME AS HINMEI_NAME,
        '' AS SUURYOU_UNIT,
        SD.SUURYOU AS SUURYOU,
        dbo.M_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU,
        SD.TANKA AS TANKA,
        SE.SHIHARAI_ZEI_KEISAN_KBN_CD AS SHIHARAI_ZEI_KEISAN_KBN_CD,
        CASE
        WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN
            '伝票毎'
        WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN
            '請求毎'
        WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN
            '明細毎'
        ELSE
            ''
        END AS SHIHARAI_ZEI_KEISAN_KBN,
        (ISNULL(SD.KINGAKU, 0) + ISNULL(SD.HINMEI_KINGAKU, 0)) AS SHIHARAI_KINGAKU,
        (CASE WHEN ISNULL(SD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
            (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
                (CASE SE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN SD.TAX_SOTO WHEN 2 THEN SD.TAX_UCHI ELSE 0 END)
            ELSE
                0 
            END)
        ELSE
            (CASE SD.HINMEI_ZEI_KBN_CD WHEN 1 THEN SD.HINMEI_TAX_SOTO WHEN 2 THEN SD.HINMEI_TAX_UCHI ELSE 0 END)
        END) AS SHOUHIZEI,
        (CASE WHEN ISNULL(SD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
            (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
                (CASE WHEN SE.SHIHARAI_ZEI_KBN_CD = 1 THEN SD.TAX_SOTO ELSE 0 END)
            ELSE
                0 
            END)
        ELSE
            (CASE WHEN SD.HINMEI_ZEI_KBN_CD = 1 THEN SD.HINMEI_TAX_SOTO ELSE 0 END)
        END) AS SHOUHI_SOTO_ZEI,
        SE.SHIHARAI_ZEI_KBN_CD AS SHIHARAI_ZEI_KBN_CD,
        CASE
        WHEN SE.SHIHARAI_ZEI_KBN_CD = 1 THEN
            '外税'
        WHEN SE.SHIHARAI_ZEI_KBN_CD = 2 THEN
            '内税'
        WHEN SE.SHIHARAI_ZEI_KBN_CD = 3 THEN
            '非課税'
        ELSE
            ''
        END AS SHIHARAI_ZEI_KBN,
        NULL AS SHUKKIN_KINGAKU,
        NULL AS SASHIHIKI_ZANDAKA,
        NULL AS MEISAI_BIKOU,
        SE.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
        dbo.M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME,
        (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
            (CASE SE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN SE.KENSHU_SHIHARAI_TAX_SOTO WHEN 2 THEN SE.KENSHU_SHIHARAI_TAX_UCHI ELSE 0 END)
        ELSE
            0 
        END) AS DENPYOU_MAI_ZEI,
        (CASE WHEN SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
            (CASE WHEN SE.SHIHARAI_ZEI_KBN_CD = 1 THEN SE.KENSHU_SHIHARAI_TAX_SOTO ELSE 0 END)
        ELSE
            0 
        END) AS DENPYOU_MAI_SOTO_ZEI,
        SE.SHIHARAI_ZEI_KEISAN_KBN_CD AS ZEI_KEISAN_KBN_CD,
        SE.SHIHARAI_ZEI_KBN_CD AS ZEI_KBN_CD,
        SE.SHIHARAI_SHOUHIZEI_RATE AS SHIHARAI_SHOUHIZEI_RATE,
        SD.HINMEI_ZEI_KBN_CD AS HINMEI_ZEI_KBN_CD
    FROM
        dbo.T_SHUKKA_ENTRY AS SE
    LEFT JOIN (
        SELECT
            KENSHU_DETAIL.SYSTEM_ID,
            KENSHU_DETAIL.SEQ,
            KENSHU_DETAIL.DETAIL_SYSTEM_ID,
            KENSHU_DETAIL.KENSHU_SYSTEM_ID,
            KENSHU_DETAIL.SHUKKA_NUMBER,
            KENSHU_DETAIL.ROW_NO,
            KENSHU_DETAIL.KENSHU_ROW_NO,
            KENSHU_DETAIL.DENPYOU_KBN_CD,
            KENSHU_DETAIL.HINMEI_CD,
            KENSHU_DETAIL.HINMEI_NAME,
            KENSHU_DETAIL.SHUKKA_NET,
            KENSHU_DETAIL.BUBIKI,
            KENSHU_DETAIL.KENSHU_NET,
            KENSHU_DETAIL.SUURYOU,
            KENSHU_DETAIL.UNIT_CD,
            KENSHU_DETAIL.TANKA,
            KENSHU_DETAIL.KINGAKU,
            KENSHU_DETAIL.TAX_SOTO,
            KENSHU_DETAIL.TAX_UCHI,
            KENSHU_DETAIL.HINMEI_ZEI_KBN_CD,
            KENSHU_DETAIL.HINMEI_KINGAKU,
            KENSHU_DETAIL.HINMEI_TAX_SOTO,
            KENSHU_DETAIL.HINMEI_TAX_UCHI
        FROM
            T_SHUKKA_DETAIL SHUKKA_DETAIL
            INNER JOIN T_KENSHU_DETAIL KENSHU_DETAIL
                ON SHUKKA_DETAIL.SYSTEM_ID = KENSHU_DETAIL.SYSTEM_ID
                AND SHUKKA_DETAIL.SEQ = KENSHU_DETAIL.SEQ
                AND SHUKKA_DETAIL.DETAIL_SYSTEM_ID = KENSHU_DETAIL.DETAIL_SYSTEM_ID
    ) AS SD ON ((SE.SYSTEM_ID = SD.SYSTEM_ID) AND (SE.SEQ = SD.SEQ))
    LEFT JOIN
        dbo.M_TORIHIKI_KBN ON SE.SHIHARAI_TORIHIKI_KBN_CD = dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_CD
    LEFT JOIN
        dbo.M_UNIT ON SD.UNIT_CD = dbo.M_UNIT.UNIT_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI ON SE.TORIHIKISAKI_CD = dbo.M_TORIHIKISAKI.TORIHIKISAKI_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI_SHIHARAI AS MTSI ON SE.TORIHIKISAKI_CD = MTSI.TORIHIKISAKI_CD
    WHERE
        (SD.DENPYOU_KBN_CD = 2) AND (SE.DELETE_FLG = 0) AND (SE.TAIRYUU_KBN = 0) 
        AND (SE.KENSHU_MUST_KBN = 1) AND (SE.KENSHU_DATE IS NOT NULL)
        AND NOT EXISTS (
            SELECT 1 FROM T_SEISAN_DENPYOU AS SEIE
            INNER JOIN T_SEISAN_DETAIL SEIDE ON SEIE.SEISAN_NUMBER = SEIDE.SEISAN_NUMBER AND SEIDE.DELETE_FLG = '0'
                /*IF startDay != null*/AND SEIE.SEISAN_DATE < /*startDay*/ /*END*/
            WHERE
                SEIDE.DENPYOU_SHURUI_CD = 2
                AND SEIDE.DENPYOU_SYSTEM_ID = SD.SYSTEM_ID 
                AND SEIDE.DENPYOU_SEQ = SD.SEQ 
                AND SEIDE.DETAIL_SYSTEM_ID = SD.DETAIL_SYSTEM_ID
        )
    /*IF startCD != '' && startCD != null*/AND SE.TORIHIKISAKI_CD >= /*startCD*//*END*/
    /*IF endCD != '' && endCD != null*/AND SE.TORIHIKISAKI_CD <= /*endCD*//*END*/
    /*IF tyuusyutuKBN == 1*/
    /*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, SE.KENSHU_DATE, 111), 120) < /*startDay*//*END*/
    /*END*/
    /*IF tyuusyutuKBN == 2*/
    /*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, SE.KENSHU_SHIHARAI_DATE, 111), 120) < /*startDay*//*END*/
    /*END*/
    /*IF kakuteiKBN == 1*/AND SE.KAKUTEI_KBN = 1/*END*/
    /*IF torihikiKBN == 2*/AND SE.SHIHARAI_TORIHIKI_KBN_CD = 1/*END*/
    /*IF torihikiKBN == 1*/AND SE.SHIHARAI_TORIHIKI_KBN_CD = 2/*END*/
    /*IF shimebi != '' && shimebi != null*/AND (MTSI.SHIMEBI1 = /*shimebi*/
    OR MTSI.SHIMEBI2 = /*shimebi*/
    OR MTSI.SHIMEBI3 = /*shimebi*/)/*END*/)
UNION ALL (
    SELECT
        3 AS DENSHU_KBN,
        /*IF tyuusyutuKBN == 1*/
        U_SE.DENPYOU_DATE AS MEISAI_DATE,
        /*END*/
        /*IF tyuusyutuKBN == 2*/
        U_SE.SHIHARAI_DATE AS MEISAI_DATE,
        /*END*/
        dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU AS TORIHIKI_KBN,
        U_SE.UR_SH_NUMBER AS DENPYOU_NUMBER,
        U_SE.GYOUSHA_CD AS GYOUSHA_CD,
        U_SE.GENBA_CD AS GENBA_CD,
        U_SE.GYOUSHA_NAME AS GYOUSHA_NAME,
        U_SE.GENBA_NAME AS GENBA_NAME,
        U_SD.HINMEI_CD AS HINMEI_CD,
        U_SE.RECEIPT_NUMBER AS SHIHARAI_NUMBER,
        U_SD.HINMEI_NAME AS HINMEI_NAME,
        '' AS SUURYOU_UNIT,
        U_SD.SUURYOU AS SUURYOU,
        dbo.M_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU,
        U_SD.TANKA AS TANKA,
        U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD AS SHIHARAI_ZEI_KEISAN_KBN_CD,
        CASE
        WHEN U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN
            '伝票毎'
        WHEN U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN
            '請求毎'
        WHEN U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN
            '明細毎'
        ELSE
            ''
        END AS SHIHARAI_ZEI_KEISAN_KBN,
        (ISNULL(U_SD.KINGAKU, 0) + ISNULL(U_SD.HINMEI_KINGAKU, 0)) AS SHIHARAI_KINGAKU,
        (CASE WHEN ISNULL(U_SD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
            (CASE WHEN U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
                (CASE U_SE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN U_SD.TAX_SOTO WHEN 2 THEN U_SD.TAX_UCHI ELSE 0 END)
            ELSE
                0 
            END)
        ELSE
            (CASE U_SD.HINMEI_ZEI_KBN_CD WHEN 1 THEN U_SD.HINMEI_TAX_SOTO WHEN 2 THEN U_SD.HINMEI_TAX_UCHI ELSE 0 END)
        END) AS SHOUHIZEI,
        (CASE WHEN ISNULL(U_SD.HINMEI_ZEI_KBN_CD, 0) = 0 THEN 
            (CASE WHEN U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN 
                (CASE WHEN U_SE.SHIHARAI_ZEI_KBN_CD = 1 THEN U_SD.TAX_SOTO ELSE 0 END)
            ELSE
                0 
            END)
        ELSE
            (CASE WHEN U_SD.HINMEI_ZEI_KBN_CD = 1 THEN U_SD.HINMEI_TAX_SOTO ELSE 0 END)
        END) AS SHOUHI_SOTO_ZEI,
        U_SE.SHIHARAI_ZEI_KBN_CD AS SHIHARAI_ZEI_KBN_CD,
        CASE
        WHEN U_SE.SHIHARAI_ZEI_KBN_CD = 1 THEN
            '外税'
        WHEN U_SE.SHIHARAI_ZEI_KBN_CD = 2 THEN
            '内税'
        WHEN U_SE.SHIHARAI_ZEI_KBN_CD = 3 THEN
            '非課税'
        ELSE
            ''
        END AS SHIHARAI_ZEI_KBN,
        NULL AS SHUKKIN_KINGAKU,
        NULL AS SASHIHIKI_ZANDAKA,
        U_SD.MEISAI_BIKOU AS MEISAI_BIKOU,
        U_SE.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
        dbo.M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME,
        (CASE WHEN U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
            (CASE U_SE.SHIHARAI_ZEI_KBN_CD WHEN 1 THEN U_SE.SHIHARAI_TAX_SOTO WHEN 2 THEN U_SE.SHIHARAI_TAX_UCHI ELSE 0 END)
        ELSE
            0 
        END) AS DENPYOU_MAI_ZEI,
        (CASE WHEN U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN 
            (CASE WHEN U_SE.SHIHARAI_ZEI_KBN_CD = 1 THEN U_SE.SHIHARAI_TAX_SOTO ELSE 0 END)
        ELSE
            0 
        END) AS DENPYOU_MAI_SOTO_ZEI,
        U_SE.SHIHARAI_ZEI_KEISAN_KBN_CD AS ZEI_KEISAN_KBN_CD,
        U_SE.SHIHARAI_ZEI_KBN_CD AS ZEI_KBN_CD,
        U_SE.SHIHARAI_SHOUHIZEI_RATE AS SHIHARAI_SHOUHIZEI_RATE,
        U_SD.HINMEI_ZEI_KBN_CD AS HINMEI_ZEI_KBN_CD
    FROM
        dbo.T_UR_SH_ENTRY AS U_SE
    LEFT JOIN
        dbo.T_UR_SH_DETAIL AS U_SD ON ((U_SE.SYSTEM_ID = U_SD.SYSTEM_ID) AND (U_SE.SEQ = U_SD.SEQ))
    LEFT JOIN
        dbo.M_TORIHIKI_KBN ON U_SE.SHIHARAI_TORIHIKI_KBN_CD = dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_CD
    LEFT JOIN
        dbo.M_UNIT ON U_SD.UNIT_CD = dbo.M_UNIT.UNIT_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI ON U_SE.TORIHIKISAKI_CD = dbo.M_TORIHIKISAKI.TORIHIKISAKI_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI_SHIHARAI AS MTSI ON U_SE.TORIHIKISAKI_CD = MTSI.TORIHIKISAKI_CD
    WHERE
        (U_SD.DENPYOU_KBN_CD = 2) AND (U_SE.DELETE_FLG = 0) 
        AND NOT EXISTS (
            SELECT 1 FROM T_SEISAN_DENPYOU AS SEIE
            INNER JOIN T_SEISAN_DETAIL SEIDE ON SEIE.SEISAN_NUMBER = SEIDE.SEISAN_NUMBER AND SEIDE.DELETE_FLG = '0'
                /*IF startDay != null*/AND SEIE.SEISAN_DATE < /*startDay*/ /*END*/
            WHERE
                SEIDE.DENPYOU_SHURUI_CD = 3
                AND SEIDE.DENPYOU_SYSTEM_ID = U_SD.SYSTEM_ID 
                AND SEIDE.DENPYOU_SEQ = U_SD.SEQ 
                AND SEIDE.DETAIL_SYSTEM_ID = U_SD.DETAIL_SYSTEM_ID
        )
    /*IF startCD != '' && startCD != null*/AND U_SE.TORIHIKISAKI_CD >= /*startCD*//*END*/
    /*IF endCD != '' && endCD != null*/AND U_SE.TORIHIKISAKI_CD <= /*endCD*//*END*/
    /*IF tyuusyutuKBN == 1*/
    /*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, U_SE.DENPYOU_DATE, 111), 120) < /*startDay*//*END*/
    /*END*/
    /*IF tyuusyutuKBN == 2*/
    /*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, U_SE.SHIHARAI_DATE, 111), 120) < /*startDay*//*END*/
    /*END*/
    /*IF kakuteiKBN == 1*/AND U_SE.KAKUTEI_KBN = 1/*END*/
    /*IF kakuteiKBN == 2*/AND U_SD.KAKUTEI_KBN = 1/*END*/
    /*IF kakuteiKBN == 2*/AND U_SD.KAKUTEI_KBN = 1/*END*/
    /*IF torihikiKBN == 2*/AND U_SE.SHIHARAI_TORIHIKI_KBN_CD = 1/*END*/
    /*IF torihikiKBN == 1*/AND U_SE.SHIHARAI_TORIHIKI_KBN_CD = 2/*END*/
    /*IF shimebi != '' && shimebi != null*/AND (MTSI.SHIMEBI1 = /*shimebi*/
    OR MTSI.SHIMEBI2 = /*shimebi*/
    OR MTSI.SHIMEBI3 = /*shimebi*/)/*END*/)
/*IF torihikiKBN != 2*/
UNION ALL (
    SELECT
        20 AS DENSHU_KBN,
        SHE.DENPYOU_DATE AS MEISAI_DATE,
        '出金' AS TORIHIKI_KBN,
        SHE.SHUKKIN_NUMBER AS DENPYOU_NUMBER,
        '' AS GYOUSHA_CD,
        '' AS GENBA_CD,
        '' AS GYOUSHA_NAME,
        '' AS GENBA_NAME,
        '' AS HINMEI_CD,
        NULL AS SHIHARAI_NUMBER,
        dbo.M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU AS HINMEI_NAME,
        '' AS SUURYOU_UNIT,
        NULL AS SUURYOU,
        '' AS UNIT_NAME_RYAKU,
        NULL AS TANKA,
        NULL AS SHIHARAI_ZEI_KEISAN_KBN_CD,
        NULL AS SHIHARAI_ZEI_KEISAN_KBN,
        NULL AS SHIHARAI_KINGAKU,
        NULL AS SHOUHIZEI,
        NULL AS SHOUHI_SOTO_ZEI,
        NULL AS URIAGE_ZEI_KBN_CD,
        NULL AS URIAGE_ZEI_KBN,
        ISNULL(SHD.KINGAKU, 0) AS SHUKKIN_KINGAKU,
        NULL AS SASHIHIKI_ZANDAKA,
        SHD.MEISAI_BIKOU AS MEISAI_BIKOU,
        SHE.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
        dbo.M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME,
        NULL AS DENPYOU_MAI_ZEI,
        NULL AS DENPYOU_MAI_SOTO_ZEI,
        NULL AS ZEI_KEISAN_KBN_CD,
        NULL AS ZEI_KBN_CD,
        NULL AS SHIHARAI_SHOUHIZEI_RATE,
        NULL AS HINMEI_ZEI_KBN_CD
    FROM
        dbo.T_SHUKKIN_ENTRY AS SHE
    LEFT JOIN
        dbo.T_SHUKKIN_DETAIL AS SHD ON ((SHE.SYSTEM_ID = SHD.SYSTEM_ID) AND (SHE.SEQ = SHD.SEQ))
    LEFT JOIN
        dbo.M_NYUUSHUKKIN_KBN ON SHD.NYUUSHUKKIN_KBN_CD = dbo.M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI ON SHE.TORIHIKISAKI_CD = dbo.M_TORIHIKISAKI.TORIHIKISAKI_CD
    LEFT JOIN
        dbo.M_TORIHIKISAKI_SHIHARAI AS MTSI ON SHE.TORIHIKISAKI_CD = MTSI.TORIHIKISAKI_CD
    WHERE (SHE.DELETE_FLG = 0) 
        AND NOT EXISTS (
            SELECT 1 FROM T_SEISAN_DENPYOU AS SEIE
            INNER JOIN T_SEISAN_DETAIL SEIDE ON SEIE.SEISAN_NUMBER = SEIDE.SEISAN_NUMBER AND SEIDE.DELETE_FLG = '0'
                /*IF startDay != null*/AND SEIE.SEISAN_DATE < /*startDay*/ /*END*/
            WHERE
                SEIDE.DENPYOU_SHURUI_CD = 20
                AND SEIDE.DENPYOU_SYSTEM_ID = SHD.SYSTEM_ID 
                AND SEIDE.DENPYOU_SEQ = SHD.SEQ 
                AND SEIDE.DETAIL_SYSTEM_ID = SHD.DETAIL_SYSTEM_ID
        )
    /*IF startCD != '' && startCD != null*/AND SHE.TORIHIKISAKI_CD >= /*startCD*//*END*/
    /*IF endCD != '' && endCD != null*/AND SHE.TORIHIKISAKI_CD <= /*endCD*//*END*/
    /*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, SHE.DENPYOU_DATE, 111), 120) < /*startDay*//*END*/
    /*IF shimebi != '' && shimebi != null*/AND (MTSI.SHIMEBI1 = /*shimebi*/
    OR MTSI.SHIMEBI2 = /*shimebi*/
    OR MTSI.SHIMEBI3 = /*shimebi*/)/*END*/)/*END*/
ORDER BY TORIHIKISAKI_CD, MEISAI_DATE, DENPYOU_NUMBER, DENSHU_KBN