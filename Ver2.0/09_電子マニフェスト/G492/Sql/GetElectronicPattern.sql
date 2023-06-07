﻿SELECT A.PATTERN_NAME	
      ,A.PATTERN_FURIGANA
      ,A.TIME_STAMP
FROM DT_PT_R18 A
WHERE A.SYSTEM_ID = /*data.SYSTEM_ID*/ 
AND A.SEQ = /*data.SEQ*/ 
AND A.DELETE_FLG = 0