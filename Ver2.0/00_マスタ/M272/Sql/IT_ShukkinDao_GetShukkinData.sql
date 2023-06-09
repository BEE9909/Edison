﻿SELECT 
	T0.SHORI,
	T0.DENPYOU_DATE,
	T0.TORIHIKISAKI_CD,
	T0.TORIHIKISAKI_NAME_RYAKU,
	T0.SYSTEM_ID,
	T0.SEQ,
	T0.UPDATE_USER,
	T0.UPDATE_DATE,
	T0.CREATE_USER,
	T0.CREATE_DATE,
	T0.SHUKKIN_NUMBER
FROM
(
SELECT 
	case 
	when T1.SEQ = 1 then '新規' 
	when T1.SEQ <> 1 and T1.SEQ <> TX.SEQ then '修正'
	when T1.SEQ <> 1 and T1.SEQ = TX.SEQ and T1.DELETE_FLG = 0 then '修正'
	when T1.SEQ <> 1 and T1.SEQ = TX.SEQ and T1.DELETE_FLG <> 0 then '削除'
	else ''
	end as SHORI,
	CONVERT(varchar,CONVERT(varchar,T1.DENPYOU_DATE,111) + '(' + LEFT(DATENAME(weekday, T1.DENPYOU_DATE),1) + ')') AS DENPYOU_DATE,
	T1.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
	T2.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME_RYAKU,
	T1.SYSTEM_ID AS SYSTEM_ID,
	T1.SEQ AS SEQ,
	T1.UPDATE_USER AS UPDATE_USER,
	T1.UPDATE_DATE,
	T1.CREATE_USER AS CREATE_USER,
	T1.CREATE_DATE,
	T1.SHUKKIN_NUMBER AS SHUKKIN_NUMBER

FROM dbo.T_SHUKKIN_ENTRY AS T1 

	LEFT JOIN dbo.M_TORIHIKISAKI AS T2 ON T1.TORIHIKISAKI_CD = T2.TORIHIKISAKI_CD AND T2.DELETE_FLG = 0
	JOIN (SELECT MAX(SEQ) AS SEQ,SYSTEM_ID FROM T_SHUKKIN_ENTRY   group by SYSTEM_ID) AS TX ON T1.SYSTEM_ID = TX.SYSTEM_ID
 
WHERE 
T1.DELETE_FLG = T1.DELETE_FLG
/*IF !kyotenCD.IsNull && ''!=kyotenCD*/AND T1.KYOTEN_CD = /*kyotenCD*/0 /*END*/
/*IF !updateFrom.IsNull && ''!=updateFrom*/AND CONVERT(date, T1.UPDATE_DATE) >= CONVERT(date, /*updateFrom*/0) /*END*/
/*IF !updateTo.IsNull && ''!=updateTo*/AND CONVERT(date, T1.UPDATE_DATE) <= CONVERT(date, /*updateTo*/0) /*END*/
) AS T0
WHERE 
T0.SEQ = T0.SEQ
/*IF !appendWhere.IsNull && ''!=appendWhere*//*$appendWhere*//*END*/

ORDER BY SYSTEM_ID desc, SEQ asc
