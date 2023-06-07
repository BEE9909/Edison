﻿-- 持込受付
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
      ,DENPYOU_DATE
      ,TORIHIKISAKI_NAME
      ,GYOUSHA_NAME
      ,GENBA_NAME
      ,SYSTEM_ID
      ,'' AS HAIKI_KBN_CD
    FROM T_UKEIRE_ENTRY
    WHERE UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
      AND DELETE_FLG = 0
      /*IF !data.UKEIRE_FLG*/
      AND 1 = 0
      /*END*/

    UNION

    -- 売上支払
    SELECT 
       '売上支払' AS DENPYOU_TYPE
      ,7 AS DENPYOU_TYPE_KBN
      ,CONVERT(VARCHAR,UR_SH_NUMBER) AS DENPYOU_NUMBER
      ,DENPYOU_DATE
      ,TORIHIKISAKI_NAME
      ,GYOUSHA_NAME
      ,GENBA_NAME
      ,SYSTEM_ID
      ,'' AS HAIKI_KBN_CD
    FROM T_UR_SH_ENTRY
    WHERE UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
      AND ISNULL(DAINOU_FLG,0) = 0
      AND DELETE_FLG = 0
      /*IF !data.UR_SH_FLG*/
      AND 1 = 0
      /*END*/

    UNION

    -- マニフェスト
    SELECT 
       'マニフェスト' AS DENPYOU_TYPE
      ,8 AS DENPYOU_TYPE_KBN
      ,TME.MANIFEST_ID AS DENPYOU_NUMBER
      ,TME.KOUFU_DATE AS DENPYOU_DATE
      ,MT.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME
      ,TME.HST_GYOUSHA_NAME AS GYOUSHA_NAME
      ,TME.HST_GENBA_NAME AS GENBA_NAME
      ,TME.SYSTEM_ID
      ,TME.HAIKI_KBN_CD
    FROM T_MANIFEST_ENTRY TME
    LEFT JOIN M_TORIHIKISAKI MT ON MT.TORIHIKISAKI_CD = TME.TORIHIKISAKI_CD
    WHERE TME.RENKEI_SYSTEM_ID = /*data.SYSTEM_ID*/
      AND TME.RENKEI_DENSHU_KBN_CD = 100
      AND TME.DELETE_FLG = 0
      /*IF !data.MANI_FLG*/
      AND 1 = 0
      /*END*/

    UNION

    SELECT
       'マニフェスト' AS DENPYOU_TYPE
      ,8 AS DENPYOU_TYPE_KBN
      ,TME.MANIFEST_ID AS DENPYOU_NUMBER
      ,TME.KOUFU_DATE AS DENPYOU_DATE
      ,MT.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME
      ,TME.HST_GYOUSHA_NAME AS GYOUSHA_NAME
      ,TME.HST_GENBA_NAME AS GENBA_NAME
      ,TME.SYSTEM_ID
      ,TME.HAIKI_KBN_CD
    FROM T_UKEIRE_ENTRY TUE
    INNER JOIN T_MANIFEST_ENTRY TME ON TME.RENKEI_SYSTEM_ID = TUE.SYSTEM_ID AND TME.RENKEI_DENSHU_KBN_CD = 1 AND TME.DELETE_FLG = 0
    LEFT JOIN M_TORIHIKISAKI MT ON MT.TORIHIKISAKI_CD = TME.TORIHIKISAKI_CD
    WHERE TUE.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
      AND TUE.DELETE_FLG = 0
      /*IF !data.MANI_FLG*/
      AND 1 = 0
      /*END*/

    UNION

    SELECT 
       'マニフェスト' AS DENPYOU_TYPE
      ,8 AS DENPYOU_TYPE_KBN
      ,TME.MANIFEST_ID AS DENPYOU_NUMBER
      ,TME.KOUFU_DATE AS DENPYOU_DATE
      ,MT.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME
      ,TME.HST_GYOUSHA_NAME AS GYOUSHA_NAME
      ,TME.HST_GENBA_NAME AS GENBA_NAME
      ,TME.SYSTEM_ID
      ,TME.HAIKI_KBN_CD
    FROM T_UR_SH_ENTRY TUSE
    INNER JOIN T_MANIFEST_ENTRY TME ON TME.RENKEI_SYSTEM_ID = TUSE.SYSTEM_ID AND TME.RENKEI_DENSHU_KBN_CD = 3 AND TME.DELETE_FLG = 0
    LEFT JOIN M_TORIHIKISAKI MT ON MT.TORIHIKISAKI_CD = TME.TORIHIKISAKI_CD
    WHERE TUSE.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
      AND ISNULL(TUSE.DAINOU_FLG,0) = 0
      AND TUSE.DELETE_FLG = 0
      /*IF !data.MANI_FLG*/
      AND 1 = 0
      /*END*/

    UNION

    -- 運賃
    SELECT
       '運賃' AS DENPYOU_TYPE
      ,9 AS DENPYOU_TYPE_KBN
      ,CONVERT(VARCHAR,UNCHI.DENPYOU_NUMBER) AS DENPYOU_NUMBER
      ,UNCHI.DENPYOU_DATE
      ,'' AS TORIHIKISAKI_NAME
      ,UNCHI.UNPAN_GYOUSHA_NAME AS GYOUSHA_NAME
      ,'' AS GENBA_NAME
      ,UNCHI.SYSTEM_ID
      ,'' AS HAIKI_KBN_CD
    FROM T_UKEIRE_ENTRY TUE
    INNER JOIN T_UNCHIN_ENTRY UNCHI ON UNCHI.RENKEI_NUMBER = TUE.UKEIRE_NUMBER AND UNCHI.DENSHU_KBN_CD = 1 AND UNCHI.DELETE_FLG = 0
    WHERE TUE.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
      AND TUE.DELETE_FLG = 0
      /*IF !data.UNCHIN_FLG*/
      AND 1 = 0
      /*END*/

    UNION

    SELECT
       '運賃' AS DENPYOU_TYPE
      ,9 AS DENPYOU_TYPE_KBN
      ,CONVERT(VARCHAR,UNCHI.DENPYOU_NUMBER) AS DENPYOU_NUMBER
      ,UNCHI.DENPYOU_DATE AS DENPYOU_DATE
      ,'' AS TORIHIKISAKI_NAME
      ,UNCHI.UNPAN_GYOUSHA_NAME AS GYOUSHA_NAME
      ,'' AS GENBA_NAME
      ,UNCHI.SYSTEM_ID
      ,'' AS HAIKI_KBN_CD
    FROM T_UR_SH_ENTRY TUSE
    INNER JOIN T_UNCHIN_ENTRY UNCHI ON UNCHI.RENKEI_NUMBER = TUSE.UR_SH_NUMBER AND UNCHI.DENSHU_KBN_CD = 3 AND UNCHI.DELETE_FLG = 0
    WHERE TUSE.UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/
      AND ISNULL(TUSE.DAINOU_FLG,0) = 0
      AND TUSE.DELETE_FLG = 0
      /*IF !data.UNCHIN_FLG*/
      AND 1 = 0
      /*END*/
) AS A
ORDER BY DENPYOU_TYPE_KBN,DENPYOU_DATE,DENPYOU_NUMBER