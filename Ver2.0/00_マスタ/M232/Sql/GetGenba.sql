﻿SELECT M_GENBA.GYOUSHA_CD
     , M_GENBA.GENBA_CD
     , M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN
     , M_GENBA.TSUMIKAEHOKAN_KBN
     , M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN
     , M_GENBA.SAISHUU_SHOBUNJOU_KBN
     , M_GENBA.TSUMIKAEHOKAN_KBN
     , M_GENBA.GENBA_NAME_RYAKU
     , M_GENBA.GENBA_POST
     , M_GENBA.GENBA_TEL
     , M_GENBA.GENBA_ADDRESS1
     , M_GENBA.GENBA_ADDRESS2
     , M_GENBA.SHOBUNSAKI_NO
  FROM M_GYOUSHA
 INNER JOIN M_GENBA ON M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD
 WHERE M_GYOUSHA.DELETE_FLG = 'false'
/*IF data.GYOUSHA_CD != null && data.GYOUSHA_CD != ''*/ AND M_GYOUSHA.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
   AND M_GENBA.DELETE_FLG = 'false'
/*IF data.GENBA_CD != null && data.GENBA_CD != ''*/ AND M_GENBA.GENBA_CD = /*data.GENBA_CD*//*END*/
--20140117 oonaka add 現場フォーカスアウトチェック修正 start ---
/*IF nioroshiFlg*/
  AND(
    M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = 'true' 
	OR M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = 'true' 
  )
  AND(
    M_GENBA.TSUMIKAEHOKAN_KBN = 'true' 
	OR M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN = 'true'   
	OR M_GENBA.SAISHUU_SHOBUNJOU_KBN = 'true' 
  )
/*END*/
--20140117 oonaka add 現場フォーカスアウトチェック修正 end ---