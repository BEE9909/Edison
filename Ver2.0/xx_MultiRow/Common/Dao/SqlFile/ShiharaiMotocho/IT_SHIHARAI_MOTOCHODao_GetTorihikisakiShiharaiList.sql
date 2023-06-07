﻿SELECT MT.TORIHIKISAKI_CD,
 MT.TORIHIKISAKI_NAME_RYAKU,
 MTSI.TORIHIKI_KBN_CD,
 MTSI.KAISHI_KAIKAKE_ZANDAKA,
 MTSI.SHOSHIKI_KBN,
 MTSI.TAX_HASUU_CD
FROM dbo.M_TORIHIKISAKI AS MT
 LEFT JOIN dbo.M_TORIHIKISAKI_SHIHARAI AS MTSI
ON MT.TORIHIKISAKI_CD = MTSI.TORIHIKISAKI_CD
WHERE 
MTSI.TORIHIKI_KBN_CD = 2
/*IF startCD != null && startCD != ''*/AND MTSI.TORIHIKISAKI_CD >= /*startCD*//*END*/
/*IF endCD != null && endCD != ''*/AND MTSI.TORIHIKISAKI_CD <= /*endCD*//*END*/
/*IF shimebi != null && shimebi != ''*/ 
 AND (MTSI.SHIMEBI1 = /*shimebi*/0
   OR MTSI.SHIMEBI2 = /*shimebi*/0
   OR MTSI.SHIMEBI3 = /*shimebi*/0) /*END*/
ORDER BY MT.TORIHIKISAKI_CD