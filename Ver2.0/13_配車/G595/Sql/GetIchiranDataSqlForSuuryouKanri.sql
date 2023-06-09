﻿SELECT
ISNULL(T_CONTENA_RESULT.DENSHU_KBN_CD, 0) AS DENSHU_KBN_CD,
T_CONTENA_RESULT.CONTENA_SHURUI_CD AS CONTENA_SHURUI_CD,
M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU AS CONTENA_SHURUI_NAME_RYAKU,
T_UKEIRE_ENTRY.GYOUSHA_CD AS GYOUSHA_CD,
M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU,
ISNULL(T_UKEIRE_ENTRY.GENBA_CD, '') AS GENBA_CD,
ISNULL(M_GENBA.GENBA_NAME_RYAKU, '') AS GENBA_NAME_RYAKU,
CONVERT(VARCHAR,T_UKEIRE_ENTRY.DENPYOU_DATE,111) AS SECCHI_DATE,
T_CONTENA_RESULT.CONTENA_SET_KBN AS CONTENA_SET_KBN,
ISNULL(T_CONTENA_RESULT.DAISUU_CNT, 0) AS DAISUU_CNT,
T_UKEIRE_ENTRY.UPDATE_DATE AS UPDATE_DATE

FROM
T_CONTENA_RESULT
LEFT JOIN M_CONTENA_SHURUI
	ON M_CONTENA_SHURUI.CONTENA_SHURUI_CD = T_CONTENA_RESULT.CONTENA_SHURUI_CD
INNER JOIN T_UKEIRE_ENTRY
	ON T_UKEIRE_ENTRY.SYSTEM_ID = T_CONTENA_RESULT.SYSTEM_ID
	AND T_UKEIRE_ENTRY.SEQ = T_CONTENA_RESULT.SEQ
	AND T_UKEIRE_ENTRY.DELETE_FLG = 0
	AND T_CONTENA_RESULT.DENSHU_KBN_CD = 1
LEFT JOIN M_GYOUSHA
	ON M_GYOUSHA.GYOUSHA_CD = T_UKEIRE_ENTRY.GYOUSHA_CD
LEFT JOIN M_GENBA
	ON M_GENBA.GYOUSHA_CD = T_UKEIRE_ENTRY.GYOUSHA_CD
	AND M_GENBA.GENBA_CD = T_UKEIRE_ENTRY.GENBA_CD
WHERE 
T_CONTENA_RESULT.DENSHU_KBN_CD = 1
AND T_CONTENA_RESULT.DELETE_FLG = 0
AND T_CONTENA_RESULT.CONTENA_SHURUI_CD IS NOT NULL
AND T_UKEIRE_ENTRY.GYOUSHA_CD IS NOT NULL
/*IF data.kyotenCd != null*/
	AND T_UKEIRE_ENTRY.KYOTEN_CD = /*data.kyotenCd*/
/*END*/
/*IF data.dateFrom != null*/
	AND T_UKEIRE_ENTRY.DENPYOU_DATE >= /*data.dateFrom*/
/*END*/
/*IF data.dateTo != null*/
	AND T_UKEIRE_ENTRY.DENPYOU_DATE <= /*data.dateTo*/
/*END*/
/*IF data.gyoushaGenbaSetting != null && data.gyoushaGenbaSetting == 1*/
	/*IF data.gyoushaFrom != null */
		AND T_UKEIRE_ENTRY.GYOUSHA_CD = /*data.gyoushaFrom*/
	/*END*/
	/*IF data.genbaFrom != null */
		AND T_UKEIRE_ENTRY.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null */
		AND T_UKEIRE_ENTRY.GENBA_CD <= /*data.genbaTo*/
	/*END*/
/*END*/
/*IF data.gyoushaGenbaSetting != null && data.gyoushaGenbaSetting == 2*/
	/*IF data.gyoushaFrom != null */
		AND T_UKEIRE_ENTRY.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null */
		AND T_UKEIRE_ENTRY.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
/*END*/
/*IF data.contenaShuruiFrom != null*/
	AND T_CONTENA_RESULT.CONTENA_SHURUI_CD >= /*data.contenaShuruiFrom*/
/*END*/
/*IF data.contenaShuruiTo != null*/
	AND T_CONTENA_RESULT.CONTENA_SHURUI_CD <= /*data.contenaShuruiTo*/
/*END*/
/*IF data.sousaKbn != null && data.sousaKbn == 1*/
	AND T_CONTENA_RESULT.CONTENA_SET_KBN = 1
/*END*/
/*IF data.sousaKbn != null && data.sousaKbn == 2*/
	AND T_CONTENA_RESULT.CONTENA_SET_KBN = 2
/*END*/

UNION ALL (
SELECT
ISNULL(T_CONTENA_RESULT.DENSHU_KBN_CD, 0) AS DENSHU_KBN_CD,
T_CONTENA_RESULT.CONTENA_SHURUI_CD AS CONTENA_SHURUI_CD,
M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU AS CONTENA_SHURUI_NAME_RYAKU,
T_UR_SH_ENTRY.GYOUSHA_CD AS GYOUSHA_CD,
M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU,
ISNULL(T_UR_SH_ENTRY.GENBA_CD, '') AS GENBA_CD,
ISNULL(M_GENBA.GENBA_NAME_RYAKU, '') AS GENBA_NAME_RYAKU,
CONVERT(VARCHAR,T_UR_SH_ENTRY.DENPYOU_DATE,111) AS SECCHI_DATE,
T_CONTENA_RESULT.CONTENA_SET_KBN AS CONTENA_SET_KBN,
ISNULL(T_CONTENA_RESULT.DAISUU_CNT, 0) AS DAISUU_CNT,
T_UR_SH_ENTRY.UPDATE_DATE AS UPDATE_DATE

FROM
T_CONTENA_RESULT
LEFT JOIN M_CONTENA_SHURUI
	ON M_CONTENA_SHURUI.CONTENA_SHURUI_CD = T_CONTENA_RESULT.CONTENA_SHURUI_CD
INNER JOIN T_UR_SH_ENTRY
	ON T_UR_SH_ENTRY.SYSTEM_ID = T_CONTENA_RESULT.SYSTEM_ID
	AND T_UR_SH_ENTRY.SEQ = T_CONTENA_RESULT.SEQ
	AND T_UR_SH_ENTRY.DELETE_FLG = 0
	AND T_CONTENA_RESULT.DENSHU_KBN_CD = 3
LEFT JOIN M_GYOUSHA
	ON M_GYOUSHA.GYOUSHA_CD = T_UR_SH_ENTRY.GYOUSHA_CD
LEFT JOIN M_GENBA
	ON M_GENBA.GYOUSHA_CD = T_UR_SH_ENTRY.GYOUSHA_CD
	AND M_GENBA.GENBA_CD = T_UR_SH_ENTRY.GENBA_CD
WHERE 
T_CONTENA_RESULT.DENSHU_KBN_CD = 3
AND T_CONTENA_RESULT.DELETE_FLG = 0
AND T_CONTENA_RESULT.CONTENA_SHURUI_CD IS NOT NULL
AND T_UR_SH_ENTRY.GYOUSHA_CD IS NOT NULL
/*IF data.kyotenCd != null*/
	AND T_UR_SH_ENTRY.KYOTEN_CD = /*data.kyotenCd*/
/*END*/
/*IF data.dateFrom != null*/
	AND T_UR_SH_ENTRY.DENPYOU_DATE >= /*data.dateFrom*/
/*END*/
/*IF data.dateTo != null*/
	AND T_UR_SH_ENTRY.DENPYOU_DATE <= /*data.dateTo*/
/*END*/
/*IF data.gyoushaGenbaSetting != null && data.gyoushaGenbaSetting == 1*/
	/*IF data.gyoushaFrom != null */
		AND T_UR_SH_ENTRY.GYOUSHA_CD = /*data.gyoushaFrom*/
	/*END*/
	/*IF data.genbaFrom != null */
		AND T_UR_SH_ENTRY.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null */
		AND T_UR_SH_ENTRY.GENBA_CD <= /*data.genbaTo*/
	/*END*/
/*END*/
/*IF data.gyoushaGenbaSetting != null && data.gyoushaGenbaSetting == 2*/
	/*IF data.gyoushaFrom != null */
		AND T_UR_SH_ENTRY.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null */
		AND T_UR_SH_ENTRY.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
/*END*/
/*IF data.contenaShuruiFrom != null*/
	AND T_CONTENA_RESULT.CONTENA_SHURUI_CD >= /*data.contenaShuruiFrom*/
/*END*/
/*IF data.contenaShuruiTo != null*/
	AND T_CONTENA_RESULT.CONTENA_SHURUI_CD <= /*data.contenaShuruiTo*/
/*END*/
/*IF data.sousaKbn != null && data.sousaKbn == 1*/
	AND T_CONTENA_RESULT.CONTENA_SET_KBN = 1
/*END*/
/*IF data.sousaKbn != null && data.sousaKbn == 2*/
	AND T_CONTENA_RESULT.CONTENA_SET_KBN = 2
/*END*/
)

UNION ALL(
SELECT
'100' AS DENSHU_KBN_CD,
T_CONTENA_RESERVE.CONTENA_SHURUI_CD AS CONTENA_SHURUI_CD,
M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU AS CONTENA_SHURUI_NAME_RYAKU,
T_UKETSUKE_SS_ENTRY.GYOUSHA_CD AS GYOUSHA_CD,
M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU,
ISNULL(T_UKETSUKE_SS_ENTRY.GENBA_CD, '') AS GENBA_CD,
ISNULL(M_GENBA.GENBA_NAME_RYAKU, '') AS GENBA_NAME_RYAKU,
CONVERT(VARCHAR,T_UKETSUKE_SS_ENTRY.SAGYOU_DATE,111) AS SECCHI_DATE,
T_CONTENA_RESERVE.CONTENA_SET_KBN AS CONTENA_SET_KBN,
ISNULL(T_CONTENA_RESERVE.DAISUU_CNT, 0) AS DAISUU_CNT,
T_UKETSUKE_SS_ENTRY.UPDATE_DATE AS UPDATE_DATE

FROM
T_CONTENA_RESERVE
LEFT JOIN M_CONTENA_SHURUI
	ON M_CONTENA_SHURUI.CONTENA_SHURUI_CD = T_CONTENA_RESERVE.CONTENA_SHURUI_CD
INNER JOIN T_UKETSUKE_SS_ENTRY
	ON T_UKETSUKE_SS_ENTRY.SYSTEM_ID = T_CONTENA_RESERVE.SYSTEM_ID
	AND T_UKETSUKE_SS_ENTRY.SEQ = T_CONTENA_RESERVE.SEQ
	AND T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0
	AND (T_UKETSUKE_SS_ENTRY.HAISHA_JOKYO_CD = '5' OR T_UKETSUKE_SS_ENTRY.HAISHA_JOKYO_CD = '3')
	AND T_CONTENA_RESERVE.CALC_DAISUU_FLG = 'true'
LEFT JOIN M_GYOUSHA
	ON M_GYOUSHA.GYOUSHA_CD = T_UKETSUKE_SS_ENTRY.GYOUSHA_CD
LEFT JOIN M_GENBA
	ON M_GENBA.GYOUSHA_CD = T_UKETSUKE_SS_ENTRY.GYOUSHA_CD
	AND M_GENBA.GENBA_CD = T_UKETSUKE_SS_ENTRY.GENBA_CD
WHERE 
T_CONTENA_RESERVE.DELETE_FLG = 0
AND T_CONTENA_RESERVE.CONTENA_SHURUI_CD IS NOT NULL
AND T_UKETSUKE_SS_ENTRY.GYOUSHA_CD IS NOT NULL
/*IF data.kyotenCd != null*/
	AND T_UKETSUKE_SS_ENTRY.KYOTEN_CD = /*data.kyotenCd*/
/*END*/
/*IF data.dateFrom != null*/
	AND T_UKETSUKE_SS_ENTRY.SAGYOU_DATE >= /*data.dateFrom*/
/*END*/
/*IF data.dateTo != null*/
	AND T_UKETSUKE_SS_ENTRY.SAGYOU_DATE <= /*data.dateTo*/
/*END*/
/*IF data.gyoushaGenbaSetting != null && data.gyoushaGenbaSetting == 1*/
	/*IF data.gyoushaFrom != null */
		AND T_UKETSUKE_SS_ENTRY.GYOUSHA_CD = /*data.gyoushaFrom*/
	/*END*/
	/*IF data.genbaFrom != null */
		AND T_UKETSUKE_SS_ENTRY.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null */
		AND T_UKETSUKE_SS_ENTRY.GENBA_CD <= /*data.genbaTo*/
	/*END*/
/*END*/
/*IF data.gyoushaGenbaSetting != null && data.gyoushaGenbaSetting == 2*/
	/*IF data.gyoushaFrom != null */
		AND T_UKETSUKE_SS_ENTRY.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null */
		AND T_UKETSUKE_SS_ENTRY.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
/*END*/
/*IF data.contenaShuruiFrom != null*/
	AND T_CONTENA_RESERVE.CONTENA_SHURUI_CD >= /*data.contenaShuruiFrom*/
/*END*/
/*IF data.contenaShuruiTo != null*/
	AND T_CONTENA_RESERVE.CONTENA_SHURUI_CD <= /*data.contenaShuruiTo*/
/*END*/
/*IF data.sousaKbn != null && data.sousaKbn == 1*/
	AND T_CONTENA_RESERVE.CONTENA_SET_KBN = 1
/*END*/
/*IF data.sousaKbn != null && data.sousaKbn == 2*/
	AND T_CONTENA_RESERVE.CONTENA_SET_KBN = 2
/*END*/
)