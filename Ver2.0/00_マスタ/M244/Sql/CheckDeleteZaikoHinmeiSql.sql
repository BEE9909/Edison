﻿SELECT DISTINCT N'開始在庫情報マスタ' AS NAME FROM M_KAISHI_ZAIKO_INFO WHERE ZAIKO_HINMEI_CD IN /*ZAIKO_HINMEI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'在庫比率マスタ' AS NAME FROM M_ZAIKO_HIRITSU WHERE ZAIKO_HINMEI_CD IN /*ZAIKO_HINMEI_CD*/('')  AND DELETE_FLG = 'False'