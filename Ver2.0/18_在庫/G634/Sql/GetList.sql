﻿SELECT DISTINCT
    DETAIL.GYOUSHA_CD,
    DETAIL.GENBA_CD,
    DETAIL.ZAIKO_HINMEI_CD
    FROM 
(
    --受入量
    SELECT
	    TUE.NIOROSHI_GYOUSHA_CD AS GYOUSHA_CD,
        TUE.NIOROSHI_GENBA_CD AS GENBA_CD,
        MZH.ZAIKO_HINMEI_CD,
        SUM(ZAIKO_RYOU) AS ZAIKO_RYOU
    FROM T_UKEIRE_ENTRY TUE
    INNER JOIN T_UKEIRE_DETAIL TUD 
    ON TUE.SYSTEM_ID = TUD.SYSTEM_ID
    AND TUE.SEQ = TUD.SEQ
    INNER JOIN T_ZAIKO_HINMEI_HURIWAKE MZH
    ON MZH.SYSTEM_ID = TUD.SYSTEM_ID
    AND MZH.SEQ = TUD.SEQ
    AND MZH.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
	AND MZH.DENSHU_KBN_CD = 1
    WHERE MZH.ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
	AND TUE.DELETE_FLG = 0
    GROUP BY NIOROSHI_GYOUSHA_CD,NIOROSHI_GENBA_CD,ZAIKO_HINMEI_CD
    
    UNION 

    --出荷量
    SELECT
        TSE.NIZUMI_GYOUSHA_CD AS GYOUSHA_CD,
        TSE.NIZUMI_GENBA_CD AS GENBA_CD,
        MZH.ZAIKO_HINMEI_CD,
        SUM(ZAIKO_RYOU) AS ZAIKO_RYOU
    FROM T_SHUKKA_ENTRY TSE
    INNER JOIN T_SHUKKA_DETAIL TSD 
    ON TSE.SYSTEM_ID = TSD.SYSTEM_ID
    AND TSE.SEQ = TSD.SEQ
    INNER JOIN T_ZAIKO_HINMEI_HURIWAKE MZH
    ON MZH.SYSTEM_ID = TSD.SYSTEM_ID
    AND MZH.SEQ = TSD.SEQ
    AND MZH.DETAIL_SYSTEM_ID = TSD.DETAIL_SYSTEM_ID
	AND MZH.DENSHU_KBN_CD = 2
    WHERE MZH.ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
	AND TSE.DELETE_FLG = 0
    GROUP BY NIZUMI_GYOUSHA_CD,NIZUMI_GENBA_CD,ZAIKO_HINMEI_CD
    
    UNION 

    --調整量
    SELECT
        TZTE.GYOUSHA_CD,
        TZTE.GENBA_CD,
        TZTD.ZAIKO_HINMEI_CD,
        SUM(TYOUSEI_RYOU) AS ZAIKO_RYOU
    FROM T_ZAIKO_TYOUSEI_ENTRY TZTE
    INNER JOIN T_ZAIKO_TYOUSEI_DETAIL TZTD
    ON TZTE.SYSTEM_ID = TZTD.SYSTEM_ID
    AND TZTE.SEQ = TZTD.SEQ
    WHERE TZTD.ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
	AND TZTE.DELETE_FLG = 0
    GROUP BY GYOUSHA_CD,GENBA_CD,ZAIKO_HINMEI_CD
    
    UNION 

    --移動量
    --該当現場から移動する移動量
    SELECT
        TZIE.GYOUSHA_CD,
        TZIE.GENBA_CD,
        TZIE.ZAIKO_HINMEI_CD,
        SUM(TZID.IDOU_RYOU) AS ZAIKO_RYOU
    FROM T_ZAIKO_IDOU_ENTRY TZIE
    INNER JOIN T_ZAIKO_IDOU_DETAIL TZID
    ON TZIE.SYSTEM_ID = TZID.SYSTEM_ID
    AND TZIE.SEQ = TZID.SEQ
    WHERE TZIE.ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
	AND TZIE.DELETE_FLG = 0
    GROUP BY TZIE.GYOUSHA_CD,TZIE.GENBA_CD,TZIE.ZAIKO_HINMEI_CD

    UNION 

    --該当現場に移動する移動量
    SELECT 
        TZIE.GYOUSHA_CD,
        TZID.GENBA_CD,
        TZIE.ZAIKO_HINMEI_CD,
        SUM(TZID.IDOU_RYOU) AS ZAIKO_RYOU
    FROM T_ZAIKO_IDOU_ENTRY TZIE
    INNER JOIN T_ZAIKO_IDOU_DETAIL TZID
    ON TZIE.SYSTEM_ID = TZID.SYSTEM_ID
    AND TZIE.SEQ = TZID.SEQ
    WHERE TZIE.ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
	AND TZIE.DELETE_FLG = 0
    GROUP BY TZIE.GYOUSHA_CD,TZID.GENBA_CD,TZIE.ZAIKO_HINMEI_CD
    
    UNION 

    SELECT
	    GYOUSHA_CD,
		GENBA_CD,
		ZAIKO_HINMEI_CD,
		GOUKEI_ZAIKO_RYOU AS ZAIKO_RYOU FROM T_MONTHLY_LOCK_ZAIKO
    WHERE YEAR <= /*year*/
    AND  MONTH <= /*month*/
	AND ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
    AND DELETE_FLG = 0

    UNION 

    SELECT
	    GYOUSHA_CD,
		GENBA_CD,
        ZAIKO_HINMEI_CD,
		KAISHI_ZAIKO_RYOU AS ZAIKO_RYOU 
    FROM M_KAISHI_ZAIKO_INFO MKZI
    --WHERE CONVERT(DATE, ISNULL(TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL(TEKIYOU_END, DATEADD(day,1,GETDATE())))
   WHERE ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
     AND DELETE_FLG = 0
) AS DETAIL
WHERE  (ZAIKO_RYOU != 0)
