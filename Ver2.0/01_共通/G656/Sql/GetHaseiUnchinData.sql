﻿-- 運賃
SELECT 
   DENPYOU_TYPE AS '伝票'
  ,DENPYOU_NUMBER AS '伝票番号'
  ,DENPYOU_DATE AS '伝票日付'
  ,TORIHIKISAKI_NAME AS '取引先'
  ,GYOUSHA_NAME AS '業者'
  ,GENBA_NAME AS '現場'
  ,SYSTEM_ID AS HIDDEN_SYSTEM_ID
  ,HAIKI_KBN_CD AS HIDDEN_HAIKI_KBN_CD
FROM(
-- 受入入力
 SELECT 
   '受入' AS DENPYOU_TYPE
  ,5 AS DENPYOU_TYPE_KBN
  ,UKEIRE_NUMBER AS DENPYOU_NUMBER
  ,DENPYOU_DATE AS DENPYOU_DATE
  ,TORIHIKISAKI_NAME
  ,GYOUSHA_NAME
  ,GENBA_NAME
  ,SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UKEIRE_ENTRY
 WHERE UKEIRE_NUMBER = /*data.RENKEI_NUMBER*/
 /*IF !data.UKEIRE_FLG || data.RENKEI_DENSHU_KBN_CD != 1*/
   AND 1 = 0
 /*END*/
   AND DELETE_FLG = 0
 
 UNION
  
 --  収集受付
 SELECT
   '収集受付' AS DENPYOU_TYPE
  ,1 AS DENPYOU_TYPE_KBN
  ,SS.UKETSUKE_NUMBER AS DENPYOU_NUMBER
  ,SS.SAGYOU_DATE AS DENPYOU_DATE
  ,SS.TORIHIKISAKI_NAME
  ,SS.GYOUSHA_NAME
  ,SS.GENBA_NAME
  ,SS.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UKETSUKE_SS_ENTRY SS
 INNER JOIN T_UKEIRE_ENTRY UKEIRE
         ON UKEIRE.UKEIRE_NUMBER = /*data.RENKEI_NUMBER*/
        AND UKEIRE.DELETE_FLG = 0
 WHERE SS.UKETSUKE_NUMBER = UKEIRE.UKETSUKE_NUMBER
 /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 1*/
   AND 1 = 0
 /*END*/
   AND SS.DELETE_FLG = 0
 
 UNION
 
 --  持込受付
 SELECT
   '持込受付' AS DENPYOU_TYPE
  ,3 AS DENPYOU_TYPE_KBN
  ,MK.UKETSUKE_NUMBER AS DENPYOU_NUMBER
  ,MK.SAGYOU_DATE AS DENPYOU_DATE
  ,MK.TORIHIKISAKI_NAME
  ,MK.GYOUSHA_NAME
  ,MK.GENBA_NAME
  ,MK.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UKETSUKE_MK_ENTRY MK
 INNER JOIN T_UKEIRE_ENTRY UKEIRE
         ON UKEIRE.UKEIRE_NUMBER = /*data.RENKEI_NUMBER*/
        AND UKEIRE.DELETE_FLG = 0
 WHERE MK.UKETSUKE_NUMBER = UKEIRE.UKETSUKE_NUMBER
 /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 1*/
   AND 1 = 0
 /*END*/
   AND MK.DELETE_FLG = 0
   
 UNION
 
 SELECT 
   '出荷' AS DENPYOU_TYPE
  ,6 AS DENPYOU_TYPE_KBN
  ,SHUKKA.SHUKKA_NUMBER AS DENPYOU_NUMBER
  ,SHUKKA.DENPYOU_DATE AS DENPYOU_DATE
  ,SHUKKA.TORIHIKISAKI_NAME
  ,SHUKKA.GYOUSHA_NAME
  ,SHUKKA.GENBA_NAME
  ,SHUKKA.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_SHUKKA_ENTRY SHUKKA
 WHERE SHUKKA.SHUKKA_NUMBER = /*data.RENKEI_NUMBER*/
 /*IF !data.SHUKKA_FLG || data.RENKEI_DENSHU_KBN_CD != 2*/
   AND 1 = 0
 /*END*/
   AND SHUKKA.DELETE_FLG = 0
   
 UNION
 
--  出荷受付
 SELECT
   '出荷受付' AS DENPYOU_TYPE
  ,2 AS DENPYOU_TYPE_KBN
  ,SK.UKETSUKE_NUMBER AS DENPYOU_NUMBER
  ,SK.SAGYOU_DATE AS DENPYOU_DATE
  ,SK.TORIHIKISAKI_NAME
  ,SK.GYOUSHA_NAME
  ,SK.GENBA_NAME
  ,SK.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UKETSUKE_SK_ENTRY SK
 INNER JOIN T_SHUKKA_ENTRY SHUKKA
         ON SHUKKA.SHUKKA_NUMBER = /*data.RENKEI_NUMBER*/
        AND SHUKKA.DELETE_FLG = 0
 WHERE SK.UKETSUKE_NUMBER = SHUKKA.UKETSUKE_NUMBER
 /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 2*/
   AND 1 = 0
 /*END*/
   AND SK.DELETE_FLG = 0
 
 UNION
 
 SELECT 
   '売上支払' AS DENPYOU_TYPE
  ,7 AS DENPYOU_TYPE_KBN
  ,URSH.UR_SH_NUMBER AS DENPYOU_NUMBER
  ,URSH.DENPYOU_DATE AS DENPYOU_DATE
  ,URSH.TORIHIKISAKI_NAME
  ,URSH.GYOUSHA_NAME
  ,URSH.GENBA_NAME
  ,URSH.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UR_SH_ENTRY URSH
 WHERE URSH.UR_SH_NUMBER = /*data.RENKEI_NUMBER*/
 /*IF !data.UR_SH_FLG || data.RENKEI_DENSHU_KBN_CD != 3*/
   AND 1 = 0
 /*END*/
   AND ISNULL(URSH.DAINOU_FLG, 0) = 0
   AND URSH.DELETE_FLG = 0
 
 UNION
 
--  収集受付
 SELECT
   '収集受付' AS DENPYOU_TYPE
  ,1 AS DENPYOU_TYPE_KBN
  ,SS.UKETSUKE_NUMBER AS DENPYOU_NUMBER
  ,SS.SAGYOU_DATE AS DENPYOU_DATE
  ,SS.TORIHIKISAKI_NAME
  ,SS.GYOUSHA_NAME
  ,SS.GENBA_NAME
  ,SS.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UKETSUKE_SS_ENTRY SS
 INNER JOIN T_UR_SH_ENTRY URSH
         ON URSH.UR_SH_NUMBER = /*data.RENKEI_NUMBER*/
        AND ISNULL(URSH.DAINOU_FLG, 0) = 0
        AND URSH.DELETE_FLG = 0
 WHERE SS.UKETSUKE_NUMBER = URSH.UKETSUKE_NUMBER
 /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 3*/
   AND 1 = 0
 /*END*/
   AND SS.DELETE_FLG = 0
 
 UNION
   
--  出荷受付
 SELECT
   '出荷受付' AS DENPYOU_TYPE
  ,2 AS DENPYOU_TYPE_KBN
  ,SK.UKETSUKE_NUMBER AS DENPYOU_NUMBER
  ,SK.SAGYOU_DATE AS DENPYOU_DATE
  ,SK.TORIHIKISAKI_NAME
  ,SK.GYOUSHA_NAME
  ,SK.GENBA_NAME
  ,SK.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UKETSUKE_SK_ENTRY SK
 INNER JOIN T_UR_SH_ENTRY URSH
         ON URSH.UR_SH_NUMBER = /*data.RENKEI_NUMBER*/
        AND ISNULL(URSH.DAINOU_FLG, 0) = 0
        AND URSH.DELETE_FLG = 0
 WHERE SK.UKETSUKE_NUMBER = URSH.UKETSUKE_NUMBER
 /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 3*/
   AND 1 = 0
 /*END*/
   AND SK.DELETE_FLG = 0   
   
   UNION
   
--  持込受付
 SELECT
   '持込受付' AS DENPYOU_TYPE
  ,3 AS DENPYOU_TYPE_KBN
  ,MK.UKETSUKE_NUMBER AS DENPYOU_NUMBER
  ,MK.SAGYOU_DATE AS DENPYOU_DATE
  ,MK.TORIHIKISAKI_NAME
  ,MK.GYOUSHA_NAME
  ,MK.GENBA_NAME
  ,MK.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UKETSUKE_MK_ENTRY MK
 INNER JOIN T_UR_SH_ENTRY URSH
         ON URSH.UR_SH_NUMBER = /*data.RENKEI_NUMBER*/
        AND ISNULL(URSH.DAINOU_FLG, 0) = 0
        AND URSH.DELETE_FLG = 0
 WHERE MK.UKETSUKE_NUMBER = URSH.UKETSUKE_NUMBER
 /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 3*/
   AND 1 = 0
 /*END*/
   AND MK.DELETE_FLG = 0
   
   UNION
   
-- 代納
 SELECT 
   '代納' AS DENPYOU_TYPE
  ,10 AS DENPYOU_TYPE_KBN
  ,URSH.UR_SH_NUMBER AS DENPYOU_NUMBER
  ,URSH.DENPYOU_DATE AS DENPYOU_DATE
  ,URSH.TORIHIKISAKI_NAME
  ,URSH.GYOUSHA_NAME
  ,URSH.GENBA_NAME
  ,URSH.SYSTEM_ID
  ,'' AS HAIKI_KBN_CD
 FROM T_UR_SH_ENTRY URSH
 LEFT JOIN T_UR_SH_DETAIL DETAIL
        ON DETAIL.SYSTEM_ID = URSH.SYSTEM_ID
       AND DETAIL.SEQ = 1
 WHERE URSH.UR_SH_NUMBER = /*data.RENKEI_NUMBER*/
 /*IF !data.DAINOU_FLG || data.RENKEI_DENSHU_KBN_CD != 170*/
   AND 1 = 0
 /*END*/
   AND ISNULL(URSH.DAINOU_FLG, 0) = 1
   AND URSH.DELETE_FLG = 0
   AND DETAIL.DENPYOU_KBN_CD = 1
) AS A
ORDER BY DENPYOU_TYPE_KBN,DENPYOU_DATE,DENPYOU_NUMBER