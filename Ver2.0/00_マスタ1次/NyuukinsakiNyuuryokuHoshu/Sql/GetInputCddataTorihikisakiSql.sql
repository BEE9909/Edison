SELECT 
    TORI.*
FROM 
    dbo.M_TORIHIKISAKI TORI
WHERE
	TORI.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001'
ORDER BY TORI.TORIHIKISAKI_CD
