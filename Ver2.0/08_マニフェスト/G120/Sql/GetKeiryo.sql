﻿SELECT TTHE.KEIRYOU_NUMBER,TKD.ROW_NO FROM T_KEIRYOU_ENTRY TTHE 
left join T_KEIRYOU_DETAIL TKD ON (TTHE.SYSTEM_ID = TKD.SYSTEM_ID AND TTHE.SEQ = TKD.SEQ
 AND TKD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/)
WHERE TTHE.DELETE_FLG = 0
AND TTHE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/