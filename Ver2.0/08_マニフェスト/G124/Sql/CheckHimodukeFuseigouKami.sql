﻿SELECT
	'1'                       AS FIRST_MANIFEST_KBN,
	TMR.NEXT_HAIKI_KBN_CD     AS HAIKI_KBN_CD,
	MHK.HAIKI_KBN_NAME_RYAKU  AS HAIKI_KBN_NAME,
	NEXT_MANI.MANIFEST_ID     AS MANIFEST_ID,
	NEXT_MANI.SYSTEM_ID        AS SYSTEM_ID,
	'310'                     AS CK_KOUMOKU

FROM
    T_MANIFEST_RELATION AS TMR
    INNER JOIN (
        -- マニフェスト番号を取得
        SELECT
		    MENTRY.SYSTEM_ID,
            MDETAIL.DETAIL_SYSTEM_ID,
            MAX(MENTRY.MANIFEST_ID) AS MANIFEST_ID,
            MENTRY.KYOTEN_CD
        FROM
             T_MANIFEST_ENTRY AS MENTRY
			LEFT JOIN T_MANIFEST_DETAIL AS MDETAIL 
				ON MDETAIL.SYSTEM_ID = MENTRY.SYSTEM_ID
				AND MENTRY.SEQ = MDETAIL.SEQ
        GROUP BY
		    MENTRY.SYSTEM_ID,
            MDETAIL.DETAIL_SYSTEM_ID,
            MENTRY.KYOTEN_CD
    ) AS NEXT_MANI ON TMR.NEXT_HAIKI_KBN_CD <> 4 AND TMR.NEXT_SYSTEM_ID = NEXT_MANI.DETAIL_SYSTEM_ID,
    M_HAIKI_KBN         AS MHK
WHERE
    (
        /*data.JOUKEN*/ = '6'
--二次=紙 ,一次=紙の場合
    AND (
        NOT EXISTS (
            -- 二次紙マニチェック
            SELECT
                MENTRY.SYSTEM_ID
            FROM
                 T_MANIFEST_ENTRY AS MENTRY
				LEFT JOIN T_MANIFEST_DETAIL AS MDETAIL 
				    ON MDETAIL.SYSTEM_ID = MENTRY.SYSTEM_ID
					AND MENTRY.SEQ = MDETAIL.SEQ
            WHERE
                TMR.NEXT_HAIKI_KBN_CD <> 4
                AND TMR.NEXT_SYSTEM_ID  = MDETAIL.DETAIL_SYSTEM_ID
                AND MENTRY.DELETE_FLG = 0
        )
        OR NOT EXISTS (
            -- 一次紙マニチェック
            SELECT
                MENTRY.SYSTEM_ID
            FROM
                T_MANIFEST_ENTRY AS MENTRY
                INNER JOIN T_MANIFEST_DETAIL AS MDETAIL
                    ON MENTRY.SYSTEM_ID = MDETAIL.SYSTEM_ID
                    AND MENTRY.SEQ = MDETAIL.SEQ
            WHERE
                TMR.FIRST_HAIKI_KBN_CD <> 4
                AND TMR.FIRST_SYSTEM_ID = MDETAIL.DETAIL_SYSTEM_ID
                AND MENTRY.DELETE_FLG = 0
        )
    )
    AND TMR.DELETE_FLG = 0
    AND TMR.NEXT_HAIKI_KBN_CD  <> 4
    AND TMR.FIRST_HAIKI_KBN_CD <> 4
    AND TMR.NEXT_HAIKI_KBN_CD = MHK.HAIKI_KBN_CD
	)
/*IF data.KYOTEN != null && data.KYOTEN != '' && data.KYOTEN != '99'*/
AND NEXT_MANI.KYOTEN_CD = /*data.KYOTEN*/0
/*END*/

UNION

SELECT
	'1'                    AS FIRST_MANIFEST_KBN,
	TMR.NEXT_HAIKI_KBN_CD  AS HAIKI_KBN_CD,
	MHK.HAIKI_KBN_NAME_RYAKU  AS HAIKI_KBN_NAME,
	NEXT_MANI.MANIFEST_ID  AS MANIFEST_ID,
	NEXT_MANI.SYSTEM_ID    AS SYSTEM_ID,
	'310'                  AS CK_KOUMOKU

FROM
    T_MANIFEST_RELATION AS TMR
    INNER JOIN (
        -- マニフェスト番号を取得
         SELECT
		    MENTRY.SYSTEM_ID,
            MDETAIL.DETAIL_SYSTEM_ID,
            MAX(MENTRY.MANIFEST_ID) AS MANIFEST_ID,
            MENTRY.KYOTEN_CD
        FROM
             T_MANIFEST_ENTRY AS MENTRY
			LEFT JOIN T_MANIFEST_DETAIL AS MDETAIL 
				ON MDETAIL.SYSTEM_ID = MENTRY.SYSTEM_ID
				AND MENTRY.SEQ = MDETAIL.SEQ
        GROUP BY
		    MENTRY.SYSTEM_ID,
            MDETAIL.DETAIL_SYSTEM_ID,
            MENTRY.KYOTEN_CD
    ) AS NEXT_MANI ON TMR.NEXT_HAIKI_KBN_CD <> 4 AND TMR.NEXT_SYSTEM_ID = NEXT_MANI.DETAIL_SYSTEM_ID,
    M_HAIKI_KBN         AS MHK
WHERE
    (
        /*data.JOUKEN*/ = '6'
--二次=紙 ,一次=電子の場合
    AND (
        NOT EXISTS (
            -- 二次紙マニチェック
            SELECT
                MENTRY.SYSTEM_ID
            FROM
                T_MANIFEST_ENTRY AS MENTRY
				LEFT JOIN T_MANIFEST_DETAIL AS MDETAIL 
				    ON MDETAIL.SYSTEM_ID = MENTRY.SYSTEM_ID
					AND MENTRY.SEQ = MDETAIL.SEQ
            WHERE
                TMR.NEXT_HAIKI_KBN_CD <> 4
                AND TMR.NEXT_SYSTEM_ID  = MDETAIL.DETAIL_SYSTEM_ID
                AND MENTRY.DELETE_FLG = 0
        )
        OR NOT EXISTS (
            -- 一次電マニチェック
            SELECT
                R18.SYSTEM_ID
            FROM
                (
                    SELECT
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE EX.SYSTEM_ID END) AS SYSTEM_ID,
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DELETE_FLG ELSE EX.DELETE_FLG END) AS DELETE_FLG
                    FROM
                        DT_R18_EX AS EX
                        LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS MIX
                            ON EX.SYSTEM_ID = MIX.SYSTEM_ID
                    WHERE
                        EX.DELETE_FLG = 0
                ) AS R18
            WHERE
                TMR.FIRST_HAIKI_KBN_CD = 4
                AND TMR.FIRST_SYSTEM_ID = R18.SYSTEM_ID
                AND R18.DELETE_FLG = 0
        )
    )
    AND TMR.DELETE_FLG = 0
    AND TMR.NEXT_HAIKI_KBN_CD <> 4
    AND TMR.FIRST_HAIKI_KBN_CD = 4
    AND TMR.NEXT_HAIKI_KBN_CD = MHK.HAIKI_KBN_CD
)
/*IF data.KYOTEN != null && data.KYOTEN != '' && data.KYOTEN != '99'*/
AND NEXT_MANI.KYOTEN_CD = /*data.KYOTEN*/0
/*END*/
GROUP BY    TMR.NEXT_HAIKI_KBN_CD,
            MHK.HAIKI_KBN_NAME_RYAKU,
            NEXT_MANI.SYSTEM_ID,
            NEXT_MANI.MANIFEST_ID

ORDER BY
    TMR.NEXT_HAIKI_KBN_CD,
    MHK.HAIKI_KBN_NAME_RYAKU,
    NEXT_MANI.SYSTEM_ID,
    NEXT_MANI.MANIFEST_ID