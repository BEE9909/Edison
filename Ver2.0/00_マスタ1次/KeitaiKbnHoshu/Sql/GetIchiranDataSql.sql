SELECT 
    KEI.*
	,BU.DENSHU_KBN_NAME_RYAKU
    ,ISNULL(BU.DENSHU_KBN_NAME_RYAKU,N'') AS DENSHU_KBN_NAME_RYAKU
FROM 
    dbo.M_KEITAI_KBN KEI
	LEFT JOIN dbo.M_DENSHU_KBN BU ON BU.DENSHU_KBN_CD = KEI.DENSHU_KBN_CD
/*BEGIN*/WHERE
/*IF !data.KEITAI_KBN_CD.IsNull */CAST(KEI.KEITAI_KBN_CD AS VARCHAR(2)) LIKE '%' + CAST(/*data.KEITAI_KBN_CD*/0 AS VARCHAR(2)) + '%'/*END*/
 /*IF data.KEITAI_KBN_NAME != null*/AND KEI.KEITAI_KBN_NAME LIKE '%' +  /*data.KEITAI_KBN_NAME*/ + '%'/*END*/
 /*IF !data.DENSHU_KBN_CD.IsNull */AND CAST(KEI.DENSHU_KBN_CD AS VARCHAR(2)) LIKE '%' + CAST(/*data.DENSHU_KBN_CD*/0 AS VARCHAR(2)) + '%'/*END*/
 /*IF data.DENSHU_KBN_NAME_RYAKU != null*/AND BU.DENSHU_KBN_NAME_RYAKU LIKE '%' +  /*data.DENSHU_KBN_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.KEITAI_KBN_BIKOU != null*/AND KEI.KEITAI_KBN_BIKOU LIKE '%' +  /*data.KEITAI_KBN_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND KEI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, KEI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND KEI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, KEI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND KEI.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY KEI.KEITAI_KBN_CD
