﻿SELECT 
  '' AS DENPYOU_KBN_NAME, 
  M_SHURUI.SHURUI_CD, 
  M_SHURUI.SHURUI_NAME_RYAKU, 
  M_SHURUI.SHURUI_FURIGANA, 
  M_HINMEI.HINMEI_CD, 
  M_HINMEI.HINMEI_NAME_RYAKU, 
  M_HINMEI.HINMEI_FURIGANA, 
  M_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME, 
  '' AS TANKA, 
  M_HINMEI.HINMEI_NAME,
  M_HINMEI.UNIT_CD,
  '' AS DENPYOU_KBN_CD 
FROM M_HINMEI 
LEFT JOIN M_SHURUI ON M_HINMEI.SHURUI_CD = M_SHURUI.SHURUI_CD 
AND M_SHURUI.DELETE_FLG = 0
LEFT JOIN M_UNIT ON M_HINMEI.UNIT_CD = M_UNIT.UNIT_CD 
AND M_UNIT.DELETE_FLG = 0