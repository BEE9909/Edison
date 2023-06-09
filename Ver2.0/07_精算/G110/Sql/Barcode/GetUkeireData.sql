﻿SELECT
	UKE.UKEIRE_NUMBER AS DENPYOU_NUMBER, 
	UKE.DENPYOU_DATE, 
	UKE.SHIHARAI_DATE AS SEISAN_SHIMEBI, 
	UKE.GYOUSHA_NAME, 
	UKE.GENBA_NAME, 
	(ISNULL(UKE.SHIHARAI_KINGAKU_TOTAL, 0) + ISNULL(UKE.HINMEI_SHIHARAI_KINGAKU_TOTAL, 0)) AS KINGAKU,
	UKE.TORIHIKISAKI_CD,
	MT.TORIHIKISAKI_NAME_RYAKU,
	CASE WHEN MTS.SHIMEBI1 IS NOT NULL 
			THEN MTS.SHIMEBI1 
		ELSE 
			CASE WHEN MTS.SHIMEBI2 IS NOT NULL 
					THEN MTS.SHIMEBI2
				ELSE MTS.SHIMEBI3
			END
	END AS SHIMEBI,
	UKE.SHIHARAI_ZEI_KEISAN_KBN_CD,
	ISNULL(HIN_COUNT.HIN_SOTO,0) AS HINMEI_SOTO_ZEI_COUNT,
	ISNULL(HIN_COUNT.HIN_NASI,0) AS HINMEI_NASI_ZEI_COUNT, 
	UKE.SHIHARAI_ZEI_KBN_CD
FROM
	T_UKEIRE_ENTRY UKE
	LEFT JOIN M_TORIHIKISAKI AS MT ON MT.TORIHIKISAKI_CD = UKE.TORIHIKISAKI_CD
	LEFT JOIN M_TORIHIKISAKI_SHIHARAI AS MTS ON MTS.TORIHIKISAKI_CD = UKE.TORIHIKISAKI_CD
	LEFT JOIN 
		(
			SELECT 
				COUNT_TABLE.SYSTEM_ID, COUNT(COUNT_TABLE.SOTO) AS HIN_SOTO, COUNT(COUNT_TABLE.NASI) AS HIN_NASI
			FROM (
				SELECT
					UE.SYSTEM_ID,
					CASE WHEN HINMEI_ZEI_KBN_CD = 1 THEN COUNT(HINMEI_ZEI_KBN_CD) END AS SOTO,
					CASE WHEN HINMEI_ZEI_KBN_CD is null THEN COUNT(HINMEI_ZEI_KBN_CD) END AS NASI
				FROM
					T_UKEIRE_ENTRY UE INNER JOIN T_UKEIRE_DETAIL UD
				ON UE.SYSTEM_ID = UD.SYSTEM_ID 
					AND UE.SEQ = UD.SEQ 
					AND UE.UKEIRE_NUMBER = /*data.DENPYOU_NUMBER*/
					/*IF data.KYOTEN_CD != 99*/AND UE.KYOTEN_CD = /*data.KYOTEN_CD*//*END*/ 		
					AND UE.SHIHARAI_TORIHIKI_KBN_CD = 2 
					AND UE.KAKUTEI_KBN = 1 
					AND UE.TAIRYUU_KBN = 0 
					AND UD.DENPYOU_KBN_CD = 2 AND UE.DELETE_FLG = 0
					/*IF data.SHIHARAI_CD != null && data.SHIHARAI_CD != ""*/AND UE.TORIHIKISAKI_CD = /*data.SHIHARAI_CD*//*END*/ 
					/*IF data.SHIHARAISHIMEBI_FROM != null && data.SHIHARAISHIMEBI_FROM != ""*/
					AND CONVERT(DATETIME, UE.SHIHARAI_DATE,111) >= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_FROM*/null,111)
					/*END*/ 
					/*IF data.SHIHARAISHIMEBI_TO != null && data.SHIHARAISHIMEBI_TO != ""*/
					AND CONVERT(DATETIME, UE.SHIHARAI_DATE,111) <= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_TO*/,111)
					/*END*/ 
				GROUP BY UE.SYSTEM_ID,HINMEI_ZEI_KBN_CD
			) AS COUNT_TABLE
			GROUP BY COUNT_TABLE.SYSTEM_ID
		) AS HIN_COUNT
	ON UKE.SYSTEM_ID = HIN_COUNT.SYSTEM_ID
WHERE
	UKE.UKEIRE_NUMBER = /*data.DENPYOU_NUMBER*/
	AND (EXISTS
		(
		SELECT
			DISTINCT UE.SYSTEM_ID
		FROM
			T_UKEIRE_ENTRY UE INNER JOIN T_UKEIRE_DETAIL UD
		ON UE.SYSTEM_ID = UD.SYSTEM_ID 
			AND UE.SEQ = UD.SEQ 
	        /*IF data.KYOTEN_CD != 99*/AND UE.KYOTEN_CD = /*data.KYOTEN_CD*//*END*/ 		
			AND UE.SHIHARAI_TORIHIKI_KBN_CD = 2 
			AND UE.KAKUTEI_KBN = 1 
			AND UE.TAIRYUU_KBN = 0 
			AND UD.DENPYOU_KBN_CD = 2 AND UE.DELETE_FLG = 0
			/*IF data.SHIHARAI_CD != null && data.SHIHARAI_CD != ""*/AND UE.TORIHIKISAKI_CD = /*data.SHIHARAI_CD*//*END*/ 
			/*IF data.SHIHARAISHIMEBI_FROM != null && data.SHIHARAISHIMEBI_FROM != ""*/
			AND CONVERT(DATETIME, UE.SHIHARAI_DATE,111) >= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_FROM*/null,111)
			/*END*/ 
			/*IF data.SHIHARAISHIMEBI_TO != null && data.SHIHARAISHIMEBI_TO != ""*/
			AND CONVERT(DATETIME, UE.SHIHARAI_DATE,111) <= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_TO*/,111)
			/*END*/ 
		WHERE UKE.SYSTEM_ID = UE.SYSTEM_ID
	)
	)
	AND (NOT EXISTS
	    (
	    SELECT
	        UKE.SYSTEM_ID, UKE.SEQ
	    FROM              
	        T_SEISAN_DETAIL SEID
	    WHERE
	        UKE.SYSTEM_ID = SEID.DENPYOU_SYSTEM_ID
	        AND UKE.SEQ = SEID.DENPYOU_SEQ
	        AND SEID.DENPYOU_SHURUI_CD = 1
	        AND SEID.DELETE_FLG = 0
	    )
		)
	AND UKE.DELETE_FLG = 0
