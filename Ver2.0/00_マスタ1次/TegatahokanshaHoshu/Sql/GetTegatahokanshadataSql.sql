SELECT
    TE.SHAIN_CD,
    ISNULL(SHA.SHAIN_NAME,N''),
    ISNULL(SHA.SHAIN_FURIGANA,N''),
    SHA.TEKIYOU_BEGIN,
    SHA.TEKIYOU_END,
    TE.CREATE_USER,
    TE.CREATE_DATE,
    TE.UPDATE_USER,
    TE.UPDATE_DATE,
    CAST(0 AS bit) AS DELETE_FLG
FROM
    dbo.M_TEGATA_HOKANSHA TE
    LEFT JOIN dbo.M_SHAIN SHA ON TE.SHAIN_CD = SHA.SHAIN_CD
/*BEGIN*/WHERE
 /*IF data.SHAIN_CD != null*/ TE.SHAIN_CD LIKE '%' + /*data.SHAIN_CD*/'01' + '%'/*END*/
 /*IF data.TEGATA_HOKANSHA_BIKOU != null*/AND TE.TEGATA_HOKANSHA_BIKOU LIKE '%' +  /*data.TEGATA_HOKANSHA_BIKOU*/ + '%'/*END*/
 /*END*/
ORDER BY TE.SHAIN_CD
