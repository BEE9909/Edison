﻿SELECT 
	SEIKYUU.TORIHIKI_KBN_CD,
	SEIKYUU.SHIMEBI1,
	SEIKYUU.SHIMEBI1,
	SEIKYUU.SHIMEBI2,
	SEIKYUU.SHIMEBI3,
	SEIKYUU.KAISHUU_DAY,
	TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU
FROM  
	M_TORIHIKISAKI_SEIKYUU SEIKYUU
	LEFT OUTER JOIN M_TORIHIKISAKI TORIHIKISAKI ON TORIHIKISAKI.TORIHIKISAKI_CD = SEIKYUU.TORIHIKISAKI_CD 
/*BEGIN*/
where 
/*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
AND	SEIKYUU.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*//*END*/ 
/*END*/