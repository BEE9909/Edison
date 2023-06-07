SELECT ISNULL(MIN(CONVERT(decimal(38, 0), T.RAKURAKU_CUSTOMER_CD)), 1) RAKURAKU_CUSTOMER_CD
FROM (
	SELECT (CONVERT(decimal(38, 0), M.RAKURAKU_CUSTOMER_CD) + 1) RAKURAKU_CUSTOMER_CD 
	FROM 
	(SELECT RAKURAKU_CUSTOMER_CD FROM M_TORIHIKISAKI_SEIKYUU
	 UNION
	 SELECT RAKURAKU_CUSTOMER_CD FROM M_GYOUSHA
	 UNION
	 SELECT RAKURAKU_CUSTOMER_CD FROM M_GENBA) AS M
	WHERE ISNUMERIC(M.RAKURAKU_CUSTOMER_CD) = 1) T
WHERE 
T.RAKURAKU_CUSTOMER_CD NOT IN (
	SELECT (CONVERT(decimal(38, 0), N.RAKURAKU_CUSTOMER_CD) + 0) RAKURAKU_CUSTOMER_CD 
	FROM
	(SELECT RAKURAKU_CUSTOMER_CD FROM M_TORIHIKISAKI_SEIKYUU
	 UNION
	 SELECT RAKURAKU_CUSTOMER_CD FROM M_GYOUSHA
	 UNION
	 SELECT RAKURAKU_CUSTOMER_CD FROM M_GENBA) AS N
	WHERE ISNUMERIC(N.RAKURAKU_CUSTOMER_CD) = 1)