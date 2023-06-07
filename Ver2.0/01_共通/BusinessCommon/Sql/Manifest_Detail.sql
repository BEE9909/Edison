﻿SELECT TMD.SYSTEM_ID
     , TMD.SEQ
     , TMD.DETAIL_SYSTEM_ID
     , CAST(TMD.TIME_STAMP AS int) AS TIME_STAMP

	 --廃棄物種類CD
     , TMD.HAIKI_SHURUI_CD

	 --廃棄物種類名
     , MHS.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI_NAME
     --, MHS.HAIKI_SHURUI_NAME

	 --廃棄物の名称CD
     , TMD.HAIKI_NAME_CD
     
	 --廃棄物の名称名
     , MHN.HAIKI_NAME_RYAKU AS HAIKI_NAME
     --, MHN.HAIKI_NAME

	 --荷姿CD
	 , TMD.NISUGATA_CD

	 --荷姿名
     , MN.NISUGATA_NAME

	 --割合
     , TMD.WARIAI

	 --数量
     , TMD.HAIKI_SUU

	 --単位CD
     , TMD.HAIKI_UNIT_CD

	 --単位名
     , MU.UNIT_NAME_RYAKU AS UNIT_NAME

	 --換算後数量
     , TMD.KANSAN_SUU

	 --減容後数量
     , TMD.GENNYOU_SUU

	 --処分方法CD
     , TMD.SBN_HOUHOU_CD

	 --処分方法名
     , MSH.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME
     --, MSH.SHOBUN_HOUHOU_NAME

	 --処分終了日
     , TMD.SBN_END_DATE

	 --最終処分終了日
     --kayo No.4811, CASE WHEN TMR.MANIFEST_ID IS NULL THEN TMD.LAST_SBN_END_DATE ELSE TMR.LAST_SBN_END_DATE END AS LAST_SBN_END_DATE
     , TMD.LAST_SBN_END_DATE AS LAST_SBN_END_DATE

	 --最終処分業者CD
     --kayo No.4811, CASE WHEN TMR.MANIFEST_ID IS NULL THEN TMD.LAST_SBN_GYOUSHA_CD ELSE TMR.LAST_SBN_GYOUSHA_CD END AS LAST_SBN_GYOUSHA_CD
     , TMD.LAST_SBN_GYOUSHA_CD AS LAST_SBN_GYOUSHA_CD

	 --最終処分業者名
     --, CASE WHEN TMR.MANIFEST_ID IS NULL THEN MG.GYOUSHA_NAME_RYAKU ELSE TMR.GYOUSHA_NAME_RYAKU END AS GYOUSHA_NAME_RYAKU
     --kayo No.4811, CASE WHEN TMR.MANIFEST_ID IS NULL THEN MG.GYOUSHA_NAME1 + MG.GYOUSHA_NAME2 ELSE TMR.GYOUSHA_NAME END AS GYOUSHA_NAME
     , MG.GYOUSHA_NAME1 + MG.GYOUSHA_NAME2 AS GYOUSHA_NAME

	 --最終処分場所CD
     --kayo No.4811 , CASE WHEN TMR.MANIFEST_ID IS NULL THEN TMD.LAST_SBN_GENBA_CD ELSE TMR.LAST_SBN_GENBA_CD END AS LAST_SBN_GENBA_CD
     , TMD.LAST_SBN_GENBA_CD AS LAST_SBN_GENBA_CD

	 --最終処分場所名
     --, CASE WHEN TMR.MANIFEST_ID IS NULL THEN MGA.GENBA_NAME_RYAKU ELSE TMR.GENBA_NAME_RYAKU END AS GENBA_NAME_RYAKU
     --kayo No.4811, CASE WHEN TMR.MANIFEST_ID IS NULL THEN MGA.GENBA_NAME1 + MGA.GENBA_NAME2 ELSE TMR.GENBA_NAME END AS GENBA_NAME
     , MGA.GENBA_NAME1 + MGA.GENBA_NAME2 AS GENBA_NAME

	 --二次マニ交付番号
	 , TMR.MANIFEST_ID AS NEXT_SYSTEM_ID
	 , TMR.NEXT_HAIKI_KBN_CD AS NEXT_HAIKI_KBN_CD
  FROM T_MANIFEST_ENTRY AS TME WITH(NOLOCK)
 INNER JOIN T_MANIFEST_DETAIL AS TMD WITH(NOLOCK)
    ON TME.SYSTEM_ID = TMD.SYSTEM_ID 
   AND TME.SEQ = TMD.SEQ
  LEFT OUTER JOIN M_HAIKI_SHURUI AS MHS WITH(NOLOCK)
    ON TME.HAIKI_KBN_CD = MHS.HAIKI_KBN_CD 
   AND TMD.HAIKI_SHURUI_CD = MHS.HAIKI_SHURUI_CD
  LEFT OUTER JOIN M_HAIKI_NAME AS MHN WITH(NOLOCK)
    ON TMD.HAIKI_NAME_CD = MHN.HAIKI_NAME_CD
  LEFT OUTER JOIN M_NISUGATA AS MN WITH(NOLOCK)
    ON TMD.NISUGATA_CD = MN.NISUGATA_CD
  LEFT OUTER JOIN M_UNIT AS MU WITH(NOLOCK)
    ON TMD.HAIKI_UNIT_CD = MU.UNIT_CD
  LEFT OUTER JOIN M_SHOBUN_HOUHOU AS MSH WITH(NOLOCK)
    ON TMD.SBN_HOUHOU_CD = MSH.SHOBUN_HOUHOU_CD
  LEFT OUTER JOIN M_GYOUSHA AS MG WITH(NOLOCK)
    ON TMD.LAST_SBN_GYOUSHA_CD = MG.GYOUSHA_CD
  LEFT OUTER JOIN M_GENBA AS MGA WITH(NOLOCK) 
    ON TMD.LAST_SBN_GYOUSHA_CD = MGA.GYOUSHA_CD 
   AND TMD.LAST_SBN_GENBA_CD = MGA.GENBA_CD

--紐付2次 START
  LEFT OUTER JOIN (
    SELECT COL_TMR.NEXT_SYSTEM_ID
         , COL_TMR.SEQ
         , COL_TMR.REC_SEQ
         , COL_TMR.NEXT_HAIKI_KBN_CD
         , COL_TMR.FIRST_SYSTEM_ID
         , COL_TMR.FIRST_HAIKI_KBN_CD
         , COL_TMR.DELETE_FLG
         , COL_TMR.TIME_STAMP
/*		 
		 --最終処分終了日
		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.LAST_SBN_END_DATE
		        WHEN 2 THEN COL_TME.LAST_SBN_END_DATE
		        WHEN 3 THEN COL_TME.LAST_SBN_END_DATE
		        WHEN 4 THEN COL_DR18E.LAST_SBN_END_DATE
                ELSE ''
           END AS LAST_SBN_END_DATE
		 
		 --最終処分業者CD
		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.LAST_SBN_GYOUSHA_CD
		        WHEN 2 THEN COL_TME.LAST_SBN_GYOUSHA_CD
		        WHEN 3 THEN COL_TME.LAST_SBN_GYOUSHA_CD
		        WHEN 4 THEN COL_DR18E.LAST_SBN_GYOUSHA_CD
                ELSE ''
           END AS LAST_SBN_GYOUSHA_CD
*/
		 --最終処分業者名
/*		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.GYOUSHA_NAME_RYAKU
		        WHEN 2 THEN COL_TME.GYOUSHA_NAME_RYAKU
		        WHEN 3 THEN COL_TME.GYOUSHA_NAME_RYAKU
		        WHEN 4 THEN COL_DR18E.GYOUSHA_NAME_RYAKU
                ELSE ''
           END AS GYOUSHA_NAME_RYAKU
*/
/*		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.GYOUSHA_NAME
		        WHEN 2 THEN COL_TME.GYOUSHA_NAME
		        WHEN 3 THEN COL_TME.GYOUSHA_NAME
		        WHEN 4 THEN COL_DR18E.GYOUSHA_NAME
                ELSE ''
           END AS GYOUSHA_NAME


		 --最終処分場所CD
		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.LAST_SBN_GENBA_CD
		        WHEN 2 THEN COL_TME.LAST_SBN_GENBA_CD
		        WHEN 3 THEN COL_TME.LAST_SBN_GENBA_CD
		        WHEN 4 THEN COL_DR18E.LAST_SBN_GENBA_CD
                ELSE ''
           END AS LAST_SBN_GENBA_CD
*/
		 --最終処分場所名
/*		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.GENBA_NAME_RYAKU
		        WHEN 2 THEN COL_TME.GENBA_NAME_RYAKU
		        WHEN 3 THEN COL_TME.GENBA_NAME_RYAKU
		        WHEN 4 THEN COL_DR18E.GENBA_NAME_RYAKU
                ELSE ''
           END AS GENBA_NAME_RYAKU
*/
/*		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.GENBA_NAME
		        WHEN 2 THEN COL_TME.GENBA_NAME
		        WHEN 3 THEN COL_TME.GENBA_NAME
		        WHEN 4 THEN COL_DR18E.GENBA_NAME
                ELSE ''
           END AS GENBA_NAME
*/
		 --二次マニ交付番号
		 , CASE COL_TMR.NEXT_HAIKI_KBN_CD 
		        WHEN 1 THEN COL_TME.MANIFEST_ID
		        WHEN 2 THEN COL_TME.MANIFEST_ID
		        WHEN 3 THEN COL_TME.MANIFEST_ID
		        WHEN 4 THEN COL_DR18E.MANIFEST_ID
                ELSE ''
           END AS MANIFEST_ID

      FROM T_MANIFEST_RELATION AS COL_TMR WITH(NOLOCK)
     INNER JOIN (
        SELECT NEXT_SYSTEM_ID
    	     , MAX(SEQ) AS SEQ
          FROM T_MANIFEST_RELATION WITH(NOLOCK)  
         WHERE DELETE_FLG = 'false' 
--           AND FIRST_HAIKI_KBN_CD = 2
    	 GROUP BY NEXT_SYSTEM_ID
           ) AS MAX_TMR
        ON COL_TMR.NEXT_SYSTEM_ID = MAX_TMR.NEXT_SYSTEM_ID 
       AND COL_TMR.SEQ = MAX_TMR.SEQ 

	 --紙マニ START
	 LEFT OUTER JOIN (
		SELECT DISTINCT COL2_TME.SYSTEM_ID 
		     , COL2_TME.SEQ 
			 , COL2_TMD.DETAIL_SYSTEM_ID 
--			 , COL2_TMD.LAST_SBN_END_DATE
--			 , COL2_TME.LAST_SBN_GYOUSHA_CD 
--			 , MG2.GYOUSHA_NAME_RYAKU
--			 , MG2.GYOUSHA_NAME1 + MG2.GYOUSHA_NAME2 AS GYOUSHA_NAME
--			 , COL2_TME.LAST_SBN_GENBA_CD
--			 , MGA2.GENBA_NAME_RYAKU
--			 , MGA2.GENBA_NAME1 + MGA2.GENBA_NAME2 AS GENBA_NAME 
		     , COL2_TME.MANIFEST_ID 
		  FROM T_MANIFEST_ENTRY AS COL2_TME WITH(NOLOCK)
		 INNER JOIN (
			 SELECT SYSTEM_ID
				  , MAX(SEQ) AS SEQ
			   FROM T_MANIFEST_ENTRY AS COL_TME WITH(NOLOCK)
			  WHERE DELETE_FLG = 'false'
			  GROUP BY SYSTEM_ID
			   ) AS MAX2_TME
			ON COL2_TME.SYSTEM_ID = MAX2_TME.SYSTEM_ID 
		   AND COL2_TME.SEQ = MAX2_TME.SEQ 
          LEFT OUTER JOIN (
			SELECT SYSTEM_ID
				 , DETAIL_SYSTEM_ID 
			     , SEQ
			     , MAX(LAST_SBN_END_DATE) AS LAST_SBN_END_DATE		
			  FROM T_MANIFEST_DETAIL WITH(NOLOCK) 
			 GROUP BY SYSTEM_ID
			     , SEQ
				 , DETAIL_SYSTEM_ID 
		  ) AS COL2_TMD 
		    ON COL2_TME.SYSTEM_ID = COL2_TMD.SYSTEM_ID
		   AND COL2_TME.SEQ = COL2_TMD.SEQ
		  LEFT OUTER JOIN M_GYOUSHA AS MG2 WITH(NOLOCK)
			ON COL2_TME.LAST_SBN_GYOUSHA_CD = MG2.GYOUSHA_CD
		   AND MG2.DELETE_FLG = 'false'
		  LEFT OUTER JOIN M_GENBA AS MGA2 WITH(NOLOCK) 
			ON COL2_TME.LAST_SBN_GYOUSHA_CD = MGA2.GYOUSHA_CD 
		   AND COL2_TME.LAST_SBN_GENBA_CD = MGA2.GENBA_CD
		   AND MGA2.DELETE_FLG = 'false'
		 WHERE COL2_TME.DELETE_FLG = 'false'
          )COL_TME
       ON COL_TMR.NEXT_SYSTEM_ID = COL_TME.DETAIL_SYSTEM_ID 
	 --紙マニ END

	 --電子マニ START
     LEFT OUTER JOIN (
		SELECT DISTINCT COL_DR18E.SYSTEM_ID 
			 , COL_DR18E.SEQ 
--			 , DR18.LAST_SBN_END_DATE
--			 , DR13E.LAST_SBN_GYOUSHA_CD AS LAST_SBN_GYOUSHA_CD
--			 , MG3.GYOUSHA_NAME_RYAKU
--			 , MG3.GYOUSHA_NAME1 + MG3.GYOUSHA_NAME1 AS GYOUSHA_NAME
--			 , DR13E.LAST_SBN_GENBA_CD AS LAST_SBN_GENBA_CD
--			 , MGA3.GENBA_NAME_RYAKU
--			 , MGA3.GENBA_NAME1 + MGA3.GENBA_NAME2 AS GENBA_NAME
			 , COL_DR18E.MANIFEST_ID 
		  FROM DT_R18_EX AS COL_DR18E WITH(NOLOCK)
		 INNER JOIN (
			SELECT SYSTEM_ID
			     , MAX(SEQ) AS SEQ
			  FROM DT_R18_EX WITH(NOLOCK)
			 WHERE DELETE_FLG = 'false'
			 GROUP BY SYSTEM_ID
		 ) MAX_DR18E
		    ON COL_DR18E.SYSTEM_ID = MAX_DR18E.SYSTEM_ID
		   AND COL_DR18E.SEQ = MAX_DR18E.SEQ
		 INNER JOIN DT_MF_TOC AS DMT WITH(NOLOCK)
		    ON COL_DR18E.KANRI_ID = DMT.KANRI_ID 
		   AND COL_DR18E.MANIFEST_ID = DMT.MANIFEST_ID 
--		   AND DMT.KIND = 4
		 INNER JOIN DT_R18 AS DR18 WITH(NOLOCK)
		    ON DMT.KANRI_ID = DR18.KANRI_ID 
		   AND DMT.LATEST_SEQ = DR18.SEQ 
		  LEFT OUTER JOIN DT_R13_EX AS DR13E WITH(NOLOCK)
		    ON COL_DR18E.SYSTEM_ID = DR13E.SYSTEM_ID
		   AND COL_DR18E.SEQ = DR13E.SEQ
		  LEFT OUTER JOIN M_GYOUSHA AS MG3 WITH(NOLOCK)
			ON DR13E.LAST_SBN_GYOUSHA_CD = MG3.GYOUSHA_CD
		   AND MG3.DELETE_FLG = 'false'
		  LEFT OUTER JOIN M_GENBA AS MGA3 WITH(NOLOCK) 
			ON DR13E.LAST_SBN_GYOUSHA_CD = MGA3.GYOUSHA_CD 
		   AND DR13E.LAST_SBN_GENBA_CD = MGA3.GENBA_CD
		   AND MGA3.DELETE_FLG = 'false'
		 WHERE COL_DR18E.DELETE_FLG = 'false'
        )COL_DR18E
     ON COL_TMR.NEXT_SYSTEM_ID = COL_DR18E.SYSTEM_ID 
     WHERE COL_TMR.DELETE_FLG = 'false' 
--       AND COL_TMR.FIRST_HAIKI_KBN_CD = 2
	 --電子マニ END

    )TMR
    ON TMD.DETAIL_SYSTEM_ID = TMR.FIRST_SYSTEM_ID 
    AND TMR.FIRST_HAIKI_KBN_CD <> 4
--紐付2次 END

 WHERE TME.SYSTEM_ID = /*data.SYSTEM_ID*/ 
   /*IF data.SEQ != null && data.SEQ != ''*/ AND TME.SEQ = /*data.SEQ*//*END*/
   /*IF data.SEQ == null || data.SEQ == ''*/ AND TME.DELETE_FLG = 'false'/*END*/
