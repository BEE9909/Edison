﻿SELECT  T_USSD.*
FROM    T_UKETSUKE_SS_DETAIL AS T_USSD
WHERE	T_USSD.SYSTEM_ID = /*data.SystemId*/
		AND T_USSD.SEQ = /*data.Seq*/
ORDER BY T_USSD.ROW_NO