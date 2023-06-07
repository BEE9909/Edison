﻿SELECT 
	T_RT.SEQ_NO AS SEQ_NO_HIDDEN,
	T_THE.SYSTEM_ID AS SYSTEM_ID_HIDDEN,
	T_THE.SEQ AS SEQ_HIDDEN,
	T_THD.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID_HIDDEN,
	T_THE.SYSTEM_ID, 
	T_THE.SEQ,
	T_THD.DETAIL_SYSTEM_ID,
	T_THE.TEIKI_HAISHA_NUMBER,
	T_THE.SAGYOU_DATE,
	T_THE.COURSE_NAME_CD AS COURSE_CD,
	M_COURSE_NAME.COURSE_NAME_RYAKU AS COURSE_NAME,
	T_THD.ROW_NUMBER,
	T_THD.ROUND_NO,
	T_THD.GYOUSHA_CD,
	M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME,
	T_THD.GENBA_CD,
	M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME,
	T_THD.MEISAI_BIKOU,
	T_THD.KIBOU_TIME,
	T_THD.SAGYOU_TIME_MINUTE,
	T_THD.UKETSUKE_NUMBER
FROM 
T_TEIKI_HAISHA_ENTRY AS T_THE
INNER JOIN T_TEIKI_HAISHA_DETAIL AS T_THD ON T_THE.SYSTEM_ID = T_THD.SYSTEM_ID AND T_THE.SEQ = T_THD.SEQ
INNER JOIN T_MOBISYO_RT AS T_RT ON T_THD.TEIKI_HAISHA_NUMBER = T_RT.HAISHA_DENPYOU_NO AND T_THD.ROW_NUMBER =T_RT.HAISHA_ROW_NUMBER 
AND T_RT.HAISHA_KBN = 0 AND T_RT.DELETE_FLG = 0 AND T_RT.GENBA_STTS = 0 AND T_RT.GENBA_JISSEKI_JYOGAIFLG = 0
LEFT JOIN M_GYOUSHA ON T_THD.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
LEFT JOIN M_GENBA ON T_THD.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND T_THD.GENBA_CD = M_GENBA.GENBA_CD
LEFT JOIN M_COURSE_NAME ON T_THE.COURSE_NAME_CD = M_COURSE_NAME.COURSE_NAME_CD
WHERE 
T_THE.DELETE_FLG = 0
AND T_RT.GENBA_STTS = 0 
AND T_RT.GENBA_JISSEKI_JYOGAIFLG = 0
AND T_THE.TEIKI_HAISHA_NUMBER IN (/*$data.TeikiHaishaNumber*/)
ORDER BY 
	T_RT.HAISHA_DENPYOU_NO,
    T_RT.HAISHA_SAGYOU_DATE
