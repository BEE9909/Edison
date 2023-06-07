﻿SELECT DISTINCT 
	T1.ROW_NO AS ROW_NO,
	T1.STACK_JYUURYOU AS STACK_JYUURYOU,
	T1.EMPTY_JYUURYOU AS EMPTY_JYUURYOU,
	T1.WARIFURI_JYUURYOU AS WARIFURI_JYUURYOU,
	T1.CHOUSEI_JYUURYOU AS CHOUSEI_JYUURYOU,
	T2.YOUKI_NAME_RYAKU AS YOUKI_NAME_RYAKU,
	T1.HINMEI_CD AS HINMEI_CD,
	T1.HINMEI_NAME AS HINMEI_NAME,
	T1.NET_JYUURYOU AS NET_JYUURYOU,
	T3.MANIFEST_ID AS MANIFEST_ID,
	T1.MEISAI_BIKOU AS MEISAI_BIKOU,
	T1.YOUKI_SUURYOU AS YOUKI_SUURYOU,
	T4.DENPYOU_KBN_NAME_RYAKU AS DENPYOU_KBN_NAME_RYAKU,
	T1.SUURYOU AS SUURYOU,
	T5.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU,
	T1.TANKA AS TANKA,
	(ISNULL(T1.KINGAKU, 0) + ISNULL(T1.HINMEI_KINGAKU, 0)) AS KINGAKU,
	T1.NISUGATA_SUURYOU AS NISUGATA_SUURYOU,
	T1.NISUGATA_UNIT_CD AS NISUGATA_UNIT_CD,
	T6.UNIT_NAME_RYAKU AS NISUGATA_NAME_RYAKU,
	CASE
	WHEN T1.KEIRYOU_TIME = null THEN ''
	ELSE SUBSTRING(CONVERT(varchar(8), T1.KEIRYOU_TIME),1,5)
	END AS KEIRYOU_TIME,
	CASE 
	WHEN (SELECT COUNT(*) FROM T_ZAIKO_HINMEI_HURIWAKE tzhh WHERE T1.SYSTEM_ID = tzhh.SYSTEM_ID AND T1.DETAIL_SYSTEM_ID = tzhh.DETAIL_SYSTEM_ID AND T1.SEQ = tzhh.SEQ AND tzhh.DENSHU_KBN_CD = '1') > 1 THEN '複数在庫品目' 
	ELSE T7.ZAIKO_HINMEI_NAME 
	END AS ZAIKO_HINMEI_NAME,
	T1.DETAIL_SYSTEM_ID AS UKEIRE_DETAIL_SYSTEM_ID

FROM dbo.T_UKEIRE_DETAIL AS T1 
LEFT JOIN dbo.M_YOUKI AS T2 ON T1.YOUKI_CD = T2.YOUKI_CD 
LEFT JOIN dbo.T_MANIFEST_ENTRY AS T3 ON T1.SYSTEM_ID = T3.RENKEI_SYSTEM_ID AND T1.DETAIL_SYSTEM_ID=T3.RENKEI_MEISAI_SYSTEM_ID AND T3.RENKEI_DENSHU_KBN_CD='1' 
LEFT JOIN dbo.M_DENPYOU_KBN AS T4 ON T1.DENPYOU_KBN_CD = T4.DENPYOU_KBN_CD 
LEFT JOIN dbo.M_UNIT AS T5 ON T1.UNIT_CD = T5.UNIT_CD 
LEFT JOIN dbo.M_UNIT AS T6 ON T1.NISUGATA_UNIT_CD = T6.UNIT_CD
LEFT JOIN
 (SELECT TZ.SYSTEM_ID,TZ.DETAIL_SYSTEM_ID,TZ.SEQ,TZ.ZAIKO_HINMEI_NAME FROM T_ZAIKO_HINMEI_HURIWAKE TZ 
   INNER JOIN (SELECT SYSTEM_ID,DETAIL_SYSTEM_ID,SEQ,DENSHU_KBN_CD FROM T_ZAIKO_HINMEI_HURIWAKE
     GROUP BY SYSTEM_ID,DETAIL_SYSTEM_ID,SEQ,DENSHU_KBN_CD) AS TT 
	 ON TZ.SYSTEM_ID = TT.SYSTEM_ID AND TZ.DETAIL_SYSTEM_ID = TT.DETAIL_SYSTEM_ID AND TZ.SEQ = TT.SEQ AND TT.DENSHU_KBN_CD = '1') AS T7
	   ON T1.SYSTEM_ID = T7.SYSTEM_ID AND T1.DETAIL_SYSTEM_ID = T7.DETAIL_SYSTEM_ID AND T1.SEQ = T7.SEQ 

WHERE 
 T1.SYSTEM_ID = /*systemId*/0 /*END*/
AND T1.SEQ = /*seq*/0 /*END*/
ORDER BY T1.ROW_NO

