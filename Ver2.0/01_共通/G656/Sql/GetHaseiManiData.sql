﻿-- マニフェスト
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
  -- Begin マニフェスト <- 受入 <- 収集受付 or 持込受付
    -- 受入入力
    SELECT 
      '受入' AS DENPYOU_TYPE
      ,5 AS DENPYOU_TYPE_KBN
     ,UKEIRE.UKEIRE_NUMBER AS DENPYOU_NUMBER
     ,UKEIRE.DENPYOU_DATE AS DENPYOU_DATE
     ,UKEIRE.TORIHIKISAKI_NAME
     ,UKEIRE.GYOUSHA_NAME
     ,UKEIRE.GENBA_NAME
     ,SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_UKEIRE_ENTRY UKEIRE
    WHERE UKEIRE.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
    /*IF !data.UKEIRE_FLG || data.RENKEI_DENSHU_KBN_CD != 1*/
      AND 1 = 0
    /*END*/
      AND UKEIRE.DELETE_FLG = 0

    UNION
     
    --  収集受付
    SELECT
      '収集受付' AS DENPYOU_TYPE
      ,1 AS DENPYOU_TYPE_KBN
     ,SS.UKETSUKE_NUMBER AS DENPYOU_NUMBER
     ,SS.SAGYOU_DATE AS DENPYOU_DATE
     ,SS.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME
     ,SS.GYOUSHA_NAME AS GYOUSHA_NAME
     ,SS.GENBA_NAME AS GENBA_NAME
     ,SS.SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_UKETSUKE_SS_ENTRY SS
    INNER JOIN T_UKEIRE_ENTRY UKEIRE
            ON UKEIRE.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
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
     ,MK.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME
     ,MK.GYOUSHA_NAME AS GYOUSHA_NAME
     ,MK.GENBA_NAME AS GENBA_NAME
     ,MK.SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_UKETSUKE_MK_ENTRY MK
    INNER JOIN T_UKEIRE_ENTRY UKEIRE
            ON UKEIRE.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
           AND UKEIRE.DELETE_FLG = 0
    WHERE MK.UKETSUKE_NUMBER = UKEIRE.UKETSUKE_NUMBER
    /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 1*/
      AND 1 = 0
    /*END*/
      AND MK.DELETE_FLG = 0
      
  -- End マニフェスト <- 受入 <- 収集受付 or 持込受付

  -- Begin マニフェスト <- 出荷 <- 出荷受付
    UNION

    SELECT 
      '出荷' AS DENPYOU_TYPE
      ,6 AS DENPYOU_TYPE_KBN
     ,SHUKKA.SHUKKA_NUMBER AS DENPYOU_NUMBER
     ,SHUKKA.DENPYOU_DATE AS DENPYOU_DATE
     ,SHUKKA.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME
     ,SHUKKA.GYOUSHA_NAME AS GYOUSHA_NAME
     ,SHUKKA.GENBA_NAME AS GENBA_NAME
     ,SHUKKA.SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_SHUKKA_ENTRY SHUKKA
    WHERE SHUKKA.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
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
     ,SK.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME
     ,SK.GYOUSHA_NAME AS GYOUSHA_NAME
     ,SK.GENBA_NAME AS GENBA_NAME
     ,SK.SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_UKETSUKE_SK_ENTRY SK
    INNER JOIN T_SHUKKA_ENTRY SHUKKA
            ON SHUKKA.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
           AND SHUKKA.DELETE_FLG = 0
    WHERE SK.UKETSUKE_NUMBER = SHUKKA.UKETSUKE_NUMBER
    /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 2*/
      AND 1 = 0
    /*END*/
      AND SK.DELETE_FLG = 0

  -- End マニフェスト <- 出荷 <- 出荷受付

  -- Begin マニフェスト <- 売上/支払 <- 収集受付 or 出荷受付 or 持込受付

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
    WHERE URSH.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
    /*IF !data.UR_SH_FLG || data.RENKEI_DENSHU_KBN_CD != 3*/
      AND 1 = 0
    /*END*/
      AND ISNULL(URSH.DAINOU_FLG,0) = 0
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
            ON URSH.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
           AND URSH.DELETE_FLG = 0
           AND ISNULL(URSH.DAINOU_FLG,0) = 0
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
            ON URSH.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
           AND URSH.DELETE_FLG = 0
           AND ISNULL(URSH.DAINOU_FLG,0) = 0
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
            ON URSH.SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
           AND URSH.DELETE_FLG = 0
           AND ISNULL(URSH.DAINOU_FLG,0) = 0
    WHERE MK.UKETSUKE_NUMBER = URSH.UKETSUKE_NUMBER
    /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 3*/
      AND 1 = 0
    /*END*/
      AND MK.DELETE_FLG = 0

  -- End マニフェスト <- 売上/支払 <- 収集受付 or 出荷受付 or 持込受付

  -- Begin マニフェスト <- 収集受付

    UNION

    -- 受付
    --  収集受付
    SELECT
      '収集受付' AS DENPYOU_TYPE
     ,1 AS DENPYOU_TYPE_KBN
     ,UKETSUKE_NUMBER AS DENPYOU_NUMBER
     ,SAGYOU_DATE AS DENPYOU_DATE
     ,TORIHIKISAKI_NAME
     ,GYOUSHA_NAME
     ,GENBA_NAME
     ,SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_UKETSUKE_SS_ENTRY WHERE SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
    /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 100*/
      AND 1 = 0
    /*END*/
      AND DELETE_FLG = 0

  -- End マニフェスト <- 収集受付

  -- Begin マニフェスト <- 出荷受付

    UNION

    --  出荷受付
    SELECT
      '出荷受付' AS DENPYOU_TYPE
     ,2 AS DENPYOU_TYPE_KBN
     ,UKETSUKE_NUMBER AS DENPYOU_NUMBER
     ,SAGYOU_DATE AS DENPYOU_DATE
     ,TORIHIKISAKI_NAME
     ,GYOUSHA_NAME
     ,GENBA_NAME
     ,SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_UKETSUKE_SK_ENTRY WHERE SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
    /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 100*/
      AND 1 = 0
    /*END*/
      AND DELETE_FLG = 0
  -- End マニフェスト <- 出荷受付

  -- Begin マニフェスト <- 持込受付
    UNION

    --  持込受付
    SELECT
      '持込受付' AS DENPYOU_TYPE
     ,3 AS DENPYOU_TYPE_KBN
     ,UKETSUKE_NUMBER AS DENPYOU_NUMBER
     ,SAGYOU_DATE AS DENPYOU_DATE
     ,TORIHIKISAKI_NAME
     ,GYOUSHA_NAME
     ,GENBA_NAME
     ,SYSTEM_ID
     ,'' AS HAIKI_KBN_CD
    FROM T_UKETSUKE_MK_ENTRY WHERE SYSTEM_ID = /*data.RENKEI_SYSTEM_ID*/
    /*IF !data.UKETSUKE_FLG || data.RENKEI_DENSHU_KBN_CD != 100*/
      AND 1 = 0
    /*END*/
      AND DELETE_FLG = 0

  -- End マニフェスト <- 持込受付

) AS A
ORDER BY DENPYOU_TYPE_KBN,DENPYOU_DATE,DENPYOU_NUMBER