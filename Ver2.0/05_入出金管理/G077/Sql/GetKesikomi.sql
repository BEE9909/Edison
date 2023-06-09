﻿SELECT
	DENPYOU.SEIKYUU_NUMBER AS SEIKYUU_NUMBER,
	DENPYOU.SEIKYUU_DATE AS SEIKYUU_DATE,
		(
			DENPYOU.KONKAI_URIAGE_GAKU + 
			DENPYOU.KONKAI_SEI_UTIZEI_GAKU +
			DENPYOU.KONKAI_SEI_SOTOZEI_GAKU + 
			DENPYOU.KONKAI_DEN_UTIZEI_GAKU +
			DENPYOU.KONKAI_DEN_SOTOZEI_GAKU +
			DENPYOU.KONKAI_MEI_UTIZEI_GAKU +
			DENPYOU.KONKAI_MEI_SOTOZEI_GAKU
		) AS SEIKYUU_GAKU,
	/*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
	T_KESHIKOMI.KESHIKOMI_GAKU AS KESHIKOMI_GAKU,
	T_KESHIKOMI.KESHIKOMI_SEQ AS KESHIKOMI_SEQ,
	T_KESHIKOMI.SYSTEM_ID AS KESIKOMI_SYSTEM_ID,
	T_KESHIKOMI.NYUUKIN_SEQ AS NYUUKIN_SEQ,
	T_KESHIKOMI.NYUUKIN_NUMBER AS NYUUKIN_NUMBER,
	CAST(T_KESHIKOMI.TIME_STAMP AS int) AS TIME_STAMP,
	/*END*/
	(		
		(
			DENPYOU.KONKAI_URIAGE_GAKU + 
			DENPYOU.KONKAI_SEI_UTIZEI_GAKU +
			DENPYOU.KONKAI_SEI_SOTOZEI_GAKU + 
			DENPYOU.KONKAI_DEN_UTIZEI_GAKU +
			DENPYOU.KONKAI_DEN_SOTOZEI_GAKU +
			DENPYOU.KONKAI_MEI_UTIZEI_GAKU +
			DENPYOU.KONKAI_MEI_SOTOZEI_GAKU
		) - isnull(KESHIKOMI.KESHI_KOMI,0)
	) AS MINYU_GAKU
FROM
	T_SEIKYUU_DENPYOU DENPYOU
	LEFT OUTER JOIN 
	(
		SELECT 
			KESHIKOMI.SEIKYUU_NUMBER AS SEIKYUU_NUMBER,
			sum(KESHIKOMI.KESHIKOMI_GAKU) AS KESHI_KOMI
		FROM
			T_NYUUKIN_KESHIKOMI KESHIKOMI,
			T_SEIKYUU_DENPYOU DENPYOU,
			T_NYUUKIN_ENTRY NYUUKIN
		/*BEGIN*/
		WHERE
		/*IF !deletechuFlg*/
			KESHIKOMI.DELETE_FLG = 0 AND
			KESHIKOMI.SEIKYUU_NUMBER = DENPYOU.SEIKYUU_NUMBER AND
			NYUUKIN.DELETE_FLG = 0 AND
			NYUUKIN.SYSTEM_ID = KESHIKOMI.SYSTEM_ID AND
			NYUUKIN.SEQ = KESHIKOMI.NYUUKIN_SEQ
		/*END*/
		/*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
			AND DENPYOU.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/ 
		/*END*/
		/*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/
			AND DENPYOU.SEIKYUU_DATE <= /*data.Denpyou_Date*/ 
		/*END*/
		/*END*/
		GROUP BY KESHIKOMI.SEIKYUU_NUMBER
	) AS KESHIKOMI ON DENPYOU.SEIKYUU_NUMBER = KESHIKOMI.SEIKYUU_NUMBER 
	/*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
	LEFT OUTER JOIN 
       T_NYUUKIN_KESHIKOMI AS T_KESHIKOMI
       ON (DENPYOU.SEIKYUU_NUMBER = T_KESHIKOMI.SEIKYUU_NUMBER and T_KESHIKOMI.NYUUKIN_NUMBER =  /*data.Nyukin_number*/
                                                 and T_KESHIKOMI.DELETE_FLG = 0)
	/*END*/
/*BEGIN*/
WHERE 
/*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
(
/*END*/
/*IF !deletechuFlg*/
	(	
		(
			DENPYOU.KONKAI_URIAGE_GAKU + 
			DENPYOU.KONKAI_SEI_UTIZEI_GAKU +
			DENPYOU.KONKAI_SEI_SOTOZEI_GAKU + 
			DENPYOU.KONKAI_DEN_UTIZEI_GAKU +
			DENPYOU.KONKAI_DEN_SOTOZEI_GAKU +
			DENPYOU.KONKAI_MEI_UTIZEI_GAKU +
			DENPYOU.KONKAI_MEI_SOTOZEI_GAKU
		) - isnull(KESHIKOMI.KESHI_KOMI,0)
	) <> 0
/*END*/
/*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
	OR  T_KESHIKOMI.KESHIKOMI_GAKU <> 0)
/*END*/

/*IF !deletechuFlg*/
	AND DENPYOU.DELETE_FLG = 0
/*END*/
/*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
	AND DENPYOU.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/ 
/*END*/
/*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/
	AND DENPYOU.SEIKYUU_DATE <= /*data.Denpyou_Date*/ 
/*END*/	 
/*END*/