SELECT
    NSG.*
FROM
    dbo.M_NISUGATA NSG
/*BEGIN*/WHERE
 /*IF data.NISUGATA_CD != null*/NSG.NISUGATA_CD LIKE '%' + /*data.NISUGATA_CD*/'000001' + '%'/*END*/
 /*IF data.NISUGATA_NAME != null*/AND NSG.NISUGATA_NAME LIKE '%' +  /*data.NISUGATA_NAME*/ + '%'/*END*/
 /*IF data.NISUGATA_BIKOU != null*/AND NSG.NISUGATA_BIKOU LIKE '%' +  /*data.NISUGATA_BIKOU*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND NSG.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, NSG.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND NSG.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, NSG.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND NSG.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY NSG.NISUGATA_CD
