﻿SELECT
	TSD.TORIHIKISAKI_CD,
	MT.TORIHIKISAKI_NAME_RYAKU,
	MT.EIGYOU_TANTOU_CD AS EIGYOUSHA_CD,
	MS.SHAIN_NAME_RYAKU,
	CONVERT(varchar, TSD.SHUKKIN_YOTEI_BI, 111) AS SHUKKIN_YOTEI_BI,
	TSD.SEISAN_GAKU,
	ISNULL(TNK.KESHIKOMI_GAKU,0) AS KESHIKOMI_GAKU,
	/*IF dto.ShukkinKeshigomuJoukyou == 1*/
	(TSD.SEISAN_GAKU - ISNULL(TNK.KESHIKOMI_GAKU,0)) AS SHUKKIN_GAKU,
	--ELSE
	TSD.SEISAN_GAKU AS SHUKKIN_GAKU,
	/*END*/
	TSD.SHIMEBI,
	CONVERT(varchar, TSD.SEISAN_DATE, 111) AS SEISAN_DATE,
	MNK.NYUUSHUKKIN_KBN_NAME_RYAKU AS NYUUSHUKKIN_KBN_NAME_RYAKU
FROM (SELECT 
			TSD.KYOTEN_CD,
			TSD.SEISAN_NUMBER,
			TSD.TORIHIKISAKI_CD,
			TSD.SHIMEBI,
			TSD.SEISAN_DATE,
			TSD.SHUKKIN_YOTEI_BI,
			(ISNULL(TSD.KONKAI_SHIHARAI_GAKU, 0) 
			+ ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU, 0) 
			+ ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU, 0) 
			+ ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU, 0) 
			+ ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU, 0) 
			+ ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU, 0) 
			+ ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU, 0)) AS SEISAN_GAKU,
			TSD.KONKAI_SHIHARAI_GAKU,
			TSD.DELETE_FLG
		FROM T_SEISAN_DENPYOU AS TSD
		WHERE TSD.DELETE_FLG = 0) AS TSD
LEFT JOIN (SELECT
				TNK.SEISAN_NUMBER,
				SUM(ISNULL(TNK.KESHIKOMI_GAKU,0)) AS KESHIKOMI_GAKU
			FROM T_SHUKKIN_KESHIKOMI AS TNK
			WHERE TNK.DELETE_FLG = 0
			GROUP BY TNK.SEISAN_NUMBER) TNK ON TSD.SEISAN_NUMBER = TNK.SEISAN_NUMBER
JOIN M_TORIHIKISAKI AS MT ON MT.TORIHIKISAKI_CD = TSD.TORIHIKISAKI_CD
LEFT JOIN M_TORIHIKISAKI_SHIHARAI AS MTS ON MT.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
LEFT JOIN M_NYUUSHUKKIN_KBN AS MNK ON MNK.NYUUSHUKKIN_KBN_CD = MTS.SHIHARAI_HOUHOU
LEFT JOIN M_SHAIN AS MS ON MS.SHAIN_CD = MT.EIGYOU_TANTOU_CD
WHERE
TSD.DELETE_FLG = 0
/*IF dto.KyotenCd != null && dto.KyotenCd != 99*/AND TSD.KYOTEN_CD = /*dto.KyotenCd*/99/*END*/
/*IF dto.ShukkinYoteiDateFrom != null && dto.ShukkinYoteiDateFrom != ''*/AND CONVERT(varchar, TSD.SHUKKIN_YOTEI_BI, 111) >= /*dto.ShukkinYoteiDateFrom*/''/*END*/
/*IF dto.ShukkinYoteiDateTo != null && dto.ShukkinYoteiDateTo != ''*/AND CONVERT(varchar, TSD.SHUKKIN_YOTEI_BI, 111) <= /*dto.ShukkinYoteiDateTo*/''/*END*/
/*IF dto.SeisanDateFrom != null && dto.SeisanDateFrom != ''*/AND CONVERT(varchar, TSD.SEISAN_DATE, 111) >= /*dto.SeisanDateFrom*/''/*END*/
/*IF dto.SeisanDateTo != null && dto.SeisanDateTo != ''*/AND CONVERT(varchar, TSD.SEISAN_DATE, 111) <= /*dto.SeisanDateTo*/''/*END*/
/*IF dto.EigyoushaCdFrom != null && dto.EigyoushaCdFrom != ''*/AND MT.EIGYOU_TANTOU_CD >= /*dto.EigyoushaCdFrom*/''/*END*/
/*IF dto.EigyoushaCdTo != null && dto.EigyoushaCdTo != ''*/AND MT.EIGYOU_TANTOU_CD <= /*dto.EigyoushaCdTo*/''/*END*/
/*IF dto.TorihikisakiCdFrom != null && dto.TorihikisakiCdFrom != ''*/AND TSD.TORIHIKISAKI_CD >= /*dto.TorihikisakiCdFrom*/''/*END*/
/*IF dto.TorihikisakiCdTo != null && dto.TorihikisakiCdTo != ''*/AND TSD.TORIHIKISAKI_CD <= /*dto.TorihikisakiCdTo*/''/*END*/
AND TSD.KONKAI_SHIHARAI_GAKU > 0
/*IF dto.ShukkinKeshigomuJoukyou == 1*/
AND (TSD.SEISAN_GAKU - ISNULL(TNK.KESHIKOMI_GAKU,0)) <> 0
/*END*/
/*IF dto.Sort1 == 1 && dto.Sort2 == 1*/
ORDER BY MT.EIGYOU_TANTOU_CD, TSD.TORIHIKISAKI_CD, TSD.SEISAN_NUMBER
/*END*/
/*IF dto.Sort1 == 1 && dto.Sort2 == 2*/
ORDER BY MS.SHAIN_FURIGANA, MT.EIGYOU_TANTOU_CD, TSD.TORIHIKISAKI_CD, TSD.SEISAN_NUMBER
/*END*/
/*IF dto.Sort1 == 2 && dto.Sort2 == 1*/
ORDER BY TSD.TORIHIKISAKI_CD, MT.EIGYOU_TANTOU_CD, TSD.SEISAN_NUMBER
/*END*/
/*IF dto.Sort1 == 2 && dto.Sort2 == 2*/
ORDER BY MT.TORIHIKISAKI_FURIGANA, TSD.TORIHIKISAKI_CD, MT.EIGYOU_TANTOU_CD, TSD.SEISAN_NUMBER
/*END*/
/*IF dto.Sort1 == 3 && dto.IsGroupEigyousha*/
ORDER BY TSD.SHUKKIN_YOTEI_BI, MT.EIGYOU_TANTOU_CD, TSD.TORIHIKISAKI_CD, TSD.SEISAN_DATE, TSD.SEISAN_NUMBER
/*END*/
/*IF dto.Sort1 == 3 && dto.IsGroupEigyousha != true*/
ORDER BY TSD.SHUKKIN_YOTEI_BI, TSD.TORIHIKISAKI_CD, TSD.SEISAN_DATE, TSD.SEISAN_NUMBER
/*END*/