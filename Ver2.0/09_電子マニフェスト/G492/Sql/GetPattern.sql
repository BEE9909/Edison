﻿SELECT A.PATTERN_NAME	
      ,A.PATTERN_FURIGANA
	  ,A.TIME_STAMP
FROM T_MANIFEST_PT_ENTRY A
WHERE A.SYSTEM_ID = /*data.SYSTEM_ID*/ 
AND A.SEQ = /*data.SEQ*/ 
AND A.DELETE_FLG = 0