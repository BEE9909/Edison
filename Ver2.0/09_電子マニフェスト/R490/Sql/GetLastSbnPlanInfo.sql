﻿--最終処分終了日・事業場情報の取得
SELECT CASE WHEN T1.LAST_SBN_JOU_ADDRESS = '' THEN ''
            ELSE T1.LAST_SBN_JOU_ADDRESS
            END  +
       CASE WHEN T1.LAST_SBN_JOU_NAME = '' AND T1.LAST_SBN_JOU_TEL = '' THEN ''
            WHEN T1.LAST_SBN_JOU_NAME = '' AND T1.LAST_SBN_JOU_TEL <> '' THEN '[' + T1.LAST_SBN_JOU_TEL + ']'
            WHEN T1.LAST_SBN_JOU_NAME <> '' AND T1.LAST_SBN_JOU_TEL = '' THEN '（' + T1.LAST_SBN_JOU_NAME + '）'
            WHEN T1.LAST_SBN_JOU_NAME <> '' AND T1.LAST_SBN_JOU_TEL <> ''
                    THEN '（' + T1.LAST_SBN_JOU_NAME + '[' + T1.LAST_SBN_JOU_TEL + ']' + '）'
            ELSE ''
            END AS LAST_SBN_JOU_YOTEI
FROM
(
SELECT  DT_R04.REC_SEQ AS REC_SEQ,              --並び順
        CASE WHEN DT_R04.LAST_SBN_JOU_ADDRESS1 IS NULL THEN ''
            ELSE DT_R04.LAST_SBN_JOU_ADDRESS1
            END +
        CASE WHEN DT_R04.LAST_SBN_JOU_ADDRESS2 IS NULL THEN ''
            ELSE DT_R04.LAST_SBN_JOU_ADDRESS2
            END AS LAST_SBN_JOU_ADDRESS,        --所在地
        CASE WHEN DT_R04.LAST_SBN_JOU_TEL IS NULL THEN ''
            ELSE DT_R04.LAST_SBN_JOU_TEL
            END AS LAST_SBN_JOU_TEL,            --電話番号
        CASE WHEN DT_R04.LAST_SBN_JOU_NAME IS NULL THEN ''
            ELSE DT_R04.LAST_SBN_JOU_NAME
            END AS LAST_SBN_JOU_NAME            --名称
FROM DT_MF_TOC
LEFT JOIN DT_R04 ON DT_MF_TOC.KANRI_ID = DT_R04.KANRI_ID
                 AND DT_MF_TOC.LATEST_SEQ = DT_R04.SEQ
WHERE (DT_MF_TOC.KIND = 4 OR DT_MF_TOC.KIND = 5 OR DT_MF_TOC.KIND IS NULL)
  AND DT_MF_TOC.KANRI_ID = /*data.kanriId*/
  AND DT_MF_TOC.STATUS_FLAG <> 9
) T1
ORDER BY T1.REC_SEQ
