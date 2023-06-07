﻿SELECT  NEXT_SYSTEM_ID,
        SEQ,
        REC_SEQ,
        NEXT_HAIKI_KBN_CD,
        FIRST_SYSTEM_ID,
        FIRST_HAIKI_KBN_CD,
        DELETE_FLG
FROM T_MANIFEST_RELATION
WHERE NEXT_SYSTEM_ID = /*data.NEXT_SYSTEM_ID*/ 
  AND SEQ = /*data.SEQ*/ 
ORDER BY REC_SEQ
