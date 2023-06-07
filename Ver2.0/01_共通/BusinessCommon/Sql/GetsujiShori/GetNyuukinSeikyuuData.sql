﻿SELECT
T_N.SYSTEM_ID,
T_N.SEQ,
T_N.NYUUKIN_AMOUNT_TOTAL,
T_N.CHOUSEI_AMOUNT_TOTAL,
T_N.NYUUKIN_NUMBER,
T_N.DENPYOU_DATE,
T_N.TORIHIKISAKI_CD,
T_N.NYUUKINSAKI_CD,

T_N_D.DETAIL_SYSTEM_ID,
T_N_D.NYUUSHUKKIN_KBN_CD,
T_N_D.KINGAKU,
T_N_D.MEISAI_BIKOU,

M_KYOTEN.KYOTEN_NAME,
M_KYOTEN.KYOTEN_DAIHYOU,
M_KYOTEN.KYOTEN_POST,
M_KYOTEN.KYOTEN_ADDRESS1,
M_KYOTEN.KYOTEN_ADDRESS2,
M_KYOTEN.KYOTEN_TEL,
M_KYOTEN.KYOTEN_FAX,

M_T.SEIKYUU_DAIHYOU_PRINT_KBN,
M_T.SEIKYUU_SOUFU_NAME1,
M_T.SEIKYUU_SOUFU_NAME2,
M_T.SEIKYUU_SOUFU_KEISHOU1,
M_T.SEIKYUU_SOUFU_KEISHOU2,
M_T.SEIKYUU_SOUFU_POST,
M_T.SEIKYUU_SOUFU_ADDRESS1,
M_T.SEIKYUU_SOUFU_ADDRESS2,
M_T.SEIKYUU_KYOTEN_PRINT_KBN,
M_T.SEIKYUU_KYOTEN_CD,
M_T.SEIKYUU_SOUFU_BUSHO,
M_T.SEIKYUU_SOUFU_TANTOU,
M_T.SEIKYUU_SOUFU_TEL,
M_T.SEIKYUU_SOUFU_FAX,
M_T.SEIKYUU_TANTOU,
M_T.SHOSHIKI_KBN,

M_TORIHIKISAKI_KYOTEN.KYOTEN_NAME AS SEIKYUU_KYOTEN_NAME,
M_TORIHIKISAKI_KYOTEN.KYOTEN_DAIHYOU AS SEIKYUU_KYOTEN_DAIHYOU,
M_TORIHIKISAKI_KYOTEN.KYOTEN_POST AS SEIKYUU_KYOTEN_POST,
M_TORIHIKISAKI_KYOTEN.KYOTEN_ADDRESS1 AS SEIKYUU_KYOTEN_ADDRESS1,
M_TORIHIKISAKI_KYOTEN.KYOTEN_ADDRESS2 AS SEIKYUU_KYOTEN_ADDRESS2,
M_TORIHIKISAKI_KYOTEN.KYOTEN_TEL AS SEIKYUU_KYOTEN_TEL,
M_TORIHIKISAKI_KYOTEN.KYOTEN_FAX AS SEIKYUU_KYOTEN_FAX,

M_C.CORP_NAME,
M_C.CORP_DAIHYOU,

M_N.NYUUSHUKKIN_KBN_NAME

FROM
T_NYUUKIN_ENTRY T_N
LEFT JOIN T_NYUUKIN_DETAIL T_N_D
ON T_N.SYSTEM_ID = T_N_D.SYSTEM_ID 
AND T_N.SEQ = T_N_D.SEQ 
LEFT OUTER JOIN M_KYOTEN
ON M_KYOTEN.KYOTEN_CD = T_N.KYOTEN_CD
LEFT JOIN M_TORIHIKISAKI_SEIKYUU M_T
ON M_T.TORIHIKISAKI_CD = /*SEIKYU_CD*/ 
LEFT OUTER JOIN M_KYOTEN AS M_TORIHIKISAKI_KYOTEN
ON ISNULL(M_T.SEIKYUU_KYOTEN_CD,'00') = M_TORIHIKISAKI_KYOTEN.KYOTEN_CD
LEFT JOIN M_CORP_INFO M_C
ON M_C.SYS_ID = 0
LEFT JOIN M_NYUUSHUKKIN_KBN M_N
ON M_N.NYUUSHUKKIN_KBN_CD = T_N_D.NYUUSHUKKIN_KBN_CD

WHERE
T_N.TORIHIKISAKI_CD = /*SEIKYU_CD*/'000001'
/*IF SEIKYUSHIMEBI_FROM != null && SEIKYUSHIMEBI_FROM != ''*/
AND CONVERT(DateTime,/*SEIKYUSHIMEBI_FROM*/null, 111) <= T_N.DENPYOU_DATE/*END*/
AND T_N.DENPYOU_DATE <= CONVERT(DateTime,/*SEIKYUSHIMEBI_TO*/null, 111)
AND T_N.DELETE_FLG = 0