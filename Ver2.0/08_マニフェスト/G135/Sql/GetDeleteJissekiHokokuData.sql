﻿SELECT
       TJHE.SYSTEM_ID
      ,TJHE.SEQ

  FROM T_JISSEKI_HOUKOKU_ENTRY AS TJHE
 WHERE
       TJHE.SYSTEM_ID = /*systemid*/0
   AND TJHE.DELETE_FLG = 0