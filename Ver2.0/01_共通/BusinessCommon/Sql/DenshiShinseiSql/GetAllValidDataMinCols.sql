﻿SELECT
	DSE.SYSTEM_ID,
	DSE.SEQ,
	DSE.SHINSEI_MASTER_KBN,
	DSS.SHINSEI_STATUS_CD
FROM
	T_DENSHI_SHINSEI_ENTRY AS DSE
	INNER JOIN T_DENSHI_SHINSEI_STATUS AS DSS
		ON DSE.SYSTEM_ID = DSS.SYSTEM_ID
		AND DSE.SEQ = DSS.SEQ
WHERE
	DSS.DELETE_FLG = 0
	/*IF data.HIKIAI_TORIHIKISAKI_CD != null && data.HIKIAI_TORIHIKISAKI_CD != ''*/
		AND DSE.HIKIAI_TORIHIKISAKI_CD = /*data.HIKIAI_TORIHIKISAKI_CD*/
	/*END*/
	/*IF data.HIKIAI_TORIHIKISAKI_CD == ''*/
		AND ( DSE.HIKIAI_TORIHIKISAKI_CD = '' OR DSE.HIKIAI_TORIHIKISAKI_CD IS NULL)
	/*END*/
	/*IF data.HIKIAI_GYOUSHA_CD != null && data.HIKIAI_GYOUSHA_CD != ''*/
		AND DSE.HIKIAI_GYOUSHA_CD = /*data.HIKIAI_GYOUSHA_CD*/
	/*END*/
	/*IF data.HIKIAI_GYOUSHA_CD == ''*/
		AND (DSE.HIKIAI_GYOUSHA_CD = '' OR DSE.HIKIAI_GYOUSHA_CD IS NULL)
	/*END*/
	/*IF data.HIKIAI_GENBA_CD != null && data.HIKIAI_GENBA_CD != ''*/
		AND DSE.HIKIAI_GENBA_CD = /*data.HIKIAI_GENBA_CD*/
	/*END*/
	/*IF data.HIKIAI_GENBA_CD == ''*/
		AND (DSE.HIKIAI_GENBA_CD = '' OR DSE.HIKIAI_GENBA_CD IS NULL)
	/*END*/
	/*IF data.TORIHIKISAKI_CD != null && data.TORIHIKISAKI_CD != ''*/
		AND DSE.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
	/*END*/
	/*IF data.TORIHIKISAKI_CD == ''*/
		AND (DSE.TORIHIKISAKI_CD = '' OR DSE.TORIHIKISAKI_CD IS NULL)
	/*END*/
	/*IF data.GYOUSHA_CD != null && data.GYOUSHA_CD != ''*/
		AND DSE.GYOUSHA_CD = /*data.GYOUSHA_CD*/
	/*END*/
	/*IF data.GYOUSHA_CD == ''*/
		AND (DSE.GYOUSHA_CD = '' OR DSE.GYOUSHA_CD IS NULL)
	/*END*/
	/*IF data.GENBA_CD != null && data.GENBA_CD != ''*/
		AND DSE.GENBA_CD = /*data.GENBA_CD*/
	/*END*/
	/*IF data.GENBA_CD == ''*/
		AND (DSE.GENBA_CD = '' OR DSE.GENBA_CD IS NULL)
	/*END*/
	/*IF data.SHINSEI_MASTER_KBN != null && data.SHINSEI_MASTER_KBN > 0*/
		AND DSE.SHINSEI_MASTER_KBN = /*data.SHINSEI_MASTER_KBN*/
	/*END*/