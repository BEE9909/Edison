﻿SELECT * FROM dbo.T_UKEIRE_JISSEKI_ENTRY
WHERE 
DELETE_FLG = 0
/*IF !data.DENPYOU_SHURUI.IsNull*/AND DENPYOU_SHURUI = /*data.DENPYOU_SHURUI.Value*//*END*/
/*IF !data.DENPYOU_SYSTEM_ID.IsNull*/AND DENPYOU_SYSTEM_ID = /*data.DENPYOU_SYSTEM_ID.Value*//*END*/
ORDER BY SEQ DESC