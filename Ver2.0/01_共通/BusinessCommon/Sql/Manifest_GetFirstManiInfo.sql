﻿-- 二次マニのSYSTEMID, HAIKI_KBN_CDから一次マニの情報を取得する
SELECT
    TMR.FIRST_SYSTEM_ID,
    TMR.FIRST_HAIKI_KBN_CD,
    CASE WHEN PAPER_MANI.SEQ IS NOT NULL
        THEN PAPER_MANI.SEQ
        ELSE ''
    END AS SEQ,
    CASE WHEN PAPER_MANI.MANIFEST_ID IS NOT NULL
        THEN PAPER_MANI.MANIFEST_ID
        ELSE ELEC_MANI.MANIFEST_ID
    END AS MANIFEST_ID,
    PAPER_MANI.SYSTEM_ID AS PAPER_SYSTEM_ID,
    ISNULL(ELEC_MANI.KANRI_ID, '') AS KANRI_ID,
    ELEC_MANI.LATEST_SEQ AS LATEST_SEQ,
    ELEC_MANI.EX_SYSTEM_ID AS EX_SYSTEM_ID,
    ELEC_MANI.EX_SEQ AS EX_SEQ,
	ELEC_MANI.LAST_SBN_END_DATE
FROM 
    T_MANIFEST_RELATION AS TMR WITH(NOLOCK)
    LEFT JOIN (
        SELECT
            TME.SYSTEM_ID AS SYSTEM_ID,
            TME.SEQ AS SEQ,
            TMD.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID,
            TME.MANIFEST_ID AS MANIFEST_ID
        FROM
            T_MANIFEST_ENTRY AS TME WITH(NOLOCK)
            INNER JOIN T_MANIFEST_DETAIL AS TMD WITH(NOLOCK) ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ
        WHERE
            TME.DELETE_FLG = 0
    ) AS PAPER_MANI ON TMR.FIRST_SYSTEM_ID = PAPER_MANI.DETAIL_SYSTEM_ID AND TMR.FIRST_HAIKI_KBN_CD <> 4
    LEFT JOIN (
        SELECT
            CASE WHEN R18MIX.DETAIL_SYSTEM_ID IS NOT NULL
                THEN R18MIX.DETAIL_SYSTEM_ID
                ELSE R18EX.SYSTEM_ID
            END AS SYSTEM_ID,
            TOC.KANRI_ID AS KANRI_ID,
            TOC.LATEST_SEQ AS LATEST_SEQ,
            R18EX.SYSTEM_ID AS EX_SYSTEM_ID,
            R18EX.SEQ AS EX_SEQ,
            TOC.MANIFEST_ID AS MANIFEST_ID,
			R13.LAST_SBN_END_DATE
        FROM
            DT_MF_TOC AS TOC WITH (NOLOCK)
            INNER JOIN DT_R18 AS R18 WITH (NOLOCK) ON TOC.KANRI_ID = R18.KANRI_ID AND TOC.LATEST_SEQ = R18.SEQ
            INNER JOIN DT_R18_EX AS R18EX WITH(NOLOCK) ON R18.KANRI_ID = R18EX.KANRI_ID AND R18EX.DELETE_FLG = 0
            LEFT JOIN DT_R18_MIX AS R18MIX WITH(NOLOCK) ON R18EX.KANRI_ID = R18MIX.KANRI_ID AND R18MIX.DELETE_FLG = 0
			LEFT JOIN DT_R13 AS R13 WITH(NOLOCK) ON TOC.KANRI_ID = R13.KANRI_ID AND TOC.LATEST_SEQ = R13.SEQ
    ) AS ELEC_MANI ON TMR.FIRST_SYSTEM_ID = ELEC_MANI.SYSTEM_ID AND TMR.FIRST_HAIKI_KBN_CD = 4

WHERE
    TMR.DELETE_FLG = 0 
    AND TMR.NEXT_SYSTEM_ID = /*SYSTEM_ID*/0 
    AND TMR.NEXT_HAIKI_KBN_CD = /*HAIKI_KBN_CD*/0