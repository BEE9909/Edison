SELECT DISTINCT N'引合現場マスタ' AS NAME FROM M_HIKIAI_GENBA_TEIKI_HINMEI WHERE DENPYOU_KBN_CD IN /*DENPYOU_KBN_CD*/('')
UNION
SELECT DISTINCT N'仮現場月極現場' AS NAME FROM M_KARI_GENBA_TSUKI_HINMEI WHERE DENPYOU_KBN_CD IN /*DENPYOU_KBN_CD*/('')
UNION
SELECT DISTINCT N'品名マスタ' AS NAME FROM M_HINMEI WHERE DENPYOU_KBN_CD IN /*DENPYOU_KBN_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'換算マスタ' AS NAME FROM M_KANSAN WHERE DENPYOU_KBN_CD IN /*DENPYOU_KBN_CD*/('') AND DELETE_FLG = 'False'
