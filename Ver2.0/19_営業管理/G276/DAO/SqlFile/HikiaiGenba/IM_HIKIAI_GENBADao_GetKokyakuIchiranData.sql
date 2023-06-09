SELECT 
    BA.GYOUSHA_CD AS GYOUSHA_CD
    ,SHA.GYOUSHA_NAME1 AS GYOUSHA_NAME1
    ,BA.GENBA_CD AS GENBA_CD
    ,BA.GENBA_NAME1 AS GENBA_NAME1
    ,BA.TANTOUSHA AS TANTOUSHA
    ,BA.POST AS POST
    ,BA.GENBA_TEL AS GENBA_TEL
    ,BA.GENBA_FAX AS GENBA_FAX
    ,BA.BIKOU1 AS BIKOU1
    ,BA.BIKOU2 AS BIKOU2
FROM 
    KankyouShougunR.dbo.M_HIKIAI_GENBA BA
	INNER JOIN KankyouShougunR.dbo.M_HIKIAI_GYOUSHA SHA ON BA.GYOUSHA_CD = SHA.GYOUSHA_CD
	INNER JOIN KankyouShougunR.dbo.M_EIGYOU_TANTOUSHA E ON BA.EIGYOU_TANTOU_CD = E.SHAIN_CD
WHERE BA.GENBA_NAME1 LIKE '%' + /*searchString*/ + '%'
ORDER BY BA.GYOUSHA_CD,BA.GENBA_CD