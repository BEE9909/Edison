﻿SELECT * FROM dbo.M_KONGOU_SHURUI
WHERE
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF !data.HAIKI_KBN_CD.IsNull*/AND HAIKI_KBN_CD = /*data.HAIKI_KBN_CD.Value*//*END*/
/*IF data.KONGOU_SHURUI_CD != null*/AND KONGOU_SHURUI_CD = /*data.KONGOU_SHURUI_CD*//*END*/
/*IF data.KONGOU_SHURUI_NAME != null*/AND KONGOU_SHURUI_NAME = /*data.KONGOU_SHURUI_NAME*//*END*/
/*IF data.KONGOU_SHURUI_NAME_RYAKU != null*/AND KONGOU_SHURUI_NAME_RYAKU = /*data.KONGOU_SHURUI_NAME_RYAKU*//*END*/
/*IF data.KONGOU_SHURUI_FURIGANA != null*/AND KONGOU_SHURUI_FURIGANA = /*data.KONGOU_SHURUI_FURIGANA*//*END*/
/*IF data.KONGOU_SHURUI_BIKOU != null*/AND KONGOU_SHURUI_BIKOU = /*data.KONGOU_SHURUI_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/