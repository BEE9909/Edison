﻿SELECT 
	HAIKI_SHURUI_NAME
FROM  
	M_DENSHI_HAIKI_SHURUI
/*BEGIN*/
where 
/*IF data.HAIKI_SHURUI_CD != null && data.HAIKI_SHURUI_CD != ''*/
AND	HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*//*END*/ 		
/*END*/