﻿SELECT COUNT(SYSTEM_ID)
FROM
/*IF target == 1*/T_UKEIRE_ENTRY/*END*/
/*IF target == 2*/T_SHUKKA_ENTRY/*END*/
WHERE DELETE_FLG = 0
AND TAIRYUU_KBN = /*tairyuKbn*/
/*IF honjitsuKbn == 1*/AND CONVERT(DATETIME, CONVERT(nvarchar, DENPYOU_DATE, 111), 120) = CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120)/*END*/
/*IF kyotenCd != nulll*/AND KYOTEN_CD = /*kyotenCd*//*END*/