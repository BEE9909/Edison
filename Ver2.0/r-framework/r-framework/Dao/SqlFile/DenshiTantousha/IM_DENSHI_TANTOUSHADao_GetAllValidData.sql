﻿SELECT * FROM dbo.M_DENSHI_TANTOUSHA
WHERE 
D/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.EDI_MEMBER_ID != null*/AND EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*//*END*/
/*IF data.TANTOUSHA_KBN != null*/AND TANTOUSHA_KBN = /*data.TANTOUSHA_KBN*//*END*/
/*IF data.TANTOUSHA_CD != null*/AND TANTOUSHA_CD = /*data.TANTOUSHA_CD*//*END*/
/*IF data.TANTOUSHA_NAME != null*/AND TANTOUSHA_NAME = /*data.TANTOUSHA_NAME*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
/*IF data.DELETE_FLG != null*/AND DELETE_FLG = /*data.DELETE_FLG*//*END*/
/*IF data.TIME_STAMP != null*/AND TIME_STAMP = /*data.TIME_STAMP*//*END*/
