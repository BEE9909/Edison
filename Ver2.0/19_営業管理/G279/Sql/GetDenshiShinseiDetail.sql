﻿SELECT
	DT.SYSTEM_ID
	,DT.SEQ
	,DT.DETAIL_SYSTEM_ID
	,DT.SHINSEI_NUMBER
	,DT.ROW_NO
	,DT.BUSHO_CD
	,BU.BUSHO_NAME_RYAKU
	,DT.SHAIN_CD
	,SH.SHAIN_NAME_RYAKU
	,DT.DAIRI_KESSAI_SHAIN_CD
	,SHD.SHAIN_NAME_RYAKU
	,DTA.CHECK_DATE
	,DTA.ACTION_FLG
	,CASE DTA.ACTION_FLG WHEN 1 THEN '承認' WHEN 2 THEN '否認' ELSE NULL END AS KESSAI
	,DTA.ACTION_COMMENT
FROM
	T_DENSHI_SHINSEI_DETAIL DT
	LEFT JOIN M_BUSHO BU
		ON BU.BUSHO_CD = DT.BUSHO_CD
	LEFT JOIN M_SHAIN SH
		ON SH.SHAIN_CD = DT.SHAIN_CD
	LEFT JOIN M_SHAIN SHD
		ON SHD.SHAIN_CD = DT.DAIRI_KESSAI_SHAIN_CD
	LEFT JOIN T_DENSHI_SHINSEI_DETAIL_ACTION DTA
		ON DTA.DETAIL_SYSTEM_ID = DT.DETAIL_SYSTEM_ID
		AND DTA.DELETE_FLG = 0
/*BEGIN*/
WHERE
	/*IF data.SYSTEM_ID != null*/ DT.SYSTEM_ID = /*data.SYSTEM_ID*/1 /*END*/
	/*IF data.SEQ != null*/ AND DT.SEQ = /*data.SEQ*/1 /*END*/
/*END*/
ORDER BY DT.ROW_NO