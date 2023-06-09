﻿SELECT
	COUNT(RT_DTL.SEQ_NO) AS KAISHU_CNT
FROM
	T_MOBISYO_RT RT
	INNER JOIN T_MOBISYO_RT_DTL RT_DTL ON RT.SEQ_NO = RT_DTL.SEQ_NO
WHERE
	RT.DELETE_FLG = 0
	AND RT.GENBA_JISSEKI_JYOGAIFLG = 0
	AND RT.HAISHA_KBN = /*HAISHA_KBN*/
	AND RT_DTL.JISSEKI_REGIST_FLG = 1
	AND RT.HAISHA_DENPYOU_NO = /*HAISHA_DENPYOU_NO*/
GROUP BY
	RT.HAISHA_DENPYOU_NO
