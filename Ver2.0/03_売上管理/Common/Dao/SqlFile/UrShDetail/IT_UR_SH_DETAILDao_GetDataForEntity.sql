﻿SELECT * FROM dbo.T_UR_SH_DETAIL
WHERE SYSTEM_ID = /*data.SYSTEM_ID.Value*/
/*IF !data.SEQ.IsNull*/AND SEQ = /*data.SEQ.Value*//*END*/
/*IF !data.DETAIL_SYSTEM_ID.IsNull*/AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID.Value*//*END*/
/*IF !data.UR_SH_NUMBER.IsNull*/AND UR_SH_NUMBER = /*data.UR_SH_NUMBER.Value*//*END*/