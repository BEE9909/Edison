SELECT 
    CHI.*
FROM 
    dbo.M_CHIIKIBETSU_JUUSHO CHI
WHERE
     CHI.CHIIKI_CD = /*data.CHIIKI_CD*/'000001'
 AND CHI.CHANGE_CHIIKI_CD = /*data.CHANGE_CHIIKI_CD*/'000001'
