﻿INSERT INTO M_TORIHIKISAKI 
(
TORIHIKISAKI_CD
,DELETE_FLG
,TORIHIKISAKI_KYOTEN_CD
,TORIHIKISAKI_NAME1
,TORIHIKISAKI_NAME2
,TORIHIKISAKI_NAME_RYAKU
,TORIHIKISAKI_FURIGANA
,TORIHIKISAKI_TEL
,TORIHIKISAKI_FAX
,EIGYOU_TANTOU_BUSHO_CD
,EIGYOU_TANTOU_CD
,TORIHIKISAKI_POST
,TORIHIKISAKI_TODOUFUKEN_CD
,TORIHIKISAKI_ADDRESS1
,TORIHIKISAKI_ADDRESS2
,TORIHIKI_JOUKYOU
,CHUUSHI_RIYUU1
,CHUUSHI_RIYUU2
,BUSHO
,TANTOUSHA
,SHUUKEI_ITEM_CD
,GYOUSHU_CD
,BIKOU1
,BIKOU2
,BIKOU3
,BIKOU4
,NYUUKINSAKI_KBN
,DAIHYOU_PRINT_KBN
,MANI_HENSOUSAKI_KBN
,SHOKUCHI_KBN
,MANI_HENSOUSAKI_NAME1
,MANI_HENSOUSAKI_NAME2
,MANI_HENSOUSAKI_KEISHOU1
,MANI_HENSOUSAKI_KEISHOU2
,MANI_HENSOUSAKI_POST
,MANI_HENSOUSAKI_ADDRESS1
,MANI_HENSOUSAKI_ADDRESS2
,MANI_HENSOUSAKI_BUSHO
,MANI_HENSOUSAKI_TANTOU
,TEKIYOU_BEGIN
,TEKIYOU_END
,CREATE_USER
,CREATE_DATE
,CREATE_PC
,UPDATE_USER
,UPDATE_DATE
,UPDATE_PC
,TORIHIKISAKI_KEISHOU1
,TORIHIKISAKI_KEISHOU2
,MANI_HENSOUSAKI_THIS_ADDRESS_KBN
)
SELECT
/*newTORIHIKISAKI_CD*/0
,0
,TORIHIKISAKI_KYOTEN_CD
,TORIHIKISAKI_NAME1
,TORIHIKISAKI_NAME2
,TORIHIKISAKI_NAME_RYAKU
,TORIHIKISAKI_FURIGANA
,TORIHIKISAKI_TEL
,TORIHIKISAKI_FAX
,EIGYOU_TANTOU_BUSHO_CD
,EIGYOU_TANTOU_CD
,TORIHIKISAKI_POST
,TORIHIKISAKI_TODOUFUKEN_CD
,TORIHIKISAKI_ADDRESS1
,TORIHIKISAKI_ADDRESS2
,TORIHIKI_JOUKYOU
,CHUUSHI_RIYUU1
,CHUUSHI_RIYUU2
,BUSHO
,TANTOUSHA
,SHUUKEI_ITEM_CD
,GYOUSHU_CD
,BIKOU1
,BIKOU2
,BIKOU3
,BIKOU4
,NYUUKINSAKI_KBN
,DAIHYOU_PRINT_KBN
,MANI_HENSOUSAKI_KBN
,SHOKUCHI_KBN
,MANI_HENSOUSAKI_NAME1
,MANI_HENSOUSAKI_NAME2
,MANI_HENSOUSAKI_KEISHOU1
,MANI_HENSOUSAKI_KEISHOU2
,MANI_HENSOUSAKI_POST
,MANI_HENSOUSAKI_ADDRESS1
,MANI_HENSOUSAKI_ADDRESS2
,MANI_HENSOUSAKI_BUSHO
,MANI_HENSOUSAKI_TANTOU
,TEKIYOU_BEGIN
,TEKIYOU_END
,/*CREATE_USER*/''
,/*CREATE_DATE*/''
,/*CREATE_PC*/''
,UPDATE_USER
,UPDATE_DATE
,UPDATE_PC
,TORIHIKISAKI_KEISHOU1
,TORIHIKISAKI_KEISHOU2
,MANI_HENSOUSAKI_THIS_ADDRESS_KBN
FROM M_HIKIAI_TORIHIKISAKI 
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0