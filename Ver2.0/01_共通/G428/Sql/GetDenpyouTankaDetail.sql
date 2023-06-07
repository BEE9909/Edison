﻿/*BEGIN*/
  /*IF data.DENPYOU_SHURUI == 1*/
SELECT
  TUE.DENPYOU_DATE
  , TUE.TORIHIKISAKI_CD
  , TUE.TORIHIKISAKI_NAME
  , TUE.GENBA_CD
  , TUE.GENBA_NAME
  , TUE.NIOROSHI_GYOUSHA_CD
  , TUE.NIOROSHI_GYOUSHA_NAME
  , '受入' AS DENPYOU_SHURUI
  , TUE.GYOUSHA_CD
  , TUE.GYOUSHA_NAME
  , TUE.UNPAN_GYOUSHA_CD
  , TUE.UNPAN_GYOUSHA_NAME
  , TUE.NIOROSHI_GENBA_CD
  , TUE.NIOROSHI_GENBA_NAME
  , TUE.UKEIRE_NUMBER AS DENPYOU_NO
  , TUD.HINMEI_CD
  , TUD.HINMEI_NAME
  , TUD.SUURYOU
  , TUD.UNIT_CD
  , M_UNIT.UNIT_NAME_RYAKU
  , TUD.TANKA
  , TUD.KINGAKU
  , TUD.HINMEI_KINGAKU
  , TUD.SYSTEM_ID
  , TUD.SEQ
  , TUD.DETAIL_SYSTEM_ID
  , TUD.DENPYOU_KBN_CD
  , TUD.HINMEI_ZEI_KBN_CD
  ,TUE.URIAGE_SHOUHIZEI_RATE
  ,TUE.SHIHARAI_SHOUHIZEI_RATE
  ,TUE.URIAGE_ZEI_KBN_CD
  ,TUE.SHIHARAI_ZEI_KBN_CD
FROM
  T_UKEIRE_DETAIL TUD JOIN T_UKEIRE_ENTRY TUE
    ON TUD.SYSTEM_ID = TUE.SYSTEM_ID
    AND TUD.SEQ = TUE.SEQ
    AND TUD.UKEIRE_NUMBER = TUE.UKEIRE_NUMBER
  LEFT JOIN M_UNIT
    ON TUD.UNIT_CD = M_UNIT.UNIT_CD
WHERE

  /*IF data.KYOTEN_CD != null*/
  AND TUE.KYOTEN_CD = /*data.KYOTEN_CD*/
  /*END*/
  /*IF data.DENPYOU_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.DENPYOU_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.TORIHIKISAKI_CD != null*/
  AND TUE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
  /*END*/
  /*IF data.GYOUSHA_CD != null*/
  AND TUE.GYOUSHA_CD = /*data.GYOUSHA_CD*/
  /*END*/
  /*IF data.GENBA_CD != null*/
  AND TUE.GENBA_CD = /*data.GENBA_CD*/
  /*END*/
  /*IF data.HINMEI_CD != null*/
  AND TUD.HINMEI_CD = /*data.HINMEI_CD*/
  /*END*/
  /*IF data.KAKUTEI_KBN != null*/
  AND TUD.KAKUTEI_KBN = /*data.KAKUTEI_KBN*/
  /*END*/
  /*IF data.DENPYOU_KBN_CD != null*/
  AND TUD.DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/
  /*END*/
  /*IF data.UNPAN_GYOUSHA_CD != null*/
  AND TUE.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GYOUSHA_CD != null*/
  AND TUE.NIOROSHI_GYOUSHA_CD = /*data.NIOROSHI_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GENBA_CD != null*/
  AND TUE.NIOROSHI_GENBA_CD = /*data.NIOROSHI_GENBA_CD*/
  /*END*/
  /*IF data.UNIT_CD != null*/
  AND TUD.UNIT_CD = /*data.UNIT_CD*/
  /*END*/
  AND TUE.TAIRYUU_KBN = '0'
  AND TUE.DELETE_FLG = 0
  AND NOT EXISTS (
    SELECT
      TSD.DENPYOU_SHURUI_CD
    FROM
      T_SEIKYUU_DETAIL TSD
    WHERE
      TSD.DENPYOU_SHURUI_CD = 1
      AND TSD.DENPYOU_SYSTEM_ID = TUD.SYSTEM_ID
      AND TSD.DENPYOU_SEQ = TUD.SEQ
      AND TSD.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
      AND TSD.DENPYOU_NUMBER = TUD.UKEIRE_NUMBER
  )
  AND M_UNIT.DELETE_FLG = 0
  ORDER BY
  DENPYOU_DATE
  , DENPYOU_NO
  , DENPYOU_SHURUI
  /*END*/

  /*IF data.DENPYOU_SHURUI == 2*/
  SELECT
  TUE.DENPYOU_DATE
  , TUE.TORIHIKISAKI_CD
  , TUE.TORIHIKISAKI_NAME
  , TUE.GENBA_CD
  , TUE.GENBA_NAME
  , NULL AS NIOROSHI_GYOUSHA_CD
  , NULL AS NIOROSHI_GYOUSHA_NAME
  , '出荷' AS DENPYOU_SHURUI
  , TUE.GYOUSHA_CD
  , TUE.GYOUSHA_NAME
  , TUE.UNPAN_GYOUSHA_CD
  , TUE.UNPAN_GYOUSHA_NAME
  , NULL AS NIOROSHI_GENBA_CD
  , NULL AS NIOROSHI_GENBA_NAME
  , TUE.SHUKKA_NUMBER AS DENPYOU_NO
  , TUD.HINMEI_CD
  , TUD.HINMEI_NAME
  , TUD.SUURYOU
  , TUD.UNIT_CD
  , M_UNIT.UNIT_NAME_RYAKU
  , TUD.TANKA
  , TUD.KINGAKU
  , TUD.HINMEI_KINGAKU
  , TUD.SYSTEM_ID
  , TUD.SEQ
  , TUD.DETAIL_SYSTEM_ID
  , TUD.DENPYOU_KBN_CD
  , TUD.HINMEI_ZEI_KBN_CD
  ,TUE.URIAGE_SHOUHIZEI_RATE
  ,TUE.SHIHARAI_SHOUHIZEI_RATE
  ,TUE.URIAGE_ZEI_KBN_CD
  ,TUE.SHIHARAI_ZEI_KBN_CD
FROM
  T_SHUKKA_DETAIL TUD JOIN T_SHUKKA_ENTRY TUE
    ON TUD.SYSTEM_ID = TUE.SYSTEM_ID
    AND TUD.SEQ = TUE.SEQ
    AND TUD.SHUKKA_NUMBER = TUE.SHUKKA_NUMBER
  LEFT JOIN M_UNIT
    ON TUD.UNIT_CD = M_UNIT.UNIT_CD
WHERE

  /*IF data.KYOTEN_CD != null*/
  AND TUE.KYOTEN_CD = /*data.KYOTEN_CD*/
  /*END*/
  /*IF data.DENPYOU_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.DENPYOU_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.TORIHIKISAKI_CD != null*/
  AND TUE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
  /*END*/
  /*IF data.GYOUSHA_CD != null*/
  AND TUE.GYOUSHA_CD = /*data.GYOUSHA_CD*/
  /*END*/
  /*IF data.GENBA_CD != null*/
  AND TUE.GENBA_CD = /*data.GENBA_CD*/
  /*END*/
  /*IF data.HINMEI_CD != null*/
  AND TUD.HINMEI_CD = /*data.HINMEI_CD*/
  /*END*/
  /*IF data.KAKUTEI_KBN != null*/
  AND TUD.KAKUTEI_KBN = /*data.KAKUTEI_KBN*/
  /*END*/
  /*IF data.DENPYOU_KBN_CD != null*/
  AND TUD.DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/
  /*END*/
  /*IF data.UNPAN_GYOUSHA_CD != null*/
  AND TUE.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/
  /*END*/
  /*IF data.UNIT_CD != null*/
  AND TUD.UNIT_CD = /*data.UNIT_CD*/
  /*END*/
  AND TUE.TAIRYUU_KBN = '0'
  AND TUE.DELETE_FLG = 0
  AND NOT EXISTS (
    SELECT
      TSD.DENPYOU_SHURUI_CD
    FROM
      T_SEIKYUU_DETAIL TSD
    WHERE
      TSD.DENPYOU_SHURUI_CD = 2
      AND TSD.DENPYOU_SYSTEM_ID = TUD.SYSTEM_ID
      AND TSD.DENPYOU_SEQ = TUD.SEQ
      AND TSD.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
      AND TSD.DENPYOU_NUMBER = TUD.SHUKKA_NUMBER
  )
  AND M_UNIT.DELETE_FLG = 0
ORDER BY
  DENPYOU_DATE
  , DENPYOU_NO
  , DENPYOU_SHURUI
  /*END*/

  /*IF data.DENPYOU_SHURUI == 3*/
  SELECT
  TUE.DENPYOU_DATE
  , TUE.TORIHIKISAKI_CD
  , TUE.TORIHIKISAKI_NAME
  , TUE.GENBA_CD
  , TUE.GENBA_NAME
  , TUE.NIOROSHI_GYOUSHA_CD
  , TUE.NIOROSHI_GYOUSHA_NAME
  , '売上/支払' AS DENPYOU_SHURUI
  , TUE.GYOUSHA_CD
  , TUE.GYOUSHA_NAME
  , TUE.UNPAN_GYOUSHA_CD
  , TUE.UNPAN_GYOUSHA_NAME
  , TUE.NIOROSHI_GENBA_CD
  , TUE.NIOROSHI_GENBA_NAME
  , TUE.UR_SH_NUMBER AS DENPYOU_NO
  , TUD.HINMEI_CD
  , TUD.HINMEI_NAME
  , TUD.SUURYOU
  , TUD.UNIT_CD
  , M_UNIT.UNIT_NAME_RYAKU
  , TUD.TANKA
  , TUD.KINGAKU
  , TUD.HINMEI_KINGAKU
  , TUD.SYSTEM_ID
  , TUD.SEQ
  , TUD.DETAIL_SYSTEM_ID
  , TUD.DENPYOU_KBN_CD
  , TUD.HINMEI_ZEI_KBN_CD
  ,TUE.URIAGE_SHOUHIZEI_RATE
  ,TUE.SHIHARAI_SHOUHIZEI_RATE
  ,TUE.URIAGE_ZEI_KBN_CD
  ,TUE.SHIHARAI_ZEI_KBN_CD
FROM
  T_UR_SH_DETAIL TUD
   JOIN T_UR_SH_ENTRY TUE
    ON TUD.SYSTEM_ID = TUE.SYSTEM_ID
    AND TUD.SEQ = TUE.SEQ
    AND TUD.UR_SH_NUMBER = TUE.UR_SH_NUMBER
  LEFT JOIN M_UNIT
    ON TUD.UNIT_CD = M_UNIT.UNIT_CD
WHERE

  /*IF data.KYOTEN_CD != null*/
  AND TUE.KYOTEN_CD = /*data.KYOTEN_CD*/
  /*END*/
  /*IF data.DENPYOU_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.DENPYOU_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.TORIHIKISAKI_CD != null*/
  AND TUE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
  /*END*/
  /*IF data.GYOUSHA_CD != null*/
  AND TUE.GYOUSHA_CD = /*data.GYOUSHA_CD*/
  /*END*/
  /*IF data.GENBA_CD != null*/
  AND TUE.GENBA_CD = /*data.GENBA_CD*/
  /*END*/
  /*IF data.HINMEI_CD != null*/
  AND TUD.HINMEI_CD = /*data.HINMEI_CD*/
  /*END*/
  /*IF data.KAKUTEI_KBN != null*/
  AND TUD.KAKUTEI_KBN = /*data.KAKUTEI_KBN*/
  /*END*/
  /*IF data.DENPYOU_KBN_CD != null*/
  AND TUD.DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/
  /*END*/
  /*IF data.UNPAN_GYOUSHA_CD != null*/
  AND TUE.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GYOUSHA_CD != null*/
  AND TUE.NIOROSHI_GYOUSHA_CD = /*data.NIOROSHI_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GENBA_CD != null*/
  AND TUE.NIOROSHI_GENBA_CD = /*data.NIOROSHI_GENBA_CD*/
  /*END*/
  /*IF data.UNIT_CD != null*/
  AND TUD.UNIT_CD = /*data.UNIT_CD*/
  /*END*/
  AND TUE.DELETE_FLG = 0
  AND NOT EXISTS (
    SELECT
      TSD.DENPYOU_SHURUI_CD
    FROM
      T_SEIKYUU_DETAIL TSD
    WHERE
      TSD.DENPYOU_SHURUI_CD = 3
      AND TSD.DENPYOU_SYSTEM_ID = TUD.SYSTEM_ID
      AND TSD.DENPYOU_SEQ = TUD.SEQ
      AND TSD.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
      AND TSD.DENPYOU_NUMBER = TUD.UR_SH_NUMBER

  )
  AND M_UNIT.DELETE_FLG = 0
      ORDER BY
  DENPYOU_DATE
  , DENPYOU_NO
  , DENPYOU_SHURUI
  /*END*/

/*IF data.DENPYOU_SHURUI == 4*/
(SELECT
  TUE.DENPYOU_DATE
  , TUE.TORIHIKISAKI_CD
  , TUE.TORIHIKISAKI_NAME
  , TUE.GENBA_CD
  , TUE.GENBA_NAME
  , TUE.NIOROSHI_GYOUSHA_CD
  , TUE.NIOROSHI_GYOUSHA_NAME
  , '受入' AS DENPYOU_SHURUI
  , TUE.GYOUSHA_CD
  , TUE.GYOUSHA_NAME
  , TUE.UNPAN_GYOUSHA_CD
  , TUE.UNPAN_GYOUSHA_NAME
  , TUE.NIOROSHI_GENBA_CD
  , TUE.NIOROSHI_GENBA_NAME
  , TUE.UKEIRE_NUMBER AS DENPYOU_NO
  , TUD.HINMEI_CD
  , TUD.HINMEI_NAME
  , TUD.SUURYOU
  , TUD.UNIT_CD
  , M_UNIT.UNIT_NAME_RYAKU
  , TUD.TANKA
  , TUD.KINGAKU
  , TUD.HINMEI_KINGAKU
  , TUD.SYSTEM_ID
  , TUD.SEQ
  , TUD.DETAIL_SYSTEM_ID
  , TUD.DENPYOU_KBN_CD
  , TUD.HINMEI_ZEI_KBN_CD
  ,TUE.URIAGE_SHOUHIZEI_RATE
  ,TUE.SHIHARAI_SHOUHIZEI_RATE
  ,TUE.URIAGE_ZEI_KBN_CD
  ,TUE.SHIHARAI_ZEI_KBN_CD
FROM
  T_UKEIRE_DETAIL TUD JOIN T_UKEIRE_ENTRY TUE
    ON TUD.SYSTEM_ID = TUE.SYSTEM_ID
    AND TUD.SEQ = TUE.SEQ
    AND TUD.UKEIRE_NUMBER = TUE.UKEIRE_NUMBER
  LEFT JOIN M_UNIT
    ON TUD.UNIT_CD = M_UNIT.UNIT_CD
WHERE

  /*IF data.KYOTEN_CD != null*/
 AND TUE.KYOTEN_CD = /*data.KYOTEN_CD*/
   /*END*/
  /*IF data.DENPYOU_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.DENPYOU_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.TORIHIKISAKI_CD != null*/
  AND TUE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
  /*END*/
  /*IF data.GYOUSHA_CD != null*/
  AND TUE.GYOUSHA_CD = /*data.GYOUSHA_CD*/
  /*END*/
  /*IF data.GENBA_CD != null*/
  AND TUE.GENBA_CD = /*data.GENBA_CD*/
  /*END*/
  /*IF data.HINMEI_CD != null*/
  AND TUD.HINMEI_CD = /*data.HINMEI_CD*/
  /*END*/
  /*IF data.KAKUTEI_KBN != null*/
  AND TUD.KAKUTEI_KBN = /*data.KAKUTEI_KBN*/
  /*END*/
  /*IF data.DENPYOU_KBN_CD != null*/
  AND TUD.DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/
  /*END*/
  /*IF data.UNPAN_GYOUSHA_CD != null*/
  AND TUE.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GYOUSHA_CD != null*/
  AND TUE.NIOROSHI_GYOUSHA_CD = /*data.NIOROSHI_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GENBA_CD != null*/
  AND TUE.NIOROSHI_GENBA_CD = /*data.NIOROSHI_GENBA_CD*/
  /*END*/
  /*IF data.UNIT_CD != null*/
  AND TUD.UNIT_CD = /*data.UNIT_CD*/
  /*END*/
  AND TUE.TAIRYUU_KBN = '0'
  AND TUE.DELETE_FLG = 0
  AND NOT EXISTS (
    SELECT
      TSD.DENPYOU_SHURUI_CD
    FROM
      T_SEIKYUU_DETAIL TSD
    WHERE
      TSD.DENPYOU_SHURUI_CD = 1
      AND TSD.DENPYOU_SYSTEM_ID = TUD.SYSTEM_ID
      AND TSD.DENPYOU_SEQ = TUD.SEQ
      AND TSD.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
      AND TSD.DENPYOU_NUMBER = TUD.UKEIRE_NUMBER
  )
  AND M_UNIT.DELETE_FLG = 0
)
UNION
(
SELECT
  TUE.DENPYOU_DATE
  , TUE.TORIHIKISAKI_CD
  , TUE.TORIHIKISAKI_NAME
  , TUE.GENBA_CD
  , TUE.GENBA_NAME
  , NULL AS NIOROSHI_GYOUSHA_CD
  , NULL AS NIOROSHI_GYOUSHA_NAME
  , '出荷' AS DENPYOU_SHURUI
  , TUE.GYOUSHA_CD
  , TUE.GYOUSHA_NAME
  , TUE.UNPAN_GYOUSHA_CD
  , TUE.UNPAN_GYOUSHA_NAME
  , NULL AS NIOROSHI_GENBA_CD
  , NULL AS NIOROSHI_GENBA_NAME
  , TUE.SHUKKA_NUMBER AS DENPYOU_NO
  , TUD.HINMEI_CD
  , TUD.HINMEI_NAME
  , TUD.SUURYOU
  , TUD.UNIT_CD
  , M_UNIT.UNIT_NAME_RYAKU
  , TUD.TANKA
  , TUD.KINGAKU
  , TUD.HINMEI_KINGAKU
  , TUD.SYSTEM_ID
  , TUD.SEQ
  , TUD.DETAIL_SYSTEM_ID
  , TUD.DENPYOU_KBN_CD
  , TUD.HINMEI_ZEI_KBN_CD
  ,TUE.URIAGE_SHOUHIZEI_RATE
  ,TUE.SHIHARAI_SHOUHIZEI_RATE
  ,TUE.URIAGE_ZEI_KBN_CD
  ,TUE.SHIHARAI_ZEI_KBN_CD
FROM
  T_SHUKKA_DETAIL TUD JOIN T_SHUKKA_ENTRY TUE
    ON TUD.SYSTEM_ID = TUE.SYSTEM_ID
    AND TUD.SEQ = TUE.SEQ
    AND TUD.SHUKKA_NUMBER = TUE.SHUKKA_NUMBER
  LEFT JOIN M_UNIT
    ON TUD.UNIT_CD = M_UNIT.UNIT_CD
WHERE
1=1
  /*IF data.KYOTEN_CD != null*/
  AND TUE.KYOTEN_CD = /*data.KYOTEN_CD*/
  /*END*/
  /*IF data.DENPYOU_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.DENPYOU_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.TORIHIKISAKI_CD != null*/
  AND TUE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
  /*END*/
  /*IF data.GYOUSHA_CD != null*/
  AND TUE.GYOUSHA_CD = /*data.GYOUSHA_CD*/
  /*END*/
  /*IF data.GENBA_CD != null*/
  AND TUE.GENBA_CD = /*data.GENBA_CD*/
  /*END*/
  /*IF data.HINMEI_CD != null*/
  AND TUD.HINMEI_CD = /*data.HINMEI_CD*/
  /*END*/
  /*IF data.KAKUTEI_KBN != null*/
  AND TUD.KAKUTEI_KBN = /*data.KAKUTEI_KBN*/
  /*END*/
  /*IF data.DENPYOU_KBN_CD != null*/
  AND TUD.DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/
  /*END*/
  /*IF data.UNPAN_GYOUSHA_CD != null*/
  AND TUE.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/
  /*END*/
  /*IF data.UNIT_CD != null*/
  AND TUD.UNIT_CD = /*data.UNIT_CD*/
  /*END*/
  AND TUE.TAIRYUU_KBN = '0'
  AND TUE.DELETE_FLG = 0
  AND NOT EXISTS (
    SELECT
      TSD.DENPYOU_SHURUI_CD
    FROM
      T_SEIKYUU_DETAIL TSD
    WHERE
      TSD.DENPYOU_SHURUI_CD = 2
      AND TSD.DENPYOU_SYSTEM_ID = TUD.SYSTEM_ID
      AND TSD.DENPYOU_SEQ = TUD.SEQ
      AND TSD.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
      AND TSD.DENPYOU_NUMBER = TUD.SHUKKA_NUMBER
  )
  AND M_UNIT.DELETE_FLG = 0
)
UNION
(
SELECT
  TUE.DENPYOU_DATE
  , TUE.TORIHIKISAKI_CD
  , TUE.TORIHIKISAKI_NAME
  , TUE.GENBA_CD
  , TUE.GENBA_NAME
  , TUE.NIOROSHI_GYOUSHA_CD
  , TUE.NIOROSHI_GYOUSHA_NAME
  , '売上/支払' AS DENPYOU_SHURUI
  , TUE.GYOUSHA_CD
  , TUE.GYOUSHA_NAME
  , TUE.UNPAN_GYOUSHA_CD
  , TUE.UNPAN_GYOUSHA_NAME
  , TUE.NIOROSHI_GENBA_CD
  , TUE.NIOROSHI_GENBA_NAME
  , TUE.UR_SH_NUMBER AS DENPYOU_NO
  , TUD.HINMEI_CD
  , TUD.HINMEI_NAME
  , TUD.SUURYOU
  , TUD.UNIT_CD
  , M_UNIT.UNIT_NAME_RYAKU
  , TUD.TANKA
  , TUD.KINGAKU
  , TUD.HINMEI_KINGAKU
  , TUD.SYSTEM_ID
  , TUD.SEQ
  , TUD.DETAIL_SYSTEM_ID
  , TUD.DENPYOU_KBN_CD
  , TUD.HINMEI_ZEI_KBN_CD
  ,TUE.URIAGE_SHOUHIZEI_RATE
  ,TUE.SHIHARAI_SHOUHIZEI_RATE
  ,TUE.URIAGE_ZEI_KBN_CD
  ,TUE.SHIHARAI_ZEI_KBN_CD
FROM
  T_UR_SH_DETAIL TUD JOIN T_UR_SH_ENTRY TUE
    ON TUD.SYSTEM_ID = TUE.SYSTEM_ID
    AND TUD.SEQ = TUE.SEQ
    AND TUD.UR_SH_NUMBER = TUE.UR_SH_NUMBER
  LEFT JOIN M_UNIT
    ON TUD.UNIT_CD = M_UNIT.UNIT_CD
WHERE
1=1
  /*IF data.KYOTEN_CD != null*/
  AND TUE.KYOTEN_CD = /*data.KYOTEN_CD*/
  /*END*/
  /*IF data.DENPYOU_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.DENPYOU_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DENPYOU_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_TO != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_TO*/null, 111), 120)
  /*END*/
  /*IF data.CREATE_DATE_FROM != null*/
  AND CONVERT(DATETIME, CONVERT(NVARCHAR, TUE.CREATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.CREATE_DATE_FROM*/null, 111), 120)
  /*END*/
  /*IF data.TORIHIKISAKI_CD != null*/
  AND TUE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
  /*END*/
  /*IF data.GYOUSHA_CD != null*/
  AND TUE.GYOUSHA_CD = /*data.GYOUSHA_CD*/
  /*END*/
  /*IF data.GENBA_CD != null*/
  AND TUE.GENBA_CD = /*data.GENBA_CD*/
  /*END*/
  /*IF data.HINMEI_CD != null*/
  AND TUD.HINMEI_CD = /*data.HINMEI_CD*/
  /*END*/
  /*IF data.KAKUTEI_KBN != null*/
  AND TUD.KAKUTEI_KBN = /*data.KAKUTEI_KBN*/
  /*END*/
  /*IF data.DENPYOU_KBN_CD != null*/
  AND TUD.DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/
  /*END*/
  /*IF data.UNPAN_GYOUSHA_CD != null*/
  AND TUE.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GYOUSHA_CD != null*/
  AND TUE.NIOROSHI_GYOUSHA_CD = /*data.NIOROSHI_GYOUSHA_CD*/
  /*END*/
  /*IF data.NIOROSHI_GENBA_CD != null*/
  AND TUE.NIOROSHI_GENBA_CD = /*data.NIOROSHI_GENBA_CD*/
  /*END*/
  /*IF data.UNIT_CD != null*/
  AND TUD.UNIT_CD = /*data.UNIT_CD*/
  /*END*/
  AND TUE.DELETE_FLG = 0
  AND NOT EXISTS (
    SELECT
      TSD.DENPYOU_SHURUI_CD
    FROM
      T_SEIKYUU_DETAIL TSD
    WHERE
      TSD.DENPYOU_SHURUI_CD = 3
      AND TSD.DENPYOU_SYSTEM_ID = TUD.SYSTEM_ID
      AND TSD.DENPYOU_SEQ = TUD.SEQ
      AND TSD.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
      AND TSD.DENPYOU_NUMBER = TUD.UR_SH_NUMBER
  )
  AND M_UNIT.DELETE_FLG = 0 )
ORDER BY
  DENPYOU_DATE
  , DENPYOU_NO
  , DENPYOU_SHURUI

  /*END*/

 /*END*/
