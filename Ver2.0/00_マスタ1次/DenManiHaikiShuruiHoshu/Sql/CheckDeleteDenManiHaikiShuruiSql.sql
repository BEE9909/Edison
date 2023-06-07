﻿SELECT DISTINCT N'電子廃棄物種類細分類マスタ' AS NAME FROM M_DENSHI_HAIKI_SHURUI_SAIBUNRUI WHERE HAIKI_SHURUI_CD IN /*HAIKI_SHURUI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'品名マスタ' AS NAME FROM M_HINMEI WHERE DM_HAIKI_SHURUI_CD IN /*HAIKI_SHURUI_CD*/('')  AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'電子混合種類' AS NAME FROM M_KONGOU_HAIKIBUTSU WHERE LEFT(HAIKI_SHURUI_CD,4) IN /*HAIKI_SHURUI_CD*/('')  AND DELETE_FLG = 'False'