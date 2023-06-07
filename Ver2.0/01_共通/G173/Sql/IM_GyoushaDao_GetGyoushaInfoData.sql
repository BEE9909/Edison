﻿SELECT
	--ヘッダ
	T1.TORIHIKISAKI_UMU_KBN AS TORIHIKISAKI_UMU_KBN,
	T1.GYOUSHAKBN_UKEIRE AS GYOUSHAKBN_UKEIRE,
	T1.GYOUSHAKBN_SHUKKA AS GYOUSHAKBN_SHUKKA,
	T1.GYOUSHAKBN_MANI AS GYOUSHAKBN_MANI,
	T1.TORIHIKISAKI_CD AS TORIHIKISAKI_CD,
	T6.TORIHIKISAKI_NAME1 AS TORIHIKISAKI_NAME1,
	T6.TORIHIKISAKI_NAME2 AS TORIHIKISAKI_NAME2,
	T6.TEKIYOU_BEGIN AS TORIHIKISAKI_TEKIYOU_BEGIN,
	T6.TEKIYOU_END AS TORIHIKISAKI_TEKIYOU_END,
	T1.KYOTEN_CD AS KYOTEN_CD,
	T7.KYOTEN_NAME_RYAKU AS KYOTEN_NAME,
	T1.GYOUSHA_CD AS GYOUSHA_CD,
	T1.GYOUSHA_FURIGANA AS GYOUSHA_FURIGANA,
	T1.GYOUSHA_NAME1 AS GYOUSHA_NAME1,
	T1.GYOUSHA_KEISHOU1 AS GYOUSHA_KEISHOU1,
	T1.GYOUSHA_NAME2 AS GYOUSHA_NAME2,
	T1.GYOUSHA_KEISHOU2 AS GYOUSHA_KEISHOU2,
	T1.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU,
	T1.GYOUSHA_TEL AS GYOUSHA_TEL,
	T1.GYOUSHA_KEITAI_TEL AS GYOUSHA_KEITAI_TEL,
	T1.GYOUSHA_FAX AS GYOUSHA_FAX,
	T1.EIGYOU_TANTOU_BUSHO_CD AS EIGYOU_TANTOU_BUSHO_CD,
	T8.BUSHO_NAME_RYAKU AS BUSHO_NAME,
	T1.EIGYOU_TANTOU_CD AS EIGYOU_TANTOU_CD,
	T9.SHAIN_NAME_RYAKU AS SHAIN_NAME,
	T1.TEKIYOU_BEGIN AS TEKIYOU_BEGIN,
	T1.TEKIYOU_END AS TEKIYOU_END,
	T1.CHUUSHI_RIYUU1 AS CHUUSHI_RIYUU1,
	T1.CHUUSHI_RIYUU2 AS CHUUSHI_RIYUU2,
	T1.SHOKUCHI_KBN AS SHOKUCHI_KBN,
	T1.JISHA_KBN AS JISHA_KBN,
	T1.CREATE_USER AS CREATE_USER,
	T1.CREATE_DATE AS CREATE_DATE,
	T1.UPDATE_USER AS UPDATE_USER,
	T1.UPDATE_DATE AS UPDATE_DATE,
	--基本 
	T1.GYOUSHA_POST AS GYOUSHA_POST,
	T1.GYOUSHA_TODOUFUKEN_CD AS GYOUSHA_TODOUFUKEN_CD,
	T2.TODOUFUKEN_NAME AS TODOUFUKEN_NAME,
	T2.TODOUFUKEN_NAME_RYAKU AS TODOUFUKEN_NAME_RYAKU,
	T1.GYOUSHA_ADDRESS1 AS GYOUSHA_ADDRESS1,
	T1.GYOUSHA_ADDRESS2 AS GYOUSHA_ADDRESS2,
	T1.CHIIKI_CD AS CHIIKI_CD,
	T3.CHIIKI_NAME AS CHIIKI_NAME,
	T3.CHIIKI_NAME_RYAKU AS CHIIKI_NAME_RYAKU,
	T1.BUSHO AS BUSHO,
	T1.TANTOUSHA AS TANTOUSHA,
	T1.GYOUSHA_DAIHYOU AS GYOUSHA_DAIHYOU,
	T1.SHUUKEI_ITEM_CD AS SHUUKEI_ITEM_CD,
	T4.SHUUKEI_KOUMOKU_NAME AS SHUUKEI_KOUMOKU_NAME,
	T4.SHUUKEI_KOUMOKU_NAME_RYAKU AS SHUUKEI_KOUMOKU_NAME_RYAKU,
	T1.GYOUSHU_CD AS GYOUSHU_CD,
	T5.GYOUSHU_NAME AS GYOUSHU_NAME,
	T5.GYOUSHU_NAME_RYAKU AS GYOUSHU_NAME_RYAKU,
	T1.BIKOU1 AS BIKOU1,
	T1.BIKOU2 AS BIKOU2,
	T1.BIKOU3 AS BIKOU3,
	T1.BIKOU4 AS BIKOU4,
	--請求
	T1.SEIKYUU_SOUFU_NAME1 AS SEIKYUU_SOUFU_NAME1,
	T1.SEIKYUU_SOUFU_KEISHOU1 AS SEIKYUU_SOUFU_KEISHOU1,
	T1.SEIKYUU_SOUFU_NAME2 AS SEIKYUU_SOUFU_NAME2,
	T1.SEIKYUU_SOUFU_KEISHOU2 AS SEIKYUU_SOUFU_KEISHOU2,
	T1.SEIKYUU_SOUFU_POST AS SEIKYUU_SOUFU_POST,
	T1.SEIKYUU_SOUFU_ADDRESS1 AS SEIKYUU_SOUFU_ADDRESS1,
	T1.SEIKYUU_SOUFU_ADDRESS2 AS SEIKYUU_SOUFU_ADDRESS2,
	T1.SEIKYUU_SOUFU_BUSHO AS SEIKYUU_SOUFU_BUSHO,
	T1.SEIKYUU_SOUFU_TANTOU AS SEIKYUU_SOUFU_TANTOU,
	T1.SEIKYUU_SOUFU_TEL AS SEIKYUU_SOUFU_TEL,
	T1.SEIKYUU_SOUFU_FAX AS SEIKYUU_SOUFU_FAX,
	T1.SEIKYUU_TANTOU AS SEIKYUU_TANTOU,
	T1.SEIKYUU_DAIHYOU_PRINT_KBN AS SEIKYUU_DAIHYOU_PRINT_KBN,
	T1.SEIKYUU_KYOTEN_PRINT_KBN AS SEIKYUU_KYOTEN_PRINT_KBN,
	T1.SEIKYUU_KYOTEN_CD AS SEIKYUU_KYOTEN_CD,
	TA.KYOTEN_NAME_RYAKU AS SEIKYUU_KYOTEN_NAME,
	T1.HAKKOUSAKI_CD AS SEIKYUU_HAKKOUSAKI_CD,
	--支払
	T1.SHIHARAI_SOUFU_NAME1 AS SHIHARAI_SOUFU_NAME1,
	T1.SHIHARAI_SOUFU_KEISHOU1 AS SHIHARAI_SOUFU_KEISHOU1,
	T1.SHIHARAI_SOUFU_NAME2 AS SHIHARAI_SOUFU_NAME2,
	T1.SHIHARAI_SOUFU_KEISHOU2 AS SHIHARAI_SOUFU_KEISHOU2,
	T1.SHIHARAI_SOUFU_POST AS SHIHARAI_SOUFU_POST,
	T1.SHIHARAI_SOUFU_ADDRESS1 AS SHIHARAI_SOUFU_ADDRESS1,
	T1.SHIHARAI_SOUFU_ADDRESS2 AS SHIHARAI_SOUFU_ADDRESS2,
	T1.SHIHARAI_SOUFU_BUSHO AS SHIHARAI_SOUFU_BUSHO,
	T1.SHIHARAI_SOUFU_TANTOU AS SHIHARAI_SOUFU_TANTOU,
	T1.SHIHARAI_SOUFU_TEL AS SHIHARAI_SOUFU_TEL,
	T1.SHIHARAI_SOUFU_FAX AS SHIHARAI_SOUFU_FAX,
	T1.SHIHARAI_KYOTEN_PRINT_KBN AS SHIHARAI_KYOTEN_PRINT_KBN,
	T1.SHIHARAI_KYOTEN_CD AS SHIHARAI_KYOTEN_CD,
	TB.KYOTEN_NAME_RYAKU AS SHIHARAI_KYOTEN_NAME,
	--分類
	T1.HAISHUTSU_NIZUMI_GYOUSHA_KBN AS HAISHUTSU_NIZUMI_GYOUSHA_KBN,
	T1.UNPAN_JUTAKUSHA_KAISHA_KBN AS UNPAN_JUTAKUSHA_KAISHA_KBN,
	T1.SHOBUN_NIOROSHI_GYOUSHA_KBN AS SHOBUN_NIOROSHI_GYOUSHA_KBN,
	T1.MANI_HENSOUSAKI_KBN AS MANI_HENSOUSAKI_KBN,
	T1.MANI_HENSOUSAKI_NAME1 AS MANI_HENSOUSAKI_NAME1,
	T1.MANI_HENSOUSAKI_KEISHOU1 AS MANI_HENSOUSAKI_KEISHOU1,
	T1.MANI_HENSOUSAKI_NAME2 AS MANI_HENSOUSAKI_NAME2,
	T1.MANI_HENSOUSAKI_KEISHOU2 AS MANI_HENSOUSAKI_KEISHOU2,
	T1.MANI_HENSOUSAKI_POST AS MANI_HENSOUSAKI_POST,
	T1.MANI_HENSOUSAKI_ADDRESS1 AS MANI_HENSOUSAKI_ADDRESS1,
	T1.MANI_HENSOUSAKI_ADDRESS2 AS MANI_HENSOUSAKI_ADDRESS2,
	T1.MANI_HENSOUSAKI_BUSHO AS MANI_HENSOUSAKI_BUSHO,
	T1.MANI_HENSOUSAKI_TANTOU AS MANI_HENSOUSAKI_TANTOU,
	T1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD,
	TC.CHIIKI_NAME AS UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME,
	TC.CHIIKI_NAME_RYAKU AS UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_RYAKU,
	T1.MANI_HENSOUSAKI_THIS_ADDRESS_KBN AS MANI_HENSOUSAKI_THIS_ADDRESS_KBN

FROM dbo.M_GYOUSHA AS T1 
LEFT JOIN dbo.M_TODOUFUKEN AS T2 ON T1.GYOUSHA_TODOUFUKEN_CD = T2.TODOUFUKEN_CD 
LEFT JOIN dbo.M_CHIIKI AS T3 ON T1.CHIIKI_CD = T3.CHIIKI_CD 
LEFT JOIN dbo.M_SHUUKEI_KOUMOKU AS T4 ON T1.SHUUKEI_ITEM_CD = T4.SHUUKEI_KOUMOKU_CD 
LEFT JOIN dbo.M_GYOUSHU AS T5 ON T1.GYOUSHU_CD = T5.GYOUSHU_CD 
LEFT JOIN dbo.M_TORIHIKISAKI AS T6 ON T1.TORIHIKISAKI_CD = T6.TORIHIKISAKI_CD 
LEFT JOIN dbo.M_KYOTEN AS T7 ON T1.KYOTEN_CD = T7.KYOTEN_CD 
LEFT JOIN dbo.M_BUSHO AS T8 ON T1.EIGYOU_TANTOU_BUSHO_CD = T8.BUSHO_CD 
LEFT JOIN dbo.M_SHAIN AS T9 ON T1.EIGYOU_TANTOU_CD = T9.SHAIN_CD 
LEFT JOIN dbo.M_KYOTEN AS TA ON T1.SEIKYUU_KYOTEN_CD = TA.KYOTEN_CD 
LEFT JOIN dbo.M_KYOTEN AS TB ON T1.SHIHARAI_KYOTEN_CD = TB.KYOTEN_CD 
LEFT JOIN dbo.M_CHIIKI AS TC ON T1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TC.CHIIKI_CD 

WHERE 
1=1
--/*IF !torihikisakiCd.IsNull && ''!=torihikisakiCd*/AND T1.TORIHIKISAKI_CD = /*torihikisakiCd*/0 /*END*/
/*IF !gyoushaCd.IsNull && ''!=gyoushaCd*/AND T1.GYOUSHA_CD = /*gyoushaCd*/0 /*END*/
