﻿SELECT 
M_BANK_SHITEN.BANK_CD,
M_BANK.BANK_NAME_RYAKU,
M_BANK_SHITEN.BANK_SHITEN_CD,
M_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU,
M_BANK_SHITEN.BANK_SHITEN_FURIGANA,
M_BANK_SHITEN.KOUZA_SHURUI,
M_BANK_SHITEN.KOUZA_NO,
M_BANK_SHITEN.KOUZA_NAME 
FROM M_BANK_SHITEN 
LEFT JOIN M_BANK ON M_BANK_SHITEN.BANK_CD = M_BANK.BANK_CD 
AND M_BANK.DELETE_FLG = 0 
WHERE 
 M_BANK_SHITEN.DELETE_FLG = 0