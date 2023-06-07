﻿/* 20140522 ria No.679 伝種区分連携 start*/
WITH UKETSUKE AS
(
    SELECT
        TUSSE.SAGYOU_DATE as KOUFU_DATE,
        TUSSE.TORIHIKISAKI_CD as TORIHIKISAKI_CD,
        TUSSE.TORIHIKISAKI_NAME as TORIHIKISAKI_NAME,
        TUSSE.GYOUSHA_CD as HAISYUTU_GYOUSHA_CD,
        TUSSE.GYOUSHA_NAME as GYOUSHA_NAME,
        TUSSE.GENBA_CD as HAISYUTU_GENBA_CD,
        TUSSE.GENBA_NAME as GENBA_NAME,
        TUSSE.UNPAN_GYOUSHA_CD as UNPAN_GYOUSHA_CD,
        TUSSE.UNPAN_GYOUSHA_NAME as UNPAN_GYOUSHA_NAME,
        TUSSE.NIOROSHI_GYOUSHA_CD as SYOBUN_GYOUSHA_CD,
        TUSSE.NIOROSHI_GYOUSHA_NAME as SYOBUN_GYOUSHA_NAME,
        TUSSE.NIOROSHI_GENBA_CD as UNPAN_GENBA_CD,
        TUSSE.NIOROSHI_GENBA_NAME as NIOROSHI_GENBA_NAME,
        TUSSE.SHASHU_CD as SHASHU_CD,
        TUSSE.SHASHU_NAME as SHASHU_NAME,
        TUSSE.SHARYOU_CD as SHARYOU_CD,
        TUSSE.SHARYOU_NAME as SHARYOU_NAME,
        TUSSE.UNTENSHA_CD as UNTENSHA_CD,
        TUSSE.UNTENSHA_NAME as UNTENSHA_NAME,
        TUSSE.NIOROSHI_GYOUSHA_CD as NIOROSHI_GYOUSHA_CD,
        TUSSE.NIOROSHI_GYOUSHA_NAME as NIOROSHI_GYOUSHA_NAME,
        TUSSE.SAGYOU_DATE as UNPAN_SYURYOU_DATA,
        TUSSD.HINMEI_CD as HINMEI_CD,
        TUSSE.SYSTEM_ID as SYSTEM_ID,
        TUSSD.DETAIL_SYSTEM_ID as DETAIL_SYSTEM_ID,
        TUSSE.MANIFEST_SHURUI_CD as MANIFEST_SHURUI_CD,
        1 as ORDERKBN
    FROM 
      (
         SELECT TOP 1 TUSSE.* FROM T_UKETSUKE_SS_ENTRY TUSSE
         left join T_UKETSUKE_SS_DETAIL TUSSD ON (TUSSE.SYSTEM_ID = TUSSD.SYSTEM_ID AND TUSSE.SEQ = TUSSD.SEQ)
         WHERE TUSSE.UKETSUKE_NUMBER = /*data.RENKEI_ID*/0
           AND TUSSE.DELETE_FLG = 0
           /*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
           AND TUSSD.ROW_NO = /*data.RENKEI_MEISAI_ID*/0
           /*END*/
           AND /*data.RENKEI_MANI_FLAG*/0 = 1
       ) TUSSE
    left join T_UKETSUKE_SS_DETAIL TUSSD ON (TUSSE.SYSTEM_ID = TUSSD.SYSTEM_ID AND TUSSE.SEQ = TUSSD.SEQ)
    WHERE TUSSE.UKETSUKE_NUMBER = /*data.RENKEI_ID*/0    
      AND TUSSE.DELETE_FLG = 0    
      /*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
      AND TUSSD.ROW_NO = /*data.RENKEI_MEISAI_ID*/0
      /*END*/
     AND /*data.RENKEI_MANI_FLAG*/0 = 1
    UNION ALL
      SELECT
        TUMKE.SAGYOU_DATE as KOUFU_DATE,
        TUMKE.TORIHIKISAKI_CD as TORIHIKISAKI_CD,
        TUMKE.TORIHIKISAKI_NAME as TORIHIKISAKI_NAME,
        TUMKE.GYOUSHA_CD as HAISYUTU_GYOUSHA_CD,
        TUMKE.GYOUSHA_NAME as GYOUSHA_NAME,
        TUMKE.GENBA_CD as HAISYUTU_GENBA_CD,
        TUMKE.GENBA_NAME as GENBA_NAME,
        TUMKE.UNPAN_GYOUSHA_CD as UNPAN_GYOUSHA_CD,
        TUMKE.UNPAN_GYOUSHA_NAME as UNPAN_GYOUSHA_NAME,
        TUMKE.NIOROSHI_GYOUSHA_CD as SYOBUN_GYOUSHA_CD,
        TUMKE.NIOROSHI_GYOUSHA_NAME as SYOBUN_GYOUSHA_NAME,
        TUMKE.NIOROSHI_GENBA_CD as UNPAN_GENBA_CD,
        TUMKE.NIOROSHI_GENBA_NAME as NIOROSHI_GENBA_NAME,
        TUMKE.SHASHU_CD as SHASHU_CD,
        TUMKE.SHASHU_NAME as SHASHU_NAME,
        TUMKE.SHARYOU_CD as SHARYOU_CD,
        TUMKE.SHARYOU_NAME as SHARYOU_NAME,
         '' as UNTENSHA_CD,
         '' as UNTENSHA_NAME,
        TUMKE.NIOROSHI_GYOUSHA_CD as NIOROSHI_GYOUSHA_CD,
        TUMKE.NIOROSHI_GYOUSHA_NAME as NIOROSHI_GYOUSHA_NAME,
        TUMKE.SAGYOU_DATE as UNPAN_SYURYOU_DATA,
        TUMKD.HINMEI_CD as HINMEI_CD,
        TUMKE.SYSTEM_ID as SYSTEM_ID,
        TUMKD.DETAIL_SYSTEM_ID as DETAIL_SYSTEM_ID,
        TUMKE.MANIFEST_SHURUI_CD as MANIFEST_SHURUI_CD,
        2 as ORDERKBN
    FROM 
     (
       SELECT TOP 1 TUMKE.* FROM T_UKETSUKE_MK_ENTRY TUMKE
       left join T_UKETSUKE_MK_DETAIL TUMKD ON (TUMKE.SYSTEM_ID = TUMKD.SYSTEM_ID AND TUMKE.SEQ = TUMKD.SEQ)
       WHERE TUMKE.UKETSUKE_NUMBER = /*data.RENKEI_ID*/0
         AND TUMKE.DELETE_FLG = 0
         /*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
         AND TUMKD.ROW_NO = /*data.RENKEI_MEISAI_ID*/0
         /*END*/
         AND /*data.RENKEI_MANI_FLAG*/0 = 1
     ) TUMKE
    left join T_UKETSUKE_MK_DETAIL TUMKD ON (TUMKE.SYSTEM_ID = TUMKD.SYSTEM_ID AND TUMKE.SEQ = TUMKD.SEQ)
    WHERE TUMKE.UKETSUKE_NUMBER = /*data.RENKEI_ID*/0
      AND TUMKE.DELETE_FLG = 0
      /*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
      AND TUMKD.ROW_NO = /*data.RENKEI_MEISAI_ID*/0
      /*END*/
      AND /*data.RENKEI_MANI_FLAG*/0 = 1
    UNION ALL
      SELECT
        TUSKE.SAGYOU_DATE as KOUFU_DATE,
        TUSKE.TORIHIKISAKI_CD as TORIHIKISAKI_CD,
        TUSKE.TORIHIKISAKI_NAME as TORIHIKISAKI_NAME,
        TUSKE.NIZUMI_GYOUSHA_CD as HAISYUTU_GYOUSHA_CD,
        TUSKE.NIZUMI_GYOUSHA_NAME as GYOUSHA_NAME,
        TUSKE.NIZUMI_GENBA_CD as HAISYUTU_GENBA_CD,
        TUSKE.NIZUMI_GENBA_NAME as GENBA_NAME,
        TUSKE.UNPAN_GYOUSHA_CD as UNPAN_GYOUSHA_CD,
        TUSKE.UNPAN_GYOUSHA_NAME as UNPAN_GYOUSHA_NAME,
        TUSKE.GYOUSHA_CD as SYOBUN_GYOUSHA_CD,
        TUSKE.GYOUSHA_NAME as SYOBUN_GYOUSHA_NAME,
        TUSKE.GENBA_CD as UNPAN_GENBA_CD,
        TUSKE.GENBA_NAME as NIOROSHI_GENBA_NAME,
        TUSKE.SHASHU_CD as SHASHU_CD,
        TUSKE.SHASHU_NAME as SHASHU_NAME,
        TUSKE.SHARYOU_CD as SHARYOU_CD,
        TUSKE.SHARYOU_NAME as SHARYOU_NAME,
        TUSKE.UNTENSHA_CD as UNTENSHA_CD,
        TUSKE.UNTENSHA_NAME as UNTENSHA_NAME,
        TUSKE.GYOUSHA_CD as NIOROSHI_GYOUSHA_CD,
        TUSKE.GYOUSHA_NAME as NIOROSHI_GYOUSHA_NAME,
        TUSKE.SAGYOU_DATE as UNPAN_SYURYOU_DATA,
        TUSKD.HINMEI_CD as HINMEI_CD,
        TUSKE.SYSTEM_ID as SYSTEM_ID,
        TUSKD.DETAIL_SYSTEM_ID as DETAIL_SYSTEM_ID,
        TUSKE.MANIFEST_SHURUI_CD as MANIFEST_SHURUI_CD,
        3 as ORDERKBN
    FROM 
    (
      SELECT TOP 1 TUSKE.* FROM T_UKETSUKE_SK_ENTRY TUSKE
      left join T_UKETSUKE_SK_DETAIL TUSKD ON (TUSKE.SYSTEM_ID = TUSKD.SYSTEM_ID AND TUSKE.SEQ = TUSKD.SEQ)
      WHERE TUSKE.UKETSUKE_NUMBER = /*data.RENKEI_ID*/0
        AND TUSKE.DELETE_FLG = 0
        /*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
        AND TUSKD.ROW_NO = /*data.RENKEI_MEISAI_ID*/0
        /*END*/
        AND /*data.RENKEI_MANI_FLAG*/0 = 2
    )  TUSKE
    left join T_UKETSUKE_SK_DETAIL TUSKD ON (TUSKE.SYSTEM_ID = TUSKD.SYSTEM_ID AND TUSKE.SEQ = TUSKD.SEQ)
    WHERE TUSKE.UKETSUKE_NUMBER = /*data.RENKEI_ID*/0
      AND TUSKE.DELETE_FLG = 0
      /*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
      AND TUSKD.ROW_NO = /*data.RENKEI_MEISAI_ID*/0
      /*END*/
      AND /*data.RENKEI_MANI_FLAG*/0 = 2
)
   SELECT UKETSUKE.*,
          HAIKI.HAIKI_SHURUI_CD         AS HAIKI_SHURUI_CD,
          HAIKI.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI_NAME_RYAKU
     FROM UKETSUKE
LEFT JOIN M_HINMEI
       ON UKETSUKE.HINMEI_CD  = M_HINMEI.HINMEI_CD
LEFT JOIN M_HAIKI_SHURUI HAIKI
       ON HAIKI.HAIKI_SHURUI_CD = M_HINMEI.SP_CHOKKOU_HAIKI_SHURUI_CD
      AND HAIKI.HAIKI_KBN_CD    = 1
  WHERE ORDERKBN = (SELECT MIN(ORDERKBN) FROM UKETSUKE)
/* 20140522 ria No.679 伝種区分連携 end*/