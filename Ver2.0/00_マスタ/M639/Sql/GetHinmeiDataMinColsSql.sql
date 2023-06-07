SELECT 
 HINMEI.UNCHIN_HINMEI_CD,
 HINMEI.UNCHIN_HINMEI_NAME,
 HINMEI.UNCHIN_HINMEI_FURIGANA,
 HINMEI.UNIT_CD,
 HINMEI.UNIT_NAME,
 HINMEI.BIKOU,
 HINMEI.DELETE_FLG 
FROM 
 dbo.M_UNCHIN_HINMEI HINMEI
/*BEGIN*/WHERE
 /*IF data.UNCHIN_HINMEI_CD != null*/HINMEI.UNCHIN_HINMEI_CD LIKE '%' + /*data.UNCHIN_HINMEI_CD*/'000001' + '%'/*END*/
 /*IF data.UNCHIN_HINMEI_NAME != null*/AND HINMEI.UNCHIN_HINMEI_NAME LIKE '%' +  /*data.UNCHIN_HINMEI_NAME*/ + '%'/*END*/
 /*IF data.UNCHIN_HINMEI_FURIGANA != null*/AND HINMEI.UNCHIN_HINMEI_FURIGANA LIKE '%' +  /*data.UNCHIN_HINMEI_FURIGANA*/ + '%'/*END*/
 /*IF !data.UNIT_CD.IsNull*/AND HINMEI.UNIT_CD = /*data.UNIT_CD*//*END*/
 /*IF data.UNIT_NAME != null*/AND HINMEI.UNIT_NAME LIKE '%' +  /*data.UNIT_NAME*/ + '%'/*END*/
 /*IF data.BIKOU != null*/AND HINMEI.BIKOU LIKE '%' +  /*data.BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND HINMEI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, HINMEI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND HINMEI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, HINMEI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY HINMEI.UNCHIN_HINMEI_CD