SELECT 
	CK.GYOUSHA_CD,
	GYO.GYOUSHA_NAME_RYAKU,
	CK.GENBA_CD,
	GEN.GENBA_NAME_RYAKU,
	CK.CHIIKI_CD,
	CHI.CHIIKI_NAME_RYAKU,
	2 AS KYOKA_KBN,
	CK.TOKUBETSU_KYOKA_NO KYOKA_NO,
	CONVERT(nvarchar(10), CK.TOKUBETSU_KYOKA_END, 111) KYOKA_END
FROM 
    dbo.M_CHIIKIBETSU_KYOKA CK
	INNER JOIN M_GYOUSHA GYO ON GYO.GYOUSHA_CD = CK.GYOUSHA_CD
	LEFT JOIN M_GENBA GEN ON GEN.GYOUSHA_CD = CK.GYOUSHA_CD AND GEN.GENBA_CD = CK.GENBA_CD
	INNER JOIN M_CHIIKI CHI ON CHI.CHIIKI_CD = CK.CHIIKI_CD
WHERE CK.KYOKA_KBN = 1
/*IF data.GYOUSHA_CD != null*/
AND CK.GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'
/*END*/
/*IF data.CHIIKI_CD != null*/
AND CK.CHIIKI_CD = /*data.CHIIKI_CD*/'000001'
/*END*/
/*IF data.TOKUBETSU_KYOKA_NO != null*/
AND CK.TOKUBETSU_KYOKA_NO = /*data.TOKUBETSU_KYOKA_NO*/''
/*END*/
/*IF !data.TOKUBETSU_KYOKA_BEGIN.IsNull*/
AND CK.TOKUBETSU_KYOKA_END >= CONVERT(DATETIME, /*data.TOKUBETSU_KYOKA_BEGIN*/null, 120)
/*END*/
/*IF !data.TOKUBETSU_KYOKA_END.IsNull*/
AND CK.TOKUBETSU_KYOKA_END <= CONVERT(DATETIME, /*data.TOKUBETSU_KYOKA_END*/null, 120)
/*END*/
AND CK.DELETE_FLG = 0