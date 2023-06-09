SELECT
  TSD.SEIKYUU_NUMBER                                               --¿Ô
  , TSDKE.KAGAMI_NUMBER                                            --ÓÔ
  , TSDKE.ROW_NUMBER                                               --sÔ
  , TSDKE.DENPYOU_SHURUI_CD                                        --`[íÞCD
  , TSDKE.DENPYOU_SYSTEM_ID                                        --`[VXeID
  , TSDKE.DENPYOU_SEQ                                              --`[}Ô
  , TSDKE.DETAIL_SYSTEM_ID                                         --¾×VXeID
  , TSDKE.DENPYOU_NUMBER                                           --`[Ô
  , TSDKE.DENPYOU_DATE                                             --`[út
  , TSDKE.TSDE_TORIHIKISAKI_CD                                     --æøæCD
  , TSDKE.TSDE_GYOUSHA_CD                                          --ÆÒCD
  , TSDKE.GYOUSHA_NAME1                                            --ÆÒ¼1
  , TSDKE.GYOUSHA_NAME2                                            --ÆÒ¼2
  , TSDKE.TSDE_GENBA_CD                                            --»êCD
  , TSDKE.GENBA_NAME1                                              --»ê¼1
  , TSDKE.GENBA_NAME2                                              --»ê¼2
  , TSDKE.HINMEI_CD                                                --i¼CD
  , TSDKE.HINMEI_NAME                                              --i¼
  , TSDKE.SUURYOU                                                  --Ê
  , TSDKE.UNIT_CD                                                  --PÊCD
  , TSDKE.UNIT_NAME                                                --PÊ¼
  , TSDKE.TANKA						                               --P¿
  , ISNULL(TSDKE.KINGAKU,0) AS KINGAKU                             --àz
  , ISNULL(TSDKE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                   --àÅz
  , ISNULL(TSDKE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                   --OÅz
  , ISNULL(TSDKE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --`[àÅz
  , ISNULL(TSDKE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --`[OÅz
  , TSDKE.DENPYOU_ZEI_KBN_CD                                       --`[ÅæªCD
  , TSDKE.MEISAI_ZEI_KBN_CD                                        --¾×ÅæªCD
  , TSDKE.MEISAI_BIKOU                                             --¾×õl
  , TSDKE.DENPYOU_ZEI_KEISAN_KBN_CD                                --`[ÅvZæª
  , TSDKE.TSDK_TORIHIKISAKI_CD                                     --æøæCD
  , TSDKE.TSDK_GYOUSHA_CD                                          --ÆÒCD
  , TSDKE.TSDK_GENBA_CD                                            --»êCD
  , TSDKE.DAIHYOU_PRINT_KBN                                        --ã\Òóæª
  , TSDKE.CORP_NAME                                                --ïÐ¼
  , TSDKE.CORP_DAIHYOU                                             --ã\Ò¼
  , TSDKE.KYOTEN_NAME_PRINT_KBN                                    --_¼óæª
  , TSDKE.KYOTEN_CD                                                --_CD
  , TSDKE.KYOTEN_NAME                                              --_¼
  , TSDKE.KYOTEN_DAIHYOU                                           --_ã\Ò¼
  , TSDKE.KYOTEN_POST                                              --_XÖÔ
  , TSDKE.KYOTEN_ADDRESS1                                          --_Z1
  , TSDKE.KYOTEN_ADDRESS2                                          --_Z2
  , TSDKE.KYOTEN_TEL                                               --_TEL
  , TSDKE.KYOTEN_FAX                                               --_FAX
  , TSDKE.SEIKYUU_SOUFU_NAME1                                      --¿tæ1
  , TSDKE.SEIKYUU_SOUFU_NAME2                                      --¿tæ2
  , TSDKE.SEIKYUU_SOUFU_KEISHOU1                                   --¿tæhÌ1
  , TSDKE.SEIKYUU_SOUFU_KEISHOU2                                   --¿tæhÌ2
  , TSDKE.SEIKYUU_SOUFU_POST                                       --¿tæXÖÔ
  , TSDKE.SEIKYUU_SOUFU_ADDRESS1                                   --¿tæZ1
  , TSDKE.SEIKYUU_SOUFU_ADDRESS2                                   --¿tæZ2
  , TSDKE.SEIKYUU_SOUFU_BUSHO                                      --¿tæ
  , TSDKE.SEIKYUU_SOUFU_TANTOU                                     --¿tæSÒ
  , TSDKE.SEIKYUU_SOUFU_TEL                                        --¿tæTEL
  , TSDKE.SEIKYUU_SOUFU_FAX                                        --¿tæFAX
  , TSDKE.SEIKYUU_TANTOU                                           --¿SÒ
  , TSDKE.BIKOU_1												  --õl1
  , TSDKE.BIKOU_2												  --õl2
  , ISNULL(TSDKE.KONKAI_URIAGE_GAKU,0) AS TSDK_KONKAI_URIAGE_GAKU            --¡ñãz
  , ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_SEI_UTIZEI_GAKU    --¡ñ¿àÅz
  , ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_SEI_SOTOZEI_GAKU  --¡ñ¿OÅz
  , ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) AS TSDK_KONKAI_DEN_UTIZEI_GAKU    --¡ñ`àÅz
  , ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSDK_KONKAI_DEN_SOTOZEI_GAKU  --¡ñ`OÅz
  , ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_MEI_UTIZEI_GAKU    --¡ñ¾àÅz
  , ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_MEI_SOTOZEI_GAKU  --¡ñ¾OÅz
  , TSD.KYOTEN_CD AS TSD_KYOTEN_CD											--_CD
  , TSD.SHIMEBI																--÷ú
  , TSD.TORIHIKISAKI_CD AS TSD_TORIHIKISAKI_CD								--æøæCD
  , TSD.SHOSHIKI_KBN														--®æª
  , TSD.SHOSHIKI_MEISAI_KBN													--®¾×æª
  , TSD.SEIKYUU_KEITAI_KBN													--¿`Ôæª
  , TSD.NYUUKIN_MEISAI_KBN													--üà¾×æª
  , TSD.YOUSHI_KBN															--pæª
  , TSD.SEIKYUU_DATE														--¿út
  , TSD.NYUUKIN_YOTEI_BI													--üà\èú
  , ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) AS ZENKAI_KURIKOSI_GAKU              --OñJzz
  , ISNULL(TSD.KONKAI_NYUUKIN_GAKU,0) AS KONKAI_NYUUKIN_GAKU                --¡ñüàz
  , ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0) AS KONKAI_CHOUSEI_GAKU                --¡ñ²®z
  , ISNULL(TSD.KONKAI_URIAGE_GAKU,0) AS TSD_KONKAI_URIAGE_GAKU              --¡ñãz
  , ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) AS TSD_KONKAI_SEI_UTIZEI_GAKU      --¡ñ¿àÅz
  , ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_SEI_SOTOZEI_GAKU    --¡ñ¿OÅz
  , ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) AS TSD_KONKAI_DEN_UTIZEI_GAKU      --¡ñ`àÅz
  , ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSD_KONKAI_DEN_SOTOZEI_GAKU    --¡ñ`OÅz
  , ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) AS TSD_KONKAI_MEI_UTIZEI_GAKU      --¡ñ¾àÅz
  , ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_MEI_SOTOZEI_GAKU    --¡ñ¾OÅz
  , ISNULL(TSD.KONKAI_SEIKYU_GAKU,0) AS KONKAI_SEIKYU_GAKU                  --¡ñä¿z
  , TSD.FURIKOMI_BANK_CD                                          --UâsCD
  , TSD.FURIKOMI_BANK_NAME                                        --Uâs¼
  , TSD.FURIKOMI_BANK_SHITEN_CD                                   --UâsxXCD
  , TSD.FURIKOMI_BANK_SHITEN_NAME                                 --UâsxX¼
  , TSD.KOUZA_SHURUI                                              --ûÀíÞ
  , TSD.KOUZA_NO                                                  --ûÀÔ
  , TSD.KOUZA_NAME                                                --ûÀ¼`
  , TSD.FURIKOMI_BANK_CD_2                                        --UâsCD2
  , TSD.FURIKOMI_BANK_NAME_2                                      --Uâs¼2
  , TSD.FURIKOMI_BANK_SHITEN_CD_2                                 --UâsxXCD2
  , TSD.FURIKOMI_BANK_SHITEN_NAME_2                               --UâsxX¼2
  , TSD.KOUZA_SHURUI_2                                            --ûÀíÞ2
  , TSD.KOUZA_NO_2                                                --ûÀÔ2
  , TSD.KOUZA_NAME_2                                              --ûÀ¼`2
  , TSD.FURIKOMI_BANK_CD_3                                        --UâsCD3
  , TSD.FURIKOMI_BANK_NAME_3                                      --Uâs¼3
  , TSD.FURIKOMI_BANK_SHITEN_CD_3                                 --UâsxXCD3
  , TSD.FURIKOMI_BANK_SHITEN_NAME_3                               --UâsxX¼3
  , TSD.KOUZA_SHURUI_3                                            --ûÀíÞ3
  , TSD.KOUZA_NO_3                                                --ûÀÔ3
  , TSD.KOUZA_NAME_3                                              --ûÀ¼`3
  , TSD.HAKKOU_KBN                                                --­sæª
  , TSD.SHIME_JIKKOU_NO                                           --÷ÀsÔ
  , (ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) - ISNULL(TSD.KONKAI_NYUUKIN_GAKU,0) - ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0)) AS SASIHIKIGAKU--·øJzz
  , (ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0)
		 + ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0)) AS SYOUHIZEIGAKU--ÁïÅz
  , (ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) AS MEISEI_SYOHIZEI
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS RANK_DENPYO_1 --`[N
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_KINGAKU_1 --`[àzv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS RANK_GENBA_1 --»êN
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_UCHIZEI --»êàÅÁïÅv
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_SOTOZEI --»êOÅÁïÅv
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_KINGAKU_1 --»êàzv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS RANK_GYOUSHA_1 --ÆÒN
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_UCHIZEI --ÆÒàÅÁïÅv
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_SOTOZEI --ÆÒOÅÁïÅv
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_KINGAKU_1 --ÆÒàzv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER) AS RANK_SEIKYU_1 --¿N
  , TSD.TOUROKU_NO
  , TSD.INVOICE_KBN
  , TSDKE.KONKAI_KAZEI_KBN_1     --¡ñÛÅæªP
  , TSDKE.KONKAI_KAZEI_RATE_1    --¡ñÛÅÅ¦P
  , TSDKE.KONKAI_KAZEI_GAKU_1    --¡ñÛÅÅ²àzP
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_1 --¡ñÛÅÅzP
  , TSDKE.KONKAI_KAZEI_KBN_2     --¡ñÛÅæªQ
  , TSDKE.KONKAI_KAZEI_RATE_2    --¡ñÛÅÅ¦Q
  , TSDKE.KONKAI_KAZEI_GAKU_2    --¡ñÛÅÅ²àzQ
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_2 --¡ñÛÅÅzQ
  , TSDKE.KONKAI_KAZEI_KBN_3     --¡ñÛÅæªR
  , TSDKE.KONKAI_KAZEI_RATE_3    --¡ñÛÅÅ¦R
  , TSDKE.KONKAI_KAZEI_GAKU_3    --¡ñÛÅÅ²àzR
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_3 --¡ñÛÅÅzR
  , TSDKE.KONKAI_KAZEI_KBN_4     --¡ñÛÅæªS
  , TSDKE.KONKAI_KAZEI_RATE_4    --¡ñÛÅÅ¦S
  , TSDKE.KONKAI_KAZEI_GAKU_4    --¡ñÛÅÅ²àzS
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_4 --¡ñÛÅÅzS
  , TSDKE.KONKAI_HIKAZEI_KBN     --¡ññÛÅæª
  , TSDKE.KONKAI_HIKAZEI_GAKU    --¡ññÛÅz
FROM
  T_SEIKYUU_DENPYOU TSD 
  INNER JOIN (
	SELECT
		TSDK.SEIKYUU_NUMBER                                             --¿Ô
		, TSDK.KAGAMI_NUMBER                                            --ÓÔ
		, TSDE.ROW_NUMBER                                               --sÔ
		, TSDE.DENPYOU_SHURUI_CD                                        --`[íÞCD
		, TSDE.DENPYOU_SYSTEM_ID                                        --`[VXeID
		, TSDE.DENPYOU_SEQ                                              --`[}Ô
		, TSDE.DETAIL_SYSTEM_ID                                         --¾×VXeID
		, TSDE.DENPYOU_NUMBER                                           --`[Ô
		, TSDE.DENPYOU_DATE                                             --`[út
		, TSDE.TORIHIKISAKI_CD AS TSDE_TORIHIKISAKI_CD                  --æøæCD
		, TSDE.GYOUSHA_CD AS TSDE_GYOUSHA_CD                            --ÆÒCD
		, TSDE.GYOUSHA_NAME1                                            --ÆÒ¼1
		, TSDE.GYOUSHA_NAME2                                            --ÆÒ¼2
		, TSDE.GENBA_CD AS TSDE_GENBA_CD                                --»êCD
		, TSDE.GENBA_NAME1                                              --»ê¼1
		, TSDE.GENBA_NAME2                                              --»ê¼2
		, TSDE.HINMEI_CD                                                --i¼CD
		, TSDE.HINMEI_NAME                                              --i¼
		, TSDE.SUURYOU                                                  --Ê
		, TSDE.UNIT_CD                                                  --PÊCD
		, TSDE.UNIT_NAME                                                --PÊ¼
		, TSDE.TANKA					                                --P¿
		, TSDE.KINGAKU                                                  --àz
		, ISNULL(TSDE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                   --àÅz
		, ISNULL(TSDE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                   --OÅz
		, ISNULL(TSDE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --`[àÅz
		, ISNULL(TSDE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --`[OÅz
		, TSDE.DENPYOU_ZEI_KBN_CD                                       --`[ÅæªCD
		, TSDE.MEISAI_ZEI_KBN_CD                                        --¾×ÅæªCD
		, TSDE.MEISAI_BIKOU                                             --¾×õl
		, TSDE.DENPYOU_ZEI_KEISAN_KBN_CD                                --`[ÅvZæª
		, TSDK.TORIHIKISAKI_CD AS TSDK_TORIHIKISAKI_CD                  --æøæCD
		, TSDK.GYOUSHA_CD AS TSDK_GYOUSHA_CD                            --ÆÒCD
		, TSDK.GENBA_CD AS TSDK_GENBA_CD                                --»êCD
		, TSDK.DAIHYOU_PRINT_KBN                                        --ã\Òóæª
		, TSDK.CORP_NAME                                                --ïÐ¼
		, TSDK.CORP_DAIHYOU                                             --ã\Ò¼
		, TSDK.KYOTEN_NAME_PRINT_KBN                                    --_¼óæª
		, TSDK.KYOTEN_CD                                                --_CD
		, TSDK.KYOTEN_NAME                                              --_¼
		, TSDK.KYOTEN_DAIHYOU                                           --_ã\Ò¼
		, TSDK.KYOTEN_POST                                              --_XÖÔ
		, TSDK.KYOTEN_ADDRESS1                                          --_Z1
		, TSDK.KYOTEN_ADDRESS2                                          --_Z2
		, TSDK.KYOTEN_TEL                                               --_TEL
		, TSDK.KYOTEN_FAX                                               --_FAX
		, TSDK.SEIKYUU_SOUFU_NAME1                                      --¿tæ1
		, TSDK.SEIKYUU_SOUFU_NAME2                                      --¿tæ2
		, TSDK.SEIKYUU_SOUFU_KEISHOU1                                   --¿tæhÌ1
		, TSDK.SEIKYUU_SOUFU_KEISHOU2                                   --¿tæhÌ2
		, TSDK.SEIKYUU_SOUFU_POST                                       --¿tæXÖÔ
		, TSDK.SEIKYUU_SOUFU_ADDRESS1                                   --¿tæZ1
		, TSDK.SEIKYUU_SOUFU_ADDRESS2                                   --¿tæZ2
		, TSDK.SEIKYUU_SOUFU_BUSHO                                      --¿tæ
		, TSDK.SEIKYUU_SOUFU_TANTOU                                     --¿tæSÒ
		, TSDK.SEIKYUU_SOUFU_TEL                                        --¿tæTEL
		, TSDK.SEIKYUU_SOUFU_FAX                                        --¿tæFAX
		, TSDK.SEIKYUU_TANTOU                                           --¿SÒ
		, ISNULL(TSDK.KONKAI_URIAGE_GAKU,0) AS KONKAI_URIAGE_GAKU            --¡ñãz
		, ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) AS KONKAI_SEI_UTIZEI_GAKU    --¡ñ¿àÅz
		, ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) AS KONKAI_SEI_SOTOZEI_GAKU  --¡ñ¿OÅz
		, ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) AS KONKAI_DEN_UTIZEI_GAKU    --¡ñ`àÅz
		, ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) AS KONKAI_DEN_SOTOZEI_GAKU  --¡ñ`OÅz
		, ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) AS KONKAI_MEI_UTIZEI_GAKU    --¡ñ¾àÅz
		, ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS KONKAI_MEI_SOTOZEI_GAKU  --¡ñ¾OÅz
		, TSDK.BIKOU_1														 --õl1
		, TSDK.BIKOU_2														 --õl2
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --¡ñÛÅæªP
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1			 --¡ñÛÅÅ¦P
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1			 --¡ñÛÅÅ²àzP
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1	 --¡ñÛÅÅzP
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --¡ñÛÅæªQ
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2			 --¡ñÛÅÅ¦Q
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2			 --¡ñÛÅÅ²àzQ
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --¡ñÛÅÅzQ
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --¡ñÛÅæªR
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3			 --¡ñÛÅÅ¦R
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3			 --¡ñÛÅÅ²àzR
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --¡ñÛÅÅzR
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --¡ñÛÅæªS
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4			 --¡ñÛÅÅ¦S
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4			 --¡ñÛÅÅ²àzS
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --¡ñÛÅÅzS
		, ISNULL(TSDK.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN			 --¡ññÛÅæª
		, ISNULL(TSDK.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU			 --¡ññÛÅz
	FROM
		T_SEIKYUU_DENPYOU_KAGAMI TSDK
		LEFT OUTER JOIN 
        T_SEIKYUU_DETAIL TSDE 
        ON TSDK.SEIKYUU_NUMBER = TSDE.SEIKYUU_NUMBER AND TSDK.KAGAMI_NUMBER = TSDE.KAGAMI_NUMBER
  ) TSDKE 
  ON TSD.SEIKYUU_NUMBER = TSDKE.SEIKYUU_NUMBER
WHERE
  TSD.DELETE_FLG = 0
  AND TSD.SEIKYUU_NUMBER = /*seikyuNumber*/
 ORDER BY
   TSDKE.KAGAMI_NUMBER
   /*$orderBy*/
  , TSDKE.DENPYOU_DATE
  , TSDKE.DENPYOU_SHURUI_CD
  , TSDKE.DENPYOU_NUMBER
  , TSDKE.ROW_NUMBER
  