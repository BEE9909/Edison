﻿
SELECT ISNULL(M_GENBA.GENBA_CD,'') AS GENBA_CD
     , M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU

  FROM M_GENBA 

 WHERE M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = 'true'
/*IF data.GYOUSHA_CD != null && data.GYOUSHA_CD != ''*/ AND M_GENBA.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.GENBA_CD != null && data.GENBA_CD != ''*/ AND M_GENBA.GENBA_CD = /*data.GENBA_CD*//*END*/