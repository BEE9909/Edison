SELECT 
    ISNULL(TEKIYOU_END,'9999/12/31') AS TEKIYOU_END
FROM 
    dbo.M_HIKIAI_GENBA
WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'
  AND DELETE_FLG = 0
  AND HIKIAI_GYOUSHA_USE_FLG = 1
ORDER BY TEKIYOU_END DESC
