﻿SELECT
  KOUMOKU_RONRI_NAME,KOUMOKU_BUTSURI_NAME
FROM
  M_OUTPUT_PATTERN_COLUMN 
WHERE
SYSTEM_ID = /*data.systemId*/
order by DETAIL_SYSTEM_ID

