﻿SELECT
	 TJ.*
	,GYOUSHA_N.GYOUSHA_NAME1 AS NIOROSHI_GYOUSHA_NAME1
	,GYOUSHA_N.GYOUSHA_NAME2 AS NIOROSHI_GYOUSHA_NAME2
	,GENBA_N.GENBA_NAME1 AS NIOROSHI_GENBA_NAME1
	,GENBA_N.GENBA_NAME2 AS NIOROSHI_GENBA_NAME2
	,ISNULL(GENBA_N.SHIKUCHOUSON_CD,'')     AS SHIKUCHOUSON_CD
	,HINMEI.HINMEI_NAME						AS HINMEI_NAME
	,UNIT.UNIT_NAME_RYAKU					AS UNIT_NAME_RYAKU
	,KANSAN_UNIT.UNIT_NAME_RYAKU			AS KANSAN_UNIT_NAME_RYAKU
	,ANBUN_UNIT.UNIT_CD						AS ANBUN_UNIT_CD
	,ANBUN_UNIT.UNIT_NAME_RYAKU				AS ANBUN_UNIT_NAME_RYAKU
	,SHIKUCHOUSON.SHIKUCHOUSON_NAME_RYAKU	AS SHIKUCHOUSON_NAME_RYAKU
	,JBUNRUI.JISSEKI_BUNRUI_CD				AS JISSEKI_BUNRUI_CD
	,JBUNRUI.JISSEKI_BUNRUI_NAME			AS JISSEKI_BUNRUI_NAME
	,/*$data.SHUUKEISUURYOU*/0				AS SHUUKEISUURYOU
 FROM
 (
	 SELECT
		TJ_NIOROSHI.NIOROSHI_GYOUSHA_CD
		,TJ_NIOROSHI.NIOROSHI_GENBA_CD
		,TJ_DETAIL.HINMEI_CD
		,TJ_DETAIL.DENPYOU_KBN_CD
		,TJ_DETAIL.UNIT_CD
		,TJ_DETAIL.KANSAN_UNIT_CD
		,SUM(TJ_DETAIL.SUURYOU)				AS SUURYOU
		,SUM(TJ_DETAIL.KANSAN_SUURYOU)		AS KANSAN_SUURYOU
		,SUM(TJ_DETAIL.ANBUN_SUURYOU)		AS ANBUN_SUURYOU
	 FROM
		T_TEIKI_JISSEKI_ENTRY AS TJ_ENTRY
	 INNER JOIN
		T_TEIKI_JISSEKI_NIOROSHI AS TJ_NIOROSHI ON TJ_ENTRY.SYSTEM_ID = TJ_NIOROSHI.SYSTEM_ID AND TJ_ENTRY.SEQ = TJ_NIOROSHI.SEQ
	 INNER JOIN
		(SELECT SYSTEM_ID,SEQ,GYOUSHA_CD,GENBA_CD,HINMEI_CD,DENPYOU_KBN_CD,KEIYAKU_KBN,UNIT_CD,KANSAN_UNIT_CD,SUURYOU,KANSAN_SUURYOU,ANBUN_SUURYOU,
             CASE WHEN ROW_NUMBER NOT IN (SELECT ROW_NUMBER FROM T_TEIKI_JISSEKI_NIOROSHI WHERE SYSTEM_ID = DETAIL.SYSTEM_ID AND SEQ = DETAIL.SEQ) THEN 1
             ELSE ROW_NUMBER END AS ROW_NUMBER
         FROM T_TEIKI_JISSEKI_DETAIL DETAIL) AS TJ_DETAIL ON TJ_NIOROSHI.SYSTEM_ID = TJ_DETAIL.SYSTEM_ID AND TJ_NIOROSHI.SEQ = TJ_DETAIL.SEQ AND TJ_NIOROSHI.ROW_NUMBER = TJ_DETAIL.ROW_NUMBER
	 WHERE
		TJ_ENTRY.DELETE_FLG = 0
		/*IF !data.KYOTEN_CD.IsNull*/AND TJ_ENTRY.KYOTEN_CD = /*data.KYOTEN_CD.Value*/0/*END*/
		/*IF !data.KIKAN_FROM.IsNull*/AND CONVERT(varchar, TJ_ENTRY.SAGYOU_DATE, 120) >= /*data.KIKAN_FROM.Value*/''/*END*/
		/*IF !data.KIKAN_TO.IsNull*/AND CONVERT(varchar, TJ_ENTRY.SAGYOU_DATE, 120) <= /*data.KIKAN_TO.Value*/''/*END*/
	 GROUP BY
		TJ_DETAIL.HINMEI_CD
		,TJ_DETAIL.DENPYOU_KBN_CD
		,TJ_DETAIL.UNIT_CD
		,TJ_DETAIL.KANSAN_UNIT_CD
		,TJ_NIOROSHI.NIOROSHI_GYOUSHA_CD
		,TJ_NIOROSHI.NIOROSHI_GENBA_CD
 ) AS TJ
 INNER JOIN
	M_HINMEI AS HINMEI ON TJ.HINMEI_CD = HINMEI.HINMEI_CD
 LEFT JOIN
	M_UNIT AS UNIT ON TJ.UNIT_CD = UNIT.UNIT_CD
 LEFT JOIN
	M_UNIT AS KANSAN_UNIT ON TJ.KANSAN_UNIT_CD = KANSAN_UNIT.UNIT_CD
 LEFT JOIN
	M_UNIT AS ANBUN_UNIT ON ANBUN_UNIT.UNIT_CD = 3
 LEFT JOIN
	M_GYOUSHA AS GYOUSHA_N ON TJ.NIOROSHI_GYOUSHA_CD = GYOUSHA_N.GYOUSHA_CD
 LEFT JOIN
	M_GENBA AS GENBA_N ON TJ.NIOROSHI_GENBA_CD = GENBA_N.GENBA_CD AND TJ.NIOROSHI_GYOUSHA_CD = GENBA_N.GYOUSHA_CD
 LEFT JOIN
	M_SHIKUCHOUSON AS SHIKUCHOUSON ON GENBA_N.SHIKUCHOUSON_CD = SHIKUCHOUSON.SHIKUCHOUSON_CD
 LEFT JOIN
	M_JISSEKI_BUNRUI AS JBUNRUI ON HINMEI.JISSEKI_BUNRUI_CD = JBUNRUI.JISSEKI_BUNRUI_CD
 WHERE
	1 = 1
	AND ISNULL(JBUNRUI.DELETE_FLG, 0) = 0
	/*IF data.SHIKUCHOUSON_CD != null*/AND GENBA_D.SHIKUCHOUSON_CD = /*data.SHIKUCHOUSON_CD*/''/*END*/
 ORDER BY
	 TJ.NIOROSHI_GYOUSHA_CD
	,TJ.NIOROSHI_GENBA_CD
	,TJ.HINMEI_CD
