﻿SELECT
MAX(SEQ)
FROM
M_ITAKU_KEIYAKU_OBOE 
/*BEGIN*/WHERE
 /*IF data.ITAKU_KEIYAKU_NO != null*/ ITAKU_KEIYAKU_NO = /*data.ITAKU_KEIYAKU_NO*/ /*END*/
 /*IF data.SYSTEM_ID != null*/AND  SYSTEM_ID = /*data.SYSTEM_ID*/ /*END*/
/*END*/