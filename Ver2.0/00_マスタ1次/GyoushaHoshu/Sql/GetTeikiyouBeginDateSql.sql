SELECT 
    ISNULL(GEN.TEKIYOU_BEGIN,'1753/01/01') AS TEKIYOU_BEGIN
FROM 
    dbo.M_GENBA GEN
WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'
  AND DELETE_FLG = 0
ORDER BY TEKIYOU_BEGIN
