﻿SELECT
    DENSHIJIGYOUJOU.EDI_MEMBER_ID,
    DENSHIJIGYOUJOU.JIGYOUJOU_CD,
    DENSHIJIGYOUJOU.JIGYOUJOU_NAME
FROM
    M_DENSHI_JIGYOUJOU AS DENSHIJIGYOUJOU
WHERE
    (DENSHIJIGYOUJOU.GENBA_CD IS NULL
OR  DENSHIJIGYOUJOU.GENBA_CD = '')