﻿SELECT * FROM dbo.T_KEIRYOU_DETAIL
WHERE
/*IF !data.SYSTEM_ID.IsNull*/ SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
/*IF !data.SEQ.IsNull*/AND SEQ = /*data.SEQ.Value*//*END*/
/*IF !data.DETAIL_SYSTEM_ID.IsNull*/AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID.Value*//*END*/
/*IF !data.KEIRYOU_NUMBER.IsNull*/AND KEIRYOU_NUMBER = /*data.KEIRYOU_NUMBER.Value*//*END*/
ORDER BY ROW_NO