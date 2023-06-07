﻿   SELECT UKEIRE_E.DENPYOU_DATE,
          UKEIRE_E.TORIHIKISAKI_CD,
          UKEIRE_E.TORIHIKISAKI_NAME,
          UKEIRE_E.GYOUSHA_CD,
          UKEIRE_E.GYOUSHA_NAME,
          UKEIRE_E.GENBA_CD,
          UKEIRE_E.GENBA_NAME,
          UKEIRE_E.UNPAN_GYOUSHA_CD,
          UKEIRE_E.UNPAN_GYOUSHA_NAME,
          UKEIRE_E.NIOROSHI_GYOUSHA_CD,
          UKEIRE_E.NIOROSHI_GYOUSHA_NAME,
          UKEIRE_E.NIOROSHI_GENBA_CD,
          UKEIRE_E.NIOROSHI_GENBA_NAME,
          HAIKI.HAIKI_SHURUI_CD,
          HAIKI.HAIKI_SHURUI_NAME_RYAKU,
          UKEIRE_E.SHASHU_CD,
          UKEIRE_E.SHASHU_NAME,
          UKEIRE_E.SHARYOU_CD,
          UKEIRE_E.SHARYOU_NAME,
          UKEIRE_E.UNTENSHA_CD,
          UKEIRE_E.UNTENSHA_NAME,
          UKEIRE_E.SYSTEM_ID,
          UJD.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID,
          UKEIRE_E.MANIFEST_SHURUI_CD,
		  ISNULL(UKEIRE_E.NET_TOTAL,0) AS NET_TOTAL,
		  ISNULL(UJD.SUURYOU_WARIAI,0) AS SUURYOU_WARIAI,
		  M_UNIT.UNIT_NAME
     FROM (SELECT TOP 1
                  UKEIRE_E.*, UJE.SEQ AS UDE_SEQ, UJD.DETAIL_SEQ AS UJD_DETAIL_SEQ, 3 AS UNIT_CD
             FROM T_UKEIRE_ENTRY  UKEIRE_E
		LEFT JOIN T_UKEIRE_JISSEKI_ENTRY UJE ON (UKEIRE_E.SYSTEM_ID = UJE.DENPYOU_SYSTEM_ID AND UJE.DENPYOU_SHURUI = 2 AND UJE.DELETE_FLG = 0) 
		LEFT JOIN T_UKEIRE_JISSEKI_DETAIL UJD ON (UJE.DENPYOU_SHURUI = UJD.DENPYOU_SHURUI AND UJE.DENPYOU_SYSTEM_ID = UJD.DENPYOU_SYSTEM_ID AND UJE.SEQ = UJD.SEQ)
            WHERE UKEIRE_E.UKEIRE_NUMBER = /*data.RENKEI_NUMBER*/0
              AND UKEIRE_E.DELETE_FLG    = 0
             /*IF data.RENKEI_ROW_NO != NULL*/
              AND UJD.DETAIL_SEQ          = /*data.RENKEI_ROW_NO*/0
             /*END*/) UKEIRE_E
LEFT JOIN T_UKEIRE_JISSEKI_DETAIL UJD ON (UKEIRE_E.SYSTEM_ID = UJD.DENPYOU_SYSTEM_ID AND UKEIRE_E.UDE_SEQ = UJD.SEQ AND UJD.DENPYOU_SHURUI = 2)
LEFT JOIN M_HINMEI
       ON UJD.HINMEI_CD  = M_HINMEI.HINMEI_CD
LEFT JOIN M_HAIKI_SHURUI HAIKI
       ON HAIKI.HAIKI_KBN_CD    = 2
      AND HAIKI.HAIKI_SHURUI_CD = M_HINMEI.KP_HAIKI_SHURUI_CD
LEFT JOIN M_UNIT ON UKEIRE_E.UNIT_CD = M_UNIT.UNIT_CD
    WHERE UKEIRE_E.UKEIRE_NUMBER = /*data.RENKEI_NUMBER*/0
      AND UKEIRE_E.DELETE_FLG    = 0
     /*IF data.RENKEI_ROW_NO != NULL*/
      AND UJD.DETAIL_SEQ        = /*data.RENKEI_ROW_NO*/0
     /*END*/
/*IF (data.RENKEI_ROW_NO == NULL || data.RENKEI_ROW_NO == '') && data.CHK_MODE_KBN == 1 */
AND UJD.SUURYOU_WARIAI IS NOT NULL
/*END*/