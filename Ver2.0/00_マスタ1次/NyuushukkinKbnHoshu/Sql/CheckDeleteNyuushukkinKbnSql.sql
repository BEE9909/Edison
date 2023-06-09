﻿SELECT DISTINCT N'取引先マスタ' AS NAME FROM M_TORIHIKISAKI AS T
 INNER JOIN M_TORIHIKISAKI_SEIKYUU AS TSE ON T.TORIHIKISAKI_CD = TSE.TORIHIKISAKI_CD
 INNER JOIN M_TORIHIKISAKI_SHIHARAI AS TSH ON T.TORIHIKISAKI_CD = TSH.TORIHIKISAKI_CD
 WHERE (TSE.KAISHUU_HOUHOU IN /*NYUUSHUKKIN_KBN_CD*/('') OR TSH.SHIHARAI_HOUHOU IN /*NYUUSHUKKIN_KBN_CD*/('')) AND T.DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'引合取引先マスタ' AS NAME FROM M_HIKIAI_TORIHIKISAKI AS T
 INNER JOIN M_HIKIAI_TORIHIKISAKI_SEIKYUU AS TSE ON T.TORIHIKISAKI_CD = TSE.TORIHIKISAKI_CD
 INNER JOIN M_HIKIAI_TORIHIKISAKI_SHIHARAI AS TSH ON T.TORIHIKISAKI_CD = TSH.TORIHIKISAKI_CD
 WHERE (TSE.KAISHUU_HOUHOU IN /*NYUUSHUKKIN_KBN_CD*/('') OR TSH.SHIHARAI_HOUHOU IN /*NYUUSHUKKIN_KBN_CD*/('')) AND T.DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'自社情報マスタ' AS NAME FROM M_CORP_INFO WHERE SHIHARAI_HOUHOU IN /*NYUUSHUKKIN_KBN_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'システム設定マスタ' AS NAME FROM M_SYS_INFO WHERE (SEIKYUU_KAISHUU_HOUHOU IN /*NYUUSHUKKIN_KBN_CD*/('') OR SHIHARAI_HOUHOU IN /*NYUUSHUKKIN_KBN_CD*/('') OR NYUUKIN_NYUUSHUKKIN_KBN_CD IN /*NYUUSHUKKIN_KBN_CD*/(''))
