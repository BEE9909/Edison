﻿SELECT 
	TH1.SYSTEM_ID
	,TH1.SEQ
	,TJ1.TEIKI_JISSEKI_NUMBER
	,TH1.KYOTEN_CD
	,MKY.KYOTEN_NAME_RYAKU
	,TH1.TEIKI_HAISHA_NUMBER
	,TH1.DENPYOU_DATE
	,TH1.SAGYOU_DATE
	,TH1.SAGYOU_BEGIN_HOUR
	,TH1.SAGYOU_BEGIN_MINUTE
	,TH1.SAGYOU_END_HOUR
	,TH1.SAGYOU_END_MINUTE
	,TH1.COURSE_NAME_CD
	,MCN.COURSE_NAME_RYAKU
	,TH1.SHARYOU_CD
	,MSR.SHARYOU_NAME_RYAKU
	,TH1.SHASHU_CD
	,MSH.SHASHU_NAME_RYAKU
	,TH1.UNTENSHA_CD
	,MUN.SHAIN_NAME_RYAKU
	,TH1.HOJOIN_CD
	,MHJ.SHAIN_NAME_RYAKU
	,TH1.TIME_STAMP
	,TH1.UNPAN_GYOUSHA_CD
	,MG.GYOUSHA_NAME_RYAKU AS UNPAN_GYOUSHA_NAME
	,ISNULL(TH1.FURIKAE_HAISHA_KBN,0) AS FURIKAE_HAISHA_KBN
	,TH1.DAY_CD
FROM
	T_TEIKI_HAISHA_ENTRY TH1
	LEFT JOIN T_TEIKI_JISSEKI_ENTRY TJ1
		ON TJ1.TEIKI_HAISHA_NUMBER = TH1.TEIKI_HAISHA_NUMBER
		AND TJ1.DELETE_FLG = 0
	LEFT JOIN M_KYOTEN MKY
		ON MKY.KYOTEN_CD = TH1.KYOTEN_CD
	LEFT JOIN M_COURSE_NAME MCN
		ON MCN.COURSE_NAME_CD = TH1.COURSE_NAME_CD
	LEFT JOIN M_SHARYOU MSR
		ON MSR.GYOUSHA_CD = TH1.UNPAN_GYOUSHA_CD
		AND MSR.SHARYOU_CD = TH1.SHARYOU_CD
	LEFT JOIN M_SHASHU MSH
		ON MSH.SHASHU_CD = TH1.SHASHU_CD
	LEFT JOIN M_SHAIN MUN
		ON MUN.SHAIN_CD = TH1.UNTENSHA_CD
	LEFT JOIN M_SHAIN MHJ
		ON MHJ.SHAIN_CD = TH1.HOJOIN_CD
	LEFT JOIN M_GYOUSHA MG
		ON MG.GYOUSHA_CD = TH1.UNPAN_GYOUSHA_CD
WHERE
	TH1.DELETE_FLG = 0
	/*IF data.KyotenCd != 99 */
	AND TH1.KYOTEN_CD = /*data.KyotenCd*/
	/*END*/
	/*IF data.SagyouDateFrom != null && data.SagyouDateFrom != ''*/
	AND /*data.SagyouDateFrom*/ <= TH1.SAGYOU_DATE
	/*END*/
	/*IF data.SagyouDateTo != null && data.SagyouDateTo != ''*/
	AND TH1.SAGYOU_DATE <= /*data.SagyouDateTo*/
	/*END*/
ORDER BY SAGYOU_DATE,TEIKI_HAISHA_NUMBER,COURSE_NAME_CD