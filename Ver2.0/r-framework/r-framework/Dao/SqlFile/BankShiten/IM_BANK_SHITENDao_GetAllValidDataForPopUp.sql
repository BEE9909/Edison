﻿SELECT M_BANK.BANK_CD, M_BANK.BANK_NAME_RYAKU, M_BANK_SHITEN.BANK_SHITEN_CD, M_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU, M_BANK_SHITEN.KOUZA_SHURUI, M_BANK_SHITEN.KOUZA_NO, M_BANK_SHITEN.KOUZA_NAME 
FROM dbo.M_BANK_SHITEN AS M_BANK_SHITEN
LEFT JOIN dbo.M_BANK AS M_BANK ON M_BANK.BANK_CD = M_BANK_SHITEN.BANK_CD 
 AND M_BANK.DELETE_FLG = 0
AND M_BANK.DELETE_FLG = 0
WHERE
 M_BANK_SHITEN.DELETE_FLG = 0
/*IF data.BANK_CD != null*/AND M_BANK_SHITEN.BANK_CD = /*data.BANK_CD*//*END*/
/*IF data.BANK_SHITEN_CD != null*/AND M_BANK_SHITEN.BANK_SHITEN_CD = /*data.BANK_SHITEN_CD*//*END*/
/*IF data.BANK_SHITEN_NAME != null*/AND M_BANK_SHITEN.BANK_SHITEN_NAME = /*data.BANK_SHITEN_NAME*//*END*/
/*IF data.BANK_SHIETN_NAME_RYAKU != null*/AND M_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU = /*data.BANK_SHIETN_NAME_RYAKU*//*END*/
/*IF data.BANK_SHITEN_FURIGANA != null*/AND M_BANK_SHITEN.BANK_SHITEN_FURIGANA = /*data.BANK_SHITEN_FURIGANA*//*END*/
/*IF !data.KOUZA_SHURUI_CD.IsNull*/AND M_BANK_SHITEN.KOUZA_SHURUI_CD = /*data.KOUZA_SHURUI_CD*//*END*/
/*IF data.KOUZA_SHURUI != null*/AND M_BANK_SHITEN.KOUZA_SHURUI = /*data.KOUZA_SHURUI*//*END*/
/*IF data.KOUZA_NO != null*/AND M_BANK_SHITEN.KOUZA_NO = /*data.KOUZA_NO*//*END*/
/*IF data.KOUZA_NAME != null*/AND M_BANK_SHITEN.KOUZA_NAME = /*data.KOUZA_NAME*//*END*/
/*IF data.RENKEI_CD != null*/AND M_BANK_SHITEN.RENKEI_CD = /*data.RENKEI_CD*//*END*/
/*IF data.BANK_SHITEN_BIKOU != null*/AND M_BANK_SHITEN.BANK_SHITEN_BIKOU = /*data.BANK_SHITEN_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND M_BANK_SHITEN.CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND M_BANK_SHITEN.CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND M_BANK_SHITEN.CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND M_BANK_SHITEN.UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND M_BANK_SHITEN.UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND M_BANK_SHITEN.UPDATE_PC = /*data.UPDATE_PC*//*END*/
