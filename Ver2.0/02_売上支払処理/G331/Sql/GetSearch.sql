﻿WITH
TORIHIKISAKI_GYOUSHA_GENBA AS 
(SELECT
    TORIHIKISAKI.TORIHIKISAKI_CD,
    TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU,
    GYOUSHA_GENBA.GYOUSHA_CD,
    GYOUSHA_GENBA.GYOUSHA_NAME_RYAKU,
    GYOUSHA_GENBA.GENBA_CD,
    GYOUSHA_GENBA.GENBA_NAME_RYAKU,
    TORIHIKISAKI.SHAIN_CD AS TORIHIKISAKI_SHAIN_CD,
    TORIHIKISAKI.SHAIN_NAME_RYAKU AS TORIHIKISAKI_SHAIN_NAME_RYAKU,
    GYOUSHA_GENBA.GYOUSHA_SHAIN_CD,
    GYOUSHA_GENBA.GYOUSHA_SHAIN_NAME_RYAKU,
    GYOUSHA_GENBA.GENBA_SHAIN_CD,
    GYOUSHA_GENBA.GENBA_SHAIN_NAME_RYAKU,
    TORIHIKISAKI.SEIKYUU_TORIHIKI_KBN_CD,
    TORIHIKISAKI.SEIKYUU_SHIMEBI1,
    TORIHIKISAKI.SHIHARAI_TORIHIKI_KBN_CD,
    TORIHIKISAKI.SEIKYUU,
    TORIHIKISAKI.SHIHARAI,
    TORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD
FROM
    (
        SELECT
            GYOUSHA.GYOUSHA_CD,
            GYOUSHA.GYOUSHA_NAME_RYAKU,
            GENBA.GENBA_CD,
            GENBA.GENBA_NAME_RYAKU,
            GENBA.TORIHIKISAKI_CD,
            GENBA.SHAIN_CD AS GENBA_SHAIN_CD,
            GENBA.SHAIN_NAME_RYAKU AS GENBA_SHAIN_NAME_RYAKU,
            GYOUSHA.SHAIN_CD AS GYOUSHA_SHAIN_CD,
            GYOUSHA.SHAIN_NAME_RYAKU AS GYOUSHA_SHAIN_NAME_RYAKU
        FROM
            (
                SELECT
                    GENBA.GYOUSHA_CD,
                    GENBA.GENBA_CD,
                    GENBA.TORIHIKISAKI_CD,
                    CASE WHEN GENBA.SHOKUCHI_KBN = 1 THEN GENBA.GENBA_NAME1
                         ELSE GENBA.GENBA_NAME_RYAKU
                         END AS GENBA_NAME_RYAKU,
                    SHAIN.SHAIN_CD,
                    SHAIN.SHAIN_NAME_RYAKU
                FROM
                    M_GENBA AS GENBA
                    LEFT JOIN M_SHAIN AS SHAIN
                    ON  GENBA.EIGYOU_TANTOU_CD = SHAIN.SHAIN_CD
                    AND SHAIN.EIGYOU_TANTOU_KBN = 1
                WHERE
                    GENBA.DELETE_FLG = 0
                    AND (GENBA.TEKIYOU_BEGIN <= /*data.SeikyuuDate*/'2000/01/01' OR GENBA.TEKIYOU_BEGIN IS NULL)
                    AND (GENBA.TEKIYOU_END >= /*data.SeikyuuDate*/'2000/01/01' OR GENBA.TEKIYOU_END IS NULL)
            ) AS GENBA JOIN (
                SELECT
                    GYOUSHA.GYOUSHA_CD,
                    GYOUSHA.TORIHIKISAKI_CD,
                    CASE WHEN GYOUSHA.SHOKUCHI_KBN = 1 THEN GYOUSHA.GYOUSHA_NAME1
                         ELSE GYOUSHA.GYOUSHA_NAME_RYAKU
                         END AS GYOUSHA_NAME_RYAKU,
                    SHAIN.SHAIN_CD,
                    SHAIN.SHAIN_NAME_RYAKU
                FROM
                    M_GYOUSHA AS GYOUSHA
                    LEFT JOIN M_SHAIN AS SHAIN
                    ON  GYOUSHA.EIGYOU_TANTOU_CD = SHAIN.SHAIN_CD
                    AND SHAIN.EIGYOU_TANTOU_KBN = 1
            ) AS GYOUSHA
        ON  GENBA.GYOUSHA_CD = GYOUSHA.GYOUSHA_CD
    ) AS GYOUSHA_GENBA
    LEFT JOIN (
            SELECT
                TORIHIKISAKI.TORIHIKISAKI_CD,
                CASE WHEN TORIHIKISAKI.SHOKUCHI_KBN = 1 THEN TORIHIKISAKI.TORIHIKISAKI_NAME1
                     ELSE TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU
                     END AS TORIHIKISAKI_NAME_RYAKU,
                TORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD AS SEIKYUU_TORIHIKI_KBN_CD,
                TORIHIKISAKI_SEIKYUU.SHIMEBI1 AS SEIKYUU_SHIMEBI1,
                TORIHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD AS SHIHARAI_TORIHIKI_KBN_CD,
                TORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD,
                SHAIN.SHAIN_CD,
                SHAIN.SHAIN_NAME_RYAKU,
                CASE
                    WHEN TORIHIKISAKI_SEIKYUU.SHIMEBI1 = /*data.Shimebi*/20 THEN 1
                    ELSE 0
                END SEIKYUU,
                CASE
                    WHEN TORIHIKISAKI_SHIHARAI.SHIMEBI1 = /*data.Shimebi*/20 THEN 1
                    ELSE 0
                END SHIHARAI
            FROM
                M_TORIHIKISAKI AS TORIHIKISAKI
                LEFT JOIN M_TORIHIKISAKI_SEIKYUU AS TORIHIKISAKI_SEIKYUU
                ON  TORIHIKISAKI.TORIHIKISAKI_CD = TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD
                LEFT JOIN M_TORIHIKISAKI_SHIHARAI AS TORIHIKISAKI_SHIHARAI
                ON  TORIHIKISAKI.TORIHIKISAKI_CD = TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD
                LEFT JOIN M_SHAIN AS SHAIN
                ON  TORIHIKISAKI.EIGYOU_TANTOU_CD = SHAIN.SHAIN_CD
                AND SHAIN.EIGYOU_TANTOU_KBN = 1
        ) AS TORIHIKISAKI
    ON  GYOUSHA_GENBA.TORIHIKISAKI_CD = TORIHIKISAKI.TORIHIKISAKI_CD
),
TEIKI_TSUKI AS 
(
    SELECT
        TSUKI_HINMEI.GYOUSHA_CD,
        TSUKI_HINMEI.GENBA_CD,
        TEIKI_HINMEI.HINMEI_CD,
        TSUKI_HINMEI.HINMEI_CD AS TSUKI_HINMEI_CD,
        TEIKI_HINMEI.DENPYOU_KBN_CD,
        TEIKI_HINMEI.KEIYAKU_KBN,
        TSUKI_HINMEI.TEIKI_JISSEKI_NO_SEIKYUU_KBN,
        TSUKI_HINMEI.ROW_NO,
        TSUKI_HINMEI.CHOUKA_SETTING,
        TSUKI_HINMEI.CHOUKA_LIMIT_AMOUNT,
        
        TSUKI_HINMEI.UNIT_CD ,
        TSUKI_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME,
        TSUKI_HINMEI.TANKA,
        
        TSUKI_HINMEI.CHOUKA_HINMEI_NAME AS CHOUKA_HINMEI_NAME,
        '3' AS CHOUKA_UNIT_CD,
        TEIKI_UNIT.UNIT_NAME_RYAKU AS CHOUKA_UNIT_NAME,
        null AS CHOUKA_TANKA
        
    FROM
        M_GENBA_TSUKI_HINMEI AS TSUKI_HINMEI
    LEFT JOIN M_GENBA_TEIKI_HINMEI AS TEIKI_HINMEI
    ON  TEIKI_HINMEI.GYOUSHA_CD = TSUKI_HINMEI.GYOUSHA_CD
    AND TEIKI_HINMEI.GENBA_CD = TSUKI_HINMEI.GENBA_CD
    AND TEIKI_HINMEI.TSUKI_HINMEI_CD = TSUKI_HINMEI.HINMEI_CD
    LEFT JOIN M_UNIT AS TSUKI_UNIT
    ON  TSUKI_UNIT.UNIT_CD = TSUKI_HINMEI.UNIT_CD
    LEFT JOIN M_UNIT AS TEIKI_UNIT
    ON  TEIKI_UNIT.UNIT_CD = 3
), 
JISSEKI AS 
(
	SELECT
		JISSEKI_DETAIL.GYOUSHA_CD,
		JISSEKI_DETAIL.GENBA_CD,
		JISSEKI_DETAIL.HINMEI_CD,
		SUM(CASE WHEN JISSEKI_DETAIL.UNIT_CD = 3 THEN ISNULL(JISSEKI_DETAIL.SUURYOU,0)
		    ELSE ISNULL(JISSEKI_DETAIL.KANSAN_SUURYOU,0)END) AS SUURYOU
	FROM
		T_TEIKI_JISSEKI_ENTRY AS JISSEKI_ENTRY JOIN T_TEIKI_JISSEKI_DETAIL AS JISSEKI_DETAIL
	ON  JISSEKI_ENTRY.SYSTEM_ID = JISSEKI_DETAIL.SYSTEM_ID
	AND JISSEKI_ENTRY.SEQ = JISSEKI_DETAIL.SEQ
	WHERE
		(
			CONVERT (VARCHAR, JISSEKI_ENTRY.SAGYOU_DATE, 111) >= CONVERT (VARCHAR, CONVERT (DATETIME, /*data.TaishouDateFrom*/'2014/02/01'), 111)
		AND CONVERT (VARCHAR, JISSEKI_ENTRY.SAGYOU_DATE, 111) <= CONVERT (VARCHAR, CONVERT (DATETIME, /*data.TaishouDateTo*/'2014/02/28'), 111)
		)
	AND JISSEKI_ENTRY.DELETE_FLG = 0
	AND JISSEKI_DETAIL.KEIYAKU_KBN = 1
	GROUP BY
		JISSEKI_DETAIL.GYOUSHA_CD,
		JISSEKI_DETAIL.GENBA_CD,
		JISSEKI_DETAIL.HINMEI_CD
) 
SELECT
    TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_NAME_RYAKU,
    TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_NAME_RYAKU,
    TORIHIKISAKI_GYOUSHA_GENBA.GENBA_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.GENBA_NAME_RYAKU,
    TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_SHAIN_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_SHAIN_NAME_RYAKU,
    TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_SHAIN_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_SHAIN_NAME_RYAKU,
    TORIHIKISAKI_GYOUSHA_GENBA.GENBA_SHAIN_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.GENBA_SHAIN_NAME_RYAKU,
    CASE
        WHEN TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU_SHIMEBI1 = /*data.Shimebi*/31 THEN TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU_TORIHIKI_KBN_CD
        ELSE NULL
    END TORIHIKI_KBN_CD,
    CASE
        WHEN TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU_SHIMEBI1 = /*data.Shimebi*/31 THEN TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU_SHIMEBI1
        ELSE NULL
    END SHIMEBI1,
    TEIKI_TSUKI.TSUKI_HINMEI_CD AS HINMEI_CD,
	TEIKI_TSUKI.DENPYOU_KBN_CD,
    TEIKI_TSUKI.UNIT_CD,
    TEIKI_TSUKI.UNIT_NAME,
    TEIKI_TSUKI.TANKA,
    TEIKI_TSUKI.CHOUKA_UNIT_CD,
    TEIKI_TSUKI.CHOUKA_UNIT_NAME,
    null AS CHOUKA_TANKA,--TEIKI_TSUKI.CHOUKA_TANKA,
	null AS CHOUKA_SETTING,--TEIKI_TSUKI.CHOUKA_SETTING,
    TEIKI_TSUKI.ROW_NO,
    CASE
        WHEN TEIKI_TSUKI.HINMEI_CD IS NOT NULL THEN '定期'
        ELSE '月極'
    END KEIYAKU_KBN,
	JISSEKI.SUURYOU,
	null AS CHOUKA_LIMIT_AMOUNT--TEIKI_TSUKI.CHOUKA_LIMIT_AMOUNT
FROM TORIHIKISAKI_GYOUSHA_GENBA
JOIN TEIKI_TSUKI
ON  TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_CD = TEIKI_TSUKI.GYOUSHA_CD
AND TORIHIKISAKI_GYOUSHA_GENBA.GENBA_CD = TEIKI_TSUKI.GENBA_CD
LEFT JOIN JISSEKI
ON  TEIKI_TSUKI.GYOUSHA_CD = JISSEKI.GYOUSHA_CD
AND TEIKI_TSUKI.GENBA_CD = JISSEKI.GENBA_CD
AND TEIKI_TSUKI.HINMEI_CD = JISSEKI.HINMEI_CD
WHERE
    (
        TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU = 0
    OR  (
            TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU > 0
        AND TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU > (
                SELECT
                    COUNT(*)
                FROM
                    T_UR_SH_ENTRY AS UR_SH_ENTRY
                    LEFT JOIN T_UR_SH_DETAIL AS UR_SH_DETAIL
                    ON  UR_SH_ENTRY.SYSTEM_ID = UR_SH_DETAIL.SYSTEM_ID
                    AND UR_SH_ENTRY.SEQ = UR_SH_DETAIL.SEQ
                WHERE
                    UR_SH_ENTRY.TORIHIKISAKI_CD = TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_CD
                AND UR_SH_ENTRY.GYOUSHA_CD = TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_CD
                AND UR_SH_ENTRY.GENBA_CD = TORIHIKISAKI_GYOUSHA_GENBA.GENBA_CD
                AND UR_SH_DETAIL.HINMEI_CD = TEIKI_TSUKI.TSUKI_HINMEI_CD
                AND UR_SH_DETAIL.UNIT_CD = TEIKI_TSUKI.UNIT_CD
                AND UR_SH_ENTRY.TSUKI_CREATE_KBN = 1
                AND CONVERT (VARCHAR, UR_SH_ENTRY.URIAGE_DATE, 111) = CONVERT (VARCHAR, CONVERT (DATETIME, /*data.SeikyuuDate*/'2014/02/20'), 111)
                AND UR_SH_DETAIL.DENPYOU_KBN_CD = 1
                AND UR_SH_ENTRY.DELETE_FLG = 0
            )
        )
    )
AND (
        TORIHIKISAKI_GYOUSHA_GENBA.SHIHARAI = 0
    OR  (
            TORIHIKISAKI_GYOUSHA_GENBA.SHIHARAI > 0
        AND TORIHIKISAKI_GYOUSHA_GENBA.SHIHARAI > (
                SELECT
                    COUNT(*)
                FROM
                    T_UR_SH_ENTRY AS UR_SH_ENTRY
                    LEFT JOIN T_UR_SH_DETAIL AS UR_SH_DETAIL
                    ON  UR_SH_ENTRY.SYSTEM_ID = UR_SH_DETAIL.SYSTEM_ID
                    AND UR_SH_ENTRY.SEQ = UR_SH_DETAIL.SEQ
                WHERE
                    UR_SH_ENTRY.TORIHIKISAKI_CD = TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_CD
                AND UR_SH_ENTRY.GYOUSHA_CD = TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_CD
                AND UR_SH_ENTRY.GENBA_CD = TORIHIKISAKI_GYOUSHA_GENBA.GENBA_CD
                AND UR_SH_DETAIL.HINMEI_CD = TEIKI_TSUKI.TSUKI_HINMEI_CD
                AND UR_SH_DETAIL.UNIT_CD = TEIKI_TSUKI.UNIT_CD
                AND UR_SH_ENTRY.TSUKI_CREATE_KBN = 1
                AND CONVERT (VARCHAR, UR_SH_ENTRY.SHIHARAI_DATE, 111) = CONVERT (VARCHAR, CONVERT (DATETIME, /*data.SeikyuuDate*/'2014/02/20'), 111)
                AND UR_SH_DETAIL.DENPYOU_KBN_CD = 2
                AND UR_SH_ENTRY.DELETE_FLG = 0
            )
        )
    )
AND (
        TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU_TORIHIKI_KBN_CD = 2
    OR  TORIHIKISAKI_GYOUSHA_GENBA.SHIHARAI_TORIHIKI_KBN_CD = 2
    )
AND (
        -- 定期用
        (TEIKI_TSUKI.HINMEI_CD IS NOT NULL AND
            (TEIKI_TSUKI.TEIKI_JISSEKI_NO_SEIKYUU_KBN = 0
            OR
                (TEIKI_TSUKI.TEIKI_JISSEKI_NO_SEIKYUU_KBN = 1
                AND JISSEKI.GYOUSHA_CD IS NOT NULL
                AND JISSEKI.SUURYOU IS NOT NULL
                AND JISSEKI.SUURYOU > 0
                )
            )
        )
        OR
        -- 月極用
        (TEIKI_TSUKI.HINMEI_CD IS NULL)
    )
AND (
        TORIHIKISAKI_GYOUSHA_GENBA.SEIKYUU_SHIMEBI1 = /*data.Shimebi*/20
    ) -- 締日
    /*IF data.TorihikisakiCD != null && data.TorihikisakiCD != ''*/
    AND TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_CD = /*data.TorihikisakiCD*/'' -- 取引先
    /*END*/
    /*IF data.GyousyaCD != null && data.GyousyaCD != ''*/
    AND TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_CD = /*data.GyousyaCD*/'' -- 業者
    /*END*/
    /*IF data.GenbaCD != null && data.GenbaCD != ''*/
    AND TORIHIKISAKI_GYOUSHA_GENBA.GENBA_CD = /*data.GenbaCD*/'' -- 現場
    /*END*/
    /*IF data.KyotenCD != null && data.KyotenCD != ''*/
    AND (TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_KYOTEN_CD = /*data.KyotenCD*/'' OR TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_KYOTEN_CD = '99') -- 拠点
    /*END*/
ORDER BY
    TORIHIKISAKI_GYOUSHA_GENBA.TORIHIKISAKI_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.GYOUSHA_CD,
    TORIHIKISAKI_GYOUSHA_GENBA.GENBA_CD,
    TEIKI_TSUKI.TSUKI_HINMEI_CD