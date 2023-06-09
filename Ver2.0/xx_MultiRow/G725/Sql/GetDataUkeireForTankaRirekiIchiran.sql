SELECT 
	TEMP.DENPYOU_KBN_CD
	,TEMP.DENPYOU_KBN_COL
	,TEMP.KAKUTEI_KBN_COL
	,TEMP.DENPYOU_DATE_COL
	,TEMP.SHURUI_CD_COL
	,TEMP.SHURUI_NAME_COL
	,TEMP.HINMEI_CD_COL
	,TEMP.HINMEI_NAME_COL
	,TEMP.UNIT_CD_COL
	,TEMP.UNIT_NAME_COL
	,TEMP.TANKA_COL
FROM
	(SELECT
		UD.DENPYOU_KBN_CD
		,(CASE WHEN UD.DENPYOU_KBN_CD = 1 
			THEN '����'
			ELSE '�x��'
		END) AS DENPYOU_KBN_COL
		,(CASE WHEN UE.KAKUTEI_KBN = 1 
			THEN '�m��'
			ELSE '���m��'
		END) AS KAKUTEI_KBN_COL
		,UE.DENPYOU_DATE AS DENPYOU_DATE_COL
		,HM.SHURUI_CD AS SHURUI_CD_COL
		,MS.SHURUI_NAME AS SHURUI_NAME_COL
		,UD.HINMEI_CD AS HINMEI_CD_COL
		,UD.HINMEI_NAME AS HINMEI_NAME_COL
		,UD.UNIT_CD AS UNIT_CD_COL
		,MU.UNIT_NAME_RYAKU AS UNIT_NAME_COL
		,UD.TANKA AS TANKA_COL

	FROM T_UKEIRE_ENTRY AS UE
		LEFT JOIN T_UKEIRE_DETAIL AS UD ON UE.SYSTEM_ID = UD.SYSTEM_ID AND UE.SEQ = UD.SEQ
		LEFT JOIN M_HINMEI AS HM ON UD.HINMEI_CD = HM.HINMEI_CD
		LEFT JOIN M_UNIT AS MU ON UD.UNIT_CD = MU.UNIT_CD
		LEFT JOIN M_SHURUI AS MS ON HM.SHURUI_CD = MS.SHURUI_CD

	WHERE UE.DELETE_FLG = 0
	/*IF dto.KYOTEN_CD != null && dto.KYOTEN_CD != '' && dto.KYOTEN_CD != '99'*/
		AND UE.KYOTEN_CD = CONVERT(int, /*dto.KYOTEN_CD*/0)
	/*END*/
	/*IF dto.TORIHIKISAKI_CD != null && dto.TORIHIKISAKI_CD != ''*/
		AND UE.TORIHIKISAKI_CD = /*dto.TORIHIKISAKI_CD*/'000000'
	/*END*/
	/*IF dto.GYOUSHA_CD != null && dto.GYOUSHA_CD != ''*/
		AND UE.GYOUSHA_CD = /*dto.GYOUSHA_CD*/'000000'
	/*END*/
	/*IF dto.GENBA_CD != null && dto.GENBA_CD != ''*/
		AND UE.GENBA_CD = /*dto.GENBA_CD*/'000000'
	/*END*/
	/*IF dto.UNPAN_GYOUSHA_CD != null && dto.UNPAN_GYOUSHA_CD != ''*/
		AND UE.UNPAN_GYOUSHA_CD = /*dto.UNPAN_GYOUSHA_CD*/'000000'
	/*END*/
	/*IF dto.NIOROSHI_GYOUSHA_CD != null && dto.NIOROSHI_GYOUSHA_CD != ''*/
		AND UE.NIOROSHI_GYOUSHA_CD = /*dto.NIOROSHI_GYOUSHA_CD*/'000000'
	/*END*/
	/*IF dto.NIOROSHI_GENBA_CD != null && dto.NIOROSHI_GENBA_CD != ''*/
		AND UE.NIOROSHI_GENBA_CD = /*dto.NIOROSHI_GENBA_CD*/'000000'
	/*END*/
	/*IF !dto.HIDZUKE_FROM.IsNull*/
		AND UE.DENPYOU_DATE >= CONVERT(datetime, /*dto.HIDZUKE_FROM.Value*/'2020/01/01')
	/*END*/
	/*IF !dto.HIDZUKE_TO.IsNull*/
		AND UE.DENPYOU_DATE <= CONVERT(datetime, /*dto.HIDZUKE_TO.Value*/'2020/01/01')
	/*END*/
	/*IF dto.DENPYOU_KBN != null && dto.DENPYOU_KBN != ''*/
		/*IF dto.DENPYOU_KBN == '1'*/
			AND UD.DENPYOU_KBN_CD = 1
		/*END*/
		/*IF dto.DENPYOU_KBN == '2'*/
			AND UD.DENPYOU_KBN_CD = 2
		/*END*/
	/*END*/
	/*IF dto.KAKUTEI_KBN != null && dto.KAKUTEI_KBN != ''*/
		/*IF dto.KAKUTEI_KBN == '1'*/
			AND UE.KAKUTEI_KBN = 1
		/*END*/
		/*IF dto.KAKUTEI_KBN == '2'*/
			AND UE.KAKUTEI_KBN = 2
		/*END*/
	/*END*/
	
	/*IF dto.HINMEI_CD != null && dto.HINMEI_CD != ''*/
		AND UD.HINMEI_CD = /*dto.HINMEI_CD*/'000000'
	/*END*/

	) AS TEMP
GROUP BY 
	TEMP.DENPYOU_KBN_CD
	,TEMP.DENPYOU_KBN_COL
	,TEMP.KAKUTEI_KBN_COL
	,TEMP.DENPYOU_DATE_COL
	,TEMP.SHURUI_CD_COL
	,TEMP.SHURUI_NAME_COL
	,TEMP.HINMEI_CD_COL
	,TEMP.HINMEI_NAME_COL
	,TEMP.UNIT_CD_COL
	,TEMP.UNIT_NAME_COL
	,TEMP.TANKA_COL
ORDER BY
	TEMP.DENPYOU_KBN_CD ASC
	,TEMP.DENPYOU_DATE_COL DESC
	,TEMP.HINMEI_CD_COL ASC
	,TEMP.HINMEI_NAME_COL ASC
	,TEMP.UNIT_CD_COL ASC
