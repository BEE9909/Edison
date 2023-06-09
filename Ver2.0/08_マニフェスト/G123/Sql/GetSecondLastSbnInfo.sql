----二次マニフェストのデータを取得（最終処分業者,最終処分場所）
SELECT
  TMD.LAST_SBN_GYOUSHA_CD AS LAST_SBN_GYOUSHA_CD
  ,TMD.LAST_SBN_GENBA_CD AS LAST_SBN_GENBA_CD
FROM T_MANIFEST_DETAIL AS TMD 
--WHERE TMD.SYSTEM_ID = /*NEXT_SYSTEM_ID*/ AND TMD.SEQ = /*SEQ*/ 
WHERE TMD.DETAIL_SYSTEM_ID = /*NEXT_SYSTEM_ID*/ AND TMD.SEQ = /*SEQ*/ 
GROUP BY
  TMD.LAST_SBN_GYOUSHA_CD
  ,TMD.LAST_SBN_GENBA_CD
ORDER BY  
  TMD.LAST_SBN_GYOUSHA_CD
  ,TMD.LAST_SBN_GENBA_CD