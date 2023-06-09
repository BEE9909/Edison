﻿SELECT 
    '受入' AS '伝票',
    UKEIRE_NUMBER AS '伝票番号',
    CONVERT(varchar(10),DENPYOU_DATE,120) AS '伝票日付',
	TORIHIKISAKI_NAME AS '取引先',
	GYOUSHA_NAME AS '業者',
	GENBA_NAME AS '現場',
	SYSTEM_ID AS HIDDEN_SYSTEM_ID,
	UKETSUKE_NUMBER AS HIDDEN_UKETSUKE_NUMBER,
    5 AS HIDDEN_DENPYOU_KBN
FROM T_UKEIRE_ENTRY
WHERE DELETE_FLG = 0
/*IF !data.KYOTEN_CD.IsNull*/
AND KYOTEN_CD = /*data.KYOTEN_CD*/
/*END*/
/*IF !data.DENPYOU_DATE_FROM.IsNull*/
AND CONVERT(varchar(10),DENPYOU_DATE,120) >= /*data.DENPYOU_DATE_FROM*/
/*END*/
/*IF !data.DENPYOU_DATE_TO.IsNull*/
AND CONVERT(varchar(10),DENPYOU_DATE,120) <= /*data.DENPYOU_DATE_TO*/
/*END*/
/*IF data.DENPYOU_NO != null*/
AND UKEIRE_NUMBER = /*data.DENPYOU_NO*/
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
ORDER BY DENPYOU_DATE,UKEIRE_NUMBER