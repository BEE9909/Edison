﻿SELECT * FROM dbo.M_GENBAMEMO_HYOUDAI
WHERE DELETE_FLG = 0
/*IF data.GENBAMEMO_HYOUDAI_CD != null*/AND GENBAMEMO_HYOUDAI_CD = /*data.GENBAMEMO_HYOUDAI_CD*//*END*/
/*IF data.GENBAMEMO_HYOUDAI_NAME != null*/AND GENBAMEMO_HYOUDAI_NAME = /*data.GENBAMEMO_HYOUDAI_NAME*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
