﻿SELECT
    GENBA.GYOUSHA_CD,
    GENBA.GENBA_CD,
    GENBA.GENBA_NAME_RYAKU
FROM
    M_GENBA AS GENBA
    INNER JOIN
        M_GYOUSHA AS GYOUSHA
    ON  GYOUSHA.GYOUSHA_CD = GENBA.GYOUSHA_CD
    AND GYOUSHA.GYOUSHAKBN_MANI = 1
    AND (
            GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = 1
        OR  GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = 1
        OR  GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = 1
        )
    /*IF data.DELETE_FLG == false*/
    AND GYOUSHA.DELETE_FLG = 0
    /*END*/
    LEFT JOIN M_CHIIKI CHIIKI
	    ON GENBA.CHIIKI_CD = CHIIKI.CHIIKI_CD
	   AND CHIIKI.DELETE_FLG = 0
WHERE 1 = 1
   /*IF data.DELETE_FLG == false*/
   AND GENBA.DELETE_FLG = 0
   /*END*/
   AND ISNULL(CHIIKI.TODOUFUKEN_CD, '') != ISNULL(GENBA.GENBA_TODOUFUKEN_CD, '')