SELECT DISTINCT N'品名マスタ' AS NAME FROM M_HINMEI WHERE DELETE_FLG = 'False' AND SP_TSUMIKAE_HAIKI_SHURUI_CD IN /*HAIKI_SHURUI_CD*/('')
UNION
SELECT DISTINCT N'混合廃棄物マスタ' AS NAME FROM M_KONGOU_HAIKIBUTSU WHERE HAIKI_SHURUI_CD IN /*HAIKI_SHURUI_CD*/('') AND DELETE_FLG = 'False'