﻿SELECT M_SHARYOU.* FROM dbo.M_SHARYOU
LEFT JOIN M_GYOUSHA ON M_GYOUSHA.GYOUSHA_CD = M_SHARYOU.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 M_SHARYOU.DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.GYOUSHA_CD != null*/AND M_SHARYOU.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.SHARYOU_CD != null*/AND SHARYOU_CD = /*data.SHARYOU_CD*//*END*/
/*IF data.SHARYOU_NAME != null*/AND SHARYOU_NAME = /*data.SHARYOU_NAME*//*END*/
/*IF data.SHARYOU_NAME_RYAKU != null*/AND SHARYOU_NAME_RYAKU = /*data.SHARYOU_NAME_RYAKU*//*END*/
/*IF data.SHASYU_CD != null*/AND SHASYU_CD = /*data.SHASYU_CD*//*END*/
/*IF data.SHAIN_CD != null*/AND SHAIN_CD = /*data.SHAIN_CD*//*END*/
/*IF !data.SAIDAI_SEKISAI.IsNull*/AND SAIDAI_SEKISAI = /*data.SAIDAI_SEKISAI.Value*//*END*/
/*IF !data.KUUSHA_JYURYO.IsNull*/AND KUUSHA_JYURYO = /*data.KUUSHA_JYURYO.Value*//*END*/
/*IF data.SHARYOU_BIKOU != null*/AND SHARYOU_BIKOU = /*data.SHARYOU_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND M_SHARYOU.CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND M_SHARYOU.CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND M_SHARYOU.CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND M_SHARYOU.UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND M_SHARYOU.UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND M_SHARYOU.UPDATE_PC = /*data.UPDATE_PC*//*END*/
/*IF GYOUSHAKBN == 1*/AND M_GYOUSHA.GYOUSHAKBN_UKEIRE = 1/*END*/
/*IF GYOUSHAKBN == 2*/AND M_GYOUSHA.GYOUSHAKBN_SHUKKA = 1/*END*/
/*IF GYOUSHAKBN == 3*/AND M_GYOUSHA.GYOUSHAKBN_SHUKKA = 1 AND M_GYOUSHA.GYOUSHAKBN_UKEIRE = 1/*END*/
/*IF GYOUSHAKBN == 4*/AND (M_GYOUSHA.GYOUSHAKBN_SHUKKA = 1 OR M_GYOUSHA.GYOUSHAKBN_UKEIRE = 1)/*END*/
/*IF UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue*/AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = 1/*END*/
/*IF GYOUSHAKBN_MANI.IsTrue*/AND M_GYOUSHA.GYOUSHAKBN_MANI = 1/*END*/
/*IF ISNOT_NEED_TEKIYOU_FLG.IsNull || ISNOT_NEED_TEKIYOU_FLG.IsFalse*/
  /*IF !TEKIYOU_DATE.IsNull*/ 
    AND (((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, /*TEKIYOU_DATE*/, 120) AND CONVERT(DATETIME, /*TEKIYOU_DATE*/, 120) <= M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, /*TEKIYOU_DATE*/, 120) AND M_GYOUSHA.TEKIYOU_END IS NULL) OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, /*TEKIYOU_DATE*/, 120) <= M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL AND M_GYOUSHA.TEKIYOU_END IS NULL)) AND M_GYOUSHA.DELETE_FLG <> 1) /*END*/
  /*IF TEKIYOU_DATE.IsNull*/ 
    AND (((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND M_GYOUSHA.TEKIYOU_END IS NULL) OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL AND M_GYOUSHA.TEKIYOU_END IS NULL)) AND M_GYOUSHA.DELETE_FLG <> 1) /*END*/
/*END*/