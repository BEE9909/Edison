﻿ SELECT 
 T_UKETSUKE_SS_DETAIL.HINMEI_CD
,T_UKETSUKE_SS_DETAIL.HINMEI_NAME
,T_UKETSUKE_SS_DETAIL.DENPYOU_KBN_CD
,T_UKETSUKE_SS_DETAIL.SUURYOU
,T_UKETSUKE_SS_DETAIL.UNIT_CD
,T_UKETSUKE_SS_DETAIL.MEISAI_BIKOU
 FROM T_UKETSUKE_SS_DETAIL
       INNER JOIN T_UKETSUKE_SS_ENTRY
         ON T_UKETSUKE_SS_ENTRY.SYSTEM_ID = T_UKETSUKE_SS_DETAIL.SYSTEM_ID
        AND T_UKETSUKE_SS_ENTRY.SEQ = T_UKETSUKE_SS_DETAIL.SEQ
        AND T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
        AND T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0
 UNION ALL
 SELECT 
 T_UKETSUKE_SK_DETAIL.HINMEI_CD
,T_UKETSUKE_SK_DETAIL.HINMEI_NAME
,T_UKETSUKE_SK_DETAIL.DENPYOU_KBN_CD
,T_UKETSUKE_SK_DETAIL.SUURYOU
,T_UKETSUKE_SK_DETAIL.UNIT_CD
,T_UKETSUKE_SK_DETAIL.MEISAI_BIKOU
 FROM T_UKETSUKE_SK_DETAIL
       INNER JOIN T_UKETSUKE_SK_ENTRY
         ON T_UKETSUKE_SK_ENTRY.SYSTEM_ID = T_UKETSUKE_SK_DETAIL.SYSTEM_ID
        AND T_UKETSUKE_SK_ENTRY.SEQ = T_UKETSUKE_SK_DETAIL.SEQ
        AND T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
        AND T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0
 UNION ALL
 SELECT 
 T_UKETSUKE_MK_DETAIL.HINMEI_CD
,T_UKETSUKE_MK_DETAIL.HINMEI_NAME
,T_UKETSUKE_MK_DETAIL.DENPYOU_KBN_CD
,T_UKETSUKE_MK_DETAIL.SUURYOU
,T_UKETSUKE_MK_DETAIL.UNIT_CD
,T_UKETSUKE_MK_DETAIL.MEISAI_BIKOU
 FROM T_UKETSUKE_MK_DETAIL
       INNER JOIN T_UKETSUKE_MK_ENTRY
         ON T_UKETSUKE_MK_ENTRY.SYSTEM_ID = T_UKETSUKE_MK_DETAIL.SYSTEM_ID
        AND T_UKETSUKE_MK_ENTRY.SEQ = T_UKETSUKE_MK_DETAIL.SEQ
        AND T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
        AND T_UKETSUKE_MK_ENTRY.DELETE_FLG = 0