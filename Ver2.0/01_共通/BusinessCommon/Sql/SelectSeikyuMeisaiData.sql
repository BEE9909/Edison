﻿SELECT
COUNT(*)
FROM
/*IF data.URIAGE_SHIHARAI_KBN == 1*/T_SEIKYUU_DETAIL
--ELSE T_SEISAN_DETAIL/*END*/
/*BEGIN*/WHERE
/*IF !deletechuFlg*/DELETE_FLG = 0/*END*/
 AND DENPYOU_SYSTEM_ID = /*data.SYSTEM_ID*/
 AND DENPYOU_SEQ = /*data.SEQ*/
/*IF data.URIAGE_SHIHARAI_KBN == 1*/
 AND TORIHIKISAKI_CD = /*data.SEIKYU_CD*/
-- ELSE
 AND TORIHIKISAKI_CD = /*data.SHIHARAI_CD*/
 /*END*/
 AND DENPYOU_SHURUI_CD = /*data.DENPYO_SHURUI_CD*/
 /*IF data.SAISHIME_FLG && data.SAISHIME_NUMBER_LIST.Count > 0*/
 /*IF data.URIAGE_SHIHARAI_KBN == 1*/
 AND SEIKYUU_NUMBER NOT IN /*data.SAISHIME_NUMBER_LIST*/(0)
 -- ELSE
 AND SEISAN_NUMBER NOT IN /*data.SAISHIME_NUMBER_LIST*/(0)
  /*END*/
 /*END*/
/*END*/