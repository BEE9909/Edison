﻿SELECT SHUKKIN_NUMBER
FROM T_SHUKKIN_ENTRY
WHERE SHUKKIN_NUMBER = (
SELECT MIN(SHUKKIN_NUMBER) 
FROM T_SHUKKIN_ENTRY 
WHERE DELETE_FLG = 0
 /*IF kyotenCd != ''*/AND (KYOTEN_CD = /*kyotenCd*/ OR /*kyotenCd*/ = 99)/*END*/
 /*IF ShukkinNumber != ''*/AND SHUKKIN_NUMBER > /*ShukkinNumber*/0/*END*/)
 AND DELETE_FLG = 0