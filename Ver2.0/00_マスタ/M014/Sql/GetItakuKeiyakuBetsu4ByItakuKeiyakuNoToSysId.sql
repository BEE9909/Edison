﻿SELECT 
    KB4.*
FROM 
    dbo.M_ITAKU_KEIYAKU_BETSU4 KB4
/*BEGIN*/
WHERE
 /*IF data.SYSTEM_ID != null*/
 AND KB4.SYSTEM_ID = /*data.SYSTEM_ID*/
 /*END*/
/*END*/
ORDER BY KB4.SYSTEM_ID
