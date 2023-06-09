﻿/* 20140523 ria No.679 伝種区分連携 start*/
SELECT
TUSSE.UKETSUKE_NUMBER,TUSSD.ROW_NO,
1 as ORDERKBN
FROM T_UKETSUKE_SS_ENTRY TUSSE
left join T_UKETSUKE_SS_DETAIL TUSSD ON (TUSSE.SYSTEM_ID = TUSSD.SYSTEM_ID AND TUSSE.SEQ = TUSSD.SEQ)
WHERE TUSSE.DELETE_FLG = 0
AND TUSSE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/ 
/*IF data.RENKEI_MEISAI_SYSTEM_ID != NULL && data.RENKEI_MEISAI_SYSTEM_ID != ''*/
AND TUSSD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/
/*END*/
AND /*data.RENKEI_MANI_FLAG*/0 = 1
UNION ALL
SELECT
TUMKE.UKETSUKE_NUMBER,TUMKD.ROW_NO,
2 as ORDERKBN
FROM T_UKETSUKE_MK_ENTRY TUMKE
left join T_UKETSUKE_MK_DETAIL TUMKD ON (TUMKE.SYSTEM_ID = TUMKD.SYSTEM_ID AND TUMKE.SEQ = TUMKD.SEQ)
WHERE TUMKE.DELETE_FLG = 0
AND TUMKE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/ 
/*IF data.RENKEI_MEISAI_SYSTEM_ID != NULL && data.RENKEI_MEISAI_SYSTEM_ID != ''*/
AND TUMKD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/
/*END*/
AND /*data.RENKEI_MANI_FLAG*/0 = 1
UNION ALL
SELECT
TUSKE.UKETSUKE_NUMBER,TUSKD.ROW_NO,
3 as ORDERKBN
FROM T_UKETSUKE_SK_ENTRY TUSKE
left join T_UKETSUKE_SK_DETAIL TUSKD ON (TUSKE.SYSTEM_ID = TUSKD.SYSTEM_ID AND TUSKE.SEQ = TUSKD.SEQ)
WHERE TUSKE.DELETE_FLG = 0
AND TUSKE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/ 
/*IF data.RENKEI_MEISAI_SYSTEM_ID != NULL && data.RENKEI_MEISAI_SYSTEM_ID != ''*/
AND TUSKD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/
/*END*/
AND /*data.RENKEI_MANI_FLAG*/0 = 2
ORDER BY ORDERKBN
/* 20140523 ria No.679 伝種区分連携 start*/