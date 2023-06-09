﻿SELECT 
DELETE_FLG,
KYOYUSAKI_CD,
KYOYUSAKI_CORP_NAME,
KYOYUSAKI_NAME,
KYOYUSAKI_MAIL_ADDRESS,
CREATE_USER,
CREATE_DATE,
CREATE_PC,
UPDATE_USER,
UPDATE_DATE,
UPDATE_PC,
TIME_STAMP
FROM 
M_KYOYUSAKI
/*BEGIN*/WHERE
/*IF !data.DELETE_FLG.IsNull*/
AND DELETE_FLG = /*data.DELETE_FLG*/
/*END*/
/*IF !data.KYOYUSAKI_CD.IsNull*/
AND CAST(KYOYUSAKI_CD AS varchar(2)) LIKE '%' + CAST(/*data.KYOYUSAKI_CD*/0 AS varchar(2)) + '%'
/*END*/
/*IF data.KYOYUSAKI_CORP_NAME != null && data.KYOYUSAKI_CORP_NAME != ''*/
AND KYOYUSAKI_CORP_NAME LIKE '%' + /*data.KYOYUSAKI_CORP_NAME*/ + '%'
/*END*/
/*IF data.KYOYUSAKI_NAME != null && data.KYOYUSAKI_NAME != ''*/
AND KYOYUSAKI_NAME LIKE '%' +  /*data.KYOYUSAKI_NAME*/ + '%'
/*END*/
/*IF data.KYOYUSAKI_MAIL_ADDRESS != null && data.KYOYUSAKI_MAIL_ADDRESS != ''*/
AND KYOYUSAKI_MAIL_ADDRESS LIKE '%' +  /*data.KYOYUSAKI_MAIL_ADDRESS*/ + '%'
/*END*/
/*IF data.CREATE_USER != null && data.CREATE_USER != ''*/
AND CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'
/*END*/
/*IF data.SEARCH_CREATE_DATE != null && data.SEARCH_CREATE_DATE != ''*/
AND CONVERT(nvarchar, CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'
/*END*/
/*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/
AND UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'
/*END*/
/*IF data.SEARCH_UPDATE_DATE != null && data.SEARCH_UPDATE_DATE != ''*/
AND CONVERT(nvarchar, UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'
/*END*/
/*IF !deletechuFlg*/AND DELETE_FLG = 0/*END*/
/*END*/
ORDER BY KYOYUSAKI_CD
