﻿SELECT *
  FROM dbo.M_GYOUSHA
 WHERE
       DELETE_FLG = 0
   AND UNPAN_JUTAKUSHA_KAISHA_KBN = 1
   AND GYOUSHA_CD = /*data.GYOUSHA_CD*/''
