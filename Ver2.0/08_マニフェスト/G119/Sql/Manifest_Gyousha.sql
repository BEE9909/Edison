﻿   SELECT M_GYOUSHA.GYOUSHA_CD,
          M_GYOUSHA.GYOUSHA_NAME1,
          M_GYOUSHA.GYOUSHA_NAME2,
          M_GYOUSHA.GYOUSHA_POST,
          M_GYOUSHA.GYOUSHA_TEL,
          M_GYOUSHA.GYOUSHA_ADDRESS1,
          M_GYOUSHA.GYOUSHA_ADDRESS2,
          M_TODOUFUKEN.TODOUFUKEN_NAME
     FROM M_GYOUSHA
LEFT JOIN M_TODOUFUKEN
       ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD
      AND M_TODOUFUKEN.DELETE_FLG         = 'false'
    WHERE 

    /*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
     M_GYOUSHA.DELETE_FLG = 0
    -- ELSE
     1 = 1
    /*END*/
   /*IF data.GYOUSHA_CD != null && data.GYOUSHA_CD != ''*/ AND M_GYOUSHA.GYOUSHA_CD = /*data.GYOUSHA_CD*/'0'/*END*/
   /*IF data.HAISHUTSU_NIZUMI_GYOUSHA_KBN != null && data.HAISHUTSU_NIZUMI_GYOUSHA_KBN != ''*/ AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = /*data.HAISHUTSU_NIZUMI_GYOUSHA_KBN*/0/*END*/
   /*IF data.SHOBUN_NIOROSHI_GYOUSHA_KBN != null && data.SHOBUN_NIOROSHI_GYOUSHA_KBN != ''*/ AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = /*data.SHOBUN_NIOROSHI_GYOUSHA_KBN*/0/*END*/
   /*IF data.UNPAN_JUTAKUSHA_KAISHA_KBN != null && data.UNPAN_JUTAKUSHA_KAISHA_KBN != ''*/ AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = /*data.UNPAN_JUTAKUSHA_KAISHA_KBN*/0/*END*/
   
   /*IF data.TSUMIKAEHOKAN_KBN != null && data.TSUMIKAEHOKAN_KBN != ''*/
    AND EXISTS ( SELECT * FROM M_GENBA 
       WHERE M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD 
      /*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
      AND M_GENBA.DELETE_FLG = 0
      /*END*/
      AND  M_GENBA.TSUMIKAEHOKAN_KBN = /*data.TSUMIKAEHOKAN_KBN*/0
      ) 
   /*END*/
