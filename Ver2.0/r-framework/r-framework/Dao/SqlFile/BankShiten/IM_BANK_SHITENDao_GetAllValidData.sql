﻿SELECT * FROM dbo.M_BANK_SHITEN
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.BANK_CD != null*/AND BANK_CD = /*data.BANK_CD*//*END*/
/*IF data.BANK_SHITEN_CD != null*/AND BANK_SHITEN_CD = /*data.BANK_SHITEN_CD*//*END*/
/*IF data.BANK_SHITEN_NAME != null*/AND BANK_SHITEN_NAME = /*data.BANK_SHITEN_NAME*//*END*/
/*IF data.BANK_SHIETN_NAME_RYAKU != null*/AND BANK_SHIETN_NAME_RYAKU = /*data.BANK_SHIETN_NAME_RYAKU*//*END*/
/*IF data.BANK_SHITEN_FURIGANA != null*/AND BANK_SHITEN_FURIGANA = /*data.BANK_SHITEN_FURIGANA*//*END*/
/*IF !data.KOUZA_SHURUI_CD.IsNull*/AND M_BANK_SHITEN.KOUZA_SHURUI_CD = /*data.KOUZA_SHURUI_CD*//*END*/
/*IF data.KOUZA_SHURUI != null*/AND KOUZA_SHURUI = /*data.KOUZA_SHURUI*//*END*/
/*IF data.KOUZA_NO != null*/AND KOUZA_NO = /*data.KOUZA_NO*//*END*/
/*IF data.KOUZA_NAME != null*/AND KOUZA_NAME = /*data.KOUZA_NAME*//*END*/
/*IF data.RENKEI_CD != null*/AND RENKEI_CD = /*data.RENKEI_CD*//*END*/
/*IF data.BANK_SHITEN_BIKOU != null*/AND BANK_SHITEN_BIKOU = /*data.BANK_SHITEN_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
