SELECT
    UNP.*
FROM
    dbo.M_UNPAN_HOUHOU UNP
/*BEGIN*/WHERE
 /*IF !deletechuFlg*/ UNP.DELETE_FLG = 0/*END*/
 /*IF data.UNPAN_HOUHOU_CD != null*/ AND UNP.UNPAN_HOUHOU_CD LIKE '%' +  /*data.UNPAN_HOUHOU_CD*/ + '%'/*END*/
 /*IF data.UNPAN_HOUHOU_NAME != null*/ AND UNP.UNPAN_HOUHOU_NAME LIKE '%' +  /*data.UNPAN_HOUHOU_NAME*/ + '%'/*END*/
 /*IF data.UNPAN_HOUHOU_NAME_RYAKU != null*/AND UNP.UNPAN_HOUHOU_NAME_RYAKU LIKE '%' +  /*data.UNPAN_HOUHOU_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND UNP.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, UNP.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND UNP.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, UNP.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND UNP.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
order by UNPAN_HOUHOU_CD