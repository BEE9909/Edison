﻿SELECT
 TORIHIKISAKI_CD,
 TORIHIKISAKI_NAME_RYAKU 
FROM dbo.M_HIKIAI_TORIHIKISAKI
WHERE 1 = 1
/*IF data.TORIHIKISAKI_CD != null*/AND TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/''/*END*/
