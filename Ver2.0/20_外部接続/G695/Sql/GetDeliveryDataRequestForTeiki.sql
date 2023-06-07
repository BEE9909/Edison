﻿SELECT 
	MSR.DIGI_SHARYOU_CD AS CAR_ID
	,MDG.POINT_ID
	,TTE.SAGYOU_DATE AS ARRIVAL_TIME
	,TTE.SAGYOU_DATE AS DEPARTURE_TIME
	,ISNULL(TTS.ROW_NUMBER, 0) AS GOODS_DETAIL_NO
	,MDH.DIGI_HINMEI_CD AS GOODS_ID
	,0 AS GOODS_COUNT	--数量は0で送る
	,MDU.DIGI_UNIT_CD AS GOODS_UNIT_ID
	--増えるかもしれないらしい
FROM T_TEIKI_HAISHA_ENTRY        AS TTE
LEFT JOIN T_TEIKI_HAISHA_DETAIL  AS TTD ON TTE.SYSTEM_ID = TTD.SYSTEM_ID AND TTE.SEQ = TTD.SEQ
LEFT JOIN T_TEIKI_HAISHA_SHOUSAI AS TTS ON TTD.SYSTEM_ID = TTS.SYSTEM_ID AND TTD.SEQ = TTS.SEQ AND TTD.DETAIL_SYSTEM_ID = TTS.DETAIL_SYSTEM_ID 
LEFT JOIN M_DIGI_OUTPUT_SHARYOU  AS MSR ON TTE.UNPAN_GYOUSHA_CD = MSR.GYOUSHA_CD AND TTE.SHARYOU_CD = MSR.SHARYOU_CD
LEFT JOIN M_GENBA_DIGI           AS MDG ON TTD.GYOUSHA_CD = MDG.GYOUSHA_CD AND TTD.GENBA_CD = MDG.GENBA_CD
LEFT JOIN M_DIGI_OUTPUT_HINMEI   AS MDH ON TTS.HINMEI_CD = MDH.HINMEI_CD
LEFT JOIN M_DIGI_OUTPUT_UNIT     AS MDU ON TTS.UNIT_CD = MDU.UNIT_CD
WHERE 
	TTE.DELETE_FLG = 0
	AND TTE.SYSTEM_ID = /*SysId*/
	AND TTD.SYSTEM_ID = /*SysId*/
	AND TTD.DETAIL_SYSTEM_ID = /*SysDetId*/
