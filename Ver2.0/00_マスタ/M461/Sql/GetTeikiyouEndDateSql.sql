SELECT 
    ISNULL(GYO.TEKIYOU_END,'9999/12/31') AS TEKIYOU_END
FROM 
    dbo.M_HIKIAI_GYOUSHA GYO
WHERE GYO.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001'
  AND GYO.DELETE_FLG = 0
UNION ALL
SELECT 
    ISNULL(GEN.TEKIYOU_END,'9999/12/31') AS TEKIYOU_END
FROM 
    dbo.M_HIKIAI_GENBA GEN
WHERE GEN.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001'
  AND GEN.DELETE_FLG = 0
ORDER BY TEKIYOU_END DESC
