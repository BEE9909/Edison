﻿SELECT
URSE.TORIHIKISAKI_CD   AS TORIHIKISAKI_CD,
URSE.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME,
URSE.KYOTEN_CD         AS KYOTEN_CD,
URSE.SYSTEM_ID         AS SYSTEM_ID,
URSE.SEQ               AS SEQ,
URSD.DETAIL_SYSTEM_ID  AS DETAIL_SYSTEM_ID,
URSD.ROW_NO            AS ROW_NO,
URSE.UR_SH_NUMBER      AS DENPYOU_NUMBER,
URSE.DENPYOU_DATE      AS DENPYOU_DATE,
URSE.URIAGE_DATE       AS URIAGE_DATE,
URSE.SHIHARAI_DATE     AS SHIHARAI_DATE,
ISNULL(URSD.KINGAKU,0)+ISNULL(URSD.HINMEI_KINGAKU,0) AS GOUKEIGAKU,
ISNULL(URSE.DAINOU_FLG,0) AS DAINOU_FLG
FROM
T_UR_SH_ENTRY URSE RIGHT OUTER JOIN T_UR_SH_DETAIL URSD
ON URSE.SYSTEM_ID = URSD.SYSTEM_ID AND URSE.SEQ = URSD.SEQ
/*BEGIN*/WHERE
/*IF !deletechuFlg*/URSE.DELETE_FLG = 0/*END*/
/*IF data.KYOTEN_CD != 99*/
 AND URSE.KYOTEN_CD = /*data.KYOTEN_CD*//*END*/
/*IF data.URIAGE_SHIHARAI_KBN == 1*/
 AND URSE.URIAGE_TORIHIKI_KBN_CD = 2
-- ELSE AND URSE.SHIHARAI_TORIHIKI_KBN_CD = 2/*END*/
 AND URSE.KAKUTEI_KBN = 1
 AND URSD.DENPYOU_KBN_CD = /*data.URIAGE_SHIHARAI_KBN*/
/*IF data.URIAGE_SHIHARAI_KBN == 1*/
 /*IF data.SEIKYUSHIMEBI_FROM != null && data.SEIKYUSHIMEBI_FROM != ""*/
 AND CONVERT(DATETIME, URSE.URIAGE_DATE,111) >= CONVERT(DATETIME, /*data.SEIKYUSHIMEBI_FROM*/null,111)
 AND CONVERT(DATETIME, URSE.URIAGE_DATE,111) <= CONVERT(DATETIME, /*data.SEIKYUSHIMEBI_TO*/null,111)
 -- ELSE AND CONVERT(DATETIME, URSE.URIAGE_DATE,111) <= CONVERT(DATETIME, /*data.SEIKYUSHIMEBI_TO*/null,111)/*END*/
 AND URSE.TORIHIKISAKI_CD = /*data.SEIKYU_CD*/
-- ELSE
 /*IF data.SHIHARAISHIMEBI_FROM != null && data.SHIHARAISHIMEBI_FROM != ""*/
 AND CONVERT(DATETIME, URSE.SHIHARAI_DATE,111) >= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_FROM*/null,111)
 AND CONVERT(DATETIME, URSE.SHIHARAI_DATE,111) <= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_TO*/null,111)
 -- ELSE AND CONVERT(DATETIME, URSE.SHIHARAI_DATE,111) <= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_TO*/null,111)/*END*/
 AND URSE.TORIHIKISAKI_CD = /*data.SHIHARAI_CD*/
/*END*/
/*IF data.DENPYOU_BANGOU != 0*/ AND URSE.UR_SH_NUMBER = /*data.DENPYOU_BANGOU*//*END*/
AND NOT EXISTS (
	SELECT
		1 
	FROM
		/*IF data.URIAGE_SHIHARAI_KBN == 1*/
		T_SEIKYUU_DENPYOU AS SEIE
		INNER JOIN T_SEIKYUU_DETAIL SEIDE
			ON SEIE.SEIKYUU_NUMBER = SEIDE.SEIKYUU_NUMBER AND SEIDE.DELETE_FLG = 0
		-- ELSE
		T_SEISAN_DENPYOU AS SEIE
		INNER JOIN T_SEISAN_DETAIL SEIDE
			ON SEIE.SEISAN_NUMBER = SEIDE.SEISAN_NUMBER AND SEIDE.DELETE_FLG = 0
		/*END*/
	WHERE
		SEIDE.DENPYOU_SHURUI_CD = 3
		AND SEIDE.DENPYOU_SYSTEM_ID = URSD.SYSTEM_ID 
        AND SEIDE.DENPYOU_SEQ = URSD.SEQ 
        AND SEIDE.DETAIL_SYSTEM_ID = URSD.DETAIL_SYSTEM_ID
		/*IF data.SAISHIME_FLG && data.SAISHIME_NUMBER_LIST.Count > 0*/
			/*IF data.URIAGE_SHIHARAI_KBN == 1*/
				AND SEIDE.SEIKYUU_NUMBER NOT IN /*data.SAISHIME_NUMBER_LIST*/(0)
			 -- ELSE
				 AND SEIDE.SEISAN_NUMBER NOT IN /*data.SAISHIME_NUMBER_LIST*/(0)
			/*END*/
		 /*END*/
)
/*END*/