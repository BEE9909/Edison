SELECT 
    TAN.*
FROM 
    dbo.M_DENSHI_TANTOUSHA TAN
WHERE
	TAN.EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*/'0000001'
AND TAN.TANTOUSHA_KBN = /*data.TANTOUSHA_KBN*/'0'
ORDER BY TAN.EDI_MEMBER_ID, TAN.TANTOUSHA_KBN, TAN.TANTOUSHA_CD
