SELECT 
    JIG.*
FROM 
    dbo.M_DENSHI_JIGYOUSHA JIG
WHERE
/*BEGIN*/
/*IF 1 == 1*/JIG.EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*/'0000001'/*END*/
/*IF data.HST_KBN.IsTrue*/AND JIG.HST_KBN = 1/*END*/
/*IF data.UPN_KBN.IsTrue*/AND JIG.UPN_KBN = 1/*END*/
/*IF data.SBN_KBN.IsTrue*/AND JIG.SBN_KBN = 1/*END*/
/*END*/
