﻿SELECT * FROM dbo.T_UKEIRE_ENTRY
WHERE 
1 = 1
/*IF data.SEQ.Value == 0*/
        AND DELETE_FLG = 0
-- ELSE AND SEQ = /*data.SEQ.Value*/
/*END*/
/*IF !data.SYSTEM_ID.IsNull*/AND SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
/*IF !data.UKEIRE_NUMBER.IsNull*/AND UKEIRE_NUMBER = /*data.UKEIRE_NUMBER.Value*//*END*/
ORDER BY SEQ DESC