﻿SELECT * FROM dbo.M_NYUUKINSAKI
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.NYUUKINSAKI_CD != null*/AND NYUUKINSAKI_CD = /*data.NYUUKINSAKI_CD*//*END*/
/*IF data.NYUUKINSAKI_NAME1 != null*/AND NYUUKINSAKI_NAME1 = /*data.NYUUKINSAKI_NAME1*//*END*/
/*IF data.NYUUKINSAKI_NAME2 != null*/AND NYUUKINSAKI_NAME2 = /*data.NYUUKINSAKI_NAME2*//*END*/
/*IF data.NYUUKINSAKI_NAME_RYAKU != null*/AND NYUUKINSAKI_NAME_RYAKU = /*data.NYUUKINSAKI_NAME_RYAKU*//*END*/
/*IF data.NYUUKINSAKI_FURIGANA != null*/AND NYUUKINSAKI_FURIGANA = /*data.NYUUKINSAKI_FURIGANA*//*END*/
/*IF data.NYUUKINSAKI_TEL != null*/AND NYUUKINSAKI_TEL = /*data.NYUUKINSAKI_TEL*//*END*/
/*IF data.NYUUKINSAKI_FAX != null*/AND NYUUKINSAKI_FAX = /*data.NYUUKINSAKI_FAX*//*END*/
/*IF data.NYUUKINSAKI_POST != null*/AND NYUUKINSAKI_POST = /*data.NYUUKINSAKI_POST*//*END*/
/*IF !data.NYUUKINSAKI_TODOUFUKEN_CD.IsNull*/AND NYUUKINSAKI_TODOUFUKEN_CD = /*data.NYUUKINSAKI_TODOUFUKEN_CD.Value*//*END*/
/*IF data.NYUUKINSAKI_ADDRESS1 != null*/AND NYUUKINSAKI_ADDRESS1 = /*data.NYUUKINSAKI_ADDRESS1*//*END*/
/*IF data.NYUUKINSAKI_ADDRESS2 != null*/AND NYUUKINSAKI_ADDRESS2 = /*data.NYUUKINSAKI_ADDRESS2*//*END*/
/*IF !data.TORIKOMI_KBN.IsNull*/AND TORIKOMI_KBN = /*data.TORIKOMI_KBN.Value*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/