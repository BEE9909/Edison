SELECT 
    SHO.SHAIN_CD,
    SHO.SHOBUN_TANTOUSHA_BIKOU,
    SHO.CREATE_DATE,
    SHO.CREATE_USER,
    SHO.CREATE_PC,
    SHO.UPDATE_DATE,
    SHO.UPDATE_USER,
    SHO.UPDATE_PC,
    SHO.DELETE_FLG,
    SHO.TIME_STAMP,
	ISNULL(SHA.SHAIN_NAME, N'') AS SHAIN_NAME,
	ISNULL(SHA.SHAIN_FURIGANA, N'') AS SHAIN_FURIGANA,
	SHA.TEKIYOU_BEGIN,
	SHA.TEKIYOU_END
FROM 
    dbo.M_SHOBUN_TANTOUSHA SHO
    INNER JOIN dbo.M_SHAIN SHA ON SHA.SHAIN_CD = SHO.SHAIN_CD
/*BEGIN*/WHERE
 /*IF data.SHAIN_CD != null*/
 SHO.SHAIN_CD LIKE '%' + /*data.SHAIN_CD*/'000001' + '%'
 /*END*/
 /*IF data.SHOBUN_TANTOUSHA_BIKOU != null*/AND SHO.SHOBUN_TANTOUSHA_BIKOU LIKE '%' +  /*data.SHOBUN_TANTOUSHA_BIKOU*/ + '%'/*END*/
 /*IF data.SEARCH_TEKIYOU_BEGIN != null*/AND CONVERT(nvarchar, SHO.TEKIYOU_BEGIN, 120) LIKE '%' +  /*data.SEARCH_TEKIYOU_BEGIN*/ + '%'/*END*/
 /*IF data.SEARCH_TEKIYOU_END != null*/AND CONVERT(nvarchar, SHO.TEKIYOU_END, 120) LIKE '%' +  /*data.SEARCH_TEKIYOU_END*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND SHA.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, SHO.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND SHA.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, SHO.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND SHO.DELETE_FLG = 0/*END*/
 /*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/AND (1 = 0/*END*/
 /*IF tekiyounaiFlg*/OR (((SHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= SHA.TEKIYOU_END) or (SHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and SHA.TEKIYOU_END IS NULL) or (SHA.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= SHA.TEKIYOU_END) or (SHA.TEKIYOU_BEGIN IS NULL and SHA.TEKIYOU_END IS NULL)) and SHO.DELETE_FLG = 0)/*END*/
 /*IF deletechuFlg*/OR SHO.DELETE_FLG = /*deletechuFlg*/1/*END*/
 /*IF tekiyougaiFlg*/OR ((SHA.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > SHA.TEKIYOU_END) and SHO.DELETE_FLG = 0)/*END*/
 /*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/ )/*END*/
/*END*/
ORDER BY SHO.SHAIN_CD
