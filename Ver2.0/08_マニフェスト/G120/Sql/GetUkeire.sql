﻿SELECT TUE.UKEIRE_NUMBER,TUD.ROW_NO FROM T_UKEIRE_ENTRY TUE 
left join T_UKEIRE_DETAIL TUD ON (TUE.SYSTEM_ID = TUD.SYSTEM_ID AND TUE.SEQ = TUD.SEQ 
AND TUD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/)
WHERE TUE.DELETE_FLG = 0
AND TUE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/