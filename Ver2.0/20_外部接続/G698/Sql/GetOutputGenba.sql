﻿SELECT 
	MG.GENBA_NAME_RYAKU 
FROM M_NAVI_OUTPUT_GENBA MNOG
	LEFT JOIN M_GENBA MG 
		ON MG.GYOUSHA_CD = MNOG.GYOUSHA_CD AND MG.GENBA_CD = MNOG.GENBA_CD
WHERE MNOG.JYOGAI_FLG = 0
	AND MNOG.GYOUSHA_CD = /*gyoushaCd*/'000000' 
	AND MNOG.GENBA_CD = /*genbaCd*/'000000'
