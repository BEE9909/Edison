SELECT 
    B4.*
	,SH.SHOBUN_HOUHOU_NAME_RYAKU
	,CONVERT(nvarchar, '') BUNRUI_NANE
	,CONVERT(nvarchar, '') END_KUBUN_NAME
	,MT1.TODOUFUKEN_NAME_RYAKU AS LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME
	,MT2.TODOUFUKEN_NAME_RYAKU AS LAST_SHOBUN_GENBA_TODOUFUKEN_NAME
FROM 
    dbo.M_ITAKU_KEIYAKU_BETSU4 B4
	LEFT JOIN dbo.M_SHOBUN_HOUHOU SH ON SH.SHOBUN_HOUHOU_CD = B4.SHOBUN_HOUHOU_CD
	LEFT JOIN M_GYOUSHA MGYOUSHA ON MGYOUSHA.GYOUSHA_CD = B4.LAST_SHOBUN_GYOUSHA_CD
	LEFT JOIN M_TODOUFUKEN MT1 ON MT1.TODOUFUKEN_CD = MGYOUSHA.GYOUSHA_TODOUFUKEN_CD
	LEFT JOIN M_GENBA MGENBA ON MGENBA.GENBA_CD = B4.LAST_SHOBUN_JIGYOUJOU_CD AND MGENBA.GYOUSHA_CD = B4.LAST_SHOBUN_GYOUSHA_CD
	LEFT JOIN M_TODOUFUKEN MT2 ON MT2.TODOUFUKEN_CD = MGENBA.GENBA_TODOUFUKEN_CD
WHERE 1 != 1