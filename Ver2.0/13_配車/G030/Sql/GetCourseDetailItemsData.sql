﻿SELECT
	MCDI.REC_ID
	,MCDI.REC_SEQ
	,MCDI.HINMEI_CD
	,MH.HINMEI_NAME_RYAKU
	,MCDI.UNIT_CD
	,MU.UNIT_NAME_RYAKU
	,MCDI.KANSANCHI
	,MCDI.KANSAN_UNIT_CD
	,MCDI.KEIYAKU_KBN
	,MCDI.KEIJYOU_KBN
	,MCDI.DENPYOU_KBN_CD
	,MCDI.KANSAN_UNIT_MOBILE_OUTPUT_FLG
	,MCDI.INPUT_KBN
	,MCDI.NIOROSHI_NO AS NIOROSHI_NUMBER
	,MCDI.ANBUN_FLG
FROM
	M_COURSE_DETAIL_ITEMS MCDI
	LEFT JOIN M_HINMEI MH
		ON MCDI.HINMEI_CD = MH.HINMEI_CD
	LEFT JOIN M_UNIT MU
		ON MCDI.UNIT_CD = MU.UNIT_CD
WHERE
	MCDI.COURSE_NAME_CD = /*data.CourseNameCd*/
	/*IF data.DayCd != null && data.DayCd !='' */ AND MCDI.DAY_CD = /*data.DayCd*/ /*END*/
	AND MCDI.REC_ID = /*data.RecId*/
	AND ((MCDI.TEKIYOU_BEGIN <= /*data.SagyouDate*/ AND /*data.SagyouDate*/ <= MCDI.TEKIYOU_END)
	OR (MCDI.TEKIYOU_BEGIN <= /*data.SagyouDate*/ AND MCDI.TEKIYOU_END IS NULL))
ORDER BY
	MCDI.REC_SEQ