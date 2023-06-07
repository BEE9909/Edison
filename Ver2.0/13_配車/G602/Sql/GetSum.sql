﻿SELECT          
MONTH(T_TEIKI_JISSEKI_ENTRY.DENPYOU_DATE) AS MONTH, 
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD, 
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD,
T_TEIKI_JISSEKI_DETAIL.GENBA_CD,
SUM(T_TEIKI_JISSEKI_DETAIL.SUURYOU) AS Expr1
FROM 
T_TEIKI_JISSEKI_DETAIL INNER JOIN T_TEIKI_JISSEKI_ENTRY ON 
T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID = T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID
WHERE 
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD >= '000002' AND 
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD <= '000011' AND 
T_TEIKI_JISSEKI_DETAIL.GENBA_CD >= '000001'  AND 
T_TEIKI_JISSEKI_DETAIL.GENBA_CD <= '000002' 
GROUP BY 
MONTH(T_TEIKI_JISSEKI_ENTRY.DENPYOU_DATE), 
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD,
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD,
T_TEIKI_JISSEKI_DETAIL.GENBA_CD