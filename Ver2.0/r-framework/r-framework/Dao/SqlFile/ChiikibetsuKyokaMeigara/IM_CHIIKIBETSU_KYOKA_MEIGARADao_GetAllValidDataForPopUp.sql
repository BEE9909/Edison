﻿SELECT * FROM dbo.M_CHIIKIBETSU_KYOKA_MEIGARA
/*BEGIN*/WHERE
 /*IF !data.KYOKA_KBN.IsNull*/KYOKA_KBN = /*data.KYOKA_KBN.Value*//*END*/
 /*IF data.GYOUSHA_CD != null*/AND GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
 /*IF data.GENBA_CD != null*/AND GENBA_CD = /*data.GENBA_CD*//*END*/
 /*IF data.CHIIKI_CD != null*/AND CHIIKI_CD = /*data.CHIIKI_CD*//*END*/
 /*IF !data.TOKUBETSU_KANRI_KBN.IsNull*/ AND TOKUBETSU_KANRI_KBN = /*data.TOKUBETSU_KANRI_KBN*//*END*/
 /*IF data.HAIKI_SHURUI_CD != null*/AND HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*//*END*/
 /*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
 /*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
 /*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
 /*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
 /*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
 /*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
/*END*/
