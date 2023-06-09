﻿SELECT *
FROM (
  SELECT
      ROW_NUMBER() OVER(PARTITION BY MLU.TORIHIKISAKI_CD ORDER BY MLU.YEAR DESC, MLU.MONTH DESC) AS ROW_NO,
      MLU.TORIHIKISAKI_CD,
      MLU.PREVIOUS_MONTH_BALANCE,
      MLU.SHIME_UTIZEI_GAKU,
      MLU.SHIME_SOTOZEI_GAKU,
      ISNULL (MAU.ADJUST_TAX, 0) AS ADJUST_TAX,
      MLU.YEAR,
      MLU.MONTH,
      ISNULL (MAU.ZANDAKA, MLU.ZANDAKA) AS SASHIHIKI_URIAGE_ZANDAKA
  FROM
      T_MONTHLY_LOCK_UR AS MLU
      LEFT JOIN
          T_MONTHLY_ADJUST_UR AS MAU
      ON  MLU.TORIHIKISAKI_CD = MAU.TORIHIKISAKI_CD
      AND MLU.YEAR = MAU.YEAR
      AND MLU.MONTH = MAU.MONTH
      AND MLU.SEQ = MAU.SEQ
      LEFT JOIN
          M_TORIHIKISAKI_SEIKYUU AS MTS
      ON  MLU.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
  WHERE
      MLU.DELETE_FLG = 0
     AND (MLU.YEAR = /*year*/2014
     AND MLU.MONTH < /*month*/12
      OR MLU.YEAR < /*year*/2014)
  /*IF startCD != null && startCD != ''*/AND MLU.TORIHIKISAKI_CD >= /*startCD*/'700001'/*END*/
  /*IF endCD != null && endCD != ''*/AND MLU.TORIHIKISAKI_CD <= /*endCD*/'700001'/*END*/
  AND (
          MAU.DELETE_FLG = 0
      OR  MAU.DELETE_FLG IS NULL
      )
  AND MTS.TORIHIKI_KBN_CD = 2
  /*IF shimebi != null && shimebi != ''*/
  AND ( MTS.SHIMEBI1 = /*shimebi*/0
     OR MTS.SHIMEBI2 = /*shimebi*/0
     OR MTS.SHIMEBI3 = /*shimebi*/0
  )
  /*END*/
) DATA
WHERE DATA.ROW_NO = 1
ORDER BY DATA.TORIHIKISAKI_CD