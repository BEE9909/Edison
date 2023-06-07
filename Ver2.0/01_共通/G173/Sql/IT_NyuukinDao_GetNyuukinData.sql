﻿SELECT TOP(100)
    '0' AS JYOUKYOU_SEIKYUU,
	T1.DENPYOU_DATE AS DENPYOU_DATE,
	T1.NYUUKIN_NUMBER AS NYUUKIN_NUMBER_DISP,
	T2.BANK_NAME_RYAKU AS BANK_NAME_RYAKU,
	T3.BANK_SHIETN_NAME_RYAKU AS BANK_SHIETN_NAME_RYAKU,
	T1.KOUZA_SHURUI AS KOUZA_SHURUI,
	T1.KOUZA_NO AS KOUZA_NO,
	T1.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
	T4.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME_RYAKU,
	T1.SYSTEM_ID AS SYSTEM_ID,
	T1.SEQ AS SEQ,
	T1.NYUUKIN_NUMBER AS NYUUKIN_NUMBER

FROM dbo.T_NYUUKIN_ENTRY AS T1 
	LEFT JOIN dbo.M_BANK AS T2 ON T1.BANK_CD = T2.BANK_CD 
	LEFT JOIN dbo.M_BANK_SHITEN AS T3 ON  T1.BANK_CD = T3.BANK_CD AND T1.BANK_SHITEN_CD = T3.BANK_SHITEN_CD AND T1.KOUZA_SHURUI=T3.KOUZA_SHURUI AND T1.KOUZA_NO=T3.KOUZA_NO 
	LEFT JOIN dbo.M_TORIHIKISAKI AS T4 ON T1.TORIHIKISAKI_CD = T4.TORIHIKISAKI_CD 
	JOIN (SELECT MAX(SEQ) AS SEQ,SYSTEM_ID FROM T_NYUUKIN_ENTRY   group by SYSTEM_ID) AS TX ON T1.SEQ = TX.SEQ  AND T1.SYSTEM_ID = TX.SYSTEM_ID

WHERE 
1=1
/*IF !torihikisakiCd.IsNull && ''!=torihikisakiCd*/AND T1.TORIHIKISAKI_CD = /*torihikisakiCd*/0 /*END*/
/*IF !kyotenCd.IsNull && ''!=kyotenCd && '99'!=kyotenCd*/AND T1.KYOTEN_CD = /*kyotenCd*//*END*/
/*IF !fromDate.IsNull && ''!=fromDate*/AND CONVERT(varchar(10), T1.DENPYOU_DATE, 120) >= /*fromDate*//*END*/
/*IF !toDate.IsNull && ''!=toDate*/AND CONVERT(varchar(10), T1.DENPYOU_DATE, 120) <= /*toDate*//*END*/
AND T1.DELETE_FLG = 0

ORDER BY T1.DENPYOU_DATE desc, T1.SYSTEM_ID asc