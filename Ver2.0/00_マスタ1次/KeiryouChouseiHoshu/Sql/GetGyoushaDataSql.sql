SELECT 
    GYOUSHA.GYOUSHA_CD as GYOUSHA_CD
    ,GYOUSHA.GYOUSHA_NAME_RYAKU as GYOUSHA_NAME_RYAKU
	,TORIHIKISAKI_CD
FROM 
    dbo.M_GYOUSHA  GYOUSHA
WHERE GYOUSHA.GYOUSHA_CD = /*data.GYOUSHA_CD*/''
  AND (
		(GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND GYOUSHA.TEKIYOU_END >= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120))
	 OR (GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND GYOUSHA.TEKIYOU_END IS NULL)
	 OR (GYOUSHA.TEKIYOU_BEGIN IS NULL AND GYOUSHA.TEKIYOU_END >= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120))
	 OR (GYOUSHA.TEKIYOU_BEGIN IS NULL AND GYOUSHA.TEKIYOU_END IS NULL)
	)
  AND GYOUSHA.DELETE_FLG = 0