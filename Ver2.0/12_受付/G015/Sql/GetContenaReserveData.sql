﻿SELECT  T_CR.*
FROM	T_CONTENA_RESERVE AS T_CR
WHERE	T_CR.SYSTEM_ID = /*data.SystemID*/
AND		T_CR.SEQ = /*data.SEQ*/
--AND		T_CR.CONTENA_SET_KBN = /*data.ContenaSetKbn*/
AND		T_CR.DELETE_FLG = 0
