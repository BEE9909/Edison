﻿SELECT * FROM dbo.M_CHIIKIBETSU_KYOKA
WHERE 
 DELETE_FLG = 0
/*IF !data.KYOKA_KBN.IsNull*/AND KYOKA_KBN = /*data.KYOKA_KBN.Value*//*END*/
/*IF data.GYOUSHA_CD != null*/AND GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.GENBA_CD != null*/AND GENBA_CD = /*data.GENBA_CD*//*END*/
/*IF data.CHIIKI_CD != null*/AND CHIIKI_CD = /*data.CHIIKI_CD*//*END*/
/*IF data.FUTSUU_KYOKA_NO != null*/AND FUTSUU_KYOKA_NO = /*data.FUTSUU_KYOKA_NO*//*END*/
/*IF !data.FUTSUU_KYOKA_BEGIN.IsNull*/AND FUTSUU_KYOKA_BEGIN = /*data.FUTSUU_KYOKA_BEGIN.Value*//*END*/
/*IF !data.FUTSUU_KYOKA_END.IsNull*/AND FUTSUU_KYOKA_END = /*data.FUTSUU_KYOKA_END.Value*//*END*/
/*IF data.FUTSUU_KYOKA_FILE_PATH != null*/AND FUTSUU_KYOKA_FILE_PATH = /*data.FUTSUU_KYOKA_FILE_PATH*//*END*/
/*IF data.TOKUBETSU_KYOKA_NO != null*/AND TOKUBETSU_KYOKA_NO = /*data.TOKUBETSU_KYOKA_NO*//*END*/
/*IF !data.TOKUBETSU_KYOKA_BEGIN.IsNull*/AND TOKUBETSU_KYOKA_BEGIN = /*data.TOKUBETSU_KYOKA_BEGIN.Value*//*END*/
/*IF !data.TOKUBETSU_KYOKA_END.IsNull*/AND TOKUBETSU_KYOKA_END = /*data.TOKUBETSU_KYOKA_END.Value*//*END*/
/*IF data.TOKUBETSU_KYOKA_FILE_PATH != null*/AND TOKUBETSU_KYOKA_FILE_PATH = /*data.TOKUBETSU_KYOKA_FILE_PATH*//*END*/
/*IF data.CHIIKIBETSU_KYOKA_BIKOU != null*/AND CHIIKIBETSU_KYOKA_BIKOU = /*data.CHIIKIBETSU_KYOKA_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/