﻿SELECT
       HOUKOKUSHO_BUNRUI_CD,
       HOUKOKU_BUNRUI_NAME
  FROM M_CHIIKIBETSU_BUNRUI
 WHERE CHIIKI_CD = /*data.CHIIKI_CD*/0
   AND HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*/0
   AND DELETE_FLG = 0