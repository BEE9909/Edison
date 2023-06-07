﻿-- SEQが最新のデータを取得する
SELECT
	TORIHIKISAKI_CD
	,YEAR
	,MONTH
	,MAX(SEQ) AS SEQ
FROM
	T_MONTHLY_LOCK_UR
WHERE
	YEAR = /*YEAR*/2015
	AND MONTH = /*MONTH*/1
GROUP BY TORIHIKISAKI_CD,YEAR,MONTH