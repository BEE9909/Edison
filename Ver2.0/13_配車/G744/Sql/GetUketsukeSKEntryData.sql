﻿SELECT T_USE.*
FROM
T_UKETSUKE_SK_ENTRY AS T_USE
WHERE
T_USE.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
AND T_USE.DELETE_FLG = 0