﻿SELECT 
	0 AS ROW_NO,
	*
FROM
-----------------支払 START------------------------
	(SELECT 
		T1.ROW_NO AS UKEIRE_ROW_NO,
		'0' AS UKEIRE_JYOUKYOU,
		T1.SYSTEM_ID AS UKEIRE_SYSTEM_ID,
		T1.SEQ AS UKEIRE_SEQ,
		T1.DETAIL_SYSTEM_ID AS UKEIRE_DETAIL_SYSTEM_ID,
		T1.UR_SH_NUMBER AS UKEIRE_DENPYOU_NUMBER,
		T1.HINMEI_CD AS UKEIRE_HINMEI_CD,
		T1.HINMEI_NAME AS UKEIRE_HINMEI_NAME,
		T2.DENPYOU_KBN_NAME_RYAKU AS SHIHARAI_DENPYOU_KBN,
		T1.STACK_JYUURYOU AS SHIHARAI_SHOUMI_JUURYOU,
		T1.CHOUSEI_JYUURYOU AS SHIHARAI_CHOUSEI,
		T1.NET_JYUURYOU AS SHIHARAI_JITSU_SHOUMI_JUURYOU,
		T1.SUURYOU AS SHIHARAI_SUURYOU,
		T3.UNIT_CD AS SHIHARAI_UNIT_CD,
		T3.UNIT_NAME_RYAKU AS SHIHARAI_UNIT_NAME,
		T1.TANKA AS SHIHARAI_TANKA,
		CASE WHEN T1.KINGAKU <> 0 THEN T1.KINGAKU ELSE T1.HINMEI_KINGAKU END AS SHIHARAI_KINGAKU,
		T1.MEISAI_BIKOU AS SHIHARAI_BIKOU
	FROM dbo.T_UR_SH_DETAIL AS T1 
	LEFT JOIN dbo.M_DENPYOU_KBN AS T2 ON T1.DENPYOU_KBN_CD = T2.DENPYOU_KBN_CD AND T2.DELETE_FLG = 0
	LEFT JOIN dbo.M_UNIT AS T3 ON T1.UNIT_CD = T3.UNIT_CD AND T3.DELETE_FLG = 0
	WHERE 
	 T1.SYSTEM_ID = /*systemIduUkeire*/0 /*END*/
	AND T1.SEQ = /*seqUkeire*/0 /*END*/) AS SHIHARAI
	-----------------支払 END--------------------------
	FULL OUTER JOIN 
	-----------------売上 START------------------------
		(SELECT 
			T1.ROW_NO AS SHUKKA_ROW_NO,
			'0' AS SHUKKA_JYOUKYOU,
			T1.SYSTEM_ID AS SHUKKA_SYSTEM_ID,
			T1.SEQ AS SHUKKA_SEQ,
			T1.DETAIL_SYSTEM_ID AS SHUKKA_DETAIL_SYSTEM_ID,
			T1.UR_SH_NUMBER AS SHUKKA_DENPYOU_NUMBER,
			T1.HINMEI_CD AS SHUKKA_HINMEI_CD,
			T1.HINMEI_NAME AS SHUKKA_HINMEI_NAME,
			T2.DENPYOU_KBN_NAME_RYAKU AS URIAGE_DENPYOU_KBN,
			T1.STACK_JYUURYOU AS URIAGE_SHOUMI_JUURYOU,
			T1.CHOUSEI_JYUURYOU AS URIAGE_CHOUSEI,
			T1.NET_JYUURYOU AS URIAGE_JITSU_SHOUMI_JUURYOU,
			T1.SUURYOU AS URIAGE_SUURYOU,
			T3.UNIT_CD AS URIAGE_UNIT_CD,
			T3.UNIT_NAME_RYAKU AS URIAGE_UNIT_NAME,
			T1.TANKA AS URIAGE_TANKA,
			CASE WHEN T1.KINGAKU <> 0 THEN T1.KINGAKU ELSE T1.HINMEI_KINGAKU END AS URIAGE_KINGAKU,
			T1.MEISAI_BIKOU AS URIAGE_BIKOU
		FROM dbo.T_UR_SH_DETAIL AS T1 
		LEFT JOIN dbo.M_DENPYOU_KBN AS T2 ON T1.DENPYOU_KBN_CD = T2.DENPYOU_KBN_CD AND T2.DELETE_FLG = 0
		LEFT JOIN dbo.M_UNIT AS T3 ON T1.UNIT_CD = T3.UNIT_CD AND T3.DELETE_FLG = 0
		WHERE 
		 T1.SYSTEM_ID = /*systemIdShukka*/0 /*END*/
		AND T1.SEQ = /*seqShukka*/0 /*END*/) AS URIAGE
	-----------------売上 END--------------------------
	ON SHIHARAI.UKEIRE_DENPYOU_NUMBER = URIAGE.SHUKKA_DENPYOU_NUMBER AND SHIHARAI.UKEIRE_ROW_NO = URIAGE.SHUKKA_ROW_NO
ORDER BY SHIHARAI.UKEIRE_ROW_NO

