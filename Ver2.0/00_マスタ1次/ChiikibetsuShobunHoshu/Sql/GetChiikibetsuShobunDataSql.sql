SELECT 
    CHI.*
    ,ISNULL(SHO.SHOBUN_HOUHOU_NAME_RYAKU,N'') AS SHOBUN_HOUHOU_NAME
FROM 
    dbo.M_CHIIKIBETSU_SHOBUN CHI
	LEFT JOIN dbo.M_SHOBUN_HOUHOU SHO ON SHO.SHOBUN_HOUHOU_CD = CHI.SHOBUN_HOUHOU_CD
WHERE CHI.CHIIKI_CD = /*data.CHIIKI_CD*/'000001'
ORDER BY CHI.CHIIKI_CD, CHI.SHOBUN_HOUHOU_CD
