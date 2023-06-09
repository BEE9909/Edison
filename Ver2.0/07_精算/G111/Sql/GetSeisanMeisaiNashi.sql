SELECT
    TSD.SEISAN_NUMBER                              --¸ZÔ
  , TSDKE.KAGAMI_NUMBER                            --ÓÔ
  , TSDKE.ROW_NUMBER                               --sÔ
  , TSDKE.DENPYOU_SHURUI_CD                        --`[íÞCD
  , TSDKE.DENPYOU_SYSTEM_ID                        --`[VXeID
  , TSDKE.DENPYOU_SEQ                              --`[}Ô
  , TSDKE.DETAIL_SYSTEM_ID                         --¾×VXeID
  , TSDKE.DENPYOU_NUMBER                           --`[Ô
  , TSDKE.DENPYOU_DATE                             --`[út
  , TSDKE.TSDE_TORIHIKISAKI_CD                     --æøæCD
  , TSDKE.TSDE_GYOUSHA_CD                          --ÆÒCD
  , TSDKE.GYOUSHA_NAME1                            --ÆÒ¼1
  , TSDKE.GYOUSHA_NAME2                            --ÆÒ¼2
  , TSDKE.TSDE_GENBA_CD                            --»êCD
  , TSDKE.GENBA_NAME1                              --»ê¼1
  , TSDKE.GENBA_NAME2                              --»ê¼2
  , TSDKE.HINMEI_CD                                --i¼CD
  , TSDKE.HINMEI_NAME                              --i¼
  , TSDKE.SUURYOU                                  --Ê
  , TSDKE.UNIT_CD                                  --PÊCD
  , TSDKE.UNIT_NAME                                --PÊ¼
  , TSDKE.TANKA					                   --P¿
  , ISNULL(TSDKE.KINGAKU,0) AS KINGAKU             --àz
  , ISNULL(TSDKE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU   --àÅz
  , ISNULL(TSDKE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU   --OÅz
  , ISNULL(TSDKE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --`[àÅz
  , ISNULL(TSDKE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --`[OÅz
  , TSDKE.DENPYOU_ZEI_KBN_CD                       --`[ÅæªCD
  , TSDKE.MEISAI_ZEI_KBN_CD                        --¾×ÅæªCD
  , TSDKE.MEISAI_BIKOU                             --¾×õl
  , TSDKE.DENPYOU_ZEI_KEISAN_KBN_CD                --`[ÅvZæª
  , TSDKE.TSDK_TORIHIKISAKI_CD                     --æøæCD
  , TSDKE.TSDK_GYOUSHA_CD                          --ÆÒCD
  , TSDKE.TSDK_GENBA_CD                            --»êCD
  , TSDKE.DAIHYOU_PRINT_KBN                        --ã\Òóæª
  , TSDKE.CORP_NAME                                --ïÐ¼
  , TSDKE.CORP_DAIHYOU                             --ã\Ò¼
  , TSDKE.KYOTEN_NAME_PRINT_KBN                    --_¼óæª
  , TSDKE.TSDK_KYOTEN_CD AS TSDK_KYOTEN_CD         --_CD
  , TSDKE.KYOTEN_NAME                              --_¼
  , TSDKE.KYOTEN_DAIHYOU                           --_ã\Ò¼
  , TSDKE.KYOTEN_POST                              --_XÖÔ
  , TSDKE.KYOTEN_ADDRESS1                          --_Z1
  , TSDKE.KYOTEN_ADDRESS2                          --_Z2
  , TSDKE.KYOTEN_TEL                               --_TEL
  , TSDKE.KYOTEN_FAX                               --_FAX
  , TSDKE.SHIHARAI_SOUFU_NAME1                     --x¥¾×tæ1
  , TSDKE.SHIHARAI_SOUFU_NAME2                     --x¥¾×tæ2
  , TSDKE.SHIHARAI_SOUFU_KEISHOU1                  --x¥¾×tæhÌ1
  , TSDKE.SHIHARAI_SOUFU_KEISHOU2                  --x¥¾×tæhÌ2
  , TSDKE.SHIHARAI_SOUFU_POST                      --x¥¾×tæXÖÔ
  , TSDKE.SHIHARAI_SOUFU_ADDRESS1                  --x¥¾×tæZ1
  , TSDKE.SHIHARAI_SOUFU_ADDRESS2                  --x¥¾×tæZ2
  , TSDKE.SHIHARAI_SOUFU_BUSHO                     --x¥¾×tæ
  , TSDKE.SHIHARAI_SOUFU_TANTOU                    --x¥¾×tæSÒ
  , TSDKE.SHIHARAI_SOUFU_TEL                       --x¥¾×tæTEL
  , TSDKE.SHIHARAI_SOUFU_FAX                       --x¥¾×tæFAX
  , TSDKE.BIKOU_1												  --õl1
  , TSDKE.BIKOU_2												  --õl2
  , ISNULL(TSDKE.KONKAI_SHIHARAI_GAKU,0) AS TSDK_KONKAI_SHIHARAI_GAKU        --¡ñx¥z
  , ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_SEI_UTIZEI_GAKU    --¡ñ¿àÅz
  , ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_SEI_SOTOZEI_GAKU  --¡ñ¿OÅz
  , ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) AS TSDK_KONKAI_DEN_UTIZEI_GAKU    --¡ñ`àÅz
  , ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSDK_KONKAI_DEN_SOTOZEI_GAKU  --¡ñ`OÅz
  , ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_MEI_UTIZEI_GAKU    --¡ñ¾àÅz
  , ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_MEI_SOTOZEI_GAKU  --¡ñ¾OÅz
  , TSD.KYOTEN_CD                                 --_CD
  , TSD.SHIMEBI                                   --÷ú
  , TSD.TORIHIKISAKI_CD AS TSD_TORIHIKISAKI_CD    --æøæCD
  , TSD.SHOSHIKI_KBN                              --®æª
  , TSD.SHOSHIKI_MEISAI_KBN                       --®¾×æª
  , TSD.SHIHARAI_KEITAI_KBN                       --x¥`Ôæª
  , TSD.SHUKKIN_MEISAI_KBN                        --üà¾×æª
  , TSD.YOUSHI_KBN                                --pæª
  , TSD.SEISAN_DATE                               --¸Zút
  , TSD.SHUKKIN_YOTEI_BI                          --oà\èú
  , ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) AS ZENKAI_KURIKOSI_GAKU              --OñJzz
  , ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) AS KONKAI_SHUKKIN_GAKU                --¡ñoàz
  , ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0) AS KONKAI_CHOUSEI_GAKU                --¡ñ²®z
  , ISNULL(TSD.KONKAI_SHIHARAI_GAKU,0) AS TSD_KONKAI_SHIHARAI_GAKU          --¡ñx¥z
  , ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) AS TSD_KONKAI_SEI_UTIZEI_GAKU      --¡ñ¿àÅz
  , ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_SEI_SOTOZEI_GAKU    --¡ñ¿OÅz
  , ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) AS TSD_KONKAI_DEN_UTIZEI_GAKU      --¡ñ`àÅz
  , ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSD_KONKAI_DEN_SOTOZEI_GAKU    --¡ñ`OÅz
  , ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) AS TSD_KONKAI_MEI_UTIZEI_GAKU      --¡ñ¾àÅz
  , ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_MEI_SOTOZEI_GAKU    --¡ñ¾OÅz
  , ISNULL(TSD.KONKAI_SEISAN_GAKU,0) AS KONKAI_SEISAN_GAKU                  --¡ñä¸Zz
  , TSD.HAKKOU_KBN                                --­sæª
  , TSD.SHIME_JIKKOU_NO                           --÷ÀsÔ
  , (ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) - ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) - ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0)) AS SASIHIKIGAKU --·øJzz
  , (ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) 
        + ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0)) AS SYOUHIZEIGAKU--ÁïÅz
  , (ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) AS MEISEI_SYOHIZEI
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS RANK_DENPYO_1 --`[N
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_SYOHIZEI_1 --`[ÁïÅv
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_KINGAKU_1 --`[àzv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS RANK_GENBA_1 --»êN
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_UCHIZEI --»êàÅÁïÅv
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_SOTOZEI --»êOÅÁïÅv
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_SYOHIZEI_1 --»êÁïÅv
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_KINGAKU_1 --»êàzv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS RANK_GYOUSHA_1 --ÆÒN
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_UCHIZEI --ÆÒàÅÁïÅv
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_SOTOZEI --ÆÒOÅÁïÅv
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_SYOHIZEI_1 --ÆÒÁïÅv
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_KINGAKU_1 --ÆÒàzv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER) AS RANK_SEISAN_1 --¸ZN
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER) AS SEISAN_UCHIZEI_1 --¸ZÁïÅv(à)
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER) AS SEISAN_SOTOZEI_1 --¸ZÁïÅv(O)
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
  T_SEISAN_DENPYOU TSD 
  INNER JOIN (
	SELECT
	  TSDK.SEISAN_NUMBER                              --¸ZÔ
	  , TSDK.KAGAMI_NUMBER                            --ÓÔ
	  , TSDE.ROW_NUMBER                               --sÔ
	  , TSDE.DENPYOU_SHURUI_CD                        --`[íÞCD
	  , TSDE.DENPYOU_SYSTEM_ID                        --`[VXeID
	  , TSDE.DENPYOU_SEQ                              --`[}Ô
	  , TSDE.DETAIL_SYSTEM_ID                         --¾×VXeID
	  , TSDE.DENPYOU_NUMBER                           --`[Ô
	  , TSDE.DENPYOU_DATE                             --`[út
	  , TSDE.TORIHIKISAKI_CD AS TSDE_TORIHIKISAKI_CD  --æøæCD
	  , TSDE.GYOUSHA_CD AS TSDE_GYOUSHA_CD            --ÆÒCD
	  , TSDE.GYOUSHA_NAME1                            --ÆÒ¼1
	  , TSDE.GYOUSHA_NAME2                            --ÆÒ¼2
	  , TSDE.GENBA_CD AS TSDE_GENBA_CD                --»êCD
	  , TSDE.GENBA_NAME1                              --»ê¼1
	  , TSDE.GENBA_NAME2                              --»ê¼2
	  , TSDE.HINMEI_CD                                --i¼CD
	  , TSDE.HINMEI_NAME                              --i¼
	  , TSDE.SUURYOU                                  --Ê
	  , TSDE.UNIT_CD                                  --PÊCD
	  , TSDE.UNIT_NAME                                --PÊ¼
	  , TSDE.TANKA						              --P¿
	  , TSDE.KINGAKU                                  --àz
	  , ISNULL(TSDE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU   --àÅz
	  , ISNULL(TSDE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU   --OÅz
	  , ISNULL(TSDE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --`[àÅz
	  , ISNULL(TSDE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --`[OÅz
	  , TSDE.DENPYOU_ZEI_KBN_CD                       --`[ÅæªCD
	  , TSDE.MEISAI_ZEI_KBN_CD                        --¾×ÅæªCD
	  , TSDE.MEISAI_BIKOU                             --¾×õl
	  , TSDE.DENPYOU_ZEI_KEISAN_KBN_CD                --`[ÅvZæª
	  , TSDK.TORIHIKISAKI_CD AS TSDK_TORIHIKISAKI_CD  --æøæCD
	  , TSDK.GYOUSHA_CD AS TSDK_GYOUSHA_CD            --ÆÒCD
	  , TSDK.GENBA_CD AS TSDK_GENBA_CD                --»êCD
	  , TSDK.DAIHYOU_PRINT_KBN                        --ã\Òóæª
	  , TSDK.CORP_NAME                                --ïÐ¼
	  , TSDK.CORP_DAIHYOU                             --ã\Ò¼
	  , TSDK.KYOTEN_NAME_PRINT_KBN                    --_¼óæª
	  , TSDK.KYOTEN_CD AS TSDK_KYOTEN_CD              --_CD
	  , TSDK.KYOTEN_NAME                              --_¼
	  , TSDK.KYOTEN_DAIHYOU                           --_ã\Ò¼
	  , TSDK.KYOTEN_POST                              --_XÖÔ
	  , TSDK.KYOTEN_ADDRESS1                          --_Z1
	  , TSDK.KYOTEN_ADDRESS2                          --_Z2
	  , TSDK.KYOTEN_TEL                               --_TEL
	  , TSDK.KYOTEN_FAX                               --_FAX
	  , TSDK.BIKOU_1								  --õl1
      , TSDK.BIKOU_2								  --õl2
	  , TSDK.SHIHARAI_SOUFU_NAME1                     --x¥¾×tæ1
	  , TSDK.SHIHARAI_SOUFU_NAME2                     --x¥¾×tæ2
	  , TSDK.SHIHARAI_SOUFU_KEISHOU1                  --x¥¾×tæhÌ1
	  , TSDK.SHIHARAI_SOUFU_KEISHOU2                  --x¥¾×tæhÌ2
	  , TSDK.SHIHARAI_SOUFU_POST                      --x¥¾×tæXÖÔ
	  , TSDK.SHIHARAI_SOUFU_ADDRESS1                  --x¥¾×tæZ1
	  , TSDK.SHIHARAI_SOUFU_ADDRESS2                  --x¥¾×tæZ2
	  , TSDK.SHIHARAI_SOUFU_BUSHO                     --x¥¾×tæ
	  , TSDK.SHIHARAI_SOUFU_TANTOU                    --x¥¾×tæSÒ
	  , TSDK.SHIHARAI_SOUFU_TEL                       --x¥¾×tæTEL
	  , TSDK.SHIHARAI_SOUFU_FAX                       --x¥¾×tæFAX
	  , ISNULL(TSDK.KONKAI_SHIHARAI_GAKU,0) AS KONKAI_SHIHARAI_GAKU        --¡ñx¥z
	  , ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) AS KONKAI_SEI_UTIZEI_GAKU    --¡ñ¿àÅz
	  , ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) AS KONKAI_SEI_SOTOZEI_GAKU  --¡ñ¿OÅz
	  , ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) AS KONKAI_DEN_UTIZEI_GAKU    --¡ñ`àÅz
	  , ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) AS KONKAI_DEN_SOTOZEI_GAKU  --¡ñ`OÅz
	  , ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) AS KONKAI_MEI_UTIZEI_GAKU    --¡ñ¾àÅz
	  , ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS KONKAI_MEI_SOTOZEI_GAKU  --¡ñ¾OÅz
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --¡ñÛÅæªP
      , ISNULL(TSDK.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1		   --¡ñÛÅÅ¦P
      , ISNULL(TSDK.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1		   --¡ñÛÅÅ²àzP
      , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1	   --¡ñÛÅÅzP
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --¡ñÛÅæªQ
      , ISNULL(TSDK.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2		   --¡ñÛÅÅ¦Q
      , ISNULL(TSDK.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2		   --¡ñÛÅÅ²àzQ
      , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --¡ñÛÅÅzQ
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --¡ñÛÅæªR
      , ISNULL(TSDK.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3		   --¡ñÛÅÅ¦R
      , ISNULL(TSDK.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3		   --¡ñÛÅÅ²àzR
      , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --¡ñÛÅÅzR
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --¡ñÛÅæªS
      , ISNULL(TSDK.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4		   --¡ñÛÅÅ¦S
      , ISNULL(TSDK.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4		   --¡ñÛÅÅ²àzS
      , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --¡ñÛÅÅzS
      , ISNULL(TSDK.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN			   --¡ññÛÅæª
      , ISNULL(TSDK.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU		   --¡ññÛÅz
	FROM
		T_SEISAN_DENPYOU_KAGAMI TSDK 
		LEFT JOIN (
			/*IF shukkinMeisaiKbn != '2'*/
			SELECT
				SEISAN_NUMBER
				,KAGAMI_NUMBER
				,MAX(ROW_NUMBER) AS ROW_NUMBER
				,DENPYOU_SHURUI_CD
				,MAX(DENPYOU_SYSTEM_ID) AS DENPYOU_SYSTEM_ID
				,MAX(DENPYOU_SEQ) AS DENPYOU_SEQ
				,MAX(DETAIL_SYSTEM_ID) AS DETAIL_SYSTEM_ID
				,DENPYOU_NUMBER
				,DENPYOU_DATE
				,TORIHIKISAKI_CD
				,NULL AS GYOUSHA_CD
				,NULL AS GYOUSHA_NAME1
				,NULL AS GYOUSHA_NAME2
				,NULL AS GENBA_CD
				,NULL AS GENBA_NAME1
				,NULL AS GENBA_NAME2
				,HINMEI_CD
				,HINMEI_NAME
				,0 AS SUURYOU
				,NULL AS UNIT_CD
				,'' AS UNIT_NAME
				,0 AS TANKA
				,SUM(KINGAKU) AS KINGAKU
				,0.00 AS UCHIZEI_GAKU
				,0.00 AS SOTOZEI_GAKU
				,0.00 AS DENPYOU_UCHIZEI_GAKU
				,0.00 AS DENPYOU_SOTOZEI_GAKU
				,NULL AS DENPYOU_ZEI_KBN_CD
				,NULL AS MEISAI_ZEI_KBN_CD
				,'' AS MEISAI_BIKOU
				,DELETE_FLG
				,NULL AS DENPYOU_ZEI_KEISAN_KBN_CD
			FROM
				T_SEISAN_DETAIL TSD_SHUKKIN
			WHERE
				TSD_SHUKKIN.DENPYOU_SHURUI_CD = 20
			GROUP BY TSD_SHUKKIN.SEISAN_NUMBER,TSD_SHUKKIN.KAGAMI_NUMBER,TSD_SHUKKIN.DENPYOU_SHURUI_CD,TSD_SHUKKIN.DENPYOU_NUMBER,TSD_SHUKKIN.DENPYOU_DATE,TSD_SHUKKIN.TORIHIKISAKI_CD,TSD_SHUKKIN.HINMEI_CD,TSD_SHUKKIN.HINMEI_NAME,TSD_SHUKKIN.DELETE_FLG
			UNION
			/*END*/ 
			SELECT
				SEISAN_NUMBER
				,KAGAMI_NUMBER
				,ROW_NUMBER
				,DENPYOU_SHURUI_CD
				,DENPYOU_SYSTEM_ID
				,DENPYOU_SEQ
				,DETAIL_SYSTEM_ID
				,DENPYOU_NUMBER
				,DENPYOU_DATE
				,TORIHIKISAKI_CD
				,GYOUSHA_CD
				,GYOUSHA_NAME1
				,GYOUSHA_NAME2
				,GENBA_CD
				,GENBA_NAME1
				,GENBA_NAME2
				,HINMEI_CD
				,HINMEI_NAME
				,SUURYOU
				,UNIT_CD
				,UNIT_NAME
				,TANKA
				,KINGAKU
				,UCHIZEI_GAKU
				,SOTOZEI_GAKU
				,DENPYOU_UCHIZEI_GAKU
				,DENPYOU_SOTOZEI_GAKU
				,DENPYOU_ZEI_KBN_CD
				,MEISAI_ZEI_KBN_CD
				,MEISAI_BIKOU
				,DELETE_FLG
				,DENPYOU_ZEI_KEISAN_KBN_CD
			FROM
				T_SEISAN_DETAIL TSDE_A
			WHERE
				(TSDE_A.DENPYOU_SHURUI_CD <> 20 OR TSDE_A.DENPYOU_SHURUI_CD IS NULL)
		) TSDE ON TSDE.SEISAN_NUMBER = TSDK.SEISAN_NUMBER AND TSDE.KAGAMI_NUMBER = TSDK.KAGAMI_NUMBER
  ) TSDKE ON TSD.SEISAN_NUMBER = TSDKE.SEISAN_NUMBER
WHERE
  TSD.SEISAN_NUMBER = /*seisanNumber*/
  AND TSD.DELETE_FLG = 0
 ORDER BY
   TSDKE.KAGAMI_NUMBER
   /*$orderBy*/
  , TSDKE.DENPYOU_DATE
  , TSDKE.DENPYOU_SHURUI_CD
  , TSDKE.DENPYOU_NUMBER
  , TSDKE.ROW_NUMBER
  