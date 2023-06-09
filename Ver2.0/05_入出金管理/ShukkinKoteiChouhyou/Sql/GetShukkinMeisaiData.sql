﻿SELECT
    TSE.KYOTEN_CD,
    TSE.SHUKKIN_NUMBER,
    TSE.DENPYOU_DATE,
    ISNULL(TSE.TORIHIKISAKI_CD, '') AS TORIHIKISAKI_CD,
    (SELECT
        T.TORIHIKISAKI_NAME_RYAKU
    FROM M_TORIHIKISAKI AS T
    WHERE T.TORIHIKISAKI_CD = TSE.TORIHIKISAKI_CD)
    AS TORIHIKISAKI_NAME_RYAKU,
    (SELECT
        T.TORIHIKISAKI_FURIGANA
    FROM M_TORIHIKISAKI AS T
    WHERE T.TORIHIKISAKI_CD = TSE.TORIHIKISAKI_CD)
    AS TORIHIKISAKI_FURIGANA,
    TSE.UPDATE_DATE,
    TSD.ROW_NUMBER,
    TSD.NYUUSHUKKIN_KBN_CD,
    (SELECT
        N.NYUUSHUKKIN_KBN_NAME_RYAKU
    FROM M_NYUUSHUKKIN_KBN AS N
    WHERE N.NYUUSHUKKIN_KBN_CD = TSD.NYUUSHUKKIN_KBN_CD)
    AS NYUUSHUKKIN_KBN_NAME_RYAKU,
    TSD.KINGAKU,
    TSD.MEISAI_BIKOU
FROM T_SHUKKIN_ENTRY AS TSE
JOIN T_SHUKKIN_DETAIL AS TSD
    ON TSE.SYSTEM_ID = TSD.SYSTEM_ID
    AND TSE.SEQ = TSD.SEQ
    AND TSD.NYUUSHUKKIN_KBN_CD IS NOT NULL
WHERE 1 = 1
/*IF dto.KyotenCd != 99*/AND TSE.KYOTEN_CD = /*dto.KyotenCd*/0/*END*/
/*IF dto.DateShuruiCd == 1*/
/*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, TSE.DENPYOU_DATE, 112) >= CONVERT(varchar, CONVERT(datetime, /*dto.DateFrom*/'2014/01/01 00:00:00'), 112)/*END*/
/*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, TSE.DENPYOU_DATE, 112) <= CONVERT(varchar, CONVERT(datetime, /*dto.DateTo*/'2014/12/31 00:00:00'), 112)/*END*/
/*END*/
/*IF dto.DateShuruiCd == 2*/
/*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, TSE.UPDATE_DATE, 112) >= CONVERT(varchar, CONVERT(datetime, /*dto.DateFrom*/'2014/01/01 00:00:00'), 112)/*END*/
/*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, TSE.UPDATE_DATE, 112) <= CONVERT(varchar, CONVERT(datetime, /*dto.DateTo*/'2014/12/31 00:00:00'), 112)/*END*/
/*END*/

/*IF dto.TorihikisakiCdFrom != null && dto.TorihikisakiCdTo != null && (dto.TorihikisakiCdFrom != '' || dto.TorihikisakiCdTo != '')*/
/*IF dto.TorihikisakiCdFrom != null*/AND TSE.TORIHIKISAKI_CD >= /*dto.TorihikisakiCdFrom*/''/*END*/
/*IF dto.TorihikisakiCdTo != null*/AND TSE.TORIHIKISAKI_CD <= /*dto.TorihikisakiCdTo*/'999999'/*END*/
/*END*/

AND TSE.DELETE_FLG = 0

/*IF dto.Sort1 == 1*/
-- 取引先・コード順
ORDER BY TSE.TORIHIKISAKI_CD, TSE.DENPYOU_DATE, TSE.SHUKKIN_NUMBER, TSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 2*/
-- 取引先・フリガナ順
ORDER BY TORIHIKISAKI_FURIGANA, TSE.TORIHIKISAKI_CD, TSE.DENPYOU_DATE, TSE.SHUKKIN_NUMBER, TSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 3*/
-- 取引先・伝票日付順
ORDER BY TSE.DENPYOU_DATE, TSE.SHUKKIN_NUMBER, TSE.TORIHIKISAKI_CD, TSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 4*/
-- 取引先・伝票番号順
ORDER BY TSE.SHUKKIN_NUMBER, TSE.DENPYOU_DATE, TSE.TORIHIKISAKI_CD, TSD.ROW_NUMBER
/*END*/
/*IF dto.Sort1 == 5*/
-- 取引先・入金区分順
ORDER BY TSD.NYUUSHUKKIN_KBN_CD, TSE.DENPYOU_DATE, TSE.SHUKKIN_NUMBER, TSE.TORIHIKISAKI_CD, TSD.DETAIL_SYSTEM_ID, TSD.ROW_NUMBER
/*END*/