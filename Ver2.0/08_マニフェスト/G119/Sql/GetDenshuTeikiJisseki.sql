﻿SELECT
--交付年月日
TUSE.SAGYOU_DATE as KOUFU_DATE,
--運搬終了年月日
TUSE.SAGYOU_DATE as UNPAN_SYURYOU_DATA,
--運搬受託者CD
TUSE.UNPAN_GYOUSHA_CD as UNPAN_GYOUSHA_CD,
--車種
TUSE.SHASHU_CD as SHASHU_CD,
MSS.SHASHU_NAME_RYAKU as SHASHU_NAME,
--車輛
TUSE.SHARYOU_CD as SHARYOU_CD,
MSR.SHARYOU_NAME_RYAKU as SHARYOU_NAME,
--運転者
TUSE.UNTENSHA_CD as UNTENSHA_CD,
MSI.SHAIN_NAME_RYAKU as UNTENSHA_NAME,
--排出事業者
TUSD.GYOUSHA_CD as HAISYUTU_GYOUSHA_CD,
--排出事業場
TUSD.GENBA_CD as HAISYUTU_GENBA_CD,
--廃棄物種類
MHS.HAIKI_SHURUI_CD as HAIKI_SHURUI_CD,
MHS.HAIKI_SHURUI_NAME_RYAKU as HAIKI_SHURUI_NAME_RYAKU,
--数量
TUSD.SUURYOU,
--単位
TUSD.UNIT_CD,
--単位名
UNIT.UNIT_NAME_RYAKU as UNIT_NAME,
--換算数量
TUSD.KANSAN_SUURYOU,
--換算単位
TUSD.KANSAN_UNIT_CD,
--換算単位名
KANSANUNIT.UNIT_NAME_RYAKU as KANSAN_UNIT_NAME,
--荷降業者
TTJN.NIOROSHI_GYOUSHA_CD,
--荷降現場
TTJN.NIOROSHI_GENBA_CD,
TUSE.SYSTEM_ID as SYSTEM_ID,
TUSD.DETAIL_SYSTEM_ID as DETAIL_SYSTEM_ID
FROM 
(SELECT TOP 1 TUSE.* FROM T_TEIKI_JISSEKI_ENTRY TUSE
left join T_TEIKI_JISSEKI_DETAIL TUSD ON (TUSE.SYSTEM_ID = TUSD.SYSTEM_ID AND TUSE.SEQ = TUSD.SEQ)
WHERE TUSE.TEIKI_JISSEKI_NUMBER =  /*data.RENKEI_ID*/0
AND TUSE.DELETE_FLG = 0
/*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
AND TUSD.ROW_NUMBER = /*data.RENKEI_MEISAI_ID*/0
/*END*/
) TUSE
left join T_TEIKI_JISSEKI_DETAIL TUSD ON (TUSE.SYSTEM_ID = TUSD.SYSTEM_ID AND TUSE.SEQ = TUSD.SEQ)
left join M_HINMEI MH ON (MH.HINMEI_CD = TUSD.HINMEI_CD)
left join M_HAIKI_SHURUI MHS ON (MHS.HAIKI_SHURUI_CD = MH.SP_CHOKKOU_HAIKI_SHURUI_CD
                              AND MHS.HAIKI_KBN_CD = 1)
left join T_TEIKI_JISSEKI_NIOROSHI TTJN ON (TUSE.SYSTEM_ID = TTJN.SYSTEM_ID AND TUSE.SEQ = TTJN.SEQ AND TUSD.NIOROSHI_NUMBER = TTJN.NIOROSHI_NUMBER)
left join M_SHASHU MSS ON (TUSE.SHASHU_CD = MSS.SHASHU_CD)
left join M_SHARYOU MSR ON (TUSE.UNPAN_GYOUSHA_CD = MSR.GYOUSHA_CD AND TUSE.SHARYOU_CD = MSR.SHARYOU_CD)
left join M_SHAIN MSI ON (TUSE.UNTENSHA_CD = MSI.SHAIN_CD)
left join M_UNIT UNIT ON (TUSD.UNIT_CD = UNIT.UNIT_CD)
left join M_UNIT KANSANUNIT ON (TUSD.KANSAN_UNIT_CD = KANSANUNIT.UNIT_CD)
WHERE TUSE.TEIKI_JISSEKI_NUMBER =  /*data.RENKEI_ID*/0
AND TUSE.DELETE_FLG = 0
/*IF data.RENKEI_MEISAI_ID != NULL && data.RENKEI_MEISAI_ID != ''*/
AND TUSD.ROW_NUMBER = /*data.RENKEI_MEISAI_ID*/0
/*END*/
ORDER BY TUSD.ROW_NUMBER