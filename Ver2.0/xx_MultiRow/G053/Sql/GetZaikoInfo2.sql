SELECT
  SYSTEM_ID,
  SEQ,
  DETAIL_SYSTEM_ID,
  DENSHU_KBN_CD,
  ZAIKO_HINMEI_CD,
  ZAIKO_HINMEI_NAME,
  ZAIKO_HIRITSU,
  ZAIKO_RYOU,
  ZAIKO_TANKA,
  ZAIKO_KINGAKU,
  TIME_STAMP
FROM
  T_ZAIKO_HINMEI_HURIWAKE
WHERE
  SYSTEM_ID = /*data.SYSTEM_ID*/'' AND
  SEQ = /*data.SEQ*/'' AND
  DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID*/'' AND
  DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/''