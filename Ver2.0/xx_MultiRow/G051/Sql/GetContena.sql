﻿SELECT
TCR.DENSHU_KBN_CD,
TCR.SYSTEM_ID,
TCR.SEQ,
TCR.CONTENA_SET_KBN,
TCR.CONTENA_SHURUI_CD,
TCR.CONTENA_CD,
TCR.DAISUU_CNT,
TCR.DELETE_FLG,
TCR.TIME_STAMP
FROM
T_CONTENA_RESULT AS TCR
WHERE
TCR.DENSHU_KBN_CD = 1
AND TCR.SYSTEM_ID = /*sysId*/
AND TCR.DELETE_FLG = 0