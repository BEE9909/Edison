﻿SELECT
BUMON_CD,
BUMON_NAME_RYAKU
FROM
M_BUMON
WHERE
DELETE_FLG = 0
/*IF data.BUMON_CD != null*/ AND BUMON_CD = /*data.BUMON_CD*//*END*/
AND ((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= TEKIYOU_END) or (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and TEKIYOU_END IS NULL) or (TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= TEKIYOU_END) or (TEKIYOU_BEGIN IS NULL and TEKIYOU_END IS NULL))
