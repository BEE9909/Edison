﻿SELECT
	DATA.*
FROM (
	SELECT
		RANK() OVER(ORDER BY KINGAKU DESC) AS RANK
		,RANK_DATA.*
	FROM (
		SELECT
			----金額は必須
			SUM(DENPYOU_DATA.KINGAKU) AS KINGAKU
			/*IF dto.SELECT_COLUMN != null*//*$dto.SELECT_COLUMN*/''/*END*/
		FROM (
			/*IF dto.DENPYOU_SHURUI == 1 || dto.DENPYOU_SHURUI == 5*/
			/* 受入入力 */
			SELECT
				'1' AS DUMMY
				,ET.TORIHIKISAKI_CD
				,TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME
				,ET.GYOUSHA_CD
				,GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
				,ET.GENBA_CD
				,GENBA.GENBA_NAME_RYAKU AS GENBA_NAME
				,ISNULL(ET.EIGYOU_TANTOUSHA_CD, '') AS EIGYOU_TANTOUSHA_CD
				,EIGYO_TANTOUSHA.SHAIN_NAME_RYAKU AS EIGYOU_TANTOUSHA_NAME
				,ISNULL(ET.UNTENSHA_CD, '') AS UNTENSHA_CD
				,UNTENSHA.SHAIN_NAME_RYAKU AS UNTENSHA_NAME
				,DT.HINMEI_CD
				,HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME
                ,ISNULL(HINMEI.SHURUI_CD, '') AS SHURUI_CD
                ,ISNULL(SHURUI.SHURUI_NAME_RYAKU, '') AS SHURUI_NAME
                ,ISNULL(HINMEI.BUNRUI_CD, '') AS BUNRUI_CD
                ,ISNULL(BUNRUI.BUNRUI_NAME_RYAKU, '') AS BUNRUI_NAME
				--PhuocLoc 2020/12/07 #136225 -Start
				,ISNULL(ET.MOD_SHUUKEI_KOUMOKU_CD, '') AS MOD_SHUUKEI_KOUMOKU_CD
                ,ISNULL(SHUUKEI.SHUUKEI_KOUMOKU_NAME_RYAKU, '') AS MOD_SHUUKEI_KOUMOKU_NAME 
				--PhuocLoc 2020/12/07 #136225 -End
				,(ISNULL(DT.KINGAKU, 0) + ISNULL(DT.HINMEI_KINGAKU, 0)) - (ISNULL(DT.TAX_UCHI, 0) + ISNULL(DT.HINMEI_TAX_UCHI, 0)) AS KINGAKU
			FROM
				T_UKEIRE_ENTRY ET
				INNER JOIN T_UKEIRE_DETAIL DT
					ON ET.SYSTEM_ID = DT.SYSTEM_ID
					AND ET.SEQ = DT.SEQ
					--伝票区分「売上」
					AND DT.DENPYOU_KBN_CD = 1
				LEfT JOIN M_TORIHIKISAKI TORIHIKISAKI
					ON ET.TORIHIKISAKI_CD = TORIHIKISAKI.TORIHIKISAKI_CD
				LEFT JOIN M_GYOUSHA GYOUSHA
					ON ET.GYOUSHA_CD = GYOUSHA.GYOUSHA_CD
				LEFT JOIN M_GENBA GENBA
					ON ET.GYOUSHA_CD = GENBA.GYOUSHA_CD
					AND ET.GENBA_CD = GENBA.GENBA_CD
				LEFT JOIN M_SHAIN EIGYO_TANTOUSHA
					ON ET.EIGYOU_TANTOUSHA_CD = EIGYO_TANTOUSHA.SHAIN_CD
				LEFT JOIN M_SHAIN UNTENSHA
					ON ET.UNTENSHA_CD = UNTENSHA.SHAIN_CD
				LEFT JOIN M_HINMEI HINMEI
					ON DT.HINMEI_CD = HINMEI.HINMEI_CD
                LEFT JOIN M_SHURUI SHURUI
                    ON HINMEI.SHURUI_CD = SHURUI.SHURUI_CD
                LEFT JOIN M_BUNRUI BUNRUI
                    ON HINMEI.BUNRUI_CD = BUNRUI.BUNRUI_CD
				LEFT JOIN (SELECT
								DENPYOU.SEIKYUU_NUMBER
								,DETAIL.DENPYOU_SYSTEM_ID
								,DETAIL.DENPYOU_SEQ
								,DETAIL.DETAIL_SYSTEM_ID
								,DETAIL.DENPYOU_NUMBER
							FROM T_SEIKYUU_DENPYOU AS DENPYOU
							JOIN T_SEIKYUU_DENPYOU_KAGAMI AS KAGAMI
								ON DENPYOU.SEIKYUU_NUMBER = KAGAMI.SEIKYUU_NUMBER
							JOIN T_SEIKYUU_DETAIL AS DETAIL
								ON KAGAMI.SEIKYUU_NUMBER = DETAIL.SEIKYUU_NUMBER
								AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER
							WHERE DETAIL.DENPYOU_SHURUI_CD = 1
							AND DENPYOU.DELETE_FLG = 0) AS SEIKYUU
					ON SEIKYUU.DENPYOU_SYSTEM_ID = DT.SYSTEM_ID
						AND SEIKYUU.DENPYOU_SEQ = DT.SEQ
						AND SEIKYUU.DETAIL_SYSTEM_ID = DT.DETAIL_SYSTEM_ID
						AND SEIKYUU.DENPYOU_NUMBER = DT.UKEIRE_NUMBER
				--PhuocLoc 2020/12/07 #136225 -Start
				LEFT JOIN M_SHUUKEI_KOUMOKU SHUUKEI
                    ON ET.MOD_SHUUKEI_KOUMOKU_CD = SHUUKEI.SHUUKEI_KOUMOKU_CD
				--PhuocLoc 2020/12/07 #136225 -End
			WHERE
				ET.DELETE_FLG = 0
				--滞留伝票除外
				AND ET.TAIRYUU_KBN = 0
				--拠点
				/*IF dto.KYOTEN_CD != 99*/
				AND ET.KYOTEN_CD = /*dto.KYOTEN_CD*/1
				/*END*/
				--伝票日付_FROM
				/*IF dto.DENPYOU_DATE_FROM != null && dto.DENPYOU_DATE_FROM != ''*/
				AND ET.DENPYOU_DATE >= /*dto.DENPYOU_DATE_FROM*/'2015/4/1'
				/*END*/
				--伝票日付_TO
				/*IF dto.DENPYOU_DATE_TO != null && dto.DENPYOU_DATE_TO != ''*/
				AND ET.DENPYOU_DATE <= /*dto.DENPYOU_DATE_TO*/'2015/4/1'
				/*END*/
				--売上日付_FROM
				/*IF dto.URIAGE_DATE_FROM != null && dto.URIAGE_DATE_FROM != ''*/
				AND ET.URIAGE_DATE >= /*dto.URIAGE_DATE_FROM*/'2015/4/1'
				/*END*/
				--売上日付_TO
				/*IF dto.URIAGE_DATE_TO != null && dto.URIAGE_DATE_TO != ''*/
				AND ET.URIAGE_DATE <= /*dto.URIAGE_DATE_TO*/'2015/4/1'
				/*END*/
				--入力日付_FROM
				/*IF dto.UPDATE_DATE_FROM != null && dto.UPDATE_DATE_FROM != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) >= /*dto.UPDATE_DATE_FROM*/'2015/4/1'
				/*END*/
				--入力日付_TO
				/*IF dto.UPDATE_DATE_TO != null && dto.UPDATE_DATE_TO != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) <= /*dto.UPDATE_DATE_TO*/'2015/4/1'
				/*END*/
				--取引区分
				/*IF dto.TORIHIKI_KBN != 3*/
				AND ET.URIAGE_TORIHIKI_KBN_CD = /*dto.TORIHIKI_KBN*/1
				/*END*/
				--確定区分
				/*IF dto.KAKUTEI_KBN != 3*/
				AND ET.KAKUTEI_KBN = /*dto.KAKUTEI_KBN*/1
				/*END*/
				--締処理状況
				/*IF dto.SHIME_JOKYO == 1*/
				AND SEIKYUU.SEIKYUU_NUMBER IS NOT NULL
				/*END*/
				/*IF dto.SHIME_JOKYO == 2*/
				AND SEIKYUU.SEIKYUU_NUMBER IS NULL
				/*END*/
				--取引先_FROM
				/*IF dto.TORIHIKISAKI_CD_FROM != null && dto.TORIHIKISAKI_CD_FROM != ''*/
				AND ET.TORIHIKISAKI_CD >= /*dto.TORIHIKISAKI_CD_FROM*/'000001'
				/*END*/
				--取引先_TO
				/*IF dto.TORIHIKISAKI_CD_TO != null && dto.TORIHIKISAKI_CD_TO != ''*/
				AND ET.TORIHIKISAKI_CD <= /*dto.TORIHIKISAKI_CD_TO*/'000001'
				/*END*/
				--業者_FROM
				/*IF dto.GYOUSHA_CD_FROM != null && dto.GYOUSHA_CD_FROM != ''*/
				AND ET.GYOUSHA_CD >= /*dto.GYOUSHA_CD_FROM*/'000001'
				/*END*/
				--業者_TO
				/*IF dto.GYOUSHA_CD_TO != null && dto.GYOUSHA_CD_TO != ''*/
				AND ET.GYOUSHA_CD <= /*dto.GYOUSHA_CD_TO*/'000001'
				/*END*/
				--現場_FROM
				/*IF dto.GENBA_CD_FROM != null && dto.GENBA_CD_FROM != ''*/
				AND ET.GENBA_CD >= /*dto.GENBA_CD_FROM*/'000001'
				/*END*/
				--現場_TO
				/*IF dto.GENBA_CD_TO != null && dto.GENBA_CD_TO != ''*/
				AND ET.GENBA_CD <= /*dto.GENBA_CD_TO*/'000001'
				/*END*/
				--営業者_FROM
				/*IF dto.EIGYOU_TANTOUSHA_CD_FROM != null && dto.EIGYOU_TANTOUSHA_CD_FROM != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD >= /*dto.EIGYOU_TANTOUSHA_CD_FROM*/'000001'
				/*END*/
				--営業者_TO
				/*IF dto.EIGYOU_TANTOUSHA_CD_TO != null && dto.EIGYOU_TANTOUSHA_CD_TO != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD <= /*dto.EIGYOU_TANTOUSHA_CD_TO*/'000001'
				/*END*/
				--運転者_FROM
				/*IF dto.UNTENSHA_CD_FROM != null && dto.UNTENSHA_CD_FROM != ''*/
				AND ET.UNTENSHA_CD >= /*dto.UNTENSHA_CD_FROM*/'000001'
				/*END*/
				--運転者_TO
				/*IF dto.UNTENSHA_CD_TO != null && dto.UNTENSHA_CD_TO != ''*/
				AND ET.UNTENSHA_CD <= /*dto.UNTENSHA_CD_TO*/'000001'
				/*END*/
				--品名_FROM
				/*IF dto.HINMEI_CD_FROM != null && dto.HINMEI_CD_FROM != ''*/
				AND DT.HINMEI_CD >= /*dto.HINMEI_CD_FROM*/'000001'
				/*END*/
				--品名_TO
				/*IF dto.HINMEI_CD_TO != null && dto.HINMEI_CD_TO != ''*/
				AND DT.HINMEI_CD <= /*dto.HINMEI_CD_TO*/'000001'
				/*END*/
                --種類_FROM
				/*IF dto.SHURUI_CD_FROM != null && dto.SHURUI_CD_FROM != ''*/
				AND HINMEI.SHURUI_CD >= /*dto.SHURUI_CD_FROM*/'000001'
				/*END*/
				--種類_TO
				/*IF dto.SHURUI_CD_TO != null && dto.SHURUI_CD_TO != ''*/
				AND HINMEI.SHURUI_CD <= /*dto.SHURUI_CD_TO*/'000001'
				/*END*/
				--分類_FROM
				/*IF dto.BUNRUI_CD_FROM != null && dto.BUNRUI_CD_FROM != ''*/
				AND HINMEI.BUNRUI_CD >= /*dto.BUNRUI_CD_FROM*/'000001'
				/*END*/
				--分類_TO
				/*IF dto.BUNRUI_CD_TO != null && dto.BUNRUI_CD_TO != ''*/
				AND HINMEI.BUNRUI_CD <= /*dto.BUNRUI_CD_TO*/'000001'
				/*END*/
				--PhuocLoc 2020/12/07 #136225 -Start
				--集計項目_FROM
				/*IF dto.SHUUKEI_KOUMOKU_CD_FROM != null && dto.SHUUKEI_KOUMOKU_CD_FROM != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD >= /*dto.SHUUKEI_KOUMOKU_CD_FROM*/'000001'
				/*END*/
				--集計項目_TO
				/*IF dto.SHUUKEI_KOUMOKU_CD_TO != null && dto.SHUUKEI_KOUMOKU_CD_TO != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD <= /*dto.SHUUKEI_KOUMOKU_CD_TO*/'000001'
				/*END*/
				--PhuocLoc 2020/12/07 #136225 -End
			/*END*/

			/*IF dto.DENPYOU_SHURUI == 5*/
			UNION ALL
			/*END*/

			/*IF dto.DENPYOU_SHURUI == 2 || dto.DENPYOU_SHURUI == 5*/
			/* 出荷入力 */
			SELECT
				'1' AS DUMMY
				,ET.TORIHIKISAKI_CD
				,TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME
				,ET.GYOUSHA_CD
				,GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
				,ET.GENBA_CD
				,GENBA.GENBA_NAME_RYAKU AS GENBA_NAME
				,ISNULL(ET.EIGYOU_TANTOUSHA_CD, '') AS EIGYOU_TANTOUSHA_CD
				,EIGYO_TANTOUSHA.SHAIN_NAME_RYAKU AS EIGYOU_TANTOUSHA_NAME
				,ISNULL(ET.UNTENSHA_CD, '') AS UNTENSHA_CD
				,UNTENSHA.SHAIN_NAME_RYAKU AS UNTENSHA_NAME
				,DT.HINMEI_CD
				,HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME
                ,ISNULL(HINMEI.SHURUI_CD, '') AS SHURUI_CD
                ,ISNULL(SHURUI.SHURUI_NAME_RYAKU, '') AS SHURUI_NAME
                ,ISNULL(HINMEI.BUNRUI_CD, '') AS BUNRUI_CD
                ,ISNULL(BUNRUI.BUNRUI_NAME_RYAKU, '') AS BUNRUI_NAME
                --PhuocLoc 2020/12/07 #136225 -Start
				,ISNULL(ET.MOD_SHUUKEI_KOUMOKU_CD, '') AS MOD_SHUUKEI_KOUMOKU_CD
                ,ISNULL(SHUUKEI.SHUUKEI_KOUMOKU_NAME_RYAKU, '') AS MOD_SHUUKEI_KOUMOKU_NAME 
                --PhuocLoc 2020/12/07 #136225 -End
				,(ISNULL(DT.KINGAKU, 0) + ISNULL(DT.HINMEI_KINGAKU, 0)) - (ISNULL(DT.TAX_UCHI, 0) + ISNULL(DT.HINMEI_TAX_UCHI, 0)) AS KINGAKU
			FROM
				T_SHUKKA_ENTRY ET
				INNER JOIN T_SHUKKA_DETAIL DT
					ON ET.SYSTEM_ID = DT.SYSTEM_ID
					AND ET.SEQ = DT.SEQ
					--伝票区分「売上」
					AND DT.DENPYOU_KBN_CD = 1
				LEfT JOIN M_TORIHIKISAKI TORIHIKISAKI
					ON ET.TORIHIKISAKI_CD = TORIHIKISAKI.TORIHIKISAKI_CD
				LEFT JOIN M_GYOUSHA GYOUSHA
					ON ET.GYOUSHA_CD = GYOUSHA.GYOUSHA_CD
				LEFT JOIN M_GENBA GENBA
					ON ET.GYOUSHA_CD = GENBA.GYOUSHA_CD
					AND ET.GENBA_CD = GENBA.GENBA_CD
				LEFT JOIN M_SHAIN EIGYO_TANTOUSHA
					ON ET.EIGYOU_TANTOUSHA_CD = EIGYO_TANTOUSHA.SHAIN_CD
				LEFT JOIN M_SHAIN UNTENSHA
					ON ET.UNTENSHA_CD = UNTENSHA.SHAIN_CD
				LEFT JOIN M_HINMEI HINMEI
					ON DT.HINMEI_CD = HINMEI.HINMEI_CD
                LEFT JOIN M_SHURUI SHURUI
                    ON HINMEI.SHURUI_CD = SHURUI.SHURUI_CD
                LEFT JOIN M_BUNRUI BUNRUI
                    ON HINMEI.BUNRUI_CD = BUNRUI.BUNRUI_CD
				LEFT JOIN (SELECT
								DENPYOU.SEIKYUU_NUMBER
								,DETAIL.DENPYOU_SYSTEM_ID
								,DETAIL.DENPYOU_SEQ
								,DETAIL.DETAIL_SYSTEM_ID
								,DETAIL.DENPYOU_NUMBER
							FROM T_SEIKYUU_DENPYOU AS DENPYOU
							JOIN T_SEIKYUU_DENPYOU_KAGAMI AS KAGAMI
								ON DENPYOU.SEIKYUU_NUMBER = KAGAMI.SEIKYUU_NUMBER
							JOIN T_SEIKYUU_DETAIL AS DETAIL
								ON KAGAMI.SEIKYUU_NUMBER = DETAIL.SEIKYUU_NUMBER
								AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER
							WHERE DETAIL.DENPYOU_SHURUI_CD = 2
							AND DENPYOU.DELETE_FLG = 0) AS SEIKYUU
					ON SEIKYUU.DENPYOU_SYSTEM_ID = DT.SYSTEM_ID
						AND SEIKYUU.DENPYOU_SEQ = DT.SEQ
						AND SEIKYUU.DETAIL_SYSTEM_ID = DT.DETAIL_SYSTEM_ID
						AND SEIKYUU.DENPYOU_NUMBER = DT.SHUKKA_NUMBER
				--PhuocLoc 2020/12/07 #136225 -Start
				LEFT JOIN M_SHUUKEI_KOUMOKU SHUUKEI
                    ON ET.MOD_SHUUKEI_KOUMOKU_CD = SHUUKEI.SHUUKEI_KOUMOKU_CD
                --PhuocLoc 2020/12/07 #136225 -End
			WHERE
				ET.DELETE_FLG = 0
				--検収区分：検収入力なし
				AND ET.KENSHU_MUST_KBN = 0
				--滞留伝票除外
				AND ET.TAIRYUU_KBN = 0
				--拠点
				/*IF dto.KYOTEN_CD != 99*/
				AND ET.KYOTEN_CD = /*dto.KYOTEN_CD*/1
				/*END*/
				--伝票日付_FROM
				/*IF dto.DENPYOU_DATE_FROM != null && dto.DENPYOU_DATE_FROM != ''*/
				AND ET.DENPYOU_DATE >= /*dto.DENPYOU_DATE_FROM*/'2015/4/1'
				/*END*/
				--伝票日付_TO
				/*IF dto.DENPYOU_DATE_TO != null && dto.DENPYOU_DATE_TO != ''*/
				AND ET.DENPYOU_DATE <= /*dto.DENPYOU_DATE_TO*/'2015/4/1'
				/*END*/
				--売上日付_FROM
				/*IF dto.URIAGE_DATE_FROM != null && dto.URIAGE_DATE_FROM != ''*/
				AND ET.URIAGE_DATE >= /*dto.URIAGE_DATE_FROM*/'2015/4/1'
				/*END*/
				--売上日付_TO
				/*IF dto.URIAGE_DATE_TO != null && dto.URIAGE_DATE_TO != ''*/
				AND ET.URIAGE_DATE <= /*dto.URIAGE_DATE_TO*/'2015/4/1'
				/*END*/
				--入力日付_FROM
				/*IF dto.UPDATE_DATE_FROM != null && dto.UPDATE_DATE_FROM != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) >= /*dto.UPDATE_DATE_FROM*/'2015/4/1'
				/*END*/
				--入力日付_TO
				/*IF dto.UPDATE_DATE_TO != null && dto.UPDATE_DATE_TO != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) <= /*dto.UPDATE_DATE_TO*/'2015/4/1'
				/*END*/
				--取引区分
				/*IF dto.TORIHIKI_KBN != 3*/
				AND ET.URIAGE_TORIHIKI_KBN_CD = /*dto.TORIHIKI_KBN*/1
				/*END*/
				--確定区分
				/*IF dto.KAKUTEI_KBN != 3*/
				AND ET.KAKUTEI_KBN = /*dto.KAKUTEI_KBN*/1
				/*END*/
				--締処理状況
				/*IF dto.SHIME_JOKYO == 1*/
				AND SEIKYUU.SEIKYUU_NUMBER IS NOT NULL
				/*END*/
				/*IF dto.SHIME_JOKYO == 2*/
				AND SEIKYUU.SEIKYUU_NUMBER IS NULL
				/*END*/
				--取引先_FROM
				/*IF dto.TORIHIKISAKI_CD_FROM != null && dto.TORIHIKISAKI_CD_FROM != ''*/
				AND ET.TORIHIKISAKI_CD >= /*dto.TORIHIKISAKI_CD_FROM*/'000001'
				/*END*/
				--取引先_TO
				/*IF dto.TORIHIKISAKI_CD_TO != null && dto.TORIHIKISAKI_CD_TO != ''*/
				AND ET.TORIHIKISAKI_CD <= /*dto.TORIHIKISAKI_CD_TO*/'000001'
				/*END*/
				--業者_FROM
				/*IF dto.GYOUSHA_CD_FROM != null && dto.GYOUSHA_CD_FROM != ''*/
				AND ET.GYOUSHA_CD >= /*dto.GYOUSHA_CD_FROM*/'000001'
				/*END*/
				--業者_TO
				/*IF dto.GYOUSHA_CD_TO != null && dto.GYOUSHA_CD_TO != ''*/
				AND ET.GYOUSHA_CD <= /*dto.GYOUSHA_CD_TO*/'000001'
				/*END*/
				--現場_FROM
				/*IF dto.GENBA_CD_FROM != null && dto.GENBA_CD_FROM != ''*/
				AND ET.GENBA_CD >= /*dto.GENBA_CD_FROM*/'000001'
				/*END*/
				--現場_TO
				/*IF dto.GENBA_CD_TO != null && dto.GENBA_CD_TO != ''*/
				AND ET.GENBA_CD <= /*dto.GENBA_CD_TO*/'000001'
				/*END*/
				--営業者_FROM
				/*IF dto.EIGYOU_TANTOUSHA_CD_FROM != null && dto.EIGYOU_TANTOUSHA_CD_FROM != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD >= /*dto.EIGYOU_TANTOUSHA_CD_FROM*/'000001'
				/*END*/
				--営業者_TO
				/*IF dto.EIGYOU_TANTOUSHA_CD_TO != null && dto.EIGYOU_TANTOUSHA_CD_TO != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD <= /*dto.EIGYOU_TANTOUSHA_CD_TO*/'000001'
				/*END*/
				--運転者_FROM
				/*IF dto.UNTENSHA_CD_FROM != null && dto.UNTENSHA_CD_FROM != ''*/
				AND ET.UNTENSHA_CD >= /*dto.UNTENSHA_CD_FROM*/'000001'
				/*END*/
				--運転者_TO
				/*IF dto.UNTENSHA_CD_TO != null && dto.UNTENSHA_CD_TO != ''*/
				AND ET.UNTENSHA_CD <= /*dto.UNTENSHA_CD_TO*/'000001'
				/*END*/
				--品名_FROM
				/*IF dto.HINMEI_CD_FROM != null && dto.HINMEI_CD_FROM != ''*/
				AND DT.HINMEI_CD >= /*dto.HINMEI_CD_FROM*/'000001'
				/*END*/
				--品名_TO
				/*IF dto.HINMEI_CD_TO != null && dto.HINMEI_CD_TO != ''*/
				AND DT.HINMEI_CD <= /*dto.HINMEI_CD_TO*/'000001'
				/*END*/
                --種類_FROM
				/*IF dto.SHURUI_CD_FROM != null && dto.SHURUI_CD_FROM != ''*/
				AND HINMEI.SHURUI_CD >= /*dto.SHURUI_CD_FROM*/'000001'
				/*END*/
				--種類_TO
				/*IF dto.SHURUI_CD_TO != null && dto.SHURUI_CD_TO != ''*/
				AND HINMEI.SHURUI_CD <= /*dto.SHURUI_CD_TO*/'000001'
				/*END*/
				--分類_FROM
				/*IF dto.BUNRUI_CD_FROM != null && dto.BUNRUI_CD_FROM != ''*/
				AND HINMEI.BUNRUI_CD >= /*dto.BUNRUI_CD_FROM*/'000001'
				/*END*/
				--分類_TO
				/*IF dto.BUNRUI_CD_TO != null && dto.BUNRUI_CD_TO != ''*/
				AND HINMEI.BUNRUI_CD <= /*dto.BUNRUI_CD_TO*/'000001'
				/*END*/
				--PhuocLoc 2020/12/07 #136225 -Start
				--集計項目_FROM
				/*IF dto.SHUUKEI_KOUMOKU_CD_FROM != null && dto.SHUUKEI_KOUMOKU_CD_FROM != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD >= /*dto.SHUUKEI_KOUMOKU_CD_FROM*/'000001'
				/*END*/
				--集計項目_TO
				/*IF dto.SHUUKEI_KOUMOKU_CD_TO != null && dto.SHUUKEI_KOUMOKU_CD_TO != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD <= /*dto.SHUUKEI_KOUMOKU_CD_TO*/'000001'
				/*END*/
				--PhuocLoc 2020/12/07 #136225 -End

			UNION ALL

			/* 出荷入力(検収) */
			SELECT
				'1' AS DUMMY
				,ET.TORIHIKISAKI_CD
				,TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME
				,ET.GYOUSHA_CD
				,GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
				,ET.GENBA_CD
				,GENBA.GENBA_NAME_RYAKU AS GENBA_NAME
				,ISNULL(ET.EIGYOU_TANTOUSHA_CD, '') AS EIGYOU_TANTOUSHA_CD
				,EIGYO_TANTOUSHA.SHAIN_NAME_RYAKU AS EIGYOU_TANTOUSHA_NAME
				,ISNULL(ET.UNTENSHA_CD, '') AS UNTENSHA_CD
				,UNTENSHA.SHAIN_NAME_RYAKU AS UNTENSHA_NAME
				,DT.HINMEI_CD
				,HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME
                ,ISNULL(HINMEI.SHURUI_CD, '') AS SHURUI_CD
                ,ISNULL(SHURUI.SHURUI_NAME_RYAKU, '') AS SHURUI_NAME
                ,ISNULL(HINMEI.BUNRUI_CD, '') AS BUNRUI_CD
                ,ISNULL(BUNRUI.BUNRUI_NAME_RYAKU, '') AS BUNRUI_NAME
                --PhuocLoc 2020/12/07 #136225 -Start
				,ISNULL(ET.MOD_SHUUKEI_KOUMOKU_CD, '') AS MOD_SHUUKEI_KOUMOKU_CD
                ,ISNULL(SHUUKEI.SHUUKEI_KOUMOKU_NAME_RYAKU, '') AS MOD_SHUUKEI_KOUMOKU_NAME 
                --PhuocLoc 2020/12/07 #136225 -End
				,(ISNULL(DT.KINGAKU, 0) + ISNULL(DT.HINMEI_KINGAKU, 0)) - (ISNULL(DT.TAX_UCHI, 0) + ISNULL(DT.HINMEI_TAX_UCHI, 0)) AS KINGAKU
			FROM
				T_SHUKKA_ENTRY ET
				INNER JOIN (
					SELECT
						T_KENSHU_DETAIL.*
					FROM
						T_SHUKKA_DETAIL
						INNER JOIN T_KENSHU_DETAIL
							ON T_SHUKKA_DETAIL.SYSTEM_ID = T_KENSHU_DETAIL.SYSTEM_ID
							AND T_SHUKKA_DETAIL.SEQ = T_KENSHU_DETAIL.SEQ
							AND T_SHUKKA_DETAIL.DETAIL_SYSTEM_ID = T_KENSHU_DETAIL.DETAIL_SYSTEM_ID
				) AS DT
					ON ET.SYSTEM_ID = DT.SYSTEM_ID
					AND ET.SEQ = DT.SEQ
					--伝票区分「売上」
					AND DT.DENPYOU_KBN_CD = 1
				LEfT JOIN M_TORIHIKISAKI TORIHIKISAKI
					ON ET.TORIHIKISAKI_CD = TORIHIKISAKI.TORIHIKISAKI_CD
				LEFT JOIN M_GYOUSHA GYOUSHA
					ON ET.GYOUSHA_CD = GYOUSHA.GYOUSHA_CD
				LEFT JOIN M_GENBA GENBA
					ON ET.GYOUSHA_CD = GENBA.GYOUSHA_CD
					AND ET.GENBA_CD = GENBA.GENBA_CD
				LEFT JOIN M_SHAIN EIGYO_TANTOUSHA
					ON ET.EIGYOU_TANTOUSHA_CD = EIGYO_TANTOUSHA.SHAIN_CD
				LEFT JOIN M_SHAIN UNTENSHA
					ON ET.UNTENSHA_CD = UNTENSHA.SHAIN_CD
				LEFT JOIN M_HINMEI HINMEI
					ON DT.HINMEI_CD = HINMEI.HINMEI_CD
                LEFT JOIN M_SHURUI SHURUI
                    ON HINMEI.SHURUI_CD = SHURUI.SHURUI_CD
                LEFT JOIN M_BUNRUI BUNRUI
                    ON HINMEI.BUNRUI_CD = BUNRUI.BUNRUI_CD
                --PhuocLoc 2020/12/07 #136225 -Start
				LEFT JOIN M_SHUUKEI_KOUMOKU SHUUKEI
                    ON ET.MOD_SHUUKEI_KOUMOKU_CD = SHUUKEI.SHUUKEI_KOUMOKU_CD 
                --PhuocLoc 2020/12/07 #136225 -End
			WHERE
				ET.DELETE_FLG = 0
				--検収区分：検収入力あり
				AND ET.KENSHU_MUST_KBN = 1
				--滞留伝票除外
				AND ET.TAIRYUU_KBN = 0
				--拠点
				/*IF dto.KYOTEN_CD != 99*/
				AND ET.KYOTEN_CD = /*dto.KYOTEN_CD*/1
				/*END*/
				--伝票日付_FROM
				/*IF dto.DENPYOU_DATE_FROM != null && dto.DENPYOU_DATE_FROM != ''*/
				AND ET.KENSHU_DATE >= /*dto.DENPYOU_DATE_FROM*/'2015/4/1'
				/*END*/
				--伝票日付_TO
				/*IF dto.DENPYOU_DATE_TO != null && dto.DENPYOU_DATE_TO != ''*/
				AND ET.KENSHU_DATE <= /*dto.DENPYOU_DATE_TO*/'2015/4/1'
				/*END*/
				--売上日付_FROM
				/*IF dto.URIAGE_DATE_FROM != null && dto.URIAGE_DATE_FROM != ''*/
				AND ET.KENSHU_URIAGE_DATE >= /*dto.URIAGE_DATE_FROM*/'2015/4/1'
				/*END*/
				--売上日付_TO
				/*IF dto.URIAGE_DATE_TO != null && dto.URIAGE_DATE_TO != ''*/
				AND ET.KENSHU_URIAGE_DATE <= /*dto.URIAGE_DATE_TO*/'2015/4/1'
				/*END*/
				--入力日付_FROM
				/*IF dto.UPDATE_DATE_FROM != null && dto.UPDATE_DATE_FROM != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) >= /*dto.UPDATE_DATE_FROM*/'2015/4/1'
				/*END*/
				--入力日付_TO
				/*IF dto.UPDATE_DATE_TO != null && dto.UPDATE_DATE_TO != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) <= /*dto.UPDATE_DATE_TO*/'2015/4/1'
				/*END*/
				--取引区分
				/*IF dto.TORIHIKI_KBN != 3*/
				AND ET.URIAGE_TORIHIKI_KBN_CD = /*dto.TORIHIKI_KBN*/1
				/*END*/
				--確定区分
				/*IF dto.KAKUTEI_KBN != 3*/
				AND ET.KAKUTEI_KBN = /*dto.KAKUTEI_KBN*/1
				/*END*/
				--締処理状況
				/*IF dto.SHIME_JOKYO == 1*/
				AND EXISTS
					(
						SELECT * FROM
							(
								SELECT
									DENPYOU.SEIKYUU_NUMBER
									,DETAIL.DENPYOU_SYSTEM_ID
									,DETAIL.DENPYOU_SEQ
									,DETAIL.DETAIL_SYSTEM_ID
									,DETAIL.DENPYOU_NUMBER
								FROM T_SEIKYUU_DENPYOU AS DENPYOU
								JOIN T_SEIKYUU_DENPYOU_KAGAMI AS KAGAMI
									ON DENPYOU.SEIKYUU_NUMBER = KAGAMI.SEIKYUU_NUMBER
								JOIN T_SEIKYUU_DETAIL AS DETAIL
									ON KAGAMI.SEIKYUU_NUMBER = DETAIL.SEIKYUU_NUMBER
									AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER
								WHERE DETAIL.DENPYOU_SHURUI_CD = 2
									AND DENPYOU.DELETE_FLG = 0
							) AS SEIKYUU
						WHERE SEIKYUU.DENPYOU_SYSTEM_ID = DT.SYSTEM_ID
							AND SEIKYUU.DENPYOU_SEQ = DT.SEQ
							AND SEIKYUU.DETAIL_SYSTEM_ID = DT.DETAIL_SYSTEM_ID
							AND SEIKYUU.DENPYOU_NUMBER = DT.SHUKKA_NUMBER
					)
				/*END*/
				/*IF dto.SHIME_JOKYO == 2*/
				AND NOT EXISTS
					(
						SELECT * FROM
							(
								SELECT
									DENPYOU.SEIKYUU_NUMBER
									,DETAIL.DENPYOU_SYSTEM_ID
									,DETAIL.DENPYOU_SEQ
									,DETAIL.DETAIL_SYSTEM_ID
									,DETAIL.DENPYOU_NUMBER
								FROM T_SEIKYUU_DENPYOU AS DENPYOU
								JOIN T_SEIKYUU_DENPYOU_KAGAMI AS KAGAMI
									ON DENPYOU.SEIKYUU_NUMBER = KAGAMI.SEIKYUU_NUMBER
								JOIN T_SEIKYUU_DETAIL AS DETAIL
									ON KAGAMI.SEIKYUU_NUMBER = DETAIL.SEIKYUU_NUMBER
									AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER
								WHERE DETAIL.DENPYOU_SHURUI_CD = 2
									AND DENPYOU.DELETE_FLG = 0
							) AS SEIKYUU
						WHERE SEIKYUU.DENPYOU_SYSTEM_ID = DT.SYSTEM_ID
							AND SEIKYUU.DENPYOU_SEQ = DT.SEQ
							AND SEIKYUU.DETAIL_SYSTEM_ID = DT.DETAIL_SYSTEM_ID
							AND SEIKYUU.DENPYOU_NUMBER = DT.SHUKKA_NUMBER
					)
				/*END*/
				--取引先_FROM
				/*IF dto.TORIHIKISAKI_CD_FROM != null && dto.TORIHIKISAKI_CD_FROM != ''*/
				AND ET.TORIHIKISAKI_CD >= /*dto.TORIHIKISAKI_CD_FROM*/'000001'
				/*END*/
				--取引先_TO
				/*IF dto.TORIHIKISAKI_CD_TO != null && dto.TORIHIKISAKI_CD_TO != ''*/
				AND ET.TORIHIKISAKI_CD <= /*dto.TORIHIKISAKI_CD_TO*/'000001'
				/*END*/
				--業者_FROM
				/*IF dto.GYOUSHA_CD_FROM != null && dto.GYOUSHA_CD_FROM != ''*/
				AND ET.GYOUSHA_CD >= /*dto.GYOUSHA_CD_FROM*/'000001'
				/*END*/
				--業者_TO
				/*IF dto.GYOUSHA_CD_TO != null && dto.GYOUSHA_CD_TO != ''*/
				AND ET.GYOUSHA_CD <= /*dto.GYOUSHA_CD_TO*/'000001'
				/*END*/
				--現場_FROM
				/*IF dto.GENBA_CD_FROM != null && dto.GENBA_CD_FROM != ''*/
				AND ET.GENBA_CD >= /*dto.GENBA_CD_FROM*/'000001'
				/*END*/
				--現場_TO
				/*IF dto.GENBA_CD_TO != null && dto.GENBA_CD_TO != ''*/
				AND ET.GENBA_CD <= /*dto.GENBA_CD_TO*/'000001'
				/*END*/
				--営業者_FROM
				/*IF dto.EIGYOU_TANTOUSHA_CD_FROM != null && dto.EIGYOU_TANTOUSHA_CD_FROM != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD >= /*dto.EIGYOU_TANTOUSHA_CD_FROM*/'000001'
				/*END*/
				--営業者_TO
				/*IF dto.EIGYOU_TANTOUSHA_CD_TO != null && dto.EIGYOU_TANTOUSHA_CD_TO != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD <= /*dto.EIGYOU_TANTOUSHA_CD_TO*/'000001'
				/*END*/
				--運転者_FROM
				/*IF dto.UNTENSHA_CD_FROM != null && dto.UNTENSHA_CD_FROM != ''*/
				AND ET.UNTENSHA_CD >= /*dto.UNTENSHA_CD_FROM*/'000001'
				/*END*/
				--運転者_TO
				/*IF dto.UNTENSHA_CD_TO != null && dto.UNTENSHA_CD_TO != ''*/
				AND ET.UNTENSHA_CD <= /*dto.UNTENSHA_CD_TO*/'000001'
				/*END*/
				--品名_FROM
				/*IF dto.HINMEI_CD_FROM != null && dto.HINMEI_CD_FROM != ''*/
				AND DT.HINMEI_CD >= /*dto.HINMEI_CD_FROM*/'000001'
				/*END*/
				--品名_TO
				/*IF dto.HINMEI_CD_TO != null && dto.HINMEI_CD_TO != ''*/
				AND DT.HINMEI_CD <= /*dto.HINMEI_CD_TO*/'000001'
				/*END*/
                --種類_FROM
				/*IF dto.SHURUI_CD_FROM != null && dto.SHURUI_CD_FROM != ''*/
				AND HINMEI.SHURUI_CD >= /*dto.SHURUI_CD_FROM*/'000001'
				/*END*/
				--種類_TO
				/*IF dto.SHURUI_CD_TO != null && dto.SHURUI_CD_TO != ''*/
				AND HINMEI.SHURUI_CD <= /*dto.SHURUI_CD_TO*/'000001'
				/*END*/
				--分類_FROM
				/*IF dto.BUNRUI_CD_FROM != null && dto.BUNRUI_CD_FROM != ''*/
				AND HINMEI.BUNRUI_CD >= /*dto.BUNRUI_CD_FROM*/'000001'
				/*END*/
				--分類_TO
				/*IF dto.BUNRUI_CD_TO != null && dto.BUNRUI_CD_TO != ''*/
				AND HINMEI.BUNRUI_CD <= /*dto.BUNRUI_CD_TO*/'000001'
				/*END*/
				--PhuocLoc 2020/12/07 #136225 -Start
				--集計項目_FROM
				/*IF dto.SHUUKEI_KOUMOKU_CD_FROM != null && dto.SHUUKEI_KOUMOKU_CD_FROM != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD >= /*dto.SHUUKEI_KOUMOKU_CD_FROM*/'000001'
				/*END*/
				--集計項目_TO
				/*IF dto.SHUUKEI_KOUMOKU_CD_TO != null && dto.SHUUKEI_KOUMOKU_CD_TO != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD <= /*dto.SHUUKEI_KOUMOKU_CD_TO*/'000001'
				/*END*/
				--PhuocLoc 2020/12/07 #136225 -End
			/*END*/

			/*IF dto.DENPYOU_SHURUI == 5*/
			UNION ALL
			/*END*/

			-- 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
			/*IF dto.DENPYOU_SHURUI == 3 || dto.DENPYOU_SHURUI == 4 || dto.DENPYOU_SHURUI == 5*/
			-- 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
			/* 売上/支払入力 */
			SELECT
				'1' AS DUMMY
				,ET.TORIHIKISAKI_CD
				,TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME
				,ET.GYOUSHA_CD
				,GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
				,ET.GENBA_CD
				,GENBA.GENBA_NAME_RYAKU AS GENBA_NAME
				,ISNULL(ET.EIGYOU_TANTOUSHA_CD, '') AS EIGYOU_TANTOUSHA_CD
				,EIGYO_TANTOUSHA.SHAIN_NAME_RYAKU AS EIGYOU_TANTOUSHA_NAME
				,ISNULL(ET.UNTENSHA_CD, '') AS UNTENSHA_CD
				,UNTENSHA.SHAIN_NAME_RYAKU AS UNTENSHA_NAME
				,DT.HINMEI_CD
				,HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME
                ,ISNULL(HINMEI.SHURUI_CD, '') AS SHURUI_CD
                ,ISNULL(SHURUI.SHURUI_NAME_RYAKU, '') AS SHURUI_NAME
                ,ISNULL(HINMEI.BUNRUI_CD, '') AS BUNRUI_CD
                ,ISNULL(BUNRUI.BUNRUI_NAME_RYAKU, '') AS BUNRUI_NAME
                --PhuocLoc 2020/12/07 #136225 -Start
				,ISNULL(ET.MOD_SHUUKEI_KOUMOKU_CD, '') AS MOD_SHUUKEI_KOUMOKU_CD
                ,ISNULL(SHUUKEI.SHUUKEI_KOUMOKU_NAME_RYAKU, '') AS MOD_SHUUKEI_KOUMOKU_NAME 
                --PhuocLoc 2020/12/07 #136225 -End
				,(ISNULL(DT.KINGAKU, 0) + ISNULL(DT.HINMEI_KINGAKU, 0)) - (ISNULL(DT.TAX_UCHI, 0) + ISNULL(DT.HINMEI_TAX_UCHI, 0)) AS KINGAKU
			FROM
				T_UR_SH_ENTRY ET
				INNER JOIN T_UR_SH_DETAIL DT
					ON ET.SYSTEM_ID = DT.SYSTEM_ID
					AND ET.SEQ = DT.SEQ
					--伝票区分「売上」
					AND DT.DENPYOU_KBN_CD = 1
				LEfT JOIN M_TORIHIKISAKI TORIHIKISAKI
					ON ET.TORIHIKISAKI_CD = TORIHIKISAKI.TORIHIKISAKI_CD
				LEFT JOIN M_GYOUSHA GYOUSHA
					ON ET.GYOUSHA_CD = GYOUSHA.GYOUSHA_CD
				LEFT JOIN M_GENBA GENBA
					ON ET.GYOUSHA_CD = GENBA.GYOUSHA_CD
					AND ET.GENBA_CD = GENBA.GENBA_CD
				LEFT JOIN M_SHAIN EIGYO_TANTOUSHA
					ON ET.EIGYOU_TANTOUSHA_CD = EIGYO_TANTOUSHA.SHAIN_CD
				LEFT JOIN M_SHAIN UNTENSHA
					ON ET.UNTENSHA_CD = UNTENSHA.SHAIN_CD
				LEFT JOIN M_HINMEI HINMEI
					ON DT.HINMEI_CD = HINMEI.HINMEI_CD
                LEFT JOIN M_SHURUI SHURUI
                    ON HINMEI.SHURUI_CD = SHURUI.SHURUI_CD
                LEFT JOIN M_BUNRUI BUNRUI
                    ON HINMEI.BUNRUI_CD = BUNRUI.BUNRUI_CD
				LEFT JOIN (SELECT
								DENPYOU.SEIKYUU_NUMBER
								,DETAIL.DENPYOU_SYSTEM_ID
								,DETAIL.DENPYOU_SEQ
								,DETAIL.DETAIL_SYSTEM_ID
								,DETAIL.DENPYOU_NUMBER
							FROM T_SEIKYUU_DENPYOU AS DENPYOU
							JOIN T_SEIKYUU_DENPYOU_KAGAMI AS KAGAMI
								ON DENPYOU.SEIKYUU_NUMBER = KAGAMI.SEIKYUU_NUMBER
							JOIN T_SEIKYUU_DETAIL AS DETAIL
								ON KAGAMI.SEIKYUU_NUMBER = DETAIL.SEIKYUU_NUMBER
								AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER
							WHERE DETAIL.DENPYOU_SHURUI_CD = 3
							AND DENPYOU.DELETE_FLG = 0) AS SEIKYUU
					ON SEIKYUU.DENPYOU_SYSTEM_ID = DT.SYSTEM_ID
						AND SEIKYUU.DENPYOU_SEQ = DT.SEQ
						AND SEIKYUU.DETAIL_SYSTEM_ID = DT.DETAIL_SYSTEM_ID
						AND SEIKYUU.DENPYOU_NUMBER = DT.UR_SH_NUMBER
				--PhuocLoc 2020/12/07 #136225 -Start
				LEFT JOIN M_SHUUKEI_KOUMOKU SHUUKEI
                    ON ET.MOD_SHUUKEI_KOUMOKU_CD = SHUUKEI.SHUUKEI_KOUMOKU_CD 
                --PhuocLoc 2020/12/07 #136225 -End
			WHERE
				ET.DELETE_FLG = 0
				--拠点
				/*IF dto.KYOTEN_CD != 99*/
				AND ET.KYOTEN_CD = /*dto.KYOTEN_CD*/1
				/*END*/
				--伝票日付_FROM
				/*IF dto.DENPYOU_DATE_FROM != null && dto.DENPYOU_DATE_FROM != ''*/
				AND ET.DENPYOU_DATE >= /*dto.DENPYOU_DATE_FROM*/'2015/4/1'
				/*END*/
				--伝票日付_TO
				/*IF dto.DENPYOU_DATE_TO != null && dto.DENPYOU_DATE_TO != ''*/
				AND ET.DENPYOU_DATE <= /*dto.DENPYOU_DATE_TO*/'2015/4/1'
				/*END*/
				--売上日付_FROM
				/*IF dto.URIAGE_DATE_FROM != null && dto.URIAGE_DATE_FROM != ''*/
				AND ET.URIAGE_DATE >= /*dto.URIAGE_DATE_FROM*/'2015/4/1'
				/*END*/
				--売上日付_TO
				/*IF dto.URIAGE_DATE_TO != null && dto.URIAGE_DATE_TO != ''*/
				AND ET.URIAGE_DATE <= /*dto.URIAGE_DATE_TO*/'2015/4/1'
				/*END*/
				--入力日付_FROM
				/*IF dto.UPDATE_DATE_FROM != null && dto.UPDATE_DATE_FROM != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) >= /*dto.UPDATE_DATE_FROM*/'2015/4/13'
				/*END*/
				--入力日付_TO
				/*IF dto.UPDATE_DATE_TO != null && dto.UPDATE_DATE_TO != ''*/
				AND CONVERT(date, ET.UPDATE_DATE, 111) <= /*dto.UPDATE_DATE_TO*/'2015/4/13'
				/*END*/
				--取引区分
				/*IF dto.TORIHIKI_KBN != 3*/
				AND ET.URIAGE_TORIHIKI_KBN_CD = /*dto.TORIHIKI_KBN*/1
				/*END*/
				--確定区分
				/*IF dto.KAKUTEI_KBN != 3*/
				AND ET.KAKUTEI_KBN = /*dto.KAKUTEI_KBN*/1
				/*END*/
				--締処理状況
				/*IF dto.SHIME_JOKYO == 1*/
				AND SEIKYUU.SEIKYUU_NUMBER IS NOT NULL
				/*END*/
				/*IF dto.SHIME_JOKYO == 2*/
				AND SEIKYUU.SEIKYUU_NUMBER IS NULL
				/*END*/
				--取引先_FROM
				/*IF dto.TORIHIKISAKI_CD_FROM != null && dto.TORIHIKISAKI_CD_FROM != ''*/
				AND ET.TORIHIKISAKI_CD >= /*dto.TORIHIKISAKI_CD_FROM*/'000001'
				/*END*/
				--取引先_TO
				/*IF dto.TORIHIKISAKI_CD_TO != null && dto.TORIHIKISAKI_CD_TO != ''*/
				AND ET.TORIHIKISAKI_CD <= /*dto.TORIHIKISAKI_CD_TO*/'000001'
				/*END*/
				--業者_FROM
				/*IF dto.GYOUSHA_CD_FROM != null && dto.GYOUSHA_CD_FROM != ''*/
				AND ET.GYOUSHA_CD >= /*dto.GYOUSHA_CD_FROM*/'000001'
				/*END*/
				--業者_TO
				/*IF dto.GYOUSHA_CD_TO != null && dto.GYOUSHA_CD_TO != ''*/
				AND ET.GYOUSHA_CD <= /*dto.GYOUSHA_CD_TO*/'000001'
				/*END*/
				--現場_FROM
				/*IF dto.GENBA_CD_FROM != null && dto.GENBA_CD_FROM != ''*/
				AND ET.GENBA_CD >= /*dto.GENBA_CD_FROM*/'000001'
				/*END*/
				--現場_TO
				/*IF dto.GENBA_CD_TO != null && dto.GENBA_CD_TO != ''*/
				AND ET.GENBA_CD <= /*dto.GENBA_CD_TO*/'000001'
				/*END*/
				--営業者_FROM
				/*IF dto.EIGYOU_TANTOUSHA_CD_FROM != null && dto.EIGYOU_TANTOUSHA_CD_FROM != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD >= /*dto.EIGYOU_TANTOUSHA_CD_FROM*/'000001'
				/*END*/
				--営業者_TO
				/*IF dto.EIGYOU_TANTOUSHA_CD_TO != null && dto.EIGYOU_TANTOUSHA_CD_TO != ''*/
				AND ET.EIGYOU_TANTOUSHA_CD <= /*dto.EIGYOU_TANTOUSHA_CD_TO*/'000001'
				/*END*/
				--運転者_FROM
				/*IF dto.UNTENSHA_CD_FROM != null && dto.UNTENSHA_CD_FROM != ''*/
				AND ET.UNTENSHA_CD >= /*dto.UNTENSHA_CD_FROM*/'000001'
				/*END*/
				--運転者_TO
				/*IF dto.UNTENSHA_CD_TO != null && dto.UNTENSHA_CD_TO != ''*/
				AND ET.UNTENSHA_CD <= /*dto.UNTENSHA_CD_TO*/'000001'
				/*END*/
				--品名_FROM
				/*IF dto.HINMEI_CD_FROM != null && dto.HINMEI_CD_FROM != ''*/
				AND DT.HINMEI_CD >= /*dto.HINMEI_CD_FROM*/'000001'
				/*END*/
				--品名_TO
				/*IF dto.HINMEI_CD_TO != null && dto.HINMEI_CD_TO != ''*/
				AND DT.HINMEI_CD <= /*dto.HINMEI_CD_TO*/'000001'
				/*END*/
                --種類_FROM
				/*IF dto.SHURUI_CD_FROM != null && dto.SHURUI_CD_FROM != ''*/
				AND HINMEI.SHURUI_CD >= /*dto.SHURUI_CD_FROM*/'000001'
				/*END*/
				--種類_TO
				/*IF dto.SHURUI_CD_TO != null && dto.SHURUI_CD_TO != ''*/
				AND HINMEI.SHURUI_CD <= /*dto.SHURUI_CD_TO*/'000001'
				/*END*/
				--分類_FROM
				/*IF dto.BUNRUI_CD_FROM != null && dto.BUNRUI_CD_FROM != ''*/
				AND HINMEI.BUNRUI_CD >= /*dto.BUNRUI_CD_FROM*/'000001'
				/*END*/
				--分類_TO
				/*IF dto.BUNRUI_CD_TO != null && dto.BUNRUI_CD_TO != ''*/
				AND HINMEI.BUNRUI_CD <= /*dto.BUNRUI_CD_TO*/'000001'
				/*END*/
				--PhuocLoc 2020/12/07 #136225 -Start
				--集計項目_FROM
				/*IF dto.SHUUKEI_KOUMOKU_CD_FROM != null && dto.SHUUKEI_KOUMOKU_CD_FROM != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD >= /*dto.SHUUKEI_KOUMOKU_CD_FROM*/'000001'
				/*END*/
				--集計項目_TO
				/*IF dto.SHUUKEI_KOUMOKU_CD_TO != null && dto.SHUUKEI_KOUMOKU_CD_TO != ''*/
				AND ET.MOD_SHUUKEI_KOUMOKU_CD <= /*dto.SHUUKEI_KOUMOKU_CD_TO*/'000001'
				--PhuocLoc 2020/12/07 #136225 -End
				/*END*/
				-- 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
				-- 伝種 = 3の場合、売上データを取得する
				-- 伝種 = 4の場合、代納データを取得する
				-- 伝種 = 5の場合、DAINOU_FLG指定せず、全てのデータを取得する
				/*IF dto.DENPYOU_SHURUI == 3*/
				AND (ET.DAINOU_FLG IS NULL OR ET.DAINOU_FLG != 1)
				/*END*/
				/*IF dto.DENPYOU_SHURUI == 4*/
				AND ET.DAINOU_FLG = 1
				/*END*/
				-- 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
			/*END*/
		) AS DENPYOU_DATA
		GROUP BY
			DENPYOU_DATA.DUMMY
			/*IF dto.GROUP_COLUMN != null*//*$dto.GROUP_COLUMN*/''/*END*/
	) AS RANK_DATA
) DATA
/*BEGIN*/
WHERE
	/*IF dto.RANK != 0*/
	DATA.RANK <= /*dto.RANK*/30
	/*END*/
/*END*/
ORDER BY /*$dto.SORT_COLUMN*/RANK