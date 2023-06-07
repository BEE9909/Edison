﻿SELECT
KONKAI_SEISAN_GAKU
FROM
T_SEISAN_DENPYOU
WHERE
DELETE_FLG = 0
AND
TORIHIKISAKI_CD = /*data.SHIHARAI_CD*/
AND
CONVERT(DateTime, SEISAN_DATE, 111) <= CONVERT(DateTime, /*data.SHIHARAISHIMEBI_TO*/null, 111)
ORDER BY SEISAN_DATE DESC, SEISAN_NUMBER DESC