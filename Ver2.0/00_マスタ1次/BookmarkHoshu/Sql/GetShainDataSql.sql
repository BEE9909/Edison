SELECT 
    SHA.*
FROM 
 M_SHAIN SHA
/*BEGIN*/WHERE 
/*IF data.BUSHO_CD != null*/SHA.BUSHO_CD = /*data.BUSHO_CD*//*END*/
/*IF data.SHAIN_CD != null*/AND SHA.SHAIN_CD = /*data.SHAIN_CD*//*END*/
/*END*/
ORDER BY SHA.BUSHO_CD, SHA.SHAIN_CD
