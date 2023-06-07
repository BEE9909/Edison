SELECT
	ISNULL(R13.LAST_SBN_END_DATE, '') AS LAST_SBN_END_DATE
	, ISNULL(R13.LAST_SBN_JOU_NAME, '')
		+ ISNULL(R13.LAST_SBN_JOU_ADDRESS1, '')
		+ ISNULL(R13.LAST_SBN_JOU_ADDRESS2, '')
		+ ISNULL(R13.LAST_SBN_JOU_ADDRESS3, '')
		+ ISNULL(R13.LAST_SBN_JOU_ADDRESS4, '')
	AS LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS
FROM
	DT_MF_TOC AS TOC
	INNER JOIN DT_R18 AS R18
	ON TOC.KANRI_ID = R18.KANRI_ID
	AND TOC.LATEST_SEQ = R18.SEQ
	INNER JOIN DT_R13 AS R13
	ON R18.KANRI_ID = R13.KANRI_ID
	AND R18.SEQ = R13.SEQ
WHERE
	TOC.KANRI_ID =  /*KANRI_ID*/