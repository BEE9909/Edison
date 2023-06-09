SELECT 
    HAI.*
    ,ISNULL(HO.HOUKOKUSHO_BUNRUI_NAME_RYAKU,N'') AS HOUKOKUSHO_BUNRUI_NAME_RYAKU
FROM 
    dbo.M_DENSHI_HAIKI_SHURUI HAI
	LEFT JOIN dbo.M_HOUKOKUSHO_BUNRUI HO ON HO.HOUKOKUSHO_BUNRUI_CD = HAI.HOUKOKUSHO_BUNRUI_CD
/*BEGIN*/WHERE
 /*IF data.HAIKI_SHURUI_CD != null*/
 HAI.HAIKI_SHURUI_CD LIKE '%' + /*data.HAIKI_SHURUI_CD*/'0001' + '%'
 /*END*/
 /*IF data.HAIKI_SHURUI_NAME != null*/AND HAI.HAIKI_SHURUI_NAME LIKE '%' +  /*data.HAIKI_SHURUI_NAME*/ + '%'/*END*/
 /*IF data.HOUKOKUSHO_BUNRUI_CD != null*/AND HAI.HOUKOKUSHO_BUNRUI_CD LIKE '%' +  /*data.HOUKOKUSHO_BUNRUI_CD*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND HAI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND HAI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, HAI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, HAI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY HAI.HAIKI_SHURUI_CD
