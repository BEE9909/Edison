﻿SELECT  
     TME.SYSTEM_ID AS SYSTEM_ID
    ,TME.SEQ AS SEQ
    ,TME.MANIFEST_ID AS MANIFEST_ID
    ,NULL AS KANRI_ID
    ,NULL AS DEN_SEQ
	,TME.HAIKI_KBN_CD AS HAIKI_KBN_CD
	,MCS.HOUKOKU_SHISETSU_NAME AS SHORI_SHISETSU_NAME
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL THEN 1 ELSE 0 END AS NEXT_KBN
	,TMR.NEXT_SYSTEM_ID AS NEXT_SYSTEM_ID
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MHS.HOUKOKUSHO_BUNRUI_CD
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MDHS.HOUKOKUSHO_BUNRUI_CD
	 ELSE NULL END AS HOUKOKUSHO_BUNRUI_CD

	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MCB.HOUKOKU_BUNRUI_NAME
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MCB .HOUKOKU_BUNRUI_NAME
	 ELSE NULL END AS SBN_AFTER_HAIKI_NAME
	,MCS.HOUKOKU_SHISETSU_CD AS SHORI_SHISETSU_CD
	,MCB.HOUKOKU_BUNRUI_CD AS HAIKI_SHURUI_CD
	,MCB.HOUKOKU_BUNRUI_NAME AS HAIKI_SHURUI_NAME
	,TMD.KANSAN_SUU AS KANSAN_SUU
	,TMD.GENNYOU_SUU AS GENNYOU_SUU
	,MU.UNIT_NAME AS UNIT_NAME
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MCS.HOUKOKU_SHOBUN_HOUHOU_CD
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MCS.HOUKOKU_SHOBUN_HOUHOU_CD
	 ELSE NULL END AS SHOBUN_HOUHOU_CD
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MCS.HOUKOKU_SHOBUN_HOUHOU_NAME
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MCS.HOUKOKU_SHOBUN_HOUHOU_NAME
	 ELSE NULL END AS SHOBUN_HOUHOU_NAME 
	,HSTMGA.CHIIKI_CD AS HST_JOU_CHIIKI_CD
	,CASE WHEN HSTMGA.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 THEN 1 ELSE 2 END AS HST_KEN_KBN
	,HSTMT.TODOUFUKEN_CD AS HST_JOU_TODOUFUKEN_CD
    
FROM 
	T_MANIFEST_ENTRY TME

	LEFT JOIN T_MANIFEST_DETAIL TMD ON TMD.SYSTEM_ID = TME.SYSTEM_ID AND TMD.SEQ = TME.SEQ
	LEFT JOIN (SELECT TMU1.* FROM T_MANIFEST_ENTRY TME1 INNER JOIN T_MANIFEST_UPN TMU1 ON TMU1.SYSTEM_ID = TME1.SYSTEM_ID AND TMU1.SEQ = TME1.SEQ AND TMU1.UPN_ROUTE_NO = 1 WHERE (TME1.HAIKI_KBN_CD = 1 OR TME1.HAIKI_KBN_CD =2) AND TME1.DELETE_FLG =0 AND TME1.SEQ = (SELECT MAX(SEQ) FROM T_MANIFEST_ENTRY WHERE SYSTEM_ID = TME1.SYSTEM_ID)
	           UNION
	           SELECT TMU2.* FROM T_MANIFEST_ENTRY TME2 INNER JOIN T_MANIFEST_UPN TMU2 ON TMU2.SYSTEM_ID = TME2.SYSTEM_ID AND TMU2.SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3  AND TMU2.UPN_ROUTE_NO = (
	                                                                              SELECT MAX(UPN_ROUTE_NO) FROM T_MANIFEST_UPN WHERE TME2.SYSTEM_ID = SYSTEM_ID AND SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3 AND UPN_SAKI_KBN = 1) AND TME2.DELETE_FLG = 0) TMU ON TMU.SYSTEM_ID = TME.SYSTEM_ID AND TMD.SEQ = TME.SEQ
	
	LEFT JOIN M_CHIIKIBETSU_SHISETSU MCS ON MCS.SHOBUN_HOUHOU_CD = TMD.SBN_HOUHOU_CD AND MCS.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0
	LEFT JOIN M_HAIKI_SHURUI MHS ON MHS.HAIKI_SHURUI_CD = TMD.HAIKI_SHURUI_CD AND MHS.HAIKI_KBN_CD = TME.HAIKI_KBN_CD
	LEFT JOIN M_CHIIKIBETSU_BUNRUI MCB ON MCB.HOUKOKUSHO_BUNRUI_CD = MHS.HOUKOKUSHO_BUNRUI_CD AND MCB.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0


	LEFT JOIN T_MANIFEST_RELATION TMR ON  TMR.FIRST_SYSTEM_ID = TMD.DETAIL_SYSTEM_ID AND TMR.DELETE_FLG = 0
	LEFT JOIN T_MANIFEST_ENTRY NEXT_TME ON NEXT_TME.SYSTEM_ID = TMR.NEXT_SYSTEM_ID AND NEXT_TME.SEQ = (SELECT MAX(SEQ) FROM T_MANIFEST_ENTRY WHERE SYSTEM_ID = NEXT_TME.SYSTEM_ID) AND NEXT_TME.DELETE_FLG = 0 AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN (SELECT TMU1.* FROM T_MANIFEST_ENTRY TME1 INNER JOIN T_MANIFEST_UPN TMU1 ON TMU1.SYSTEM_ID = TME1.SYSTEM_ID AND TMU1.SEQ = TME1.SEQ AND TMU1.UPN_ROUTE_NO = 1 WHERE (TME1.HAIKI_KBN_CD = 1 OR TME1.HAIKI_KBN_CD =2) AND TME1.DELETE_FLG =0 AND TME1.SEQ = (SELECT MAX(SEQ) FROM T_MANIFEST_ENTRY WHERE SYSTEM_ID = TME1.SYSTEM_ID)
	           UNION
	           SELECT TMU2.* FROM T_MANIFEST_ENTRY TME2 INNER JOIN T_MANIFEST_UPN TMU2 ON TMU2.SYSTEM_ID = TME2.SYSTEM_ID AND TMU2.SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3  AND TMU2.UPN_ROUTE_NO = (
	                                                                              SELECT MAX(UPN_ROUTE_NO) FROM T_MANIFEST_UPN WHERE TME2.SYSTEM_ID = SYSTEM_ID AND SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3 AND UPN_SAKI_KBN = 1) AND TME2.DELETE_FLG = 0 ) NEXT_TMU ON NEXT_TMU.SYSTEM_ID = NEXT_TME.SYSTEM_ID AND NEXT_TMU.SEQ = NEXT_TME.SEQ AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN T_MANIFEST_DETAIL NEXT_TMD ON NEXT_TMD.SYSTEM_ID = NEXT_TME.SYSTEM_ID AND NEXT_TMD.SEQ = NEXT_TME.SEQ AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN M_HAIKI_SHURUI NEXT_MHS ON NEXT_MHS.HAIKI_SHURUI_CD = NEXT_TMD.HAIKI_SHURUI_CD AND NEXT_MHS.HAIKI_KBN_CD = TMR.NEXT_HAIKI_KBN_CD AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN M_CHIIKIBETSU_BUNRUI NEXT_MCB ON NEXT_MHS.HOUKOKUSHO_BUNRUI_CD = NEXT_MCB.HOUKOKUSHO_BUNRUI_CD AND NEXT_MCB.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN M_CHIIKIBETSU_SHOBUN NEXT_MCS ON NEXT_MCS.SHOBUN_HOUHOU_CD = NEXT_TMD.SBN_HOUHOU_CD AND NEXT_MCS.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND TMR.NEXT_HAIKI_KBN_CD != 4

	LEFT JOIN DT_R18_EX NEXT_DTR18EX ON NEXT_DTR18EX.SYSTEM_ID = TMR.NEXT_SYSTEM_ID AND NEXT_DTR18EX.DELETE_FLG = 0 AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN DT_R19_EX NEXT_DTR19EX ON NEXT_DTR19EX.SYSTEM_ID = TMR.NEXT_SYSTEM_ID AND NEXT_DTR19EX.DELETE_FLG = 0 AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN DT_R19 NEXT_DTR19 ON NEXT_DTR19.KANRI_ID = NEXT_DTR19EX.KANRI_ID AND NEXT_DTR19.SEQ = NEXT_DTR19EX.SEQ AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN DT_R18 NEXT_DTR18 ON NEXT_DTR18.KANRI_ID = NEXT_DTR18EX.KANRI_ID AND NEXT_DTR18EX.SEQ = NEXT_DTR18EX.SEQ AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN M_DENSHI_HAIKI_SHURUI NEXT_DEN_MDHS ON NEXT_DEN_MDHS.HAIKI_SHURUI_CD = (NEXT_DTR18.HAIKI_DAI_CODE + NEXT_DTR18.HAIKI_CHU_CODE + NEXT_DTR18.HAIKI_SHO_CODE + NEXT_DTR18.HAIKI_SAI_CODE) AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN M_CHIIKIBETSU_BUNRUI NEXT_DEN_MCB ON NEXT_DEN_MCB.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND NEXT_DEN_MCB.HOUKOKUSHO_BUNRUI_CD = NEXT_DEN_MDHS.HOUKOKUSHO_BUNRUI_CD AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN M_CHIIKIBETSU_SHOBUN NEXT_DEN_MCS ON NEXT_DEN_MCS.SHOBUN_HOUHOU_CD = NEXT_DTR18EX.SBN_HOUHOU_CD AND NEXT_DEN_MCS.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND TMR.NEXT_HAIKI_KBN_CD = 4

	LEFT JOIN M_UNIT MU ON MU.UNIT_CD = (SELECT MANI_KANSAN_KIHON_UNIT_CD FROM M_SYS_INFO) 
	LEFT JOIN M_GENBA HSTMGA ON HSTMGA.GYOUSHA_CD = TME.HST_GYOUSHA_CD AND HSTMGA.GENBA_CD = TME.HST_GENBA_CD
	LEFT JOIN M_CHIIKI HSTMC ON HSTMC.CHIIKI_CD = HSTMGA.CHIIKI_CD
	LEFT JOIN M_TODOUFUKEN HSTMT ON HSTMT.TODOUFUKEN_CD = HSTMC.TODOUFUKEN_CD
WHERE TME.DELETE_FLG = 0
	/*IF data.JIGYOUJOU_KBN.Value == 1*/
	    AND ((TMD.SBN_END_DATE IS NOT NULL
		    AND (TMD.SBN_END_DATE <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_END.Value*/20140901, 111), 120) AND CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_BEGIN.Value*/20100901, 111), 120) <= TMD.SBN_END_DATE)
		    AND TME.FIRST_MANIFEST_KBN = 0
		     )
		     OR
			 (
			 TMD.LAST_SBN_END_DATE IS NOT NULL
			 AND (TMD.LAST_SBN_END_DATE <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_END.Value*/20140901, 111), 120) AND CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_BEGIN.Value*/20100901, 111), 120) <= TMD.LAST_SBN_END_DATE)
			 )
		   )
		AND TMU.UPN_SAKI_GYOUSHA_CD = /*data.HOUKOKU_GYOUSHA_CD*/0
		AND TMU.UPN_SAKI_GENBA_CD = /*data.HOUKOKU_GENBA_CD*/0
	/*END*/

	/*IF data.JIGYOUJOU_KBN.Value == 2*/
		AND (TMD.LAST_SBN_END_DATE <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_END.Value*/20140901, 111), 120) AND CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_BEGIN.Value*/20100901, 111), 120) <= TMD.LAST_SBN_END_DATE)
		AND TMD.LAST_SBN_END_DATE IS NOT NULL
		AND TMD.LAST_SBN_GYOUSHA_CD = /*data.HOUKOKU_GYOUSHA_CD*/0
		AND TMD.LAST_SBN_GENBA_CD = /*data.HOUKOKU_GENBA_CD*/0
	/*END*/

/*IF data.DENMANI_KBN.Value == 1*/
UNION

SELECT 
     NULL AS SYSTEM_ID
    ,NULL AS SEQ
    ,DTR18.MANIFEST_ID AS MANIFEST_ID
    ,DTR18.KANRI_ID AS KANRI_ID
    ,DTR18.SEQ AS DEN_SEQ
	,'4' AS HAIKI_KBN_CD
	,MCS.HOUKOKU_SHISETSU_NAME AS SHORI_SHISETSU_NAME
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL THEN 0 ELSE 1 END AS NEXT_KBN
	,TMR.NEXT_SYSTEM_ID AS NEXT_SYSTEM_ID
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MHS.HOUKOKUSHO_BUNRUI_CD
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MDHS.HOUKOKUSHO_BUNRUI_CD
	 ELSE NULL END AS HOUKOKUSHO_BUNRUI_CD

	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MCB.HOUKOKU_BUNRUI_NAME
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MCB .HOUKOKU_BUNRUI_NAME
	 ELSE NULL END AS SBN_AFTER_HAIKI_NAME
	,MCS.HOUKOKU_SHISETSU_CD AS SHORI_SHISETSU_CD
	,MCB.HOUKOKU_BUNRUI_CD AS HAIKI_SHURUI_CD
	,MCB.HOUKOKU_BUNRUI_NAME AS HAIKI_SHURUI_NAME
	,DTR18EX.KANSAN_SUU AS KANSAN_SUU
	,DTR18EX.GENNYOU_SUU AS GENNYOU_SUU
	,MU.UNIT_NAME AS UNIT_NAME
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MCS.HOUKOKU_SHOBUN_HOUHOU_CD
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MCS.HOUKOKU_SHOBUN_HOUHOU_CD
	 ELSE NULL END AS SHOBUN_HOUHOU_CD
	,CASE WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD != 4 THEN NEXT_MCS.HOUKOKU_SHOBUN_HOUHOU_NAME
	 WHEN TMR.NEXT_SYSTEM_ID IS NOT NULL AND TMR.NEXT_HAIKI_KBN_CD = 4 THEN NEXT_DEN_MCS.HOUKOKU_SHOBUN_HOUHOU_NAME
	 ELSE NULL END AS SHOBUN_HOUHOU_NAME 
	,HSTMGA.CHIIKI_CD AS HST_JOU_CHIIKI_CD
	,CASE WHEN HSTMGA.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 THEN 1 ELSE 2 END AS HST_KEN_KBN
	,HSTMT.TODOUFUKEN_CD AS HST_JOU_TODOUFUKEN_CD

FROM 
	DT_R18 DTR18
	LEFT JOIN DT_R18_EX DTR18EX ON DTR18EX.KANRI_ID = DTR18.KANRI_ID AND DTR18EX.SEQ = DTR18.SEQ AND DELETE_FLG = 0
	LEFT JOIN DT_R19 DTR19 ON DTR19.KANRI_ID = DTR18.KANRI_ID AND  DTR19.UPN_ROUTE_NO = (SELECT DISTINCT MAX(UPN_ROUTE_NO) FROM DT_R19 WHERE KANRI_ID = DTR19.KANRI_ID AND SEQ = DTR19.SEQ)
	LEFT JOIN DT_R19_EX DTR19EX ON DTR19EX.KANRI_ID = DTR19.KANRI_ID AND DTR19EX.UPN_ROUTE_NO = DTR19.UPN_ROUTE_NO AND DTR19EX.DELETE_FLG = 0
	LEFT JOIN DT_R13 DTR13 ON DTR13.KANRI_ID = DTR18.KANRI_ID AND DTR13.SEQ = (SELECT MAX(SEQ) FROM DT_R13 WHERE  KANRI_ID = DTR13.KANRI_ID)
	LEFT JOIN DT_R13_EX DTR13EX ON DTR13EX.KANRI_ID = DTR13.KANRI_ID AND DTR13EX.REC_SEQ = DTR13.SEQ
	
	LEFT JOIN M_CHIIKIBETSU_SHISETSU MCS ON MCS.SHOBUN_HOUHOU_CD = DTR18EX.SBN_HOUHOU_CD AND MCS.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0
	LEFT JOIN M_DENSHI_HAIKI_SHURUI MHS ON MHS.HAIKI_SHURUI_CD = (DTR18.HAIKI_DAI_CODE + DTR18.HAIKI_CHU_CODE + DTR18.HAIKI_SHO_CODE + DTR18.HAIKI_SAI_CODE)
	LEFT JOIN M_CHIIKIBETSU_BUNRUI MCB ON MCB.HOUKOKUSHO_BUNRUI_CD = MHS.HOUKOKUSHO_BUNRUI_CD AND MCB.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0

	LEFT JOIN T_MANIFEST_RELATION TMR ON  TMR.FIRST_SYSTEM_ID = DTR18EX.SYSTEM_ID AND TMR.DELETE_FLG = 1
	LEFT JOIN T_MANIFEST_ENTRY NEXT_TME ON NEXT_TME.SYSTEM_ID = TMR.NEXT_SYSTEM_ID AND NEXT_TME.DELETE_FLG = 0 AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN (SELECT TMU1.* FROM T_MANIFEST_ENTRY TME1 INNER JOIN T_MANIFEST_UPN TMU1 ON TMU1.SYSTEM_ID = TME1.SYSTEM_ID AND TMU1.SEQ = TME1.SEQ AND TMU1.UPN_ROUTE_NO = 1 WHERE (TME1.HAIKI_KBN_CD = 1 OR TME1.HAIKI_KBN_CD = 2) AND TME1.DELETE_FLG =0 AND TME1.SEQ = (SELECT MAX(SEQ) FROM T_MANIFEST_ENTRY WHERE SYSTEM_ID = TME1.SYSTEM_ID)
	           UNION
	           SELECT TMU2.* FROM T_MANIFEST_ENTRY TME2 INNER JOIN T_MANIFEST_UPN TMU2 ON TMU2.SYSTEM_ID = TME2.SYSTEM_ID AND TMU2.SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3  AND TMU2.UPN_ROUTE_NO = (
	                                                                              SELECT MAX(UPN_ROUTE_NO) FROM T_MANIFEST_UPN WHERE TME2.SYSTEM_ID = SYSTEM_ID AND SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3 AND UPN_SAKI_KBN = 1) AND TME2.DELETE_FLG = 0 ) NEXT_TMU ON NEXT_TMU.SYSTEM_ID = NEXT_TME.SYSTEM_ID AND NEXT_TMU.SEQ = NEXT_TME.SEQ AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN T_MANIFEST_DETAIL NEXT_TMD ON NEXT_TMD.SYSTEM_ID = NEXT_TME.SYSTEM_ID AND NEXT_TMD.SEQ = NEXT_TME.SEQ AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN M_HAIKI_SHURUI NEXT_MHS ON NEXT_MHS.HAIKI_SHURUI_CD = NEXT_TMD.HAIKI_SHURUI_CD AND NEXT_MHS.HAIKI_KBN_CD = TMR.NEXT_HAIKI_KBN_CD AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN M_CHIIKIBETSU_BUNRUI NEXT_MCB ON NEXT_MHS.HOUKOKUSHO_BUNRUI_CD = NEXT_MCB.HOUKOKUSHO_BUNRUI_CD AND NEXT_MCB.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND TMR.NEXT_HAIKI_KBN_CD != 4
	LEFT JOIN M_CHIIKIBETSU_SHOBUN NEXT_MCS ON NEXT_MCS.SHOBUN_HOUHOU_CD = NEXT_TMD.SBN_HOUHOU_CD AND NEXT_MCS.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND TMR.NEXT_HAIKI_KBN_CD != 4

    LEFT JOIN DT_R18_EX NEXT_DTR18EX ON NEXT_DTR18EX.SYSTEM_ID = TMR.NEXT_SYSTEM_ID AND NEXT_DTR18EX.DELETE_FLG = 0 AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN DT_R19_EX NEXT_DTR19EX ON NEXT_DTR19EX.SYSTEM_ID = TMR.NEXT_SYSTEM_ID AND NEXT_DTR19EX.DELETE_FLG = 0 AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN DT_R19 NEXT_DTR19 ON NEXT_DTR19.KANRI_ID = NEXT_DTR19EX.KANRI_ID AND NEXT_DTR19.SEQ = NEXT_DTR19EX.SEQ AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN DT_R18 NEXT_DTR18 ON NEXT_DTR18.KANRI_ID = NEXT_DTR18EX.KANRI_ID AND NEXT_DTR18EX.SEQ = NEXT_DTR18EX.SEQ AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN M_DENSHI_HAIKI_SHURUI NEXT_DEN_MDHS ON NEXT_DEN_MDHS.HAIKI_SHURUI_CD = (DTR18.HAIKI_DAI_CODE + DTR18.HAIKI_CHU_CODE + DTR18.HAIKI_SHO_CODE + DTR18.HAIKI_SAI_CODE) AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN M_CHIIKIBETSU_BUNRUI NEXT_DEN_MCB ON NEXT_DEN_MCB.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND NEXT_DEN_MCB.HOUKOKUSHO_BUNRUI_CD = NEXT_DEN_MDHS.HOUKOKUSHO_BUNRUI_CD AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN M_CHIIKIBETSU_SHOBUN NEXT_DEN_MCS ON NEXT_DEN_MCS.SHOBUN_HOUHOU_CD = NEXT_DTR18EX.SBN_HOUHOU_CD AND NEXT_DEN_MCS.CHIIKI_CD = /*data.TEISHUTSU_CHIIKI_CD*/0 AND TMR.NEXT_HAIKI_KBN_CD = 4
	LEFT JOIN M_UNIT MU ON MU.UNIT_CD = (SELECT MANI_KANSAN_KIHON_UNIT_CD FROM M_SYS_INFO) 

	LEFT JOIN M_GENBA HSTMGA ON HSTMGA.GYOUSHA_CD = DTR18EX.SBN_GYOUSHA_CD AND HSTMGA.GENBA_CD = DTR18EX.SBN_GENBA_CD
    LEFT JOIN M_CHIIKI HSTMC ON HSTMC.CHIIKI_CD = HSTMGA.CHIIKI_CD
    LEFT JOIN M_TODOUFUKEN HSTMT ON HSTMT.TODOUFUKEN_CD = HSTMC.TODOUFUKEN_CD

WHERE 1=1

	/*IF data.JIGYOUJOU_KBN.Value == 1*/
		AND ((DTR18.SBN_END_DATE IS NOT NULL
		    AND (DTR18.SBN_END_DATE <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_END.Value*/20140901, 111), 120) AND CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_BEGIN.Value*/20100901, 111), 120) <= DTR18.SBN_END_DATE)
		    AND DTR18.MANIFEST_KBN = 0
		     )
		     OR
			 (
			 DTR13.LAST_SBN_END_DATE IS NOT NULL
			 AND (DTR13.LAST_SBN_END_DATE <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_END.Value*/20140901, 111), 120) AND CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_BEGIN.Value*/20100901, 111), 120) <= DTR13.LAST_SBN_END_DATE)
			 )
		   )
		AND DTR18EX.SBN_GYOUSHA_CD = /*data.HOUKOKU_GYOUSHA_CD*/0
		AND DTR18EX.SBN_GENBA_CD = /*data.HOUKOKU_GENBA_CD*/0
	/*END*/

	/*IF data.JIGYOUJOU_KBN.Value == 2*/
		AND (DTR13.LAST_SBN_END_DATE <= CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_END.Value*/20140901, 111), 120) AND CONVERT(DATETIME, CONVERT(NVARCHAR, /*data.DATE_BEGIN.Value*/20100901, 111), 120) <= DTR13.LAST_SBN_END_DATE)
		AND DTR13.LAST_SBN_END_DATE IS NOT NULL
		AND DTR13EX.LAST_SBN_GYOUSHA_CD = /*data.HOUKOKU_GYOUSHA_CD*/0
		AND DTR13EX.LAST_SBN_GENBA_CD = /*data.HOUKOKU_GENBA_CD*/0
	/*END*/
/*END*/
ORDER BY SYSTEM_ID, SEQ, KANRI_ID, DEN_SEQ