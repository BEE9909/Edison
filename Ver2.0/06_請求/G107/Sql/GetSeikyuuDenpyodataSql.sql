﻿SELECT DISTINCT
    TSD.HAKKOU_KBN,
	/*IF data.OutputKbn != 4*/
	CAST(CASE MTS.OUTPUT_KBN WHEN 2 THEN 1 ELSE 0 END AS bit) AS DETAIL_OUTPUT_KBN,
	CAST(CASE MTS.OUTPUT_KBN WHEN 3 THEN 1 ELSE 0 END AS bit) AS RAKURAKU_CSV_OUTPUT,
	--ELSE
	CAST(0 AS bit) AS DETAIL_OUTPUT_KBN,
	CAST(0 AS bit) AS RAKURAKU_CSV_OUTPUT,
	/*END*/
    TSD.SEIKYUU_NUMBER,
    TSD.TORIHIKISAKI_CD,
    MT.TORIHIKISAKI_NAME_RYAKU,
    TSD.SHIMEBI,
	(CASE TSD.SEIKYUU_KEITAI_KBN 
		WHEN 2 THEN TSD.ZENKAI_KURIKOSI_GAKU 
        WHEN 1 THEN NULL
		ELSE 0 
		END) AS ZENKAI_KURIKOSI_GAKU,
    CASE TSD.SEIKYUU_KEITAI_KBN 
        WHEN 1 THEN NULL
        ELSE TSD.KONKAI_NYUUKIN_GAKU
    END AS KONKAI_NYUUKIN_GAKU,
    CASE TSD.SEIKYUU_KEITAI_KBN 
        WHEN 1 THEN NULL
        ELSE TSD.KONKAI_CHOUSEI_GAKU
    END AS KONKAI_CHOUSEI_GAKU,
    TSD.KONKAI_URIAGE_GAKU,
    TSD.KONKAI_SEI_UTIZEI_GAKU
        + TSD.KONKAI_SEI_SOTOZEI_GAKU
        + TSD.KONKAI_DEN_UTIZEI_GAKU
        + TSD.KONKAI_DEN_SOTOZEI_GAKU
        + TSD.KONKAI_MEI_UTIZEI_GAKU
        + TSD.KONKAI_MEI_SOTOZEI_GAKU SHOHIZEI_GAKU,
	(CASE TSD.SEIKYUU_KEITAI_KBN 
		WHEN 2 THEN TSD.KONKAI_SEIKYU_GAKU
		ELSE (TSD.KONKAI_URIAGE_GAKU + TSD.KONKAI_SEI_UTIZEI_GAKU + TSD.KONKAI_SEI_SOTOZEI_GAKU + TSD.KONKAI_DEN_UTIZEI_GAKU + TSD.KONKAI_DEN_SOTOZEI_GAKU + TSD.KONKAI_MEI_UTIZEI_GAKU + TSD.KONKAI_MEI_SOTOZEI_GAKU)
	    END) AS KONKAI_SEIKYU_GAKU,
    TSD.NYUUKIN_YOTEI_BI,
    TSD.TIME_STAMP,
    TSD.SEIKYUU_DATE
    /*IF data.PrintOrder == 1*/
    ,MT.TORIHIKISAKI_FURIGANA
    /*END*/
    /*IF data.PrintOrder == 2*/
    ,TSD.TORIHIKISAKI_CD
    /*END*/
	/*IF !data.FilteringData.IsNull && data.FilteringData == 2*/
	,TSDE.SEIKYUU_NUMBER
	/*END*/
FROM
    T_SEIKYUU_DENPYOU TSD
    LEFT OUTER JOIN M_TORIHIKISAKI MT ON
        TSD.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD
	LEFT OUTER JOIN M_TORIHIKISAKI_SEIKYUU MTS ON
	    TSD.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
	/*IF !data.FilteringData.IsNull && data.FilteringData == 1*/
	INNER JOIN T_SEIKYUU_DETAIL AS TSDE ON 
	TSD.SEIKYUU_NUMBER = TSDE.SEIKYUU_NUMBER
	AND TSDE.DELETE_FLG = 0
	AND TSDE.DENPYOU_SHURUI_CD != 10
	/*END*/
	/*IF !data.FilteringData.IsNull && data.FilteringData == 2*/
	LEFT JOIN T_SEIKYUU_DETAIL AS TSDE ON 
	TSD.SEIKYUU_NUMBER = TSDE.SEIKYUU_NUMBER
	AND TSDE.DELETE_FLG = 0
	/*END*/
/*BEGIN*/
WHERE
    /*IF !deletechuFlg*/
    TSD.DELETE_FLG = 0
    /*END*/
    /*IF data.OutputKbn == 1*/
	AND MTS.OUTPUT_KBN = 1
	/*END*/
	/*IF data.OutputKbn == 2*/
	AND MTS.OUTPUT_KBN = 2
	/*END*/
	/*IF data.OutputKbn == 3*/
	AND MTS.OUTPUT_KBN = 3
	/*END*/
    /*IF data.DenpyoHizukeFrom != null && data.DenpyoHizukeFrom != ''*/
    AND TSD.SEIKYUU_DATE >= /*data.DenpyoHizukeFrom*/'2013/01/07'
    /*END*/
    /*IF data.DenpyoHizukeTo != null && data.DenpyoHizukeTo != ''*/
    AND TSD.SEIKYUU_DATE <= /*data.DenpyoHizukeTo*/'2014/01/07'
    /*END*/
    /*IF data.HakkouKyotenCD != null && data.HakkouKyotenCD != ''*/
    AND TSD.KYOTEN_CD = /*data.HakkouKyotenCD*/''
    /*END*/
    /*IF !deletechuFlg && data.Simebi != null && data.Simebi != ''*/
    AND TSD.SHIMEBI = /*data.Simebi*/31
    /*END*/
    /*IF data.SeikyuPaper < 3*/
    AND TSD.YOUSHI_KBN = /*data.SeikyuPaper*/2
	--ELSE
	/*IF data.SeikyuPaper == 3*/
	AND MTS.YOUSHI_KBN = 1
	/*END*/
	/*IF data.SeikyuPaper == 4*/
	AND MTS.YOUSHI_KBN = 2
	/*END*/
    /*END*/
	/*IF data.TorihikisakiCD != null && data.TorihikisakiCD != ''*/
	AND TSD.TORIHIKISAKI_CD = /*data.TorihikisakiCD*/''
	/*END*/
	/*IF !data.HakkoKbn.IsNull*/
	AND TSD.HAKKOU_KBN = /*data.HakkoKbn*/1
	/*END*/
	/*IF !data.FilteringData.IsNull && data.FilteringData == 2*/
	AND 
	(
		TSD.SEIKYUU_NUMBER IS NOT NULL
		OR NOT (
			CASE TSD.SEIKYUU_KEITAI_KBN 
			WHEN 2 THEN
			ISNULL(TSD.ZENKAI_KURIKOSI_GAKU, 0)
			ELSE 0
			END = 0
			AND
			CASE TSD.SEIKYUU_KEITAI_KBN 
			WHEN 2 THEN ISNULL(TSD.KONKAI_SEIKYU_GAKU, 0)
			ELSE (TSD.KONKAI_URIAGE_GAKU + TSD.KONKAI_SEI_UTIZEI_GAKU + TSD.KONKAI_SEI_SOTOZEI_GAKU + TSD.KONKAI_DEN_UTIZEI_GAKU + TSD.KONKAI_DEN_SOTOZEI_GAKU + TSD.KONKAI_MEI_UTIZEI_GAKU + TSD.KONKAI_MEI_SOTOZEI_GAKU)
			END = 0
		)
	)
	/*END*/
	/*IF data.ZeroKingakuTaishogai*/
	--今回御請求額
	AND (
		 (TSD.SHOSHIKI_KBN != 1 
		 AND EXISTS (SELECT 1 
					   FROM T_SEIKYUU_DENPYOU_KAGAMI TSDK
					   WHERE
					   TSDK.SEIKYUU_NUMBER = TSD.SEIKYUU_NUMBER
					   AND (ISNULL(TSDK.KONKAI_URIAGE_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) <> 0)))
		OR
		(TSD.SHOSHIKI_KBN = 1
		 AND (CASE TSD.SEIKYUU_KEITAI_KBN 
				WHEN 2 THEN ISNULL(TSD.KONKAI_SEIKYU_GAKU, 0)
				ELSE (ISNULL(TSD.KONKAI_URIAGE_GAKU,0) + 
					  ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0)+ 
					  ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0))
				END) <> 0))
	/*END*/
	/*IF data.UseInxsSeikyuuKbn*/
		AND (MTS.INXS_SEIKYUU_KBN = 2 OR MTS.INXS_SEIKYUU_KBN IS NULL)
	/*END*/
    /*IF data.PrintOrder == 1*/
    ORDER BY MT.TORIHIKISAKI_FURIGANA
    /*END*/
    /*IF data.PrintOrder == 2*/
    ORDER BY TSD.TORIHIKISAKI_CD
    /*END*/
/*END*/
