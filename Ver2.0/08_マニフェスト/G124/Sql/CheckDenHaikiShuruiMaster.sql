﻿SELECT
    HAIKISHURUI.HAIKI_SHURUI_CD,
    HAIKISHURUI.HAIKI_SHURUI_NAME
FROM
    M_DENSHI_HAIKI_SHURUI AS HAIKISHURUI
WHERE
HAIKISHURUI.DELETE_FLG = 0
AND (
        HAIKISHURUI.HOUKOKUSHO_BUNRUI_CD IS NULL
    OR  HAIKISHURUI.HOUKOKUSHO_BUNRUI_CD = ''
    )