﻿SELECT 
	JIGYOUSHA_NAME
FROM  
	M_DENSHI_JIGYOUSHA
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	HST_KBN = 1
/*END*/
/*IF data.JIGYOUSHA_CD != null && data.JIGYOUSHA_CD != ''*/
AND	EDI_MEMBER_ID = /*data.JIGYOUSHA_CD*//*END*/ 		
/*END*/