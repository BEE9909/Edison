﻿SELECT 
TSD.TORIHIKISAKI_CD,
	MT.TORIHIKISAKI_NAME_RYAKU,
	TSDK.GYOUSHA_CD,
	MGY.GYOUSHA_NAME_RYAKU,
	TSDK.GENBA_CD,
	MGE.GENBA_NAME_RYAKU,
	MT.EIGYOU_TANTOU_CD,
	MS.SHAIN_NAME_RYAKU,
	CONVERT(varchar(10),TSD.NYUUKIN_YOTEI_BI,111) AS NYUUKIN_YOTEI_BI,
	TSDK.SEIKYUU_GAKU,
	ISNULL(TNK.KESHIKOMI_GAKU,0) AS KESHIKOMI_GAKU,
	/*IF dto.NyuukinKeshigomuJoukyou == 1*/
	(TSDK.SEIKYUU_GAKU - ISNULL(TNK.KESHIKOMI_GAKU,0)) AS NYUUKIN_GAKU,
	--ELSE
	TSDK.SEIKYUU_GAKU AS NYUUKIN_GAKU,
	/*END*/
	TSD.SHIMEBI,
	CONVERT(varchar(10),TSD.SEIKYUU_DATE,111) AS SEIKYUU_DATE,
	MNK.NYUUSHUKKIN_KBN_NAME_RYAKU AS NYUUSHUKKIN_KBN_NAME_RYAKU
FROM T_SEIKYUU_DENPYOU TSD
INNER JOIN (SELECT 
				TSDK.SEIKYUU_NUMBER,
				TSDK.KAGAMI_NUMBER,
				TSDK.TORIHIKISAKI_CD,
				TSDK.GYOUSHA_CD,
				TSDK.GENBA_CD,
				(ISNULL(TSDK.KONKAI_URIAGE_GAKU, 0) 
				+ ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU, 0) 
				+ ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU, 0)
				+ ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU, 0) 
				+ ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU, 0) 
				+ ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU, 0)
				+ ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU, 0)) AS SEIKYUU_GAKU,
				TSDK.KONKAI_URIAGE_GAKU
			FROM T_SEIKYUU_DENPYOU_KAGAMI AS TSDK	
			WHERE TSDK.DELETE_FLG = 0) AS TSDK ON TSD.SEIKYUU_NUMBER = TSDK.SEIKYUU_NUMBER
LEFT JOIN (SELECT
				TNK.SEIKYUU_NUMBER,TNK.KAGAMI_NUMBER,
				SUM(ISNULL(TNK.KESHIKOMI_GAKU,0)) AS KESHIKOMI_GAKU
			FROM T_NYUUKIN_KESHIKOMI AS TNK
			WHERE TNK.DELETE_FLG = 0
			GROUP BY TNK.SEIKYUU_NUMBER,TNK.KAGAMI_NUMBER) TNK ON TSDK.SEIKYUU_NUMBER = TNK.SEIKYUU_NUMBER AND TSDK.KAGAMI_NUMBER = TNK.KAGAMI_NUMBER
INNER JOIN M_TORIHIKISAKI MT ON TSDK.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD
LEFT JOIN M_TORIHIKISAKI_SEIKYUU AS MTS ON MT.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
LEFT JOIN M_NYUUSHUKKIN_KBN AS MNK ON MNK.NYUUSHUKKIN_KBN_CD = MTS.KAISHUU_HOUHOU
LEFT JOIN M_SHAIN MS ON MT.EIGYOU_TANTOU_CD = MS.SHAIN_CD
LEFT JOIN M_GYOUSHA MGY ON TSDK.GYOUSHA_CD = MGY.GYOUSHA_CD
LEFT JOIN M_GENBA MGE ON TSDK.GYOUSHA_CD = MGE.GYOUSHA_CD AND TSDK.GENBA_CD = MGE.GENBA_CD
WHERE
TSD.DELETE_FLG = 0
/*IF dto.TorihikisakiCdFrom != null && dto.TorihikisakiCdFrom != ''*/AND TSD.TORIHIKISAKI_CD >= /*dto.TorihikisakiCdFrom*/''/*END*/
/*IF dto.TorihikisakiCdTo != null && dto.TorihikisakiCdTo != ''*/AND TSD.TORIHIKISAKI_CD <= /*dto.TorihikisakiCdTo*/''/*END*/
/*IF dto.SeikyuuDateFrom != null && dto.SeikyuuDateFrom != ''*/AND CONVERT(varchar, TSD.SEIKYUU_DATE, 111) >= /*dto.SeikyuuDateFrom*/''/*END*/
/*IF dto.SeikyuuDateTo != null && dto.SeikyuuDateTo != ''*/AND CONVERT(varchar, TSD.SEIKYUU_DATE, 111) <= /*dto.SeikyuuDateTo*/''/*END*/
/*IF dto.NyuukinYoteiDateFrom != null && dto.NyuukinYoteiDateFrom != ''*/AND CONVERT(varchar, TSD.NYUUKIN_YOTEI_BI, 111) >= /*dto.NyuukinYoteiDateFrom*/''/*END*/
/*IF dto.NyuukinYoteiDateTo != null && dto.NyuukinYoteiDateTo != ''*/AND CONVERT(varchar, TSD.NYUUKIN_YOTEI_BI, 111) <= /*dto.NyuukinYoteiDateTo*/''/*END*/
/*IF dto.KyotenCd != null && dto.KyotenCd != 99*/AND TSD.KYOTEN_CD = /*dto.KyotenCd*/99/*END*/
/*IF dto.EigyoushaCdFrom != null && dto.EigyoushaCdFrom != ''*/AND MT.EIGYOU_TANTOU_CD >= /*dto.EigyoushaCdFrom*/''/*END*/
/*IF dto.EigyoushaCdTo != null && dto.EigyoushaCdTo != ''*/AND MT.EIGYOU_TANTOU_CD <= /*dto.EigyoushaCdTo*/''/*END*/
AND TSDK.KONKAI_URIAGE_GAKU > 0
/*IF dto.NyuukinKeshigomuJoukyou == 1*/
AND (TSDK.SEIKYUU_GAKU - ISNULL(TNK.KESHIKOMI_GAKU,0)) <> 0
/*END*/
/*IF dto.Sort2 == 1*/
ORDER BY TSD.TORIHIKISAKI_CD, TSDK.GYOUSHA_CD, TSDK.GENBA_CD, MT.EIGYOU_TANTOU_CD, TSD.SEIKYUU_NUMBER
/*END*/
/*IF dto.Sort2 == 2*/
ORDER BY MT.TORIHIKISAKI_FURIGANA, TSD.TORIHIKISAKI_CD, TSDK.GYOUSHA_CD, TSDK.GENBA_CD, MT.EIGYOU_TANTOU_CD, TSD.SEIKYUU_NUMBER
/*END*/
/*IF dto.Sort1 == 3*/
ORDER BY TSD.NYUUKIN_YOTEI_BI, TSD.TORIHIKISAKI_CD, TSDK.GYOUSHA_CD, TSDK.GENBA_CD, TSD.SEIKYUU_DATE, TSD.SEIKYUU_NUMBER
/*END*/
