﻿SELECT
    M_SHASHU.SHASHU_CD,
    M_SHASHU.SHASHU_NAME_RYAKU
FROM
    M_SHASHU INNER JOIN M_SHARYOU ON M_SHARYOU.SHASYU_CD = M_SHASHU.SHASHU_CD
	AND M_SHARYOU.GYOUSHA_CD = /*data.gyoushaCd*/
	AND	M_SHARYOU.SHARYOU_CD = /*data.sharyouCd*/
WHERE
    M_SHASHU.DELETE_FLG = 0