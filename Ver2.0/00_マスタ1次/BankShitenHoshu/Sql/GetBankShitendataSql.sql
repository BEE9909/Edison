SELECT 
    BSN.*
FROM 
    dbo.M_BANK_SHITEN BSN
WHERE
	BSN.BANK_CD = /*data.BANK_CD*/''
ORDER BY BSN.BANK_CD,BANK_SHITEN_CD,KOUZA_SHURUI_CD,KOUZA_NO
