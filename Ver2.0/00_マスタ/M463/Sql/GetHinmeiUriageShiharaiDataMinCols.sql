SELECT 
 H.HINMEI_CD,
 H.HINMEI_NAME_RYAKU,
 H.DENPYOU_KBN_CD,
 H.UNIT_CD 
FROM M_HINMEI H 
WHERE H.HINMEI_CD = /*data.HINMEI_CD*/''
  AND H.DENSHU_KBN_CD IN (3, 9)
  AND H.DELETE_FLG = 0