﻿SELECT 
		T_CONTENA_RESERVE.CONTENA_SET_KBN,
		T_CONTENA_RESERVE.CONTENA_SHURUI_CD,
		T_CONTENA_RESERVE.CONTENA_CD,
		T_CONTENA_RESERVE.DAISUU_CNT,
		T_CONTENA_RESERVE.SYSTEM_ID,
		T_CONTENA_RESERVE.SEQ,
		M_CONTENA_SHURUI.CONTENA_SHURUI_NAME
FROM 
	T_CONTENA_RESERVE
INNER JOIN M_CONTENA_SHURUI
	ON M_CONTENA_SHURUI.CONTENA_SHURUI_CD = T_CONTENA_RESERVE.CONTENA_SHURUI_CD
WHERE
	T_CONTENA_RESERVE.DELETE_FLG = 0
	/*IF data.SYSTEM_ID != null*/ AND T_CONTENA_RESERVE.SYSTEM_ID = /*data.SYSTEM_ID*//*END*/
	/*IF !data.SEQ.IsNull*/ AND T_CONTENA_RESERVE.SEQ = /*data.SEQ.Value*//*END*/