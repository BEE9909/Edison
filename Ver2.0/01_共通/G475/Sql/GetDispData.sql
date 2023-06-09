﻿SELECT
	KIHON.ITAKU_KEIYAKU_NO,
	SHA.GYOUSHA_NAME_RYAKU,
	GEN.GENBA_NAME_RYAKU,
	KIHON.ITAKU_KEIYAKU_STATUS,
	KIHON.ITAKU_KEIYAKU_SHURUI,
	KIHON.KOUSHIN_SHUBETSU,
	KIHON.KEIYAKUSHO_CREATE_DATE,
	KIHON.KEIYAKUSHO_SEND_DATE,
	KIHON.KEIYAKUSHO_RETURN_DATE,
	KIHON.KEIYAKUSHO_END_DATE,
	KIHON.YUUKOU_BEGIN,
	KIHON.YUUKOU_END,

	KIHON.HAISHUTSU_JIGYOUSHA_CD,
	KIHON.HAISHUTSU_JIGYOUJOU_CD,
	UN.UNPAN_GYOUSHA_CD,
	SHO.SHOBUN_GYOUSHA_CD,
	SHO.SHOBUN_JIGYOUJOU_CD,
	SAI.LAST_SHOBUN_GYOUSHA_CD,
	SAI.LAST_SHOBUN_JIGYOUJOU_CD
   ,KIHON.SYSTEM_ID
FROM
	(
	SELECT
		SYSTEM_ID,
		ITAKU_KEIYAKU_NO,
		HAISHUTSU_JIGYOUSHA_CD,
		HAISHUTSU_JIGYOUJOU_CD,
		CASE WHEN M_ITAKU_KEIYAKU_KIHON.YUUKOU_END IS NOT NULL AND ISNULL(M_ITAKU_KEIYAKU_KIHON.KOUSHIN_SHUBETSU, 0) = 2 AND CONVERT(date, M_ITAKU_KEIYAKU_KIHON.YUUKOU_END) < CONVERT(date, GETDATE())
			THEN '5'
			ELSE
				CASE WHEN M_ITAKU_KEIYAKU_KIHON.KOUSHIN_END_DATE IS NOT NULL AND ISNULL(M_ITAKU_KEIYAKU_KIHON.KOUSHIN_SHUBETSU, 0) = 1 AND CONVERT(date, M_ITAKU_KEIYAKU_KIHON.KOUSHIN_END_DATE) < CONVERT(date, GETDATE())
				THEN '5'
				ELSE 
					CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_END_DATE IS NOT NULL
					THEN '4'
					ELSE
						CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_RETURN_DATE IS NOT NULL
						THEN '3'
						ELSE
							CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_SEND_DATE IS NOT NULL
							THEN '2'
							ELSE 
								CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_CREATE_DATE IS NOT NULL
								THEN '1'
								ELSE ''
								END
							END
						END
					END
				END
		END AS ITAKU_KEIYAKU_STATUS,
		ITAKU_KEIYAKU_SHURUI,
		KOUSHIN_SHUBETSU,
		KEIYAKUSHO_CREATE_DATE,
		KEIYAKUSHO_SEND_DATE,
		KEIYAKUSHO_RETURN_DATE,
		KEIYAKUSHO_END_DATE,
		YUUKOU_BEGIN,
		YUUKOU_END,
		KOUSHIN_END_DATE

    FROM
		M_ITAKU_KEIYAKU_KIHON
	WHERE
		(DELETE_FLG = 0)

		/*IF data.DAY_HANI == 1 */ 
			AND (KEIYAKUSHO_CREATE_DATE BETWEEN /*data.DAY_FROM*/null AND /*data.DAY_TO*/null) /*END*/

		/*IF data.DAY_HANI == 2 */ 
			AND (KEIYAKUSHO_SEND_DATE BETWEEN /*data.DAY_FROM*/null AND /*data.DAY_TO*/null) /*END*/

		/*IF data.DAY_HANI == 3 */ 
			AND (KEIYAKUSHO_RETURN_DATE BETWEEN /*data.DAY_FROM*/null AND /*data.DAY_TO*/null) /*END*/

		/*IF data.DAY_HANI == 4 */ 
			AND (KEIYAKUSHO_END_DATE BETWEEN /*data.DAY_FROM*/null AND /*data.DAY_TO*/null) /*END*/

		/*IF data.DAY_HANI == 5 */ 
			AND (YUUKOU_BEGIN BETWEEN /*data.DAY_FROM*/null AND /*data.DAY_TO*/null) /*END*/

		/*IF data.DAY_HANI == 6 */ 
			AND (YUUKOU_END BETWEEN /*data.DAY_FROM*/null AND /*data.DAY_TO*/null) /*END*/

		/*IF data.DAY_HANI == 7 */ 
			AND (KOUSHIN_END_DATE BETWEEN /*data.DAY_FROM*/null AND /*data.DAY_TO*/null) /*END*/


	) AS KIHON

	LEFT OUTER JOIN
	(
	SELECT
		GYOUSHA_CD,
		GYOUSHA_NAME_RYAKU
	FROM
		M_GYOUSHA
	WHERE
		(HAISHUTSU_NIZUMI_GYOUSHA_KBN = 1)
		AND (DELETE_FLG = 0)
	) AS SHA ON KIHON.HAISHUTSU_JIGYOUSHA_CD = SHA.GYOUSHA_CD
	
	
	LEFT OUTER JOIN
	(
	SELECT
		GENBA_CD,
		GYOUSHA_CD,
		GENBA_NAME_RYAKU
	FROM
		M_GENBA
	WHERE
		(HAISHUTSU_NIZUMI_GENBA_KBN = 1)
		AND (DELETE_FLG = 0)
	)AS GEN ON KIHON.HAISHUTSU_JIGYOUJOU_CD = GEN.GENBA_CD
			AND KIHON.HAISHUTSU_JIGYOUSHA_CD = GEN.GYOUSHA_CD
			
	LEFT OUTER JOIN
	(
	SELECT
		MAIN.SYSTEM_ID,
		MAIN.ITAKU_KEIYAKU_NO,
		MAIN.UNPAN_GYOUSHA_CD,
		MAIN.SEQ
	FROM
		M_ITAKU_KEIYAKU_BETSU2 AS MAIN
	INNER JOIN
		(
		SELECT
			SYSTEM_ID,
			ITAKU_KEIYAKU_NO,
			MAX(SEQ) AS SEQ
		FROM
			M_ITAKU_KEIYAKU_BETSU2
		GROUP BY
			SYSTEM_ID,ITAKU_KEIYAKU_NO
		) AS SUB ON MAIN.SYSTEM_ID=SUB.SYSTEM_ID
				 AND MAIN.ITAKU_KEIYAKU_NO = SUB.ITAKU_KEIYAKU_NO
				 AND MAIN.SEQ = SUB.SEQ
	) AS UN ON KIHON.SYSTEM_ID = UN.SYSTEM_ID
			AND KIHON.ITAKU_KEIYAKU_NO = UN.ITAKU_KEIYAKU_NO

	LEFT OUTER JOIN
	(
	SELECT 
		MAIN.SYSTEM_ID, 
		MAIN.ITAKU_KEIYAKU_NO, 
		MAIN.SHOBUN_GYOUSHA_CD, 
		MAIN.SHOBUN_JIGYOUJOU_CD, 
		MAIN.SEQ
	FROM M_ITAKU_KEIYAKU_BETSU3 AS MAIN
		INNER JOIN (
	   	SELECT 
			SYSTEM_ID, 
			ITAKU_KEIYAKU_NO, 
			MAX(SEQ) AS SEQ
		FROM
			M_ITAKU_KEIYAKU_BETSU3
		GROUP BY
			SYSTEM_ID, 
			ITAKU_KEIYAKU_NO
		) AS SUB
	 	ON MAIN.SYSTEM_ID = SUB.SYSTEM_ID
		AND MAIN.ITAKU_KEIYAKU_NO = SUB.ITAKU_KEIYAKU_NO
		AND MAIN.SEQ = SUB.SEQ
	) AS SHO ON KIHON.SYSTEM_ID = SHO.SYSTEM_ID
			 AND KIHON.ITAKU_KEIYAKU_NO = SHO.ITAKU_KEIYAKU_NO

	LEFT OUTER JOIN
	(
	SELECT 
		MAIN.SYSTEM_ID, 
		MAIN.ITAKU_KEIYAKU_NO, 
		MAIN.LAST_SHOBUN_GYOUSHA_CD, 
		MAIN.LAST_SHOBUN_JIGYOUJOU_CD, 
		MAIN.SEQ
	FROM M_ITAKU_KEIYAKU_BETSU4 AS MAIN
		INNER JOIN (
	   	SELECT 
			SYSTEM_ID, 
			ITAKU_KEIYAKU_NO, 
			MAX(SEQ) AS SEQ
		FROM
			M_ITAKU_KEIYAKU_BETSU4
		GROUP BY
			SYSTEM_ID, 
			ITAKU_KEIYAKU_NO
		) AS SUB
	 	ON MAIN.SYSTEM_ID = SUB.SYSTEM_ID
		AND MAIN.ITAKU_KEIYAKU_NO = SUB.ITAKU_KEIYAKU_NO
		AND MAIN.SEQ = SUB.SEQ
	) AS SAI ON KIHON.SYSTEM_ID = SAI.SYSTEM_ID
			 AND KIHON.ITAKU_KEIYAKU_NO = SAI.ITAKU_KEIYAKU_NO
WHERE
	(KIHON.ITAKU_KEIYAKU_STATUS IS NOT NULL)
	
	/*IF data.ITAKU_STATUS != 6 */ 
		AND (KIHON.ITAKU_KEIYAKU_STATUS = /*data.ITAKU_STATUS*/null)/*END*/

	/*IF data.JIGYOUSHA_CD != '' */ 
		AND KIHON.HAISHUTSU_JIGYOUSHA_CD =  /*data.JIGYOUSHA_CD*/'0' /*END*/

	/*IF data.JIGYOUJOU_CD != '' */ 
		AND KIHON.HAISHUTSU_JIGYOUJOU_CD =  /*data.JIGYOUJOU_CD*/'0' /*END*/

	/*IF data.UNPANSHA_CD != '' */ 
		AND UN.UNPAN_GYOUSHA_CD =  /*data.UNPANSHA_CD*/'0' /*END*/

	/*IF data.SHOBUNSHA_CD != '' */ 
		AND SHO.SHOBUN_GYOUSHA_CD =  /*data.SHOBUNSHA_CD*/'0' /*END*/



