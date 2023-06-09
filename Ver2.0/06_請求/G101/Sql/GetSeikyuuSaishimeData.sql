﻿SELECT
SEIKYUU_NUMBER
FROM
T_SEIKYUU_DENPYOU
WHERE
DELETE_FLG = 0
AND TORIHIKISAKI_CD = /*data.SEIKYU_CD*/
/*IF data.SEIKYUSHIMEBI_FROM != null && data.SEIKYUSHIMEBI_FROM != ''*/
AND SEIKYUU_DATE >= CONVERT(DateTime,/*data.SEIKYUSHIMEBI_FROM*/null, 111)
AND SEIKYUU_DATE <= CONVERT(DateTime,/*data.SEIKYUSHIMEBI_TO*/null, 111)
--ELSE
AND SEIKYUU_DATE = CONVERT(DateTime, /*data.SEIKYUSHIMEBI_TO*/null, 111)
/*END*/
ORDER BY SEIKYUU_DATE DESC, SEIKYUU_NUMBER DESC