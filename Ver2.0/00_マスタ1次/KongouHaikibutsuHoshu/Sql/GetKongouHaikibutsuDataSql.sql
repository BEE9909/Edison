SELECT
    KON.*,
/*IF data.HAIKI_KBN_CD.Value != 4*/
    HAIKI.HAIKI_SHURUI_NAME_RYAKU HAIKI_SHURUI_NAME_RYAKU,
/*END*/
/*IF data.HAIKI_KBN_CD.Value == 4*/
    HAIKI.HAIKI_SHURUI_NAME HAIKI_SHURUI_NAME_RYAKU,
/*END*/
        KON.HAIKI_KBN_CD AS UK_HAIKI_KBN_CD,
    KON.KONGOU_SHURUI_CD AS UK_KONGOU_SHURUI_CD,
    KON.HAIKI_SHURUI_CD AS UK_HAIKI_SHURUI_CD
FROM
    dbo.M_KONGOU_HAIKIBUTSU KON
/*IF data.HAIKI_KBN_CD.Value != 4*/
    INNER JOIN dbo.M_HAIKI_SHURUI HAIKI ON KON.HAIKI_KBN_CD = HAIKI.HAIKI_KBN_CD AND KON.HAIKI_SHURUI_CD = HAIKI.HAIKI_SHURUI_CD
/*END*/
/*IF data.HAIKI_KBN_CD.Value == 4*/
    INNER JOIN dbo.M_DENSHI_HAIKI_SHURUI HAIKI ON KON.HAIKI_SHURUI_CD = (HAIKI.HAIKI_SHURUI_CD + '000')
/*END*/
WHERE KON.HAIKI_KBN_CD=/*data.HAIKI_KBN_CD.Value*/0
  AND KON.KONGOU_SHURUI_CD = /*data.KONGOU_SHURUI_CD*/'01'
ORDER BY KON.HAIKI_KBN_CD,KON.KONGOU_SHURUI_CD,KON.HAIKI_SHURUI_CD
