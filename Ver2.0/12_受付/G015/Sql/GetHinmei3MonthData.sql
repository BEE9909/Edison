SELECT
    '�i��CD�F' + TUD.HINMEI_CD AS HINMEI_CD 
    ,TUD.HINMEI_NAME 
FROM T_UKETSUKE_SS_ENTRY TUE 
    LEFT JOIN T_UKETSUKE_SS_DETAIL TUD ON TUD.SYSTEM_ID = TUE.SYSTEM_ID AND TUD.SEQ = TUE.SEQ 
WHERE 
    1 = 1 
/*IF toriCd != null*/AND TUE.TORIHIKISAKI_CD = /*toriCd*//*END*/
/*IF gyoushaCd != null*/AND TUE.GYOUSHA_CD = /*gyoushaCd*//*END*/
/*IF genbaCd != null*/AND TUE.GENBA_CD = /*genbaCd*//*END*/
    AND TUE.SAGYOU_DATE >= CONVERT(DATETIME ,/*dateFrom*/,120)
    AND TUE.SAGYOU_DATE <= CONVERT(DATETIME ,/*dateTo*/,120)
    AND TUE.DELETE_FLG = 0
    AND TUD.HINMEI_CD IS NOT NULL  
GROUP BY 
	TUD.HINMEI_CD 
   ,TUD.HINMEI_NAME 