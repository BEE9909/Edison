﻿SELECT	M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD
		, M_ZAIKO_HINMEI.ZAIKO_HINMEI_RYAKU
FROM
		M_ZAIKO_HINMEI	
WHERE
		M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD = /*data.ZAIKO_HINMEI_CD*/
    AND (M_ZAIKO_HINMEI.TEKIYOU_BEGIN <= GETDATE()
      OR M_ZAIKO_HINMEI.TEKIYOU_BEGIN IS NULL)
	AND (M_ZAIKO_HINMEI.TEKIYOU_END >= GETDATE()
      OR M_ZAIKO_HINMEI.TEKIYOU_END IS NULL)
    AND M_ZAIKO_HINMEI.DELETE_FLG = 0