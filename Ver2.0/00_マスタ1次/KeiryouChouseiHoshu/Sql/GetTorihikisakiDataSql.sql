SELECT 
    TORI.TORIHIKISAKI_CD as TORIHIKISAKI_CD
    ,TORI.TORIHIKISAKI_NAME_RYAKU as TORIHIKISAKI_NAME_RYAKU
FROM 
    dbo.M_TORIHIKISAKI TORI 
WHERE TORI.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/''
  AND (
		(TORI.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND TORI.TEKIYOU_END >= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120))
	 OR (TORI.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND TORI.TEKIYOU_END IS NULL)
	 OR (TORI.TEKIYOU_BEGIN IS NULL AND TORI.TEKIYOU_END >= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120))
	 OR (TORI.TEKIYOU_BEGIN IS NULL AND TORI.TEKIYOU_END IS NULL)
	)
  AND TORI.DELETE_FLG = 0