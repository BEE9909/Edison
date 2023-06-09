SELECT
	DISTINCT SYSTEM_ID,
	ITAKU_KEIYAKU_NO,
	ITAKU_KEIYAKU_SHURUI,
	YUUKOU_BEGIN,
	YUUKOU_END,
	ITAKU_KEIYAKU_STATUS,
	ITAKU_KEIYAKU_TOUROKU_HOUHOU
FROM
	((
		SELECT
			A.SYSTEM_ID,
			A.ITAKU_KEIYAKU_NO,
			A.ITAKU_KEIYAKU_SHURUI,
			A.YUUKOU_BEGIN,
			A.YUUKOU_END,
			CASE WHEN A.YUUKOU_END IS NOT NULL AND ISNULL(A.KOUSHIN_SHUBETSU, 0) = 2 AND CONVERT(date, A.YUUKOU_END) < CONVERT(date, GETDATE())
				THEN '5'
				ELSE
					CASE WHEN A.KOUSHIN_END_DATE IS NOT NULL AND ISNULL(A.KOUSHIN_SHUBETSU, 0) = 1 AND CONVERT(date, A.KOUSHIN_END_DATE) < CONVERT(date, GETDATE())
					THEN '5'
					ELSE
						CASE WHEN A.KEIYAKUSHO_END_DATE IS NOT NULL
						THEN '4'
						ELSE
							CASE WHEN A.KEIYAKUSHO_RETURN_DATE IS NOT NULL
							THEN '3'
							ELSE
								CASE WHEN A.KEIYAKUSHO_SEND_DATE IS NOT NULL
								THEN '2'
								ELSE
									CASE WHEN A.KEIYAKUSHO_CREATE_DATE IS NOT NULL
									THEN '1'
									ELSE ''
								END
							END
						END
					END
				END
			END AS 'ITAKU_KEIYAKU_STATUS',
			A.ITAKU_KEIYAKU_TOUROKU_HOUHOU
		FROM
			M_ITAKU_KEIYAKU_KIHON A
		WHERE
			A.HAISHUTSU_JIGYOUSHA_CD = /*data.HAISHUTSU_JIGYOUSHA_CD*/'000001'
		AND A.HAISHUTSU_JIGYOUJOU_CD = /*data.HAISHUTSU_JIGYOUJOU_CD*/'000001'
        AND A.DELETE_FLG = 0
	)
	UNION ALL
	(
		SELECT
			B.SYSTEM_ID,
			B.ITAKU_KEIYAKU_NO,
			B.ITAKU_KEIYAKU_SHURUI,
			B.YUUKOU_BEGIN,
			B.YUUKOU_END,
			CASE WHEN B.YUUKOU_END IS NOT NULL AND ISNULL(B.KOUSHIN_SHUBETSU, 0) = 2 AND CONVERT(date, B.YUUKOU_END) < CONVERT(date, GETDATE())
				THEN '5'
				ELSE
					CASE WHEN B.KOUSHIN_END_DATE IS NOT NULL AND ISNULL(B.KOUSHIN_SHUBETSU, 0) = 1 AND CONVERT(date, B.KOUSHIN_END_DATE) < CONVERT(date, GETDATE())
					THEN '5'
					ELSE
						CASE WHEN B.KEIYAKUSHO_END_DATE IS NOT NULL
						THEN '4'
						ELSE
							CASE WHEN B.KEIYAKUSHO_RETURN_DATE IS NOT NULL
							THEN '3'
							ELSE
								CASE WHEN B.KEIYAKUSHO_SEND_DATE IS NOT NULL
								THEN '2'
								ELSE
									CASE WHEN B.KEIYAKUSHO_CREATE_DATE IS NOT NULL
									THEN '1'
									ELSE ''
								END
							END
						END
					END
				END
			END AS 'ITAKU_KEIYAKU_STATUS',
			B.ITAKU_KEIYAKU_TOUROKU_HOUHOU
		FROM
			M_ITAKU_KEIYAKU_KIHON_HST_GENBA A
				LEFT JOIN M_ITAKU_KEIYAKU_KIHON B ON B.SYSTEM_ID = A.SYSTEM_ID
		WHERE
			A.HAISHUTSU_JIGYOUSHA_CD = /*data.HAISHUTSU_JIGYOUSHA_CD*/'000001'
		AND A.HAISHUTSU_JIGYOUJOU_CD = /*data.HAISHUTSU_JIGYOUJOU_CD*/'000001'
        AND B.DELETE_FLG = 0
	)) TBL
ORDER BY ITAKU_KEIYAKU_NO