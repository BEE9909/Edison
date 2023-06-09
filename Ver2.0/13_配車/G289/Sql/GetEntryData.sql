﻿SELECT 
	TTHE.*	
	,MK.KYOTEN_NAME_RYAKU
	,MCN.COURSE_NAME_RYAKU
	,MSHARYOU.SHARYOU_NAME_RYAKU
	,MSHASHU.SHASHU_NAME_RYAKU
	,UNTENSHA.SHAIN_NAME_RYAKU AS UNTENSHA_NAME
	,HOJOIN.SHAIN_NAME_RYAKU AS HOJOIN_NAME
	,UNPAN_GYOUSHA.GYOUSHA_NAME_RYAKU AS UNPAN_GYOUSHA_NAME
FROM
	T_TEIKI_JISSEKI_ENTRY TTHE
	LEFT JOIN M_KYOTEN MK
		ON TTHE.KYOTEN_CD = MK.KYOTEN_CD
	LEFT JOIN M_COURSE_NAME MCN
		ON TTHE.COURSE_NAME_CD = MCN.COURSE_NAME_CD
	LEFT JOIN M_SHARYOU MSHARYOU
		ON TTHE.SHARYOU_CD = MSHARYOU.SHARYOU_CD
		AND MSHARYOU.GYOUSHA_CD = TTHE.UNPAN_GYOUSHA_CD
	LEFT JOIN M_SHASHU MSHASHU
		ON TTHE.SHASHU_CD = MSHASHU.SHASHU_CD
	LEFT JOIN M_SHAIN UNTENSHA
		ON TTHE.UNTENSHA_CD = UNTENSHA.SHAIN_CD
	LEFT JOIN M_GYOUSHA UNPAN_GYOUSHA
		ON TTHE.UNPAN_GYOUSHA_CD = UNPAN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_SHAIN HOJOIN
		ON TTHE.HOJOIN_CD = HOJOIN.SHAIN_CD
WHERE
	TTHE.TEIKI_JISSEKI_NUMBER = /*data.TeikiJissekiNumber*/
	AND TTHE.DELETE_FLG = 0