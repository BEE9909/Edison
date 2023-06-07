﻿SELECT UKEIRE.SYSTEM_ID FROM T_UKEIRE_ENTRY UKEIRE
WHERE NOT EXISTS(
    SELECT 1 FROM T_SEIKYUU_DETAIL DETAIL
    WHERE DETAIL.DENPYOU_SHURUI_CD = 1
    AND DETAIL.DENPYOU_SYSTEM_ID = UKEIRE.SYSTEM_ID
    AND DETAIL.DENPYOU_SEQ = UKEIRE.SEQ
	AND DETAIL.DELETE_FLG = 0)
AND UKEIRE.TORIHIKISAKI_CD = /*torihikisakiCd*/
AND UKEIRE.DELETE_FLG = 0

UNION ALL

SELECT SHUKKA.SYSTEM_ID FROM T_SHUKKA_ENTRY SHUKKA
WHERE NOT EXISTS(
    SELECT 1 FROM T_SEIKYUU_DETAIL DETAIL
    WHERE DETAIL.DENPYOU_SHURUI_CD = 1
    AND DETAIL.DENPYOU_SYSTEM_ID = SHUKKA.SYSTEM_ID
    AND DETAIL.DENPYOU_SEQ = SHUKKA.SEQ
	AND DETAIL.DELETE_FLG = 0)
AND SHUKKA.TORIHIKISAKI_CD = /*torihikisakiCd*/
AND SHUKKA.DELETE_FLG = 0

UNION ALL

SELECT URSH.SYSTEM_ID FROM T_UR_SH_ENTRY URSH
WHERE NOT EXISTS(
    SELECT 1 FROM T_SEIKYUU_DETAIL DETAIL
    WHERE DETAIL.DENPYOU_SHURUI_CD = 1
    AND DETAIL.DENPYOU_SYSTEM_ID = URSH.SYSTEM_ID
    AND DETAIL.DENPYOU_SEQ = URSH.SEQ
	AND DETAIL.DELETE_FLG = 0)
AND URSH.TORIHIKISAKI_CD = /*torihikisakiCd*/
AND URSH.DELETE_FLG = 0