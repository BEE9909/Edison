﻿SELECT
  TMR.NEXT_SYSTEM_ID   AS NEXT_SYSTEM_ID,
  TME.SYSTEM_ID        AS SYSTEM_ID,
  TME.SEQ              AS SEQ,
  TMD.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID,
  TME.HAIKI_KBN_CD     AS HAIKI_KBN_CD
FROM T_MANIFEST_RELATION TMR
INNER JOIN T_MANIFEST_DETAIL TMD
ON TMR.FIRST_SYSTEM_ID = TMD.DETAIL_SYSTEM_ID
INNER JOIN T_MANIFEST_ENTRY TME
ON TME.SYSTEM_ID = TMD.SYSTEM_ID
AND TME.SEQ = TMD.SEQ 
AND TME.DELETE_FLG = 0
WHERE TMR.NEXT_SYSTEM_ID = /*NEXT_SYSTEM_ID*/  
AND TMR.DELETE_FLG = 0
ORDER BY TMR.NEXT_SYSTEM_ID,TME.SYSTEM_ID,TME.SEQ,TMR.FIRST_SYSTEM_ID
