﻿SELECT
	TMAS.TORIHIKISAKI_CD
	,TMAS.YEAR
	,TMAS.MONTH
	,TMAS.SEQ
	,TMAS.PREVIOUS_MONTH_BALANCE
	,TMAS.SHUKKIN_KINGAKU
	,TMAS.KINGAKU
	,TMAS.TAX
	,TMAS.TOTAL_KINGAKU
	,TMAS.ZANDAKA
	,TMAS.CREATE_USER
	,TMAS.CREATE_DATE
	,TMAS.CREATE_PC
	,TMAS.UPDATE_USER
	,TMAS.UPDATE_DATE
	,TMAS.UPDATE_PC
	,TMAS.DELETE_FLG
	,TMAS.TIME_STAMP
FROM
	T_MONTHLY_LOCK_SH TMAS
WHERE
	DELETE_FLG = 0
	/*IF data.TORIHIKISAKI_CD != null*/ AND TMAS.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001' /*END*/
	/*IF !data.YEAR.IsNull*/ AND TMAS.YEAR = /*data.YEAR*/1 /*END*/
	/*IF !data.MONTH.IsNull*/ AND TMAS.MONTH = /*data.MONTH*/1 /*END*/
	/*IF !data.SEQ.IsNull*/ AND TMAS.SEQ = /*data.SEQ*/1 /*END*/
ORDER BY TMAS.YEAR DESC, TMAS.MONTH DESC, TORIHIKISAKI_CD