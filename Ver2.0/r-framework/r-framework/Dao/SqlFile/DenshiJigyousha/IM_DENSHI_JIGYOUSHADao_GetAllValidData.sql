﻿SELECT * FROM dbo.M_DENSHI_JIGYOUSHA
/*BEGIN*/WHERE 
/*IF data.EDI_MEMBER_ID != null*/AND EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*//*END*/
/*IF data.EDI_PASSWORD != null*/AND EDI_PASSWORD = /*data.EDI_PASSWORD*//*END*/
/*IF data.JIGYOUSHA_NAME != null*/AND JIGYOUSHA_NAME = /*data.JIGYOUSHA_NAME*//*END*/
/*IF data.JIGYOUSHA_POST != null*/AND JIGYOUSHA_POST = /*data.JIGYOUSHA_POST*//*END*/
/*IF data.JIGYOUSHA_ADDRESS1 != null*/AND JIGYOUSHA_ADDRESS1 = /*data.JIGYOUSHA_ADDRESS1*//*END*/
/*IF data.JIGYOUSHA_ADDRESS2 != null*/AND JIGYOUSHA_ADDRESS2 = /*data.JIGYOUSHA_ADDRESS2*//*END*/
/*IF data.JIGYOUSHA_ADDRESS3 != null*/AND JIGYOUSHA_ADDRESS3 = /*data.JIGYOUSHA_ADDRESS3*//*END*/
/*IF data.JIGYOUSHA_ADDRESS4 != null*/AND JIGYOUSHA_ADDRESS4 = /*data.JIGYOUSHA_ADDRESS4*//*END*/
/*IF data.JIGYOUSHA_TEL != null*/AND JIGYOUSHA_TEL = /*data.JIGYOUSHA_TEL*//*END*/
/*IF data.JIGYOUSHA_FAX != null*/AND JIGYOUSHA_FAX = /*data.JIGYOUSHA_FAX*//*END*/
/*IF !data.HST_KBN.IsNull*/AND HST_KBN = /*data.HST_KBN*//*END*/
/*IF !data.UPN_KBN.IsNull*/AND UPN_KBN = /*data.UPN_KBN*//*END*/
/*IF !data.SBN_KBN.IsNull*/AND SBN_KBN = /*data.SBN_KBN*//*END*/
/*IF !data.HOUKOKU_HUYOU_KBN.IsNull*/AND HOUKOKU_HUYOU_KBN = /*data.HOUKOKU_HUYOU_KBN*//*END*/
/*IF data.GYOUSHA_CD != null*/AND GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
/*IF data.TIME_STAMP != null*/AND TIME_STAMP = /*data.TIME_STAMP*//*END*/
/*END*/