﻿SELECT TTHE.KEIRYOU_NUMBER,UJD.DETAIL_SEQ AS ROW_NO FROM T_KEIRYOU_ENTRY TTHE 
left join T_UKEIRE_JISSEKI_ENTRY UJE ON TTHE.SYSTEM_ID = UJE.DENPYOU_SYSTEM_ID AND UJE.DENPYOU_SHURUI = 1 AND UJE.DELETE_FLG = 0
left join T_UKEIRE_JISSEKI_DETAIL UJD ON UJE.DENPYOU_SHURUI = UJD.DENPYOU_SHURUI AND UJE.DENPYOU_SYSTEM_ID = UJD.DENPYOU_SYSTEM_ID AND UJE.SEQ = UJD.SEQ AND UJD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/
WHERE TTHE.DELETE_FLG = 0
AND TTHE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/