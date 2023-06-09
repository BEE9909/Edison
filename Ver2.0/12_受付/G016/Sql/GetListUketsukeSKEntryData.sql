﻿SELECT  T_USSE.*
		, M_KYOTEN.KYOTEN_NAME_RYAKU
		, M_MANIFEST_SHURUI.MANIFEST_SHURUI_NAME_RYAKU
		, M_MANIFEST_TEHAI.MANIFEST_TEHAI_NAME_RYAKU
		, M_COURSE_NAME.COURSE_NAME_RYAKU
		, M_TORIHIKISAKI_SEIKYUU.TAX_HASUU_CD			AS SEIKYUU_TAX_HASUU_CD
		, M_TORIHIKISAKI_SHIHARAI.TAX_HASUU_CD			AS SHIHARAI_TAX_HASUU_CD
FROM	T_UKETSUKE_SK_ENTRY AS T_USSE LEFT OUTER JOIN
		M_COURSE_NAME ON T_USSE.COURSE_NAME_CD = M_COURSE_NAME.COURSE_NAME_CD AND M_COURSE_NAME.DELETE_FLG = 0 LEFT OUTER JOIN
		M_MANIFEST_TEHAI ON T_USSE.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI.MANIFEST_TEHAI_CD AND M_MANIFEST_TEHAI.DELETE_FLG = 0 LEFT OUTER JOIN
		M_MANIFEST_SHURUI ON T_USSE.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI.MANIFEST_SHURUI_CD AND M_MANIFEST_SHURUI.DELETE_FLG = 0 LEFT OUTER JOIN
		M_KYOTEN ON T_USSE.KYOTEN_CD = M_KYOTEN.KYOTEN_CD LEFT OUTER JOIN
		M_TORIHIKISAKI_SEIKYUU ON T_USSE.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD LEFT OUTER JOIN
		M_TORIHIKISAKI_SHIHARAI ON T_USSE.TORIHIKISAKI_CD = M_TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD
WHERE	T_USSE.SYSTEM_ID = /*data.SYSTEM_ID*/
AND		T_USSE.SEQ = /*data.SEQ*/