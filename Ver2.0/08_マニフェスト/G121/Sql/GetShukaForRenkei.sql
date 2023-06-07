﻿   SELECT SHUKKA_E.DENPYOU_DATE,
          SHUKKA_E.TORIHIKISAKI_CD,
          SHUKKA_E.TORIHIKISAKI_NAME,
          SHUKKA_E.GYOUSHA_CD,
          SHUKKA_E.GYOUSHA_NAME,
          SHUKKA_E.GENBA_CD,
          SHUKKA_E.GENBA_NAME,
          SHUKKA_E.UNPAN_GYOUSHA_CD,
          SHUKKA_E.UNPAN_GYOUSHA_NAME,
          SHUKKA_E.NIZUMI_GYOUSHA_CD,
          SHUKKA_E.NIZUMI_GYOUSHA_NAME,
          SHUKKA_E.NIZUMI_GENBA_CD,
          SHUKKA_E.NIZUMI_GENBA_NAME,
          HAIKI.HAIKI_SHURUI_CD,
          HAIKI.HAIKI_SHURUI_NAME_RYAKU,
          SHUKKA_E.SHASHU_CD,
          SHUKKA_E.SHASHU_NAME,
          SHUKKA_E.SHARYOU_CD,
          SHUKKA_E.SHARYOU_NAME,
          SHUKKA_E.UNTENSHA_CD,
          SHUKKA_E.UNTENSHA_NAME,
          SHUKKA_E.SYSTEM_ID,
          SHUKKA_D.DETAIL_SYSTEM_ID,
          SHUKKA_E.MANIFEST_SHURUI_CD
     FROM (SELECT TOP 1
                  SHUKKA_E.*
             FROM T_SHUKKA_ENTRY  SHUKKA_E
        LEFT JOIN T_SHUKKA_DETAIL SHUKKA_D
               ON SHUKKA_E.SYSTEM_ID = SHUKKA_D.SYSTEM_ID
              AND SHUKKA_E.SEQ       = SHUKKA_D.SEQ
            WHERE SHUKKA_E.SHUKKA_NUMBER = /*data.RENKEI_NUMBER*/0
              AND SHUKKA_E.DELETE_FLG    = 0
             /*IF data.RENKEI_ROW_NO != NULL*/
              AND SHUKKA_D.ROW_NO        = /*data.RENKEI_ROW_NO*/0
             /*END*/) SHUKKA_E
LEFT JOIN T_SHUKKA_DETAIL SHUKKA_D
       ON SHUKKA_E.SYSTEM_ID = SHUKKA_D.SYSTEM_ID
      AND SHUKKA_E.SEQ       = SHUKKA_D.SEQ
LEFT JOIN M_HINMEI
       ON SHUKKA_D.HINMEI_CD  = M_HINMEI.HINMEI_CD
LEFT JOIN M_HAIKI_SHURUI HAIKI
       ON HAIKI.HAIKI_KBN_CD    = 2
      AND HAIKI.HAIKI_SHURUI_CD = M_HINMEI.KP_HAIKI_SHURUI_CD
    WHERE SHUKKA_E.SHUKKA_NUMBER = /*data.RENKEI_NUMBER*/0
      AND SHUKKA_E.DELETE_FLG    = 0
     /*IF data.RENKEI_ROW_NO != NULL*/
      AND SHUKKA_D.ROW_NO        = /*data.RENKEI_ROW_NO*/0
     /*END*/
