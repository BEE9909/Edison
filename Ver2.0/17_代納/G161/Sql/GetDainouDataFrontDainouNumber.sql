﻿SELECT TOP 1 T1.*
  FROM T_UR_SH_ENTRY T1
  INNER JOIN T_UR_SH_DETAIL T2
    ON T2.SYSTEM_ID = T1.SYSTEM_ID
   AND T2.SEQ = T1.SEQ
 WHERE T1.DELETE_FLG = 0
   AND T1.DAINOU_FLG = 1
   AND T1.KYOTEN_CD = /*data.KYOTEN_CD*/0
   AND T1.UR_SH_NUMBER < /*data.UR_SH_NUMBER*/0
 ORDER BY T1.UR_SH_NUMBER DESC