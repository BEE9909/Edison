﻿SELECT 
	DETAIL.*
FROM
	T_UKETSUKE_SK_DETAIL DETAIL
INNER JOIN T_UKETSUKE_SK_ENTRY ENTRY ON ENTRY.SYSTEM_ID = DETAIL.SYSTEM_ID AND ENTRY.SEQ = DETAIL.SEQ
WHERE
	ENTRY.DELETE_FLG = 0
	AND DETAIL.SYSTEM_ID = /*systemId*/
	AND DETAIL.DETAIL_SYSTEM_ID = /*detailSystemId*/
