﻿SELECT TORIKOMI.TORIKOMI_NUMBER
      ,TORIKOMI.ROW_NUMBER
      ,TORIKOMI.BANK_RENKEI_CD
      ,TORIKOMI.BANK_SHITEN_RENKEI_CD
      ,TORIKOMI.KOUZA_NO
      ,TORIKOMI.YONYUU_DATE
      ,TORIKOMI.KINGAKU
      ,TORIKOMI.FURIKOMI_JINMEI
      ,TORIKOMI.TEKIYOU_NAIYOU
	  ,TORIKOMI.TIME_STAMP
  FROM T_NYUUKIN_DATA_TORIKOMI TORIKOMI
  WHERE TORIKOMI.DELETE_FLG =0 
  /*IF !data.YonyuuDateFrom.IsNull */ AND TORIKOMI.YONYUU_DATE >= /*data.YonyuuDateFrom*/'2016/04/01' /*END*/
  /*IF !data.YonyuuDateTo.IsNull */ AND TORIKOMI.YONYUU_DATE <= /*data.YonyuuDateTo*/'2016/04/01' /*END*/