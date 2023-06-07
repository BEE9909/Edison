SELECT 
TSD.HINMEI_CD,
TSD.HINMEI_NAME
FROM
T_SHUKKA_ENTRY AS TSE
LEFT JOIN T_SHUKKA_DETAIL TSD
ON TSE.SYSTEM_ID = TSD.SYSTEM_ID
AND TSE.SEQ = TSD.SEQ
WHERE 
TSE.DELETE_FLG = 0 
AND TSE.TAIRYUU_KBN = 0 
AND DENPYOU_DATE BETWEEN /*dateFrom*/0 AND /*dateTo*/0
/*IF data.TORIHIKISAKI_CD != null*/AND TSE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/0/*END*/
/*IF data.GYOUSHA_CD != null*/AND TSE.GYOUSHA_CD = /*data.GYOUSHA_CD*/0/*END*/
/*IF data.GENBA_CD != null*/AND TSE.GENBA_CD = /*data.GENBA_CD*/0/*END*/
/*IF !data.KYOTEN_CD.IsNull*/
AND KYOTEN_CD = /*data.KYOTEN_CD*/
/*END*/
GROUP BY
TSD.HINMEI_CD,
TSD.HINMEI_NAME
ORDER BY TSD.HINMEI_CD