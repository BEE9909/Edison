﻿--引合業者マスタ
UPDATE M_HIKIAI_GYOUSHA SET UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = /*UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--引合現場マスタ
UPDATE M_HIKIAI_GENBA SET UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = /*UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--業者マスタ
UPDATE M_GYOUSHA SET UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = /*UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--現場マスタ
UPDATE M_GENBA SET UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = /*UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

/*IF CHIIKI_CD_NEW != CHIIKI_CD_OLD */
--地域マスタ
DELETE FROM  M_CHIIKI WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--更新後(CHIIKI_CD_NEW)のデータに存在
--地域別許可番号マスタ
DELETE M_CHIIKIBETSU_KYOKA 
FROM M_CHIIKIBETSU_KYOKA INNER JOIN (
   SELECT 
      MCK.CHIIKI_CD,
	  MCK.KYOKA_KBN,
      MCK.GYOUSHA_CD,
      MCK.GENBA_CD
   FROM M_CHIIKIBETSU_KYOKA MCK
   INNER JOIN  M_CHIIKIBETSU_KYOKA 
	   ON M_CHIIKIBETSU_KYOKA.DELETE_FLG = 0
	  AND M_CHIIKIBETSU_KYOKA.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_KYOKA.KYOKA_KBN = MCK.KYOKA_KBN
      AND M_CHIIKIBETSU_KYOKA.GYOUSHA_CD = MCK.GYOUSHA_CD
      AND M_CHIIKIBETSU_KYOKA.GENBA_CD = MCK.GENBA_CD
   WHERE MCK.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCK ON M_CHIIKIBETSU_KYOKA.CHIIKI_CD = KIZON_DATA_MCK.CHIIKI_CD
AND M_CHIIKIBETSU_KYOKA.KYOKA_KBN = KIZON_DATA_MCK.KYOKA_KBN
AND M_CHIIKIBETSU_KYOKA.GYOUSHA_CD = KIZON_DATA_MCK.GYOUSHA_CD
AND M_CHIIKIBETSU_KYOKA.GENBA_CD = KIZON_DATA_MCK.GENBA_CD

DELETE M_CHIIKIBETSU_KYOKA 
FROM M_CHIIKIBETSU_KYOKA INNER JOIN (
   SELECT 
      M_CHIIKIBETSU_KYOKA.CHIIKI_CD,
	  M_CHIIKIBETSU_KYOKA.KYOKA_KBN,
      M_CHIIKIBETSU_KYOKA.GYOUSHA_CD,
      M_CHIIKIBETSU_KYOKA.GENBA_CD
   FROM M_CHIIKIBETSU_KYOKA MCK
   INNER JOIN  M_CHIIKIBETSU_KYOKA 
	   ON M_CHIIKIBETSU_KYOKA.DELETE_FLG = 1
	  AND M_CHIIKIBETSU_KYOKA.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_KYOKA.KYOKA_KBN = MCK.KYOKA_KBN
      AND M_CHIIKIBETSU_KYOKA.GYOUSHA_CD = MCK.GYOUSHA_CD
      AND M_CHIIKIBETSU_KYOKA.GENBA_CD = MCK.GENBA_CD
   WHERE MCK.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCK ON M_CHIIKIBETSU_KYOKA.CHIIKI_CD = KIZON_DATA_MCK.CHIIKI_CD
AND M_CHIIKIBETSU_KYOKA.KYOKA_KBN = KIZON_DATA_MCK.KYOKA_KBN
AND M_CHIIKIBETSU_KYOKA.GYOUSHA_CD = KIZON_DATA_MCK.GYOUSHA_CD
AND M_CHIIKIBETSU_KYOKA.GENBA_CD = KIZON_DATA_MCK.GENBA_CD

DELETE M_CHIIKIBETSU_KYOKA_MEIGARA 
FROM M_CHIIKIBETSU_KYOKA_MEIGARA INNER JOIN (
   SELECT 
      MCKM.CHIIKI_CD,
	  MCKM.KYOKA_KBN,
      MCKM.GYOUSHA_CD,
      MCKM.GENBA_CD,
	  MCKM.TOKUBETSU_KANRI_KBN,
	  MCKM.HOUKOKUSHO_BUNRUI_CD
   FROM M_CHIIKIBETSU_KYOKA_MEIGARA MCKM
   INNER JOIN  M_CHIIKIBETSU_KYOKA_MEIGARA 
	   ON M_CHIIKIBETSU_KYOKA_MEIGARA.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_KYOKA_MEIGARA.KYOKA_KBN = MCKM.KYOKA_KBN
      AND M_CHIIKIBETSU_KYOKA_MEIGARA.GYOUSHA_CD = MCKM.GYOUSHA_CD
      AND M_CHIIKIBETSU_KYOKA_MEIGARA.GENBA_CD = MCKM.GENBA_CD
      AND M_CHIIKIBETSU_KYOKA_MEIGARA.TOKUBETSU_KANRI_KBN = MCKM.TOKUBETSU_KANRI_KBN
      AND M_CHIIKIBETSU_KYOKA_MEIGARA.HOUKOKUSHO_BUNRUI_CD = MCKM.HOUKOKUSHO_BUNRUI_CD
   WHERE  MCKM.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCKM ON M_CHIIKIBETSU_KYOKA_MEIGARA.CHIIKI_CD = KIZON_DATA_MCKM.CHIIKI_CD
AND M_CHIIKIBETSU_KYOKA_MEIGARA.KYOKA_KBN = KIZON_DATA_MCKM.KYOKA_KBN
AND M_CHIIKIBETSU_KYOKA_MEIGARA.GYOUSHA_CD = KIZON_DATA_MCKM.GYOUSHA_CD
AND M_CHIIKIBETSU_KYOKA_MEIGARA.GENBA_CD = KIZON_DATA_MCKM.GENBA_CD
AND M_CHIIKIBETSU_KYOKA_MEIGARA.TOKUBETSU_KANRI_KBN = KIZON_DATA_MCKM.TOKUBETSU_KANRI_KBN
AND M_CHIIKIBETSU_KYOKA_MEIGARA.HOUKOKUSHO_BUNRUI_CD = KIZON_DATA_MCKM.HOUKOKUSHO_BUNRUI_CD

--地域別業種マスタ
DELETE M_CHIIKIBETSU_GYOUSHU 
FROM M_CHIIKIBETSU_GYOUSHU INNER JOIN (
   SELECT 
      MCG.CHIIKI_CD,
	  MCG.GYOUSHU_CD
   FROM M_CHIIKIBETSU_GYOUSHU MCG
   INNER JOIN  M_CHIIKIBETSU_GYOUSHU 
	   ON M_CHIIKIBETSU_GYOUSHU.DELETE_FLG = 0
	  AND M_CHIIKIBETSU_GYOUSHU.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_GYOUSHU.GYOUSHU_CD = MCG.GYOUSHU_CD
   WHERE MCG.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCG ON M_CHIIKIBETSU_GYOUSHU.CHIIKI_CD = KIZON_DATA_MCG.CHIIKI_CD
AND M_CHIIKIBETSU_GYOUSHU.GYOUSHU_CD = KIZON_DATA_MCG.GYOUSHU_CD

DELETE M_CHIIKIBETSU_GYOUSHU 
FROM M_CHIIKIBETSU_GYOUSHU INNER JOIN (
   SELECT 
      M_CHIIKIBETSU_GYOUSHU.CHIIKI_CD,
	  M_CHIIKIBETSU_GYOUSHU.GYOUSHU_CD
   FROM M_CHIIKIBETSU_GYOUSHU MCG
   INNER JOIN  M_CHIIKIBETSU_GYOUSHU 
	   ON M_CHIIKIBETSU_GYOUSHU.DELETE_FLG = 1
	  AND M_CHIIKIBETSU_GYOUSHU.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_GYOUSHU.GYOUSHU_CD = MCG.GYOUSHU_CD
   WHERE MCG.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCG ON M_CHIIKIBETSU_GYOUSHU.CHIIKI_CD = KIZON_DATA_MCG.CHIIKI_CD
AND M_CHIIKIBETSU_GYOUSHU.GYOUSHU_CD = KIZON_DATA_MCG.GYOUSHU_CD

--地域別施設マスタ
DELETE M_CHIIKIBETSU_SHISETSU 
FROM M_CHIIKIBETSU_SHISETSU INNER JOIN (
   SELECT 
      MCS.CHIIKI_CD,
	  MCS.SHOBUN_HOUHOU_CD
   FROM M_CHIIKIBETSU_SHISETSU MCS
   INNER JOIN  M_CHIIKIBETSU_SHISETSU 
	   ON M_CHIIKIBETSU_SHISETSU.DELETE_FLG = 0
	  AND M_CHIIKIBETSU_SHISETSU.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_SHISETSU.SHOBUN_HOUHOU_CD = MCS.SHOBUN_HOUHOU_CD
   WHERE MCS.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCS ON M_CHIIKIBETSU_SHISETSU.CHIIKI_CD = KIZON_DATA_MCS.CHIIKI_CD
AND M_CHIIKIBETSU_SHISETSU.SHOBUN_HOUHOU_CD = KIZON_DATA_MCS.SHOBUN_HOUHOU_CD

DELETE M_CHIIKIBETSU_SHISETSU 
FROM M_CHIIKIBETSU_SHISETSU INNER JOIN (
   SELECT 
      M_CHIIKIBETSU_SHISETSU.CHIIKI_CD,
	  M_CHIIKIBETSU_SHISETSU.SHOBUN_HOUHOU_CD
   FROM M_CHIIKIBETSU_SHISETSU MCS
   INNER JOIN  M_CHIIKIBETSU_SHISETSU 
	   ON M_CHIIKIBETSU_SHISETSU.DELETE_FLG = 1
	  AND M_CHIIKIBETSU_SHISETSU.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_SHISETSU.SHOBUN_HOUHOU_CD = MCS.SHOBUN_HOUHOU_CD
   WHERE MCS.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCS ON M_CHIIKIBETSU_SHISETSU.CHIIKI_CD = KIZON_DATA_MCS.CHIIKI_CD
AND M_CHIIKIBETSU_SHISETSU.SHOBUN_HOUHOU_CD = KIZON_DATA_MCS.SHOBUN_HOUHOU_CD

--地域別住所マスタ
DELETE M_CHIIKIBETSU_JUUSHO 
FROM M_CHIIKIBETSU_JUUSHO INNER JOIN (
   SELECT 
      MCJ.CHIIKI_CD,
	  MCJ.CHANGE_CHIIKI_CD
   FROM M_CHIIKIBETSU_JUUSHO MCJ
   INNER JOIN  M_CHIIKIBETSU_JUUSHO 
	   ON M_CHIIKIBETSU_JUUSHO.DELETE_FLG = 0
	  AND M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = MCJ.CHANGE_CHIIKI_CD
   WHERE MCJ.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCJ ON M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = KIZON_DATA_MCJ.CHIIKI_CD
AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = KIZON_DATA_MCJ.CHANGE_CHIIKI_CD

DELETE M_CHIIKIBETSU_JUUSHO 
FROM M_CHIIKIBETSU_JUUSHO INNER JOIN (
   SELECT 
      M_CHIIKIBETSU_JUUSHO.CHIIKI_CD,
	  M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD
   FROM M_CHIIKIBETSU_JUUSHO MCJ
   INNER JOIN  M_CHIIKIBETSU_JUUSHO 
	   ON M_CHIIKIBETSU_JUUSHO.DELETE_FLG = 1
	  AND M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = MCJ.CHANGE_CHIIKI_CD
   WHERE MCJ.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCJ ON M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = KIZON_DATA_MCJ.CHIIKI_CD
AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = KIZON_DATA_MCJ.CHANGE_CHIIKI_CD 

UPDATE M_CHIIKIBETSU_JUUSHO SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'

DELETE M_CHIIKIBETSU_JUUSHO 
FROM M_CHIIKIBETSU_JUUSHO INNER JOIN (
   SELECT 
      MCJ.CHIIKI_CD,
	  MCJ.CHANGE_CHIIKI_CD
   FROM M_CHIIKIBETSU_JUUSHO MCJ
   INNER JOIN  M_CHIIKIBETSU_JUUSHO 
	   ON M_CHIIKIBETSU_JUUSHO.DELETE_FLG = 0
	  AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = MCJ.CHIIKI_CD
   WHERE MCJ.CHANGE_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCJ ON M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = KIZON_DATA_MCJ.CHIIKI_CD
AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = KIZON_DATA_MCJ.CHANGE_CHIIKI_CD

DELETE M_CHIIKIBETSU_JUUSHO 
FROM M_CHIIKIBETSU_JUUSHO INNER JOIN (
   SELECT 
      M_CHIIKIBETSU_JUUSHO.CHIIKI_CD,
	  M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD
   FROM M_CHIIKIBETSU_JUUSHO MCJ
   INNER JOIN  M_CHIIKIBETSU_JUUSHO 
	   ON M_CHIIKIBETSU_JUUSHO.DELETE_FLG = 1
	  AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = MCJ.CHIIKI_CD
   WHERE MCJ.CHANGE_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCJ ON M_CHIIKIBETSU_JUUSHO.CHIIKI_CD = KIZON_DATA_MCJ.CHIIKI_CD
AND M_CHIIKIBETSU_JUUSHO.CHANGE_CHIIKI_CD = KIZON_DATA_MCJ.CHANGE_CHIIKI_CD

UPDATE M_CHIIKIBETSU_JUUSHO SET CHANGE_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHANGE_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'

--地域別処分マスタ
DELETE M_CHIIKIBETSU_SHOBUN 
FROM M_CHIIKIBETSU_SHOBUN INNER JOIN (
   SELECT 
      MCSBN.CHIIKI_CD,
	  MCSBN.SHOBUN_HOUHOU_CD
   FROM M_CHIIKIBETSU_SHOBUN MCSBN
   INNER JOIN  M_CHIIKIBETSU_SHOBUN 
	   ON M_CHIIKIBETSU_SHOBUN.DELETE_FLG = 0
	  AND M_CHIIKIBETSU_SHOBUN.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD = MCSBN.SHOBUN_HOUHOU_CD
   WHERE MCSBN.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCSBN ON M_CHIIKIBETSU_SHOBUN.CHIIKI_CD = KIZON_DATA_MCSBN.CHIIKI_CD
AND M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD = KIZON_DATA_MCSBN.SHOBUN_HOUHOU_CD

DELETE M_CHIIKIBETSU_SHOBUN 
FROM M_CHIIKIBETSU_SHOBUN INNER JOIN (
   SELECT 
      M_CHIIKIBETSU_SHOBUN.CHIIKI_CD,
	  M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD
   FROM M_CHIIKIBETSU_SHOBUN MCSBN
   INNER JOIN  M_CHIIKIBETSU_SHOBUN 
	   ON M_CHIIKIBETSU_SHOBUN.DELETE_FLG = 1
	  AND M_CHIIKIBETSU_SHOBUN.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD = MCSBN.SHOBUN_HOUHOU_CD
   WHERE MCSBN.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCSBN ON M_CHIIKIBETSU_SHOBUN.CHIIKI_CD = KIZON_DATA_MCSBN.CHIIKI_CD
AND M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD = KIZON_DATA_MCSBN.SHOBUN_HOUHOU_CD

--地域別分類マスタ
DELETE M_CHIIKIBETSU_BUNRUI 
FROM M_CHIIKIBETSU_BUNRUI INNER JOIN (
   SELECT 
      MCB.CHIIKI_CD,
	  MCB.HOUKOKUSHO_BUNRUI_CD
   FROM M_CHIIKIBETSU_BUNRUI MCB
   INNER JOIN  M_CHIIKIBETSU_BUNRUI 
	   ON M_CHIIKIBETSU_BUNRUI.DELETE_FLG = 0
	  AND M_CHIIKIBETSU_BUNRUI.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_BUNRUI.HOUKOKUSHO_BUNRUI_CD = MCB.HOUKOKUSHO_BUNRUI_CD
   WHERE MCB.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCB ON M_CHIIKIBETSU_BUNRUI.CHIIKI_CD = KIZON_DATA_MCB.CHIIKI_CD
AND M_CHIIKIBETSU_BUNRUI.HOUKOKUSHO_BUNRUI_CD = KIZON_DATA_MCB.HOUKOKUSHO_BUNRUI_CD

DELETE M_CHIIKIBETSU_BUNRUI 
FROM M_CHIIKIBETSU_BUNRUI INNER JOIN (
   SELECT 
      M_CHIIKIBETSU_BUNRUI.CHIIKI_CD,
	  M_CHIIKIBETSU_BUNRUI.HOUKOKUSHO_BUNRUI_CD
   FROM M_CHIIKIBETSU_BUNRUI MCB
   INNER JOIN  M_CHIIKIBETSU_BUNRUI 
	   ON M_CHIIKIBETSU_BUNRUI.DELETE_FLG = 1
	  AND M_CHIIKIBETSU_BUNRUI.CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000'
	  AND M_CHIIKIBETSU_BUNRUI.HOUKOKUSHO_BUNRUI_CD = MCB.HOUKOKUSHO_BUNRUI_CD
   WHERE MCB.CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
) KIZON_DATA_MCB ON M_CHIIKIBETSU_BUNRUI.CHIIKI_CD = KIZON_DATA_MCB.CHIIKI_CD
AND M_CHIIKIBETSU_BUNRUI.HOUKOKUSHO_BUNRUI_CD = KIZON_DATA_MCB.HOUKOKUSHO_BUNRUI_CD

--地域別許可番号マスタ
UPDATE M_CHIIKIBETSU_KYOKA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 
UPDATE M_CHIIKIBETSU_KYOKA_MEIGARA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'

--地域別業種マスタ
UPDATE M_CHIIKIBETSU_GYOUSHU SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--地域別施設マスタ
UPDATE M_CHIIKIBETSU_SHISETSU SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--地域別処分マスタ
UPDATE M_CHIIKIBETSU_SHOBUN SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--地域別分類マスタ
UPDATE M_CHIIKIBETSU_BUNRUI SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--委託運搬許可書マスタ
UPDATE M_ITAKU_UPN_KYOKASHO SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'

--委託処分許可書マスタ
UPDATE M_ITAKU_SBN_KYOKASHO SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'

--仮業者マスタ
UPDATE M_KARI_GYOUSHA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--仮現場マスタ
UPDATE M_KARI_GENBA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--業者マスタ
UPDATE M_GYOUSHA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--現場マスタ
UPDATE M_GENBA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--引合業者マスタ
UPDATE M_HIKIAI_GYOUSHA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--引合現場マスタ
UPDATE M_HIKIAI_GENBA SET CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 

--実績報告書
UPDATE T_JISSEKI_HOUKOKU_ENTRY SET TEISHUTSU_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE TEISHUTSU_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000' 
UPDATE T_JISSEKI_HOUKOKU_SBN_DETAIL SET TEISHUTSUSAKI_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE TEISHUTSUSAKI_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_SBN_DETAIL SET HST_GENBA_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE HST_GENBA_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_SBN_DETAIL SET SBN_GENBA_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE SBN_GENBA_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_SBN_DETAIL SET ITAKUSAKI_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE ITAKUSAKI_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_SBN_DETAIL SET HST_JOU_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE HST_JOU_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_SHORI_DETAIL SET TEISHUTSUSAKI_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE TEISHUTSUSAKI_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_SHORI_DETAIL SET HST_JOU_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE HST_JOU_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_UPN_DETAIL SET TEISHUTSUSAKI_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE TEISHUTSUSAKI_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_UPN_DETAIL SET HST_GENBA_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE HST_GENBA_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_UPN_DETAIL SET SBN_GENBA_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE SBN_GENBA_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_UPN_DETAIL SET HIKIWATASHISAKI_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE HIKIWATASHISAKI_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_UPN_DETAIL SET HST_JOU_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE HST_JOU_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
UPDATE T_JISSEKI_HOUKOKU_UPN_DETAIL SET UPNSAKI_JOU_CHIIKI_CD = /*CHIIKI_CD_NEW*/'000000' WHERE UPNSAKI_JOU_CHIIKI_CD = /*CHIIKI_CD_OLD*/'000000'
/*END*/
