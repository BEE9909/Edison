﻿SELECT * FROM DT_R18_EX
WHERE KANRI_ID = /*data.KANRI_ID*/ 
/*IF !data.DELETE_FLG.IsNull*/
AND DELETE_FLG = /*data.DELETE_FLG*/
/*END*/