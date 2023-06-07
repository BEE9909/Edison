﻿INSERT INTO M_GENBA_TSUKI_HINMEI 
(
GYOUSHA_CD
,GENBA_CD
,ROW_NO
,HINMEI_CD
,UNIT_CD
,TANKA
,TEIKI_JISSEKI_NO_SEIKYUU_KBN
,DENPYOU_KBN_CD
,CHOUKA_SETTING
,CHOUKA_LIMIT_AMOUNT
,CHOUKA_HINMEI_NAME
)
SELECT
GYOUSHA_CD
,/*newGENBA_CD*/0
,ROW_NO
,HINMEI_CD
,UNIT_CD
,TANKA
,TEIKI_JISSEKI_NO_SEIKYUU_KBN
,DENPYOU_KBN_CD
,CHOUKA_SETTING
,CHOUKA_LIMIT_AMOUNT
,CHOUKA_HINMEI_NAME
FROM M_HIKIAI_GENBA_TSUKI_HINMEI 
WHERE GENBA_CD = /*oldGENBA_CD*/0
AND GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND HIKIAI_GYOUSHA_USE_FLG = 0
