SELECT DISTINCT N'地域別業者マスタ' AS NAME FROM M_CHIIKIBETSU_GYOUSHU WHERE CHIIKI_CD IN /*CHIIKI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'地域別分類マスタ' AS NAME FROM M_CHIIKIBETSU_BUNRUI WHERE CHIIKI_CD IN /*CHIIKI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'地域別住所マスタ' AS NAME FROM M_CHIIKIBETSU_JUUSHO WHERE CHIIKI_CD IN /*CHIIKI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'地域別許可マスタ' AS NAME FROM M_CHIIKIBETSU_KYOKA WHERE CHIIKI_CD IN /*CHIIKI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'地域別施設マスタ' AS NAME FROM M_CHIIKIBETSU_SHISETSU WHERE CHIIKI_CD IN /*CHIIKI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'地域別処分マスタ' AS NAME FROM M_CHIIKIBETSU_SHOBUN WHERE CHIIKI_CD IN /*CHIIKI_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'業者マスタ' AS NAME FROM M_GYOUSHA WHERE (CHIIKI_CD IN /*CHIIKI_CD*/('') OR UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD IN /*CHIIKI_CD*/('')) AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'現場マスタ' AS NAME FROM M_GENBA WHERE (CHIIKI_CD IN /*CHIIKI_CD*/('') OR UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD IN /*CHIIKI_CD*/('')) AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'引合業者マスタ' AS NAME FROM M_HIKIAI_GYOUSHA WHERE (CHIIKI_CD IN /*CHIIKI_CD*/('') OR UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD IN /*CHIIKI_CD*/('')) AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'引合現場マスタ' AS NAME FROM M_HIKIAI_GENBA WHERE (CHIIKI_CD IN /*CHIIKI_CD*/('') OR UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD IN /*CHIIKI_CD*/('')) AND DELETE_FLG = 'False'