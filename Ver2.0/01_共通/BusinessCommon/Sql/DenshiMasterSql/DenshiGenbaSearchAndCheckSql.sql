﻿--電子現場検索
SELECT	M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID, 
		M_DENSHI_JIGYOUJOU.GYOUSHA_CD, 
		M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME, 
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_CD, 
		M_DENSHI_JIGYOUJOU.GENBA_CD,
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME,
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_POST,
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1 AS TODOFUKEN_NAME,
		(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2 + 
		 M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3 + 
		 M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4) AS DISP_JIGYOUJOU_ADDRESS,
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_TEL, 
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, 
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2,
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3,
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4,
		M_DENSHI_JIGYOUJOU.JIGYOUJOU_KBN,
		(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1 + 
		 M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2 + 
		 M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3 + 
		 M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4) AS JIGYOUJOU_ADDRESS,
		M_DENSHI_JIGYOUSHA.JIGYOUSHA_POST, 
		M_DENSHI_JIGYOUSHA.JIGYOUSHA_TEL,
		(M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS1+
		 M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS2+
		 M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS3+
		 M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS4) AS JIGYOUSHA_ADDRESS,
		M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS1
FROM M_DENSHI_JIGYOUJOU 
LEFT JOIN M_DENSHI_JIGYOUSHA
 ON M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID = M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID
/*IF !data.ISNEED_SAME_GYOUSHA_FLG.IsNull && data.ISNEED_SAME_GYOUSHA_FLG.IsTrue*/
AND M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_DENSHI_JIGYOUSHA.GYOUSHA_CD
/*END*/
LEFT JOIN M_GENBA 
ON M_GENBA.GYOUSHA_CD = M_DENSHI_JIGYOUJOU.GYOUSHA_CD 
AND M_GENBA.GENBA_CD = M_DENSHI_JIGYOUJOU.GENBA_CD 
LEFT JOIN M_GYOUSHA 
ON M_GYOUSHA.GYOUSHA_CD = M_DENSHI_JIGYOUSHA.GYOUSHA_CD 
WHERE M_DENSHI_JIGYOUJOU.GYOUSHA_CD IS NOT NULL AND M_DENSHI_JIGYOUJOU.GENBA_CD IS NOT NULL AND M_DENSHI_JIGYOUJOU.GYOUSHA_CD!='' AND M_DENSHI_JIGYOUJOU.GENBA_CD!='' 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
AND M_GENBA.DELETE_FLG = 0
AND M_GYOUSHA.DELETE_FLG = 0
/*END*/
/*IF data.EDI_MEMBER_ID != null &&  data.EDI_MEMBER_ID !='' */ AND M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*//*END*/
/*IF data.JIGYOUJOU_CD != null &&  data.JIGYOUJOU_CD !='' */ AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_CD = /*data.JIGYOUJOU_CD*//*END*/
/*IF data.GYOUSHA_CD != null &&  data.GYOUSHA_CD !='' */ AND M_DENSHI_JIGYOUJOU.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.GENBA_CD != null &&  data.GENBA_CD !='' && (data.JIGYOUJOU_FLG == null || data.JIGYOUJOU_FLG == '')*/ AND M_DENSHI_JIGYOUJOU.GENBA_CD = /*data.GENBA_CD*//*END*/
/*IF data.JIGYOUSHA_KBN != null &&  data.JIGYOUSHA_KBN !='' */ AND M_DENSHI_JIGYOUJOU.JIGYOUSHA_KBN = /*data.JIGYOUSHA_KBN*//*END*/
/*IF data.JIGYOUJOU_KBN != null &&  data.JIGYOUJOU_KBN !='' */ AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_KBN = /*data.JIGYOUJOU_KBN*//*END*/
/*IF data.JIGYOUSHA_KBN_LIST != null*/ AND M_DENSHI_JIGYOUJOU.JIGYOUSHA_KBN in /*data.JIGYOUSHA_KBN_LIST*/('-1', '0')/*END*/
/*IF data.JIGYOUJOU_KBN_LIST != null*/ AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_KBN in /*data.JIGYOUJOU_KBN_LIST*/('-1', '0')/*END*/
/*IF data.JIGYOUJOU_FLG == null || data.JIGYOUJOU_FLG == '' */
 /*IF data.JIGYOUJOU_NAME != null &&  data.JIGYOUJOU_NAME !='' */ AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME = /*data.JIGYOUJOU_NAME*//*END*/ 
 /*IF data.JIGYOUJOU_ADDRESS != null &&  data.JIGYOUJOU_ADDRESS !='' */ AND ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1,'') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2,'') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3,'') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4,'') = /*data.JIGYOUJOU_ADDRESS*//*END*/
 /*IF data.JIGYOUJOU_TEL != null && data.JIGYOUJOU_TEL != '' */ AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_TEL = /*data.JIGYOUJOU_TEL*//*END*/
-- ELSE AND JIGYOUJOU_NAME = /*data.JIGYOUJOU_NAME*/ COLLATE Japanese_CS_AS_KS_WS AND ISNULL(JIGYOUJOU_ADDRESS1,'') + ISNULL(JIGYOUJOU_ADDRESS2,'') + ISNULL(JIGYOUJOU_ADDRESS3,'') + ISNULL(JIGYOUJOU_ADDRESS4,'') = /*data.JIGYOUJOU_ADDRESS*/
/*END*/
/*BEGIN*/
AND (
	/*IF data.HST_KBN != null &&  data.HST_KBN !='' */ M_DENSHI_JIGYOUSHA.HST_KBN = 1/*END*/
	/*IF data.UPN_KBN != null &&  data.UPN_KBN !='' */ OR M_DENSHI_JIGYOUSHA.UPN_KBN = 1/*END*/
	/*IF data.SBN_KBN != null &&  data.SBN_KBN !='' */ OR M_DENSHI_JIGYOUSHA.SBN_KBN = 1/*END*/
	)
/*END*/