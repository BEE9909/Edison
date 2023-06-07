﻿SELECT
	CONVERT(varchar,CONVERT(varchar,T1.UKETSUKE_DATE,111) + '(' + LEFT(DATENAME(weekday, T1.UKETSUKE_DATE),1) + ')') AS UKETSUKE_DATE,
	T1.UKETSUKE_NUMBER AS UKETSUKE_NUMBER,
	T1.GYOUSHA_CD AS GYOUSHA_CD,
	T1.GYOUSHA_NAME AS GYOUSHA_NAME,
	T1.GENBA_CD AS GENBA_CD,
	T1.GENBA_NAME AS GENBA_NAME,
	T1.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
	T1.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME,
	CONVERT(varchar,CONVERT(varchar,T1.TAIOU_END__DATE,111) + '(' + LEFT(DATENAME(weekday, T1.TAIOU_END__DATE),1) + ')') AS TAIOU_END__DATE,
	T1.TITLE_NAME AS TITLE_NAME,
	T1.SENPOU_TOIAWASE_USER AS SENPOU_TOIAWASE_USER,
	T1.NAIYOU_1 AS NAIYOU1,
	T1.NAIYOU_2 AS NAIYOU2,
	T1.NAIYOU_3 AS NAIYOU3,
	T1.NAIYOU_4 AS NAIYOU4,
	T1.NAIYOU_5 AS NAIYOU5,
	T1.NAIYOU_6 AS NAIYOU6,
	T1.NAIYOU_7 AS NAIYOU7,
	T1.NAIYOU_8 AS NAIYOU8



FROM dbo.T_UKETSUKE_CM_ENTRY AS T1 ,(SELECT MAX(SEQ) AS SEQ,SYSTEM_ID FROM T_UKETSUKE_CM_ENTRY   group by SYSTEM_ID) AS TX

WHERE 
T1.DELETE_FLG = 0
/*IF !torihikisakiCd.IsNull && ''!=torihikisakiCd*/AND T1.TORIHIKISAKI_CD = /*torihikisakiCd*/0 /*END*/
/*IF !gyoushaCd.IsNull && ''!=gyoushaCd*/AND T1.GYOUSHA_CD = /*gyoushaCd*/0 /*END*/
/*IF !genbaCd.IsNull && ''!=genbaCd*/AND T1.GENBA_CD = /*genbaCd*/0 /*END*/
AND T1.SEQ = T1.SEQ  AND T1.SYSTEM_ID = TX.SYSTEM_ID

ORDER BY T1.SYSTEM_ID desc, T1.SEQ asc