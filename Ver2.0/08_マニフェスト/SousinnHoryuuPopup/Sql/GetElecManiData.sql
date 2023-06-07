﻿SELECT
    R18.MANIFEST_ID,
    R18.HST_SHA_NAME,
    R18.HST_JOU_NAME,
    R18.HAIKI_NAME

FROM
    DT_MF_TOC AS MF_TOC
    INNER JOIN DT_R18 AS R18
    ON MF_TOC.KANRI_ID = R18.KANRI_ID
    AND MF_TOC.LATEST_SEQ = R18.SEQ

WHERE
    MF_TOC.KANRI_ID IN /*data*/('aaa','bbb')