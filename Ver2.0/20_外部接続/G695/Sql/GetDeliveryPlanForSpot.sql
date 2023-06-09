﻿SELECT DISTINCT 
	0 AS KUMIKOMI
	,'' AS TEIKI_HAISHA_NUMBER
	,'' AS COURSE_NAME_CD
	,'' AS COURSE_NAME
	,ENTRY.SHASHU_CD
	,M_SHASHU.SHASHU_NAME_RYAKU
	,ENTRY.SHARYOU_CD
	,M_SHARYOU.SHARYOU_NAME_RYAKU
	,ENTRY.UNTENSHA_CD
	,M_SHAIN.SHAIN_NAME_RYAKU AS UNTENSHA_NAME_RYAKU
	,ENTRY.UNPAN_GYOUSHA_CD
	,M_GYOUSHA.GYOUSHA_NAME_RYAKU AS UNPAN_GYOUSHA_NAME_RYAKU
FROM
	(SELECT
		ENT.SHASHU_CD
		,ENT.SHARYOU_CD
		,ENT.UNTENSHA_CD
		,ENT.UNPAN_GYOUSHA_CD
	FROM
		T_UKETSUKE_SS_ENTRY ENT
	LEFT JOIN T_UKETSUKE_SS_DETAIL AS DEL ON ENT.SYSTEM_ID = DEL.SYSTEM_ID AND ENT.SEQ = DEL.SEQ
	WHERE
		ENT.DELETE_FLG = 0
		AND (ENT.HAISHA_SHURUI_CD = 1 OR ENT.HAISHA_SHURUI_CD = 3)
		AND ENT.HAISHA_JOKYO_CD = 2
		AND (ENT.GENBA_CD IS NOT NULL AND ENT.GENBA_CD != '')
		AND (ENT.SHASHU_CD IS NOT NULL AND ENT.SHASHU_CD != '')
		AND (ENT.SHARYOU_CD IS NOT NULL AND ENT.SHARYOU_CD != '')
		AND (ENT.SAGYOU_DATE IS NOT NULL AND ENT.SAGYOU_DATE != '')
		AND (ENT.UNPAN_GYOUSHA_CD IS NOT NULL AND ENT.UNPAN_GYOUSHA_CD != '')
		AND CONVERT(DATETIME, CONVERT(nvarchar, ENT.SAGYOU_DATE, 111), 120) >= /*data.SAGYOU_DATE_FROM*/
		AND CONVERT(DATETIME, CONVERT(nvarchar, ENT.SAGYOU_DATE, 111), 120) <= /*data.SAGYOU_DATE_TO*/
		AND NOT EXISTS (SELECT * FROM T_LOGI_DELIVERY_DETAIL WHERE DENPYOU_ATTR = 1 AND DELETE_FLG = 0 AND REF_SYSTEM_ID = ENT.SYSTEM_ID)
		--画面上の条件
		/*IF data.GYOUSHA_CD != null && data.GYOUSHA_CD != ''*/ AND ENT.GYOUSHA_CD = /*data.GYOUSHA_CD*/ /*END*/
		/*IF data.GENBA_CD != null && data.GENBA_CD != ''*/ AND ENT.GENBA_CD = /*data.GENBA_CD*/ /*END*/
		/*IF data.UNPAN_GYOUSHA_CD != null && data.UNPAN_GYOUSHA_CD != ''*/ AND ENT.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/ /*END*/
		/*IF data.SHARYOU_CD != null && data.SHARYOU_CD != ''*/ AND ENT.SHARYOU_CD = /*data.SHARYOU_CD*/ /*END*/
		/*IF data.SHASHU_CD != null && data.SHASHU_CD != ''*/ AND ENT.SHASHU_CD = /*data.SHASHU_CD*/ /*END*/
		/*IF data.UNTENSHA_CD != null && data.UNTENSHA_CD != ''*/ AND ENT.UNTENSHA_CD = /*data.UNTENSHA_CD*/ /*END*/
	UNION ALL
	SELECT
		ENT.SHASHU_CD
		,ENT.SHARYOU_CD
		,ENT.UNTENSHA_CD
		,ENT.UNPAN_GYOUSHA_CD
	FROM
		T_UKETSUKE_SK_ENTRY ENT
	LEFT JOIN T_UKETSUKE_SK_DETAIL AS DEL ON ENT.SYSTEM_ID = DEL.SYSTEM_ID AND ENT.SEQ = DEL.SEQ
	WHERE
		ENT.DELETE_FLG = 0
		AND (ENT.HAISHA_SHURUI_CD = 1 OR ENT.HAISHA_SHURUI_CD = 3)
		AND ENT.HAISHA_JOKYO_CD = 2
		AND (ENT.GENBA_CD IS NOT NULL AND ENT.GENBA_CD != '')
		AND (ENT.SHARYOU_CD IS NOT NULL AND ENT.SHARYOU_CD != '')
		AND (ENT.SAGYOU_DATE IS NOT NULL AND ENT.SAGYOU_DATE != '')
		AND (ENT.UNPAN_GYOUSHA_CD IS NOT NULL AND ENT.UNPAN_GYOUSHA_CD != '')
		AND CONVERT(DATETIME, CONVERT(nvarchar, ENT.SAGYOU_DATE, 111), 120) >= /*data.SAGYOU_DATE_FROM*/
		AND CONVERT(DATETIME, CONVERT(nvarchar, ENT.SAGYOU_DATE, 111), 120) <= /*data.SAGYOU_DATE_TO*/
		AND NOT EXISTS (SELECT * FROM T_LOGI_DELIVERY_DETAIL WHERE DENPYOU_ATTR = 2 AND DELETE_FLG = 0 AND REF_SYSTEM_ID = ENT.SYSTEM_ID)
		--画面上の条件
		/*IF data.GYOUSHA_CD != null && data.GYOUSHA_CD != ''*/ AND ENT.GYOUSHA_CD = /*data.GYOUSHA_CD*/ /*END*/
		/*IF data.GENBA_CD != null && data.GENBA_CD != ''*/ AND ENT.GENBA_CD = /*data.GENBA_CD*/ /*END*/
		/*IF data.UNPAN_GYOUSHA_CD != null && data.UNPAN_GYOUSHA_CD != ''*/ AND ENT.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/ /*END*/
		/*IF data.SHARYOU_CD != null && data.SHARYOU_CD != ''*/ AND ENT.SHARYOU_CD = /*data.SHARYOU_CD*/ /*END*/
		/*IF data.SHASHU_CD != null && data.SHASHU_CD != ''*/ AND ENT.SHASHU_CD = /*data.SHASHU_CD*/ /*END*/
		/*IF data.UNTENSHA_CD != null && data.UNTENSHA_CD != ''*/ AND ENT.UNTENSHA_CD = /*data.UNTENSHA_CD*/ /*END*/
	) AS ENTRY
	LEFT JOIN M_SHASHU ON ENTRY.SHASHU_CD = M_SHASHU.SHASHU_CD
	LEFT JOIN M_SHARYOU ON ENTRY.UNPAN_GYOUSHA_CD = M_SHARYOU.GYOUSHA_CD AND ENTRY.SHARYOU_CD = M_SHARYOU.SHARYOU_CD
	INNER JOIN M_DIGI_OUTPUT_SHARYOU MDSR ON ENTRY.UNPAN_GYOUSHA_CD = MDSR.GYOUSHA_CD AND ENTRY.SHARYOU_CD = MDSR.SHARYOU_CD
	LEFT JOIN M_SHAIN ON ENTRY.UNTENSHA_CD = M_SHAIN.SHAIN_CD
	INNER JOIN M_DIGI_OUTPUT_SHAIN MDSH ON ENTRY.UNTENSHA_CD = MDSH.SHAIN_CD
	LEFT JOIN M_GYOUSHA ON ENTRY.UNPAN_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
WHERE
	(MDSR.OUTPUT_DATE IS NOT NULL)
	AND (MDSH.OUTPUT_DATE IS NOT NULL)ORDER BY 
	ENTRY.SHASHU_CD
	,ENTRY.SHARYOU_CD
	,ENTRY.UNTENSHA_CD
	,ENTRY.UNPAN_GYOUSHA_CD
