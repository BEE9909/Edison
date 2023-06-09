SELECT 
    SHO.*
FROM 
    dbo.M_SHOBUN_HOUHOU SHO
/*BEGIN*/WHERE
 /*IF data.SHOBUN_HOUHOU_CD != null*/SHO.SHOBUN_HOUHOU_CD LIKE '%' + /*data.SHOBUN_HOUHOU_CD*/ + '%'/*END*/
 /*IF data.SHOBUN_HOUHOU_NAME != null*/AND SHO.SHOBUN_HOUHOU_NAME LIKE '%' +  /*data.SHOBUN_HOUHOU_NAME*/ + '%'/*END*/
 /*IF data.SHOBUN_HOUHOU_NAME_RYAKU != null*/AND SHO.SHOBUN_HOUHOU_NAME_RYAKU LIKE '%' +  /*data.SHOBUN_HOUHOU_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.SHOBUN_HOUHOU_BIKOU != null*/AND SHO.SHOBUN_HOUHOU_BIKOU LIKE '%' +  /*data.SHOBUN_HOUHOU_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND SHO.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, SHO.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF !data.UPDATE_DATE.IsNull*/AND SHO.UPDATE_DATE LIKE '%' +  /*data.UPDATE_DATE.Value*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, SHO.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY SHO.SHOBUN_HOUHOU_CD
