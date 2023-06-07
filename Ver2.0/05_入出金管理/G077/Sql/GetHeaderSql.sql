﻿SELECT
NYUUKIN.SYSTEM_ID,
NYUUKIN.SEQ,
RIGHT('00' + convert(varchar,NYUUKIN.KYOTEN_CD),2) as KYOTEN_CD,
NYUUKIN.NYUUKIN_NUMBER,
NYUUKIN.DENPYOU_DATE,
NYUUKIN.TORIHIKISAKI_CD,
NYUUKIN.BANK_CD,
NYUUKIN.BANK_SHITEN_CD,
NYUUKIN.KOUZA_SHURUI,
NYUUKIN.KOUZA_NO,
NYUUKIN.KOUZA_NAME,
NYUUKIN.EIGYOU_TANTOUSHA_CD,
NYUUKIN.NYUUKIN_AMOUNT_TOTAL,
NYUUKIN.CHOUSEI_AMOUNT_TOTAL,
NYUUKIN.DENPYOU_BIKOU,
NYUUKIN.CREATE_USER,
NYUUKIN.CREATE_DATE,
NYUUKIN.CREATE_PC,
NYUUKIN.UPDATE_USER,
NYUUKIN.UPDATE_DATE,
NYUUKIN.UPDATE_PC,
NYUUKIN.TIME_STAMP,
KYOTEN.KYOTEN_NAME_RYAKU,
TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU,
SEIKYUU.TORIHIKI_KBN_CD,
SEIKYUU.KAISHI_URIKAKE_ZANDAKA,
SEIKYUU.KAISHUU_DAY,
SEIKYUU.SHIMEBI1,
SEIKYUU.SHIMEBI2,
SEIKYUU.SHIMEBI3,
BANK.BANK_NAME_RYAKU,
SHITEN.BANK_SHIETN_NAME_RYAKU,
SHAIN.SHAIN_NAME_RYAKU
FROM
T_NYUUKIN_ENTRY NYUUKIN
LEFT OUTER JOIN M_KYOTEN KYOTEN ON KYOTEN.KYOTEN_CD = NYUUKIN.KYOTEN_CD 
LEFT OUTER JOIN M_TORIHIKISAKI TORIHIKISAKI ON TORIHIKISAKI.TORIHIKISAKI_CD = NYUUKIN.TORIHIKISAKI_CD 
LEFT OUTER JOIN M_TORIHIKISAKI_SEIKYUU SEIKYUU ON SEIKYUU.TORIHIKISAKI_CD = NYUUKIN.TORIHIKISAKI_CD 
LEFT OUTER JOIN M_BANK BANK ON BANK.BANK_CD = NYUUKIN.BANK_CD 
LEFT OUTER JOIN M_BANK_SHITEN SHITEN ON (SHITEN.BANK_CD = NYUUKIN.BANK_CD AND SHITEN.BANK_SHITEN_CD = NYUUKIN.BANK_SHITEN_CD)
LEFT OUTER JOIN M_SHAIN SHAIN ON SHAIN.SHAIN_CD = NYUUKIN.EIGYOU_TANTOUSHA_CD 
/*BEGIN*/
WHERE 
/*IF !deletechuFlg*/ NYUUKIN.DELETE_FLG = 0 /*END*/
/*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
AND  NYUUKIN.NYUUKIN_NUMBER = /*data.Nyukin_number*/ /*END*/
/*END*/