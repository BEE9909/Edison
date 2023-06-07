SELECT
    S_HAIKI.*
    ,ISNULL(K_HAIKI.HAIKI_KBN_NAME,N'') AS HAIKI_KBN_NAME
    ,ISNULL(HOUKOKU.HOUKOKUSHO_BUNRUI_NAME_RYAKU,N'') AS HOUKOKUSHO_BUNRUI_NAME_RYAKU
FROM
    dbo.M_HAIKI_SHURUI S_HAIKI
    LEFT JOIN dbo.M_HAIKI_KBN K_HAIKI ON S_HAIKI.HAIKI_KBN_CD = K_HAIKI.HAIKI_KBN_CD
    LEFT JOIN dbo.M_HOUKOKUSHO_BUNRUI HOUKOKU ON S_HAIKI.HOUKOKUSHO_BUNRUI_CD = HOUKOKU.HOUKOKUSHO_BUNRUI_CD
/*BEGIN*/WHERE
 /*IF !data.HAIKI_KBN_CD.IsNull*/ S_HAIKI.HAIKI_KBN_CD = /*data.HAIKI_KBN_CD.Value*/1/*END*/
 /*IF data.HAIKI_SHURUI_BIKOU != null*/AND S_HAIKI.HAIKI_SHURUI_BIKOU LIKE '%' +  /*data.HAIKI_SHURUI_BIKOU*/ + '%'/*END*/
 /*IF data.HAIKI_SHURUI_CD != null*/AND S_HAIKI.HAIKI_SHURUI_CD LIKE '%' +  /*data.HAIKI_SHURUI_CD*/ + '%'/*END*/
 /*IF data.HAIKI_SHURUI_FURIGANA != null*/AND S_HAIKI.HAIKI_SHURUI_FURIGANA LIKE '%' +  /*data.HAIKI_SHURUI_FURIGANA*/ + '%'/*END*/
 /*IF data.HAIKI_SHURUI_NAME != null*/AND S_HAIKI.HAIKI_SHURUI_NAME LIKE '%' +  /*data.HAIKI_SHURUI_NAME*/ + '%'/*END*/
 /*IF data.HAIKI_SHURUI_NAME_RYAKU != null*/AND S_HAIKI.HAIKI_SHURUI_NAME_RYAKU LIKE '%' +  /*data.HAIKI_SHURUI_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.HOUKOKUSHO_BUNRUI_CD != null*/AND S_HAIKI.HOUKOKUSHO_BUNRUI_CD LIKE '%' +  /*data.HOUKOKUSHO_BUNRUI_CD*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND S_HAIKI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar,S_HAIKI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND S_HAIKI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar,S_HAIKI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND S_HAIKI.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
 ORDER BY S_HAIKI.HAIKI_KBN_CD, S_HAIKI.HAIKI_SHURUI_CD
