﻿SELECT ROW_NUMBER()OVER(ORDER BY TM.SAGYOU_DATE,TM.UKETSUKE_KBN,TM.UKETSUKE_NUMBER) ROW_NO,TM.*
FROM
(
	SELECT
	 2 AS UKETSUKE_KBN
	,T.*
	FROM (
		SELECT
			 DISTINCT
			 CAST(ENT.SAGYOU_DATE AS DATETIME) SAGYOU_DATE
			,ENT.UKETSUKE_NUMBER
			,ENT.GYOUSHA_CD
			,ENT.GYOUSHA_NAME
			,ENT.GENBA_CD
			,ENT.GENBA_NAME
			,ENT.SHARYOU_NAME
			,ENT.UNTENSHA_NAME
			,ENT.SHASHU_CD
			,ENT.SHASHU_NAME
			,ENT.SHARYOU_CD
			,ENT.UNTENSHA_CD
			,ENT.UNPAN_GYOUSHA_CD
			,ENT.SYSTEM_ID
			,ENT.SEQ
			,ENT.GENCHAKU_TIME_NAME
			,ENT.GENCHAKU_TIME
			,ENT.GENBA_TEL
			,ENT.TANTOSHA_NAME
			,ENT.TANTOSHA_TEL
			,ENT.EIGYOU_TANTOUSHA_NAME
			,ENT.UKETSUKE_BIKOU1
			,ENT.UKETSUKE_BIKOU2
			,ENT.UKETSUKE_BIKOU3
			,ENT.UNTENSHA_SIJIJIKOU1
			,ENT.UNTENSHA_SIJIJIKOU2
			,ENT.UNTENSHA_SIJIJIKOU3
		FROM
			T_UKETSUKE_SK_ENTRY ENT
			LEFT JOIN T_UKETSUKE_SK_DETAIL DET ON ENT.SYSTEM_ID = DET.SYSTEM_ID AND ENT.SEQ = DET.SEQ
			LEFT JOIN M_HINMEI HINMEI ON DET.HINMEI_CD = HINMEI.HINMEI_CD
			LEFT JOIN (
				SELECT
					HAISHA_DENPYOU_NO
				FROM
					T_MOBISYO_RT
				WHERE
					DELETE_FLG = 0
					AND HAISHA_KBN = 1
					AND HAISHA_SAGYOU_DATE >= /*data.SAGYOU_DATE_FROM*/ AND HAISHA_SAGYOU_DATE <= /*data.SAGYOU_DATE_TO*/
				GROUP BY
					HAISHA_DENPYOU_NO
				) REGISTED ON ENT.UKETSUKE_NUMBER = REGISTED.HAISHA_DENPYOU_NO
			LEFT JOIN (
				--同一品名CDの最大件数取得
				SELECT
					SYSTEM_ID, SEQ, MAX(HINMEI_COUNT) HINMEI_MAX
				FROM (
					SELECT
						SYSTEM_ID, SEQ, HINMEI_CD, COUNT(HINMEI_CD) AS HINMEI_COUNT
					FROM 
						T_UKETSUKE_SK_DETAIL
					GROUP BY
						SYSTEM_ID, SEQ, HINMEI_CD
					) AS HIN
				GROUP BY SYSTEM_ID, SEQ
			) AS HINMEI_COUNT ON HINMEI_COUNT.SYSTEM_ID = ENT.SYSTEM_ID AND HINMEI_COUNT.SEQ = ENT.SEQ
		WHERE
			ENT.DELETE_FLG = 0
			AND	ENT.UKETSUKE_NUMBER in (/*$data.Renkei_UketsukeNumber*/)
			AND ENT.SAGYOU_DATE >= /*data.SAGYOU_DATE_FROM*/ AND ENT.SAGYOU_DATE <= /*data.SAGYOU_DATE_TO*/
			AND (ENT.HAISHA_SHURUI_CD = 1 OR ENT.HAISHA_SHURUI_CD = 3)
			AND (ENT.COURSE_NAME_CD IS NULL OR ENT.COURSE_NAME_CD = '')
			AND ENT.HAISHA_JOKYO_CD = 2
			AND (ENT.UNTENSHA_CD IS NOT NULL AND ENT.UNTENSHA_CD != '')
			AND (ENT.SHARYOU_CD IS NOT NULL AND ENT.SHARYOU_CD != '')
			AND REGISTED.HAISHA_DENPYOU_NO IS NULL
			AND (DET.HINMEI_CD is NULL
				OR
				(DET.HINMEI_CD IS NOT NULL AND DET.HINMEI_CD != '') AND HINMEI.DENSHU_KBN_CD IN (3,9)
				)
			AND (ENT.SHASHU_CD IS NOT NULL AND ENT.SHASHU_CD != '')
			AND (ENT.SHASHU_NAME IS NOT NULL AND ENT.SHASHU_NAME != '')
			AND (HINMEI_COUNT.HINMEI_MAX <= 1 OR HINMEI_COUNT.HINMEI_MAX is NULL)
			AND (ENT.TORIHIKISAKI_CD IS NOT NULL AND ENT.TORIHIKISAKI_CD != '')
		)T
) TM

ORDER BY
     SAGYOU_DATE
	,UKETSUKE_KBN
	,UKETSUKE_NUMBER