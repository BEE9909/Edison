﻿SELECT
SHAIN_CD,
SHAIN_NAME_RYAKU
FROM
M_SHAIN
WHERE
DELETE_FLG = 0
/*IF data.SHAIN_CD!= null*/ AND SHAIN_CD = /*data.SHAIN_CD*//*END*/