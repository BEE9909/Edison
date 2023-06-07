﻿-- SEQが最新のデータを取得する
SELECT
	GYOUSHA_CD
	,GENBA_CD
	,ZAIKO_HINMEI_CD
	,YEAR
	,MONTH
	,MAX(SEQ) AS SEQ
FROM
	T_MONTHLY_LOCK_ZAIKO
WHERE
	YEAR = /*YEAR*/2015
	AND MONTH = /*MONTH*/1
GROUP BY GYOUSHA_CD,GENBA_CD,ZAIKO_HINMEI_CD,YEAR,MONTH