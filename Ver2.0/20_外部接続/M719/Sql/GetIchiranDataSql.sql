﻿SELECT 
MDSR.DELETE_FLG,
MDSR.SHAIN_CD,
MSN.SHAIN_NAME,
MDSR.CREATE_USER,
MDSR.CREATE_DATE,
MDSR.CREATE_PC,
MDSR.UPDATE_USER,
MDSR.UPDATE_DATE,
MDSR.UPDATE_PC,
MDSR.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO
FROM 
M_DENSHI_KEIYAKU_SHANAI_KEIRO MDSR 
LEFT JOIN M_SHAIN MSN
ON MDSR.SHAIN_CD = MSN.SHAIN_CD
WHERE
/*IF !data.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.IsNull*/
 MDSR.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = /*data.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD*/
/*END*/
/*IF data.SHAIN_CD != null && data.SHAIN_CD != ''*/
 AND MDSR.SHAIN_CD LIKE '%' +  /*data.SHAIN_CD*/ + '%'
/*END*/
/*IF data.SHAIN_NAME != null && data.SHAIN_NAME != ''*/
 AND MSN.SHAIN_NAME LIKE '%' +  /*data.SHAIN_NAME*/ + '%'
/*END*/
/*IF data.CREATE_USER != null && data.CREATE_USER != ''*/
 AND MDSR.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'
/*END*/
/*IF data.CREATE_DATE != null && data.CREATE_DATE != ''*/
 AND CONVERT(nvarchar, MDSR.CREATE_DATE, 111) + ' ' + CONVERT(nvarchar, MDSR.CREATE_DATE, 114) LIKE '%' +  /*data.CREATE_DATE*/ + '%'
/*END*/
/*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/
 AND MDSR.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'
/*END*/
/*IF data.UPDATE_DATE != null && data.UPDATE_DATE != ''*/
 AND CONVERT(nvarchar, MDSR.UPDATE_DATE, 111) + ' ' + CONVERT(nvarchar, MDSR.UPDATE_DATE, 114) LIKE '%' +  /*data.UPDATE_DATE*/ + '%'
/*END*/
ORDER BY MDSR.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,MDSR.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO,MDSR.SHAIN_CD
