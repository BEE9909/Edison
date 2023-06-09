SELECT DISTINCT
KAGAMI.TORIHIKISAKI_CD --請求先CD
,TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU --請求先名
,KAGAMI.GYOUSHA_CD --業者CD
,GYOUSHA.GYOUSHA_NAME_RYAKU --業者名略称
,KAGAMI.GENBA_CD --現場CD
,GENBA.GENBA_NAME_RYAKU --現場名略称
,DENPYOU.SHIMEBI --締日
,KAGAMI.KONKAI_URIAGE_GAKU --今回取引額(税抜）
--　KONKAI_SEI_UTIZEI_GAKU（今回請内税額）＋KONKAI_SEI_SOTOZEI_GAKU（今回請外税額）
--　＋KONKAI_DEN_UTIZEI_GAKU（今回伝内税額）＋KONKAI_DEN_SOTOZEI_GAKU（今回伝外税額）
--  ＋KONKAI_MEI_UTIZEI_GAKU（今回明内税額）＋KONKAI_MEI_SOTOZEI_GAKU（今回明外税額）
,(ISNULL(KAGAMI.KONKAI_SEI_UTIZEI_GAKU,0) 
	+ ISNULL(KAGAMI.KONKAI_SEI_SOTOZEI_GAKU,0)  
	+ ISNULL(KAGAMI.KONKAI_DEN_UTIZEI_GAKU,0)  
	+ ISNULL(KAGAMI.KONKAI_DEN_SOTOZEI_GAKU,0)  
	+ ISNULL(KAGAMI.KONKAI_MEI_UTIZEI_GAKU,0) 
	+ ISNULL(KAGAMI.KONKAI_MEI_SOTOZEI_GAKU,0))AS SHOUHIZEI --消費税
		
 --KONKAI_URIAGE_GAKU（今回売上額）＋SHOUHIZEI (消費税)
,KAGAMI.KONKAI_URIAGE_GAKU  
+(ISNULL(KAGAMI.KONKAI_SEI_UTIZEI_GAKU,0) 
	+ ISNULL(KAGAMI.KONKAI_SEI_SOTOZEI_GAKU,0)  
	+ ISNULL(KAGAMI.KONKAI_DEN_UTIZEI_GAKU,0)  
	+ ISNULL(KAGAMI.KONKAI_DEN_SOTOZEI_GAKU,0)  
	+ ISNULL(KAGAMI.KONKAI_MEI_UTIZEI_GAKU,0) 
	+ ISNULL(KAGAMI.KONKAI_MEI_SOTOZEI_GAKU,0)) AS KONKAI_SEIKYU_GAKU --今回御請求額
,DENPYOU.NYUUKIN_YOTEI_BI --入金予定日
,DENPYOU.SEIKYUU_DATE --請求年月日
,TORIHIKISAKI.TORIHIKISAKI_FURIGANA --取引先フリガナ
,DENPYOU.SEIKYUU_NUMBER --伝票番号
FROM 
T_SEIKYUU_DENPYOU AS DENPYOU
INNER JOIN T_SEIKYUU_DENPYOU_KAGAMI AS KAGAMI ON DENPYOU.SEIKYUU_NUMBER=KAGAMI.SEIKYUU_NUMBER
INNER JOIN T_SEIKYUU_DETAIL AS DE 
        ON DE.SEIKYUU_NUMBER = KAGAMI.SEIKYUU_NUMBER
       AND DE.KAGAMI_NUMBER = KAGAMI.KAGAMI_NUMBER 
	   AND (DE.DENPYOU_SHURUI_CD <> 10 OR DE.DENPYOU_SHURUI_CD IS NULL)
INNER JOIN M_TORIHIKISAKI AS TORIHIKISAKI ON TORIHIKISAKI.TORIHIKISAKI_CD = KAGAMI.TORIHIKISAKI_CD 
LEFT JOIN M_GYOUSHA AS GYOUSHA ON GYOUSHA.GYOUSHA_CD = KAGAMI.GYOUSHA_CD 
LEFT JOIN M_GENBA AS GENBA ON GENBA.GYOUSHA_CD = KAGAMI.GYOUSHA_CD AND GENBA.GENBA_CD=KAGAMI.GENBA_CD
WHERE DENPYOU.DELETE_FLG = 0
/*IF !data.HIDUKE_FROM.IsNull*/ AND DENPYOU.SEIKYUU_DATE >= /*data.HIDUKE_FROM*//*END*/
/*IF !data.HIDUKE_TO.IsNull*/ AND DENPYOU.SEIKYUU_DATE <= /*data.HIDUKE_TO*//*END*/
/*IF data.TORIHIKISAKI_CD_FROM != ''*/ AND DENPYOU.TORIHIKISAKI_CD >= /*data.TORIHIKISAKI_CD_FROM*//*END*/
/*IF data.TORIHIKISAKI_CD_TO != ''*/ AND DENPYOU.TORIHIKISAKI_CD <= /*data.TORIHIKISAKI_CD_TO*//*END*/
/*IF !data.KYOTEN_CD.IsNull*/ AND (DENPYOU.KYOTEN_CD = /*data.KYOTEN_CD*/ OR /*data.KYOTEN_CD*/ = 99) /*END*/
/*IF !data.TORIHIKISAKI_SORT.IsNull && data.TORIHIKISAKI_SORT == '1'*/ ORDER BY TORIHIKISAKI.TORIHIKISAKI_FURIGANA, KAGAMI.TORIHIKISAKI_CD, KAGAMI.GYOUSHA_CD, KAGAMI.GENBA_CD, DENPYOU.SEIKYUU_DATE, DENPYOU.SEIKYUU_NUMBER ASC /*END*/
/*IF !data.TORIHIKISAKI_SORT.IsNull && data.TORIHIKISAKI_SORT == '2'*/ ORDER BY KAGAMI.TORIHIKISAKI_CD ASC /*END*/

