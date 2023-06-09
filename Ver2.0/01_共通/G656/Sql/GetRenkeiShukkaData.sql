﻿-- 検索対象伝票で3.出荷が選択された場合
SELECT 
   DENPYOU_TYPE1 AS '伝票'
  ,DENPYOU_NUMBER1 AS '伝票番号'
  ,DENPYOU_DATE1 AS '伝票日付'
  ,TORIHIKISAKI_NAME1 AS '取引先'
  ,GYOUSHA_NAME1 AS '業者'
  ,GENBA_NAME1 AS '現場'
  ,DENPYOU_TYPE2 AS '伝票'
  ,DENPYOU_NUMBER2 AS '伝票番号'
  ,DENPYOU_DATE2 AS '伝票日付'
  ,TORIHIKISAKI_NAME2 AS '取引先'
  ,GYOUSHA_NAME2 AS '業者'
  ,GENBA_NAME2 AS '現場'
  ,DENPYOU_TYPE_KBN AS HIDDEN_DENPYOU_TYPE
  ,DENPYOU_TYPE1 AS HIDDEN_DENPYOU_KBN
  ,DENPYOU_NUMBER1  AS HIDDEN_DENPYOU_NO
  ,DENPYOU_DATE1 AS HIDDEN_DENPYOU_DATE
  ,TORIHIKISAKI_NAME1 AS HIDDEN_TORIHIKISAKI_NAME
  ,GYOUSHA_NAME1 AS HIDDEN_GYOUSHA_NAME
  ,GENBA_NAME1 AS HIDDEN_GENBA_NAME
  ,SYSTEM_ID1 AS HIDDEN_SYSTEM_ID
  ,SYSTEM_ID2 AS HIDDEN_SYSTEM_ID_R
  ,HAIKI_KBN_CD1 AS HIDDEN_HAIKI_KBN_CD
  ,HAIKI_KBN_CD2 AS HIDDEN_HAIKI_KBN_CD_R
FROM(
SELECT 
       '出荷' AS DENPYOU_TYPE1
      ,SHUKKA.SHUKKA_NUMBER  AS DENPYOU_NUMBER1
      ,SHUKKA.DENPYOU_DATE AS DENPYOU_DATE1
      ,SHUKKA.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME1
      ,SHUKKA.GYOUSHA_NAME AS GYOUSHA_NAME1
      ,SHUKKA.GENBA_NAME AS GENBA_NAME1
	  ,6 AS DENPYOU_TYPE_KBN
      ,'マニフェスト' AS DENPYOU_TYPE2
      ,MANI.MANIFEST_ID AS DENPYOU_NUMBER2
      ,MANI.KOUFU_DATE AS DENPYOU_DATE2
      ,MT.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME2
      ,MANI.HST_GYOUSHA_NAME AS GYOUSHA_NAME2
      ,MANI.HST_GENBA_NAME AS GENBA_NAME2
      ,SHUKKA.SYSTEM_ID AS SYSTEM_ID1
	  ,'' AS HAIKI_KBN_CD1
      ,MANI.SYSTEM_ID AS SYSTEM_ID2
	  ,MANI.HAIKI_KBN_CD AS HAIKI_KBN_CD2
 FROM 
     T_SHUKKA_ENTRY SHUKKA
LEFT JOIN T_MANIFEST_ENTRY MANI
       ON MANI.RENKEI_SYSTEM_ID = SHUKKA.SYSTEM_ID
      AND MANI.DELETE_FLG = 0
      AND MANI.RENKEI_DENSHU_KBN_CD = 2
LEFT JOIN M_TORIHIKISAKI MT
       ON MT.TORIHIKISAKI_CD = MANI.TORIHIKISAKI_CD
WHERE 
SHUKKA.DELETE_FLG = 0
AND (
     (/*data.RENKEI_KBN*/ = 1) 
  OR (/*data.RENKEI_KBN*/ = 2 AND MANI.SYSTEM_ID IS NOT NULL) 
  OR (/*data.RENKEI_KBN*/ = 3 AND MANI.SYSTEM_ID IS NULL)
  )
/*IF !data.KYOTEN_CD.IsNull*/
AND SHUKKA.KYOTEN_CD = /*data.KYOTEN_CD*/
/*END*/
/*IF !data.DENPYOU_DATE_FROM.IsNull*/
AND CONVERT(varchar(10), SHUKKA.DENPYOU_DATE,120) >= /*data.DENPYOU_DATE_FROM*/
/*END*/
/*IF !data.DENPYOU_DATE_TO.IsNull*/
AND CONVERT(varchar(10), SHUKKA.DENPYOU_DATE,120) <= /*data.DENPYOU_DATE_TO*/
/*END*/
/*IF data.DENPYOU_NO != null*/
AND SHUKKA.SHUKKA_NUMBER = /*data.DENPYOU_NO*/
/*END*/
/*IF data.TORIHIKISAKI_CD != null*/
AND SHUKKA.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
/*END*/
/*IF data.GYOUSHA_CD != null*/
AND SHUKKA.GYOUSHA_CD = /*data.GYOUSHA_CD*/
/*END*/
/*IF data.GYOUSHA_CD != null*/
AND SHUKKA.GENBA_CD = /*data.GYOUSHA_CD*/
/*END*/

UNION

SELECT 
       '出荷' AS DENPYOU_TYPE1
      ,SHUKKA.SHUKKA_NUMBER  AS DENPYOU_NUMBER1
      ,SHUKKA.DENPYOU_DATE AS DENPYOU_DATE1
      ,SHUKKA.TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME1
      ,SHUKKA.GYOUSHA_NAME AS GYOUSHA_NAME1
      ,SHUKKA.GENBA_NAME AS GENBA_NAME1
	  ,6 AS DENPYOU_TYPE_KBN
      ,'運賃' AS DENPYOU_TYPE2
      ,CONVERT(nvarchar,UNCHIN.DENPYOU_NUMBER) AS DENPYOU_NUMBER2
      ,UNCHIN.DENPYOU_DATE AS DENPYOU_DATE2
      ,'' AS TORIHIKISAKI_NAME2
      ,UNCHIN.UNPAN_GYOUSHA_NAME AS GYOUSHA_NAME2
      ,'' AS GENBA_NAME2
      ,SHUKKA.SYSTEM_ID AS SYSTEM_ID1
	  ,'' AS HAIKI_KBN_CD1
      ,UNCHIN.SYSTEM_ID AS SYSTEM_ID2
	  ,'' AS HAIKI_KBN_CD2
 FROM 
     T_SHUKKA_ENTRY SHUKKA
LEFT JOIN T_UNCHIN_ENTRY UNCHIN
       ON UNCHIN.RENKEI_NUMBER = SHUKKA.SHUKKA_NUMBER
      AND UNCHIN.DELETE_FLG = 0
      AND UNCHIN.DENSHU_KBN_CD = 2
WHERE 
SHUKKA.DELETE_FLG = 0
AND (
     (/*data.RENKEI_KBN*/ = 1) 
  OR (/*data.RENKEI_KBN*/ = 2 AND UNCHIN.SYSTEM_ID IS NOT NULL) 
  OR (/*data.RENKEI_KBN*/ = 3 AND UNCHIN.SYSTEM_ID IS NULL)
  )
/*IF !data.KYOTEN_CD.IsNull*/
AND SHUKKA.KYOTEN_CD = /*data.KYOTEN_CD*/
/*END*/
/*IF !data.DENPYOU_DATE_FROM.IsNull*/
AND CONVERT(varchar(10), SHUKKA.DENPYOU_DATE,120) >= /*data.DENPYOU_DATE_FROM*/
/*END*/
/*IF !data.DENPYOU_DATE_TO.IsNull*/
AND CONVERT(varchar(10), SHUKKA.DENPYOU_DATE,120) <= /*data.DENPYOU_DATE_TO*/
/*END*/
/*IF data.DENPYOU_NO != null*/
AND SHUKKA.SHUKKA_NUMBER = /*data.DENPYOU_NO*/
/*END*/
/*IF data.TORIHIKISAKI_CD != null*/
AND SHUKKA.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/
/*END*/
/*IF data.GYOUSHA_CD != null*/
AND SHUKKA.GYOUSHA_CD = /*data.GYOUSHA_CD*/
/*END*/
/*IF data.GYOUSHA_CD != null*/
AND SHUKKA.GENBA_CD = /*data.GYOUSHA_CD*/
/*END*/
) AS A
ORDER BY DENPYOU_TYPE_KBN,DENPYOU_DATE1,DENPYOU_NUMBER1