﻿SELECT * FROM dbo.M_DENSHI_HAIKI_NAME
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.EDI_MEMBER_ID != null*/AND EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*//*END*/
/*IF data.HAIKI_NAME_CD != null*/AND HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
/*IF !data.DELETE_FLG.IsNull*/AND DELETE_FLG = /*data.DELETE_FLG*//*END*/
/*IF data.TIME_STAMP != null*/AND TIME_STAMP = /*data.TIME_STAMP*//*END*/
