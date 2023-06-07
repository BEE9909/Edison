﻿SELECT * FROM T_ZAIKO_IDOU_ENTRY
WHERE IDOU_NUMBER = (
   SELECT 
  CASE 
    WHEN TMP.IDOU_NUMBER IS NOT NULL
	THEN TMP.IDOU_NUMBER
	ELSE TNTE.IDOU_NUMBER
	END AS IDOU_NUMBER
  FROM
   (SELECT MAX(IDOU_NUMBER) AS IDOU_NUMBER
   FROM T_ZAIKO_IDOU_ENTRY 
   WHERE DELETE_FLG = 0 /*IF idouNumber != ""*/ AND IDOU_NUMBER < /*idouNumber*/0/*END*/)TMP
  LEFT JOIN T_ZAIKO_IDOU_ENTRY TNTE ON TNTE.IDOU_NUMBER = (SELECT MAX(IDOU_NUMBER) FROM T_ZAIKO_IDOU_ENTRY WHERE DELETE_FLG = 0))
AND DELETE_FLG = 0