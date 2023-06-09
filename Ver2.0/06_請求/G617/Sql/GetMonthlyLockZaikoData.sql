﻿SELECT
	TMLZ.GYOUSHA_CD
	,TMLZ.GENBA_CD
	,TMLZ.ZAIKO_HINMEI_CD
	,TMLZ.YEAR
	,TMLZ.MONTH
	,TMLZ.SEQ
	,TMLZ.PREVIOUS_MONTH_ZAIKO_RYOU
	,TMLZ.PREVIOUS_MONTH_KINGAKU
	,TMLZ.UKEIRE_RYOU
	,TMLZ.SHUKKA_RYOU
	,TMLZ.TYOUSEI_RYOU
	,TMLZ.IDOU_RYOU
	,TMLZ.MONTH_ZAIKO_RYOU
	,TMLZ.MONTH_KINGAKU
	,TMLZ.GOUKEI_ZAIKO_RYOU
	,TMLZ.GOUKEI_KINGAKU
	,TMLZ.CREATE_USER
	,TMLZ.CREATE_DATE
	,TMLZ.CREATE_PC
	,TMLZ.UPDATE_USER
	,TMLZ.UPDATE_DATE
	,TMLZ.UPDATE_PC
	,TMLZ.DELETE_FLG
	,TMLZ.TIME_STAMP	
FROM
	T_MONTHLY_LOCK_ZAIKO TMLZ
WHERE
	DELETE_FLG = 0
	/*IF data.GYOUSHA_CD != null*/ AND TMLZ.GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001' /*END*/
	/*IF data.GENBA_CD != null*/ AND TMLZ.GENBA_CD = /*data.GENBA_CD*/'000001' /*END*/
	/*IF data.ZAIKO_HINMEI_CD != null*/ AND TMLZ.ZAIKO_HINMEI_CD = /*data.ZAIKO_HINMEI_CD*/'000001' /*END*/
	/*IF !data.YEAR.IsNull*/ AND TMLZ.YEAR = /*data.YEAR*/1 /*END*/
	/*IF !data.MONTH.IsNull*/ AND TMLZ.MONTH = /*data.MONTH*/1 /*END*/
	/*IF !data.SEQ.IsNull*/ AND TMLZ.SEQ = /*data.SEQ*/1 /*END*/
ORDER BY TMLZ.YEAR DESC, TMLZ.MONTH DESC, TMLZ.GYOUSHA_CD, TMLZ.GENBA_CD, TMLZ.ZAIKO_HINMEI_CD