﻿/*SELECT * FROM T_XXXX where XXXX_NO = /*data.XXXX_NO*/

 入金入力　T_SHUKKIN_ENTRY
 入金明細  T_NYUUKIN_DETAIL */
		

/*伝票種類（1.出金）時の検索*/
SELECT T_SHUKKIN_ENTRY.KYOTEN_CD,
       T_SHUKKIN_ENTRY.BUMON_CD,
	   T_SHUKKIN_ENTRY.SHUKKIN_NUMBER,
	   T_SHUKKIN_ENTRY.DENPYOU_DATE,
	   T_SHUKKIN_ENTRY.TORIHIKISAKI_CD,
	   T_SHUKKIN_ENTRY.EIGYOU_TANTOUSHA_CD,
	   T_SHUKKIN_ENTRY.DENPYOU_BIKOU,
	   T_SHUKKIN_ENTRY.SHUKKIN_AMOUNT_TOTAL,
	   T_SHUKKIN_ENTRY.CHOUSEI_AMOUNT_TOTAL,
	   T_NYUUKIN_DETAIL.SYSTEM_ID,
	   T_NYUUKIN_DETAIL.ROW_NUMBER,
	   T_NYUUKIN_DETAIL.NYUUSHUKKIN_KBN_CD,
	   T_NYUUKIN_DETAIL.KINGAKU,
	   T_NYUUKIN_DETAIL.MEISAI_BIKOU,
	   M_KYOTEN.KYOTEN_NAME_RYAKU,
	   M_BUMON.BUMON_NAME_RYAKU,
	   M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU,
	   M_SHAIN.SHAIN_NAME_RYAKU,
	   M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU,
	   M_SYUKKINSAKI.SYUKKINSAKI_NAME_RYAKU
FROM T_SHUKKIN_ENTRY,
	 T_NYUUKIN_DETAIL
	 LEFT JOIN M_KYOTEN ON M_KYOTEN.KYOTEN_CD = T_SHUKKIN_ENTRY.KYOTEN_CD
	 LEFT JOIN M_BUMON ON M_BUMON.BUMON_CD = T_SHUKKIN_ENTRY.BUMON_CD
	 LEFT JOIN M_TORIHIKISAKI ON M_TORIHIKISAKI.TORIHIKISAKI_CD = T_SHUKKIN_ENTRY.TORIHIKISAKI_CD
	 LEFT JOIN M_SHAIN ON M_SHAIN.SHAIN_CD = T_SHUKKIN_ENTRY.EIGYOU_TANTOUSHA_CD
	 LEFT JOIN M_NYUUSHUKKIN_KBN ON M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD = T_NYUUKIN_DETAIL.NYUUSHUKKIN_KBN_CD
	 LEFT JOIN M_SYUKKINSAKI ON M_SYUKKINSAKI.SYUKKINSAKI_CD = T_SHUKKIN_ENTRY.SHUKKINSAKI_CD
WHERE T_SHUKKIN_ENTRY.DELETE_FLG = 0;
     /*AND
	  T_SHUKKIN_ENTRY.TIME_STAMP = '2013/08/26' AND /*出金入力.タイムスタンプ FROM*/
	  T_SHUKKIN_ENTRY.TIME_STAMP = '2013/08/28' AND /*出金入力.タイムスタンプ TO*/
	  T_SHUKKIN_ENTRY.DENPYOU_DATE = '' AND /*伝票日付* FROM*/
	  T_SHUKKIN_ENTRY.DENPYOU_DATE = '' AND /*伝票日付* TO*/
	  T_SHUKKIN_ENTRY.SYSTEM_ID = T_NYUUKIN_DETAIL.SYSTEM_ID AND
	  T_SHUKKIN_ENTRY.SEQ = T_NYUUKIN_DETAIL.SEQ AND
	  T_SHUKKIN_ENTRY.KYOTEN_CD = '画面の拠点CD' AND
	  T_SHUKKIN_ENTRY.BUMON_CD = '画面の部門CD';

