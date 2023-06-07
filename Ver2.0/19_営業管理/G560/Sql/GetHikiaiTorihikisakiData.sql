SELECT
	CONVERT(nvarchar, M_HIKIAI_TORIHIKISAKI.CREATE_DATE, 111)
	, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD
	, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU
	, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_FURIGANA
	, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1
	, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_TEL
	, M_SHAIN.SHAIN_NAME_RYAKU
	, CASE ISNULL(TDSE.HIKIAI_TORIHIKISAKI_CD,'') WHEN '' THEN '' ELSE '�\����' END AS SHINSEI_JOUKYOU
FROM
	M_HIKIAI_TORIHIKISAKI
	LEFT JOIN M_SHAIN
		ON M_HIKIAI_TORIHIKISAKI.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD
		AND M_SHAIN.DELETE_FLG = 0
	LEFT JOIN (
		SELECT
			DS.HIKIAI_TORIHIKISAKI_CD
		FROM
			T_DENSHI_SHINSEI_ENTRY DS
			INNER JOIN T_DENSHI_SHINSEI_STATUS SS
				ON DS.SYSTEM_ID = SS.SYSTEM_ID
				AND DS.SEQ = SS.SEQ
				AND SS.DELETE_FLG = 0
		WHERE
			DS.DELETE_FLG = 0
			AND DS.SHINSEI_MASTER_KBN = 4
			AND (SS.SHINSEI_STATUS_CD = 1 OR SS.SHINSEI_STATUS_CD = 2 OR SS.SHINSEI_STATUS_CD = 5)
	) TDSE
		ON TDSE.HIKIAI_TORIHIKISAKI_CD = M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD
WHERE
	M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 0
	/*IF data.eigyouTantoushaCd != null && data.eigyouTantoushaCd != ''*/
		AND M_HIKIAI_TORIHIKISAKI.EIGYOU_TANTOU_CD = /*data.eigyouTantoushaCd*/
	/*END*/