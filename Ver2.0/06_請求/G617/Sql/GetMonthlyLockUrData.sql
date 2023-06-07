﻿SELECT
	TMAU.TORIHIKISAKI_CD
	,TMAU.YEAR
	,TMAU.MONTH
	,TMAU.SEQ
	,TMAU.PREVIOUS_MONTH_BALANCE
	,TMAU.NYUUKIN_KINGAKU
	,TMAU.KINGAKU
	,TMAU.TAX
	,TMAU.SHIME_UTIZEI_GAKU
	,TMAU.SHIME_SOTOZEI_GAKU
	,TMAU.DEN_UTIZEI_GAKU
	,TMAU.DEN_SOTOZEI_GAKU
	,TMAU.MEI_UTIZEI_GAKU
	,TMAU.MEI_SOTOZEI_GAKU
	,TMAU.TOTAL_KINGAKU
	,TMAU.ZANDAKA
	,TMAU.INVOICE_KBN
	,TMAU.CREATE_USER
	,TMAU.CREATE_DATE
	,TMAU.CREATE_PC
	,TMAU.UPDATE_USER
	,TMAU.UPDATE_DATE
	,TMAU.UPDATE_PC
	,TMAU.DELETE_FLG
	,TMAU.TIME_STAMP
FROM
	T_MONTHLY_LOCK_UR TMAU
WHERE
	DELETE_FLG = 0
	/*IF data.TORIHIKISAKI_CD != null*/ AND TMAU.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001' /*END*/
	/*IF !data.YEAR.IsNull*/ AND TMAU.YEAR = /*data.YEAR*/1 /*END*/
	/*IF !data.MONTH.IsNull*/ AND TMAU.MONTH = /*data.MONTH*/1 /*END*/
	/*IF !data.SEQ.IsNull*/ AND TMAU.SEQ = /*data.SEQ*/1 /*END*/
ORDER BY TMAU.YEAR DESC, TMAU.MONTH DESC, TORIHIKISAKI_CD