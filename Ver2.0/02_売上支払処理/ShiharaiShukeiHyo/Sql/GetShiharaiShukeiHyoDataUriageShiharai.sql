﻿SELECT
    T_UR_SH_ENTRY.SHIHARAI_TORIHIKI_KBN_CD,
    CASE T_UR_SH_ENTRY.SHIHARAI_TORIHIKI_KBN_CD WHEN 1 THEN '現金' WHEN 2 THEN '掛け' ELSE '' END SHIHARAI_TORIHIKI_KBN_NAME,
    T_UR_SH_ENTRY.KYOTEN_CD,
    (SELECT KYOTEN_NAME_RYAKU FROM M_KYOTEN WHERE M_KYOTEN.KYOTEN_CD = T_UR_SH_ENTRY.KYOTEN_CD) AS KYOTEN_NAME,
    T_UR_SH_ENTRY.TORIHIKISAKI_CD,
    (SELECT TORIHIKISAKI_NAME_RYAKU FROM M_TORIHIKISAKI WHERE M_TORIHIKISAKI.TORIHIKISAKI_CD = T_UR_SH_ENTRY.TORIHIKISAKI_CD) AS TORIHIKISAKI_NAME,
    T_UR_SH_ENTRY.GYOUSHA_CD,
    (SELECT GYOUSHA_NAME_RYAKU FROM M_GYOUSHA WHERE M_GYOUSHA.GYOUSHA_CD = T_UR_SH_ENTRY.GYOUSHA_CD) AS GYOUSHA_NAME,
    T_UR_SH_ENTRY.GENBA_CD,
    (SELECT GENBA_NAME_RYAKU FROM M_GENBA WHERE M_GENBA.GYOUSHA_CD = T_UR_SH_ENTRY.GYOUSHA_CD AND M_GENBA.GENBA_CD = T_UR_SH_ENTRY.GENBA_CD) AS GENBA_NAME,
    T_UR_SH_ENTRY.NIOROSHI_GYOUSHA_CD,
    (SELECT GYOUSHA_NAME_RYAKU FROM M_GYOUSHA WHERE M_GYOUSHA.GYOUSHA_CD = T_UR_SH_ENTRY.NIOROSHI_GYOUSHA_CD) AS NIOROSHI_GYOUSHA_NAME,
    T_UR_SH_ENTRY.NIOROSHI_GENBA_CD,
    (SELECT GENBA_NAME_RYAKU FROM M_GENBA WHERE M_GENBA.GYOUSHA_CD = T_UR_SH_ENTRY.NIOROSHI_GYOUSHA_CD AND M_GENBA.GENBA_CD = T_UR_SH_ENTRY.NIOROSHI_GENBA_CD) AS NIOROSHI_GENBA_NAME,
    T_UR_SH_ENTRY.NIZUMI_GYOUSHA_CD,
    (SELECT GYOUSHA_NAME_RYAKU FROM M_GYOUSHA WHERE M_GYOUSHA.GYOUSHA_CD = T_UR_SH_ENTRY.NIZUMI_GYOUSHA_CD) AS NIZUMI_GYOUSHA_NAME,
    T_UR_SH_ENTRY.NIZUMI_GENBA_CD,
    (SELECT GENBA_NAME_RYAKU FROM M_GENBA WHERE M_GENBA.GYOUSHA_CD = T_UR_SH_ENTRY.GYOUSHA_CD AND M_GENBA.GENBA_CD = T_UR_SH_ENTRY.NIZUMI_GENBA_CD) AS NIZUMI_GENBA_NAME,
    T_UR_SH_ENTRY.EIGYOU_TANTOUSHA_CD,
    (SELECT SHAIN_NAME_RYAKU FROM M_SHAIN WHERE M_SHAIN.SHAIN_CD = T_UR_SH_ENTRY.EIGYOU_TANTOUSHA_CD) AS EIGYOU_TANTOUSHA_NAME,
    T_UR_SH_ENTRY.NYUURYOKU_TANTOUSHA_CD,
    (SELECT SHAIN_NAME_RYAKU FROM M_SHAIN WHERE M_SHAIN.SHAIN_CD = T_UR_SH_ENTRY.NYUURYOKU_TANTOUSHA_CD) AS NYUURYOKU_TANTOUSHA_NAME,
    T_UR_SH_ENTRY.SHARYOU_CD,
    (SELECT SHARYOU_NAME_RYAKU FROM M_SHARYOU WHERE M_SHARYOU.GYOUSHA_CD = T_UR_SH_ENTRY.UNPAN_GYOUSHA_CD AND M_SHARYOU.SHARYOU_CD = T_UR_SH_ENTRY.SHARYOU_CD) AS SHARYOU_NAME,
    T_UR_SH_ENTRY.SHASHU_CD,
    (SELECT SHASHU_NAME_RYAKU FROM M_SHASHU WHERE SHASHU_CD = T_UR_SH_ENTRY.SHASHU_CD) AS SHASHU_NAME,
    T_UR_SH_ENTRY.UNPAN_GYOUSHA_CD,
    (SELECT GYOUSHA_NAME_RYAKU FROM M_GYOUSHA WHERE M_GYOUSHA.GYOUSHA_CD = T_UR_SH_ENTRY.UNPAN_GYOUSHA_CD) AS UNPAN_GYOUSHA_NAME,
    T_UR_SH_ENTRY.UNTENSHA_CD,
    (SELECT SHAIN_NAME_RYAKU FROM M_SHAIN WHERE M_SHAIN.SHAIN_CD = T_UR_SH_ENTRY.UNTENSHA_CD) AS UNTENSHA_NAME,
    T_UR_SH_ENTRY.KEITAI_KBN_CD,
    (SELECT KEITAI_KBN_NAME_RYAKU FROM M_KEITAI_KBN WHERE M_KEITAI_KBN.KEITAI_KBN_CD = T_UR_SH_ENTRY.KEITAI_KBN_CD) AS KEITAI_KBN_NAME,
	/*IF dto.SearchHinmeiFlg*/
    T_UR_SH_DETAIL.HINMEI_CD,
    (SELECT HINMEI_NAME_RYAKU FROM M_HINMEI WHERE M_HINMEI.HINMEI_CD = T_UR_SH_DETAIL.HINMEI_CD) AS HINMEI_NAME,
	/*END*/
	/*IF dto.SearchSuuryouTaniFlg*/
    T_UR_SH_DETAIL.SUURYOU,
    T_UR_SH_DETAIL.UNIT_CD,
    (SELECT UNIT_NAME_RYAKU FROM M_UNIT WHERE M_UNIT.UNIT_CD = T_UR_SH_DETAIL.UNIT_CD) AS UNIT_NAME,
	/*END*/
    (ISNULL(T_UR_SH_DETAIL.KINGAKU, 0) + ISNULL(T_UR_SH_DETAIL.HINMEI_KINGAKU, 0)) - (ISNULL(T_UR_SH_DETAIL.TAX_UCHI, 0) + ISNULL(T_UR_SH_DETAIL.HINMEI_TAX_UCHI, 0)) AS KINGAKU,
    M_HINMEI.SHURUI_CD,
    (SELECT SHURUI_NAME_RYAKU FROM M_SHURUI WHERE M_SHURUI.SHURUI_CD = M_HINMEI.SHURUI_CD) AS SHURUI_NAME,
    M_HINMEI.BUNRUI_CD,
    (SELECT BUNRUI_NAME_RYAKU FROM M_BUNRUI WHERE M_BUNRUI.BUNRUI_CD = M_HINMEI.BUNRUI_CD) AS BUNRUI_NAME,
	T_UR_SH_ENTRY.DENPYOU_DATE,
	T_UR_SH_ENTRY.SHIHARAI_DATE,
	T_UR_SH_ENTRY.UPDATE_DATE,
	--PhuocLoc 2020/12/08 #136226 -Start
	T_UR_SH_ENTRY.MOD_SHUUKEI_KOUMOKU_CD,
    (SELECT SHUUKEI_KOUMOKU_NAME_RYAKU FROM M_SHUUKEI_KOUMOKU WHERE M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_CD = T_UR_SH_ENTRY.MOD_SHUUKEI_KOUMOKU_CD) AS MOD_SHUUKEI_KOUMOKU_NAME 
    --PhuocLoc 2020/12/08 #136226 -End

FROM T_UR_SH_ENTRY
    JOIN T_UR_SH_DETAIL
        ON T_UR_SH_ENTRY.SYSTEM_ID = T_UR_SH_DETAIL.SYSTEM_ID
            AND T_UR_SH_ENTRY.SEQ = T_UR_SH_DETAIL.SEQ
    LEFT JOIN M_HINMEI
    ON T_UR_SH_DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD
    LEFT JOIN (SELECT
                   DENPYOU.SEISAN_NUMBER,
                   DETAIL.DENPYOU_SYSTEM_ID,
                   DETAIL.DENPYOU_SEQ,
                   DETAIL.DETAIL_SYSTEM_ID,
                   DETAIL.DENPYOU_NUMBER
               FROM T_SEISAN_DENPYOU AS DENPYOU
               JOIN T_SEISAN_DENPYOU_KAGAMI AS KAGAMI
                   ON DENPYOU.SEISAN_NUMBER = KAGAMI.SEISAN_NUMBER
               JOIN T_SEISAN_DETAIL AS DETAIL
                   ON KAGAMI.SEISAN_NUMBER = DETAIL.SEISAN_NUMBER
                   AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER
               WHERE DETAIL.DENPYOU_SHURUI_CD = 3
               AND DENPYOU.DELETE_FLG = 0) AS SEISAN
        ON SEISAN.DENPYOU_SYSTEM_ID = T_UR_SH_DETAIL.SYSTEM_ID
            AND SEISAN.DENPYOU_SEQ = T_UR_SH_DETAIL.SEQ
            AND SEISAN.DETAIL_SYSTEM_ID = T_UR_SH_DETAIL.DETAIL_SYSTEM_ID
            AND SEISAN.DENPYOU_NUMBER = T_UR_SH_DETAIL.UR_SH_NUMBER
WHERE T_UR_SH_ENTRY.DELETE_FLG = 0
  AND T_UR_SH_DETAIL.DENPYOU_KBN_CD = 2

-- 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) Start
-- 伝種 = 3の場合、売上データを取得する
-- 伝種 = 4の場合、代納データを取得する
-- 伝種 = 5の場合、DAINOU_FLG指定せず、全てのデータを取得する
/*IF dto.DenpyouShurui == 3*/
  AND (T_UR_SH_ENTRY.DAINOU_FLG IS NULL OR T_UR_SH_ENTRY.DAINOU_FLG != 1)
/*END*/
/*IF dto.DenpyouShurui == 4*/
  AND T_UR_SH_ENTRY.DAINOU_FLG = 1
/*END*/
-- 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) End

/*IF dto.DateShurui == 1*/
  AND T_UR_SH_ENTRY.DENPYOU_DATE >= /*dto.DateFrom*/''
  AND T_UR_SH_ENTRY.DENPYOU_DATE <= /*dto.DateTo*/''
/*END*/
/*IF dto.DateShurui == 2*/
  AND T_UR_SH_ENTRY.SHIHARAI_DATE >= /*dto.DateFrom*/''
  AND T_UR_SH_ENTRY.SHIHARAI_DATE <= /*dto.DateTo*/''
/*END*/
/*IF dto.DateShurui == 3*/
  AND T_UR_SH_ENTRY.UPDATE_DATE >= /*dto.DateFrom*/''
  AND T_UR_SH_ENTRY.UPDATE_DATE <= /*dto.DateTo*/''
/*END*/

/*IF dto.TorihikiKbn != 3*/
  AND T_UR_SH_ENTRY.URIAGE_TORIHIKI_KBN_CD = /*dto.TorihikiKbn*/1
/*END*/

/*IF dto.KakuteiKbn != 3*/
  AND T_UR_SH_ENTRY.KAKUTEI_KBN = /*dto.KakuteiKbn*/1
/*END*/

/*IF dto.ShimeKbn == 1*/
  AND SEISAN.SEISAN_NUMBER IS NOT NULL
/*END*/
/*IF dto.ShimeKbn == 2*/
  AND SEISAN.SEISAN_NUMBER IS NULL
/*END*/

/*IF dto.KyotenCd != 99*/
  AND T_UR_SH_ENTRY.KYOTEN_CD = /*dto.KyotenCd*/0
/*END*/

/*IF dto.TorihikisakiCdFrom != null && dto.TorihikisakiCdFrom != ''*/
  AND T_UR_SH_ENTRY.TORIHIKISAKI_CD >= /*dto.TorihikisakiCdFrom*/''
/*END*/
/*IF dto.TorihikisakiCdTo != null && dto.TorihikisakiCdTo != ''*/
  AND T_UR_SH_ENTRY.TORIHIKISAKI_CD <= /*dto.TorihikisakiCdTo*/''
/*END*/

/*IF dto.GyoushaCdFrom != null && dto.GyoushaCdFrom != ''*/
  AND T_UR_SH_ENTRY.GYOUSHA_CD >= /*dto.GyoushaCdFrom*/''
/*END*/
/*IF dto.GyoushaCdTo != null && dto.GyoushaCdTo != ''*/
  AND T_UR_SH_ENTRY.GYOUSHA_CD <= /*dto.GyoushaCdTo*/''
/*END*/

/*IF dto.GenbaCdFrom != null && dto.GenbaCdFrom != ''*/
  AND T_UR_SH_ENTRY.GENBA_CD >= /*dto.GenbaCdFrom*/''
/*END*/
/*IF dto.GenbaCdTo != null && dto.GenbaCdTo != ''*/
  AND T_UR_SH_ENTRY.GENBA_CD <= /*dto.GenbaCdTo*/''
/*END*/

/*IF dto.HinmeiCdFrom != null && dto.HinmeiCdFrom != ''*/
  AND T_UR_SH_DETAIL.HINMEI_CD >= /*dto.HinmeiCdFrom*/''
/*END*/
/*IF dto.HinmeiCdTo != null && dto.HinmeiCdTo != ''*/
  AND T_UR_SH_DETAIL.HINMEI_CD <= /*dto.HinmeiCdTo*/''
/*END*/

/*IF dto.NioroshiGyoushaCdFrom != null && dto.NioroshiGyoushaCdFrom != ''*/
  AND T_UR_SH_ENTRY.NIOROSHI_GYOUSHA_CD >= /*dto.NioroshiGyoushaCdFrom*/''
/*END*/
/*IF dto.NioroshiGyoushaCdTo != null && dto.NioroshiGyoushaCdTo != ''*/
  AND T_UR_SH_ENTRY.NIOROSHI_GYOUSHA_CD <= /*dto.NioroshiGyoushaCdTo*/''
/*END*/

/*IF dto.NioroshiGenbaCdFrom != null && dto.NioroshiGenbaCdFrom != ''*/
  AND T_UR_SH_ENTRY.NIOROSHI_GENBA_CD >= /*dto.NioroshiGenbaCdFrom*/''
/*END*/
/*IF dto.NioroshiGenbaCdTo != null && dto.NioroshiGenbaCdTo != ''*/
  AND T_UR_SH_ENTRY.NIOROSHI_GENBA_CD <= /*dto.NioroshiGenbaCdTo*/''
/*END*/

/*IF dto.NizumiGyoushaCdFrom != null && dto.NizumiGyoushaCdFrom != ''*/
  AND T_UR_SH_ENTRY.NIZUMI_GYOUSHA_CD >= /*dto.NizumiGyoushaCdFrom*/''
/*END*/
/*IF dto.NizumiGyoushaCdTo != null && dto.NizumiGyoushaCdTo != ''*/
  AND T_UR_SH_ENTRY.NIZUMI_GYOUSHA_CD <= /*dto.NizumiGyoushaCdTo*/''
/*END*/

/*IF dto.NizumiGenbaCdFrom != null && dto.NizumiGenbaCdFrom != ''*/
  AND T_UR_SH_ENTRY.NIZUMI_GENBA_CD >= /*dto.NizumiGenbaCdFrom*/''
/*END*/
/*IF dto.NizumiGenbaCdTo != null && dto.NizumiGenbaCdTo != ''*/
  AND T_UR_SH_ENTRY.NIZUMI_GENBA_CD <= /*dto.NizumiGenbaCdTo*/''
/*END*/

/*IF dto.EigyouTantoushaCdFrom != null && dto.EigyouTantoushaCdFrom != ''*/
  AND T_UR_SH_ENTRY.EIGYOU_TANTOUSHA_CD >= /*dto.EigyouTantoushaCdFrom*/''
/*END*/
/*IF dto.EigyouTantoushaCdTo != null && dto.EigyouTantoushaCdTo != ''*/
  AND T_UR_SH_ENTRY.EIGYOU_TANTOUSHA_CD <= /*dto.EigyouTantoushaCdTo*/''
/*END*/

/*IF dto.NyuuryokuTantoushaCdFrom != null && dto.NyuuryokuTantoushaCdFrom != ''*/
  AND T_UR_SH_ENTRY.NYUURYOKU_TANTOUSHA_CD >= /*dto.NyuuryokuTantoushaCdFrom*/''
/*END*/
/*IF dto.NyuuryokuTantoushaCdTo != null && dto.NyuuryokuTantoushaCdTo != ''*/
  AND T_UR_SH_ENTRY.NYUURYOKU_TANTOUSHA_CD <= /*dto.NyuuryokuTantoushaCdTo*/''
/*END*/

/*IF dto.UnpanGyoushaCdFrom != null && dto.UnpanGyoushaCdFrom != ''*/
  AND T_UR_SH_ENTRY.UNPAN_GYOUSHA_CD >= /*dto.UnpanGyoushaCdFrom*/''
/*END*/
/*IF dto.UnpanGyoushaCdTo != null && dto.UnpanGyoushaCdTo != ''*/
  AND T_UR_SH_ENTRY.UNPAN_GYOUSHA_CD <= /*dto.UnpanGyoushaCdTo*/''
/*END*/

/*IF dto.ShashuCdFrom != null && dto.ShashuCdFrom != ''*/
  AND T_UR_SH_ENTRY.SHASHU_CD >= /*dto.ShashuCdFrom*/''
/*END*/
/*IF dto.ShashuCdTo != null && dto.ShashuCdTo != ''*/
  AND T_UR_SH_ENTRY.SHASHU_CD <= /*dto.ShashuCdTo*/''
/*END*/

/*IF dto.SharyouCdFrom != null && dto.SharyouCdFrom != ''*/
  AND T_UR_SH_ENTRY.SHARYOU_CD >= /*dto.SharyouCdFrom*/''
/*END*/
/*IF dto.SharyouCdTo != null && dto.SharyouCdTo != ''*/
  AND T_UR_SH_ENTRY.SHARYOU_CD <= /*dto.SharyouCdTo*/''
/*END*/

/*IF dto.KeitaiKbnCdFrom != null && dto.KeitaiKbnCdFrom != ''*/
  AND T_UR_SH_ENTRY.KEITAI_KBN_CD >= /*dto.KeitaiKbnCdFrom*/''
/*END*/
/*IF dto.KeitaiKbnCdTo != null && dto.KeitaiKbnCdTo != ''*/
  AND T_UR_SH_ENTRY.KEITAI_KBN_CD <= /*dto.KeitaiKbnCdTo*/''
/*END*/

/*IF dto.ShuruiCdFrom != null && dto.ShuruiCdFrom != ''*/
  AND M_HINMEI.SHURUI_CD >= /*dto.ShuruiCdFrom*/''
/*END*/
/*IF dto.ShuruiCdTo != null && dto.ShuruiCdTo != ''*/
  AND M_HINMEI.SHURUI_CD <= /*dto.ShuruiCdTo*/''
/*END*/

/*IF dto.BunruiCdFrom != null && dto.BunruiCdFrom != ''*/
  AND M_HINMEI.BUNRUI_CD >= /*dto.BunruiCdFrom*/''
/*END*/
/*IF dto.BunruiCdTo != null && dto.BunruiCdTo != ''*/
  AND M_HINMEI.BUNRUI_CD <= /*dto.BunruiCdTo*/''
/*END*/

--PhuocLoc 2020/12/08 #136226 -Start
/*IF dto.ShuukeiKoumokuCdFrom != null && dto.ShuukeiKoumokuCdFrom != ''*/
  AND T_UR_SH_ENTRY.MOD_SHUUKEI_KOUMOKU_CD >= /*dto.ShuukeiKoumokuCdFrom*/''
/*END*/
/*IF dto.ShuukeiKoumokuCdTo != null && dto.ShuukeiKoumokuCdTo != ''*/
  AND T_UR_SH_ENTRY.MOD_SHUUKEI_KOUMOKU_CD <= /*dto.ShuukeiKoumokuCdTo*/''
/*END*/
--PhuocLoc 2020/12/08 #136226 -End
