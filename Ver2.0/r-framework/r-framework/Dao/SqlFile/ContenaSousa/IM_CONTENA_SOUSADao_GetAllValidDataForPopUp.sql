﻿SELECT M_CONTENA_SOUSA.CONTENA_SOUSA_CD, M_CONTENA_SOUSA.CONTENA_SOUSA_NAME_RYAKU FROM dbo.M_CONTENA_SOUSA AS M_CONTENA_SOUSA
WHERE 
 DELETE_FLG = 0
/*IF !data.CONTENA_SOUSA_CD.IsNull*/AND CONTENA_SOUSA_CD = /*data.CONTENA_SOUSA_CD.Value*//*END*/
/*IF data.CONTENA_SOUSA_NAME != null*/AND CONTENA_SOUSA_NAME = /*data.CONTENA_SOUSA_NAME*//*END*/
/*IF data.CONTENA_SOUSA_NAME_RYAKU != null*/AND CONTENA_SOUSA_NAME_RYAKU = /*data.CONTENA_SOUSA_NAME_RYAKU*//*END*/
/*IF data.CONTENA_SOUSA_BIKOU != null*/AND CONTENA_SOUSA_BIKOU = /*data.CONTENA_SOUSA_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/