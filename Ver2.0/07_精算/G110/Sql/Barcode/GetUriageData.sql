﻿SELECT
	URSE.UR_SH_NUMBER AS DENPYOU_NUMBER, 
	URSE.DENPYOU_DATE, 
	URSE.SHIHARAI_DATE AS SEISAN_SHIMEBI, 
	URSE.GYOUSHA_NAME, 
	URSE.GENBA_NAME, 
	(ISNULL(URSE.SHIHARAI_AMOUNT_TOTAL, 0) + ISNULL(URSE.HINMEI_SHIHARAI_KINGAKU_TOTAL, 0)) AS KINGAKU,
	URSE.TORIHIKISAKI_CD,
	MT.TORIHIKISAKI_NAME_RYAKU,
	CASE WHEN MTS.SHIMEBI1 IS NOT NULL 
			THEN MTS.SHIMEBI1 
		ELSE 
			CASE WHEN MTS.SHIMEBI2 IS NOT NULL 
					THEN MTS.SHIMEBI2
				ELSE MTS.SHIMEBI3
			END
	END AS SHIMEBI,
	URSE.SHIHARAI_ZEI_KEISAN_KBN_CD,
	ISNULL(HIN_COUNT.HIN_SOTO,0) AS HINMEI_SOTO_ZEI_COUNT,
	ISNULL(HIN_COUNT.HIN_NASI,0) AS HINMEI_NASI_ZEI_COUNT, 
	URSE.SHIHARAI_ZEI_KBN_CD
FROM
	T_UR_SH_ENTRY URSE
	LEFT JOIN M_TORIHIKISAKI AS MT ON MT.TORIHIKISAKI_CD = URSE.TORIHIKISAKI_CD
	LEFT JOIN M_TORIHIKISAKI_SHIHARAI AS MTS ON MTS.TORIHIKISAKI_CD = URSE.TORIHIKISAKI_CD
	LEFT JOIN 
		(
			SELECT 
				COUNT_TABLE.SYSTEM_ID, COUNT(COUNT_TABLE.SOTO) AS HIN_SOTO, COUNT(COUNT_TABLE.NASI) AS HIN_NASI
			FROM (
				SELECT
					E.SYSTEM_ID,
					CASE WHEN HINMEI_ZEI_KBN_CD = 1 THEN COUNT(HINMEI_ZEI_KBN_CD) END AS SOTO,
					CASE WHEN HINMEI_ZEI_KBN_CD is null THEN COUNT(HINMEI_ZEI_KBN_CD) END AS NASI
				FROM
					T_UR_SH_ENTRY E INNER JOIN T_UR_SH_DETAIL D
				ON E.SYSTEM_ID = D.SYSTEM_ID 
					AND E.SEQ = D.SEQ 
					AND E.UR_SH_NUMBER = /*data.DENPYOU_NUMBER*/
					/*IF data.KYOTEN_CD != 99*/AND E.KYOTEN_CD = /*data.KYOTEN_CD*//*END*/ 		
					AND E.SHIHARAI_TORIHIKI_KBN_CD = 2 
					AND E.KAKUTEI_KBN = 1 
					AND D.DENPYOU_KBN_CD = 2
					AND E.DELETE_FLG = 0
					/*IF data.SHIHARAI_CD != null && data.SHIHARAI_CD != ""*/AND E.TORIHIKISAKI_CD = /*data.SHIHARAI_CD*//*END*/ 
					/*IF data.SHIHARAISHIMEBI_FROM != null && data.SHIHARAISHIMEBI_FROM != ""*/
					AND CONVERT(DATETIME, E.SHIHARAI_DATE,111) >= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_FROM*/null,111)
					/*END*/ 
					/*IF data.SHIHARAISHIMEBI_TO != null && data.SHIHARAISHIMEBI_TO != ""*/
					AND CONVERT(DATETIME, E.SHIHARAI_DATE,111) <= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_TO*/,111)
					/*END*/ 
				GROUP BY E.SYSTEM_ID,HINMEI_ZEI_KBN_CD
			) AS COUNT_TABLE
		    GROUP BY COUNT_TABLE.SYSTEM_ID
		) AS HIN_COUNT
	ON URSE.SYSTEM_ID = HIN_COUNT.SYSTEM_ID
WHERE
	URSE.UR_SH_NUMBER = /*data.DENPYOU_NUMBER*/
	AND (EXISTS
		(
		SELECT
			DISTINCT E.SYSTEM_ID
		FROM
			T_UR_SH_ENTRY E INNER JOIN T_UR_SH_DETAIL D
		ON E.SYSTEM_ID = D.SYSTEM_ID 
			AND E.SEQ = D.SEQ 
	        /*IF data.KYOTEN_CD != 99*/AND E.KYOTEN_CD = /*data.KYOTEN_CD*//*END*/ 		
			AND E.SHIHARAI_TORIHIKI_KBN_CD = 2 
			AND E.KAKUTEI_KBN = 1 
			AND D.DENPYOU_KBN_CD = 2
			AND E.DELETE_FLG = 0
			/*IF data.SHIHARAI_CD != null && data.SHIHARAI_CD != ""*/AND E.TORIHIKISAKI_CD = /*data.SHIHARAI_CD*//*END*/ 
			/*IF data.SHIHARAISHIMEBI_FROM != null && data.SHIHARAISHIMEBI_FROM != ""*/
			AND CONVERT(DATETIME, E.SHIHARAI_DATE,111) >= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_FROM*/null,111)
			/*END*/ 
			/*IF data.SHIHARAISHIMEBI_TO != null && data.SHIHARAISHIMEBI_TO != ""*/
			AND CONVERT(DATETIME, E.SHIHARAI_DATE,111) <= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_TO*/,111)
			/*END*/ 
		WHERE URSE.SYSTEM_ID = E.SYSTEM_ID
		)
	)
	AND (NOT EXISTS
	    (
	    SELECT
	        URSE.SYSTEM_ID, URSE.SEQ
	    FROM              
	        T_SEISAN_DETAIL SEID
	    WHERE
	        URSE.SYSTEM_ID = SEID.DENPYOU_SYSTEM_ID
	        AND URSE.SEQ = SEID.DENPYOU_SEQ
	        AND SEID.DENPYOU_SHURUI_CD = 3
	        AND SEID.DELETE_FLG = 0
	    )
		)
	AND URSE.DELETE_FLG = 0
		