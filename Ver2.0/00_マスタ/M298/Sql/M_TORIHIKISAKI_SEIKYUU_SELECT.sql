﻿SELECT M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD
, M_TORIHIKISAKI.TORIHIKISAKI_NAME1
, M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD
, M_BANK.BANK_NAME
, M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD
, M_BANK_SHITEN.BANK_SHITEN_NAME
, M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD FURIKOMI_BANK_CD_AFTER
, M_BANK.BANK_NAME BANK_NAME_AFTER
, M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD FURIKOMI_BANK_SHITEN_CD_AFTER
, M_BANK_SHITEN.BANK_SHITEN_NAME BANK_SHITEN_NAME_AFTER
, M_TORIHIKISAKI_SEIKYUU.KOUZA_SHURUI KOUZA_SHURUI_AFTER
, M_TORIHIKISAKI_SEIKYUU.KOUZA_NO KOUZA_NO_AFTER
, M_TORIHIKISAKI_SEIKYUU.KOUZA_NAME KOUZA_NAME_AFTER
FROM M_TORIHIKISAKI_SEIKYUU
INNER JOIN M_TORIHIKISAKI ON M_TORIHIKISAKI.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD
INNER JOIN M_BANK ON M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = M_BANK.BANK_CD
INNER JOIN M_BANK_SHITEN ON M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD = M_BANK_SHITEN.BANK_SHITEN_CD
AND M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = M_BANK_SHITEN.BANK_CD
WHERE M_TORIHIKISAKI.DELETE_FLG = 0
AND M_BANK.DELETE_FLG = 0
AND M_BANK_SHITEN.DELETE_FLG = 0
/*IF data.BankCd != null && data.BankCd != ''*/
AND M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = /*data.BankCd*/
/*END*/
/*IF data.BankShitenCd != null && data.BankShitenCd != ''*/
AND M_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD =  /*data.BankShitenCd*/
/*END*/
/*IF data.KouzaShurui != null && data.KouzaShurui != ''*/
AND M_TORIHIKISAKI_SEIKYUU.KOUZA_SHURUI =  /*data.KouzaShurui*/
/*END*/
/*IF data.KouzaNo != null && data.KouzaNo != ''*/
AND M_TORIHIKISAKI_SEIKYUU.KOUZA_NO =  /*data.KouzaNo*/
/*END*/
ORDER BY M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD