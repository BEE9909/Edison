﻿SELECT TTHE.TEIKI_HAISHA_NUMBER,TTHD.ROW_NUMBER FROM T_TEIKI_HAISHA_ENTRY TTHE 
left join T_TEIKI_HAISHA_DETAIL TTHD ON (TTHE.SYSTEM_ID = TTHD.SYSTEM_ID AND TTHE.SEQ = TTHD.SEQ)
WHERE TTHE.DELETE_FLG = 0
AND TTHE.SYSTEM_ID =  /*data.RENKEI_SYSTEM_ID*/ AND TTHD.DETAIL_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID*/
