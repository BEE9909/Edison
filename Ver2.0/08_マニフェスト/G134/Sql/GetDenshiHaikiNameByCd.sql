﻿SELECT EDI_MEMBER_ID,
       HAIKI_NAME_CD,
       HAIKI_NAME
  FROM M_DENSHI_HAIKI_NAME (NOLOCK)
 WHERE HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/'0'