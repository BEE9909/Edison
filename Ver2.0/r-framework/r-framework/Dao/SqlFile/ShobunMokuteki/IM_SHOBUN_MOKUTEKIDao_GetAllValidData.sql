﻿SELECT * FROM dbo.M_SHOBUN_MOKUTEKI
WHERE 
CONVERT(DATE, ISNULL(TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL(TEKIYOU_END, DATEADD(day,1,GETDATE())))
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
AND DELETE_FLG = 0
/*END*/
/*IF data.SHOBUN_MOKUTEKI_CD != null*/AND SHOBUN_MOKUTEKI_CD = /*data.SHOBUN_MOKUTEKI_CD*//*END*/
/*IF data.SHOBUN_MOKUTEKI_NAME != null*/AND SHOBUN_MOKUTEKI_NAME = /*data.SHOBUN_MOKUTEKI_NAME*//*END*/
/*IF data.SHOBUN_MOKUTEKI_NAME_RYAKU != null*/AND SHOBUN_MOKUTEKI_NAME_RYAKU = /*data.SHOBUN_MOKUTEKI_NAME_RYAKU*//*END*/
/*IF data.SHOBUN_MOKUTEKI_BIKOU != null*/AND SHOBUN_MOKUTEKI_BIKOU = /*data.SHOBUN_MOKUTEKI_BIKOU*//*END*/
/*IF !data.TEKIYOU_BEGIN.IsNull*/AND TEKIYOU_BEGIN = /*data.TEKIYOU_BEGIN.Value*//*END*/
/*IF !data.TEKIYOU_END.IsNull*/AND TEKIYOU_END = /*data.TEKIYOU_END.Value*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
