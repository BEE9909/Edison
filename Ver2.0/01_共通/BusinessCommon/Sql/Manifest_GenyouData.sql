﻿SELECT MHS.HAIKI_KBN_CD
     , MHS.HAIKI_SHURUI_CD

	 , MG.HAIKI_NAME_CD AS HAIKI_NAME_CD
     , ISNULL(MG.SHOBUN_HOUHOU_CD,'') AS SHOBUN_HOUHOU_CD
     , ISNULL(MG.GENNYOURITSU,0) AS GENNYOURITSU
     , CAST(/*data.KANSAN_SUU*/  AS NUMERIC(14,5)) *( 100 - ISNULL(MG.GENNYOURITSU,0) ) / 100  AS GENYOU_CHI

  FROM M_HAIKI_SHURUI AS MHS

 INNER JOIN M_GENNYOURITSU AS MG 
    ON MHS.HOUKOKUSHO_BUNRUI_CD = MG.HOUKOKUSHO_BUNRUI_CD 
   AND MG.HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/
   AND MG.SHOBUN_HOUHOU_CD = /*data.SHOBUN_HOUHOU_CD*/
   AND MG.DELETE_FLG = 'false'

 WHERE MHS.DELETE_FLG = 'false'
   AND MHS.HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/
   AND MHS.HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*/
