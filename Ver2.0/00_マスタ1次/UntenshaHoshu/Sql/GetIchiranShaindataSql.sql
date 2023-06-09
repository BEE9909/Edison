SELECT 
    UNT.SHAIN_CD,
    UNT.UNTENSHA_BIKOU,
    UNT.CREATE_DATE,
    UNT.CREATE_USER,
    UNT.CREATE_PC,
    UNT.UPDATE_DATE,
    UNT.UPDATE_USER,
    UNT.UPDATE_PC,
    UNT.DELETE_FLG,
    UNT.TIME_STAMP,
	ISNULL(SHA.SHAIN_NAME, N'') AS SHAIN_NAME,
	ISNULL(SHA.SHAIN_FURIGANA, N'') AS SHAIN_FURIGANA,
	SHA.TEKIYOU_BEGIN,
	SHA.TEKIYOU_END
FROM 
	dbo.M_UNTENSHA UNT
		LEFT JOIN dbo.M_SHAIN SHA ON UNT.SHAIN_CD = SHA.SHAIN_CD
/*BEGIN*/WHERE
	/*IF !deletechuFlg*/ UNT.DELETE_FLG = 0/*END*/
	/*IF data.SHAIN_CD != null*/ AND UNT.SHAIN_CD LIKE '%' +  /*data.SHAIN_CD*/ + '%'/*END*/
	/*IF data.SHAIN_NAME != null*/ AND SHA.SHAIN_NAME LIKE '%' +  /*data.SHAIN_NAME*/ + '%'/*END*/
	/*IF data.SHAIN_FURIGANA != null*/AND SHA.SHAIN_FURIGANA LIKE '%' +  /*data.SHAIN_FURIGANA*/ + '%'/*END*/
	/*IF data.UNTENSHA_BIKOU != null*/AND UNT.UNTENSHA_BIKOU LIKE '%' +  /*data.UNTENSHA_BIKOU*/ + '%'/*END*/
	/*IF data.SEARCH_TEKIYOU_BEGIN != null*/AND CONVERT(nvarchar, SHA.TEKIYOU_BEGIN, 120) LIKE '%' +  /*data.SEARCH_TEKIYOU_BEGIN*/ + '%'/*END*/
	/*IF data.SEARCH_TEKIYOU_END != null*/AND CONVERT(nvarchar, SHA.TEKIYOU_END, 120) LIKE '%' +  /*data.SEARCH_TEKIYOU_END*/ + '%'/*END*/
	/*IF data.UPDATE_USER != null*/AND UNT.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, UNT.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
	/*IF data.CREATE_USER != null*/AND UNT.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, UNT.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
	/*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/AND (1 = 0/*END*/
	/*IF tekiyounaiFlg*/OR (((SHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= SHA.TEKIYOU_END) or (SHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and SHA.TEKIYOU_END IS NULL) or (SHA.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= SHA.TEKIYOU_END) or (SHA.TEKIYOU_BEGIN IS NULL and SHA.TEKIYOU_END IS NULL)) and UNT.DELETE_FLG = 0)/*END*/
	/*IF deletechuFlg*/OR UNT.DELETE_FLG = /*deletechuFlg*/0/*END*/
	/*IF tekiyougaiFlg*/OR ((SHA.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > SHA.TEKIYOU_END) and UNT.DELETE_FLG = 0)/*END*/
	/*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/ )/*END*/
/*END*/
ORDER BY SHA.SHAIN_CD
