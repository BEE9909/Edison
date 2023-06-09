﻿/*IF !JISSEKI*/
SELECT DISTINCT * FROM 
(
SELECT 
	RT_DTL.HANYU_JISSEKI_SEQ_NO AS HANYU_SEQ_NO
FROM 
    T_MOBISYO_RT_DTL RT_DTL
    INNER JOIN T_MOBISYO_RT RT 
               ON RT.SEQ_NO = RT_DTL.SEQ_NO
               AND RT.DELETE_FLG = 0
               AND RT.HAISHA_DENPYOU_NO = /*TEIKI_HAISHA_NUMBER*/
               AND RT.HAISHA_KBN = 0
			   AND RT_DTL.HANYU_JISSEKI_SEQ_NO IS NOT NULL
WHERE 
    RT_DTL.DELETE_FLG = 0
UNION ALL
SELECT 
	RT_DTL.HANYU_SEQ_NO
FROM 
    T_MOBISYO_RT_DTL RT_DTL
    INNER JOIN T_MOBISYO_RT RT 
               ON RT.SEQ_NO = RT_DTL.SEQ_NO
               AND RT.DELETE_FLG = 0
               AND RT.HAISHA_DENPYOU_NO = /*TEIKI_HAISHA_NUMBER*/
               AND RT.HAISHA_KBN = 0
WHERE 
    RT_DTL.DELETE_FLG = 0
)ALLDATA
ORDER BY ALLDATA.HANYU_SEQ_NO
--ELSE
SELECT 
	RT_DTL.HANYU_JISSEKI_SEQ_NO
FROM 
    T_MOBISYO_RT_DTL RT_DTL
    INNER JOIN T_MOBISYO_RT RT 
               ON RT.SEQ_NO = RT_DTL.SEQ_NO
               AND RT.GENBA_JISSEKI_JYOGAIFLG = 0
               AND RT.DELETE_FLG = 0
               AND RT.HAISHA_DENPYOU_NO = /*TEIKI_HAISHA_NUMBER*/
               AND RT.HAISHA_KBN = 0
WHERE 
    RT_DTL.DELETE_FLG = 0
GROUP BY
	RT_DTL.HANYU_JISSEKI_SEQ_NO
/*END*/
