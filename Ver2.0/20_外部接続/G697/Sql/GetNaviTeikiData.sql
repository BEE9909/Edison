﻿SELECT DISTINCT
0 AS TAISHO,
ISNULL(TNCE.PROCESSING_ID, '') AS PROCESSING_ID,
ISNULL(ND.DELIVERY_DATE, CONVERT(NVARCHAR, GETDATE(), 111)) AS DELIVERY_DATE,
ENT.DAY_CD,
CASE ENT.DAY_CD WHEN 1 THEN '月' WHEN 2 THEN '火' WHEN 3 THEN '水' WHEN 4 THEN '木' WHEN 5 THEN '金' WHEN 6 THEN '土' WHEN 7 THEN '日' ELSE '' END DAY_NAME,
ENT.COURSE_NAME_CD,
MCN.COURSE_NAME_RYAKU AS COURSE_NAME,
ENT.SHASHU_CD,
MSS.SHASHU_NAME_RYAKU AS SHASHU_NAME,
ENT.SHARYOU_CD,
MSR.SHARYOU_NAME_RYAKU AS SHARYOU_NAME,
MNOS.NAVI_SHASHU_CD AS SHARYOU_TYPE,
ENT.UNTENSHA_CD,
MU.SHAIN_NAME_RYAKU AS UNTENSHA_NAME,
ENT.UNPAN_GYOUSHA_CD,
MG.GYOUSHA_NAME_RYAKU AS UNPAN_GYOUSHA_NAME,
ND.SAGYOUSHA_CD,
MNS.GYOUSHA_CD AS SHUPPATSU_GYOUSHA_CD,
MNS.GENBA_CD AS SHUPPATSU_GENBA_CD,
MGEN.GENBA_NAME_RYAKU AS SHUPPATSU_GENBA_NAME,
MNS.NAVI_EIGYOUSHO_CD AS SHUPPATSU_EIGYOUSHO_CD,
CASE WHEN ND.NIOROSHI_GYOUSHA_CD IS NOT NULL THEN ND.NIOROSHI_GYOUSHA_CD
WHEN (SELECT COUNT(CN.SYSTEM_ID) AS CNT FROM T_TEIKI_HAISHA_NIOROSHI AS CN WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ) = 1 
THEN (SELECT CN.NIOROSHI_GYOUSHA_CD FROM T_TEIKI_HAISHA_NIOROSHI AS CN INNER JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA AS MNN ON CN.NIOROSHI_GYOUSHA_CD = MNN.GYOUSHA_CD AND CN.NIOROSHI_GENBA_CD = MNN.GENBA_CD WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ)
ELSE '' END NIOROSHI_GYOUSHA_CD,
CASE WHEN ND.NIOROSHI_GENBA_CD IS NOT NULL THEN ND.NIOROSHI_GENBA_CD
WHEN (SELECT COUNT(CN.SYSTEM_ID) AS CNT FROM T_TEIKI_HAISHA_NIOROSHI AS CN WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ) = 1 
THEN (SELECT CN.NIOROSHI_GENBA_CD FROM T_TEIKI_HAISHA_NIOROSHI AS CN INNER JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA AS MNN ON CN.NIOROSHI_GYOUSHA_CD = MNN.GYOUSHA_CD AND CN.NIOROSHI_GENBA_CD = MNN.GENBA_CD WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ)
ELSE '' END NIOROSHI_GENBA_CD,
CASE WHEN ND.NIOROSHI_GENBA_CD IS NOT NULL THEN MGN.GENBA_NAME_RYAKU
WHEN (SELECT COUNT(CN.SYSTEM_ID) AS CNT FROM T_TEIKI_HAISHA_NIOROSHI AS CN WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ) = 1 
THEN (SELECT MG.GENBA_NAME_RYAKU FROM T_TEIKI_HAISHA_NIOROSHI AS CN INNER JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA AS MNN ON CN.NIOROSHI_GYOUSHA_CD = MNN.GYOUSHA_CD AND CN.NIOROSHI_GENBA_CD = MNN.GENBA_CD LEFT JOIN M_GENBA MG ON MNN.GYOUSHA_CD = MG.GYOUSHA_CD AND MNN.GENBA_CD = MG.GENBA_CD WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ)
ELSE '' END NIOROSHI_GENBA_NAME,
CASE WHEN ND.NIOROSHI_EIGYOUSHO_CD IS NOT NULL THEN ND.NIOROSHI_EIGYOUSHO_CD
WHEN (SELECT COUNT(CN.SYSTEM_ID) AS CNT FROM T_TEIKI_HAISHA_NIOROSHI AS CN WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ) = 1 
THEN (SELECT MNN.NAVI_EIGYOUSHO_CD FROM T_TEIKI_HAISHA_NIOROSHI AS CN INNER JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA AS MNN ON CN.NIOROSHI_GYOUSHA_CD = MNN.GYOUSHA_CD AND CN.NIOROSHI_GENBA_CD = MNN.GENBA_CD WHERE CN.SYSTEM_ID = ENT.SYSTEM_ID AND CN.SEQ = ENT.SEQ)
ELSE '' END NIOROSHI_EIGYOUSHO_CD,
CASE WHEN (CASE WHEN ND.TRAFFIC_CONSIDERATION IS NULL THEN SYS.NAVI_TRAFFIC ELSE ND.TRAFFIC_CONSIDERATION END) = 1 THEN -1 ELSE 0 END TRAFFIC_CONSIDERATION,
CASE WHEN (CASE WHEN ND.SMART_IC_CONSIDERATION IS NULL THEN SYS.NAVI_SMART_IC ELSE ND.SMART_IC_CONSIDERATION END) = 1 THEN -1 ELSE 0 END SMART_IC_CONSIDERATION,
CASE WHEN (CASE WHEN ND.PRIORITY IS NULL THEN SYS.NAVI_TOLL ELSE ND.PRIORITY END) = 1 THEN -1 ELSE 0 END PRIORITY,
0 AS NAVI_DELIVERY_ORDER,
ISNULL(ND.SYSTEM_ID, 0) AS SYSTEM_ID,
/*IF data.RENKEI_KBN == 1*/
ISNULL(ND.DEPARTURE_TIME, LEFT(CONVERT(VARCHAR, CONVERT(VARCHAR, ENT.SAGYOU_BEGIN_HOUR) + ':' + CONVERT(VARCHAR, ENT.SAGYOU_BEGIN_MINUTE), 108), 5)) AS DEPARTURE_TIME,
ISNULL(ND.ARRIVAL_TIME, LEFT(CONVERT(VARCHAR, CONVERT(VARCHAR, ENT.SAGYOU_END_HOUR) + ':' + CONVERT(VARCHAR, ENT.SAGYOU_END_MINUTE), 108), 5)) AS ARRIVAL_TIME,
--ELSE
ND.DEPARTURE_TIME,
ND.ARRIVAL_TIME,
/*END*/
ND.BIN_NO,
ENT.SYSTEM_ID AS TEIKI_SYSTEM_ID,
ENT.SEQ AS TEIKI_SEQ

FROM T_TEIKI_HAISHA_ENTRY ENT

LEFT JOIN M_COURSE_NAME MCN ON ENT.COURSE_NAME_CD = MCN.COURSE_NAME_CD
LEFT JOIN M_SHASHU MSS ON ENT.SHASHU_CD = MSS.SHASHU_CD
LEFT JOIN M_SHARYOU MSR ON ENT.UNPAN_GYOUSHA_CD = MSR.GYOUSHA_CD AND ENT.SHARYOU_CD = MSR.SHARYOU_CD
LEFT JOIN M_SHAIN MU ON ENT.UNTENSHA_CD = MU.SHAIN_CD
LEFT JOIN M_GYOUSHA MG ON ENT.UNPAN_GYOUSHA_CD = MG.GYOUSHA_CD
LEFT JOIN T_NAVI_DELIVERY ND ON ENT.SYSTEM_ID = ND.TEIKI_SYSTEM_ID AND ND.DELETE_FLG = 0
LEFT JOIN T_NAVI_COLLABORATION_EVENTS TNCE ON ND.SYSTEM_ID = TNCE.SYSTEM_ID
/*IF data.RENKEI_KBN == 1*/
LEFT JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA MNS ON ENT.SHUPPATSU_GYOUSHA_CD = MNS.GYOUSHA_CD AND ENT.SHUPPATSU_GENBA_CD = MNS.GENBA_CD AND MNS.OUTPUT_DATE IS NOT NULL AND MNS.JYOGAI_FLG = 0
/*END*/
/*IF data.RENKEI_KBN == 2*/
LEFT JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA MNS ON ND.SHUPPATSU_GYOUSHA_CD = MNS.GYOUSHA_CD AND ND.SHUPPATSU_GENBA_CD = MNS.GENBA_CD AND MNS.OUTPUT_DATE IS NOT NULL AND MNS.JYOGAI_FLG = 0
INNER JOIN T_NAVI_LINK_STATUS NLS ON ND.SYSTEM_ID = NLS.SYSTEM_ID AND NLS.DELETE_FLG = 0 AND LINK_STATUS = 2
/*END*/
/*IF data.RENKEI_KBN == 3*/
LEFT JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA MNS ON ND.SHUPPATSU_GYOUSHA_CD = MNS.GYOUSHA_CD AND ND.SHUPPATSU_GENBA_CD = MNS.GENBA_CD AND MNS.OUTPUT_DATE IS NOT NULL AND MNS.JYOGAI_FLG = 0
INNER JOIN T_NAVI_LINK_STATUS NLS ON ND.SYSTEM_ID = NLS.SYSTEM_ID AND NLS.DELETE_FLG = 0 AND LINK_STATUS = 3
/*END*/
LEFT JOIN M_GENBA MGEN ON MNS.GYOUSHA_CD = MGEN.GYOUSHA_CD AND MNS.GENBA_CD = MGEN.GENBA_CD
LEFT JOIN M_NAVI_OUTPUT_SHASHU MNOS ON ENT.SHASHU_CD = MNOS.SHASHU_CD AND MNOS.OUTPUT_DATE IS NOT NULL AND MNOS.JYOGAI_FLG = 0
LEFT JOIN M_GENBA MGN ON ND.NIOROSHI_GYOUSHA_CD = MGN.GYOUSHA_CD AND ND.NIOROSHI_GENBA_CD = MGN.GENBA_CD
LEFT JOIN M_SYS_INFO SYS ON SYS.SYS_ID = 0

WHERE
ENT.DELETE_FLG = 0
AND ENT.KYOTEN_CD = /*data.KYOTEN_CD*/
/*IF data.RENKEI_KBN == 1*/
AND ND.SYSTEM_ID IS NULL
--ELSE
AND ND.SYSTEM_ID IS NOT NULL
/*END*/
/*IF data.SAGYOU_DATE != null*/ AND ENT.SAGYOU_DATE = /*data.SAGYOU_DATE*//*END*/
/*IF data.COURSE_NAME_CD != null*/ AND ENT.COURSE_NAME_CD = /*data.COURSE_NAME_CD*//*END*/
/*IF data.SHASHU_CD != null*/ AND ENT.SHASHU_CD = /*data.SHASHU_CD*//*END*/
/*IF data.SHARYOU_CD != null*/ AND ENT.SHARYOU_CD = /*data.SHARYOU_CD*//*END*/
/*IF data.UNTENSHA_CD != null*/ AND ENT.UNTENSHA_CD = /*data.UNTENSHA_CD*//*END*/
/*IF data.UNPAN_GYOUSHA_CD != null*/ AND ENT.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*//*END*/

ORDER BY ENT.COURSE_NAME_CD, ENT.DAY_CD