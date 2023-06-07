﻿SELECT ISNULL(MAX(MAX_TABLE.TORIHIKISAKI_CD),0) + 1 AS TORIHIKISAKI_CD
FROM
(
	SELECT TORIHIKISAKI_CD
	FROM
	(
		SELECT ISNULL(MAX(TORIHIKISAKI_CD),0) AS TORIHIKISAKI_CD
		FROM M_TORIHIKISAKI 
		WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0
	) AS TORIHIKISAKI
	UNION ALL
	(
		SELECT ISNULL(MAX(S.SYUKKINSAKI_CD),0)  AS TORIHIKISAKI_CD
		FROM M_SYUKKINSAKI S 
		LEFT JOIN M_TORIHIKISAKI T 
		ON T.TORIHIKISAKI_CD = S.SYUKKINSAKI_CD  
		where ISNUMERIC(S.SYUKKINSAKI_CD) = 1 AND (T.SHOKUCHI_KBN IS NULL OR T.SHOKUCHI_KBN = 0)
	)
	UNION ALL
	(
		SELECT ISNULL(MAX(N.NYUUKINSAKI_CD),0)  AS TORIHIKISAKI_CD
		FROM M_NYUUKINSAKI N 
		LEFT JOIN M_TORIHIKISAKI T 
		ON T.TORIHIKISAKI_CD = N.NYUUKINSAKI_CD 
		where ISNUMERIC(N.NYUUKINSAKI_CD) = 1 AND (T.SHOKUCHI_KBN IS NULL OR T.SHOKUCHI_KBN = 0)
	)
) AS MAX_TABLE