﻿SELECT HAISHA_DENPYOU_NO,GENBA_NO 
FROM T_MOBISYO_RT 
WHERE JISSEKI_REGIST_FLG = 0 
AND HAISHA_KBN = 0
AND DELETE_FLG = 0
AND HAISHA_DENPYOU_NO = /*haishaDenpyouNo*/
AND HAISHA_ROW_NUMBER = /*rowNo*/