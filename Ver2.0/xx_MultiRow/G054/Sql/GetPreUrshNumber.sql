SELECT 
MAX(TUE.UR_SH_NUMBER) AS preUrshNumber 
FROM 
T_UR_SH_ENTRY AS TUE
WHERE 
TUE.DELETE_FLG = 0 
AND ISNULL(TUE.DAINOU_FLG,0) != 1
/*IF !KyotenCD.IsNull && KyotenCD != ''*/AND TUE.KYOTEN_CD = /*KyotenCD*//*END*/
AND TUE.UR_SH_NUMBER < /*UrshNumber*/