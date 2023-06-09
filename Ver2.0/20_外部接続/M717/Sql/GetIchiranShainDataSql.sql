SELECT
CID.DELETE_FLG,
CID.SHAIN_CD,
S.SHAIN_NAME,
CID.DENSHI_KEIYAKU_CLIENT_ID,
CID.UPDATE_USER,
CID.UPDATE_DATE,
CID.UPDATE_PC,
CID.CREATE_USER,
CID.CREATE_DATE,
CID.CREATE_PC,
CID.TIME_STAMP
FROM 
dbo.M_SHAIN S
LEFT JOIN dbo.M_DENSHI_KEIYAKU_CLIENT_ID CID ON S.SHAIN_CD = CID.SHAIN_CD
/*BEGIN*/WHERE
/*IF data.SHAIN_CD != null && data.SHAIN_CD != ''*/AND CID.SHAIN_CD LIKE '%' +  /*data.SHAIN_CD*/ + '%'/*END*/
/*IF data.SHAIN_NAME != null && data.SHAIN_NAME != ''*/AND S.SHAIN_NAME LIKE '%' +  /*data.SHAIN_NAME*/ + '%'/*END*/

/* P1 */
/*IF !deletechuFlg */
AND CID.DELETE_FLG = 0
/*END*/

/*END*/
ORDER BY CID.SHAIN_CD
