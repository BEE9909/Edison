﻿SELECT
M_SHARYOU.SHARYOU_CD,
M_SHARYOU.SHARYOU_NAME_RYAKU,
M_SHASHU.SHASHU_CD,
M_SHASHU.SHASHU_NAME_RYAKU,
M_GYOUSHA.GYOUSHA_CD,
M_GYOUSHA.GYOUSHA_NAME_RYAKU,
M_SHARYOU.KUUSHA_JYURYO,
M_SHAIN.SHAIN_CD,
M_SHAIN.SHAIN_NAME_RYAKU
FROM
M_SHARYOU
LEFT JOIN M_GYOUSHA ON M_SHARYOU.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD 
LEFT OUTER JOIN M_SHASHU  ON M_SHARYOU.SHASYU_CD = M_SHASHU.SHASHU_CD 
LEFT OUTER JOIN M_SHAIN ON M_SHARYOU.SHAIN_CD = M_SHAIN.SHAIN_CD AND M_SHAIN.UNTEN_KBN = 1 

