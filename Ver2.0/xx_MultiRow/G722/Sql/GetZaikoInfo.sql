SELECT
  TZSD.DENSHU_KBN_CD,
  TZSD.SYSTEM_ID,
  TZSD.SEQ,
  TZSD.DETAIL_SYSTEM_ID,
  TZSD.ROW_NO,
  TZSD.GYOUSHA_CD,
  TZSD.GENBA_CD,
  TZSD.ZAIKO_HINMEI_CD,
  TZSD.ZAIKO_RITSU,
  TZSD.JYUURYOU,
  TZSD.TANKA,
  TZSD.KINGAKU,
  TZSD.TIME_STAMP,
  TZSD.DELETE_FLG
FROM
  T_ZAIKO_SHUKKA_DETAIL AS TZSD
WHERE
  TZSD.SYSTEM_ID = /*data.SYSTEM_ID*/'' AND
  TZSD.DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID*/'' AND
  TZSD.SEQ = /*data.SEQ*/'' AND
  TZSD.DELETE_FLG = 0
ORDER BY
  TZSD.ROW_NO