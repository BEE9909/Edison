﻿SELECT 
TNK.SYSTEM_ID,
ISNULL(TNK.KESHIKOMI_SEQ,0) AS KESHIKOMI_SEQ,
ISNULL(TNK.SHUKKIN_NUMBER,0) AS SHUKKIN_NUM,
NULL AS GYOUSHA_CD,
NULL AS GYOUSHA_NAME,
NULL AS GENBA_CD,
NULL AS GENBA_NAME,
'開始残高' AS SEISAN_DATE,
'1753-01-01 00:00:00.000' AS SORT_SEISAN_DATE,
ISNULL(KAISHI_KAIKAKE_ZANDAKA,0) AS SEISAN_KINGAKU,
TNK.KESHIKOMI_GAKU AS KESHIKOMI_KINGAKU,
TNK.KESHIKOMI_GAKU AS KESHIKOMIGAKU_TOTAL,
ISNULL(KAISHI_KAIKAKE_ZANDAKA,0) - ISNULL(TNK.KESHIKOMI_GAKU,0) AS MIKESHIKOMI_KINGAKU,
0 AS SEISAN_NUMBER,
0 AS KAGAMI_NUMBER,
TNK.KESHIKOMI_BIKOU AS KESHIKOMI_BIKOU
FROM M_TORIHIKISAKI_SHIHARAI MTS
LEFT JOIN T_SHUKKIN_KESHIKOMI TNK
ON MTS.TORIHIKISAKI_CD = TNK.TORIHIKISAKI_CD
AND TNK.DELETE_FLG = 0
AND TNK.SEISAN_NUMBER = 0
WHERE MTS.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/

UNION

SELECT 
TNK.SYSTEM_ID,
TNK.KESHIKOMI_SEQ, 
TNK.SHUKKIN_NUMBER AS SHUKKIN_NUM,
TSDK.GYOUSHA_CD AS GYOUSHA_CD,
MGY.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME,
TSDK.GENBA_CD AS GENBA_CD,
MGE.GENBA_NAME_RYAKU AS GENBA_NAME,
convert(nvarchar(10),TSD.SEISAN_DATE,111) AS SEISAN_DATE,
TSD.SEISAN_DATE AS SORT_SEISAN_DATE,
ISNULL(TSDK.KONKAI_SHIHARAI_GAKU,0)+
ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS SEISAN_KINGAKU,
TNK.KESHIKOMI_GAKU AS KESHIKOMI_KINGAKU,
TNK2.KESHIKOMI_GAKU_TOTAL AS KESHIKOMIGAKU_TOTAL,
ISNULL(TSDK.KONKAI_SHIHARAI_GAKU,0)+
ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0)+
ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0)-
ISNULL(TNK2.KESHIKOMI_GAKU_TOTAL,0) AS MIKESHIKOMI_KINGAKU,
TSDK.SEISAN_NUMBER AS SEISAN_NUMBER,
TSDK.KAGAMI_NUMBER AS KAGAMI_NUMBER,
TNK.KESHIKOMI_BIKOU AS KESHIKOMI_BIKOU
FROM T_SEISAN_DENPYOU TSD
INNER JOIN T_SEISAN_DENPYOU_KAGAMI TSDK
ON TSD.SEISAN_NUMBER = TSDK.SEISAN_NUMBER
AND TSDK.DELETE_FLG = 0
LEFT JOIN T_SHUKKIN_KESHIKOMI TNK
ON TSDK.SEISAN_NUMBER = TNK.SEISAN_NUMBER
AND TSDK.KAGAMI_NUMBER = TNK.KAGAMI_NUMBER
AND TNK.DELETE_FLG = 0
LEFT JOIN M_GYOUSHA MGY
ON MGY.GYOUSHA_CD = TSDK.GYOUSHA_CD
LEFT JOIN M_GENBA MGE
ON MGE.GYOUSHA_CD = TSDK.GYOUSHA_CD
AND MGE.GENBA_CD = TSDK.GENBA_CD
LEFT JOIN (SELECT
              T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER,
			  T_SHUKKIN_KESHIKOMI.KAGAMI_NUMBER,
			  SUM(T_SHUKKIN_KESHIKOMI.KESHIKOMI_GAKU) AS KESHIKOMI_GAKU_TOTAL
			FROM T_SHUKKIN_KESHIKOMI T_SHUKKIN_KESHIKOMI
			WHERE T_SHUKKIN_KESHIKOMI.DELETE_FLG = 0
			GROUP BY T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER,
			         T_SHUKKIN_KESHIKOMI.KAGAMI_NUMBER
		   ) TNK2
	   ON TNK2.SEISAN_NUMBER = TNK.SEISAN_NUMBER
	  AND TNK2.KAGAMI_NUMBER = TNK.KAGAMI_NUMBER
WHERE TSD.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
AND TSD.SEISAN_DATE <=  /*data.DENPYOU_DATE*/
AND TSD.DELETE_FLG = 0
ORDER BY SORT_SEISAN_DATE,GYOUSHA_CD,GENBA_CD