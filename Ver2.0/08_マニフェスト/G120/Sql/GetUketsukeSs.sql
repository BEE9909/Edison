﻿SELECT 
TUSSE.UKETSUKE_NUMBER,
TUSSD.ROW_NO
FROM  T_UKETSUKE_SS_ENTRY TUSSE 
left join T_UKETSUKE_SS_DETAIL TUSSD ON (TUSSE.SYSTEM_ID = TUSSD.SYSTEM_ID AND TUSSE.SEQ = TUSSD.SEQ
 AND TUSSD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/0)
WHERE TUSSE.DELETE_FLG = 0	
AND TUSSE.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/0