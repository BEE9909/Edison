SELECT 
     CASE WHEN MDHS_MIX.HAIKI_SHURUI_CD IS NOT NULL
            THEN MDHS_MIX.HOUKOKUSHO_BUNRUI_CD
            ELSE MDHS.HOUKOKUSHO_BUNRUI_CD
       END AS HAIKI_SHURUI_CD 
     , CASE WHEN MHB_MIX.HOUKOKUSHO_BUNRUI_CD IS NOT NULL
            THEN MHB_MIX.HOUKOKUSHO_BUNRUI_NAME
            ELSE MHB.HOUKOKUSHO_BUNRUI_NAME
       END AS HAIKI_SHURUI_NAME
     , CASE WHEN ISDATE(DR181.SBN_END_DATE) > 0 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, DR181.SBN_END_DATE), 111) 
            ELSE NULL 
       END AS SHOBUN_NENGAPPI 
     , CASE WHEN ISDATE(DR181.HIKIWATASHI_DATE) > 0 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, DR181.HIKIWATASHI_DATE), 111) 
            ELSE NULL 
       END AS KOUFU_NENGAPPI 
     , CONVERT(smallint, 4) AS HAIKI_KBN_CD
     , '電子' AS HAIKIBUTU_KBN 
     , DR181.HST_SHA_NAME AS KOUFUSHAMEI 
     , DR181.SBN_SHA_NAME AS UKEIRESAKI 
     , ISNULL(DR18EX1.KANSAN_SUU, 0) AS UKEIRERYO 
     , MU.UNIT_NAME AS UKEIRERYO_TANI 
     , '' AS KOUFUNO2 
     , DR181.MANIFEST_ID AS KOUFUNO 
     , MSH.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME 
     , ISNULL(DR18EX1.KANSAN_SUU, 0) AS SHOBUNRYO 
     , MU.UNIT_NAME AS SHOBUNRYO_TANI 
     , '' AS MOCHIDASHISAKI 
     , CAST(0 AS MONEY) AS MOCHIDASHIRYO 
     , MU.UNIT_NAME_RYAKU AS MOCHIDASHIRYO_TANI 
     , DR181.SBN_SHA_NAME AS UKEIRESAKI_GOUKEI_NAME
     , MU.UNIT_NAME_RYAKU AS UKEIRESAKI_GOUKEI_TANI 
     , MSH.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_GOUKEI_NAME
     , MU.UNIT_NAME_RYAKU AS SHOBUN_HOUHOU_GOUKEI_TANI 
     , '' AS MOCHIDASHISAKI_GOUKEI_NAME
     , MU.UNIT_NAME_RYAKU AS MOCHIDASHISAKI_GOUKEI_TANI 
     , CASE WHEN MDHS_MIX.HAIKI_SHURUI_CD IS NOT NULL
            THEN MDHS_MIX.HOUKOKUSHO_BUNRUI_CD
            ELSE MDHS.HOUKOKUSHO_BUNRUI_CD
       END AS HAIKI_SHURUI_GOUKEI_CD 
     , CASE WHEN MHB_MIX.HOUKOKUSHO_BUNRUI_CD IS NOT NULL
            THEN MHB_MIX.HOUKOKUSHO_BUNRUI_NAME
            ELSE MHB.HOUKOKUSHO_BUNRUI_NAME
       END AS HAIKI_SHURUI_GOUKEI_NAME 
     , MU.UNIT_NAME_RYAKU AS HAIKI_SHURUI_GOUKEI_TANI 
     , MU.UNIT_NAME_RYAKU AS UKEIRESAKI_SOUGOUKEI_TANI 
     , MU.UNIT_NAME_RYAKU AS SHOBUN_HOUHOU_SOUGOUKEI_TANI 
     , MU.UNIT_NAME_RYAKU AS MOCHIDASHISAKI_SOUGOUKEI_TANI 
     , MU.UNIT_NAME_RYAKU AS HAIKI_SHURUI_SOUGOUKEI_TANI 

  FROM DT_MF_TOC AS DMT1 
 INNER JOIN DT_R18 AS DR181 
    ON DMT1.KANRI_ID = DR181.KANRI_ID 
   AND DMT1.LATEST_SEQ = DR181.SEQ 
 INNER JOIN DT_R19 AS DR191 
    ON DR181.KANRI_ID = DR191.KANRI_ID 
   AND DR181.SEQ = DR191.SEQ 
   AND DR181.UPN_ROUTE_CNT = DR191.UPN_ROUTE_NO 
 INNER JOIN (
        SELECT 
            (CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.DETAIL_SYSTEM_ID ELSE DT_R18_EX.SYSTEM_ID END) AS SYSTEM_ID
            ,DT_R18_EX.KANRI_ID
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.KANSAN_SUU ELSE DT_R18_EX.KANSAN_SUU END) AS KANSAN_SUU
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.SBN_HOUHOU_CD ELSE DT_R18_EX.SBN_HOUHOU_CD END) AS SBN_HOUHOU_CD
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.HAIKI_NAME_CD ELSE DT_R18_EX.HAIKI_NAME_CD END) AS HAIKI_NAME_CD
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.HAIKI_DAI_CODE ELSE '' END) AS HAIKI_DAI_CODE
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.HAIKI_CHU_CODE ELSE '' END) AS HAIKI_CHU_CODE
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.HAIKI_SHO_CODE ELSE '' END) AS HAIKI_SHO_CODE
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.SBN_ENDREP_KBN ELSE r18.SBN_ENDREP_KBN END) AS SBN_ENDREP_KBN
            ,DT_R18_EX.SBN_GYOUSHA_CD
            ,DT_R18_EX.SBN_GENBA_CD
        FROM DT_R18_EX
		INNER JOIN DT_MF_TOC toc ON DT_R18_EX.KANRI_ID = toc.KANRI_ID
		INNER JOIN DT_R18 r18 ON toc.KANRI_ID = r18.KANRI_ID AND toc.LATEST_SEQ = r18.SEQ
        LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS DT_R18_MIX ON
        DT_R18_EX.SYSTEM_ID = DT_R18_MIX.SYSTEM_ID
        WHERE DT_R18_EX.DELETE_FLG = 0
    ) AS DR18EX1
    ON DR181.KANRI_ID = DR18EX1.KANRI_ID 
 INNER JOIN DT_R19_EX AS DR19EX1 
    ON DR191.KANRI_ID = DR19EX1.KANRI_ID 
   AND DR191.UPN_ROUTE_NO = DR19EX1.UPN_ROUTE_NO 
   AND DR19EX1.DELETE_FLG = 'false' 
 INNER JOIN M_GYOUSHA AS MG_SBN 
    ON DR18EX1.SBN_GYOUSHA_CD = MG_SBN.GYOUSHA_CD 
   AND MG_SBN.JISHA_KBN = 'true'
   AND MG_SBN.SHOBUN_NIOROSHI_GYOUSHA_KBN = 'true'
 INNER JOIN M_SYS_INFO 
    ON SYS_ID = '0' 
/*IF dto.HIDUKESYURUI == '2'*/ 
 LEFT JOIN (
			SELECT R19M.KANRI_ID, R19M.SEQ, R19M.UPN_ROUTE_NO, MAX(Other_KUKAN.UPN_ROUTE_NO) AS 表示日付区間
			FROM DT_MF_TOC 
			INNER JOIN DT_R19 AS R19M ON DT_MF_TOC.KANRI_ID = R19M.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = R19M.SEQ
			LEFT JOIN (
					SELECT R18.KANRI_ID, R18.SEQ, UPN_ROUTE_NO 
					FROM DT_MF_TOC toc
					LEFT JOIN DT_R18 R18 on toc.KANRI_ID = R18.KANRI_ID AND toc.LATEST_SEQ = R18.SEQ
					LEFT JOIN DT_R19 R19 on R18.KANRI_ID = R19.KANRI_ID AND R18.SEQ = R19.SEQ AND R18.HST_SHA_EDI_MEMBER_ID != R19.UPN_SHA_EDI_MEMBER_ID
                    UNION 
					SELECT R18.KANRI_ID, R18.SEQ, 0 AS UPN_ROUTE_NO 
					FROM DT_MF_TOC toc
					LEFT JOIN DT_R18 R18 on toc.KANRI_ID = R18.KANRI_ID AND toc.LATEST_SEQ = R18.SEQ
			) AS Other_KUKAN
			ON R19M.KANRI_ID = Other_KUKAN.KANRI_ID AND R19M.SEQ = Other_KUKAN.SEQ AND R19M.UPN_ROUTE_NO >= Other_KUKAN.UPN_ROUTE_NO
			GROUP BY R19M.KANRI_ID, R19M.SEQ, R19M.UPN_ROUTE_NO
 ) Other_KUKAN_F
 ON DR191.KANRI_ID = Other_KUKAN_F.KANRI_ID AND DR191.SEQ = Other_KUKAN_F.SEQ AND DR191.UPN_ROUTE_NO = Other_KUKAN_F.UPN_ROUTE_NO
 LEFT JOIN (
			SELECT R19U.KANRI_ID, R19U.SEQ, R19U.UPN_ROUTE_NO, R19U.UPN_END_DATE
			FROM DT_MF_TOC toc INNER JOIN DT_R19 R19U ON toc.KANRI_ID = R19U.KANRI_ID AND toc.LATEST_SEQ = R19U.SEQ
			UNION 
			SELECT R18U.KANRI_ID, R18U.SEQ, 0 AS UPN_ROUTE_NO, R18U.HIKIWATASHI_DATE AS UPN_END_DATE
			FROM DT_MF_TOC toc INNER JOIN DT_R18 R18U ON toc.KANRI_ID = R18U.KANRI_ID AND toc.LATEST_SEQ = R18U.SEQ
 ) ROUTE_DATA 
 ON Other_KUKAN_F.KANRI_ID = ROUTE_DATA.KANRI_ID and Other_KUKAN_F.SEQ = ROUTE_DATA.SEQ and Other_KUKAN_F.表示日付区間 = ROUTE_DATA.UPN_ROUTE_NO
 /*END*/ 
  LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI AS MDHS 
    ON DR181.HAIKI_DAI_CODE = SUBSTRING(MDHS.HAIKI_SHURUI_CD,1,2) 
   AND DR181.HAIKI_CHU_CODE = SUBSTRING(MDHS.HAIKI_SHURUI_CD,3,1) 
   AND DR181.HAIKI_SHO_CODE = SUBSTRING(MDHS.HAIKI_SHURUI_CD,4,1) 
  LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI AS MDHS_MIX 
    ON DR18EX1.HAIKI_DAI_CODE = SUBSTRING(MDHS_MIX.HAIKI_SHURUI_CD,1,2) 
   AND DR18EX1.HAIKI_CHU_CODE = SUBSTRING(MDHS_MIX.HAIKI_SHURUI_CD,3,1) 
   AND DR18EX1.HAIKI_SHO_CODE = SUBSTRING(MDHS_MIX.HAIKI_SHURUI_CD,4,1) 
  LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI AS MHB 
    ON MDHS.HOUKOKUSHO_BUNRUI_CD = MHB.HOUKOKUSHO_BUNRUI_CD
  LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI AS MHB_MIX 
    ON MDHS_MIX.HOUKOKUSHO_BUNRUI_CD = MHB_MIX.HOUKOKUSHO_BUNRUI_CD
  LEFT OUTER JOIN M_SHOBUN_HOUHOU AS MSH 
    ON DR18EX1.SBN_HOUHOU_CD = MSH.SHOBUN_HOUHOU_CD 
   AND MSH.DENSHI_USE_KBN = 'true' 
  LEFT OUTER JOIN M_GENNYOURITSU AS MGENYO 
    ON MDHS.HOUKOKUSHO_BUNRUI_CD = MGENYO.HOUKOKUSHO_BUNRUI_CD 
   AND DR18EX1.HAIKI_NAME_CD = MGENYO.HAIKI_NAME_CD 
   AND DR18EX1.SBN_HOUHOU_CD = MGENYO.SHOBUN_HOUHOU_CD 
  LEFT OUTER JOIN M_UNIT AS MU 
    ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = MU.UNIT_CD 

 WHERE DMT1.STATUS_FLAG = 4  
   AND NOT EXISTS ( 
      SELECT * 
        FROM T_MANIFEST_RELATION AS TMR 
       WHERE DR18EX1.SYSTEM_ID = TMR.FIRST_SYSTEM_ID 
         AND TMR.FIRST_HAIKI_KBN_CD = 4 
         AND TMR.DELETE_FLG = 'false' 
       ) 
    AND (ISNULL(DR18EX1.SBN_ENDREP_KBN, 1) = 1 
    OR (ISNULL(DR18EX1.SBN_HOUHOU_CD, 0) != 300
    AND ISNULL(DR18EX1.SBN_HOUHOU_CD, 0) != 301
    AND ISNULL(DR18EX1.SBN_HOUHOU_CD, 0) != 302
    AND ISNULL(DR18EX1.SBN_HOUHOU_CD, 0) != 303
    AND ISNULL(DR18EX1.SBN_HOUHOU_CD, 0) != 304
    AND ISNULL(DR18EX1.SBN_HOUHOU_CD, 0) != 310)) 
    AND ISDATE(DR181.SBN_END_DATE) > 0
/*IF dto.HIDUKESYURUI == '1'*/ 
    AND DR181.HIKIWATASHI_DATE IS NOT NULL 
    AND DR181.HIKIWATASHI_DATE <> '' 
    AND DR181.HIKIWATASHI_DATE <> '0' 
    AND CONVERT(DATETIME,DR181.HIKIWATASHI_DATE) >= /*dto.DATE_FROM*/ 
    AND CONVERT(DATETIME,DR181.HIKIWATASHI_DATE) <= /*dto.DATE_TO*/ 
/*END*/ 
/*IF dto.HIDUKESYURUI == '2'*/ 
    AND ROUTE_DATA.UPN_END_DATE IS NOT NULL 
    AND ROUTE_DATA.UPN_END_DATE <> '' 
    AND ROUTE_DATA.UPN_END_DATE <> '0' 
    AND CONVERT(DATETIME,ROUTE_DATA.UPN_END_DATE) >= /*dto.DATE_FROM*/ 
    AND CONVERT(DATETIME,ROUTE_DATA.UPN_END_DATE) <= /*dto.DATE_TO*/ 
/*END*/ 
/*IF dto.HIDUKESYURUI == '3'*/ 
    AND DR181.SBN_END_DATE IS NOT NULL 
    AND DR181.SBN_END_DATE <> '' 
    AND DR181.SBN_END_DATE <> '0' 
    AND CONVERT(DATETIME,DR181.SBN_END_DATE) >= /*dto.DATE_FROM*/ 
    AND CONVERT(DATETIME,DR181.SBN_END_DATE) <= /*dto.DATE_TO*/ 
/*END*/ 
/*IF dto.SBN_GYOUSHA_CD != null && dto.SBN_GYOUSHA_CD != '' */ 
    AND DR18EX1.SBN_GYOUSHA_CD = /*dto.SBN_GYOUSHA_CD*/ 
/*END*/ 
/*IF dto.SBN_GENBA_CD != null && dto.SBN_GENBA_CD != '' */ 
    AND DR18EX1.SBN_GENBA_CD >= /*dto.SBN_GENBA_CD*/ 
/*END*/ 
/*IF dto.SBN_GENBA_CD_TO != null && dto.SBN_GENBA_CD_TO != '' */ 
    AND DR18EX1.SBN_GENBA_CD <= /*dto.SBN_GENBA_CD_TO*/ 
/*END*/ 

 ORDER BY MDHS.HOUKOKUSHO_BUNRUI_CD 
     , DR181.SBN_SHA_NAME 
     , MSH.SHOBUN_HOUHOU_NAME_RYAKU 
     , DR181.SBN_END_DATE 
