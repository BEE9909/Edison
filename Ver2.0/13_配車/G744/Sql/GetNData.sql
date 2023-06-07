﻿SELECT DISTINCT
	N.SYSTEM_ID,
	N.SEQ,
	N.NIOROSHI_NUMBER,
	N.TEIKI_HAISHA_NUMBER,
	N.NIOROSHI_GYOUSHA_CD,
	N.NIOROSHI_GENBA_CD
FROM 
	T_TEIKI_HAISHA_ENTRY E
INNER JOIN 
	T_TEIKI_HAISHA_DETAIL D ON E.SYSTEM_ID = D.SYSTEM_ID AND E.SEQ = D.SEQ 
INNER JOIN 
	T_TEIKI_HAISHA_SHOUSAI S ON D.SYSTEM_ID = S.SYSTEM_ID AND D.SEQ = S.SEQ AND D.DETAIL_SYSTEM_ID = S.DETAIL_SYSTEM_ID
INNER JOIN 
	T_TEIKI_HAISHA_NIOROSHI N ON S.SYSTEM_ID = N.SYSTEM_ID AND S.SEQ = N.SEQ AND S.NIOROSHI_NUMBER = N.NIOROSHI_NUMBER
WHERE 
	E.DELETE_FLG = 0
	AND E.SYSTEM_ID = /*data.SYSTEM_ID*/
	AND S.DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID*/