SELECT 
MIN(TUE.UKEIRE_NUMBER) AS nextUkeireNumber 
FROM 
T_UKEIRE_ENTRY AS TUE 
WHERE 
TUE.DELETE_FLG = 0 
/*IF !KyotenCD.IsNull && KyotenCD != ''*/AND TUE.KYOTEN_CD = /*KyotenCD*//*END*/
AND TUE.UKEIRE_NUMBER > /*UkeireNumber*/