SELECT 
    GENBA.GYOUSHA_CD as GYOUSHA_CD
    ,GENBA.GENBA_CD as GENBA_CD
    ,GENBA.GENBA_NAME_RYAKU as GENBA_NAME_RYAKU
	,TORIHIKISAKI_CD
FROM 
    dbo.M_GENBA GENBA
WHERE GENBA.GENBA_CD = /*data.GENBA_CD*/''
  AND GENBA.GYOUSHA_CD = /*data.GYOUSHA_CD*/'='
  AND (
		(GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND GENBA.TEKIYOU_END >= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120))
	 OR (GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND GENBA.TEKIYOU_END IS NULL)
	 OR (GENBA.TEKIYOU_BEGIN IS NULL AND GENBA.TEKIYOU_END >= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120))
	 OR (GENBA.TEKIYOU_BEGIN IS NULL AND GENBA.TEKIYOU_END IS NULL)
	)
  AND GENBA.DELETE_FLG = 0