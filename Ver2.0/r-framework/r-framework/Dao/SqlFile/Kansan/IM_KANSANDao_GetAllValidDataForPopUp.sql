﻿SELECT JOIN_M_KANSAN2.DENPYOU_KBN_CD, JOIN_M_KANSAN2.DENPYOU_KBN_NAME_RYAKU, JOIN_M_KANSAN2.HINMEI_CD, JOIN_M_KANSAN2.HINMEI_NAME_RYAKU, JOIN_M_KANSAN2.UNIT_CD, M_UNIT.UNIT_NAME_RYAKU, JOIN_M_KANSAN2.KANSANSHIKI, JOIN_M_KANSAN2.KANSANCHI FROM 
(
SELECT JOIN_M_KANSAN.*, M_HINMEI.HINMEI_NAME_RYAKU FROM 
(
SELECT M_DENPYOU_KBN.DENPYOU_KBN_NAME_RYAKU, M_KANSAN.* 
FROM dbo.M_KANSAN AS M_KANSAN 
LEFT JOIN dbo.M_DENPYOU_KBN AS M_DENPYOU_KBN ON M_KANSAN.DENPYOU_KBN_CD=M_DENPYOU_KBN.DENPYOU_KBN_CD 
AND M_DENPYOU_KBN.DELETE_FLG = 0
WHERE 
 M_KANSAN.DELETE_FLG = 0
) AS JOIN_M_KANSAN
LEFT JOIN dbo.M_HINMEI AS M_HINMEI ON M_HINMEI.HINMEI_CD = JOIN_M_KANSAN.HINMEI_CD 
 M_HINMEI.DELETE_FLG = 0
) AS JOIN_M_KANSAN2 
LEFT JOIN dbo.M_UNIT AS M_UNIT ON JOIN_M_KANSAN2.UNIT_CD = M_UNIT.UNIT_CD 
 M_UNIT.DELETE_FLG = 0
/*IF !data.DENPYOU_KBN_CD.IsNull*/AND JOIN_M_KANSAN2.DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD.Value*//*END*/
/*IF data.HINMEI_CD != null*/AND JOIN_M_KANSAN2.HINMEI_CD = /*data.HINMEI_CD*//*END*/
/*IF !data.UNIT_CD.IsNull*/AND JOIN_M_KANSAN2.UNIT_CD = /*data.UNIT_CD.Value*//*END*/
/*IF !data.KANSANSHIKI.IsNull*/AND JOIN_M_KANSAN2.KANSANSHIKI = /*data.KANSANSHIKI.Value*//*END*/
/*IF !data.KANSANCHI.IsNull*/AND JOIN_M_KANSAN2.KANSANCHI = /*data.KANSANCHI.Value*//*END*/
/*IF data.KANSAN_BIKOU != null*/AND JOIN_M_KANSAN2.KANSAN_BIKOU = /*data.KANSAN_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND JOIN_M_KANSAN2.CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND JOIN_M_KANSAN2.CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND JOIN_M_KANSAN2.CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND JOIN_M_KANSAN2.UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND JOIN_M_KANSAN2.UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND JOIN_M_KANSAN2.UPDATE_PC = /*data.UPDATE_PC*//*END*/