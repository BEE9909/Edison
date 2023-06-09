SELECT DISTINCT 
M_TORIHIKISAKI.TORIHIKISAKI_CD, 
M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU, 
M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA, 
M_TORIHIKISAKI.TORIHIKISAKI_POST, 
M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, 
M_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1, 
M_TORIHIKISAKI.TORIHIKISAKI_TEL,
M_TORIHIKISAKI.TORIHIKISAKI_NAME1,
M_TORIHIKISAKI.TORIHIKISAKI_NAME2,
M_TORIHIKISAKI.TORIHIKISAKI_ADDRESS2 
FROM M_TORIHIKISAKI 
LEFT JOIN M_TODOUFUKEN ON M_TORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD 
AND M_TODOUFUKEN.DELETE_FLG = 0 
LEFT JOIN ( 
    SELECT UKEIRE.TORIHIKISAKI_CD FROM T_UKEIRE_ENTRY UKEIRE 
    WHERE NOT EXISTS( 
        SELECT 1 FROM T_SEIKYUU_DETAIL DETAIL 
        WHERE DETAIL.DENPYOU_SHURUI_CD = 1 
        AND DETAIL.DENPYOU_SYSTEM_ID = UKEIRE.SYSTEM_ID 
        AND DETAIL.DENPYOU_SEQ = UKEIRE.SEQ 
	    AND DETAIL.DELETE_FLG = 0) 
    AND UKEIRE.DELETE_FLG = 0 
    UNION 
    SELECT SHUKKA.TORIHIKISAKI_CD FROM T_SHUKKA_ENTRY SHUKKA 
    WHERE NOT EXISTS( 
        SELECT 1 FROM T_SEIKYUU_DETAIL DETAIL 
        WHERE DETAIL.DENPYOU_SHURUI_CD = 1 
        AND DETAIL.DENPYOU_SYSTEM_ID = SHUKKA.SYSTEM_ID 
        AND DETAIL.DENPYOU_SEQ = SHUKKA.SEQ 
	    AND DETAIL.DELETE_FLG = 0) 
    AND SHUKKA.DELETE_FLG = 0 
    UNION 
    SELECT URSH.TORIHIKISAKI_CD FROM T_UR_SH_ENTRY URSH 
    WHERE NOT EXISTS( 
        SELECT 1 FROM T_SEIKYUU_DETAIL DETAIL 
        WHERE DETAIL.DENPYOU_SHURUI_CD = 1 
        AND DETAIL.DENPYOU_SYSTEM_ID = URSH.SYSTEM_ID 
        AND DETAIL.DENPYOU_SEQ = URSH.SEQ 
	    AND DETAIL.DELETE_FLG = 0) 
    AND URSH.DELETE_FLG = 0) AS DENPYOU 
ON DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD 
LEFT JOIN T_SEIKYUU_DENPYOU 
ON M_TORIHIKISAKI.TORIHIKISAKI_CD = T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD 
AND T_SEIKYUU_DENPYOU.DELETE_FLG = 0 
AND SEIKYUU_NUMBER = (select max(SEIKYUU_NUMBER) from T_SEIKYUU_DENPYOU tmp where tmp.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD)
/*joinStr*/ 
/*sqlWhere*/ 
AND
 ( /*sqlTekiyou*/
    OR DENPYOU.TORIHIKISAKI_CD IS NOT NULL
    OR T_SEIKYUU_DENPYOU.KONKAI_SEIKYU_GAKU != 0
    OR (T_SEIKYUU_DENPYOU.KONKAI_SEIKYU_GAKU IS NULL 
        AND M_TORIHIKISAKI_SEIKYUU .KAISHI_URIKAKE_ZANDAKA != 0))
ORDER BY M_TORIHIKISAKI.TORIHIKISAKI_CD 