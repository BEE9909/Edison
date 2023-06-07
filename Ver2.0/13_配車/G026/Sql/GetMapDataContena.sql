﻿SELECT
	'' AS SecchiChouhuku,
	CONTENA.CONTENA_SHURUI_CD,
	M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU,
	CONTENA.CONTENA_CD,
	M_CONTENA.CONTENA_NAME_RYAKU,
	UKEIRE.GYOUSHA_CD,
	M_GYOUSHA.GYOUSHA_NAME_RYAKU,
	UKEIRE.GENBA_CD,
	M_GENBA.GENBA_NAME_RYAKU,
	M_GENBA.EIGYOU_TANTOU_CD,
	M_SHAIN.SHAIN_NAME_RYAKU,
	CONTENA.CONTENA_SET_KBN,
	CONVERT(VARCHAR,UKEIRE.DENPYOU_DATE,111) AS SECCHI_DATE,
	DATEDIFF(day, UKEIRE.DENPYOU_DATE, /*data.SAGYOU_DATE*/) AS DAYSCOUNT
FROM
	T_CONTENA_RESULT AS CONTENA
	INNER JOIN T_UKEIRE_ENTRY AS UKEIRE
		ON CONTENA.SYSTEM_ID = UKEIRE.SYSTEM_ID
		AND CONTENA.SEQ = UKEIRE.SEQ
		AND UKEIRE.DELETE_FLG = 0
	INNER JOIN 
	(
		SELECT
			CONTENA.CONTENA_SHURUI_CD,
			CONTENA.CONTENA_CD,
			UKEIRE.DENPYOU_DATE AS DENPYOU_DATE,
			UKEIRE.GYOUSHA_CD,
			UKEIRE.GENBA_CD
		FROM
			T_CONTENA_RESULT AS CONTENA
			INNER JOIN T_UKEIRE_ENTRY AS UKEIRE
			ON CONTENA.SYSTEM_ID = UKEIRE.SYSTEM_ID
			AND CONTENA.SEQ = UKEIRE.SEQ
			AND UKEIRE.DELETE_FLG = 0
		WHERE
			CONTENA.DELETE_FLG = 0
			AND CONTENA.DENSHU_KBN_CD = 1
		GROUP BY
			CONTENA.CONTENA_SHURUI_CD, CONTENA.CONTENA_CD, UKEIRE.GYOUSHA_CD, UKEIRE.GENBA_CD, UKEIRE.DENPYOU_DATE
	) AS TEMP_DATA
		ON CONTENA.CONTENA_SHURUI_CD = TEMP_DATA.CONTENA_SHURUI_CD
		AND CONTENA.CONTENA_CD = TEMP_DATA.CONTENA_CD
		AND UKEIRE.DENPYOU_DATE = TEMP_DATA.DENPYOU_DATE
		AND UKEIRE.GYOUSHA_CD = TEMP_DATA.GYOUSHA_CD
		AND UKEIRE.GENBA_CD = TEMP_DATA.GENBA_CD
	INNER JOIN M_GYOUSHA
		ON UKEIRE.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD 
	INNER JOIN M_GENBA
		ON UKEIRE.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND UKEIRE.GENBA_CD = M_GENBA.GENBA_CD
	LEFT JOIN M_SHAIN
		ON M_GENBA.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD 
	INNER JOIN M_CONTENA_SHURUI
		ON CONTENA.CONTENA_SHURUI_CD = M_CONTENA_SHURUI.CONTENA_SHURUI_CD
	INNER JOIN M_CONTENA
		ON CONTENA.CONTENA_SHURUI_CD = M_CONTENA.CONTENA_SHURUI_CD
		AND CONTENA.CONTENA_CD = M_CONTENA.CONTENA_CD
WHERE 
	CONTENA.DELETE_FLG = 0
	AND CONTENA.DENSHU_KBN_CD = 1
	AND (CONTENA.CONTENA_CD IS NOT NULL AND CONTENA.CONTENA_CD <> '')
	/*IF data.GYOUSHA_CD != null*/AND UKEIRE.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
	/*IF data.CONTENA_SHURUI_CD != null*/AND CONTENA.CONTENA_SHURUI_CD = /*data.CONTENA_SHURUI_CD*//*END*/
	/*IF data.GENBA_CD != null*/AND UKEIRE.GENBA_CD = /*data.GENBA_CD*//*END*/
	/*IF data.CONTENA_CD != null*/AND CONTENA.CONTENA_CD = /*data.CONTENA_CD*//*END*/
	/*IF data.EIGYOU_TANTOU_CD != null*/AND M_GENBA.EIGYOU_TANTOU_CD = /*data.EIGYOU_TANTOU_CD*//*END*/
	/*END*/

UNION ALL (

SELECT
	'' AS SecchiChouhuku,
	CONTENA.CONTENA_SHURUI_CD,
	M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU,
	CONTENA.CONTENA_CD,
	M_CONTENA.CONTENA_NAME_RYAKU,
	URSHE.GYOUSHA_CD,
	M_GYOUSHA.GYOUSHA_NAME_RYAKU,
	URSHE.GENBA_CD,
	M_GENBA.GENBA_NAME_RYAKU,
	M_GENBA.EIGYOU_TANTOU_CD,
	M_SHAIN.SHAIN_NAME_RYAKU,
	CONTENA.CONTENA_SET_KBN,
	CONVERT(VARCHAR,URSHE.DENPYOU_DATE,111) AS SECCHI_DATE,
	DATEDIFF(day, URSHE.DENPYOU_DATE, /*data.SAGYOU_DATE*/) AS DAYSCOUNT
FROM
	T_CONTENA_RESULT AS CONTENA
	INNER JOIN T_UR_SH_ENTRY AS URSHE
		ON CONTENA.SYSTEM_ID = URSHE.SYSTEM_ID
		AND CONTENA.SEQ = URSHE.SEQ
		AND URSHE.DELETE_FLG = 0
	INNER JOIN 
	(
		SELECT
			CONTENA.CONTENA_SHURUI_CD,
			CONTENA.CONTENA_CD,
			URSHE.DENPYOU_DATE AS DENPYOU_DATE,
			URSHE.GYOUSHA_CD,
			URSHE.GENBA_CD
		FROM
			T_CONTENA_RESULT AS CONTENA
			INNER JOIN T_UR_SH_ENTRY AS URSHE
			ON CONTENA.SYSTEM_ID = URSHE.SYSTEM_ID
			AND CONTENA.SEQ = URSHE.SEQ
			AND URSHE.DELETE_FLG = 0
		WHERE
			CONTENA.DELETE_FLG = 0
			AND CONTENA.DENSHU_KBN_CD = 3
		GROUP BY
			CONTENA.CONTENA_SHURUI_CD, CONTENA.CONTENA_CD, URSHE.GYOUSHA_CD, URSHE.GENBA_CD, URSHE.DENPYOU_DATE
	) AS TEMP_DATA
		ON CONTENA.CONTENA_SHURUI_CD = TEMP_DATA.CONTENA_SHURUI_CD
		AND CONTENA.CONTENA_CD = TEMP_DATA.CONTENA_CD
		AND URSHE.DENPYOU_DATE = TEMP_DATA.DENPYOU_DATE
		AND URSHE.GYOUSHA_CD = TEMP_DATA.GYOUSHA_CD
		AND URSHE.GENBA_CD = TEMP_DATA.GENBA_CD
	INNER JOIN M_GYOUSHA
		ON URSHE.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD 
	INNER JOIN M_GENBA
		ON URSHE.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND URSHE.GENBA_CD = M_GENBA.GENBA_CD
	LEFT JOIN M_SHAIN
		ON M_GENBA.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD 
	INNER JOIN M_CONTENA_SHURUI
		ON CONTENA.CONTENA_SHURUI_CD = M_CONTENA_SHURUI.CONTENA_SHURUI_CD
	INNER JOIN M_CONTENA
		ON CONTENA.CONTENA_SHURUI_CD = M_CONTENA.CONTENA_SHURUI_CD
		AND CONTENA.CONTENA_CD = M_CONTENA.CONTENA_CD
WHERE 
	CONTENA.DELETE_FLG = 0
	AND CONTENA.DENSHU_KBN_CD = 3
	AND (CONTENA.CONTENA_CD IS NOT NULL AND CONTENA.CONTENA_CD <> '')
	/*IF data.GYOUSHA_CD != null*/AND URSHE.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
	/*IF data.CONTENA_SHURUI_CD != null*/AND CONTENA.CONTENA_SHURUI_CD = /*data.CONTENA_SHURUI_CD*//*END*/
	/*IF data.GENBA_CD != null*/AND URSHE.GENBA_CD = /*data.GENBA_CD*//*END*/
	/*IF data.CONTENA_CD != null*/AND CONTENA.CONTENA_CD = /*data.CONTENA_CD*//*END*/
	/*IF data.EIGYOU_TANTOU_CD != null*/AND M_GENBA.EIGYOU_TANTOU_CD = /*data.EIGYOU_TANTOU_CD*//*END*/
)

UNION ALL (

SELECT
	'' AS SecchiChouhuku,
	CONTENA.CONTENA_SHURUI_CD,
	M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU,
	CONTENA.CONTENA_CD,
	M_CONTENA.CONTENA_NAME_RYAKU,
	UKETSUKE.GYOUSHA_CD,
	M_GYOUSHA.GYOUSHA_NAME_RYAKU,
	UKETSUKE.GENBA_CD,
	M_GENBA.GENBA_NAME_RYAKU,
	M_GENBA.EIGYOU_TANTOU_CD,
	M_SHAIN.SHAIN_NAME_RYAKU,
	CONTENA.CONTENA_SET_KBN,
	CONVERT(VARCHAR,UKETSUKE.SAGYOU_DATE,111) AS SECCHI_DATE,
	DATEDIFF(day, UKETSUKE.SAGYOU_DATE, /*data.SAGYOU_DATE*/) AS DAYSCOUNT
FROM
	T_CONTENA_RESERVE AS CONTENA
	INNER JOIN T_UKETSUKE_SS_ENTRY AS UKETSUKE
		ON CONTENA.SYSTEM_ID = UKETSUKE.SYSTEM_ID
		AND CONTENA.SEQ = UKETSUKE.SEQ
		AND UKETSUKE.DELETE_FLG = 0
	INNER JOIN 
	(
		SELECT
			CONTENA.CONTENA_SHURUI_CD,
			CONTENA.CONTENA_CD,
			UKETSUKE.SAGYOU_DATE AS SAGYOU_DATE,
			UKETSUKE.GYOUSHA_CD,
			UKETSUKE.GENBA_CD
		FROM
			T_CONTENA_RESERVE AS CONTENA
			INNER JOIN T_UKETSUKE_SS_ENTRY AS UKETSUKE
			ON CONTENA.SYSTEM_ID = UKETSUKE.SYSTEM_ID
			AND CONTENA.SEQ = UKETSUKE.SEQ
			AND UKETSUKE.DELETE_FLG = 0
		WHERE
			CONTENA.DELETE_FLG = 0
			AND (UKETSUKE.HAISHA_JOKYO_CD = 3 OR UKETSUKE.HAISHA_JOKYO_CD = 5)
		GROUP BY
			CONTENA.CONTENA_SHURUI_CD, CONTENA.CONTENA_CD, UKETSUKE.GYOUSHA_CD, UKETSUKE.GENBA_CD, UKETSUKE.SAGYOU_DATE
	) AS TEMP_DATA
		ON CONTENA.CONTENA_SHURUI_CD = TEMP_DATA.CONTENA_SHURUI_CD
		AND CONTENA.CONTENA_CD = TEMP_DATA.CONTENA_CD
		AND UKETSUKE.SAGYOU_DATE = TEMP_DATA.SAGYOU_DATE
		AND UKETSUKE.GYOUSHA_CD = TEMP_DATA.GYOUSHA_CD
		AND UKETSUKE.GENBA_CD = TEMP_DATA.GENBA_CD
	INNER JOIN M_GYOUSHA
		ON UKETSUKE.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD 
	INNER JOIN M_GENBA
		ON UKETSUKE.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND UKETSUKE.GENBA_CD = M_GENBA.GENBA_CD
	LEFT JOIN M_SHAIN
		ON M_GENBA.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD 
	INNER JOIN M_CONTENA_SHURUI
		ON CONTENA.CONTENA_SHURUI_CD = M_CONTENA_SHURUI.CONTENA_SHURUI_CD
	INNER JOIN M_CONTENA
		ON CONTENA.CONTENA_SHURUI_CD = M_CONTENA.CONTENA_SHURUI_CD
		AND CONTENA.CONTENA_CD = M_CONTENA.CONTENA_CD
WHERE 
	CONTENA.DELETE_FLG = 0
	AND (CONTENA.CONTENA_CD IS NOT NULL AND CONTENA.CONTENA_CD <> '')
	AND (UKETSUKE.HAISHA_JOKYO_CD = 3 OR UKETSUKE.HAISHA_JOKYO_CD = 5)
	/*IF data.GYOUSHA_CD != null*/AND UKETSUKE.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
	/*IF data.CONTENA_SHURUI_CD != null*/AND CONTENA.CONTENA_SHURUI_CD = /*data.CONTENA_SHURUI_CD*//*END*/
	/*IF data.GENBA_CD != null*/AND UKETSUKE.GENBA_CD = /*data.GENBA_CD*//*END*/
	/*IF data.CONTENA_CD != null*/AND CONTENA.CONTENA_CD = /*data.CONTENA_CD*//*END*/
	/*IF data.EIGYOU_TANTOU_CD != null*/AND M_GENBA.EIGYOU_TANTOU_CD = /*data.EIGYOU_TANTOU_CD*//*END*/
)

ORDER BY GYOUSHA_CD, GENBA_CD, SECCHI_DATE