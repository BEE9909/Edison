﻿SELECT
KYOTEN_CD,
KYOTEN_NAME_RYAKU,
KYOTEN_NAME,
KYOTEN_DAIHYOU,
KYOTEN_POST,
KYOTEN_ADDRESS1,
KYOTEN_ADDRESS2,
KYOTEN_TEL,
KYOTEN_FAX
FROM
M_KYOTEN
/*BEGIN*/WHERE
/*IF data.KYOTEN_CD != null*/
KYOTEN_CD = /*data.KYOTEN_CD*//*END*/
/*END*/
ORDER BY KYOTEN_CD