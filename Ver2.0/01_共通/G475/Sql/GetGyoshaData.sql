﻿SELECT
	GYOUSHA_CD,
	GYOUSHA_NAME_RYAKU
FROM
	M_GYOUSHA
WHERE
	DELETE_FLG = 0 
	AND GYOUSHA_CD = /*data.GYOUSHA_CD*/null
	/*IF data.SHUTOKU_KBN == 0 */ 
	AND (HAISHUTSU_NIZUMI_GYOUSHA_KBN = 1) /*END*/

	/*IF data.SHUTOKU_KBN == 1 */ 
	AND (UNPAN_JUTAKUSHA_KAISHA_KBN = 1) /*END*/

	/*IF data.SHUTOKU_KBN == 2 */ 
	AND (SHOBUN_NIOROSHI_GYOUSHA_KBN = 1) /*END*/

