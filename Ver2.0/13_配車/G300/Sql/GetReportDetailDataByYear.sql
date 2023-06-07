﻿SELECT
temp.UNIT_ORDER,
temp.SAGYOU_DATE,
temp.GYOUSHA_CD,
temp.GENBA_CD,
temp.HINMEI_CD,
temp.HINMEI_NAME,
temp.UNIT_NAME_RYAKU,
temp.TSUKIGIME_KBN,
temp.UNIT_CD,
CONVERT(DECIMAL(13,3),COALESCE(SUM(temp.SUURYOU),0)) AS Expr1,
temp.GENBA_NAME,
temp.GYOUSHA_NAME

FROM
(
/*IF data.SHUUKEISUURYOU == 1 || data.SHUUKEISUURYOU == 4*/
SELECT
/*IF data.SHUUKEISUURYOU == 1*/
CASE WHEN T_TEIKI_JISSEKI_DETAIL.UNIT_CD = 3 THEN 1
     ELSE 0
END
--ELSE
1 AS
/*END*/
 UNIT_ORDER,
T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD,
T_TEIKI_JISSEKI_DETAIL.GENBA_CD,
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD,
CASE WHEN MKH1.HINMEI_CD IS NOT NULL
     THEN MKH1.SEIKYUU_HINMEI_NAME
	 WHEN MKH2.HINMEI_CD IS NOT NULL
	 THEN MKH2.SEIKYUU_HINMEI_NAME
	 ELSE M_HINMEI.HINMEI_NAME
	 END AS HINMEI_NAME,
M_UNIT.UNIT_NAME_RYAKU,
T_TEIKI_JISSEKI_DETAIL.TSUKIGIME_KBN,
T_TEIKI_JISSEKI_DETAIL.UNIT_CD,
T_TEIKI_JISSEKI_DETAIL.SUURYOU,
M_GENBA.GENBA_NAME1 + '　' + M_GENBA.GENBA_NAME2 AS GENBA_NAME,
M_GYOUSHA.GYOUSHA_NAME1 + '　' + M_GYOUSHA.GYOUSHA_NAME2 AS GYOUSHA_NAME

FROM
T_TEIKI_JISSEKI_ENTRY
INNER JOIN T_TEIKI_JISSEKI_DETAIL ON
T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID = T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID AND
T_TEIKI_JISSEKI_ENTRY.SEQ = T_TEIKI_JISSEKI_DETAIL.SEQ
INNER JOIN M_UNIT ON T_TEIKI_JISSEKI_DETAIL.UNIT_CD = M_UNIT.UNIT_CD
INNER JOIN M_HINMEI ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD
LEFT JOIN M_KOBETSU_HINMEI MKH1 ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = MKH1.HINMEI_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = MKH1.GYOUSHA_CD AND
T_TEIKI_JISSEKI_DETAIL.GENBA_CD = MKH1.GENBA_CD AND
MKH1.DELETE_FLG = 0
LEFT JOIN M_KOBETSU_HINMEI MKH2 ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = MKH2.HINMEI_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = MKH2.GYOUSHA_CD AND
MKH2.GENBA_CD = '' AND
MKH2.DELETE_FLG = 0
LEFT JOIN M_GENBA ON
T_TEIKI_JISSEKI_DETAIL.GENBA_CD = M_GENBA.GENBA_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = M_GENBA.GYOUSHA_CD
LEFT JOIN M_GYOUSHA ON
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD

WHERE
T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 AND
T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE IS NOT NULL AND
T_TEIKI_JISSEKI_DETAIL.SUURYOU IS NOT NULL

/*IF data.KyotenCD != null*/AND T_TEIKI_JISSEKI_ENTRY.KYOTEN_CD = /*data.KyotenCD*//*END*/
/*IF data.DENPYOU_DATE_FROM != null*/AND CONVERT(varchar,T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,111) >= CONVERT(varchar,/*data.DENPYOU_DATE_FROM*/,111)/*END*/
/*IF data.dtp_KikanTO != null*/AND CONVERT(varchar,T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,111) <= CONVERT(varchar,/*data.dtp_KikanTO*/,111)/*END*/
/*IF data.TORIHIKISAKI_CD_FROM != null*/AND M_GENBA.TORIHIKISAKI_CD >= /*data.TORIHIKISAKI_CD_FROM*//*END*/
/*IF data.TORIHIKISAKI_CD_TO != null*/AND M_GENBA.TORIHIKISAKI_CD <= /*data.TORIHIKISAKI_CD_TO*//*END*/
/*IF data.GYOUSHA_CD_FROM != null*/AND T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD >= /*data.GYOUSHA_CD_FROM*//*END*/
/*IF data.GYOUSHA_CD_TO != null*/AND T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD <= /*data.GYOUSHA_CD_TO*//*END*/
/*IF data.GENBA_CD_FROM != null*/AND T_TEIKI_JISSEKI_DETAIL.GENBA_CD >= /*data.GENBA_CD_FROM*//*END*/
/*IF data.GENBA_CD_TO != null*/AND T_TEIKI_JISSEKI_DETAIL.GENBA_CD <= /*data.GENBA_CD_TO*//*END*/
/*IF data.SHURUI_CD_FROM != null*/AND M_HINMEI.SHURUI_CD >= /*data.SHURUI_CD_FROM*//*END*/
/*IF data.SHURUI_CD_TO != null*/AND M_HINMEI.SHURUI_CD <= /*data.SHURUI_CD_TO*//*END*/

/*END*/

/*IF data.SHUUKEISUURYOU == 4*/
UNION ALL
/*END*/

/*IF data.SHUUKEISUURYOU == 2 || data.SHUUKEISUURYOU == 4*/
SELECT
1 AS UNIT_ORDER,
T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD,
T_TEIKI_JISSEKI_DETAIL.GENBA_CD,
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD,
CASE WHEN MKH1.HINMEI_CD IS NOT NULL
     THEN MKH1.SEIKYUU_HINMEI_NAME
	 WHEN MKH2.HINMEI_CD IS NOT NULL
	 THEN MKH2.SEIKYUU_HINMEI_NAME
	 ELSE M_HINMEI.HINMEI_NAME
	 END AS HINMEI_NAME,
M_UNIT.UNIT_NAME_RYAKU,
T_TEIKI_JISSEKI_DETAIL.TSUKIGIME_KBN,
T_TEIKI_JISSEKI_DETAIL.KANSAN_UNIT_CD AS UNIT_CD,
T_TEIKI_JISSEKI_DETAIL.KANSAN_SUURYOU AS SUURYOU,
M_GENBA.GENBA_NAME1 + '　' + M_GENBA.GENBA_NAME2 AS GENBA_NAME,
M_GYOUSHA.GYOUSHA_NAME1 + '　' + M_GYOUSHA.GYOUSHA_NAME2 AS GYOUSHA_NAME

FROM
T_TEIKI_JISSEKI_ENTRY
INNER JOIN T_TEIKI_JISSEKI_DETAIL ON
T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID = T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID AND
T_TEIKI_JISSEKI_ENTRY.SEQ = T_TEIKI_JISSEKI_DETAIL.SEQ
INNER JOIN M_UNIT ON T_TEIKI_JISSEKI_DETAIL.KANSAN_UNIT_CD = M_UNIT.UNIT_CD
INNER JOIN M_HINMEI ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD
LEFT JOIN M_KOBETSU_HINMEI MKH1 ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = MKH1.HINMEI_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = MKH1.GYOUSHA_CD AND
T_TEIKI_JISSEKI_DETAIL.GENBA_CD = MKH1.GENBA_CD AND
MKH1.DELETE_FLG = 0
LEFT JOIN M_KOBETSU_HINMEI MKH2 ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = MKH2.HINMEI_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = MKH2.GYOUSHA_CD AND
MKH2.GENBA_CD = '' AND
MKH2.DELETE_FLG = 0
LEFT JOIN M_GENBA ON
T_TEIKI_JISSEKI_DETAIL.GENBA_CD = M_GENBA.GENBA_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = M_GENBA.GYOUSHA_CD
LEFT JOIN M_GYOUSHA ON
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD

WHERE
T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 AND
T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE IS NOT NULL AND
T_TEIKI_JISSEKI_DETAIL.SUURYOU IS NOT NULL

/*IF data.KyotenCD != null*/AND T_TEIKI_JISSEKI_ENTRY.KYOTEN_CD = /*data.KyotenCD*//*END*/
/*IF data.DENPYOU_DATE_FROM != null*/AND CONVERT(varchar,T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,111) >= CONVERT(varchar,/*data.DENPYOU_DATE_FROM*/,111)/*END*/
/*IF data.dtp_KikanTO != null*/AND CONVERT(varchar,T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,111) <= CONVERT(varchar,/*data.dtp_KikanTO*/,111)/*END*/
/*IF data.TORIHIKISAKI_CD_FROM != null*/AND M_GENBA.TORIHIKISAKI_CD >= /*data.TORIHIKISAKI_CD_FROM*//*END*/
/*IF data.TORIHIKISAKI_CD_TO != null*/AND M_GENBA.TORIHIKISAKI_CD <= /*data.TORIHIKISAKI_CD_TO*//*END*/
/*IF data.GYOUSHA_CD_FROM != null*/AND T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD >= /*data.GYOUSHA_CD_FROM*//*END*/
/*IF data.GYOUSHA_CD_TO != null*/AND T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD <= /*data.GYOUSHA_CD_TO*//*END*/
/*IF data.GENBA_CD_FROM != null*/AND T_TEIKI_JISSEKI_DETAIL.GENBA_CD >= /*data.GENBA_CD_FROM*//*END*/
/*IF data.GENBA_CD_TO != null*/AND T_TEIKI_JISSEKI_DETAIL.GENBA_CD <= /*data.GENBA_CD_TO*//*END*/
/*IF data.SHURUI_CD_FROM != null*/AND M_HINMEI.SHURUI_CD >= /*data.SHURUI_CD_FROM*//*END*/
/*IF data.SHURUI_CD_TO != null*/AND M_HINMEI.SHURUI_CD <= /*data.SHURUI_CD_TO*//*END*/

/*END*/

/*IF data.SHUUKEISUURYOU == 3*/
SELECT
0 AS UNIT_ORDER,
T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD,
T_TEIKI_JISSEKI_DETAIL.GENBA_CD,
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD,
CASE WHEN MKH1.HINMEI_CD IS NOT NULL
     THEN MKH1.SEIKYUU_HINMEI_NAME
	 WHEN MKH2.HINMEI_CD IS NOT NULL
	 THEN MKH2.SEIKYUU_HINMEI_NAME
	 ELSE M_HINMEI.HINMEI_NAME
	 END AS HINMEI_NAME,
M_UNIT.UNIT_NAME_RYAKU,
T_TEIKI_JISSEKI_DETAIL.TSUKIGIME_KBN,
CAST(T_TEIKI_JISSEKI_DETAIL.UNIT_CD_KG AS SMALLINT) AS UNIT_CD,
T_TEIKI_JISSEKI_DETAIL.ANBUN_SUURYOU  AS SUURYOU,
M_GENBA.GENBA_NAME1 + '　' + M_GENBA.GENBA_NAME2 AS GENBA_NAME,
M_GYOUSHA.GYOUSHA_NAME1 + '　' + M_GYOUSHA.GYOUSHA_NAME2 AS GYOUSHA_NAME

FROM
T_TEIKI_JISSEKI_ENTRY
INNER JOIN (select 3 as UNIT_CD_KG,* from T_TEIKI_JISSEKI_DETAIL) AS T_TEIKI_JISSEKI_DETAIL ON
T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID = T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID AND
T_TEIKI_JISSEKI_ENTRY.SEQ = T_TEIKI_JISSEKI_DETAIL.SEQ
INNER JOIN M_UNIT ON
T_TEIKI_JISSEKI_DETAIL.UNIT_CD_KG = M_UNIT.UNIT_CD
INNER JOIN M_HINMEI ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD
LEFT JOIN M_KOBETSU_HINMEI MKH1 ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = MKH1.HINMEI_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = MKH1.GYOUSHA_CD AND
T_TEIKI_JISSEKI_DETAIL.GENBA_CD = MKH1.GENBA_CD AND
MKH1.DELETE_FLG = 0
LEFT JOIN M_KOBETSU_HINMEI MKH2 ON
T_TEIKI_JISSEKI_DETAIL.HINMEI_CD = MKH2.HINMEI_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = MKH2.GYOUSHA_CD AND
MKH2.GENBA_CD = '' AND
MKH2.DELETE_FLG = 0
LEFT JOIN M_GENBA ON
T_TEIKI_JISSEKI_DETAIL.GENBA_CD = M_GENBA.GENBA_CD AND
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = M_GENBA.GYOUSHA_CD
LEFT JOIN M_GYOUSHA ON
T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD

WHERE
T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 AND
T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE IS NOT NULL AND
T_TEIKI_JISSEKI_DETAIL.SUURYOU IS NOT NULL

/*IF data.KyotenCD != null*/AND T_TEIKI_JISSEKI_ENTRY.KYOTEN_CD = /*data.KyotenCD*//*END*/
/*IF data.DENPYOU_DATE_FROM != null*/AND CONVERT(varchar,T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,111) >= CONVERT(varchar,/*data.DENPYOU_DATE_FROM*/,111)/*END*/
/*IF data.dtp_KikanTO != null*/AND CONVERT(varchar,T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE,111) <= CONVERT(varchar,/*data.dtp_KikanTO*/,111)/*END*/
/*IF data.TORIHIKISAKI_CD_FROM != null*/AND M_GENBA.TORIHIKISAKI_CD >= /*data.TORIHIKISAKI_CD_FROM*//*END*/
/*IF data.TORIHIKISAKI_CD_TO != null*/AND M_GENBA.TORIHIKISAKI_CD <= /*data.TORIHIKISAKI_CD_TO*//*END*/
/*IF data.GYOUSHA_CD_FROM != null*/AND T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD >= /*data.GYOUSHA_CD_FROM*//*END*/
/*IF data.GYOUSHA_CD_TO != null*/AND T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD <= /*data.GYOUSHA_CD_TO*//*END*/
/*IF data.GENBA_CD_FROM != null*/AND T_TEIKI_JISSEKI_DETAIL.GENBA_CD >= /*data.GENBA_CD_FROM*//*END*/
/*IF data.GENBA_CD_TO != null*/AND T_TEIKI_JISSEKI_DETAIL.GENBA_CD <= /*data.GENBA_CD_TO*//*END*/
/*IF data.SHURUI_CD_FROM != null*/AND M_HINMEI.SHURUI_CD >= /*data.SHURUI_CD_FROM*//*END*/
/*IF data.SHURUI_CD_TO != null*/AND M_HINMEI.SHURUI_CD <= /*data.SHURUI_CD_TO*//*END*/

/*END*/
) temp

GROUP BY
MONTH(SAGYOU_DATE),
UNIT_ORDER,
SAGYOU_DATE,
GYOUSHA_CD,
GENBA_CD,
HINMEI_CD,
HINMEI_NAME,
UNIT_NAME_RYAKU,
TSUKIGIME_KBN,
UNIT_CD,
GENBA_NAME,
GYOUSHA_NAME

ORDER BY
GYOUSHA_CD,
GENBA_CD,
SAGYOU_DATE,
UNIT_ORDER,
HINMEI_CD,
UNIT_CD
