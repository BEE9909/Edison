﻿SELECT TB1.* FROM T_UKETSUKE_SK_ENTRY AS TB1
INNER JOIN (
	SELECT MAX(SYSTEM_ID) AS SYSTEM_ID, MAX(SEQ) AS SEQ FROM T_UKETSUKE_SK_ENTRY
    WHERE (DELETE_FLG = 0)
	/*IF data.UKETSUKE_NUMBER != null*/ AND (UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/)/*END*/
) AS TB2 ON (TB1.SYSTEM_ID = TB2.SYSTEM_ID) AND (TB1.SEQ = TB2.SEQ)
WHERE (TB1.DELETE_FLG = 0)
