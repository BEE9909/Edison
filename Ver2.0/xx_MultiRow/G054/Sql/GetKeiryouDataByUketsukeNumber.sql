SELECT
 TKE.KEIRYOU_NUMBER 
FROM T_KEIRYOU_ENTRY AS TKE
WHERE TKE.UKETSUKE_NUMBER = /*uketsukeNum*/
AND TKE.DELETE_FLG = 0
