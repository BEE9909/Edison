﻿SELECT MKH.HAIKI_SHURUI_CD
     , MHS.HAIKI_SHURUI_NAME
     , MKH.HAIKI_HIRITSU
  FROM M_KONGOU_HAIKIBUTSU MKH
  LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI MHS 
    ON (MHS.HAIKI_SHURUI_CD + '000') = MKH.HAIKI_SHURUI_CD
   AND MHS.DELETE_FLG = 'false'
 WHERE MKH.DELETE_FLG = 'false'
   AND MKH.HAIKI_KBN_CD = 4
   AND MKH.KONGOU_SHURUI_CD = /*data.KONGOU_SHURUI_CD*/
