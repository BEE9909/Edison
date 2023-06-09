--dq
SELECT		--ñ¬p
	R18_EX.SYSTEM_ID											--VXeID@ñ\¦
	,R18_EX.SEQ											        --SEQ@ñ\¦
	,'' AS DETAIL_SYSTEM_ID										--¾×VXeID@ñ\¦
	,4 AS HAIKI_KBN_CD											--pü¨æªCD@ñ\¦
	,'dq' AS HAIKI_KBN_NAME									--pü¨æª¼@\¦
	,'0' AS ISKONGOU                                            --¬æª
	,CASE
		WHEN R18.HIKIWATASHI_DATE = '' THEN NULL
		ELSE CONVERT(DATETIME, R18.HIKIWATASHI_DATE) END AS KOUFU_DATE	--ðtNú@\¦
	,R18.MANIFEST_ID AS MANIFEST_ID						        --ðtÔ@\¦
	,(R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE) AS HAIKI_SHURUI_CD		--pü¨íÞCD@ñ\¦
	,CASE R18.HAIKI_SAI_CODE
		WHEN '000' THEN HAIKI_SHU.HAIKI_SHURUI_NAME
		ELSE HAIKI_SAI.HAIKI_SHURUI_NAME END AS HAIKI_SHURUI_NAME	--pü¨íÞ¼@\¦
	,HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD								--ñªÞCD@ñ\¦
	,HOU_BUN.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_NAME	--ñªÞ¼@\¦
	,R18.HAIKI_SUU AS HAIKI_SUU						                --pü¨ÌÊ@\¦
	,R18.HAIKI_UNIT_CODE AS HAIKI_UNIT_CD				            --pü¨ÌÊPÊR[h@ñ\¦
	-- ·ZpÊÆPÊCDiñ¬dqpAñ\¦j start@
	,R18.HAIKI_KAKUTEI_SUU AS HAIKI_KAKUTEI_SUU						--pü¨ÌmèÊ
	,R18.HAIKI_KAKUTEI_UNIT_CODE AS HAIKI_KAKUTEI_UNIT_CODE			--pü¨ÌmèÊPÊR[h@

	,MEMBER_SU1.EDI_PASSWORD AS SU1_UPN_SHA_EDI_PASSWORD            -- ÁüÒîñ}X^(ûW^À1).EDI_EDI_PASSWORD
	,R19_SU1.UPN_SUU AS SU1_UPN_SUU                                 -- ^ÀÊ(ûW^À1)
	,R19_SU1.UPN_UNIT_CODE AS SU1_UPN_UNIT_CODE                     -- ^ÀPÊR[h(ûW^À1)

	,MEMBER_SU2.EDI_PASSWORD AS SU2_UPN_SHA_EDI_PASSWORD            -- ÁüÒîñ}X^(ûW^À2).EDI_EDI_PASSWORD
	,R19_SU2.UPN_SUU AS SU2_UPN_SUU                                 -- ^ÀÊ(ûW^À1)
	,R19_SU2.UPN_UNIT_CODE AS SU2_UPN_UNIT_CODE                     -- ^ÀPÊR[h(ûW^À2)

	,MEMBER_SU3.EDI_PASSWORD AS SU3_UPN_SHA_EDI_PASSWORD            -- ÁüÒîñ}X^(ûW^À3).EDI_EDI_PASSWORD
	,R19_SU3.UPN_SUU AS SU3_UPN_SUU                                 -- ^ÀÊ(ûW^À3)
	,R19_SU3.UPN_UNIT_CODE AS SU3_UPN_UNIT_CODE                     -- ^ÀPÊR[h(ûW^À3)

	,MEMBER_SU4.EDI_PASSWORD AS SU4_UPN_SHA_EDI_PASSWORD            -- ÁüÒîñ}X^(ûW^À4).EDI_EDI_PASSWORD
	,R19_SU4.UPN_SUU AS SU4_UPN_SUU                                 -- ^ÀÊ(ûW^À4)
	,R19_SU4.UPN_UNIT_CODE AS SU4_UPN_UNIT_CODE                     -- ^ÀPÊR[h(ûW^À4)

	,MEMBER_SU5.EDI_PASSWORD AS SU5_UPN_SHA_EDI_PASSWORD            -- ÁüÒîñ}X^(ûW^À5).EDI_EDI_PASSWORD
	,R19_SU5.UPN_SUU AS SU5_UPN_SUU                                 -- ^ÀÊ(ûW^À5)
	,R19_SU5.UPN_UNIT_CODE AS SU5_UPN_UNIT_CODE                     -- ^ÀPÊR[h(ûW^À5)

	,R18.RECEPT_SUU AS RECEPT_SUU                                   -- ªÊ
	,R18.RECEPT_UNIT_CODE AS RECEPT_UNIT_CODE                       -- ªPÊR[h
	-- ·ZpÊÆPÊCD end
	,HAIKI_UNIT.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME					--pü¨ÌÊPÊ¼@\¦
	,R18_EX.KANSAN_SUU AS OLD_KANSAN_SUU					    --·ZãÊ(ÏXO)@\¦
	,R18_EX.GENNYOU_SUU AS OLD_GENNYOU_SUU					    --¸eãÊ(ÏXO)@\¦
	,R18_EX.HST_GYOUSHA_CD										--roÆÒCD@ñ\¦
	,HST_GYOUSHA.GYOUSHA_NAME_RYAKU AS HST_GYOUSHA_NAME			--roÆÒ¼@\¦
	,R18_EX.HST_GENBA_CD										--roÆêCD@ñ\¦
	,HST_GENBA.GENBA_NAME_RYAKU AS HST_GENBA_NAME				--roÆê¼@\¦
	,R19_EX.UPN_GYOUSHA_CD										--(æÔ)^ÀÆÒCD@ñ\¦
	,UPN_GYOUSHA.GYOUSHA_NAME_RYAKU AS UPN_GYOUSHA_NAME			--(æÔ)^ÀÆÒ¼@\¦
	,R18_EX.SBN_GYOUSHA_CD										--ªÆÒCD@ñ\¦
	,SBN_GYOUSHA.GYOUSHA_NAME_RYAKU AS SBN_GYOUSHA_NAME			--ªÆÒ¼@\¦
	,R18_EX.HAIKI_NAME_CD										--pü¨¼ÌCD@ñ\¦
	,R18.HAIKI_NAME AS HAIKI_NAME								--pü¨¼Ì@\¦
	,R18.NISUGATA_CODE AS NISUGATA_CD							--×pCD@ñ\¦
	,R18.NISUGATA_NAME AS NISUGATA_NAME							--×p¼@\¦
	,R18_EX.SBN_HOUHOU_CD AS SBN_HOUHOU_CD						--ªû@CD@ñ\¦
	,SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME			--ªû@¼@\¦
    ,R18.SBN_WAY_CODE AS R18_SBN_HOUHOU_CD						--ªû@CD@d
	,R18_SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS R18_SHOBUN_HOUHOU_NAME  --ªû@¼@d

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 1*/
	,CASE
		WHEN R18.HAIKI_KAKUTEI_SUU IS NULL THEN R18.HAIKI_SUU
		ELSE R18.HAIKI_KAKUTEI_SUU END AS DEN_OLD_KANSAN_SUU	--d}j·ZOi [Êj@\¦
	,CASE
		WHEN R18.HAIKI_KAKUTEI_SUU IS NULL THEN HAIKI_UNIT.UNIT_NAME_RYAKU
		ELSE HAIKI_KAKUTEI_UNIT.UNIT_NAME_RYAKU END AS DEN_OLD_KANSAN_UNIT_NAME	--·ZOPÊ@\¦
	/*END*/

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 2*/
	,R18.HAIKI_SUU AS DEN_OLD_KANSAN_SUU                       --d}j·ZOi [Êj@\¦
	,HAIKI_UNIT.UNIT_NAME_RYAKU AS DEN_OLD_KANSAN_UNIT_NAME          --·ZOPÊ@\¦
	/*END*/

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 3*/
	,CASE 
	 WHEN R19_SU5.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
	  CASE
	  WHEN R19_SU4.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
		  CASE
		  WHEN R19_SU3.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
			  CASE
		      WHEN R19_SU2.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
				  CASE
		          WHEN R19_SU1.UPN_SHA_EDI_MEMBER_ID IS NULL THEN ''
		          ELSE R19_SU1.UPN_SUU END
		      ELSE R19_SU2.UPN_SUU END
		  ELSE R19_SU3.UPN_SUU END
	   ELSE R19_SU4.UPN_SUU END
	 ELSE R19_SU5.UPN_SUU END AS DEN_OLD_KANSAN_SUU	            --d}j·ZOi [Êj@\¦

	,CASE
	 WHEN R19_SU5.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
		  CASE
		  WHEN R19_SU4.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
			  CASE
		      WHEN R19_SU3.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
				  CASE
		          WHEN R19_SU2.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
					 CASE
		             WHEN R19_SU1.UPN_SHA_EDI_MEMBER_ID IS NULL THEN ''
		             ELSE SU1_UPN_UNIT.UNIT_NAME_RYAKU END
		          ELSE SU2_UPN_UNIT.UNIT_NAME_RYAKU END
		      ELSE SU3_UPN_UNIT.UNIT_NAME_RYAKU END
		  ELSE SU4_UPN_UNIT.UNIT_NAME_RYAKU END
	 ELSE SU5_UPN_UNIT.UNIT_NAME_RYAKU END AS DEN_OLD_KANSAN_UNIT_NAME	   --·ZOPÊ@\¦
	/*END*/

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 4*/
	,R18.RECEPT_SUU AS DEN_OLD_KANSAN_SUU                       --d}j·ZOi [Êj@\¦
	,RECEPT_UNIT.UNIT_NAME_RYAKU AS DEN_OLD_KANSAN_UNIT_NAME          --·ZOPÊ@\¦
	/*END*/

     --ñ}jðtÔ
	, '' AS NEXT_SYSTEM_ID
	, '' AS NEXT_HAIKI_KBN_CD

FROM
	DT_MF_TOC TOC
	INNER JOIN DT_R18 R18
		ON TOC.KANRI_ID = R18.KANRI_ID AND TOC.LATEST_SEQ = R18.SEQ
	INNER JOIN DT_R18_EX R18_EX
		ON R18.KANRI_ID = R18_EX.KANRI_ID AND R18_EX.DELETE_FLG = 0
	INNER JOIN DT_R19 R19
		ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ AND R19.UPN_ROUTE_NO = 1
	INNER JOIN DT_R19_EX R19_EX
		ON R19.KANRI_ID = R19_EX.KANRI_ID AND R19_EX.UPN_ROUTE_NO = 1 AND R19_EX.DELETE_FLG = 0

	LEFT JOIN M_DENSHI_HAIKI_SHURUI HAIKI_SHU
		ON (R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE) = HAIKI_SHU.HAIKI_SHURUI_CD
	LEFT JOIN M_HOUKOKUSHO_BUNRUI HOU_BUN 
		ON HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = HOU_BUN.HOUKOKUSHO_BUNRUI_CD
	LEFT JOIN M_DENSHI_HAIKI_SHURUI_SAIBUNRUI HAIKI_SAI
		ON R18.HST_SHA_EDI_MEMBER_ID = HAIKI_SAI.EDI_MEMBER_ID AND (R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE + R18.HAIKI_SAI_CODE) = (HAIKI_SAI.HAIKI_SHURUI_CD + HAIKI_SAI.HAIKI_SHURUI_SAIBUNRUI_CD)
	LEFT JOIN M_UNIT HAIKI_UNIT
		ON R18.HAIKI_UNIT_CODE = HAIKI_UNIT.UNIT_CD
	LEFT JOIN M_GYOUSHA HST_GYOUSHA
		ON R18_EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GENBA HST_GENBA
		ON R18_EX.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND R18_EX.HST_GENBA_CD = HST_GENBA.GENBA_CD
	LEFT JOIN M_GYOUSHA UPN_GYOUSHA
		ON R19_EX.UPN_GYOUSHA_CD = UPN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GYOUSHA SBN_GYOUSHA
		ON R18_EX.SBN_GYOUSHA_CD = SBN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_SHOBUN_HOUHOU SBN_HOU
		ON R18_EX.SBN_HOUHOU_CD = SBN_HOU.SHOBUN_HOUHOU_CD
    LEFT JOIN M_SHOBUN_HOUHOU R18_SBN_HOU
	    ON CONVERT(nvarchar(3), R18.SBN_WAY_CODE) = R18_SBN_HOU.SHOBUN_HOUHOU_CD

  --LEFT JOIN ûW^Àîñ(ûW^À1)
  --  ON }jtFXgîñ.ÇÔ@@ûW^Àîñ(ûW^À1).ÇÔ
  --  AND }jtFXgîñ.}Ô@@ûW^Àîñ(ûW^À1).}Ô
  --  AND ûW^Àîñ(ûW^À1).æÔÔ@@1(æÔ1)
  LEFT JOIN DT_R19 R19_SU1 ON R18.KANRI_ID = R19_SU1.KANRI_ID
                          AND R18.SEQ = R19_SU1.SEQ
					      AND R19_SU1.UPN_ROUTE_NO = 1
  --LEFT JOIN dqÆÒ}X^(ûW^À1)
  --  ON ûW^Àîñ(ûW^À1).ûW^ÀÆÒÁüÒÔ@@dqÆÒ}X^(ûW^À1).ÁüÒÔ
  --  AND dqÆÒ}X^(ûW^À1).^ÀÆÒæª@@1(ûW^ÀÆÒ)
  LEFT JOIN M_DENSHI_JIGYOUSHA  JIGYOUSHA_SU1 ON R19_SU1.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU1.EDI_MEMBER_ID
                                             AND JIGYOUSHA_SU1.UPN_KBN = 'True'

  -- LEFT JOIN ÁüÒîñ}X^(ûW^À1)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU1 ON JIGYOUSHA_SU1.EDI_MEMBER_ID = MEMBER_SU1.EDI_MEMBER_ID

    --LEFT JOIN ûW^Àîñ(ûW^À2)
  --  ON }jtFXgîñ.ÇÔ@@ûW^Àîñ(ûW^À2).ÇÔ
  --  AND }jtFXgîñ.}Ô@@ûW^Àîñ(ûW^À2).}Ô
  --  AND ûW^Àîñ(ûW^À2).æÔÔ@@2(æÔ2)
  LEFT JOIN DT_R19 R19_SU2 ON R18.KANRI_ID = R19_SU2.KANRI_ID
                          AND R18.SEQ = R19_SU2.SEQ
					      AND R19_SU2.UPN_ROUTE_NO = 2

  --LEFT JOIN dqÆÒ}X^(ûW^À2)
  --  ON ûW^Àîñ(ûW^À2).ûW^ÀÆÒÁüÒÔ@@dqÆÒ}X^(ûW^À2).ÁüÒÔ
  --  AND dqÆÒ}X^(ûW^À2).^ÀÆÒæª@@1(ûW^ÀÆÒ)
  LEFT JOIN M_DENSHI_JIGYOUSHA  JIGYOUSHA_SU2 ON R19_SU2.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU2.EDI_MEMBER_ID
                                             AND JIGYOUSHA_SU2.UPN_KBN = 'True'

  -- LEFT JOIN ÁüÒîñ}X^(ûW^À2)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU2 ON JIGYOUSHA_SU2.EDI_MEMBER_ID = MEMBER_SU2.EDI_MEMBER_ID

  --LEFT JOIN ûW^Àîñ(ûW^À3)
  --  ON }jtFXgîñ.ÇÔ@@ûW^Àîñ(ûW^À3).ÇÔ
  --  AND }jtFXgîñ.}Ô@@ûW^Àîñ(ûW^À3).}Ô
  --  AND ûW^Àîñ(ûW^À3).æÔÔ@@3(æÔ3)
  LEFT JOIN DT_R19 R19_SU3 ON R18.KANRI_ID = R19_SU3.KANRI_ID
                          AND R18.SEQ = R19_SU3.SEQ
					      AND R19_SU3.UPN_ROUTE_NO = 3

  --LEFT JOIN dqÆÒ}X^(ûW^À3)
  --  ON ûW^Àîñ(ûW^À3).ûW^ÀÆÒÁüÒÔ@@dqÆÒ}X^(ûW^À3).ÁüÒÔ
  --  AND dqÆÒ}X^(ûW^À3).^ÀÆÒæª@@1(ûW^ÀÆÒ)
  LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU3 ON R19_SU3.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU3.EDI_MEMBER_ID
                                            AND JIGYOUSHA_SU3.UPN_KBN = 'True'


  -- LEFT JOIN ÁüÒîñ}X^(ûW^À3)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU3 ON JIGYOUSHA_SU3.EDI_MEMBER_ID = MEMBER_SU3.EDI_MEMBER_ID

  --LEFT JOIN ûW^Àîñ(ûW^À4)
  --  ON }jtFXgîñ.ÇÔ@@ûW^Àîñ(ûW^À4).ÇÔ
  --  AND }jtFXgîñ.}Ô@@ûW^Àîñ(ûW^À4).}Ô
  --  AND ûW^Àîñ(ûW^À4).æÔÔ@@4(æÔ4)
  LEFT JOIN DT_R19 R19_SU4 ON R18.KANRI_ID = R19_SU4.KANRI_ID
                          AND R18.SEQ = R19_SU4.SEQ
					      AND R19_SU4.UPN_ROUTE_NO = 4

  --LEFT JOIN dqÆÒ}X^(ûW^À4)
  --  ON ûW^Àîñ(ûW^À4).ûW^ÀÆÒÁüÒÔ@@dqÆÒ}X^(ûW^À4).ÁüÒÔ
  --  AND dqÆÒ}X^(ûW^À4).^ÀÆÒæª@@1(ûW^ÀÆÒ)
  LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU4 ON R19_SU4.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU4.EDI_MEMBER_ID
                                            AND JIGYOUSHA_SU4.UPN_KBN = 'True'

  -- LEFT JOIN ÁüÒîñ}X^(ûW^À4)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU4 ON JIGYOUSHA_SU4.EDI_MEMBER_ID = MEMBER_SU4.EDI_MEMBER_ID

  --LEFT JOIN ûW^Àîñ(ûW^À5)
  --  ON }jtFXgîñ.ÇÔ@@ûW^Àîñ(ûW^À5).ÇÔ
  --  AND }jtFXgîñ.}Ô@@ûW^Àîñ(ûW^À5).}Ô
  --  AND ûW^Àîñ(ûW^À5).æÔÔ@@5(æÔ5)
  LEFT JOIN DT_R19 R19_SU5 ON R18.KANRI_ID = R19_SU5.KANRI_ID
                          AND R18.SEQ = R19_SU5.SEQ
					      AND R19_SU5.UPN_ROUTE_NO = 5

  --LEFT JOIN dqÆÒ}X^(ûW^À5)
  --  ON ûW^Àîñ(ûW^À5).ûW^ÀÆÒÁüÒÔ@@dqÆÒ}X^(ûW^À5).ÁüÒÔ
  --  AND dqÆÒ}X^(ûW^À5).^ÀÆÒæª@@1(ûW^ÀÆÒ)
  LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU5 ON R19_SU5.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU5.EDI_MEMBER_ID
                                            AND JIGYOUSHA_SU5.UPN_KBN = 'True'

  -- LEFT JOIN ÁüÒîñ}X^(ûW^À5)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU5 ON JIGYOUSHA_SU5.EDI_MEMBER_ID = MEMBER_SU5.EDI_MEMBER_ID

  /*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 1*/
  LEFT JOIN M_UNIT HAIKI_KAKUTEI_UNIT
	ON R18.HAIKI_KAKUTEI_UNIT_CODE = HAIKI_KAKUTEI_UNIT.UNIT_CD
  /*END*/

  /*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 4*/
  LEFT JOIN M_UNIT RECEPT_UNIT
	ON R18.RECEPT_UNIT_CODE = RECEPT_UNIT.UNIT_CD
  /*END*/

  /*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 3*/
  LEFT JOIN M_UNIT SU1_UPN_UNIT
	ON R19_SU1.UPN_UNIT_CODE = SU1_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU2_UPN_UNIT
	ON R19_SU2.UPN_UNIT_CODE = SU2_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU3_UPN_UNIT
	ON R19_SU3.UPN_UNIT_CODE = SU3_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU4_UPN_UNIT
	ON R19_SU4.UPN_UNIT_CODE = SU4_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU5_UPN_UNIT
	ON R19_SU5.UPN_UNIT_CODE = SU5_UPN_UNIT.UNIT_CD
/*END*/
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
	NOT EXISTS 
		(
			SELECT
				R18.KANRI_ID
			FROM
				DT_R18_MIX MIX2
			WHERE
				MIX2.DELETE_FLG = 0
				AND R18.KANRI_ID = MIX2.KANRI_ID
		)
	AND TOC.STATUS_FLAG in (3,4)
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
	AND R18.HAIKI_DAI_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,1,2)
	AND R18.HAIKI_CHU_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,3,1)
	AND R18.HAIKI_SHO_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,4,1)
	/*END*/
	/*IF searchInfo.HAIKI_NAME_CD != ''*/
	AND R18_EX.HAIKI_NAME_CD = /*searchInfo.HAIKI_NAME_CD*/
	/*END*/
    /*IF searchInfo.SBN_HOUHOU_CD != ''*/
    AND R18_EX.SBN_HOUHOU_CD = /*searchInfo.SBN_HOUHOU_CD*/
    /*END*/
    /*IF searchInfo.SHOBUN_CHECK && searchInfo.SBN_HOUHOU_CD == '' */
    AND (R18_EX.SBN_HOUHOU_CD IS NULL OR R18_EX.SBN_HOUHOU_CD = '')
    /*END*/
ORDER BY
	R18.HIKIWATASHI_DATE
	,R18.MANIFEST_ID
