﻿SELECT * FROM dbo.M_OUTPUT_PATTERN_COLUMN_INXS
WHERE 
SYSTEM_ID = /*data.SYSTEM_ID.Value*/
AND SEQ = /*data.SEQ.Value*/
/*IF !data.DETAIL_SYSTEM_ID.IsNull*/AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID.Value*//*END*/
/*IF data.TABLE_NAME != null*/AND TABLE_NAME = /*data.TABLE_NAME*//*END*/
/*IF data.KOUMOKU_RONRI_NAME != null*/AND KOUMOKU_RONRI_NAME = /*data.KOUMOKU_RONRI_NAME*//*END*/
/*IF data.KOUMOKU_BUTSURI_NAME != null*/AND KOUMOKU_BUTSURI_NAME = /*data.KOUMOKU_BUTSURI_NAME*//*END*/
/*IF !data.OUTPUT_KBN.IsNull*/AND OUTPUT_KBN = /*data.KOUMOKU_BUTSURI_NAME*//*END*/
/*IF !data.KOUMOKU_ID.IsNull*/AND KOUMOKU_ID = /*data.KOUMOKU_BUTSURI_NAME*//*END*/
/*IF !data.SORT_NO.IsNull*/AND SORT_NO = /*data.SORT_NO.Value*//*END*/
/*IF !data.PRIORITY_NO.IsNull*/AND PRIORITY_NO = /*data.PRIORITY_NO.Value*//*END*/
order by PRIORITY_NO ASC