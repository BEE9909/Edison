﻿SELECT * FROM T_ZAIKO_TYOUSEI_ENTRY
WHERE TYOUSEI_NUMBER = (
   SELECT 
  CASE 
    WHEN TMP.TYOUSEI_NUMBER IS NOT NULL
	THEN TMP.TYOUSEI_NUMBER
	ELSE TNTE.TYOUSEI_NUMBER
	END AS TYOUSEI_NUMBER
  FROM
   (SELECT MAX(TYOUSEI_NUMBER) AS TYOUSEI_NUMBER
   FROM T_ZAIKO_TYOUSEI_ENTRY 
   WHERE DELETE_FLG = 0 /*IF tyouseiNumber != ''*/ AND TYOUSEI_NUMBER < /*tyouseiNumber*/0/*END*/)TMP
  LEFT JOIN T_ZAIKO_TYOUSEI_ENTRY TNTE ON TNTE.TYOUSEI_NUMBER = (SELECT MAX(TYOUSEI_NUMBER) FROM T_ZAIKO_TYOUSEI_ENTRY WHERE DELETE_FLG = 0))
AND DELETE_FLG = 0