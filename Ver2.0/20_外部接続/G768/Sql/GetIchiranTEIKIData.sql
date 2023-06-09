﻿SELECT DATA.*
FROM
(
SELECT DISTINCT
THE.SAGYOU_DATE,
SUBSTRING(CONVERT(nvarchar, THD.KIBOU_TIME, 108), 1, 5) AS GENCHAKU_TIME,
(CASE /*data.SEND_JOKYO*/
    WHEN 1 THEN '未'
    ELSE '済'
    END)AS SEND_JOKYO,
'' AS HAISHA_JOKYO_NAME,
'定期' AS DENPYOU_SHURUI,
THE.TEIKI_HAISHA_NUMBER AS DENPYOU_NUMBER,
THE.COURSE_NAME_CD,
CO.COURSE_NAME,
SMS.SEQ,
THD.ROW_NUMBER,
THD.ROUND_NO,
THD.GYOUSHA_CD,
GY.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME,
THD.GENBA_CD,
GE.GENBA_NAME_RYAKU AS GENBA_NAME,
THD.SEQ AS TEIKI_SEQ,
/*IF data.SEND_JOKYO == 1*/
NULL AS RECEIVER_NAME,
NULL AS MOBILE_PHONE_NUMBER,
NULL AS MESSAGE_ID,
NULL AS ERROR_CODE,
NULl AS ERROR_DETAIL,
NULl AS SEND_DATE_R,
NULl AS SEND_USER,
NULL AS SYSTEM_ID
/*END*/
/*IF data.SEND_JOKYO != 1*/
SMS.RECEIVER_NAME,
SMS.MOBILE_PHONE_NUMBER,
SMS.MESSAGE_ID,
SMS.ERROR_CODE,
SMS.ERROR_DETAIL,
SMS.SEND_DATE_R,
SMS.SEND_USER,
SMS.SYSTEM_ID
/*END*/
FROM T_TEIKI_HAISHA_ENTRY THE
INNER JOIN T_TEIKI_HAISHA_DETAIL THD
ON THE.TEIKI_HAISHA_NUMBER = THD.TEIKI_HAISHA_NUMBER
LEFT OUTER JOIN T_SMS SMS
ON THE.TEIKI_HAISHA_NUMBER = SMS.DENPYOU_NUMBER
INNER JOIN M_GYOUSHA GY
ON THD.GYOUSHA_CD = GY.GYOUSHA_CD
INNER JOIN M_GENBA GE
ON THD.GYOUSHA_CD = GE.GYOUSHA_CD
AND THD.GENBA_CD = GE.GENBA_CD
INNER JOIN M_COURSE_NAME CO
ON THE.COURSE_NAME_CD = CO.COURSE_NAME_CD
WHERE 1 = 1
/*IF data.KYOTEN_CD != null && data.KYOTEN_CD != ''*/AND THE.KYOTEN_CD = /*data.KYOTEN_CD*//*END*/
/*IF data.SAGYOU_DATE_FROM != null && data.SAGYOU_DATE_FROM != ''*/AND CONVERT(DATETIME, CONVERT(nvarchar, THE.SAGYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, /*data.SAGYOU_DATE_FROM*/'', 111), 120)/*END*/
/*IF data.SAGYOU_DATE_TO != null && data.SAGYOU_DATE_TO != ''*/AND CONVERT(DATETIME, CONVERT(nvarchar, THE.SAGYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, /*data.SAGYOU_DATE_TO*/'', 111), 120)/*END*/
/*IF data.GYOUSHA_CD != null && data.GYOUSHA_CD != ''*/AND THD.GYOUSHA_CD LIKE '%' + /*data.GYOUSHA_CD*/ + '%'/*END*/
/*IF data.GENBA_CD != null && data.GENBA_CD != ''*/AND THD.GENBA_CD LIKE '%' + /*data.GENBA_CD*/ + '%'/*END*/
/*IF data.UNPAN_GYOUSHA_CD != null && data.UNPAN_GYOUSHA_CD != ''*/AND THE.UNPAN_GYOUSHA_CD LIKE '%' + /*data.UNPAN_GYOUSHA_CD*/ + '%'/*END*/
AND THE.DELETE_FLG = 0
AND GE.SMS_USE = '1'
/*IF data.SEND_JOKYO == 2*/AND SMS.ROW_NUMBER = THD.ROW_NUMBER AND SMS.ERROR_CODE IS NULL/*END*/
/*IF data.SEND_JOKYO == 3*/AND SMS.ROW_NUMBER = THD.ROW_NUMBER AND SMS.ERROR_CODE IS NOT NULL/*END*/
)AS DATA
ORDER BY SAGYOU_DATE,
GENCHAKU_TIME,
DENPYOU_SHURUI,
DENPYOU_NUMBER,
MOBILE_PHONE_NUMBER