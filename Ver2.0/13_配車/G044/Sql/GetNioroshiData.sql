﻿SELECT 
	TTHN.*
	,M_GYOUSHA.GYOUSHA_NAME_RYAKU AS NIOROSHI_GYOUSHA_NAME_RYAKU
	,M_GENBA.GENBA_NAME_RYAKU AS NIOROSHI_GENBA_NAME_RYAKU
FROM
	T_TEIKI_HAISHA_NIOROSHI TTHN
	LEFT JOIN M_GYOUSHA
		ON TTHN.NIOROSHI_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GENBA
		ON TTHN.NIOROSHI_GYOUSHA_CD = M_GENBA.GYOUSHA_CD
		AND TTHN.NIOROSHI_GENBA_CD = M_GENBA.GENBA_CD
WHERE
	TTHN.SYSTEM_ID = /*data.SystemId*/
	AND TTHN.SEQ = /*data.Seq*/
ORDER BY
	TTHN.NIOROSHI_NUMBER