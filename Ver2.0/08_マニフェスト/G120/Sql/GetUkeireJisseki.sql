﻿SELECT TUE.UKEIRE_NUMBER,UJD.DETAIL_SEQ AS ROW_NO FROM T_UKEIRE_ENTRY TUE 
left join T_UKEIRE_JISSEKI_ENTRY UJE ON TUE.SYSTEM_ID = UJE.DENPYOU_SYSTEM_ID AND UJE.DENPYOU_SHURUI = 2 AND UJE.DELETE_FLG = 0
left join T_UKEIRE_JISSEKI_DETAIL UJD ON UJE.DENPYOU_SHURUI = UJD.DENPYOU_SHURUI AND UJE.DENPYOU_SYSTEM_ID = UJD.DENPYOU_SYSTEM_ID AND UJE.SEQ = UJD.SEQ AND UJD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/
WHERE TUE.DELETE_FLG = 0
AND TUE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/