﻿SELECT
    TMLS.TORIHIKISAKI_CD,
    MT.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME,
    TMLS.PREVIOUS_MONTH_BALANCE AS KURIKOSHI_ZANDAKA,
    TMLS.SHUKKIN_KINGAKU AS SHUKKIN_KINGAKU,
    TMLS.KINGAKU AS SHIHARAI_KINGAKU,
    (TMLS.TAX + ISNULL (TMAS.ADJUST_TAX, 0)) AS SHOHIZEI,
    (TMLS.TOTAL_KINGAKU + ISNULL (TMAS.ADJUST_TAX, 0)) AS ZEIKOMI_SHIHARAI,
    ISNULL (TMAS.ZANDAKA, TMLS.ZANDAKA) AS SASHIHIKI_ZANDAKA,
    MTS.SHIMEBI1,
    MTS.SHIMEBI2,
    MTS.SHIMEBI3
FROM
    T_MONTHLY_LOCK_SH AS TMLS
    LEFT JOIN
        T_MONTHLY_ADJUST_SH AS TMAS
    ON  TMLS.TORIHIKISAKI_CD = TMAS.TORIHIKISAKI_CD
    AND TMLS.YEAR = TMAS.YEAR
    AND TMLS.MONTH = TMAS.MONTH
    AND TMLS.SEQ = TMAS.SEQ
    INNER JOIN
        M_TORIHIKISAKI AS MT
    ON  TMLS.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD
    LEFT JOIN
        M_TORIHIKISAKI_SHIHARAI AS MTS
    ON  TMLS.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
WHERE
    TMLS.DELETE_FLG = 0
AND (
        TMAS.DELETE_FLG = 0
    OR  TMAS.DELETE_FLG IS NULL
    )
AND TMLS.YEAR = /*year*/2014
AND TMLS.MONTH = /*month*/12
AND TMLS.TORIHIKISAKI_CD >= /*startCD*/'700001'
AND TMLS.TORIHIKISAKI_CD <= /*endCD*/'700001'
AND MTS.TORIHIKI_KBN_CD = 2
ORDER BY TMLS.TORIHIKISAKI_CD