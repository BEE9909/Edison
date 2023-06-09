SELECT 
    CHI.*
    ,ISNULL(GYO.GYOUSHU_NAME_RYAKU,N'') AS GYOUSHU_NAME_RYAKU
FROM 
    dbo.M_CHIIKIBETSU_GYOUSHU CHI
	LEFT JOIN dbo.M_GYOUSHU GYO ON GYO.GYOUSHU_CD = CHI.GYOUSHU_CD
WHERE CHI.CHIIKI_CD = /*data.CHIIKI_CD*/'000001'
  /*IF data.GYOUSHU_CD != null*/AND CHI.GYOUSHU_CD LIKE '%' +  /*data.GYOUSHU_CD*/ + '%'/*END*/
  /*IF data.HOUKOKU_GYOUSHU_CD != null*/AND CHI.HOUKOKU_GYOUSHU_CD LIKE '%' +  /*data.HOUKOKU_GYOUSHU_CD*/ + '%'/*END*/
  /*IF data.HOUKOKU_GYOUSHU_NAME != null*/AND CHI.HOUKOKU_GYOUSHU_NAME LIKE '%' +  /*data.HOUKOKU_GYOUSHU_NAME*/ + '%'/*END*/
  /*IF data.CHIIKIBETSU_GYOUSHU_BIKOU != null*/AND CHI.CHIIKIBETSU_GYOUSHU_BIKOU LIKE '%' +  /*data.CHIIKIBETSU_GYOUSHU_BIKOU*/ + '%'/*END*/
  /*IF data.UPDATE_USER != null*/AND CHI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
  /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, CHI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
  /*IF data.CREATE_USER != null*/AND CHI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
  /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, CHI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
  /*IF !deletechuFlg*/AND CHI.DELETE_FLG = /*deletechuFlg*/0/*END*/
ORDER BY CHI.CHIIKI_CD, CHI.GYOUSHU_CD
