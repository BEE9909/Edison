SELECT
	BUSHO.*
	,CAST(0 AS bit) AS DELETE_FLG
FROM
	dbo.M_BUSHO BUSHO
/*BEGIN*/WHERE
 /*IF data.BUSHO_CD != null*/ BUSHO.BUSHO_CD LIKE '%' + /*data.BUSHO_CD*/'01' + '%'/*END*/
 /*IF data.BUSNO_NAME != null*/AND BUSHO.BUSHO_NAME LIKE '%' +  /*data.BUSHO_NAME*/ + '%'/*END*/
 /*END*/
ORDER BY BUSHO.BUSHO_CD
