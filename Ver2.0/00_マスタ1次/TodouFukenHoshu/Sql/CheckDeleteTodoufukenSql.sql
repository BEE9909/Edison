SELECT DISTINCT N'地域マスタ' AS NAME FROM M_CHIIKI WHERE TODOUFUKEN_CD IN /*TODOUFUKEN_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'業者マスタ' AS NAME FROM M_GYOUSHA WHERE GYOUSHA_TODOUFUKEN_CD IN /*TODOUFUKEN_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'現場マスタ' AS NAME FROM M_GENBA WHERE GENBA_TODOUFUKEN_CD IN /*TODOUFUKEN_CD*/('') AND DELETE_FLG = 'False'
