SELECT 
    GYOUSHA.GYOUSHA_CD as GYOUSHA_CD
    ,GYOUSHA.GYOUSHA_NAME_RYAKU as GYOUSHA_NAME_RYAKU
FROM 
    dbo.M_GYOUSHA  GYOUSHA
WHERE ( GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = '1' ) 
AND GYOUSHA.DELETE_FLG = 0
 /*BEGIN*/
 /*IF data.GYOUSHA_CD != null*/AND GYOUSHA.GYOUSHA_CD LIKE '%' + /*data.GYOUSHA_CD*/ + '%' /*END*/
 /*END*/