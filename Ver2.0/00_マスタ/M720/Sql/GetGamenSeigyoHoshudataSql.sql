SELECT
	M_SMW.*
	,CAST(0 AS bit) AS DELETE_FLG
FROM
	dbo.M_SHAIN_MAX_WINDOW M_SMW
/*BEGIN*/WHERE
 /*IF data.SHAIN_CD != null*/ M_SMW.SHAIN_CD LIKE '%' + /*data.SHAIN_CD*/'01' + '%'/*END*/
 /*END*/
ORDER BY M_SMW.SHAIN_CD
