﻿SELECT
SEIKYUU_NUMBER,
KAGAMI_NUMBER,
ROW_NUMBER
FROM
T_SEIKYUU_DETAIL
WHERE
 DELETE_FLG = 0
 AND DENPYOU_SHURUI_CD = /*data.DENPYOU_SHURUI_CD*/
 AND DENPYOU_SYSTEM_ID = /*data.DENPYOU_SYSTEM_ID*/
 AND DENPYOU_SEQ = /*data.DENPYOU_SEQ*/
 /*IF !data.DETAIL_SYSTEM_ID.IsNull*/AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID.Value*//*END*/
 /*IF !data.TORIHIKISAKI_CD == null*/AND TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*//*END*/