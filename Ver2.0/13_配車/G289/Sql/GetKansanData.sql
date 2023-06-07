﻿ SELECT 
	KANSAN.GYOUSHA_CD
	,KANSAN.GENBA_CD
	,KANSAN.HINMEI_CD
	,KANSAN.UNIT_CD
	,UNIT.UNIT_NAME_RYAKU
	,KANSAN.KANSANCHI
	,KANSAN.KEIJYOU_KBN
	,KANSAN.KANSAN_UNIT_CD 
	,UNITKANSAN.UNIT_NAME_RYAKU  AS UNITKANSAN_NAME
	,KANSAN.MONDAY
	,KANSAN.TUESDAY
	,KANSAN.WEDNESDAY
	,KANSAN.THURSDAY
	,KANSAN.FRIDAY
	,KANSAN.SATURDAY
	,KANSAN.SUNDAY
    ,KANSAN.DENPYOU_KBN_CD
	,DEN.DENPYOU_KBN_NAME_RYAKU AS DENPYOU_KBN_CD_NM
	,KANSAN.KEIYAKU_KBN
	,HIN.HINMEI_NAME_RYAKU
	,KANSAN.ROW_NO
	,KANSAN.KANSAN_UNIT_MOBILE_OUTPUT_FLG

 FROM
	M_GENBA_TEIKI_HINMEI AS KANSAN
		LEFT JOIN M_HINMEI HIN
			ON KANSAN.HINMEI_CD = HIN.HINMEI_CD
		LEFT JOIN M_UNIT AS UNIT
			ON KANSAN.UNIT_CD = UNIT.UNIT_CD
		LEFT JOIN M_UNIT AS UNITKANSAN
			ON KANSAN.KANSAN_UNIT_CD = UNITKANSAN.UNIT_CD
		LEFT JOIN M_DENPYOU_KBN AS DEN
			ON KANSAN.DENPYOU_KBN_CD = DEN.DENPYOU_KBN_CD
 /*BEGIN*/WHERE
 /*IF data.GyoushaCd != null && data.GyoushaCd != ''*/ KANSAN.GYOUSHA_CD = /*data.GyoushaCd*/ /*END*/
 /*IF data.GenbaCd != null && data.GenbaCd != ''*/ AND  KANSAN.GENBA_CD = /*data.GenbaCd*/ /*END*/
 /*IF data.HinmeiCd != null && data.HinmeiCd != ''*/ AND KANSAN.HINMEI_CD = /*data.HinmeiCd*/ /*END*/
 /*IF data.UnitCd != null && data.UnitCd != 0 */ AND KANSAN.UNIT_CD = /*data.UnitCd*/ /*END*/
 /*IF data.DenpyouKbnCd != null && data.DenpyouKbnCd != 0 */ AND KANSAN.DENPYOU_KBN_CD = /*data.DenpyouKbnCd*/ /*END*/
 /*END*/
 ORDER BY KANSAN.GYOUSHA_CD,KANSAN.GENBA_CD,KANSAN.ROW_NO