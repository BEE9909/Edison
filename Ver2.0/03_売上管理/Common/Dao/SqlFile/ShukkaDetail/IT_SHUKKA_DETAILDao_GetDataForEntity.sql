﻿SELECT * FROM dbo.T_SHUKKA_DETAIL
WHERE
/*IF !data.SYSTEM_ID.IsNull*/ SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
/*IF !data.SEQ.IsNull*/AND SEQ = /*data.SEQ.Value*//*END*/
/*IF !data.DETAIL_SYSTEM_ID.IsNull*/AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID.Value*//*END*/
/*IF !data.SHUKKA_NUMBER.IsNull*/AND SHUKKA_NUMBER = /*data.SHUKKA_NUMBER.Value*//*END*/
