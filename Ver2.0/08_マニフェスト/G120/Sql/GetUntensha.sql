﻿SELECT
M_SHAIN.SHAIN_CD,
M_SHAIN.SHAIN_NAME_RYAKU
FROM 
M_SHAIN
INNER JOIN M_UNTENSHA ON M_UNTENSHA.SHAIN_CD = M_SHAIN.SHAIN_CD  
WHERE
M_SHAIN.UNTEN_KBN = CONVERT(bit,'True') 
AND M_UNTENSHA.DELETE_FLG = 0
AND M_SHAIN.SHAIN_CD = /*data.SHAIN_CD*/