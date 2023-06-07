﻿SELECT *
FROM
(
  SELECT 
     ROW_NUMBER() OVER(PARTITION BY TSD.TORIHIKISAKI_CD ORDER BY TSD.SEIKYUU_DATE DESC, TSD.SEIKYUU_NUMBER DESC) AS ROW_NO,
     TSD.SEIKYUU_DATE,
     TSD.TORIHIKISAKI_CD,
     NULL AS PREVIOUS_MONTH_BALANCE,
     NULL AS SHIME_UTIZEI_GAKU,
     NULL AS SHIME_SOTOZEI_GAKU,
     NULL AS ADJUST_TAX,
     NULL AS YEAR,
     NULL AS MONTH,
     CASE TSD.SEIKYUU_KEITAI_KBN 
       WHEN 1 THEN (ISNULL(TSD.KONKAI_URIAGE_GAKU,0) + ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) + 
                    ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) + 
                    ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) + 
                    ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0)) 
       ELSE TSD.KONKAI_SEIKYU_GAKU END
     AS SASHIHIKI_URIAGE_ZANDAKA
  FROM T_SEIKYUU_DENPYOU TSD
     LEFT JOIN
          M_TORIHIKISAKI_SEIKYUU AS MTS
      ON  TSD.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
  WHERE TSD.DELETE_FLG = 0
  /*IF startCD != null && startCD != ''*/AND TSD.TORIHIKISAKI_CD >= /*startCD*/'700001'/*END*/
  /*IF endCD != null && endCD != ''*/AND TSD.TORIHIKISAKI_CD <= /*endCD*/'700001'/*END*/
  /*IF startDay != null*/
  AND CONVERT(DATETIME, CONVERT(nvarchar, TSD.SEIKYUU_DATE, 111), 120) < /*startDay*/'2019/02/15'
  /*END*/
  /*IF shimebi != null && shimebi != ''*/
  AND ( MTS.SHIMEBI1 = /*shimebi*/0
     OR MTS.SHIMEBI2 = /*shimebi*/0
     OR MTS.SHIMEBI3 = /*shimebi*/0
  )
  /*END*/
 ) DATA
WHERE DATA.ROW_NO = 1
ORDER BY DATA.TORIHIKISAKI_CD