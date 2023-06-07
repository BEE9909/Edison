﻿----一括入力内容取得SQL文
SELECT
DT_R18.HIKIWATASHI_DATE AS R18_SYOBUNN_SYUURYOUHI,  --処分終了日(引渡し日を使用)
DT_R19.UPN_END_DATE AS R19_SYOBUNN_SYUURYOUHI,  --処分終了日(運搬終了日を使用)
DT_R18.HIKIWATASHI_DATE AS R18_HAIKIBUTU_JYURYOUHI,  --廃棄物受領日(引渡し日を使用)
DT_R19.UPN_END_DATE AS R19_HAIKIBUTU_JYURYOUHI,  --廃棄物受領日(運搬終了日を使用)
DT_R19.UPN_TAN_NAME AS R19_UNNPANN_TANNTOUSYA_NAME1,--運搬担当者名(登録時の運搬担当者を使用)
DT_R19.UPNREP_UPN_TAN_NAME AS R19_UNNPANN_TANNTOUSYA_NAME2,--運搬担当者名(運搬終了報告時の運搬担当者を使用)
DT_R19.CAR_NO AS SYARYOU_CD,--車輌番号(登録時の車輌番号を使用)
M_SHARYOU.SHARYOU_NAME_RYAKU AS SYARYOU_NAME,--車輌名称(登録時の車輌番号を使用)
DT_R19_EX.UPNREP_SHARYOU_CD AS UPN_SYARYOU_CD,--車輌番号(運搬終了報告時の車輌番号を使用)
DT_R19.UPNREP_CAR_NO AS UPN_SYARYOU_NAME,--車輌名称(運搬終了報告時の車輌番号を使用)
DT_R18.HAIKI_SUU AS R18_UKEIRERYOU,--受入量(登録時の引渡し量を使用)
DT_R19.UPN_SUU AS R19_UKEIRERYOU,--受入量(搬終了報告時の運搬量を使用)
DT_R18.HAIKI_UNIT_CODE AS R18_SYARYOU_CD,--受入量単位コード(登録時の引渡し量を使用)
DT_R19.UPN_UNIT_CODE AS R19_SYARYOU_CD,--受入量単位コード(搬終了報告時の運搬量を使用)
M_UNIT_R18.UNIT_NAME AS R18_SYARYOU_NAME,--受入量単位名称(登録時の引渡し量を使用)
M_UNIT_R19.UNIT_NAME AS R19_SYARYOU_NAME,--受入量単位名称(搬終了報告時の運搬量を使用)
DT_MF_TOC.KIND AS KIND -- 種類（入力区分）
FROM
DT_R18
LEFT JOIN DT_R19
ON  DT_R18.KANRI_ID = DT_R19.KANRI_ID
AND DT_R18.SEQ = DT_R19.SEQ
AND DT_R19.UPN_ROUTE_NO = (
SELECT MAX(DT_R19.UPN_ROUTE_NO) AS MAX_UPN_ROUTE_NO
FROM DT_R18
INNER JOIN DT_R19 
ON 
DT_R18.KANRI_ID = DT_R19.KANRI_ID
AND DT_R18.SEQ = DT_R19.SEQ
AND DT_R18.KANRI_ID = /*data.KANRI_ID*/
AND DT_R18.SEQ = /*data.SEQ*/
)
INNER JOIN DT_MF_TOC 
ON DT_R18.KANRI_ID = DT_MF_TOC.KANRI_ID
AND DT_R18.SEQ = DT_MF_TOC.LATEST_SEQ
LEFT JOIN DT_R19_EX 
ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID
AND DT_R19.SEQ = DT_R19_EX.SEQ
AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO
LEFT JOIN M_UNIT M_UNIT_R18
ON DT_R18.HAIKI_UNIT_CODE = M_UNIT_R18.UNIT_CD
LEFT JOIN M_UNIT M_UNIT_R19
ON DT_R19.UPN_UNIT_CODE = M_UNIT_R19.UNIT_CD
LEFT JOIN M_SHARYOU
ON M_SHARYOU.SHARYOU_CD = DT_R19.CAR_NO
AND M_SHARYOU.GYOUSHA_CD = /*data.GYOUSHA_CD*/
WHERE
DT_R18.KANRI_ID = /*data.KANRI_ID*/ AND
DT_R18.SEQ = /*data.SEQ*/