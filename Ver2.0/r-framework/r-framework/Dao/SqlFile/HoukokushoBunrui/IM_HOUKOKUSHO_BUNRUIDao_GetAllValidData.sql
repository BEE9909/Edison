﻿SELECT * FROM dbo.M_HOUKOKUSHO_BUNRUI
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.HOUKOKUSHO_BUNRUI_CD != null*/AND HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*//*END*/
/*IF data.HOUKOKUSHO_BUNRUI_NAME != null*/AND HOUKOKUSHO_BUNRUI_NAME = /*data.HOUKOKUSHO_BUNRUI_NAME*//*END*/
/*IF data.HOUKOKUSHO_BUNRUI_NAME_RYAKU != null*/AND HOUKOKUSHO_BUNRUI_NAME_RYAKU = /*data.HOUKOKUSHO_BUNRUI_NAME_RYAKU*//*END*/
/*IF data.HOUKOKUSHO_BUNRUI_FURIGANA != null*/AND HOUKOKUSHO_BUNRUI_FURIGANA = /*data.HOUKOKUSHO_BUNRUI_FURIGANA*//*END*/
/*IF data.HOUKOKUSHO_BUNRUI_BIKOU != null*/AND HOUKOKUSHO_BUNRUI_BIKOU = /*data.HOUKOKUSHO_BUNRUI_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
