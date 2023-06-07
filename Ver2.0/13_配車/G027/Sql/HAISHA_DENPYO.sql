﻿SELECT CAST(1 AS smallint) SHUBETSU_KBN_
, SS.SYSTEM_ID SYSTEM_ID_
, SS.UKETSUKE_NUMBER DENPYOU_NUM_
, SS.SEQ
, SS.HAISHA_JOKYO_CD AS HAISHA_JOKYO
, CASE SS.HAISHA_SHURUI_CD WHEN 2 THEN '仮' WHEN 3 THEN '確' ELSE '' END AS HAISHA_SHURUI
, CASE WHEN SS.SAGYOU_DATE_BEGIN IS NULL AND SS.SAGYOU_DATE_END IS NULL THEN '' WHEN SS.SAGYOU_DATE_END <> SS.SAGYOU_DATE THEN '期間' ELSE '期間終了' END AS SAGYOUDATE_KUBUN
, ISNULL(SS.GENCHAKU_TIME_NAME,'') + ISNULL(LEFT(CONVERT(varchar, SS.GENCHAKU_TIME, 114), 5),'') AS GENCHAKU_JIKAN
, GTSS.GENCHAKU_BACK_COLOR
, CASE SS.HAISHA_SIJISHO_FLG WHEN 0 THEN '未印刷' WHEN 1 THEN '印刷済' ELSE '' END AS HAISHA_SIJISHO_STATUS
, CAST(0 AS bit) HAISHA_SIJISHO_CHECKED
, CASE SS.MAIL_SEND_FLG WHEN 0 THEN '未送信' WHEN 1 THEN '送信済'  ELSE '' END AS MAIL_SEND_STATUS
, CAST(0 AS bit) MAIL_SEND_CHECKED
, ISNULL(SS.GYOUSHA_NAME,'') + CHAR(13) + CHAR(10) + ISNULL(SS.GENBA_NAME,'') + CHAR(13) + CHAR(10) + ISNULL(GBSS.GENBA_ADDRESS1,'') AS DENPYOU_CONTENT
, CAST(0 AS bit) KARADENPYOU_FLG_
, ISNULL(GTSS.GENCHAKU_PRIORITY, 0) SORT_KEY1_
, RIGHT(CONVERT(varchar, SS.GENCHAKU_TIME, 120), 8) SORT_KEY2_
, ISNULL(SS.HAISHA_SHURUI_CD, 0) SORT_KEY3_
, SS.GYOUSHA_CD SORT_KEY4_
, SS.GENBA_CD SORT_KEY5_
, ISNULL(SS.UKETSUKE_NUMBER, 0) SORT_KEY6_
, CASE WHEN WDSS.SHUBETSU_KBN_01 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_01 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_01 THEN 1
  WHEN WDSS.SHUBETSU_KBN_02 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_02 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_02 THEN 2
  WHEN WDSS.SHUBETSU_KBN_03 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_03 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_03 THEN 3
  WHEN WDSS.SHUBETSU_KBN_04 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_04 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_04 THEN 4
  WHEN WDSS.SHUBETSU_KBN_05 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_05 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_05 THEN 5
  WHEN WDSS.SHUBETSU_KBN_06 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_06 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_06 THEN 6
  WHEN WDSS.SHUBETSU_KBN_07 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_07 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_07 THEN 7
  WHEN WDSS.SHUBETSU_KBN_08 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_08 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_08 THEN 8
  WHEN WDSS.SHUBETSU_KBN_09 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_09 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_09 THEN 9
  WHEN WDSS.SHUBETSU_KBN_10 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_10 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_10 THEN 10
  WHEN WDSS.SHUBETSU_KBN_11 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_11 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_11 THEN 11
  WHEN WDSS.SHUBETSU_KBN_12 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_12 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_12 THEN 12
  WHEN WDSS.SHUBETSU_KBN_13 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_13 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_13 THEN 13
  WHEN WDSS.SHUBETSU_KBN_14 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_14 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_14 THEN 14
  WHEN WDSS.SHUBETSU_KBN_15 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_15 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_15 THEN 15
  WHEN WDSS.SHUBETSU_KBN_16 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_16 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_16 THEN 16
  WHEN WDSS.SHUBETSU_KBN_17 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_17 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_17 THEN 17
  WHEN WDSS.SHUBETSU_KBN_18 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_18 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_18 THEN 18
  WHEN WDSS.SHUBETSU_KBN_19 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_19 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_19 THEN 19
  WHEN WDSS.SHUBETSU_KBN_20 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_20 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_20 THEN 20
  WHEN WDSS.SHUBETSU_KBN_21 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_21 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_21 THEN 21
  WHEN WDSS.SHUBETSU_KBN_22 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_22 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_22 THEN 22
  WHEN WDSS.SHUBETSU_KBN_23 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_23 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_23 THEN 23
  WHEN WDSS.SHUBETSU_KBN_24 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_24 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_24 THEN 24
  WHEN WDSS.SHUBETSU_KBN_25 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_25 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_25 THEN 25
  WHEN WDSS.SHUBETSU_KBN_26 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_26 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_26 THEN 26
  WHEN WDSS.SHUBETSU_KBN_27 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_27 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_27 THEN 27
  WHEN WDSS.SHUBETSU_KBN_28 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_28 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_28 THEN 28
  WHEN WDSS.SHUBETSU_KBN_29 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_29 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_29 THEN 29
  WHEN WDSS.SHUBETSU_KBN_30 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_30 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_30 THEN 30
  ELSE 31 END AS DENPYOU_SEQ
, 0 ROW_NUM
, SS.SHASHU_CD
, SS.SHARYOU_CD
, SS.UNTENSHA_CD
, ISNULL(SS.UNPAN_GYOUSHA_CD,'') AS UNPAN_GYOUSHA_CD
, (CASE WHEN WDSS.SAGYOU_DATE IS NULL THEN 1 ELSE 0 END) AS HAISHA_FLG
, 0 AS DENSHU_TYPE
FROM (SELECT * FROM T_UKETSUKE_SS_ENTRY
WHERE DELETE_FLG = 0 AND COURSE_KUMIKOMI_CD = 1
AND SHARYOU_CD IS NOT NULL AND SHARYOU_CD <> ''
/*IF data.KyotenCd != null && data.KyotenCd != '' && data.KyotenCd != 99*/
AND (KYOTEN_CD = /*data.KyotenCd*/ OR KYOTEN_CD = 99)
/*END*/
AND ((SAGYOU_DATE IS NOT NULL AND SAGYOU_DATE = /*data.SagyouDate*/)
OR (SAGYOU_DATE IS NULL AND SAGYOU_DATE_BEGIN <= /*data.SagyouDate*/ AND SAGYOU_DATE_END >= /*data.SagyouDate*/))) SS
LEFT JOIN (SELECT * FROM M_GENBA WHERE DELETE_FLG = 0) GBSS
ON SS.GYOUSHA_CD = GBSS.GYOUSHA_CD AND SS.GENBA_CD = GBSS.GENBA_CD
LEFT JOIN (SELECT * FROM M_GENCHAKU_TIME WHERE DELETE_FLG = 0) GTSS
ON SS.GENCHAKU_TIME_CD = GTSS.GENCHAKU_TIME_CD
LEFT JOIN (SELECT * FROM T_HAISHA_WARIATE_DAY WHERE DELETE_FLG = 0 AND SAGYOU_DATE = /*data.SagyouDate*/) WDSS
ON SS.SHARYOU_CD = WDSS.SHARYOU_CD
AND ((WDSS.SHUBETSU_KBN_01 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_01 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_01)
OR (WDSS.SHUBETSU_KBN_02 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_02 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_02)
OR (WDSS.SHUBETSU_KBN_03 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_03 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_03)
OR (WDSS.SHUBETSU_KBN_04 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_04 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_04)
OR (WDSS.SHUBETSU_KBN_05 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_05 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_05)
OR (WDSS.SHUBETSU_KBN_06 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_06 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_06)
OR (WDSS.SHUBETSU_KBN_07 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_07 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_07)
OR (WDSS.SHUBETSU_KBN_08 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_08 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_08)
OR (WDSS.SHUBETSU_KBN_09 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_09 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_09)
OR (WDSS.SHUBETSU_KBN_10 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_10 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_10)
OR (WDSS.SHUBETSU_KBN_11 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_11 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_11)
OR (WDSS.SHUBETSU_KBN_12 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_12 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_12)
OR (WDSS.SHUBETSU_KBN_13 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_13 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_13)
OR (WDSS.SHUBETSU_KBN_14 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_14 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_14)
OR (WDSS.SHUBETSU_KBN_15 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_15 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_15)
OR (WDSS.SHUBETSU_KBN_16 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_16 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_16)
OR (WDSS.SHUBETSU_KBN_17 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_17 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_17)
OR (WDSS.SHUBETSU_KBN_18 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_18 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_18)
OR (WDSS.SHUBETSU_KBN_19 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_19 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_19)
OR (WDSS.SHUBETSU_KBN_20 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_20 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_20)
OR (WDSS.SHUBETSU_KBN_21 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_21 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_21)
OR (WDSS.SHUBETSU_KBN_22 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_22 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_22)
OR (WDSS.SHUBETSU_KBN_23 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_23 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_23)
OR (WDSS.SHUBETSU_KBN_24 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_24 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_24)
OR (WDSS.SHUBETSU_KBN_25 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_25 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_25)
OR (WDSS.SHUBETSU_KBN_26 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_26 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_26)
OR (WDSS.SHUBETSU_KBN_27 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_27 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_27)
OR (WDSS.SHUBETSU_KBN_28 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_28 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_28)
OR (WDSS.SHUBETSU_KBN_29 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_29 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_29)
OR (WDSS.SHUBETSU_KBN_30 = 1 AND SS.SYSTEM_ID = WDSS.SYSTEM_ID_30 AND SS.UKETSUKE_NUMBER = WDSS.DENPYOU_NUM_30)
)

UNION ALL

SELECT CAST(2 AS smallint) SHUBETSU_KBN_
, SK.SYSTEM_ID SYSTEM_ID_
, SK.UKETSUKE_NUMBER DENPYOU_NUM_
, SK.SEQ
, SK.HAISHA_JOKYO_CD AS HAISHA_JOKYO
, CASE SK.HAISHA_SHURUI_CD WHEN 2 THEN '仮' WHEN 3 THEN '確' ELSE '' END AS HAISHA_SHURUI
, CASE WHEN SK.SAGYOU_DATE_BEGIN IS NULL AND SK.SAGYOU_DATE_END IS NULL THEN '' WHEN SK.SAGYOU_DATE_END <> SK.SAGYOU_DATE THEN '期間' ELSE '期間終了' END AS SAGYOUDATE_KUBUN
, ISNULL(SK.GENCHAKU_TIME_NAME,'') + ISNULL(LEFT(CONVERT(varchar, SK.GENCHAKU_TIME, 114), 5),'') AS GENCHAKU_JIKAN
, GTSK.GENCHAKU_BACK_COLOR
, CASE SK.HAISHA_SIJISHO_FLG WHEN 0 THEN '未印刷' WHEN 1 THEN '印刷済' ELSE '' END AS HAISHA_SIJISHO_STATUS
, CAST(0 AS bit) HAISHA_SIJISHO_CHECKED
, CASE SK.MAIL_SEND_FLG WHEN 0 THEN '未送信' WHEN 1 THEN '送信済'  ELSE '' END AS MAIL_SEND_STATUS
, CAST(0 AS bit) MAIL_SEND_CHECKED
, ISNULL(SK.GYOUSHA_NAME,'') + CHAR(13) + CHAR(10) + ISNULL(SK.GENBA_NAME,'') + CHAR(13) + CHAR(10) + ISNULL(GBSK.GENBA_ADDRESS1,'') AS DENPYOU_CONTENT
, CAST(0 AS bit) KARADENPYOU_FLG_
, ISNULL(GTSK.GENCHAKU_PRIORITY, 0) SORT_KEY1_
, RIGHT(CONVERT(varchar, SK.GENCHAKU_TIME, 120), 8) SORT_KEY2_
, ISNULL(SK.HAISHA_SHURUI_CD, 0) SORT_KEY3_
, SK.GYOUSHA_CD SORT_KEY4_
, SK.GENBA_CD SORT_KEY5_
, ISNULL(SK.UKETSUKE_NUMBER, 0) SORT_KEY6_
, CASE WHEN WDSK.SHUBETSU_KBN_01 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_01 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_01 THEN 1
  WHEN WDSK.SHUBETSU_KBN_02 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_02 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_02 THEN 2
  WHEN WDSK.SHUBETSU_KBN_03 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_03 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_03 THEN 3
  WHEN WDSK.SHUBETSU_KBN_04 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_04 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_04 THEN 4
  WHEN WDSK.SHUBETSU_KBN_05 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_05 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_05 THEN 5
  WHEN WDSK.SHUBETSU_KBN_06 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_06 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_06 THEN 6
  WHEN WDSK.SHUBETSU_KBN_07 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_07 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_07 THEN 7
  WHEN WDSK.SHUBETSU_KBN_08 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_08 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_08 THEN 8
  WHEN WDSK.SHUBETSU_KBN_09 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_09 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_09 THEN 9
  WHEN WDSK.SHUBETSU_KBN_10 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_10 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_10 THEN 10
  WHEN WDSK.SHUBETSU_KBN_11 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_11 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_11 THEN 11
  WHEN WDSK.SHUBETSU_KBN_12 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_12 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_12 THEN 12
  WHEN WDSK.SHUBETSU_KBN_13 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_13 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_13 THEN 13
  WHEN WDSK.SHUBETSU_KBN_14 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_14 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_14 THEN 14
  WHEN WDSK.SHUBETSU_KBN_15 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_15 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_15 THEN 15
  WHEN WDSK.SHUBETSU_KBN_16 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_16 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_16 THEN 16
  WHEN WDSK.SHUBETSU_KBN_17 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_17 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_17 THEN 17
  WHEN WDSK.SHUBETSU_KBN_18 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_18 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_18 THEN 18
  WHEN WDSK.SHUBETSU_KBN_19 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_19 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_19 THEN 19
  WHEN WDSK.SHUBETSU_KBN_20 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_20 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_20 THEN 20
  WHEN WDSK.SHUBETSU_KBN_21 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_21 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_21 THEN 21
  WHEN WDSK.SHUBETSU_KBN_22 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_22 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_22 THEN 22
  WHEN WDSK.SHUBETSU_KBN_23 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_23 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_23 THEN 23
  WHEN WDSK.SHUBETSU_KBN_24 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_24 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_24 THEN 24
  WHEN WDSK.SHUBETSU_KBN_25 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_25 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_25 THEN 25
  WHEN WDSK.SHUBETSU_KBN_26 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_26 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_26 THEN 26
  WHEN WDSK.SHUBETSU_KBN_27 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_27 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_27 THEN 27
  WHEN WDSK.SHUBETSU_KBN_28 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_28 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_28 THEN 28
  WHEN WDSK.SHUBETSU_KBN_29 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_29 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_29 THEN 29
  WHEN WDSK.SHUBETSU_KBN_30 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_30 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_30 THEN 30
  ELSE 31 END AS DENPYOU_SEQ
, 0 ROW_NUM
, SK.SHASHU_CD
, SK.SHARYOU_CD
, SK.UNTENSHA_CD
, ISNULL(SK.UNPAN_GYOUSHA_CD,'') AS UNPAN_GYOUSHA_CD
, (CASE WHEN WDSK.SAGYOU_DATE IS NULL THEN 1 ELSE 0 END) AS HAISHA_FLG
, 0 AS DENSHU_TYPE
FROM (SELECT * FROM T_UKETSUKE_SK_ENTRY
WHERE DELETE_FLG = 0 AND COURSE_KUMIKOMI_CD = 1
AND SHARYOU_CD IS NOT NULL AND SHARYOU_CD <> ''
/*IF data.KyotenCd != null && data.KyotenCd != '' && data.KyotenCd != 99*/
AND (KYOTEN_CD = /*data.KyotenCd*/ OR KYOTEN_CD = 99)
/*END*/
AND ((SAGYOU_DATE IS NOT NULL AND SAGYOU_DATE = /*data.SagyouDate*/)
OR (SAGYOU_DATE IS NULL AND SAGYOU_DATE_BEGIN <= /*data.SagyouDate*/ AND SAGYOU_DATE_END >= /*data.SagyouDate*/))) SK
LEFT JOIN (SELECT * FROM M_GENBA WHERE DELETE_FLG = 0) GBSK
ON SK.GYOUSHA_CD = GBSK.GYOUSHA_CD AND SK.GENBA_CD = GBSK.GENBA_CD
LEFT JOIN (SELECT * FROM M_GENCHAKU_TIME WHERE DELETE_FLG = 0) GTSK
ON SK.GENCHAKU_TIME_CD = GTSK.GENCHAKU_TIME_CD
LEFT JOIN (SELECT * FROM T_HAISHA_WARIATE_DAY WHERE DELETE_FLG = 0 AND SAGYOU_DATE = /*data.SagyouDate*/) WDSK
ON SK.SHARYOU_CD = WDSK.SHARYOU_CD
AND ((WDSK.SHUBETSU_KBN_01 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_01 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_01)
OR (WDSK.SHUBETSU_KBN_02 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_02 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_02)
OR (WDSK.SHUBETSU_KBN_03 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_03 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_03)
OR (WDSK.SHUBETSU_KBN_04 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_04 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_04)
OR (WDSK.SHUBETSU_KBN_05 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_05 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_05)
OR (WDSK.SHUBETSU_KBN_06 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_06 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_06)
OR (WDSK.SHUBETSU_KBN_07 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_07 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_07)
OR (WDSK.SHUBETSU_KBN_08 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_08 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_08)
OR (WDSK.SHUBETSU_KBN_09 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_09 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_09)
OR (WDSK.SHUBETSU_KBN_10 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_10 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_10)
OR (WDSK.SHUBETSU_KBN_11 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_11 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_11)
OR (WDSK.SHUBETSU_KBN_12 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_12 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_12)
OR (WDSK.SHUBETSU_KBN_13 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_13 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_13)
OR (WDSK.SHUBETSU_KBN_14 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_14 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_14)
OR (WDSK.SHUBETSU_KBN_15 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_15 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_15)
OR (WDSK.SHUBETSU_KBN_16 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_16 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_16)
OR (WDSK.SHUBETSU_KBN_17 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_17 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_17)
OR (WDSK.SHUBETSU_KBN_18 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_18 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_18)
OR (WDSK.SHUBETSU_KBN_19 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_19 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_19)
OR (WDSK.SHUBETSU_KBN_20 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_20 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_20)
OR (WDSK.SHUBETSU_KBN_21 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_21 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_21)
OR (WDSK.SHUBETSU_KBN_22 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_22 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_22)
OR (WDSK.SHUBETSU_KBN_23 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_23 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_23)
OR (WDSK.SHUBETSU_KBN_24 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_24 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_24)
OR (WDSK.SHUBETSU_KBN_25 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_25 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_25)
OR (WDSK.SHUBETSU_KBN_26 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_26 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_26)
OR (WDSK.SHUBETSU_KBN_27 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_27 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_27)
OR (WDSK.SHUBETSU_KBN_28 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_28 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_28)
OR (WDSK.SHUBETSU_KBN_29 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_29 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_29)
OR (WDSK.SHUBETSU_KBN_30 = 2 AND SK.SYSTEM_ID = WDSK.SYSTEM_ID_30 AND SK.UKETSUKE_NUMBER = WDSK.DENPYOU_NUM_30)
)

UNION ALL

SELECT CAST(3 AS smallint) SHUBETSU_KBN_
, TK.SYSTEM_ID SYSTEM_ID_
, TK.TEIKI_HAISHA_NUMBER DENPYOU_NUM_
, TK.SEQ
, '' AS HAISHA_JOKYO
, '' AS HAISHA_SHURUI
, '' AS SAGYOUDATE_KUBUN
, REPLACE(STR(TK.SAGYOU_BEGIN_HOUR, 2, 0), ' ', '0') + ':'
+ REPLACE(STR(TK.SAGYOU_BEGIN_MINUTE, 2, 0), ' ', '0') + '～'
+ REPLACE(STR(TK.SAGYOU_END_HOUR, 2, 0), ' ', '0') + ':'
+ REPLACE(STR(TK.SAGYOU_END_MINUTE, 2, 0), ' ', '0') AS GENCHAKU_JIKAN
, NULL GENCHAKU_BACK_COLOR
, '' AS HAISHA_SIJISHO_STATUS
, CAST(0 AS bit) HAISHA_SIJISHO_CHECKED
, '' AS MAIL_SEND_STATUS
, CAST(0 AS bit) MAIL_SEND_CHECKED
, CN.COURSE_NAME_RYAKU AS DENPYOU_CONTENT
, CAST(0 AS bit) KARADENPYOU_FLG_
, POWER(2, 16) - 1 SORT_KEY1_
, REPLACE(STR(TK.SAGYOU_BEGIN_HOUR, 2, 0), ' ', '0') + ':'
+ REPLACE(STR(TK.SAGYOU_BEGIN_MINUTE, 2, 0), ' ', '0') + ':00' AS SORT_KEY2_
, 0 SORT_KEY3_
, '' SORT_KEY4_
, '' SORT_KEY5_
, TK.TEIKI_HAISHA_NUMBER SORT_KEY6_
, CASE WHEN WDTK.SHUBETSU_KBN_01 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_01 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_01 THEN 1
  WHEN WDTK.SHUBETSU_KBN_02 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_02 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_02 THEN 2
  WHEN WDTK.SHUBETSU_KBN_03 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_03 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_03 THEN 3
  WHEN WDTK.SHUBETSU_KBN_04 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_04 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_04 THEN 4
  WHEN WDTK.SHUBETSU_KBN_05 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_05 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_05 THEN 5
  WHEN WDTK.SHUBETSU_KBN_06 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_06 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_06 THEN 6
  WHEN WDTK.SHUBETSU_KBN_07 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_07 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_07 THEN 7
  WHEN WDTK.SHUBETSU_KBN_08 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_08 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_08 THEN 8
  WHEN WDTK.SHUBETSU_KBN_09 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_09 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_09 THEN 9
  WHEN WDTK.SHUBETSU_KBN_10 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_10 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_10 THEN 10
  WHEN WDTK.SHUBETSU_KBN_11 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_11 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_11 THEN 11
  WHEN WDTK.SHUBETSU_KBN_12 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_12 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_12 THEN 12
  WHEN WDTK.SHUBETSU_KBN_13 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_13 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_13 THEN 13
  WHEN WDTK.SHUBETSU_KBN_14 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_14 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_14 THEN 14
  WHEN WDTK.SHUBETSU_KBN_15 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_15 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_15 THEN 15
  WHEN WDTK.SHUBETSU_KBN_16 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_16 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_16 THEN 16
  WHEN WDTK.SHUBETSU_KBN_17 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_17 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_17 THEN 17
  WHEN WDTK.SHUBETSU_KBN_18 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_18 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_18 THEN 18
  WHEN WDTK.SHUBETSU_KBN_19 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_19 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_19 THEN 19
  WHEN WDTK.SHUBETSU_KBN_20 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_20 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_20 THEN 20
  WHEN WDTK.SHUBETSU_KBN_21 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_21 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_21 THEN 21
  WHEN WDTK.SHUBETSU_KBN_22 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_22 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_22 THEN 22
  WHEN WDTK.SHUBETSU_KBN_23 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_23 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_23 THEN 23
  WHEN WDTK.SHUBETSU_KBN_24 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_24 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_24 THEN 24
  WHEN WDTK.SHUBETSU_KBN_25 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_25 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_25 THEN 25
  WHEN WDTK.SHUBETSU_KBN_26 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_26 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_26 THEN 26
  WHEN WDTK.SHUBETSU_KBN_27 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_27 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_27 THEN 27
  WHEN WDTK.SHUBETSU_KBN_28 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_28 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_28 THEN 28
  WHEN WDTK.SHUBETSU_KBN_29 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_29 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_29 THEN 29
  WHEN WDTK.SHUBETSU_KBN_30 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_30 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_30 THEN 30
  ELSE 31 END AS DENPYOU_SEQ
, 0 ROW_NUM
, TK.SHASHU_CD
, TK.SHARYOU_CD
, TK.UNTENSHA_CD
, ISNULL(TK.UNPAN_GYOUSHA_CD,'') AS UNPAN_GYOUSHA_CD
, (CASE WHEN WDTK.SAGYOU_DATE IS NULL THEN 1 ELSE 0 END) AS HAISHA_FLG
, 1 AS DENSHU_TYPE
FROM (SELECT * FROM T_TEIKI_HAISHA_ENTRY
WHERE DELETE_FLG = 0
AND SHARYOU_CD IS NOT NULL AND SHARYOU_CD <> ''
/*IF data.KyotenCd != null && data.KyotenCd != '' && data.KyotenCd != 99*/
AND (KYOTEN_CD = /*data.KyotenCd*/ OR KYOTEN_CD = 99)
/*END*/
AND SAGYOU_DATE = /*data.SagyouDate*/) TK
LEFT JOIN (SELECT * FROM M_COURSE_NAME WHERE DELETE_FLG = 0) CN
ON TK.COURSE_NAME_CD = CN.COURSE_NAME_CD
LEFT JOIN (SELECT * FROM T_HAISHA_WARIATE_DAY WHERE DELETE_FLG = 0 AND SAGYOU_DATE = /*data.SagyouDate*/) WDTK
ON TK.SHARYOU_CD = WDTK.SHARYOU_CD
AND ((WDTK.SHUBETSU_KBN_01 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_01 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_01)
OR (WDTK.SHUBETSU_KBN_02 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_02 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_02)
OR (WDTK.SHUBETSU_KBN_03 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_03 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_03)
OR (WDTK.SHUBETSU_KBN_04 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_04 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_04)
OR (WDTK.SHUBETSU_KBN_05 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_05 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_05)
OR (WDTK.SHUBETSU_KBN_06 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_06 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_06)
OR (WDTK.SHUBETSU_KBN_07 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_07 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_07)
OR (WDTK.SHUBETSU_KBN_08 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_08 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_08)
OR (WDTK.SHUBETSU_KBN_09 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_09 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_09)
OR (WDTK.SHUBETSU_KBN_10 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_10 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_10)
OR (WDTK.SHUBETSU_KBN_11 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_11 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_11)
OR (WDTK.SHUBETSU_KBN_12 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_12 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_12)
OR (WDTK.SHUBETSU_KBN_13 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_13 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_13)
OR (WDTK.SHUBETSU_KBN_14 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_14 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_14)
OR (WDTK.SHUBETSU_KBN_15 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_15 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_15)
OR (WDTK.SHUBETSU_KBN_16 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_16 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_16)
OR (WDTK.SHUBETSU_KBN_17 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_17 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_17)
OR (WDTK.SHUBETSU_KBN_18 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_18 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_18)
OR (WDTK.SHUBETSU_KBN_19 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_19 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_19)
OR (WDTK.SHUBETSU_KBN_20 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_20 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_20)
OR (WDTK.SHUBETSU_KBN_21 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_21 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_21)
OR (WDTK.SHUBETSU_KBN_22 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_22 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_22)
OR (WDTK.SHUBETSU_KBN_23 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_23 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_23)
OR (WDTK.SHUBETSU_KBN_24 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_24 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_24)
OR (WDTK.SHUBETSU_KBN_25 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_25 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_25)
OR (WDTK.SHUBETSU_KBN_26 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_26 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_26)
OR (WDTK.SHUBETSU_KBN_27 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_27 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_27)
OR (WDTK.SHUBETSU_KBN_28 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_28 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_28)
OR (WDTK.SHUBETSU_KBN_29 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_29 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_29)
OR (WDTK.SHUBETSU_KBN_30 = 3 AND TK.SYSTEM_ID = WDTK.SYSTEM_ID_30 AND TK.TEIKI_HAISHA_NUMBER = WDTK.DENPYOU_NUM_30)
)

ORDER BY SHARYOU_CD, DENPYOU_SEQ