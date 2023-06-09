﻿SELECT 
	CONVERT(nvarchar, M_GYOUSHA.CREATE_DATE, 111),
	M_GYOUSHA.GYOUSHA_CD, 
	M_GYOUSHA.GYOUSHA_NAME_RYAKU, 
	M_GYOUSHA.GYOUSHA_FURIGANA,
	M_GYOUSHA.GYOUSHA_ADDRESS1, 
	M_GYOUSHA.GYOUSHA_TEL,
	M_GYOUSHA.EIGYOU_TANTOU_CD
FROM
	M_GYOUSHA 
	LEFT JOIN M_SHAIN
	ON M_GYOUSHA.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD
	AND M_SHAIN.DELETE_FLG = 0
WHERE
	M_GYOUSHA.DELETE_FLG = 0
	/*END*/
	/*IF data.eigyouTantoushaCd != null && data.eigyouTantoushaCd != ''*/
		AND M_GYOUSHA.EIGYOU_TANTOU_CD = /*data.eigyouTantoushaCd*/
	/*END*/