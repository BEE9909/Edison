SELECT
    GYOUSHA.GYOUSHA_CD as NIOROSHI_GYOUSHA_CD
    ,GYOUSHA.GYOUSHA_NAME_RYAKU as NIOROSHI_GYOUSHA_RYAKU
FROM
    dbo.M_GYOUSHA GYOUSHA
/*BEGIN*/WHERE
	/*IF data.GYOUSHA_CD != null*/ GYOUSHA.GYOUSHA_CD =/*data.GYOUSHA_CD*/'000001'/*END*/
	AND
	(
		GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = 1
		OR GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = 1
	)
	AND GYOUSHA.DELETE_FLG = 0
 /*END*/
