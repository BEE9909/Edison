﻿SELECT 
TMKK.SYSTEM_ID
,TMKK.SEQ
,TMKK.REC_NO
,TMKK.KEIJOU_CD
,TMKK.KEIJOU_NAME
,TMKK.PRT_FLG
 FROM T_MANIFEST_PT_KP_KEIJYOU TMKK
WHERE TMKK.SYSTEM_ID = /*data.SYSTEM_ID*/ AND TMKK.SEQ =  /*data.SEQ*/