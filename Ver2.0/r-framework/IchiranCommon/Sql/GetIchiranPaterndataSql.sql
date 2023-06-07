﻿SELECT
MOP.SYSTEM_ID AS SYSTEM_ID_MOP,
MOP.SEQ AS SEQ_MOP,
MOP.OUTPUT_KBN,
MOP.PATTERN_NAME,
MOP.DENSHU_KBN_CD,
MOP.DELETE_FLG,
CAST(MOP.TIME_STAMP AS int) AS TIME_STAMP_MOP,
MOPK.SYSTEM_ID AS SYSTEM_ID_MOPK,
MOPK.SEQ AS SEQ_MOPK,
MOPK.SHAIN_CD,
CAST(MOPK.TIME_STAMP AS int) AS TIME_STAMP_MOPK,
ISNULL(MOPK.DEFAULT_KBN,'FALSE') AS DEFAULT_KBN,
MOPK.DISP_NUMBER
FROM
M_OUTPUT_PATTERN MOP
LEFT OUTER JOIN M_OUTPUT_PATTERN_KOBETSU MOPK ON MOPK.SYSTEM_ID = MOP.SYSTEM_ID  AND MOPK.SHAIN_CD = /*data.Shain_Cd*/ AND MOPK.DELETE_FLG = 0
/*BEGIN*/WHERE
/*IF !deletechuFlg*/ MOP.DELETE_FLG = 0/*END*/
 AND MOP.DENSHU_KBN_CD = /*data.Denshu_Kbn_Cd*/ 
/*IF data.Patern_Name != null && data.Patern_Name != ''*/
 AND MOP.PATTERN_NAME LIKE '%' + /*data.Patern_Name*/ + '%'/*END*/
/*END*/
ORDER BY MOP.PATTERN_NAME, MOPK.DISP_NUMBER