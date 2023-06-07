﻿SELECT
    LOCK.TORIHIKISAKI_CD,
    TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU,
    LOCK.YEAR,
    LOCK.MONTH,
    LOCK.SEQ,
    LOCK.PREVIOUS_MONTH_BALANCE,
    /*IF isUR*/
    LOCK.NYUUKIN_KINGAKU AS NYUUSHUKKIN_KINGAKU,
    -- ELSE LOCK.SHUKKIN_KINGAKU AS NYUUSHUKKIN_KINGAKU,
    /*END*/
    LOCK.KINGAKU,
    LOCK.TAX,
    LOCK.TOTAL_KINGAKU,
    LOCK.ZANDAKA AS LOCK_ZANDAKA,
    ADJUST.UPDATE_SEQ,
    ADJUST.ADJUST_TAX,
    ADJUST.ZANDAKA
FROM
    /*IF isUR*/
    T_MONTHLY_LOCK_UR AS LOCK
    -- ELSE T_MONTHLY_LOCK_SH AS LOCK 
    /*END*/
    INNER JOIN
        M_TORIHIKISAKI AS TORIHIKISAKI
    ON  LOCK.TORIHIKISAKI_CD = TORIHIKISAKI.TORIHIKISAKI_CD
    INNER JOIN
        /*IF isUR*/
        M_TORIHIKISAKI_SEIKYUU AS TS
        -- ELSE M_TORIHIKISAKI_SHIHARAI AS TS
        /*END*/
    ON  LOCK.TORIHIKISAKI_CD = TS.TORIHIKISAKI_CD
    AND TS.TORIHIKI_KBN_CD = 2
    LEFT JOIN
        /*IF isUR*/
        T_MONTHLY_ADJUST_UR AS ADJUST
        -- ELSE T_MONTHLY_ADJUST_SH AS ADJUST
        /*END*/
    ON  LOCK.TORIHIKISAKI_CD = ADJUST.TORIHIKISAKI_CD
    AND LOCK.YEAR = ADJUST.YEAR
    AND LOCK.MONTH = ADJUST.MONTH
    AND LOCK.SEQ = ADJUST.SEQ
    AND ADJUST.DELETE_FLG = 0
WHERE
    LOCK.DELETE_FLG = 0
AND LOCK.YEAR = /*year*/2015
AND LOCK.MONTH = /*month*/1
AND (
        LOCK.PREVIOUS_MONTH_BALANCE != 0
    /*IF isUR*/
    OR  LOCK.NYUUKIN_KINGAKU != 0
    -- ELSE OR  LOCK.SHUKKIN_KINGAKU != 0
    /*END*/
    OR  LOCK.KINGAKU != 0
    OR  LOCK.TAX != 0
    OR  LOCK.TOTAL_KINGAKU != 0
    OR  LOCK.ZANDAKA != 0
    )
ORDER BY 
TORIHIKISAKI_CD