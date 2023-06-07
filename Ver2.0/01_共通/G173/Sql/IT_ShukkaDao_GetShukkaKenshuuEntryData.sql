﻿SELECT TOP(100)
    '0' AS JYOUKYOU_SEIKYUU,
    '0' AS JYOUKYOU_SEISAN,
    '0' AS JYOUKYOU_SALES_ZAIKO,
	T1.DENPYOU_DATE AS DENPYOU_DATE,
	T1.SHUKKA_NUMBER AS SHUKKA_NUMBER,
	T1.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
	T1.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME,
	T1.GYOUSHA_CD AS GYOUSHA_CD,
	T1.GYOUSHA_NAME AS GYOUSHA_NAME,
	T1.GENBA_CD AS GENBA_CD,
	T1.GENBA_NAME AS GENBA_NAME,
	T1.UNPAN_GYOUSHA_CD AS UNPAN_GYOUSHA_CD,
	T1.UNPAN_GYOUSHA_NAME AS UNPAN_GYOUSHA_NAME,
	T1.SHASHU_NAME AS SHASHU_NAME,
	T1.SHARYOU_NAME AS SHARYOU_NAME,
	T1.UNTENSHA_NAME AS UNTENSHA_NAME,
	T1.NIZUMI_GYOUSHA_CD AS NIZUMI_GYOUSHA_CD,
	T1.NIZUMI_GYOUSHA_NAME AS NIZUMI_GYOUSHA_NAME,
	T1.NIZUMI_GENBA_CD AS NIZUMI_GENBA_CD,
	T1.NIZUMI_GENBA_NAME AS NIZUMI_GENBA_NAME,
	T1.DENPYOU_BIKOU AS DENPYOU_BIKOU,
	T1.SYSTEM_ID AS SYSTEM_ID,
	T1.SEQ AS SEQ
	

FROM dbo.T_SHUKKA_ENTRY AS T1 ,(SELECT MAX(SEQ) AS SEQ,SYSTEM_ID FROM T_SHUKKA_ENTRY   group by SYSTEM_ID) AS TX
INNER JOIN dbo.T_KENSHU_DETAIL T4 ON TX.SYSTEM_ID = T4.SYSTEM_ID AND TX.SEQ = T4.SEQ

WHERE 
1=1
/*IF !torihikisakiCd.IsNull && ''!=torihikisakiCd*/AND T1.TORIHIKISAKI_CD = /*torihikisakiCd*/0 /*END*/
/*IF !gyoushaCd.IsNull && ''!=gyoushaCd*/AND T1.GYOUSHA_CD = /*gyoushaCd*/0 /*END*/
/*IF !genbaCd.IsNull && ''!=genbaCd*/AND T1.GENBA_CD = /*genbaCd*/0 /*END*/
/*IF !kyotenCd.IsNull && ''!=kyotenCd && '99'!=kyotenCd*/AND T1.KYOTEN_CD = /*kyotenCd*//*END*/
/*IF !fromDate.IsNull && ''!=fromDate*/AND CONVERT(varchar(10), T1.DENPYOU_DATE, 120) >= /*fromDate*//*END*/
/*IF !toDate.IsNull && ''!=toDate*/AND CONVERT(varchar(10), T1.DENPYOU_DATE, 120) <= /*toDate*//*END*/
AND T1.SEQ = TX.SEQ  AND T1.SYSTEM_ID = TX.SYSTEM_ID

ORDER BY T1.DENPYOU_DATE desc, T1.SYSTEM_ID asc