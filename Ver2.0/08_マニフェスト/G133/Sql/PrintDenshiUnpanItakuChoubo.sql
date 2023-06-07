SELECT 
     CASE WHEN MDHS_MIX.HAIKI_SHURUI_CD IS NOT NULL
            THEN MDHS_MIX.HOUKOKUSHO_BUNRUI_CD
            ELSE MDHS.HOUKOKUSHO_BUNRUI_CD
     END AS HAIKI_SHURUI_CD 
     , CASE WHEN MHB_MIX.HOUKOKUSHO_BUNRUI_CD IS NOT NULL
            THEN MHB_MIX.HOUKOKUSHO_BUNRUI_NAME
            ELSE MHB.HOUKOKUSHO_BUNRUI_NAME
       END AS HAIKI_SHURUI_NAME
     , CASE WHEN ISDATE(DR182.HIKIWATASHI_DATE) > 0 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, DR182.HIKIWATASHI_DATE), 111) 
            ELSE NULL 
       END AS ITAKU_NENGAPPI 
     , CASE WHEN DR192.UPN_ROUTE_NO = 1
            THEN 
               CASE WHEN ( DR182.HAIKI_DAI_CODE + DR182.HAIKI_CHU_CODE + DR182.HAIKI_SHO_CODE ) < '7000' THEN
                       --地域の許可番号がなければ運搬報告書提出先地域の許可番号を表示する
                       CASE WHEN ISNULL(MCK_MOTO_HST_CK.FUTSUU_KYOKA_NO, '') = '' THEN MCK_MOTO_HST.FUTSUU_KYOKA_NO
                            ELSE MCK_MOTO_HST_CK.FUTSUU_KYOKA_NO
                       END
                    WHEN ( DR182.HAIKI_DAI_CODE + DR182.HAIKI_CHU_CODE + DR182.HAIKI_SHO_CODE ) >= '7000' THEN
                       --地域の許可番号がなければ運搬報告書提出先地域の許可番号を表示する
                       CASE WHEN ISNULL(MCK_MOTO_HST_CK.TOKUBETSU_KYOKA_NO, '') = '' THEN MCK_MOTO_HST.TOKUBETSU_KYOKA_NO
                            ELSE MCK_MOTO_HST_CK.TOKUBETSU_KYOKA_NO
                       END
                    ELSE '' 
               END
            ELSE
               CASE WHEN ( DR182.HAIKI_DAI_CODE + DR182.HAIKI_CHU_CODE + DR182.HAIKI_SHO_CODE ) < '7000' THEN
                       --地域の許可番号がなければ運搬報告書提出先地域の許可番号を表示する
                       CASE WHEN ISNULL(MCK_MOTO_CK.FUTSUU_KYOKA_NO, '') = '' THEN MCK_MOTO.FUTSUU_KYOKA_NO
                            ELSE MCK_MOTO_CK.FUTSUU_KYOKA_NO
                       END
                    WHEN ( DR182.HAIKI_DAI_CODE + DR182.HAIKI_CHU_CODE + DR182.HAIKI_SHO_CODE ) >= '7000' THEN
                       --地域の許可番号がなければ運搬報告書提出先地域の許可番号を表示する
                       CASE WHEN ISNULL(MCK_MOTO_CK.TOKUBETSU_KYOKA_NO, '') = '' THEN MCK_MOTO.TOKUBETSU_KYOKA_NO
                            ELSE MCK_MOTO_CK.TOKUBETSU_KYOKA_NO
                       END
                    ELSE '' 
               END
       END AS KYOKANO 
     , CASE WHEN ( DR182.HAIKI_DAI_CODE + DR182.HAIKI_CHU_CODE + DR182.HAIKI_SHO_CODE ) < '7000' THEN
                --地域の許可番号がなければ運搬報告書提出先地域の許可番号を表示する
                CASE WHEN ISNULL(MCK_SAKI_CK.FUTSUU_KYOKA_NO, '') = '' THEN MCK_SAKI.FUTSUU_KYOKA_NO
                     ELSE MCK_SAKI_CK.FUTSUU_KYOKA_NO
                END
            WHEN ( DR182.HAIKI_DAI_CODE + DR182.HAIKI_CHU_CODE + DR182.HAIKI_SHO_CODE ) >= '7000' THEN
                --地域の許可番号がなければ運搬報告書提出先地域の許可番号を表示する
                CASE WHEN ISNULL(MCK_SAKI_CK.TOKUBETSU_KYOKA_NO, '') = '' THEN MCK_SAKI.TOKUBETSU_KYOKA_NO
                     ELSE MCK_SAKI_CK.TOKUBETSU_KYOKA_NO
                END
            ELSE '' 
       END AS UPNSAKI_KYOKANO 
     , DR192.UPN_SHA_NAME AS JUTAKUSHA 
     , CASE WHEN ISDATE(DR182.HIKIWATASHI_DATE) > 0 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, DR182.HIKIWATASHI_DATE), 111) 
            ELSE NULL 
       END AS KOUFU_NENGAPPI 
     , CONVERT(smallint, 4) AS HAIKI_KBN_CD
     , '電子' AS HAIKIBUTU_KBN 
     , DR182.MANIFEST_ID AS KOUFUNO 
     , DR192.UPN_SHA_ADDRESS1 + DR192.UPN_SHA_ADDRESS2 + DR192.UPN_SHA_ADDRESS3 + DR192.UPN_SHA_ADDRESS4 AS JUTAKUSHA_ADDRESS 
     , DR192.UPNSAKI_JOU_NAME AS UNPANSAKI 
     , ISNULL(DR18EX2.KANSAN_SUU, 0) AS ITAKURYO 
     , MU.UNIT_NAME_RYAKU AS ITAKURYO_TANI 
     , DR192.UPNSAKI_JOU_NAME AS UNPANSAKI_GOUKEI_NAME
     , MU.UNIT_NAME_RYAKU AS UNPANSAKI_GOUKEI_TANI 
     , CASE WHEN MDHS_MIX.HAIKI_SHURUI_CD IS NOT NULL
            THEN MDHS_MIX.HOUKOKUSHO_BUNRUI_CD
            ELSE MDHS.HOUKOKUSHO_BUNRUI_CD
       END AS HAIKI_SHURUI_GOUKEI_CD 
     , CASE WHEN MHB_MIX.HOUKOKUSHO_BUNRUI_CD IS NOT NULL
            THEN MHB_MIX.HOUKOKUSHO_BUNRUI_NAME
            ELSE MHB.HOUKOKUSHO_BUNRUI_NAME
       END AS HAIKI_SHURUI_GOUKEI_NAME 
     , MU.UNIT_NAME_RYAKU AS HAIKI_SHURUI_GOUKEI_TANI 
     , MU.UNIT_NAME_RYAKU AS ITAKURYO_SOUGOUKEI_TANI 

  FROM DT_MF_TOC AS DMT2 
 INNER JOIN DT_R18 AS DR182 
    ON DMT2.KANRI_ID = DR182.KANRI_ID 
   AND DMT2.LATEST_SEQ = DR182.SEQ 
 INNER JOIN DT_R19 AS DR192 
    ON DR182.KANRI_ID = DR192.KANRI_ID 
   AND DR182.SEQ = DR192.SEQ 
   AND DR182.UPN_ROUTE_CNT = DR192.UPN_ROUTE_NO 
 INNER JOIN (
        SELECT
            DT_R18_EX.KANRI_ID
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.KANSAN_SUU ELSE DT_R18_EX.KANSAN_SUU END) AS KANSAN_SUU
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.HAIKI_DAI_CODE ELSE '' END) AS HAIKI_DAI_CODE
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.HAIKI_CHU_CODE ELSE '' END) AS HAIKI_CHU_CODE
            ,(CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN DT_R18_MIX.HAIKI_SHO_CODE ELSE '' END) AS HAIKI_SHO_CODE
            ,DT_R18_EX.HST_GYOUSHA_CD
            ,DT_R18_EX.HST_GENBA_CD
        FROM DT_R18_EX
        LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS DT_R18_MIX ON
        DT_R18_EX.SYSTEM_ID = DT_R18_MIX.SYSTEM_ID
        WHERE DT_R18_EX.DELETE_FLG = 0
    ) AS DR18EX2
    ON DR182.KANRI_ID = DR18EX2.KANRI_ID 
 INNER JOIN DT_R19_EX AS DR19EX2 
    ON DR192.KANRI_ID = DR19EX2.KANRI_ID 
   AND DR192.UPN_ROUTE_NO = DR19EX2.UPN_ROUTE_NO 
   AND DR19EX2.DELETE_FLG = 'false' 
 LEFT JOIN DT_R19_EX AS DR19EX2_PREV
    ON DR19EX2.KANRI_ID = DR19EX2_PREV.KANRI_ID 
   AND DR19EX2_PREV.UPN_ROUTE_NO = (DR19EX2.UPN_ROUTE_NO - 1)
   AND DR19EX2_PREV.DELETE_FLG = 'false' 
 INNER JOIN M_SYS_INFO 
    ON SYS_ID = '0' 
 INNER JOIN M_GYOUSHA AS MG_HST 
    ON DR18EX2.HST_GYOUSHA_CD = MG_HST.GYOUSHA_CD 
   AND MG_HST.JISHA_KBN = 'true' 
   AND MG_HST.HAISHUTSU_NIZUMI_GYOUSHA_KBN = 'true' 
 INNER JOIN M_GYOUSHA AS MG_UPN 
    ON DR19EX2.UPN_GYOUSHA_CD = MG_UPN.GYOUSHA_CD 
   AND MG_UPN.JISHA_KBN = 'false' 
   AND MG_UPN.UNPAN_JUTAKUSHA_KAISHA_KBN = 'true' 
  LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI AS MDHS 
    ON DR182.HAIKI_DAI_CODE = SUBSTRING(MDHS.HAIKI_SHURUI_CD,1,2) 
   AND DR182.HAIKI_CHU_CODE = SUBSTRING(MDHS.HAIKI_SHURUI_CD,3,1) 
   AND DR182.HAIKI_SHO_CODE = SUBSTRING(MDHS.HAIKI_SHURUI_CD,4,1) 
  LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI AS MDHS_MIX 
    ON DR18EX2.HAIKI_DAI_CODE = SUBSTRING(MDHS_MIX.HAIKI_SHURUI_CD,1,2) 
   AND DR18EX2.HAIKI_CHU_CODE = SUBSTRING(MDHS_MIX.HAIKI_SHURUI_CD,3,1) 
   AND DR18EX2.HAIKI_SHO_CODE = SUBSTRING(MDHS_MIX.HAIKI_SHURUI_CD,4,1) 
  LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI AS MHB 
    ON MDHS.HOUKOKUSHO_BUNRUI_CD = MHB.HOUKOKUSHO_BUNRUI_CD
  LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI AS MHB_MIX 
    ON MDHS_MIX.HOUKOKUSHO_BUNRUI_CD = MHB_MIX.HOUKOKUSHO_BUNRUI_CD
  LEFT OUTER JOIN M_UNIT AS MU 
    ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = MU.UNIT_CD 
  LEFT JOIN M_GENBA AS GENBA_UPNSAKI
    ON DR19EX2.UPNSAKI_GYOUSHA_CD = GENBA_UPNSAKI.GYOUSHA_CD 
   AND DR19EX2.UPNSAKI_GENBA_CD = GENBA_UPNSAKI.GENBA_CD 
  LEFT JOIN M_GENBA AS GENBA_UPNSAKI_PREV
    ON DR19EX2_PREV.UPNSAKI_GYOUSHA_CD = GENBA_UPNSAKI_PREV.GYOUSHA_CD
   AND DR19EX2_PREV.UPNSAKI_GENBA_CD = GENBA_UPNSAKI_PREV.GENBA_CD
   -- 4種類の許可番号情報を取得(運搬元 or 運搬先 * 特定の区間 or その他の区間 => 全4種類)
   -- どれを表示するかはSELECT句で制御
  LEFT OUTER JOIN M_GENBA AS MGEN_HST_CHIIKI
    ON DR18EX2.HST_GYOUSHA_CD = MGEN_HST_CHIIKI.GYOUSHA_CD
   AND DR18EX2.HST_GENBA_CD = MGEN_HST_CHIIKI.GENBA_CD
  LEFT OUTER JOIN M_GENBA AS MGEN_MOTO_CHIIKI
    ON DR19EX2_PREV.UPNSAKI_GYOUSHA_CD = MGEN_MOTO_CHIIKI.GYOUSHA_CD 
   AND DR19EX2_PREV.UPNSAKI_GENBA_CD = MGEN_MOTO_CHIIKI.GENBA_CD 
  LEFT OUTER JOIN M_CHIIKIBETSU_KYOKA AS MCK_MOTO_HST
    ON DR19EX2.UPN_GYOUSHA_CD = MCK_MOTO_HST.GYOUSHA_CD
   AND MGEN_HST_CHIIKI.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = MCK_MOTO_HST.CHIIKI_CD
   AND MCK_MOTO_HST.KYOKA_KBN = '1'
  LEFT OUTER JOIN M_CHIIKIBETSU_KYOKA AS MCK_MOTO_HST_CK
    ON DR19EX2.UPN_GYOUSHA_CD = MCK_MOTO_HST_CK.GYOUSHA_CD
   AND MGEN_HST_CHIIKI.CHIIKI_CD = MCK_MOTO_HST_CK.CHIIKI_CD
   AND MCK_MOTO_HST_CK.KYOKA_KBN = '1'
  LEFT OUTER JOIN M_CHIIKIBETSU_KYOKA AS MCK_MOTO
    ON DR19EX2.UPN_GYOUSHA_CD = MCK_MOTO.GYOUSHA_CD
   AND MGEN_MOTO_CHIIKI.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = MCK_MOTO.CHIIKI_CD
   AND MCK_MOTO.KYOKA_KBN = '1'
  LEFT OUTER JOIN M_CHIIKIBETSU_KYOKA AS MCK_MOTO_CK
    ON DR19EX2.UPN_GYOUSHA_CD = MCK_MOTO_CK.GYOUSHA_CD
   AND MGEN_MOTO_CHIIKI.CHIIKI_CD = MCK_MOTO_CK.CHIIKI_CD
   AND MCK_MOTO_CK.KYOKA_KBN = '1'
  LEFT OUTER JOIN M_CHIIKIBETSU_KYOKA AS MCK_SAKI
    ON DR19EX2.UPN_GYOUSHA_CD = MCK_SAKI.GYOUSHA_CD
   AND GENBA_UPNSAKI.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = MCK_SAKI.CHIIKI_CD
   AND MCK_SAKI.KYOKA_KBN = '1'
  LEFT OUTER JOIN M_CHIIKIBETSU_KYOKA AS MCK_SAKI_CK
    ON DR19EX2.UPN_GYOUSHA_CD = MCK_SAKI_CK.GYOUSHA_CD
   AND GENBA_UPNSAKI.CHIIKI_CD = MCK_SAKI_CK.CHIIKI_CD
   AND MCK_SAKI_CK.KYOKA_KBN = '1'

WHERE DMT2.STATUS_FLAG = 4
/*IF dto.HIDUKESYURUI == '1'*/ 
   AND DR182.HIKIWATASHI_DATE IS NOT NULL 
   AND DR182.HIKIWATASHI_DATE <> '' 
   AND DR182.HIKIWATASHI_DATE <> '0' 
   AND CONVERT(DATETIME,DR182.HIKIWATASHI_DATE) >= /*dto.DATE_FROM*/ 
   AND CONVERT(DATETIME,DR182.HIKIWATASHI_DATE) <= /*dto.DATE_TO*/ 
/*END*/ 
/*IF dto.HIDUKESYURUI == '2'*/ 
   AND DR192.UPN_END_DATE IS NOT NULL 
   AND DR192.UPN_END_DATE <> '' 
   AND DR192.UPN_END_DATE <> '0' 
   AND CONVERT(DATETIME,DR192.UPN_END_DATE) >= /*dto.DATE_FROM*/ 
   AND CONVERT(DATETIME,DR192.UPN_END_DATE) <= /*dto.DATE_TO*/ 
/*END*/ 
/*IF dto.HIDUKESYURUI == '3'*/ 
   AND DR182.SBN_END_DATE IS NOT NULL 
   AND DR182.SBN_END_DATE <> '' 
   AND DR182.SBN_END_DATE <> '0' 
   AND CONVERT(DATETIME,DR182.SBN_END_DATE) >= /*dto.DATE_FROM*/ 
   AND CONVERT(DATETIME,DR182.SBN_END_DATE) <= /*dto.DATE_TO*/ 
/*END*/ 
/*IF dto.SBN_GYOUSHA_CD != null && dto.SBN_GYOUSHA_CD != '' */ 
   AND DR18EX2.HST_GYOUSHA_CD = /*dto.SBN_GYOUSHA_CD*/ 
/*END*/ 
/*IF dto.SBN_GENBA_CD != null && dto.SBN_GENBA_CD != '' */ 
   AND DR18EX2.HST_GENBA_CD >= /*dto.SBN_GENBA_CD*/ 
/*END*/ 
/*IF dto.SBN_GENBA_CD_TO != null && dto.SBN_GENBA_CD_TO != '' */ 
   AND DR18EX2.HST_GENBA_CD <= /*dto.SBN_GENBA_CD_TO*/ 
/*END*/ 

ORDER BY
    MDHS.HOUKOKUSHO_BUNRUI_CD,
    DR192.UPNSAKI_JOU_NAME,
    DR182.HIKIWATASHI_DATE