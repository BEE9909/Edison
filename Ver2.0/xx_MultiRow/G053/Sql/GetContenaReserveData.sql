﻿SELECT  T_CR.*
FROM	T_CONTENA_RESERVE AS T_CR
WHERE	T_CR.SYSTEM_ID = /*sysId*/
AND		T_CR.SEQ = /*SEQ*/
AND		T_CR.DELETE_FLG = 0
