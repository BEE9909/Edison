﻿SELECT 
     ROW_NUMBER()OVER(ORDER BY DTL.ROW_NO) ROW_NO
    ,ENT.SHASHU_CD
    ,ENT.SHASHU_NAME
    ,ENT.SHARYOU_CD
    ,ENT.SHARYOU_NAME
    ,ENT.UNTENSHA_NAME
    ,ENT.UNTENSHA_CD
    ,CAST(ENT.SAGYOU_DATE AS DATETIME)AS HAISHA_SAGYOU_DATE
    ,ENT.UKETSUKE_NUMBER AS HAISHA_DENPYOU_NO
    ,ENT.GYOUSHA_CD AS GENBA_JISSEKI_GYOUSHACD
    ,ENT.GENBA_CD AS GENBA_JISSEKI_GYOUSHACD
    ,DTL.HINMEI_CD AS GENBA_DETAIL_HINMEICD
    ,DTL.UNIT_CD AS GENBA_DETAIL_UNIT_CD1
    ,ENT.NIOROSHI_GYOUSHA_CD AS HANNYUU_GYOUSHACD
    ,ENT.NIOROSHI_GENBA_CD AS HANNYUU_GENBACD
FROM
    T_UKETSUKE_SS_ENTRY ENT
    LEFT JOIN T_UKETSUKE_SS_DETAIL DTL ON ENT.SYSTEM_ID = DTL.SYSTEM_ID AND ENT.SEQ = DTL.SEQ
    LEFT JOIN M_HINMEI HINMEI ON DTL.HINMEI_CD = HINMEI.HINMEI_CD
WHERE
    ENT.SYSTEM_ID = /*SYSTEM_ID*/
    AND ENT.SEQ = /*SEQ*/
	AND (DTL.HINMEI_CD IS NOT NULL AND DTL.HINMEI_CD != '')
    AND HINMEI.DENSHU_KBN_CD IN (3,9)
ORDER BY
    DTL.ROW_NO