﻿SELECT * FROM dbo.T_MANIFEST_ENTRY
WHERE DELETE_FLG = 0 
/*IF !data.RENKEI_DENSHU_KBN_CD.IsNull*/AND RENKEI_DENSHU_KBN_CD = /*data.RENKEI_DENSHU_KBN_CD.Value*//*END*/
/*IF !data.RENKEI_SYSTEM_ID.IsNull*/AND RENKEI_SYSTEM_ID = /*data.RENKEI_SYSTEM_ID.Value*//*END*/
/*IF !data.RENKEI_MEISAI_SYSTEM_ID.IsNull*/AND RENKEI_MEISAI_SYSTEM_ID = /*data.RENKEI_MEISAI_SYSTEM_ID.Value*//*END*/