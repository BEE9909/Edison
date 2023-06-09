﻿SELECT ISNULL(MIN(T.TORIHIKISAKI_CD),1) TORIHIKISAKI_CD
FROM (
		SELECT (TORIHIKISAKI_CD+1) TORIHIKISAKI_CD FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD)=1 
		UNION 
		SELECT (NYUUKINSAKI_CD+1) TORIHIKISAKI_CD FROM M_NYUUKINSAKI WHERE ISNUMERIC(NYUUKINSAKI_CD)=1
		UNION 
		SELECT (SYUKKINSAKI_CD+1) TORIHIKISAKI_CD FROM M_SYUKKINSAKI WHERE ISNUMERIC(SYUKKINSAKI_CD)=1
	) T
WHERE T.TORIHIKISAKI_CD NOT IN (
		SELECT (TORIHIKISAKI_CD+0) TORIHIKISAKI_CD FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD)=1
		UNION
		SELECT (NYUUKINSAKI_CD+0) TORIHIKISAKI_CD FROM M_NYUUKINSAKI WHERE ISNUMERIC(NYUUKINSAKI_CD)=1
		UNION
		SELECT (SYUKKINSAKI_CD+0) TORIHIKISAKI_CD FROM M_SYUKKINSAKI WHERE ISNUMERIC(SYUKKINSAKI_CD)=1
	)