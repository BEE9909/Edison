﻿SELECT DISTINCT '見積マスタ' AS NAME FROM T_MITSUMORI_ENTRY WHERE (MOD_URIAGE_GURUUPU_CD IN /*GURUUPU_CD*/('') OR MOD_SHIHARAI_GURUUPU_CD IN /*GURUUPU_CD*/(''))
 AND DELETE_FLG = 'False' 