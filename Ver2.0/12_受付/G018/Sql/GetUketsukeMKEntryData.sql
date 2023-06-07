﻿SELECT  T_UMKE.*
		, M_KYOTEN.KYOTEN_NAME_RYAKU
		, M_TORIHIKISAKI_SEIKYUU.TAX_HASUU_CD AS SEIKYUU_TAX_HASUU_CD
		, M_TORIHIKISAKI_SHIHARAI.TAX_HASUU_CD AS SHIHARAI_TAX_HASUU_CD
		, M_MANIFEST_SHURUI.MANIFEST_SHURUI_NAME_RYAKU
		, M_MANIFEST_TEHAI.MANIFEST_TEHAI_NAME_RYAKU
FROM	T_UKETSUKE_MK_ENTRY AS T_UMKE 
 LEFT OUTER JOIN M_KYOTEN 
              ON T_UMKE.KYOTEN_CD = M_KYOTEN.KYOTEN_CD 
 LEFT OUTER JOIN M_TORIHIKISAKI_SEIKYUU 
              ON T_UMKE.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD 
 LEFT OUTER JOIN M_TORIHIKISAKI_SHIHARAI 
              ON T_UMKE.TORIHIKISAKI_CD = M_TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD
 LEFT OUTER JOIN M_MANIFEST_TEHAI 
              ON T_UMKE.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI.MANIFEST_TEHAI_CD
			 AND M_MANIFEST_TEHAI.DELETE_FLG = 0
 LEFT OUTER JOIN M_MANIFEST_SHURUI 
              ON T_UMKE.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI.MANIFEST_SHURUI_CD
			 AND M_MANIFEST_SHURUI.DELETE_FLG = 0
WHERE	T_UMKE.UKETSUKE_NUMBER = /*data.UketsukeNumber*/
/*IF data.SEQ == 0*/
		AND T_UMKE.DELETE_FLG = 0
-- ELSE AND T_UMKE.SEQ = /*data.SEQ*/
/*END*/
