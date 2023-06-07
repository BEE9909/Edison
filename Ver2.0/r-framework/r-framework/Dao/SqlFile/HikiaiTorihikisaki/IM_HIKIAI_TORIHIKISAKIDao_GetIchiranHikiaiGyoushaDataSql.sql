SELECT 
    GYO.GYOUSHA_CD AS GYOUSHA_CD
    ,ISNULL(GYO.GYOUSHA_NAME1,N'') AS GYOUSHA_NAME1
    ,ISNULL(GYO.GYOUSHA_ADDRESS1,N'') AS GYOUSHA_ADDRESS1
    ,ISNULL(GYO.GYOUSHA_NAME2,N'') AS GYOUSHA_NAME2
    ,ISNULL(GYO.GYOUSHA_ADDRESS2,N'') AS GYOUSHA_ADDRESS2
FROM 
    dbo.M_HIKIAI_GYOUSHA GYO
WHERE GYO.TORIHIKISAKI_CD LIKE /*data.TORIHIKISAKI_CD*/'000001'
  /*IF !data.TORIHIKI_JOUKYOU.IsNull && data.TORIHIKI_JOUKYOU.Value == 2*/AND GYO.TORIHIKI_JOUKYOU = 2/*END*/
  AND GYO.DELETE_FLG = 0
ORDER BY GYO.GYOUSHA_CD