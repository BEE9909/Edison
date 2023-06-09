--
SELECT
	MANI_ENT.SYSTEM_ID												--SYSTEM_ID@ñ\¦
	,MANI_ENT.SEQ												    --SEQ@ñ\¦
	,MANI_DET.DETAIL_SYSTEM_ID										--DETAIL_SYSTEM_ID@ñ\¦
	,MANI_ENT.HAIKI_KBN_CD											--pü¨æªCD@ñ\¦
	,'0' AS ISKONGOU                                                --¬æª
	,HAIKI_KBN.HAIKI_KBN_NAME AS HAIKI_KBN_NAME						--pü¨æª¼@\¦
	,MANI_ENT.KOUFU_DATE AS KOUFU_DATE				    			--ðtNú@\¦
	,MANI_ENT.MANIFEST_ID AS MANIFEST_ID			    			--ðtÔ@\¦
	,MANI_DET.HAIKI_SHURUI_CD										--pü¨íÞCD@ñ\¦
	,HAIKI_SHU.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI_NAME			--pü¨íÞ¼@\¦
	,HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD									--ñªÞCD@ñ\¦
	,HOU_BUN.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_NAME	--ñªÞ¼@\¦
	,MANI_DET.HAIKI_SUU AS HAIKI_SUU								--pü¨ÌÊ@\¦
	,MANI_DET.HAIKI_UNIT_CD											--pü¨ÌÊPÊR[h@ñ\¦
	-- ·ZpÊÆPÊCDiñ¬dqpAñ\¦j start@
	,'' AS HAIKI_KAKUTEI_SUU						            --pü¨ÌmèÊ
	,'' AS HAIKI_KAKUTEI_UNIT_CODE			                    --pü¨ÌmèÊPÊR[h@

	,'' AS SU1_UPN_SHA_EDI_PASSWORD                             -- ÁüÒîñ}X^(ûW^À1).EDI_EDI_PASSWORD
	,'' AS SU1_UPN_SUU                                          -- ^ÀÊ(ûW^À1)
	,'' AS SU1_UPN_UNIT_CODE                                    -- ^ÀPÊR[h(ûW^À1)

	,'' AS SU2_UPN_SHA_EDI_PASSWORD                             -- ÁüÒîñ}X^(ûW^À2).EDI_EDI_PASSWORD
	,'' AS SU2_UPN_SUU                                          -- ^ÀÊ(ûW^À1)
	,'' AS SU2_UPN_UNIT_CODE                                    -- ^ÀPÊR[h(ûW^À2)

	,'' AS SU3_UPN_SHA_EDI_PASSWORD                             -- ÁüÒîñ}X^(ûW^À3).EDI_EDI_PASSWORD
	,'' AS SU3_UPN_SUU                                          -- ^ÀÊ(ûW^À3)
	,'' AS SU3_UPN_UNIT_CODE                                    -- ^ÀPÊR[h(ûW^À3)

	,'' AS SU4_UPN_SHA_EDI_PASSWORD                             -- ÁüÒîñ}X^(ûW^À4).EDI_EDI_PASSWORD
	,'' AS SU4_UPN_SUU                                          -- ^ÀÊ(ûW^À4)
	,'' AS SU4_UPN_UNIT_CODE                                    -- ^ÀPÊR[h(ûW^À4)

	,'' AS SU5_UPN_SHA_EDI_PASSWORD                             -- ÁüÒîñ}X^(ûW^À5).EDI_EDI_PASSWORD
	,'' AS SU5_UPN_SUU                                          -- ^ÀÊ(ûW^À5)
	,'' AS SU5_UPN_UNIT_CODE                                    -- ^ÀPÊR[h(ûW^À5)

	,'' AS RECEPT_SUU                                           -- ªÊ
	,'' AS RECEPT_UNIT_CODE                                     -- ªPÊR[h
	-- ·ZpÊÆPÊCD end
	,HAIKI_UNIT.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME					--pü¨ÌÊPÊ¼@\¦
	,MANI_DET.KANSAN_SUU AS OLD_KANSAN_SUU							--·ZãÊ(ÏXO)@\¦
	,MANI_DET.GENNYOU_SUU AS OLD_GENNYOU_SUU						--¸eãÊ(ÏXO)@\¦
	,MANI_ENT.HST_GYOUSHA_CD										--roÆÒCD@ñ\¦
	,HST_GYOUSHA.GYOUSHA_NAME_RYAKU AS HST_GYOUSHA_NAME				--roÆÒ¼@\¦
	,MANI_ENT.HST_GENBA_CD											--roÆêCD@ñ\¦
	,HST_GENBA.GENBA_NAME_RYAKU AS HST_GENBA_NAME					--roÆê¼@\¦
	,MANI_UPN.UPN_GYOUSHA_CD							   			--(æÔ)^ÀÆÒCD@ñ\¦
	,UPN_GYOUSHA.GYOUSHA_NAME_RYAKU AS UPN_GYOUSHA_NAME				--(æÔ)^ÀÆÒ¼@\¦
	,MANI_ENT.SBN_GYOUSHA_CD										--ªÆÒCD@ñ\¦
	,SBN_GYOUSHA.GYOUSHA_NAME_RYAKU AS SBN_GYOUSHA_NAME					--ªÆÒ¼@\¦
	,MANI_DET.HAIKI_NAME_CD											--pü¨¼ÌCD@ñ\¦
	,HAIKI_NAME.HAIKI_NAME AS HAIKI_NAME							--pü¨¼Ì@\¦
	,MANI_DET.NISUGATA_CD											--×pCD@ñ\¦
	,NISUGATA.NISUGATA_NAME AS NISUGATA_NAME						--×p¼@\¦
	,MANI_DET.SBN_HOUHOU_CD											--ªû@CD@ñ\¦
	,SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME				--ªû@¼@\¦
    ,'' AS DEN_OLD_KANSAN_SUU                                       --d}j·ZOi [Êj@\¦
    ,'' AS DEN_OLD_KANSAN_UNIT_NAME                                       --·ZOPÊ@\¦
     --ñ}jðtÔ
	, TMR.MANIFEST_ID AS NEXT_SYSTEM_ID
	, TMR.NEXT_HAIKI_KBN_CD AS NEXT_HAIKI_KBN_CD
FROM
	T_MANIFEST_ENTRY MANI_ENT
	LEFT JOIN T_MANIFEST_DETAIL MANI_DET 
		ON MANI_ENT.SYSTEM_ID = MANI_DET.SYSTEM_ID AND MANI_ENT.SEQ = MANI_DET.SEQ
	LEFT JOIN T_MANIFEST_UPN MANI_UPN 
		ON MANI_ENT.SYSTEM_ID = MANI_UPN.SYSTEM_ID AND MANI_ENT.SEQ = MANI_UPN.SEQ AND MANI_UPN.UPN_ROUTE_NO = 1	
	LEFT JOIN M_HAIKI_KBN HAIKI_KBN 
		ON MANI_ENT.HAIKI_KBN_CD = HAIKI_KBN.HAIKI_KBN_CD
	LEFT JOIN M_HAIKI_SHURUI HAIKI_SHU 
		ON MANI_ENT.HAIKI_KBN_CD = HAIKI_SHU.HAIKI_KBN_CD AND MANI_DET.HAIKI_SHURUI_CD = HAIKI_SHU.HAIKI_SHURUI_CD 
	LEFT JOIN M_HOUKOKUSHO_BUNRUI HOU_BUN 
		ON HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = HOU_BUN.HOUKOKUSHO_BUNRUI_CD
	LEFT JOIN M_UNIT HAIKI_UNIT 
		ON MANI_DET.HAIKI_UNIT_CD = HAIKI_UNIT.UNIT_CD
	LEFT JOIN M_GYOUSHA HST_GYOUSHA 
		ON MANI_ENT.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GENBA HST_GENBA 
		ON MANI_ENT.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND MANI_ENT.HST_GENBA_CD = HST_GENBA.GENBA_CD
	LEFT JOIN M_GYOUSHA UPN_GYOUSHA 
		ON MANI_UPN.UPN_GYOUSHA_CD = UPN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GYOUSHA SBN_GYOUSHA 
		ON MANI_ENT.SBN_GYOUSHA_CD = SBN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_HAIKI_NAME HAIKI_NAME 
		ON MANI_DET.HAIKI_NAME_CD = HAIKI_NAME.HAIKI_NAME_CD
	LEFT JOIN M_NISUGATA NISUGATA 
		ON MANI_DET.NISUGATA_CD = NISUGATA.NISUGATA_CD
	LEFT JOIN M_SHOBUN_HOUHOU SBN_HOU 
		ON MANI_DET.SBN_HOUHOU_CD = SBN_HOU.SHOBUN_HOUHOU_CD
    LEFT JOIN
	(
	  SELECT COL_TMR.NEXT_SYSTEM_ID
         , COL_TMR.SEQ
         , COL_TMR.REC_SEQ
         , COL_TMR.NEXT_HAIKI_KBN_CD
         , COL_TMR.FIRST_SYSTEM_ID
         , COL_TMR.FIRST_HAIKI_KBN_CD
         , COL_TMR.DELETE_FLG
         , COL_TMR.TIME_STAMP
		 --ñ}jðtÔ
		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.MANIFEST_ID
		        WHEN 2 THEN COL_TME.MANIFEST_ID
		        WHEN 3 THEN COL_TME.MANIFEST_ID
				WHEN 4 THEN COL_DR18E.MANIFEST_ID
                ELSE ''
           END AS MANIFEST_ID

      FROM T_MANIFEST_RELATION AS COL_TMR WITH(NOLOCK)
     INNER JOIN (
        SELECT NEXT_SYSTEM_ID
    	     , MAX(SEQ) AS SEQ
          FROM T_MANIFEST_RELATION WITH(NOLOCK)  
         WHERE DELETE_FLG = 'false' 
    	 GROUP BY NEXT_SYSTEM_ID
           ) AS MAX_TMR
        ON COL_TMR.NEXT_SYSTEM_ID = MAX_TMR.NEXT_SYSTEM_ID 
       AND COL_TMR.SEQ = MAX_TMR.SEQ 

	 --}j START
	 LEFT OUTER JOIN (
		SELECT DISTINCT COL2_TME.SYSTEM_ID 
		     , COL2_TME.SEQ 
			 , COL2_TMD.DETAIL_SYSTEM_ID 
		     , COL2_TME.MANIFEST_ID 
		  FROM T_MANIFEST_ENTRY AS COL2_TME WITH(NOLOCK)
		 INNER JOIN (
			 SELECT SYSTEM_ID
				  , MAX(SEQ) AS SEQ
			   FROM T_MANIFEST_ENTRY AS COL_TME WITH(NOLOCK)
			  WHERE DELETE_FLG = 'false'
			  GROUP BY SYSTEM_ID
			   ) AS MAX2_TME
			ON COL2_TME.SYSTEM_ID = MAX2_TME.SYSTEM_ID 
		   AND COL2_TME.SEQ = MAX2_TME.SEQ 
          LEFT OUTER JOIN (
			SELECT SYSTEM_ID
				 , DETAIL_SYSTEM_ID 
			     , SEQ
			     , MAX(LAST_SBN_END_DATE) AS LAST_SBN_END_DATE		
			  FROM T_MANIFEST_DETAIL 
			 GROUP BY SYSTEM_ID
			     , SEQ
				 , DETAIL_SYSTEM_ID 
		  ) AS COL2_TMD 
		    ON COL2_TME.SYSTEM_ID = COL2_TMD.SYSTEM_ID
		   AND COL2_TME.SEQ = COL2_TMD.SEQ
		  LEFT OUTER JOIN M_GYOUSHA AS MG2 WITH(NOLOCK)
			ON COL2_TME.LAST_SBN_GYOUSHA_CD = MG2.GYOUSHA_CD
		   AND MG2.DELETE_FLG = 'false'
		  LEFT OUTER JOIN M_GENBA AS MGA2 WITH(NOLOCK) 
			ON COL2_TME.LAST_SBN_GYOUSHA_CD = MGA2.GYOUSHA_CD 
		   AND COL2_TME.LAST_SBN_GENBA_CD = MGA2.GENBA_CD
		   AND MGA2.DELETE_FLG = 'false'
		 WHERE COL2_TME.DELETE_FLG = 'false'
          )COL_TME
       ON COL_TMR.NEXT_SYSTEM_ID = COL_TME.DETAIL_SYSTEM_ID 
	 --}j END

	  --dq}j START
     LEFT OUTER JOIN (
		SELECT DISTINCT COL_DR18E.SYSTEM_ID 
			 , COL_DR18E.SEQ 
			 , COL_DR18E.MANIFEST_ID 
		  FROM DT_R18_EX AS COL_DR18E WITH(NOLOCK)
		 INNER JOIN (
			SELECT SYSTEM_ID
			     , MAX(SEQ) AS SEQ
			  FROM DT_R18_EX WITH(NOLOCK)
			 WHERE DELETE_FLG = 'false'
			 GROUP BY SYSTEM_ID
		 ) MAX_DR18E
		    ON COL_DR18E.SYSTEM_ID = MAX_DR18E.SYSTEM_ID
		   AND COL_DR18E.SEQ = MAX_DR18E.SEQ
		 INNER JOIN DT_MF_TOC AS DMT WITH(NOLOCK)
		    ON COL_DR18E.KANRI_ID = DMT.KANRI_ID 
		   AND COL_DR18E.MANIFEST_ID = DMT.MANIFEST_ID 
		 INNER JOIN DT_R18 AS DR18 WITH(NOLOCK)
		    ON DMT.KANRI_ID = DR18.KANRI_ID 
		   AND DMT.LATEST_SEQ = DR18.SEQ 
		  LEFT OUTER JOIN DT_R13_EX AS DR13E WITH(NOLOCK)
		    ON COL_DR18E.SYSTEM_ID = DR13E.SYSTEM_ID
		   AND COL_DR18E.SEQ = DR13E.SEQ
		  LEFT OUTER JOIN M_GYOUSHA AS MG3 WITH(NOLOCK)
			ON DR13E.LAST_SBN_GYOUSHA_CD = MG3.GYOUSHA_CD
		   AND MG3.DELETE_FLG = 'false'
		  LEFT OUTER JOIN M_GENBA AS MGA3 WITH(NOLOCK) 
			ON DR13E.LAST_SBN_GYOUSHA_CD = MGA3.GYOUSHA_CD 
		   AND DR13E.LAST_SBN_GENBA_CD = MGA3.GENBA_CD
		   AND MGA3.DELETE_FLG = 'false'
		 WHERE COL_DR18E.DELETE_FLG = 'false'
        )COL_DR18E
     ON COL_TMR.NEXT_SYSTEM_ID = COL_DR18E.SYSTEM_ID 
     WHERE COL_TMR.DELETE_FLG = 'false' 
	) TMR 
	ON MANI_DET.DETAIL_SYSTEM_ID = TMR.FIRST_SYSTEM_ID 
    AND TMR.FIRST_HAIKI_KBN_CD <> 4 
	/*IF SearchInfo.DATE_KBN == 2*/
	INNER JOIN ( 
		SELECT DISTINCT SYSTEM_ID, SEQ 
		  FROM T_MANIFEST_UPN
		 WHERE  
			UPN_END_DATE >= /*SearchInfo.DATE_FR*/ 
			AND UPN_END_DATE <= /*SearchInfo.DATE_TO*/
	) AS UNPAN ON MANI_ENT.SYSTEM_ID = UNPAN.SYSTEM_ID AND MANI_ENT.SEQ = UNPAN.SEQ 
	/*END*/
WHERE
	MANI_ENT.DELETE_FLG = 0
	/*IF SearchInfo.HOUKOKUSHO_BUNRUI_CD != ''*/
	AND HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = /*SearchInfo.HOUKOKUSHO_BUNRUI_CD*/
	/*END*/
	/*IF SearchInfo.HAIKI_KBN_CD != ''*/
	AND MANI_ENT.HAIKI_KBN_CD = /*SearchInfo.HAIKI_KBN_CD*/
	/*END*/
	/*IF SearchInfo.KYOTEN_CD != ''*/
	AND MANI_ENT.KYOTEN_CD = /*SearchInfo.KYOTEN_CD*/
	/*END*/
	/*IF SearchInfo.DATE_KBN == 1 && SearchInfo.DATE_FR != ''*/
	AND MANI_ENT.KOUFU_DATE >= /*SearchInfo.DATE_FR*/
	/*END*/
	/*IF SearchInfo.DATE_KBN == 1 && SearchInfo.DATE_TO != ''*/
	AND MANI_ENT.KOUFU_DATE <= /*SearchInfo.DATE_TO*/
	/*END*/
	/*IF SearchInfo.HST_GYOUSHA_CD != ''*/
	AND MANI_ENT.HST_GYOUSHA_CD = /*SearchInfo.HST_GYOUSHA_CD*/
	/*END*/
	/*IF SearchInfo.HST_GENBA_CD != ''*/
	AND MANI_ENT.HST_GENBA_CD = /*SearchInfo.HST_GENBA_CD*/
	/*END*/
	/*IF SearchInfo.SBN_GYOUSHA_CD != ''*/
	AND MANI_ENT.SBN_GYOUSHA_CD = /*SearchInfo.SBN_GYOUSHA_CD*/
	/*END*/
	/*IF SearchInfo.SBN_GENBA_CD != '' && SearchInfo.HAIKI_KBN_CD == ''*/
	AND EXISTS (SELECT * FROM T_MANIFEST_UPN TMU_CONDITION 
	WHERE ((MANI_ENT.HAIKI_KBN_CD <> 3 AND TMU_CONDITION.UPN_SAKI_GENBA_CD = /*SearchInfo.SBN_GENBA_CD*/ ) OR ( MANI_ENT.HAIKI_KBN_CD = 3 AND TMU_CONDITION.UPN_SAKI_KBN = 1 AND TMU_CONDITION.UPN_SAKI_GENBA_CD =/*SearchInfo.SBN_GENBA_CD*/ ) ) 
	AND TMU_CONDITION.SYSTEM_ID = MANI_ENT.SYSTEM_ID
	AND TMU_CONDITION.SEQ = MANI_ENT.SEQ)
	/*END*/
	/*IF SearchInfo.SBN_GENBA_CD != '' && SearchInfo.HAIKI_KBN_CD != '3' && SearchInfo.HAIKI_KBN_CD != ''*/
	AND EXISTS (SELECT * FROM T_MANIFEST_UPN TMU_CONDITION 
	WHERE TMU_CONDITION.UPN_SAKI_GENBA_CD = /*SearchInfo.SBN_GENBA_CD*/ 
	AND TMU_CONDITION.SYSTEM_ID = MANI_ENT.SYSTEM_ID
	AND TMU_CONDITION.SEQ = MANI_ENT.SEQ)
	/*END*/
	/*IF SearchInfo.SBN_GENBA_CD != '' && SearchInfo.HAIKI_KBN_CD == '3'*/
	AND EXISTS (SELECT * FROM T_MANIFEST_UPN TMU_CONDITION 
	WHERE TMU_CONDITION.UPN_SAKI_GENBA_CD = /*SearchInfo.SBN_GENBA_CD*/ 
	AND TMU_CONDITION.UPN_SAKI_KBN = 1
	AND TMU_CONDITION.SYSTEM_ID = MANI_ENT.SYSTEM_ID
	AND TMU_CONDITION.SEQ = MANI_ENT.SEQ)
	/*END*/
	/*IF SearchInfo.UPN_GYOUSHA_CD != ''*/
	AND MANI_UPN.UPN_GYOUSHA_CD = /*SearchInfo.UPN_GYOUSHA_CD*/
	/*END*/
	/*IF SearchInfo.DATE_KBN == 3 && SearchInfo.DATE_FR != ''*/
	AND MANI_DET.SBN_END_DATE >= /*SearchInfo.DATE_FR*/ 
	/*END*/
	/*IF SearchInfo.DATE_KBN == 3 && SearchInfo.DATE_TO != ''*/
	AND MANI_DET.SBN_END_DATE <= /*SearchInfo.DATE_TO*/
	/*END*/
	/*IF SearchInfo.DATE_KBN == 4 && SearchInfo.DATE_FR != ''*/
	AND MANI_DET.LAST_SBN_END_DATE >= /*SearchInfo.DATE_FR*/ 
	/*END*/
	/*IF SearchInfo.DATE_KBN == 4 && SearchInfo.DATE_TO != ''*/
	AND MANI_DET.LAST_SBN_END_DATE <= /*SearchInfo.DATE_TO*/
	/*END*/
	/*IF SearchInfo.HAIKI_SHURUI_CD != ''*/
	AND MANI_DET.HAIKI_SHURUI_CD = /*SearchInfo.HAIKI_SHURUI_CD*/
	/*END*/
	/*IF SearchInfo.HAIKI_NAME_CD != ''*/
	AND MANI_DET.HAIKI_NAME_CD = /*SearchInfo.HAIKI_NAME_CD*/
	/*END*/
   /*IF searchInfo.SBN_HOUHOU_CD != ''*/
    AND MANI_DET.SBN_HOUHOU_CD = /*searchInfo.SBN_HOUHOU_CD*/
    /*END*/
    /*IF searchInfo.SHOBUN_CHECK && searchInfo.SBN_HOUHOU_CD == '' */
    AND (MANI_DET.SBN_HOUHOU_CD IS NULL OR MANI_DET.SBN_HOUHOU_CD = '')
    /*END*/
ORDER BY
    MANI_ENT.HAIKI_KBN_CD
	,MANI_ENT.KOUFU_DATE
	,MANI_ENT.MANIFEST_ID
