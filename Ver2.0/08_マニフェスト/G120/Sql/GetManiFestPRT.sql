﻿   SELECT TMDP.SYSTEM_ID
     , TMDP.SEQ
     , TMDP.PRT_HAIKI_SHURUI_CD
     , TMDP.PRT_HAIKI_SHURUI_NAME
  FROM T_MANIFEST_ENTRY TME 
 INNER JOIN T_MANIFEST_PRT TMDP
    ON TME.SYSTEM_ID = TMDP.SYSTEM_ID 
   AND TME.SEQ = TMDP.SEQ
   AND TME.DELETE_FLG = 'false'
 WHERE TMDP.SYSTEM_ID = /*data.SYSTEM_ID*/ 
   AND TMDP.SEQ =  /*data.SEQ*/