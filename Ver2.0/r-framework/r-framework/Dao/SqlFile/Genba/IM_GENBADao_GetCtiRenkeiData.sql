SELECT 
     M_GENBA.TORIHIKISAKI_CD
	,M_GENBA.GYOUSHA_CD
	,M_GENBA.GENBA_CD AS GENBA_CD
	,M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU
FROM M_GENBA
WHERE 
  /*IF selectType == 3*/
    M_GENBA.GYOUSHA_CD = /*gyousha*/
    AND M_GENBA.GENBA_CD = /*genba*/
  -- ELSE
  ISNULL(REPLACE(REPLACE(REPLACE(M_GENBA.GENBA_TEL, '-', ''), ')', ''), '(', ''), '') LIKE '%' + /*tel*/ + '%' OR ISNULL(REPLACE(REPLACE(REPLACE(M_GENBA.GENBA_KEITAI_TEL, '-', ''), ')', ''), '(', ''), '') LIKE '%' + /*tel*/ + '%'
  /*END*/
ORDER BY  GENBA_CD  ASC
