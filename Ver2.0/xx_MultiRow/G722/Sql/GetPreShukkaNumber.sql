SELECT 
MAX(TSE.SHUKKA_NUMBER) AS preShukkaNumber 
FROM 
T_SHUKKA_ENTRY AS TSE
WHERE 
TSE.DELETE_FLG = 0 
/*IF !KyotenCD.IsNull && KyotenCD != ''*/AND TSE.KYOTEN_CD = /*KyotenCD*//*END*/
AND TSE.SHUKKA_NUMBER < /*ShukkaNumber*/