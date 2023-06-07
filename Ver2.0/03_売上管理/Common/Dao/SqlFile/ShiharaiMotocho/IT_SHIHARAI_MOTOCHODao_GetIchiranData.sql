﻿SELECT
	CONVERT(nvarchar, UE.SHIHARAI_DATE, 111) AS MEISAI_DATE,
	dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU AS TORIHIKI_KBN,
	UE.UKEIRE_NUMBER AS DENPYOU_NUMBER,
	UE.GYOUSHA_CD AS GYOUSHA_CD,
	UE.GENBA_CD AS GENBA_CD,
	dbo.M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME,
	dbo.M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME,
	UD.HINMEI_CD AS HINMEI_CD,
	UE.RECEIPT_NUMBER AS SEIKYUU_NUMBER,
	dbo.M_HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME,
	((CONVERT(varchar, UD.SUURYOU)) + dbo.M_UNIT.UNIT_NAME_RYAKU) AS SUURYOU_UNIT,
	UD.TANKA AS TANKA,
	UD.KINGAKU AS SHIHARAI_KINGAKU,
	UE.SHIHARAI_TAX_SOTO AS SHOUHIZEI,
	1234567 AS SHUKKIN_KINGAKU,
	(UD.KINGAKU + UE.SHIHARAI_TAX_SOTO) AS SASHIHIKI_ZANDAKA,
	UD.MEISAI_BIKOU AS MEISAI_BIKOU
FROM
	dbo.T_UKEIRE_ENTRY AS UE
LEFT JOIN
	dbo.T_UKEIRE_DETAIL AS UD ON ((UE.SYSTEM_ID = UD.SYSTEM_ID) AND (UE.SEQ = UD.SEQ))
LEFT JOIN
	dbo.M_TORIHIKI_KBN ON UE.SHIHARAI_TORIHIKI_KBN_CD = dbo.M_TORIHIKI_KBN.TORIHIKI_KBN_CD
LEFT JOIN
	dbo.M_GYOUSHA ON UE.GYOUSHA_CD = dbo.M_GYOUSHA.GYOUSHA_CD
LEFT JOIN
	dbo.M_GENBA ON ((dbo.M_GENBA.GYOUSHA_CD = UE.GYOUSHA_CD) AND (dbo.M_GENBA.GENBA_CD = UE.GENBA_CD))
LEFT JOIN
	dbo.M_HINMEI ON UD.HINMEI_CD = dbo.M_HINMEI.HINMEI_CD
LEFT JOIN
	dbo.M_UNIT ON UD.UNIT_CD = dbo.M_UNIT.UNIT_CD
/*BEGIN*/WHERE
/*IF showCD != null*/UE.TORIHIKISAKI_CD = /*showCD*//*END*/
/*IF startDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, UE.SHIHARAI_DATE, 111), 120) >= /*startDay*//*END*/
/*IF endDay != null*/AND CONVERT(DATETIME, CONVERT(nvarchar, UE.SHIHARAI_DATE, 111), 120) <= /*endDay*//*END*/
/*END*/
