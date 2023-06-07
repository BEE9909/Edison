SELECT 
    CHI.*
    ,ISNULL(GYO.GYOUSHU_NAME_RYAKU,N'') AS GYOUSHU_NAME_RYAKU
FROM 
    dbo.M_CHIIKIBETSU_GYOUSHU CHI
	LEFT JOIN dbo.M_GYOUSHU GYO ON GYO.GYOUSHU_CD = CHI.GYOUSHU_CD
WHERE CHI.CHIIKI_CD = /*data.CHIIKI_CD*/'000001'
ORDER BY CHI.CHIIKI_CD, CHI.GYOUSHU_CD
