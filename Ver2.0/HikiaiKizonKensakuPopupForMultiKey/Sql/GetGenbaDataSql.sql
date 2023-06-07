﻿SELECT 
  M_GENBA.GENBA_CD 
, M_GENBA.GENBA_NAME_RYAKU 
, M_GENBA.GENBA_FURIGANA 
, M_GENBA.GENBA_POST 
, M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU 
, M_GENBA.GENBA_ADDRESS1 
, M_GENBA.GENBA_TEL 
, CONVERT(bit, '0') AS GENBA_HIKIAI_FLG
, M_GYOUSHA.GYOUSHA_CD
, M_GYOUSHA.GYOUSHA_NAME_RYAKU
, M_GYOUSHA.GYOUSHA_FURIGANA
, M_GYOUSHA.GYOUSHA_POST
, M_GYOUSHA.GYOUSHA_ADDRESS1
, M_GYOUSHA.GYOUSHA_TEL
, CONVERT(bit, '0') AS GYOUSHA_HIKIAI_FLG
, M_GYOUSHA.SHOKUCHI_KBN AS GYOUSHA_SHOKUCHI_KBN 
, M_GENBA.SHOKUCHI_KBN AS GENBA_SHOKUCHI_KBN 
, M_GYOUSHA.TEKIYOU_BEGIN
, M_GYOUSHA.TEKIYOU_END
, M_GYOUSHA.DELETE_FLG 
FROM M_GENBA 
INNER JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD 
LEFT JOIN M_TODOUFUKEN ON M_GENBA.GENBA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD 
AND M_TODOUFUKEN.DELETE_FLG = 0 