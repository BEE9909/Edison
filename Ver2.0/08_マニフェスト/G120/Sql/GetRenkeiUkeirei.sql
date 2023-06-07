﻿SELECT
TUE.DENPYOU_DATE AS KOHU_DATE,
TUE.TORIHIKISAKI_CD,
TUE.TORIHIKISAKI_NAME,
TUE.GYOUSHA_CD,
TUE.GYOUSHA_NAME,
TUE.GENBA_CD,
TUE.GENBA_NAME,
TUE.UNPAN_GYOUSHA_CD,
TUE.UNPAN_GYOUSHA_NAME,
TUE.SHASHU_CD,
TUE.SHASHU_NAME,
TUE.SHARYOU_CD,
TUE.SHARYOU_NAME,
TUE.UNTENSHA_CD,
TUE.UNTENSHA_NAME,
TUE.NIOROSHI_GENBA_CD,
TUE.NIOROSHI_GENBA_NAME,
TUE.NIOROSHI_GYOUSHA_CD,
TUE.NIOROSHI_GYOUSHA_NAME,
TUE.DENPYOU_DATE AS UNBAN_DATE,
MHS.HAIKI_SHURUI_CD,
MHS.HAIKI_SHURUI_NAME_RYAKU,
GB.TSUMIKAEHOKAN_KBN,
GB.SHOBUN_NIOROSHI_GENBA_KBN,
TUE.SYSTEM_ID,
TUD.DETAIL_SYSTEM_ID,
TUE.MANIFEST_SHURUI_CD
FROM(SELECT TOP 1 TUE.* FROM T_UKEIRE_ENTRY TUE 
left join T_UKEIRE_DETAIL TUD ON (TUE.SYSTEM_ID = TUD.SYSTEM_ID AND TUE.SEQ = TUD.SEQ)
	WHERE TUE.UKEIRE_NUMBER =  /*data.RENKEI_NO*/0
	AND TUE.DELETE_FLG = 0 
	/*IF data.RENKEI_GYO_NO != NULL && data.RENKEI_GYO_NO !=''*/
	AND TUD.ROW_NO = /*data.RENKEI_GYO_NO*/0
	/*END*/	) TUE
left join T_UKEIRE_DETAIL TUD ON (TUE.SYSTEM_ID = TUD.SYSTEM_ID AND TUE.SEQ = TUD.SEQ)
left join M_HINMEI MH ON (MH.HINMEI_CD = TUD.HINMEI_CD)
left join M_HAIKI_SHURUI MHS ON (MHS.HAIKI_SHURUI_CD = MH.SP_TSUMIKAE_HAIKI_SHURUI_CD
							  AND MHS.HAIKI_KBN_CD = 3)
left join M_GENBA GB ON (TUE.NIOROSHI_GYOUSHA_CD = GB.GYOUSHA_CD
					 AND TUE.NIOROSHI_GENBA_CD = GB.GENBA_CD)
WHERE TUE.UKEIRE_NUMBER=  /*data.RENKEI_NO*/0
AND TUE.DELETE_FLG = 0 
/*IF data.RENKEI_GYO_NO != NULL && data.RENKEI_GYO_NO !=''*/
 AND TUD.ROW_NO = /*data.RENKEI_GYO_NO*/0
/*END*/	