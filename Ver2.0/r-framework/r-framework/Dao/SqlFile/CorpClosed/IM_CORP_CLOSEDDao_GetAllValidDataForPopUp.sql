﻿SELECT * FROM dbo.M_CORP_CLOSED
WHERE 
DELETE_FLG = 0
/*IF !data.KYOTEN_CD.IsNull*/AND SHA.KYOTEN_CD = /*data.KYOTEN_CD.Value*//*END*/
/*IF !data.CORP_CLOSED_DATE.IsNull*/AND SHA.CORP_CLOSED_DATE = /*data.CORP_CLOSED_DATE.Value*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
