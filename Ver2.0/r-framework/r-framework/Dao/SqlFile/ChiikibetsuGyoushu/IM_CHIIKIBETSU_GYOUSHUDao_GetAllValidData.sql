﻿SELECT * FROM dbo.M_CHIIKIBETSU_GYOUSHU
WHERE
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.CHIIKI_CD != null*/AND CHIIKI_CD = /*data.CHIIKI_CD*//*END*/
/*IF data.GYOUSHU_CD != null*/AND GYOUSHU_CD = /*data.GYOUSHU_CD*//*END*/
/*IF data.HOUKOKU_GYOUSHU_CD != null*/AND HOUKOKU_GYOUSHU_CD = /*data.HOUKOKU_GYOUSHU_CD*//*END*/
/*IF data.HOUKOKU_GYOUSHU_NAME != null*/AND HOUKOKU_GYOUSHU_NAME = /*data.HOUKOKU_GYOUSHU_NAME*//*END*/
/*IF data.CHIIKIBETSU_GYOUSHU_BIKOU != null*/AND CHIIKIBETSU_GYOUSHU_BIKOU = /*data.CHIIKIBETSU_GYOUSHU_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/