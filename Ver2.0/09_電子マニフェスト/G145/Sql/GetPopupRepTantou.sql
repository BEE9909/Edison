﻿SELECT 
	TANTOUSHA_CD
	,TANTOUSHA_NAME
FROM  
	M_DENSHI_TANTOUSHA
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	TANTOUSHA_KBN = 4
AND DELETE_FLG = 0 
/*END*/
/*IF data.JIGYOUSHA_CD != null && data.JIGYOUSHA_CD != ''*/
AND EDI_MEMBER_ID = /*data.JIGYOUSHA_CD*//*END*/ 
/*IF !deletechuFlg*/ 
group by TANTOUSHA_CD, TANTOUSHA_NAME
/*END*/ 			
/*END*/
