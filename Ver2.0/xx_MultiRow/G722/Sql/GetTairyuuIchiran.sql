	  SELECT 
				  ENTRY.DENPYOU_DATE AS MOD_DENPYOU_DATE 
				, ENTRY.SHUKKA_NUMBER AS MOD_DENPYOU_NUMBER
				, MSHA.SHASHU_NAME_RYAKU AS MOD_SHASHU_NAME
				, MSHAR.SHARYOU_NAME AS MOD_SHARYOU_NAME
				, ENTRY.GYOUSHA_NAME AS MOD_GYOUSHA_NAME
				, ENTRY.TAIRYUU_BIKOU AS MOD_TAIRYUU_BIKOU
       FROM T_SHUKKA_ENTRY AS ENTRY 
  LEFT JOIN M_SHASHU MSHA ON ENTRY.SHASHU_CD = MSHA.SHASHU_CD AND MSHA.DELETE_FLG = 0 
  LEFT JOIN M_SHARYOU MSHAR ON ENTRY.SHARYOU_CD = MSHAR.SHARYOU_CD AND ENTRY.UNPAN_GYOUSHA_CD =  MSHAR.GYOUSHA_CD AND MSHAR.DELETE_FLG = 0
      WHERE ENTRY.TAIRYUU_KBN = 1 AND ENTRY.DELETE_FLG = 0 
	 /*IF !kyotenCd.IsNull*/
    AND ENTRY.KYOTEN_CD = /*kyotenCd*/
    /*END*/
	/*IF sharyou != null && sharyou != ''*/AND ENTRY.SHARYOU_CD = /*sharyou*//*END*/
	/*IF unpanGyousha != null && unpanGyousha != ''*/AND ENTRY.UNPAN_GYOUSHA_CD = /*unpanGyousha*//*END*/
   ORDER BY ENTRY.CREATE_DATE ASC