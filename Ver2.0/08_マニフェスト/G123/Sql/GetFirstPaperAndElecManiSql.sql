﻿----紙マニ場合紐付対象一次マニフェスト情報取得SQL文
SELECT TME.SYSTEM_ID                   AS SYSTEM_ID 
     , TME.SEQ                          AS SEQ 
     , MHK.HAIKI_KBN_NAME               AS HAIKI_KBN_NAME 
     , TME.KOUFU_DATE                   AS KOUFU_DATE                   --交付日付
     , TME.MANIFEST_ID                  AS MANIFEST_ID                  --交付番号
     , RTRIM(SUBSTRING(TME.HST_GYOUSHA_NAME,1,40)) + SUBSTRING(TME.HST_GYOUSHA_NAME,41,40) AS HST_GYOUSHA_NAME 
     , RTRIM(SUBSTRING(TME.HST_GENBA_NAME,1,40)) + SUBSTRING(TME.HST_GENBA_NAME,41,40) AS HST_GENBA_NAME 
     , MHS.HAIKI_SHURUI_NAME_RYAKU      AS HAIKI_SHURUI_NAME_RYAKU 
     , MHN.HAIKI_NAME                   AS HAIKI_NAME 
     , MHB.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_NAME_RYAKU 
     , TMD.HAIKI_SUU                    AS HAIKI_SUU 
     , MU.UNIT_NAME_RYAKU               AS UNIT_NAME_RYAKU 
     , TMD.GENNYOU_SUU                  AS GENNYOU_SUU 
     , MN.NISUGATA_NAME_RYAKU           AS NISUGATA_NAME_RYAKU 
     , MSH.SHOBUN_HOUHOU_NAME_RYAKU     AS SHOBUN_HOUHOU_NAME_RYAKU 
     , MIN_TMU.UPN_GYOUSHA_NAME         AS UPN_GYOUSHA_NAME 
     , TME.SBN_GYOUSHA_NAME             AS SBN_JYUTAKUSHA_NAME
     , MAX_TMU.UPN_SAKI_GENBA_NAME      AS UPN_SAKI_GENBA_NAME 
     , MGB.GENBA_NAME_RYAKU             AS LAST_SBN_GENBA_NAME          --最終処分事業場名称
     , TME.HAIKI_KBN_CD                 AS HAIKI_KBN_CD                 --マニ種類
     , TME.HST_GYOUSHA_CD               AS HST_GYOUSHA_CD               --排出事業者CD
     , TME.HST_GENBA_CD                 AS HST_GENBA_CD                 --排出事業場CD
     , TMD.HAIKI_SHURUI_CD              AS HAIKI_SHURUI_CD              --廃棄物種類
     , TMD.HAIKI_NAME_CD                AS HAIKI_NAME_CD                --廃棄物名称CD
     , MHS.HOUKOKUSHO_BUNRUI_CD         AS HOUKOKUSHO_BUNRUI_CD         --報告書分類CD
     , TMD.HAIKI_UNIT_CD                AS HAIKI_UNIT_CD                --廃棄物単位CD
     , TMD.NISUGATA_CD                  AS NISUGATA_CD                  --荷姿CD
     , TMD.SBN_HOUHOU_CD                AS SBN_HOUHOU_CD                --処分方法CD
     , MIN_TMU.UPN_GYOUSHA_CD           AS UPN_GYOUSHA_CD               --運搬受託者
     , TME.SBN_GYOUSHA_CD               AS SBN_JYUTAKUSHA_CD            --処分受託者
     , MAX_TMU.UPN_SAKI_GENBA_CD        AS SBN_GENBA_CD                 --最後の運搬区間の運搬先事業場(処分事業場)
     , MGB.GYOUSHA_CD                   AS LAST_SBN_GYOUSHA_CD          --最終処分事業者CD
     , MGB.GENBA_CD                     AS LAST_SBN_GENBA_CD            --最終処分事業場CD
     , TMD.SBN_END_DATE                 AS SBN_END_DATE                 --処分終了日
     , MAX_TMU.UPN_END_DATE             AS LAST_UPN_END_DATE            --運搬終了日
     , NULL                             AS KANSAN_SUU                   --換算後数量
     , NULL                             AS KANRI_ID                     --管理番号
     , NULL                             AS LATEST_SEQ                   --最後のSEQ
     , TMD.DETAIL_SYSTEM_ID             AS DETAIL_SYSTEM_ID             --マニ明細SYSTEM_ID
     , TME.SYSTEM_ID                    AS TME_SYSTEM_ID
     , TME.SEQ                          AS TME_SEQ
     , CONVERT(INT,TME.TIME_STAMP)      AS TME_TIME_STAMP
     , NULL                             AS R18EX_SYSTEM_ID
     , NULL                             AS R18EX_SEQ
     , NULL                             AS R18EX_TIME_STAMP
     , NULL                             AS LAST_SBN_GENBA_NAME_AND_ADDRESS --最終処分事業場名称 + 住所 
     , NULL                             AS LAST_SBN_END_DATE            --最終処分終了日 

  FROM T_MANIFEST_ENTRY AS TME WITH(NOLOCK) 
 INNER JOIN T_MANIFEST_DETAIL AS TMD WITH(NOLOCK) 
    ON TME.SYSTEM_ID = TMD.SYSTEM_ID 
   AND TME.SEQ = TMD.SEQ 
 INNER JOIN T_MANIFEST_UPN AS MAX_TMU WITH(NOLOCK) 
    ON TME.SYSTEM_ID = MAX_TMU.SYSTEM_ID 
   AND TME.SEQ = MAX_TMU.SEQ 
 INNER JOIN (SELECT T_MANIFEST_ENTRY.SYSTEM_ID 
           , T_MANIFEST_ENTRY.SEQ 
           , MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) AS UPN_ROUTE_NO
        FROM T_MANIFEST_ENTRY WITH(NOLOCK) 
       INNER JOIN T_MANIFEST_UPN WITH(NOLOCK) 
          ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN.SYSTEM_ID 
         AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_UPN.SEQ 
       WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false' 
         AND 
         (
         ((T_MANIFEST_ENTRY.HAIKI_KBN_CD = '1' OR T_MANIFEST_ENTRY.HAIKI_KBN_CD = '2') AND T_MANIFEST_UPN.UPN_END_DATE IS NOT NULL)
         OR (T_MANIFEST_ENTRY.HAIKI_KBN_CD = '3' AND T_MANIFEST_UPN.UPN_SAKI_KBN IS NOT NULL)
         )
       GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID 
           , T_MANIFEST_ENTRY.SEQ 
      ) AS TMU_SEARCH
    ON MAX_TMU.SYSTEM_ID = TMU_SEARCH.SYSTEM_ID 
   AND MAX_TMU.SEQ = TMU_SEARCH.SEQ 
   AND MAX_TMU.UPN_ROUTE_NO = TMU_SEARCH.UPN_ROUTE_NO 
 INNER JOIN T_MANIFEST_UPN AS MIN_TMU WITH(NOLOCK) 
    ON TME.SYSTEM_ID = MIN_TMU.SYSTEM_ID 
   AND TME.SEQ = MIN_TMU.SEQ 
   AND MIN_TMU.UPN_ROUTE_NO = 1
  LEFT OUTER JOIN T_MANIFEST_RELATION TMR WITH(NOLOCK) 
    ON TMD.DETAIL_SYSTEM_ID = TMR.FIRST_SYSTEM_ID 
    AND TMR.FIRST_HAIKI_KBN_CD <> 4
   AND TMR.DELETE_FLG = 0
  LEFT OUTER JOIN M_HAIKI_KBN MHK WITH(NOLOCK) 
    ON TME.HAIKI_KBN_CD = MHK.HAIKI_KBN_CD 
   AND MHK.DELETE_FLG = 0 
  LEFT OUTER JOIN M_HAIKI_SHURUI MHS WITH(NOLOCK) 
    ON MHS.HAIKI_KBN_CD = TME.HAIKI_KBN_CD 
   AND TMD.HAIKI_SHURUI_CD = MHS.HAIKI_SHURUI_CD 
   AND MHS.DELETE_FLG = 0 
  LEFT OUTER JOIN M_HAIKI_NAME MHN WITH(NOLOCK) 
    ON MHN.HAIKI_NAME_CD = TMD.HAIKI_NAME_CD 
   AND MHN.DELETE_FLG = 0 
  LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI MHB WITH(NOLOCK) 
    ON MHB.HOUKOKUSHO_BUNRUI_CD = MHS.HOUKOKUSHO_BUNRUI_CD 
   AND MHB.DELETE_FLG = 0
  LEFT OUTER JOIN M_UNIT MU WITH(NOLOCK) 
    ON MU.UNIT_CD = TMD.HAIKI_UNIT_CD 
   AND MU.DELETE_FLG = 0
  LEFT OUTER JOIN M_NISUGATA MN WITH(NOLOCK) 
    ON MN.NISUGATA_CD = TMD.NISUGATA_CD 
   AND MN.DELETE_FLG = 0
  LEFT OUTER JOIN M_SHOBUN_HOUHOU MSH WITH(NOLOCK) 
    ON MSH.SHOBUN_HOUHOU_CD = TMD.SBN_HOUHOU_CD 
   AND MSH.DELETE_FLG = 0
  LEFT OUTER JOIN M_GYOUSHA MGA WITH(NOLOCK) 
    ON MGA.GYOUSHA_CD = TMD.LAST_SBN_GYOUSHA_CD 
   AND MGA.SHOBUN_NIOROSHI_GYOUSHA_KBN = 1 
   AND MGA.DELETE_FLG = 0 
  LEFT OUTER JOIN M_GENBA MGB WITH(NOLOCK) 
    ON MGB.GYOUSHA_CD = TMD.LAST_SBN_GYOUSHA_CD 
   AND MGB.GENBA_CD = TMD.LAST_SBN_GENBA_CD 
   AND MGB.SAISHUU_SHOBUNJOU_KBN = 1 
   AND MGB.DELETE_FLG = 0
  LEFT OUTER JOIN M_GENBA MGB_SHOBUN WITH(NOLOCK)
    ON MGB_SHOBUN.GYOUSHA_CD =  TME.SBN_GYOUSHA_CD
  AND MGB_SHOBUN.GENBA_CD = MAX_TMU.UPN_SAKI_GENBA_CD
  LEFT OUTER JOIN M_GENBA MGB_TSUMIKAE WITH(NOLOCK)
    ON MGB_TSUMIKAE.GYOUSHA_CD =  MAX_TMU.UPN_SAKI_GYOUSHA_CD
  AND MGB_TSUMIKAE.GENBA_CD = MAX_TMU.UPN_SAKI_GENBA_CD
 WHERE TME.DELETE_FLG = 'false' 
   AND TME.FIRST_MANIFEST_KBN = 'false' 
   AND TME.MANIFEST_ID IS NOT NULL 
   AND TME.MANIFEST_ID != ''
   AND TMD.LAST_SBN_END_DATE IS NULL
   AND TMD.SBN_END_DATE IS NOT NULL
   AND 
   (
     ((TME.HAIKI_KBN_CD = '1' OR TME.HAIKI_KBN_CD = '2') AND MGB_SHOBUN.JISHA_KBN = 1)
     OR
     (TME.HAIKI_KBN_CD = '3' AND MGB_TSUMIKAE.JISHA_KBN = 1 AND MGB_TSUMIKAE.SHOBUN_NIOROSHI_GENBA_KBN = 1)
   )
/*IF data.LAST_SBN_GENBA_TYPE == '1'*/
   AND TMD.LAST_SBN_GENBA_CD IS NOT NULL
/*END*/
/*IF data.LAST_SBN_GENBA_TYPE == '2'*/
   AND TMD.LAST_SBN_GENBA_CD IS NULL
/*END*/
/*IF !data.NEXT_SYSTEM_ID.IsNull && data.MANI_KBN != '4'*/AND TME.SYSTEM_ID != /*data.NEXT_SYSTEM_ID*//*END*/
   AND TMR.FIRST_SYSTEM_ID IS NULL
   AND (( 1 = /*data.paper*/1
/*IF !data.MANI_TYPE.IsNull && data.MANI_TYPE != '' && data.MANI_TYPE != '5' */AND TME.HAIKI_KBN_CD = /*data.MANI_TYPE*//*END*/
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '1'*/
      /*IF !data.START_DATETIME.IsNull && data.START_DATETIME !=''*/ 
            AND CONVERT(datetime,TME.KOUFU_DATE,120) >= /*data.START_DATETIME*/
      /*END*/
      /*IF !data.END_DATETIME.IsNull && data.END_DATETIME !=''*/ 
            AND CONVERT(datetime,TME.KOUFU_DATE,120) <= /*data.END_DATETIME*/
      /*END*/
/*END*/
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '2'*/
      /*IF !data.START_DATETIME.IsNull && data.START_DATETIME !=''*/ 
            AND CONVERT(datetime,MAX_TMU.UPN_END_DATE,120) >= /*data.START_DATETIME*/
      /*END*/
      /*IF !data.END_DATETIME.IsNull && data.END_DATETIME !=''*/ 
            AND CONVERT(datetime,MAX_TMU.UPN_END_DATE,120) <= /*data.END_DATETIME*/
      /*END*/
/*END*/
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '3'*/
      /*IF !data.START_DATETIME.IsNull && data.START_DATETIME !=''*/ 
            AND CONVERT(datetime,TMD.SBN_END_DATE,120) >= /*data.START_DATETIME*/
      /*END*/
      /*IF !data.END_DATETIME.IsNull && data.END_DATETIME !=''*/ 
            AND CONVERT(datetime,TMD.SBN_END_DATE,120) <= /*data.END_DATETIME*/
      /*END*/
/*END*/
/*IF !data.HAIKI_SHURUI_CD.IsNull && data.HAIKI_SHURUI_CD !=''*/ AND TMD.HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*//*END*/
/*IF !data.HAIKI_NAME_CD.IsNull && data.HAIKI_NAME_CD !=''*/ AND TMD.HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*//*END*/
/*IF !data.HOUKOKUSHO_BUNRUI_CD.IsNull && data.HOUKOKUSHO_BUNRUI_CD !=''*/ AND  MHS.HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*//*END*/
/*IF !data.NISUGATA_CD.IsNull && data.NISUGATA_CD !=''*/ AND TMD.NISUGATA_CD = /*data.NISUGATA_CD*//*END*/
/*IF !data.SBN_HOUHOU_CD.IsNull && data.SBN_HOUHOU_CD !=''*/ AND TMD.SBN_HOUHOU_CD = /*data.SBN_HOUHOU_CD*//*END*/
/*IF !data.HST_GYOUSHA_CD.IsNull && data.HST_GYOUSHA_CD !=''*/ AND HST_GYOUSHA_CD = /*data.HST_GYOUSHA_CD*//*END*/
/*IF !data.HST_GENBA_CD.IsNull && data.HST_GENBA_CD !=''*/ AND HST_GENBA_CD = /*data.HST_GENBA_CD*//*END*/
/*IF !data.UPN_GYOUSHA_CD.IsNull && data.UPN_GYOUSHA_CD !=''*/ AND MIN_TMU.UPN_GYOUSHA_CD = /*data.UPN_GYOUSHA_CD*//*END*/
/*IF !data.SBN_GYOUSHA_CD.IsNull && data.SBN_GYOUSHA_CD !=''*/ AND TME.SBN_JYUTAKUSHA_CD = /*data.SBN_GYOUSHA_CD*//*END*/
/*IF !data.UPN_SAKI_GENBA_CD.IsNull && data.UPN_SAKI_GENBA_CD !=''*/ AND MAX_TMU.UPN_SAKI_GENBA_CD = /*data.UPN_SAKI_GENBA_CD*//*END*/
----/*IF !data.LAST_SBN_GYOUSHA_CD.IsNull && data.LAST_SBN_GYOUSHA_CD !=''*/ AND TME.LAST_SBN_GYOUSHA_CD = /*data.LAST_SBN_GYOUSHA_CD*//*END*/
----/*IF !data.LAST_SBN_GENBA_CD.IsNull && data.LAST_SBN_GENBA_CD !='' */ AND TME.LAST_SBN_GENBA_CD = /*data.LAST_SBN_GENBA_CD*//*END*/
/*IF data.DETAIL_SYSTEM_ID != null && data.DETAIL_SYSTEM_ID.Count != 0 */ ) OR (  CONVERT(NVARCHAR(100), TMD.DETAIL_SYSTEM_ID) IN (/*data.STRING_DETAIL_SYSTEM_ID*/1)))
-- ELSE ))
/*END*/ 

UNION ALL

----電子マニ場合紐付け対象一次マニフェスト情報取得SQL文
SELECT R18EX.CONNECT_SYSTEM_ID AS SYSTEM_ID
     , R18EX.CONNECT_SEQ AS SEQ
     , '電子' AS HAIKI_KBN_NAME 
     , CASE ISDATE(DT_R18.HIKIWATASHI_DATE)
        WHEN 1 THEN CONVERT(datetime,DT_R18.HIKIWATASHI_DATE,120)
        ELSE NULL
     END AS KOUFU_DATE --交付日付 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.CONNECT_MANIFEST_ID ELSE DT_R18.MANIFEST_ID END) AS MANIFEST_ID   --交付番号
     , DT_R18.HST_SHA_NAME AS HST_GYOUSHA_NAME 
     , DT_R18.HST_JOU_NAME AS HST_GENBA_NAME 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HAIKI_SHURUI_NAME ELSE DT_R18.HAIKI_SHURUI END) AS HAIKI_SHURUI_NAME_RYAKU
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HAIKI_NAME ELSE DT_R18.HAIKI_NAME END) AS HAIKI_NAME
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HOUKOKUSHO_BUNRUI_NAME_RYAKU ELSE MHB.HOUKOKUSHO_BUNRUI_NAME_RYAKU END) AS HOUKOKUSHO_BUNRUI_NAME_RYAKU
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HAIKI_SUU ELSE DT_R18.HAIKI_KAKUTEI_SUU END) AS HAIKI_SUU
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.UNIT_NAME_RYAKU ELSE MU.UNIT_NAME_RYAKU END) AS UNIT_NAME_RYAKU
     , R18EX.CONNECT_GENNYOU_SUU GENNYOU_SUU
     , DT_R18.NISUGATA_NAME AS NISUGATA_NAME_RYAKU 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.SHOBUN_HOUHOU_NAME_RYAKU ELSE DT_R18.SBN_WAY_NAME END) AS SHOBUN_HOUHOU_NAME_RYAKU
     , MINR19.UPN_SHA_NAME AS UPN_GYOUSHA_NAME 
     , DT_R18.SBN_SHA_NAME AS SBN_JYUTAKUSHA_NAME 
     , MAXR19.UPNSAKI_JOU_NAME AS UPN_SAKI_GENBA_NAME 
     , DT_R13.LAST_SBN_JOU_NAME AS LAST_SBN_GENBA_NAME      --最終処分事業場名称 
     , CAST('4' AS SMALLINT )AS HAIKI_KBN_CD                --マニ種類 
     , R18EX.HST_GYOUSHA_CD AS HST_GYOUSHA_CD               --排出事業者CD 
     , R18EX.HST_GENBA_CD AS HST_GENBA_CD                   --排出事業場CD 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HAIKI_DAI_CODE + R18EX.HAIKI_CHU_CODE + R18EX.HAIKI_SHO_CODE
            ELSE DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE END) AS HAIKI_SHURUI_CD --電子廃棄物種類 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HAIKI_NAME_CD
            ELSE (SELECT TOP 1 HAIKI_NAME_CD FROM M_DENSHI_HAIKI_NAME WITH(NOLOCK) WHERE DELETE_FLG = 0 AND HAIKI_NAME = DT_R18.HAIKI_NAME) END) AS HAIKI_NAME_CD --電子廃棄物名称CD
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HOUKOKUSHO_BUNRUI_CD ELSE MDHS.HOUKOKUSHO_BUNRUI_CD END) AS HOUKOKUSHO_BUNRUI_CD    --報告書分類CD 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.HAIKI_UNIT_CD ELSE DT_R18.HAIKI_KAKUTEI_UNIT_CODE END) AS HAIKI_UNIT_CODE            --廃棄物単位CD 
     , DT_R18.NISUGATA_CODE AS NISUGATA_CODE                --荷姿CD 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.SBN_HOUHOU_CD ELSE CONVERT(nvarchar,DT_R18.SBN_WAY_CODE) END) AS SBN_HOUHOU_CD                 --処分方法CD 
     , MINR19EX.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD            --運搬受託者 
     , R18EX.SBN_GYOUSHA_CD AS SBN_JYUTAKUSHA_CD            --処分受託者 
     , MAXR19EX.UPN_GYOUSHA_CD AS SBN_GENBA_CD              --最後の運搬区間の運搬先事業場(処分事業場) 
     , DT_R13.LAST_SBN_GYOUSHA_CD AS LAST_SBN_GYOUSHA_CD    --最終処分事業者CD 
     , DT_R13.LAST_SBN_GENBA_CD AS LAST_SBN_GENBA_CD        --最終処分事業場CD 
     , CASE ISDATE(DT_R18.SBN_END_DATE)
            WHEN 1 THEN CONVERT(datetime, DT_R18.SBN_END_DATE ,120)
            ELSE NULL
       END AS SBN_END_DATE                                                             --処分終了日 
     , CASE ISDATE(MAXR19.UPN_END_DATE) WHEN 1 THEN CONVERT(datetime,MAXR19.UPN_END_DATE,120)else null end AS LAST_UPN_END_DATE --運搬終了日 
     , (CASE WHEN R18EX.DETAIL_SYSTEM_ID IS NOT NULL THEN R18EX.KANSAN_SUU
            ELSE (SELECT CASE WHEN TBL.KANSANSHIKI=0 THEN DT_R18.HAIKI_KAKUTEI_SUU*TBL.KANSANCHI ELSE DT_R18.HAIKI_KAKUTEI_SUU/TBL.KANSANCHI END 
                    FROM (SELECT KANSANSHIKI, KANSANCHI FROM M_MANIFEST_KANSAN WITH(NOLOCK) WHERE DELETE_FLG = 0 
                            AND HOUKOKUSHO_BUNRUI_CD = MDHS.HOUKOKUSHO_BUNRUI_CD
                            AND HAIKI_NAME_CD = (SELECT TOP 1 HAIKI_NAME_CD 
                                                    FROM M_DENSHI_HAIKI_NAME WITH(NOLOCK) 
                                                WHERE DELETE_FLG = 0  
                                                    AND HAIKI_NAME = DT_R18.HAIKI_NAME 
                                                )
                            AND UNIT_CD = DT_R18.HAIKI_KAKUTEI_UNIT_CODE
                            AND NISUGATA_CD = DT_R18.NISUGATA_CODE) AS TBL) END) AS KANSAN_SUU                                   --換算後数量
     , DT_R18.KANRI_ID               AS KANRI_ID         --管理番号 
     , DMT.LATEST_SEQ               AS LATEST_SEQ       --最後のSEQ 
     , NULL                          AS DETAIL_SYSTEM_ID --マニ明細SYSTEM_ID 
     , NULL                          AS TME_SYSTEM_ID
     , NULL                          AS TME_SEQ
     , NULL                          AS TME_TIME_STAMP
     , R18EX.CONNECT_SYSTEM_ID AS R18EX_SYSTEM_ID
     , R18EX.CONNECT_SEQ AS R18EX_SEQ
     , R18EX.CONNECT_TIME_STAMP AS R18EX_TIME_STAMP
     , ISNULL(DT_R13.LAST_SBN_JOU_NAME, '')
         + ISNULL(DT_R13.LAST_SBN_JOU_ADDRESS1, '')
         + ISNULL(DT_R13.LAST_SBN_JOU_ADDRESS2, '')
         + ISNULL(DT_R13.LAST_SBN_JOU_ADDRESS3, '')
         + ISNULL(DT_R13.LAST_SBN_JOU_ADDRESS4, '')
       AS LAST_SBN_GENBA_NAME_AND_ADDRESS                   --最終処分事業場名称 + 住所 
     , DT_R13.LAST_SBN_END_DATE AS LAST_SBN_END_DATE        --最終処分終了日 

  FROM DT_MF_TOC AS DMT WITH(NOLOCK) 
 INNER JOIN DT_R18 WITH(NOLOCK) 
    ON DMT.KANRI_ID = DT_R18.KANRI_ID 
   AND DMT.LATEST_SEQ = DT_R18.SEQ 
   AND DT_R18.MANIFEST_ID IS NOT NULL 
   AND DT_R18.MANIFEST_ID <> '' 
 INNER JOIN (
        SELECT
            EX.SYSTEM_ID AS EX_SYS_ID
            ,EX.SEQ AS EX_SEQ
            ,EX.HST_GYOUSHA_CD
            ,EX.HST_GENBA_CD
            ,EX.SBN_GYOUSHA_CD
            ,EX.KANRI_ID AS EX_KANRI_ID
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.GENNYOU_SUU ELSE EX.GENNYOU_SUU END) AS  CONNECT_GENNYOU_SUU
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN convert(int,MIX.TIME_STAMP) ELSE convert(int,EX.TIME_STAMP) END) AS CONNECT_TIME_STAMP
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE EX.SYSTEM_ID END) AS CONNECT_SYSTEM_ID
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SEQ ELSE EX.SEQ END) AS CONNECT_SEQ
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.MANIFEST_ID ELSE EX.MANIFEST_ID END) AS CONNECT_MANIFEST_ID
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.HAIKI_DAI_CODE + MIX.HAIKI_CHU_CODE + MIX.HAIKI_SHO_CODE
                ELSE R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE END) AS CONNECT_HAIKI_SHURUI_CD
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.HAIKI_NAME_CD ELSE EX.HAIKI_NAME_CD END) AS CONNECT_HAIKI_NAME_CD
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN M_DENSHI_HAIKI_SHURUI_MIX.HOUKOKUSHO_BUNRUI_CD ELSE M_DENSHI_HAIKI_SHURUI_R18.HOUKOKUSHO_BUNRUI_CD END) AS CONNECT_HOUKOKUSHO_BUNRUI_CD
            ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SBN_HOUHOU_CD ELSE R18.SBN_WAY_CODE END) AS CONNECT_SBN_HOUHOU_CD
            ,MIX.*
			,M_DENSHI_HAIKI_NAME.HAIKI_NAME
        FROM DT_R18_EX AS EX WITH(NOLOCK)
        INNER JOIN (
                SELECT DT_R18.* FROM DT_R18 WITH(NOLOCK)
                INNER JOIN DT_MF_TOC WITH(NOLOCK) ON
                    ((DT_R18.KANRI_ID = DT_MF_TOC.KANRI_ID) AND (DT_R18.SEQ = DT_MF_TOC.LATEST_SEQ))
            ) AS R18
                ON EX.KANRI_ID = R18.KANRI_ID
        LEFT JOIN (
                SELECT
                    M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME
                    ,M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME_RYAKU
                    ,M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD
                    ,M_UNIT.UNIT_NAME_RYAKU
                    ,M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU
                    ,DT_R18_MIX.*
                FROM DT_R18_MIX WITH(NOLOCK)
                LEFT JOIN (SELECT * FROM M_DENSHI_HAIKI_SHURUI WITH(NOLOCK) WHERE DELETE_FLG = 0) AS M_DENSHI_HAIKI_SHURUI
                    ON (DT_R18_MIX.HAIKI_DAI_CODE + DT_R18_MIX.HAIKI_CHU_CODE + DT_R18_MIX.HAIKI_SHO_CODE) = M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD
                LEFT JOIN (SELECT * FROM M_HOUKOKUSHO_BUNRUI WITH(NOLOCK) WHERE DELETE_FLG = 0) AS M_HOUKOKUSHO_BUNRUI
                    ON M_DENSHI_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD = M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD
                LEFT JOIN (SELECT * FROM M_UNIT WITH(NOLOCK) WHERE DELETE_FLG = 0) AS M_UNIT
                    ON DT_R18_MIX.HAIKI_UNIT_CD = M_UNIT.UNIT_CD
                LEFT JOIN (SELECT * FROM M_SHOBUN_HOUHOU WITH(NOLOCK) WHERE DELETE_FLG = 0) AS M_SHOBUN_HOUHOU
                    ON DT_R18_MIX.SBN_HOUHOU_CD = M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_CD
                WHERE DT_R18_MIX.DELETE_FLG = 0
            ) AS MIX
                ON EX.SYSTEM_ID = MIX.SYSTEM_ID
        LEFT JOIN (SELECT * FROM M_DENSHI_HAIKI_SHURUI WITH(NOLOCK) WHERE DELETE_FLG = 0) AS M_DENSHI_HAIKI_SHURUI_MIX
            ON (MIX.HAIKI_DAI_CODE + MIX.HAIKI_CHU_CODE + MIX.HAIKI_SHO_CODE) = M_DENSHI_HAIKI_SHURUI_MIX.HAIKI_SHURUI_CD
        LEFT JOIN (SELECT * FROM M_DENSHI_HAIKI_SHURUI WITH(NOLOCK) WHERE DELETE_FLG = 0) AS M_DENSHI_HAIKI_SHURUI_R18
            ON (R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE) = M_DENSHI_HAIKI_SHURUI_R18.HAIKI_SHURUI_CD
        LEFT JOIN (SELECT * FROM M_DENSHI_HAIKI_NAME WITH(NOLOCK) WHERE DELETE_FLG = 0) AS M_DENSHI_HAIKI_NAME
		    ON R18.HST_SHA_EDI_MEMBER_ID= M_DENSHI_HAIKI_NAME.EDI_MEMBER_ID AND MIX.HAIKI_NAME_CD = M_DENSHI_HAIKI_NAME.HAIKI_NAME_CD
        WHERE EX.DELETE_FLG = 0
    ) AS R18EX
        ON R18EX.EX_KANRI_ID = DT_R18.KANRI_ID
 LEFT JOIN M_GYOUSHA AS HST_GYOUSHA WITH(NOLOCK) ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
 LEFT JOIN T_MANIFEST_RELATION AS TMR WITH(NOLOCK) 
    ON R18EX.CONNECT_SYSTEM_ID = TMR.FIRST_SYSTEM_ID 
    AND TMR.FIRST_HAIKI_KBN_CD = 4
   AND TMR.DELETE_FLG = 0 
 INNER JOIN DT_R19_EX AS MAXR19EX WITH(NOLOCK) 
    ON R18EX.EX_SYS_ID = MAXR19EX.SYSTEM_ID
   AND R18EX.EX_SEQ = MAXR19EX.SEQ    
 INNER JOIN DT_R19 AS MAXR19 WITH(NOLOCK) 
    ON MAXR19EX.KANRI_ID = MAXR19.KANRI_ID 
   AND MAXR19EX.UPN_ROUTE_NO = MAXR19.UPN_ROUTE_NO 
 INNER JOIN (SELECT DT_R19.KANRI_ID 
                   , DT_R19.SEQ 
                   , MAX(DT_R19.UPN_ROUTE_NO) AS UPN_ROUTE_NO 
                FROM DT_MF_TOC WITH(NOLOCK) 
               INNER JOIN DT_R19 WITH(NOLOCK) 
                  ON DT_MF_TOC.KANRI_ID = DT_R19.KANRI_ID 
                 AND DT_MF_TOC.LATEST_SEQ = DT_R19.SEQ 
                 WHERE DT_R19.UPN_END_DATE IS NOT NULL  
                GROUP BY DT_R19.KANRI_ID 
                    , DT_R19.SEQ 
             ) AS SEARCHR19 
    ON MAXR19.KANRI_ID = SEARCHR19.KANRI_ID 
   AND MAXR19.SEQ = SEARCHR19.SEQ 
   AND MAXR19.UPN_ROUTE_NO = SEARCHR19.UPN_ROUTE_NO
 INNER JOIN DT_R19_EX AS MINR19EX WITH(NOLOCK) 
    ON R18EX.EX_SYS_ID = MINR19EX.SYSTEM_ID 
   AND R18EX.EX_SEQ = MINR19EX.SEQ 
   AND MINR19EX.UPN_ROUTE_NO = 1
   AND MINR19EX.DELETE_FLG = 0
 INNER JOIN DT_R19 AS MINR19 WITH(NOLOCK) 
    ON MINR19EX.KANRI_ID = MINR19.KANRI_ID 
   AND DMT.LATEST_SEQ = MINR19.SEQ
   AND MINR19EX.UPN_ROUTE_NO =  MINR19.UPN_ROUTE_NO 
 LEFT JOIN (
   SELECT
      DT_R13.KANRI_ID,
      DT_R13.SEQ,
      DT_R13.MANIFEST_ID,
      DT_R13.LAST_SBN_JOU_NAME,
      DT_R13.LAST_SBN_JOU_ADDRESS1,
      DT_R13.LAST_SBN_JOU_ADDRESS2,
      DT_R13.LAST_SBN_JOU_ADDRESS3,
      DT_R13.LAST_SBN_JOU_ADDRESS4,
      DT_R13.LAST_SBN_END_DATE,
      DT_R13.LAST_SBN_GYOUSHA_CD,
      DT_R13.LAST_SBN_GENBA_CD,
      DT_R13.ROW_COUNT
   FROM 
      (SELECT
        DT_R13.KANRI_ID,
        DT_R13.SEQ,
        DT_R13.MANIFEST_ID,
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13.LAST_SBN_JOU_NAME
          ELSE '全' + CONVERT(varchar, DT_R13_COUNT.ROW_COUNT) + '件'
        END AS LAST_SBN_JOU_NAME,
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13.LAST_SBN_JOU_ADDRESS1
          ELSE '' 
        END AS LAST_SBN_JOU_ADDRESS1,  
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13.LAST_SBN_JOU_ADDRESS2
          ELSE '' 
        END AS LAST_SBN_JOU_ADDRESS2,  
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13.LAST_SBN_JOU_ADDRESS3
          ELSE '' 
        END AS LAST_SBN_JOU_ADDRESS3,
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13.LAST_SBN_JOU_ADDRESS3
          ELSE '' 
        END AS LAST_SBN_JOU_ADDRESS4,
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13.LAST_SBN_END_DATE
          ELSE '' 
        END AS LAST_SBN_END_DATE,
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13_EX.LAST_SBN_GYOUSHA_CD
          ELSE '' 
        END AS LAST_SBN_GYOUSHA_CD,
        CASE WHEN DT_R13_COUNT.ROW_COUNT = 1 THEN DT_R13_EX.LAST_SBN_GENBA_CD
          ELSE '' 
        END AS LAST_SBN_GENBA_CD,
        DT_R13_COUNT.ROW_COUNT
      FROM
        DT_R13 WITH(NOLOCK)
      INNER JOIN 
        (
        SELECT
          DT_R13.KANRI_ID,
          DT_R13.SEQ,
          COUNT(*) AS ROW_COUNT
        FROM
          DT_R13 WITH(NOLOCK)
        GROUP BY
          DT_R13.KANRI_ID,
          DT_R13.SEQ
        ) AS DT_R13_COUNT
      ON
        DT_R13.KANRI_ID = DT_R13_COUNT.KANRI_ID
        AND DT_R13.SEQ = DT_R13_COUNT.SEQ
      LEFT JOIN
        DT_R13_EX WITH(NOLOCK)
      ON
        DT_R13.KANRI_ID = DT_R13_EX.KANRI_ID
        AND DT_R13_EX.REC_SEQ = DT_R13.REC_SEQ
        AND DT_R13_EX.DELETE_FLG = 0
      ) DT_R13
   GROUP BY
      DT_R13.KANRI_ID,
      DT_R13.SEQ,
      DT_R13.MANIFEST_ID,
      DT_R13.LAST_SBN_JOU_NAME,
      DT_R13.LAST_SBN_JOU_ADDRESS1,
      DT_R13.LAST_SBN_JOU_ADDRESS2,
      DT_R13.LAST_SBN_JOU_ADDRESS3,
      DT_R13.LAST_SBN_JOU_ADDRESS4,
      DT_R13.LAST_SBN_END_DATE,
      DT_R13.LAST_SBN_GYOUSHA_CD,
      DT_R13.LAST_SBN_GENBA_CD,
      DT_R13.ROW_COUNT
  ) AS DT_R13 ON DT_R18.KANRI_ID = DT_R13.KANRI_ID AND DT_R18.SEQ = DT_R13.SEQ
  LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI AS MDHS WITH(NOLOCK) 
    ON MDHS.HAIKI_SHURUI_CD = DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE 
   AND MDHS.DELETE_FLG = 0
  LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI AS MHB WITH(NOLOCK) 
    ON MHB.HOUKOKUSHO_BUNRUI_CD = MDHS.HOUKOKUSHO_BUNRUI_CD 
   AND MHB.DELETE_FLG = 0 
  LEFT OUTER JOIN M_UNIT AS MU WITH(NOLOCK) 
    ON MU.UNIT_CD = DT_R18.HAIKI_KAKUTEI_UNIT_CODE 
   AND MU.DELETE_FLG = 0 
  LEFT OUTER JOIN M_GENBA MGB_SHOBUN WITH(NOLOCK)
    ON MGB_SHOBUN.GYOUSHA_CD =  MAXR19EX.UPNSAKI_GYOUSHA_CD
  AND MGB_SHOBUN.GENBA_CD = MAXR19EX.UPNSAKI_GENBA_CD
  AND MGB_SHOBUN.DELETE_FLG = 0

 WHERE (DT_R18.FIRST_MANIFEST_FLAG IS NULL or DT_R18.FIRST_MANIFEST_FLAG = '' or ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0) 
 AND TMR.FIRST_SYSTEM_ID IS NULL
 AND DT_R18.SBN_END_DATE IS NOT NULL
 AND DT_R18.SBN_ENDREP_FLAG = 1
 AND DT_R18.SBN_ENDREP_KBN = 1
 AND (R18EX.SBN_ENDREP_KBN IS NULL OR (R18EX.SBN_ENDREP_KBN IS NOT NULL AND R18EX.SBN_ENDREP_KBN = 1))
 AND DMT.STATUS_FLAG = 4
 AND MGB_SHOBUN.JISHA_KBN = 1
/*IF !data.NEXT_SYSTEM_ID.IsNull && data.MANI_KBN == '4'*/AND ( R18EX.CONNECT_SYSTEM_ID IS NULL OR R18EX.CONNECT_SYSTEM_ID != /*data.NEXT_SYSTEM_ID*/ ) /*END*/
   AND (( 1 = /*data.elec*/1
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '1'*/
      /*IF !data.START_DATETIME.IsNull && data.START_DATETIME !=''*/ 
            AND CASE ISDATE(DT_R18.HIKIWATASHI_DATE) WHEN 1 THEN CONVERT(datetime,DT_R18.HIKIWATASHI_DATE,120) ELSE NULL END  >= /*data.START_DATETIME*/
      /*END*/
      /*IF !data.END_DATETIME.IsNull && data.END_DATETIME !=''*/ 
            AND CASE ISDATE(DT_R18.HIKIWATASHI_DATE) WHEN 1 THEN CONVERT(datetime,DT_R18.HIKIWATASHI_DATE,120) ELSE NULL END <= /*data.END_DATETIME*/
      /*END*/
/*END*/
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '2'*/
      /*IF !data.START_DATETIME.IsNull && data.START_DATETIME !=''*/ 
            AND CASE ISDATE(MAXR19.UPN_END_DATE) WHEN 1 THEN CONVERT(datetime,MAXR19.UPN_END_DATE,120) ELSE NULL END >= /*data.START_DATETIME*/
      /*END*/
      /*IF !data.END_DATETIME.IsNull && data.END_DATETIME !=''*/ 
            AND CASE ISDATE(MAXR19.UPN_END_DATE) WHEN 1 THEN CONVERT(datetime,MAXR19.UPN_END_DATE,120) ELSE NULL END <= /*data.END_DATETIME*/
      /*END*/
/*END*/
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '3'*/
      /*IF !data.START_DATETIME.IsNull && data.START_DATETIME !=''*/ 
            AND CASE ISDATE(DT_R18.SBN_END_DATE) WHEN 1 THEN CONVERT(datetime,DT_R18.SBN_END_DATE,120) ELSE NULL END >= /*data.START_DATETIME*/
      /*END*/
      /*IF !data.END_DATETIME.IsNull && data.END_DATETIME !=''*/ 
            AND CASE ISDATE(DT_R18.SBN_END_DATE) WHEN 1 THEN CONVERT(datetime,DT_R18.SBN_END_DATE,120) ELSE NULL END <= /*data.END_DATETIME*/
      /*END*/
/*END*/
/*IF !data.HAIKI_SHURUI_CD.IsNull && data.HAIKI_SHURUI_CD != ''*/ AND R18EX.CONNECT_HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*//*END*/
/*IF !data.HAIKI_NAME_CD.IsNull && data.HAIKI_NAME_CD !=''*/ AND R18EX.CONNECT_HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*//*END*/
/*IF !data.HOUKOKUSHO_BUNRUI_CD.IsNull && data.HOUKOKUSHO_BUNRUI_CD !=''*/ AND R18EX.CONNECT_HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*//*END*/
/*IF !data.NISUGATA_CD.IsNull && data.NISUGATA_CD !=''*/ AND NISUGATA_CODE = /*data.NISUGATA_CD*//*END*/
/*IF !data.SBN_HOUHOU_CD.IsNull && data.SBN_HOUHOU_CD !=''*/ AND R18EX.CONNECT_SBN_HOUHOU_CD = /*data.SBN_HOUHOU_CD*//*END*/
/*IF !data.HST_GYOUSHA_CD.IsNull && data.HST_GYOUSHA_CD !=''*/ AND HST_GYOUSHA_CD = /*data.HST_GYOUSHA_CD*//*END*/
/*IF !data.HST_GENBA_CD.IsNull && data.HST_GENBA_CD !=''*/ AND HST_GENBA_CD = /*data.HST_GENBA_CD*//*END*/
/*IF !data.UPN_GYOUSHA_CD.IsNull && data.UPN_GYOUSHA_CD !=''*/ AND MINR19EX.UPN_GYOUSHA_CD = /*data.UPN_GYOUSHA_CD*//*END*/
/*IF !data.SBN_GYOUSHA_CD.IsNull && data.SBN_GYOUSHA_CD!=''*/ AND R18EX.SBN_GYOUSHA_CD = /*data.SBN_GYOUSHA_CD*//*END*/
/*IF !data.UPN_SAKI_GENBA_CD.IsNull && data.UPN_SAKI_GENBA_CD !=''*/ AND MAXR19EX.UPNSAKI_GENBA_CD = /*data.UPN_SAKI_GENBA_CD*//*END*/
----/*IF !data.LAST_SBN_GYOUSHA_CD.IsNull && data.LAST_SBN_GYOUSHA_CD!=''*/ AND DT_R13_EX.LAST_SBN_GYOUSHA_CD = /*data.LAST_SBN_GYOUSHA_CD*//*END*/
----/*IF !data.LAST_SBN_GENBA_CD.IsNull && data.LAST_SBN_GENBA_CD!=''*/ AND DT_R13_EX.LAST_SBN_GENBA_CD = /*data.LAST_SBN_GENBA_CD*//*END*/
/*IF data.ELEC_SYSTEM_ID != null && data.ELEC_SYSTEM_ID.Count != 0 */ ) OR (R18EX.CONNECT_SYSTEM_ID IS NOT NULL AND CONVERT(NVARCHAR(100), R18EX.CONNECT_SYSTEM_ID) IN 
(/*data.STRING_ELEC_SYSTEM_ID*/1)))
-- ELSE )) 
/*END*/
/*IF data.LAST_SBN_GENBA_TYPE == '1'*/AND ISNULL(DT_R13.LAST_SBN_JOU_NAME, '') <> ''/*END*/
/*IF data.LAST_SBN_GENBA_TYPE == '2'*/AND ISNULL(DT_R13.LAST_SBN_JOU_NAME, '') = ''/*END*/

 ORDER BY 
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '1'*/KOUFU_DATE,/*END*/
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '2'*/LAST_UPN_END_DATE,/*END*/
/*IF !data.DATETIME_TYPE.IsNull && data.DATETIME_TYPE !='' && data.DATETIME_TYPE == '3'*/SBN_END_DATE,/*END*/
    MANIFEST_ID,
    HAIKI_SHURUI_CD,
    SYSTEM_ID, 
    DETAIL_SYSTEM_ID 
