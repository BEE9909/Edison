UPDATE M_DENSHI_JIGYOUJOU SET
	GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001',
	GENBA_CD = /*data.GENBA_CD*/'000001',
	UPDATE_USER = /*data.UPDATE_USER*/'',
	UPDATE_DATE = /*data.UPDATE_DATE*/'',
	UPDATE_PC = /*data.UPDATE_PC*/''
 WHERE EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*/'0000001'
   AND JIGYOUJOU_CD = /*data.JIGYOUJOU_CD*/'000001'