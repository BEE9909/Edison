﻿SELECT
M_SHARYOU.SHASYU_CD,
M_SHASHU.SHASHU_NAME_RYAKU,
M_SHARYOU.SHARYOU_CD,
M_SHARYOU.SHARYOU_NAME_RYAKU,
M_SHARYOU.GYOUSHA_CD
FROM
M_SHARYOU  LEFT JOIN M_SHASHU on  M_SHARYOU.SHASYU_CD = M_SHASHU.SHASHU_CD
WHERE M_SHARYOU.DELETE_FLG = 0