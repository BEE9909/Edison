﻿SELECT
MGS.GYOUSHA_CD
,MGS.HAISHUTSU_NIZUMI_GYOUSHA_KBN
,MGS.UNPAN_JUTAKUSHA_KAISHA_KBN
,MGS.SHOBUN_NIOROSHI_GYOUSHA_KBN
,MGB.TSUMIKAEHOKAN_KBN
FROM M_GYOUSHA MGS
LEFT JOIN M_GENBA MGB ON (MGB.GYOUSHA_CD = MGS.GYOUSHA_CD)
WHERE MGS.DELETE_FLG = 0
AND MGS.GYOUSHA_CD = /*data.GYOUSHA_CD*/