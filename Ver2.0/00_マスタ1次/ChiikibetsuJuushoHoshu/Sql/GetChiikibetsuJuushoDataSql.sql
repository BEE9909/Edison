SELECT 
    CHJ.*
    ,ISNULL(CHI.CHIIKI_NAME_RYAKU,N'') AS CHIIKI_NAME_RYAKU
FROM 
    dbo.M_CHIIKIBETSU_JUUSHO CHJ
	LEFT JOIN dbo.M_CHIIKI CHI ON CHI.CHIIKI_CD = CHJ.CHANGE_CHIIKI_CD
WHERE CHJ.CHIIKI_CD = /*data.CHIIKI_CD*/'000001'
ORDER BY CHJ.CHIIKI_CD, CHJ.CHANGE_CHIIKI_CD
