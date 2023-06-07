﻿SELECT
    DENPYOU_NUMBER, 
    MEISAI_NO, 
	DETAIL_SYSTEM_ID,
    DENPYOU_DATE, 
    SEISAN_SHIMEBI, 
    GYOUSHA_NAME, 
    GENBA_NAME, 
    HIN_NAME, 
    SUURYOU,
    TANI, 
    TANKA, 
    SUM(ISNULL(KINGAKU, 0) + ISNULL(HINMEI_KINGAKU, 0)) AS KINGAKU
FROM
    (SELECT
        DENPYOU.SYSTEM_ID, 
        DENPYOU.SEQ, 
        DENPYOU.DETAIL_SYSTEM_ID, 
        DENPYOU.DENPYOU_NUMBER, 
        DENPYOU.ROW_NO AS MEISAI_NO, 
        DENPYOU.DENPYOU_DATE, 
        DENPYOU.SEISAN_SHIMEBI, 
        DENPYOU.GYOUSHA_NAME, 
        DENPYOU.GENBA_NAME, 
        DENPYOU.HINMEI_NAME AS HIN_NAME, 
        DENPYOU.SUURYOU, 
        MU.UNIT_NAME_RYAKU AS TANI, 
        DENPYOU.TANKA, 
        DENPYOU.KINGAKU, 
        DENPYOU.HINMEI_KINGAKU
     FROM
        (SELECT
            ENT.SYSTEM_ID, 
            ENT.SEQ, 
            DET.DETAIL_SYSTEM_ID, 
			/*IF data.DENPYO_SHURUI == 2*/ENT.UKEIRE_NUMBER AS DENPYOU_NUMBER, /*END*/
            /*IF data.DENPYO_SHURUI == 3*/ENT.SHUKKA_NUMBER AS DENPYOU_NUMBER, /*END*/
            /*IF data.DENPYO_SHURUI == 4*/ENT.UR_SH_NUMBER AS DENPYOU_NUMBER, /*END*/
            DET.ROW_NO, 
            ENT.DENPYOU_DATE, 
            ENT.SHIHARAI_DATE AS SEISAN_SHIMEBI, 
            ENT.GYOUSHA_NAME, 
            ENT.GENBA_NAME, 
            DET.HINMEI_NAME, 
            DET.SUURYOU, 
            DET.UNIT_CD, 
            DET.TANKA, 
            DET.KINGAKU,
            DET.HINMEI_KINGAKU
         FROM
		    /*IF data.DENPYO_SHURUI == 2*/T_UKEIRE_ENTRY AS ENT RIGHT OUTER JOIN T_UKEIRE_DETAIL AS DET /*END*/
            /*IF data.DENPYO_SHURUI == 3*/T_SHUKKA_ENTRY AS ENT RIGHT OUTER JOIN T_SHUKKA_DETAIL AS DET /*END*/
            /*IF data.DENPYO_SHURUI == 4*/T_UR_SH_ENTRY AS ENT RIGHT OUTER JOIN T_UR_SH_DETAIL AS DET /*END*/
            ON ENT.SYSTEM_ID = DET.SYSTEM_ID AND ENT.SEQ = DET.SEQ
         WHERE
            /*IF !deletechuFlg*/ENT.DELETE_FLG = 0/*END*/
            /*IF data.KYOTEN_CD != 99*/
            AND ENT.KYOTEN_CD = /*data.KYOTEN_CD*//*END*/ 
            /*IF data.SHIHARAI_CD != null && data.SHIHARAI_CD != ""*/
            AND ENT.TORIHIKISAKI_CD = /*data.SHIHARAI_CD*//*END*/
            AND ENT.SHIHARAI_TORIHIKI_KBN_CD = 2 
            AND DET.KAKUTEI_KBN = 1 
            AND DET.DENPYOU_KBN_CD = 2 
			/*IF data.DENPYO_SHURUI == 2 || data.DENPYO_SHURUI == 3*/
			AND DET.TAIRYUU_KBN = 0
			/*END*/
            AND CONVERT(DATETIME, ENT.SHIHARAI_DATE,111) >= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_FROM*/null,111)
            AND CONVERT(DATETIME, ENT.SHIHARAI_DATE,111) <= CONVERT(DATETIME, /*data.SHIHARAISHIMEBI_TO*/null,111)
            AND (NOT EXISTS
                    (SELECT
                        ENT.SYSTEM_ID, 
                        ENT.SEQ
                     FROM
                        T_SEISAN_DETAIL
                     WHERE
                        (ENT.SYSTEM_ID = DENPYOU_SYSTEM_ID) 
                        AND (ENT.SEQ = DENPYOU_SEQ)
                        AND (DET.DETAIL_SYSTEM_ID = DETAIL_SYSTEM_ID)
/*IF data.DENPYO_SHURUI == 2*/AND (DENPYOU_SHURUI_CD = '1')/*END*/
/*IF data.DENPYO_SHURUI == 3*/AND (DENPYOU_SHURUI_CD = '2')/*END*/
/*IF data.DENPYO_SHURUI == 4*/AND (DENPYOU_SHURUI_CD = '3')/*END*/
                        AND (DELETE_FLG = 0)))
        ) AS DENPYOU LEFT OUTER JOIN M_UNIT AS MU 
          ON DENPYOU.UNIT_CD = MU.UNIT_CD) AS SMRY

GROUP BY
    DENPYOU_NUMBER, MEISAI_NO, DETAIL_SYSTEM_ID, DENPYOU_DATE, SEISAN_SHIMEBI, GYOUSHA_NAME, GENBA_NAME, HIN_NAME, SUURYOU, TANI, TANKA
