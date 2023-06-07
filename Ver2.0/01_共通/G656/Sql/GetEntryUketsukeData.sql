﻿SELECT 
    DENPYOU_KBN AS '伝票',
    DENPYOU_NO AS '伝票番号',
    DENPYOU_DATE AS '伝票日付',
	TORIHIKISAKI_NAME AS '取引先',
	GYOUSHA_NAME AS '業者',
	GENBA_NAME AS '現場',
	SYSTEM_ID AS HIDDEN_SYSTEM_ID,
	DENPYOU_NO AS HIDDEN_UKETSUKE_NUMBER,
    DENPYOU_TYPE AS HIDDEN_DENPYOU_KBN
FROM(
	SELECT 
		'収集受付' AS DENPYOU_KBN,
		UKETSUKE_NUMBER AS DENPYOU_NO,
		CONVERT(varchar(10),SAGYOU_DATE,111) AS DENPYOU_DATE,
		TORIHIKISAKI_NAME,
		GYOUSHA_NAME,
		GENBA_NAME,
	    SYSTEM_ID,
        1 AS DENPYOU_TYPE
	FROM T_UKETSUKE_SS_ENTRY
	WHERE DELETE_FLG = 0
	/*IF !data.KYOTEN_CD.IsNull*/
	AND KYOTEN_CD = /*data.KYOTEN_CD*/
	/*END*/
	/*IF !data.DENPYOU_DATE_FROM.IsNull*/
	AND CONVERT(varchar(10),SAGYOU_DATE,120) >= /*data.DENPYOU_DATE_FROM*/
	/*END*/
	/*IF !data.DENPYOU_DATE_TO.IsNull*/
	AND CONVERT(varchar(10),SAGYOU_DATE,120) <= /*data.DENPYOU_DATE_TO*/
	/*END*/
	/*IF data.DENPYOU_NO != null*/
	AND UKETSUKE_NUMBER = /*data.DENPYOU_NO*/
	/*END*/
	/*IF data.TORIHIKISAKI_CD != null*/
	AND TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
	/*END*/
	/*IF data.GYOUSHA_CD != null*/
	AND GYOUSHA_CD = /*data.GYOUSHA_CD*/
	/*END*/
	/*IF data.GENBA_CD != null*/
	AND GENBA_CD = /*data.GENBA_CD*/
	/*END*/

	UNION ALL

	SELECT 
		'出荷受付' AS DENPYOU_KBN,
		UKETSUKE_NUMBER AS DENPYOU_NO,
		CONVERT(varchar(10),SAGYOU_DATE,111) AS DENPYOU_DATE,
		TORIHIKISAKI_NAME,
		GYOUSHA_NAME,
		GENBA_NAME,
	    SYSTEM_ID,
        2 AS DENPYOU_TYPE
	FROM T_UKETSUKE_SK_ENTRY SK
	WHERE DELETE_FLG = 0
	/*IF !data.KYOTEN_CD.IsNull*/
	AND KYOTEN_CD = /*data.KYOTEN_CD*/
	/*END*/
	/*IF !data.DENPYOU_DATE_FROM.IsNull*/
	AND CONVERT(varchar(10),SAGYOU_DATE,120) >= /*data.DENPYOU_DATE_FROM*/
	/*END*/
	/*IF !data.DENPYOU_DATE_TO.IsNull*/
	AND CONVERT(varchar(10),SAGYOU_DATE,120) <= /*data.DENPYOU_DATE_TO*/
	/*END*/
	/*IF data.DENPYOU_NO != null*/
	AND UKETSUKE_NUMBER = /*data.DENPYOU_NO*/
	/*END*/
	/*IF data.TORIHIKISAKI_CD != null*/
	AND TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
	/*END*/
	/*IF data.GYOUSHA_CD != null*/
	AND GYOUSHA_CD = /*data.GYOUSHA_CD*/
	/*END*/
	/*IF data.GENBA_CD != null*/
	AND GENBA_CD = /*data.GENBA_CD*/
	/*END*/

	UNION ALL

	SELECT 
		'持込受付' AS DENPYOU_KBN,
		UKETSUKE_NUMBER AS DENPYOU_NO,
		CONVERT(varchar(10),SAGYOU_DATE,111) AS DENPYOU_DATE,
		TORIHIKISAKI_NAME,
		GYOUSHA_NAME,
		GENBA_NAME,
	    SYSTEM_ID,
        3 AS DENPYOU_TYPE
	FROM T_UKETSUKE_MK_ENTRY
	WHERE DELETE_FLG = 0
	/*IF !data.KYOTEN_CD.IsNull*/
	AND KYOTEN_CD = /*data.KYOTEN_CD*/
	/*END*/
	/*IF !data.DENPYOU_DATE_FROM.IsNull*/
	AND CONVERT(varchar(10),SAGYOU_DATE,120) >= /*data.DENPYOU_DATE_FROM*/
	/*END*/
	/*IF !data.DENPYOU_DATE_TO.IsNull*/
	AND CONVERT(varchar(10),SAGYOU_DATE,120) <= /*data.DENPYOU_DATE_TO*/
	/*END*/
	/*IF data.DENPYOU_NO != null*/
	AND UKETSUKE_NUMBER = /*data.DENPYOU_NO*/
	/*END*/
	/*IF data.TORIHIKISAKI_CD != null*/
	AND TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
	/*END*/
	/*IF data.GYOUSHA_CD != null*/
	AND GYOUSHA_CD = /*data.GYOUSHA_CD*/
	/*END*/
	/*IF data.GENBA_CD != null*/
	AND GENBA_CD = /*data.GENBA_CD*/
	/*END*/
) AS A
order by DENPYOU_TYPE, DENPYOU_DATE, DENPYOU_NO