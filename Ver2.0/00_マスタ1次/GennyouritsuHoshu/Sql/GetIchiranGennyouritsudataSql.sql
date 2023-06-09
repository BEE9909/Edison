SELECT 
    GENNYOURITSU.*
    ,ISNULL(HOUKOKUSHO.HOUKOKUSHO_BUNRUI_NAME_RYAKU,N'') AS HOUKOKUSHO_BUNRUI_NAME_RYAKU
    ,ISNULL(HAIKI.HAIKI_NAME_RYAKU,N'') AS HAIKI_NAME_RYAKU
    ,ISNULL(SHOBUN.SHOBUN_HOUHOU_NAME_RYAKU,N'') AS SHOBUN_HOUHOU_NAME_RYAKU
    ,GENNYOURITSU.HOUKOKUSHO_BUNRUI_CD AS UK_HOUKOKUSHO_BUNRUI_CD
    ,GENNYOURITSU.HAIKI_NAME_CD AS UK_HAIKI_NAME_CD
    ,GENNYOURITSU.SHOBUN_HOUHOU_CD AS UK_SHOBUN_HOUHOU_CD
FROM 
    dbo.M_GENNYOURITSU GENNYOURITSU
	LEFT JOIN dbo.M_HOUKOKUSHO_BUNRUI HOUKOKUSHO ON HOUKOKUSHO.HOUKOKUSHO_BUNRUI_CD = GENNYOURITSU.HOUKOKUSHO_BUNRUI_CD
	LEFT JOIN dbo.M_HAIKI_NAME HAIKI ON HAIKI.HAIKI_NAME_CD = GENNYOURITSU.HAIKI_NAME_CD
	LEFT JOIN dbo.M_SHOBUN_HOUHOU SHOBUN ON SHOBUN.SHOBUN_HOUHOU_CD = GENNYOURITSU.SHOBUN_HOUHOU_CD
/*BEGIN*/WHERE
 /*IF data.HOUKOKUSHO_BUNRUI_CD != null*/
 GENNYOURITSU.HOUKOKUSHO_BUNRUI_CD LIKE '%' + /*data.HOUKOKUSHO_BUNRUI_CD*/'000001' + '%'
 /*END*/
 /*IF data.HAIKI_NAME_CD != null*/AND GENNYOURITSU.HAIKI_NAME_CD LIKE '%' +  /*data.HAIKI_NAME_CD*/ + '%'/*END*/
 /*IF data.SHOBUN_HOUHOU_CD != null*/AND GENNYOURITSU.SHOBUN_HOUHOU_CD LIKE '%' +  /*data.SHOBUN_HOUHOU_CD*/ + '%'/*END*/
 /*IF !data.GENNYOURITSU.IsNull*/AND GENNYOURITSU.GENNYOURITSU = /*data.GENNYOURITSU.Value*/0/*END*/
 /*IF data.GENNYOURITSU_BIKOU != null*/AND GENNYOURITSU.GENNYOURITSU_BIKOU LIKE '%' +  /*data.GENNYOURITSU_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND GENNYOURITSU.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, GENNYOURITSU.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND GENNYOURITSU.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, GENNYOURITSU.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND GENNYOURITSU.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY GENNYOURITSU.HOUKOKUSHO_BUNRUI_CD, GENNYOURITSU.HAIKI_NAME_CD, GENNYOURITSU.SHOBUN_HOUHOU_CD
