﻿SELECT 
SYSTEM_ID,
SEQ,
KYOTEN_CD,
PEGE_TOTAL,
JOKYO_FLG,
SINKOU_DATE,
JUCHU_DATE,
SICHU_DATE,
MITSUMORI_NUMBER,
MITSUMORI_DATE,
INJI_KYOTEN1_CD,
INJI_KYOTEN2_CD,
HIKIAI_TORIHIKISAKI_FLG,
TORIHIKISAKI_CD,
TORIHIKISAKI_NAME,
TORIHIKISAKI_INJI,
HIKIAI_GYOUSHA_FLG,
GYOUSHA_CD,
GYOUSHA_NAME,
GYOUSHA_INJI,
HIKIAI_GENBA_FLG,
GENBA_CD,
GENBA_NAME,
GENBA_INJI,
SHAIN_CD,
SHAIN_NAME,
KEISHOU,
KENMEI,
MITSUMORI_1,
MITSUMORI_2,
MITSUMORI_3,
MITSUMORI_4,
BIKOU_1,
BIKOU_2,
BIKOU_3,
BIKOU_4,
BIKOU_5,
MITSUMORI_INJI_DATE,
SHANAI_BIKOU,
ZEI_KEISAN_KBN_CD,
ZEI_KBN_CD,
KINGAKU_TOTAL,
SHOUHIZEI_RATE,
TAX_SOTO,
TAX_UCHI,
TAX_SOTO_TOTAL,
TAX_UCHI_TOTAL,
SHOUHIZEI_TOTAL,
GOUKEI_KINGAKU_TOTAL,
CREATE_USER,
CREATE_DATE,
CREATE_PC,
UPDATE_USER,
UPDATE_DATE,
UPDATE_PC,
DELETE_FLG,
MOD_URIAGE_GURUUPU_CD,
MOD_URIAGE_GURUUPU_NAME,
MOD_SHIHARAI_GURUUPU_CD,
MOD_SHIHARAI_GURUUPU_NAME
FROM 
T_MITSUMORI_ENTRY
/*BEGIN*/WHERE
/*IF !deletechuFlg*/ DELETE_FLG = 0/*END*/
/*IF data.MITSUMORI_NUMBER != null*/
AND MITSUMORI_NUMBER = /*data.MITSUMORI_NUMBER.Value*//*END*/
/*END*/