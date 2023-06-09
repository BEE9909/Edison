--dq
SELECT		--¬p
	R18_EX.SYSTEM_ID											--VXeID@ñ\¦
	,R18_EX.SEQ											        --SEQ@ñ\¦
	,R18_MIX.DETAIL_SYSTEM_ID									--¾×VXeID@ñ\¦
	,4 AS HAIKI_KBN_CD											--pü¨æªCD@ñ\¦
	,'dq' AS HAIKI_KBN_NAME									--pü¨æª¼@\¦
	,'1' AS ISKONGOU                                            --¬æª
	,CASE
		WHEN R18.HIKIWATASHI_DATE = '' THEN NULL
		ELSE CONVERT(DATETIME, R18.HIKIWATASHI_DATE) END AS KOUFU_DATE	--ðtNú@\¦
	,R18.MANIFEST_ID AS MANIFEST_ID						        --ðtÔ@\¦
	--,R18_MIX.ROW_NO AS 's'										--UªsÔ@\¦
	,(R18_MIX.HAIKI_DAI_CODE + R18_MIX.HAIKI_CHU_CODE + R18_MIX.HAIKI_SHO_CODE) AS HAIKI_SHURUI_CD		--pü¨íÞCD@ñ\¦
	,CASE R18_MIX.HAIKI_SAI_CODE
		WHEN '000' THEN HAIKI_SHU.HAIKI_SHURUI_NAME
		ELSE HAIKI_SAI.HAIKI_SHURUI_NAME END AS HAIKI_SHURUI_NAME	--pü¨íÞ¼@\¦
	,HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD									--ñªÞCD@ñ\¦
	,HOU_BUN.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_NAME		--ñªÞ¼@\¦
	,R18_MIX.HAIKI_SUU AS HAIKI_SUU							    --pü¨ÌÊ@\¦
	,R18_MIX.HAIKI_UNIT_CD										--pü¨ÌÊPÊR[h@ñ\¦
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
	,HAIKI_UNIT.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME				--pü¨ÌÊPÊ¼@\¦
	,R18_MIX.KANSAN_SUU AS OLD_KANSAN_SUU					    --·ZãÊ(ÏXO)@\¦
	,R18_MIX.GENNYOU_SUU AS OLD_GENNYOU_SUU				        --¸eãÊ(ÏXO)@\¦
	,R18_EX.HST_GYOUSHA_CD										--roÆÒCD@ñ\¦
	,HST_GYOUSHA.GYOUSHA_NAME1 AS HST_GYOUSHA_NAME				--roÆÒ¼@\¦
	,R18_EX.HST_GENBA_CD										--roÆêCD@ñ\¦
	,HST_GENBA.GENBA_NAME1 AS HST_GENBA_NAME					--roÆê¼@\¦
	,R19_EX.UPN_GYOUSHA_CD										--(æÔ)^ÀÆÒCD@ñ\¦
	,UPN_GYOUSHA.GYOUSHA_NAME1 AS UPN_GYOUSHA_NAME			    --(æÔ)^ÀÆÒ¼@\¦
	,R18_EX.SBN_GYOUSHA_CD										--ªÆÒCD@ñ\¦
	,SBN_GYOUSHA.GYOUSHA_NAME1 AS SBN_GYOUSHA_NAME				--ªÆÒ¼@\¦
	,R18_MIX.HAIKI_NAME_CD										--pü¨¼ÌCD@ñ\¦
	,HAIKI_NAME.HAIKI_NAME AS HAIKI_NAME								--pü¨¼Ì@\¦
	,R18.NISUGATA_CODE AS NISUGATA_CD							--×pCD@ñ\¦
	,R18.NISUGATA_NAME AS NISUGATA_NAME							--×p¼@\¦
	,R18_MIX.SBN_HOUHOU_CD										--ªû@CD@ñ\¦
	,SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME			--ªû@¼@\¦
    ,R18.SBN_WAY_CODE AS R18_SBN_HOUHOU_CD						--ªû@CD@d
	,R18_SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS R18_SHOBUN_HOUHOU_NAME  --ªû@¼@d

    ,'' AS DEN_OLD_KANSAN_SUU                                       --d}j·ZOi [Êj@\¦
    ,'' AS DEN_OLD_KANSAN_UNIT_NAME                                       --·ZOPÊ@\¦
    --ñ}jðtÔ
	, '' AS NEXT_SYSTEM_ID
	, '' AS NEXT_HAIKI_KBN_CD

FROM
	DT_MF_TOC TOC
	INNER JOIN DT_R18 R18 
		ON TOC.KANRI_ID = R18.KANRI_ID AND TOC.LATEST_SEQ = R18.SEQ
	INNER JOIN DT_R18_MIX R18_MIX
		ON R18.KANRI_ID = R18_MIX.KANRI_ID AND R18_MIX.DELETE_FLG = 0
	INNER JOIN DT_R18_EX R18_EX 
		ON R18.KANRI_ID = R18_EX.KANRI_ID AND R18_EX.DELETE_FLG = 0
	INNER JOIN DT_R19 R19 
		ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ AND R19.UPN_ROUTE_NO = 1
	INNER JOIN DT_R19_EX R19_EX 
		ON R19.KANRI_ID = R19_EX.KANRI_ID AND R19_EX.UPN_ROUTE_NO = 1 AND R19_EX.DELETE_FLG = 0

	LEFT JOIN M_DENSHI_HAIKI_SHURUI HAIKI_SHU
		ON (R18_MIX.HAIKI_DAI_CODE + R18_MIX.HAIKI_CHU_CODE + R18_MIX.HAIKI_SHO_CODE) = HAIKI_SHU.HAIKI_SHURUI_CD
	LEFT JOIN M_DENSHI_HAIKI_SHURUI_SAIBUNRUI HAIKI_SAI
		ON R18.HST_SHA_EDI_MEMBER_ID = HAIKI_SAI.EDI_MEMBER_ID AND (R18_MIX.HAIKI_DAI_CODE + R18_MIX.HAIKI_CHU_CODE + R18_MIX.HAIKI_SHO_CODE + R18_MIX.HAIKI_SAI_CODE) = (HAIKI_SAI.HAIKI_SHURUI_CD + HAIKI_SAI.HAIKI_SHURUI_SAIBUNRUI_CD)
	LEFT JOIN M_HOUKOKUSHO_BUNRUI HOU_BUN
		ON HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = HOU_BUN.HOUKOKUSHO_BUNRUI_CD
	LEFT JOIN M_UNIT HAIKI_UNIT
		ON R18_MIX.HAIKI_UNIT_CD = HAIKI_UNIT.UNIT_CD
	LEFT JOIN M_GYOUSHA HST_GYOUSHA 
		ON R18_EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GENBA HST_GENBA 
		ON R18_EX.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND R18_EX.HST_GENBA_CD = HST_GENBA.GENBA_CD
	LEFT JOIN M_GYOUSHA UPN_GYOUSHA 
		ON R19_EX.UPN_GYOUSHA_CD = UPN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GYOUSHA SBN_GYOUSHA 
		ON R18_EX.SBN_GYOUSHA_CD = SBN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_SHOBUN_HOUHOU SBN_HOU
		ON R18_MIX.SBN_HOUHOU_CD = SBN_HOU.SHOBUN_HOUHOU_CD
    LEFT JOIN M_DENSHI_HAIKI_NAME AS HAIKI_NAME
        ON R18.HST_SHA_EDI_MEMBER_ID = HAIKI_NAME.EDI_MEMBER_ID
        AND R18_MIX.HAIKI_NAME_CD = HAIKI_NAME.HAIKI_NAME_CD
    LEFT JOIN M_SHOBUN_HOUHOU R18_SBN_HOU
	    ON CONVERT(nvarchar(3), R18.SBN_WAY_CODE) = R18_SBN_HOU.SHOBUN_HOUHOU_CD
	/*IF SearchInfo.DATE_KBN == 2*/
	INNER JOIN ( 
		SELECT DISTINCT KANRI_ID, SEQ 
		  FROM DT_R19
		 WHERE  
			UPN_END_DATE >= /*SearchInfo.DATE_FR*/ 
			AND UPN_END_DATE <= /*SearchInfo.DATE_TO*/
	) AS UNPAN ON TOC.KANRI_ID = UNPAN.KANRI_ID AND TOC.LATEST_SEQ = UNPAN.SEQ 
	/*END*/
WHERE
	TOC.STATUS_FLAG in (3,4)
	AND R18.CANCEL_FLAG = 0
	/*IF searchInfo.DATE_KBN == 1 && searchInfo.DATE_FR != ''*/
	AND R18.HIKIWATASHI_DATE >= /*searchInfo.DATE_FR*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 1 && searchInfo.DATE_TO != ''*/
	AND R18.HIKIWATASHI_DATE <= /*searchInfo.DATE_TO*/
	/*END*/	
	/*IF searchInfo.DATE_KBN == 3 && searchInfo.DATE_FR != ''*/
	AND R18.SBN_END_DATE >= /*searchInfo.DATE_FR*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 3 && searchInfo.DATE_TO != ''*/
	AND R18.SBN_END_DATE <= /*searchInfo.DATE_TO*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 4 && searchInfo.DATE_FR != ''*/
	AND R18.LAST_SBN_END_DATE >= /*searchInfo.DATE_FR*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 4 && searchInfo.DATE_TO != ''*/
	AND R18.LAST_SBN_END_DATE <= /*searchInfo.DATE_TO*/
	/*END*/
	/*IF searchInfo.HST_GYOUSHA_CD != ''*/
	AND R18_EX.HST_GYOUSHA_CD = /*searchInfo.HST_GYOUSHA_CD*/
	/*END*/
	/*IF searchInfo.HST_GENBA_CD != ''*/
	AND R18_EX.HST_GENBA_CD = /*searchInfo.HST_GENBA_CD*/
	/*END*/
	/*IF searchInfo.UPN_GYOUSHA_CD != ''*/
	AND R19_EX.UPN_GYOUSHA_CD = /*searchInfo.UPN_GYOUSHA_CD*/
	/*END*/
	/*IF searchInfo.SBN_GYOUSHA_CD != ''*/
	AND R18_EX.SBN_GYOUSHA_CD = /*searchInfo.SBN_GYOUSHA_CD*/
	/*END*/
	/*IF searchInfo.SBN_GENBA_CD != ''*/
	AND R18_EX.SBN_GENBA_CD = /*searchInfo.SBN_GENBA_CD*/
	/*END*/
	/*IF searchInfo.HOUKOKUSHO_BUNRUI_CD != ''*/
	AND HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = /*searchInfo.HOUKOKUSHO_BUNRUI_CD*/
	/*END*/
	/*IF (searchInfo.HAIKI_SHURUI_CD != '')*/
	AND R18_MIX.HAIKI_DAI_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,1,2)
	AND R18_MIX.HAIKI_CHU_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,3,1)
	AND R18_MIX.HAIKI_SHO_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,4,1)
	/*END*/
	/*IF searchInfo.HAIKI_NAME_CD != ''*/
	AND R18_MIX.HAIKI_NAME_CD = /*searchInfo.HAIKI_NAME_CD*/
	/*END*/
   /*IF searchInfo.SBN_HOUHOU_CD != ''*/
    AND R18_MIX.SBN_HOUHOU_CD = /*searchInfo.SBN_HOUHOU_CD*/
    /*END*/
    /*IF searchInfo.SHOBUN_CHECK && searchInfo.SBN_HOUHOU_CD == '' */
    AND (R18_MIX.SBN_HOUHOU_CD IS NULL OR R18_MIX.SBN_HOUHOU_CD = '')
    /*END*/
ORDER BY
	R18.HIKIWATASHI_DATE
	,R18.MANIFEST_ID
	,R18_MIX.ROW_NO
