﻿SELECT SHOUHIZEI_RATE
FROM M_SHOUHIZEI
WHERE DELETE_FLG = 0
AND TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*denpyouHiduke*/'', 111), 120)
AND (TEKIYOU_END IS NULL OR CONVERT(DATETIME, CONVERT(NVARCHAR, /*denpyouHiduke*/'', 111), 120) <= TEKIYOU_END)