﻿SELECT MTS.* 
FROM M_TORIHIKISAKI_SEIKYUU AS MTS 
INNER JOIN M_TORIHIKISAKI AS MT 
ON MT.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
WHERE NYUUKINSAKI_CD = /*nyuukinsakiCd*/''
AND   ((MT.TEKIYOU_BEGIN <= CONVERT(DATETIME, /*denpyouDate*/, 120) AND CONVERT(DATETIME, CONVERT(nvarchar, /*denpyouDate*/, 111), 120) <= MT.TEKIYOU_END) 
    OR (MT.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, /*denpyouDate*/, 111), 120) AND MT.TEKIYOU_END IS NULL) 
    OR (MT.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, /*denpyouDate*/, 111), 120) <= MT.TEKIYOU_END) 
    OR (MT.TEKIYOU_BEGIN IS NULL AND MT.TEKIYOU_END IS NULL)) 
AND MT.DELETE_FLG = 0
ORDER BY MTS.TORIHIKISAKI_CD