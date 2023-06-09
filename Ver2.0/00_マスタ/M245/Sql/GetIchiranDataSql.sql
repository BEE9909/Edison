﻿SELECT 
  HIRITSU.DELETE_FLG AS DELETE_FLG
, HIRITSU.ZAIKO_HINMEI_CD AS ZAIKO_HINMEI_CD
, HIRITSU.ZAIKO_HINMEI_NAME AS ZAIKO_HINMEI_NAME
, HIRITSU.ZAIKO_HIRITSU as ZAIKO_HIRITSU
, '%' AS ZAIKO_HIRITSU_UNIT
, HIRITSU.BIKOU AS BIKOU
, HIRITSU.UPDATE_USER AS UPDATE_USER
, HIRITSU.UPDATE_DATE AS UPDATE_DATE
, HIRITSU.CREATE_USER AS CREATE_USER
, HIRITSU.CREATE_DATE AS CREATE_DATE
, HIRITSU.CREATE_PC AS CREATE_PC
, HIRITSU.UPDATE_PC AS UPDATE_PC
, HIRITSU.TIME_STAMP AS TIME_STAMP
, HIRITSU.DENSHU_KBN_CD AS DENSHUKBNCD
, HIRITSU.HINMEI_CD AS HINMEICD
, HIRITSU.DENSHU_KBN_CD AS UK_DENSHU_KBN_CD
, HIRITSU.HINMEI_CD AS UK_HINMEI_CD
, HIRITSU.ZAIKO_HINMEI_CD AS UK_ZAIKO_HINMEI_CD
FROM dbo.M_ZAIKO_HIRITSU HIRITSU
/*BEGIN*/ 
WHERE 
/*IF dataHiritsu.HINMEI_CD != null*/HIRITSU.HINMEI_CD = /*dataHiritsu.HINMEI_CD*/ /*END*/
/*IF !dataHiritsu.DENSHU_KBN_CD.IsNull*/AND HIRITSU.DENSHU_KBN_CD =/*dataHiritsu.DENSHU_KBN_CD*//*END*/
/*IF dataHiritsu.ZAIKO_HINMEI_CD!= null*/AND HIRITSU.ZAIKO_HINMEI_CD LIKE '%' +/*dataHiritsu.ZAIKO_HINMEI_CD*/ + '%'/*END*/
/*IF dataHiritsu.ZAIKO_HINMEI_NAME!= null*/AND HIRITSU.ZAIKO_HINMEI_NAME LIKE '%' +/*dataHiritsu.ZAIKO_HINMEI_NAME*/ + '%'/*END*/
/*IF !dataHiritsu.ZAIKO_HIRITSU.IsNull*/AND HIRITSU.ZAIKO_HIRITSU = /*dataHiritsu.ZAIKO_HIRITSU*//*END*/
/*IF zaikoHinmei!= null && zaikoHinmei!= ''*/AND HIRITSU.ZAIKO_HINMEI_NAME LIKE '%' + /*zaikoHinmei*/ + '%'/*END*/
/*IF dataHiritsu.BIKOU!= null*/AND HIRITSU.BIKOU LIKE '%' +/*dataHiritsu.BIKOU*/ + '%'/*END*/
/*IF dataHiritsu.CREATE_USER != null*/AND HIRITSU.CREATE_USER LIKE '%' +  /*dataHiritsu.CREATE_USER*/ + '%'/*END*/
/*IF dataHiritsu.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, HIRITSU.CREATE_DATE, 120) LIKE '%' +  /*dataHiritsu.SEARCH_CREATE_DATE*/ + '%'/*END*/
/*IF dataHiritsu.UPDATE_USER != null*/AND HIRITSU.UPDATE_USER LIKE '%' +  /*dataHiritsu.UPDATE_USER*/ + '%'/*END*/
/*IF dataHiritsu.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, HIRITSU.UPDATE_DATE, 120) LIKE '%' +  /*dataHiritsu.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*IF !deletechuFlg*/AND HIRITSU.DELETE_FLG = 0/*END*/
/*END*/
ORDER BY HIRITSU.ZAIKO_HINMEI_CD