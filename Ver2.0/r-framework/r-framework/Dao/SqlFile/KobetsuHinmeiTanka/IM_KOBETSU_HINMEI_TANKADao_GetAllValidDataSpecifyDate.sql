﻿SELECT * FROM dbo.M_KOBETSU_HINMEI_TANKA
WHERE 
DELETE_FLG = 0
/*IF !referenceDate.IsNull*/
AND CONVERT(DATE, ISNULL(TEKIYOU_BEGIN, DATEADD(day,-1,/*referenceDate*/))) <= CONVERT(DATE, /*referenceDate*/) 
and CONVERT(DATE, /*referenceDate*/) <= CONVERT(DATE, ISNULL(TEKIYOU_END, DATEADD(day,1,/*referenceDate*/)))
/*END*/
/*IF !data.SYS_ID.IsNull*/AND SYS_ID = /*data.SYS_ID.Value*//*END*/
/*IF !data.DENPYOU_KBN_CD.IsNull*/AND DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD.Value*//*END*/
/*IF data.TORIHIKISAKI_CD != null*/AND TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*//*END*/
/*IF data.GYOUSHA_CD != null*/AND GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.GENBA_CD != null*/AND GENBA_CD = /*data.GENBA_CD*//*END*/
/*IF data.HINMEI_CD != null*/AND HINMEI_CD = /*data.HINMEI_CD*//*END*/
/*IF !data.DENSHU_KBN_CD.IsNull*/AND DENSHU_KBN_CD = /*data.DENSHU_KBN_CD.Value*//*END*/
/*IF !data.UNIT_CD.IsNull*/AND UNIT_CD = /*data.UNIT_CD.Value*//*END*/
/*IF data.UNPAN_GYOUSHA_CD != null*/AND UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*//*END*/
/*IF data.NIOROSHI_GYOUSHA_CD != null*/AND NIOROSHI_GYOUSHA_CD = /*data.NIOROSHI_GYOUSHA_CD*//*END*/
/*IF data.NIOROSHI_GENBA_CD != null*/AND NIOROSHI_GENBA_CD = /*data.NIOROSHI_GENBA_CD*//*END*/
/*IF !data.TANKA.IsNull*/AND TANKA = /*data.TANKA.Value*//*END*/
/*IF data.BIKOU != null*/AND BIKOU = /*data.BIKOU*//*END*/
/*IF !data.TEKIYOU_BEGIN.IsNull*/AND TEKIYOU_BEGIN = /*data.TEKIYOU_BEGIN.Value*//*END*/
/*IF !data.TEKIYOU_END.IsNull*/AND TEKIYOU_END = /*data.TEKIYOU_END.Value*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
