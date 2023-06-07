﻿SELECT 
M_HIKIAI_GYOUSHA.TORIHIKISAKI_CD, 
CASE M_HIKIAI_GYOUSHA.HIKIAI_TORIHIKISAKI_USE_FLG 
WHEN '0' THEN M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU 
ELSE M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU 
END AS TORIHIKISAKI_NAME_RYAKU, 
M_HIKIAI_GYOUSHA.GYOUSHA_CD, 
M_HIKIAI_GYOUSHA.GYOUSHA_NAME_RYAKU, 
M_HIKIAI_GYOUSHA.GYOUSHA_FURIGANA,
M_HIKIAI_GYOUSHA.GYOUSHA_POST,  
M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, 
M_HIKIAI_GYOUSHA.GYOUSHA_ADDRESS1, 
M_HIKIAI_GYOUSHA.GYOUSHA_TEL,
'1' AS GYOUSHA_HIKIAI_FLG 

FROM M_HIKIAI_GYOUSHA 
LEFT JOIN M_TORIHIKISAKI ON M_HIKIAI_GYOUSHA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD 
LEFT JOIN M_HIKIAI_TORIHIKISAKI ON M_HIKIAI_GYOUSHA.TORIHIKISAKI_CD = M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD 
LEFT JOIN M_TODOUFUKEN ON M_HIKIAI_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD 
 AND M_TODOUFUKEN.DELETE_FLG = 0