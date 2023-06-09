SELECT
    KYO.*
FROM
    dbo.M_KYOTEN KYO
/*BEGIN*/WHERE
 /*IF !data.KYOTEN_CD.IsNull */CAST(KYO.KYOTEN_CD AS VARCHAR(5)) LIKE '%' + CAST(/*data.KYOTEN_CD*/0 AS VARCHAR(5)) + '%'/*END*/
 /*IF data.KYOTEN_NAME != null*/AND KYO.KYOTEN_NAME LIKE '%' +  /*data.KYOTEN_NAME*/ + '%'/*END*/
 /*IF data.KYOTEN_NAME_RYAKU != null*/AND KYO.KYOTEN_NAME_RYAKU LIKE '%' +  /*data.KYOTEN_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.KYOTEN_FURIGANA != null*/AND KYO.KYOTEN_FURIGANA LIKE '%' +  /*data.KYOTEN_FURIGANA*/ + '%'/*END*/
 /*IF data.KYOTEN_POST != null*/AND KYO.KYOTEN_POST LIKE '%' +  /*data.KYOTEN_POST*/ + '%'/*END*/
 /*IF !data.KYOTEN_TODOUFUKEN_CD.IsNull */AND CAST(KYO.KYOTEN_TODOUFUKEN_CD AS VARCHAR(5)) LIKE '%' + CAST(/*data.KYOTEN_TODOUFUKEN_CD*/0 AS VARCHAR(5)) + '%'/*END*/
 /*IF data.KYOTEN_ADDRESS1 != null*/AND KYO.KYOTEN_ADDRESS1 LIKE '%' +  /*data.KYOTEN_ADDRESS1*/ + '%'/*END*/
 /*IF data.KYOTEN_ADDRESS2 != null*/AND KYO.KYOTEN_ADDRESS2 LIKE '%' +  /*data.KYOTEN_ADDRESS2*/ + '%'/*END*/
 /*IF data.KYOTEN_TEL != null*/AND KYO.KYOTEN_TEL LIKE '%' +  /*data.KYOTEN_TEL*/ + '%'/*END*/
 /*IF data.KYOTEN_FAX != null*/AND KYO.KYOTEN_FAX LIKE '%' +  /*data.KYOTEN_FAX*/ + '%'/*END*/
 /*IF data.KYOTEN_DAIHYOU != null*/AND KYO.KYOTEN_DAIHYOU LIKE '%' +  /*data.KYOTEN_DAIHYOU*/ + '%'/*END*/
 /*IF data.KYOTEN_BIKOU != null*/AND KYO.KYOTEN_BIKOU LIKE '%' +  /*data.KYOTEN_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND KYO.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, KYO.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND KYO.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, KYO.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF data.KYOTEN_BIKOU != null*/AND KYO.KEIRYOU_SHOUMEI_1 LIKE '%' +  /*data.KEIRYOU_SHOUMEI_1*/ + '%'/*END*/
 /*IF data.KYOTEN_BIKOU != null*/AND KYO.KEIRYOU_SHOUMEI_2 LIKE '%' +  /*data.KEIRYOU_SHOUMEI_2*/ + '%'/*END*/
 /*IF data.KYOTEN_BIKOU != null*/AND KYO.KEIRYOU_SHOUMEI_3 LIKE '%' +  /*data.KEIRYOU_SHOUMEI_3*/ + '%'/*END*/
/*END*/
ORDER BY KYO.KYOTEN_CD
