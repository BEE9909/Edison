﻿SELECT
COUNT(*)
FROM
T_SEIKYUU_DENPYOU
/*BEGIN*/WHERE
/*IF !deletechuFlg*/DELETE_FLG = 0/*END*/
/*IF data.SEIKYUSHIMEBI_TO != null && data.SEIKYUSHIMEBI_TO != ""*/
 AND CONVERT(DATETIME, SEIKYUU_DATE,111) > CONVERT(DATETIME, /*data.SEIKYUSHIMEBI_TO*/null,111)/*END*/ 
 AND TORIHIKISAKI_CD = /*data.SEIKYU_CD*/
/*END*/