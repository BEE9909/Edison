﻿SELECT 
	UNIT_CD , 
	UNIT_NAME_RYAKU
FROM  
	M_UNIT
WHERE 
DENSHI_USE_KBN = 1
AND DELETE_FLG = 0 
AND UNIT_CD != 5 /* 電子の有価物は 5利用不可 */
group by UNIT_CD, UNIT_NAME_RYAKU

