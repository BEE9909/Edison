SELECT
MCS.DELETE_FLG,
MCS.CONTENA_SHURUI_CD,
MCS.CONTENA_SHURUI_NAME,
MCS.CONTENA_SHURUI_NAME_RYAKU,
MCS.CONTENA_SHURUI_FURIGANA,
MCS.CONTENA_SHURUI_BIKOU,
MCS.TEKIYOU_BEGIN,
MCS.TEKIYOU_END,
MCS.CREATE_USER,
MCS.CREATE_DATE,
MCS.CREATE_PC,
MCS.UPDATE_USER,
MCS.UPDATE_DATE,
MCS.UPDATE_PC
FROM 
dbo.M_CONTENA_SHURUI MCS
/*BEGIN*/WHERE
/*IF data.CONTENA_SHURUI_CD != null*/CAST(MCS.CONTENA_SHURUI_CD AS varchar(3)) LIKE '%' + CAST(/*data.CONTENA_SHURUI_CD*/0 AS varchar(3)) + '%'/*END*/
/*IF data.CONTENA_SHURUI_NAME != null*/AND MCS.CONTENA_SHURUI_NAME LIKE '%' +  /*data.CONTENA_SHURUI_NAME*/ + '%'/*END*/
/*IF data.CONTENA_SHURUI_NAME_RYAKU != null*/AND MCS.CONTENA_SHURUI_NAME_RYAKU LIKE '%' +  /*data.CONTENA_SHURUI_NAME_RYAKU*/ + '%'/*END*/
/*IF data.CONTENA_SHURUI_FURIGANA != null*/AND MCS.CONTENA_SHURUI_FURIGANA LIKE '%' +  /*data.CONTENA_SHURUI_FURIGANA*/ + '%'/*END*/
/*IF data.CONTENA_SHURUI_BIKOU != null*/AND MCS.CONTENA_SHURUI_BIKOU LIKE '%' +  /*data.CONTENA_SHURUI_BIKOU*/ + '%'/*END*/
/*IF !data.TEKIYOU_BEGIN.IsNull*/ AND MCS.TEKIYOU_BEGIN LIKE '%' +  /*data.TEKIYOU_BEGIN.Value*/ + '%'/*END*/
/*IF !data.TEKIYOU_END.IsNull*/AND MCS.TEKIYOU_END LIKE '%' +  /*data.TEKIYOU_END.Value*/ + '%'/*END*/
/*IF data.CREATE_USER != null*/AND MCS.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
/*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, MCS.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
/*IF data.UPDATE_USER != null*/AND MCS.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
/*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, MCS.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*IF data.SEARCH_TEKIYOU_BEGIN != null*/AND CONVERT(nvarchar, MCS.TEKIYOU_BEGIN, 120) LIKE '%' +  /*data.SEARCH_TEKIYOU_BEGIN*/ + '%'/*END*/
/*IF data.SEARCH_TEKIYOU_END != null*/AND CONVERT(nvarchar, MCS.TEKIYOU_END, 120) LIKE '%' +  /*data.SEARCH_TEKIYOU_END*/ + '%'/*END*/
/*IF !deletechuFlg*/AND MCS.DELETE_FLG = 0/*END*/
/*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/AND (1 = 0/*END*/
/*IF tekiyounaiFlg*/OR (((MCS.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MCS.TEKIYOU_END) or (MCS.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and MCS.TEKIYOU_END IS NULL) or (MCS.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MCS.TEKIYOU_END) or (MCS.TEKIYOU_BEGIN IS NULL and MCS.TEKIYOU_END IS NULL)) and MCS.DELETE_FLG = 0)/*END*/
/*IF deletechuFlg*/OR MCS.DELETE_FLG = 1/*END*/
/*IF tekiyougaiFlg*/OR ((MCS.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > MCS.TEKIYOU_END) and MCS.DELETE_FLG = 0)/*END*/
/*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/ )/*END*/
/*IF tekiyounaiFlg && deletechuFlg && tekiyougaiFlg*/AND MCS.DELETE_FLG = 1/*END*/
/*IF tekiyounaiFlg && !deletechuFlg && tekiyougaiFlg*/AND MCS.DELETE_FLG = 0/*END*/
/*IF tekiyounaiFlg && deletechuFlg && !tekiyougaiFlg*/AND (((MCS.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MCS.TEKIYOU_END) or (MCS.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and MCS.TEKIYOU_END IS NULL) or (MCS.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MCS.TEKIYOU_END) or (MCS.TEKIYOU_BEGIN IS NULL and MCS.TEKIYOU_END IS NULL)) and MCS.DELETE_FLG = 1)/*END*/
/*IF !tekiyounaiFlg && deletechuFlg && tekiyougaiFlg*/AND ((MCS.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > MCS.TEKIYOU_END) and MCS.DELETE_FLG = 1)/*END*/
/*IF !tekiyounaiFlg && !deletechuFlg && !tekiyougaiFlg*/AND MCS.DELETE_FLG = 0/*END*/
/*END*/
ORDER BY MCS.CONTENA_SHURUI_CD
