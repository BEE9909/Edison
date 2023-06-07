﻿select 
		TDE.DENPYOU_DATE
		,MKT.KYOTEN_CD
		,MKT.KYOTEN_NAME_RYAKU
		,TDUE.DAINOU_NUMBER AS DENPYOU_NUMBER
		,TDUE.GYOUSHA_CD AS UKEIRE_GYOUSHA_CD
		,TDUE.GYOUSHA_NAME AS UKEIRE_GYOUSHA_NAME
		,TDUE.TORIHIKISAKI_CD AS UKEIRE_TORIHIKISAKI_CD
		,TDUE.TORIHIKISAKI_NAME AS UKEIRE_TORIHIKISAKI_NAME
		,TDUE.GENBA_CD AS UKEIRE_GENBA_CD
		,TDUE.GENBA_NAME AS UKEIRE_GENBA_NAME
		,TUE.UNPAN_GYOUSHA_CD AS UPN_GYOUSHA_CD
		,TUE.UNPAN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME
		,TDSE.GYOUSHA_CD AS SHUKKA_GYOUSHA_CD
		,TDSE.GYOUSHA_NAME AS SHUKKA_GYOUSHA_NAME
		,TDSE.TORIHIKISAKI_CD AS SHUKKA_TORIHIKISAKI_CD
		,TDSE.TORIHIKISAKI_NAME AS SHUKKA_TORIHIKISAKI_NAME
		,TDSE.GENBA_CD AS SHUKKA_GENBA_CD
		,TDSE.GENBA_NAME AS SHUKKA_GENBA_NAME
		--,TUCD.KINGAKU AS UNCHIN_KINGAKU
		,TDUD.ROW_NO AS ROW_NO
		,TDUD.HINMEI_CD AS UKEIRE_HINMEI_CD
		,TDUD.HINMEI_NAME AS UKEIRE_HINMEI_NAME
		,TDUD.NET_JYUURYOU AS UKEIRE_SYOUMI
		,TDUD.SUURYOU AS UKEIRE_SUURYOU 
		,(select top 1 UNIT_NAME_RYAKU from M_UNIT u where u.UNIT_CD = TDUD.UNIT_CD) AS UKEIRE_UNIT_NAME
		,TDUD.TANKA AS UKEIRE_TANKA
		,TDUD.KINGAKU AS UKEIRE_KINGAKU
		,TDUD.MEISAI_BIKOU AS UKEIRE_BIKOU
		,TDSD.HINMEI_CD AS SHUKKA_HINMEI_CD
		,TDSD.HINMEI_NAME AS SHUKKA_HINMEI_NAME
		,TDSD.NET_JYUURYOU AS SHUKKA_SYOUMI
		,TDSD.SUURYOU AS SHUKKA_SUURYOU
		,(select top 1 UNIT_NAME_RYAKU from M_UNIT u where u.UNIT_CD = TDSD.UNIT_CD) AS SHUKKA_UNIT_NAME
		,TDSD.TANKA AS SHUKKA_TANKA
		,TDSD.KINGAKU AS SHUKKA_KINGAKU
		,TDSD.KINGAKU - TDUD.KINGAKU AS SAEKI_KINGAKU
		,TDSD.MEISAI_BIKOU AS SHUKKA_BIKOU
		---------------------------------
		,NULL AS KINGAKU_TOTAL  
		--,(
		-- (select TUD.KINGAKU
		-- from T_UNCHIN_DETAIL TUD 
		-- inner join T_DAINOU_UKEIRE_DETAIL TDUD1 on TUD.ROW_NO = TDUD1.ROW_NO and TUD.HINMEI_CD = TDUD1.HINMEI_CD
		-- where TUD.ROW_NO = TDUD.ROW_NO)
		-- +
		-- (select TUD.KINGAKU 
		-- from T_UNCHIN_DETAIL TUD 
		-- inner join T_DAINOU_SHUKKA_DETAIL TDSD1 on TUD.ROW_NO = TDSD1.ROW_NO and TUD.HINMEI_CD = TDSD1.HINMEI_CD
		-- where TUD.ROW_NO = TDUD.ROW_NO)
		--) AS UNCHIN_KINGAKU
		,Null as UNCHIN_KINGAKU
		----------------------------- 
		,(select sum(UNCHIN.KINGAKU) 
		 from T_UNCHIN_DETAIL as UNCHIN
		 where UNCHIN.SYSTEM_ID = TUE.SYSTEM_ID 
		 and UNCHIN.SEQ = TUE.SEQ and TUE.DELETE_FLG = 0) as UNCHIN_KINGAKU_GOUKEI
		 -----------sum UKEIRE-------------------
		,(select sum(UKEIRE.NET_JYUURYOU) 
		 from T_DAINOU_UKEIRE_DETAIL UKEIRE
		 where UKEIRE.SYSTEM_ID = TDUD.SYSTEM_ID 
		 and UKEIRE.SEQ = TDUD.SEQ) as UKEIRE_SYOUMI_GOUKEI
		 -------------------------------------------
		,(select sum(UKEIRE.KINGAKU) 
		 from T_DAINOU_UKEIRE_DETAIL as UKEIRE
		 where UKEIRE.SYSTEM_ID = TDUD.SYSTEM_ID 
		 and UKEIRE.SEQ = TDUD.SEQ) as UKEIRE_KINGAKU_GOUKEI 
		 -----------sum SHUKKA-------------------
		,(select sum(SHUKKA.NET_JYUURYOU) 
		 from T_DAINOU_SHUKKA_DETAIL as SHUKKA
		 where SHUKKA.SYSTEM_ID = TDSD.SYSTEM_ID 
		 and SHUKKA.SEQ = TDSD.SEQ) as SHUKKA_SYOUMI_GOUKEI 
		 ----------------------------------------
		,(select sum(SHUKKA.KINGAKU) 
		 from T_DAINOU_SHUKKA_DETAIL as SHUKKA
		 where SHUKKA.SYSTEM_ID = TDSD.SYSTEM_ID 
		 and SHUKKA.SEQ = TDSD.SEQ) as SHUKKA_KINGAKU_GOUKEI
		 ------------sum SAEKI------------------
		,(select sum(SHUKKA.KINGAKU - UKEIRE.KINGAKU)  
		 from T_DAINOU_SHUKKA_DETAIL SHUKKA inner join T_DAINOU_UKEIRE_DETAIL UKEIRE
		 ON (SHUKKA.SYSTEM_ID = UKEIRE.SYSTEM_ID AND SHUKKA.SEQ = UKEIRE.SEQ AND SHUKKA.ROW_NO = UKEIRE.ROW_NO)
		 WHERE SHUKKA.SYSTEM_ID = TDSD.SYSTEM_ID AND SHUKKA.SEQ = TDSD.SEQ
		 GROUP BY SHUKKA.SYSTEM_ID,SHUKKA.SEQ) AS SAEKI_KINGAKU_GOUKEI
		from T_DAINOU_ENTRY as TDE
		----------------------------------
		inner join T_UNCHIN_ENTRY TUE on TDE.SYSTEM_ID = TUE.RENKEI_SYSTEM_ID And TUE.RENKEI_DENSHU_KBN_CD = 170
		inner join M_KYOTEN MKT on TDE.KYOTEN_CD = MKT.KYOTEN_CD
		----------------------------------
		inner join T_DAINOU_UKEIRE_ENTRY TDUE on (TDE.SYSTEM_ID = TDUE.SYSTEM_ID and TDE.SEQ = TDUE.SEQ)
		inner join T_DAINOU_SHUKKA_ENTRY TDSE on (TDUE.SYSTEM_ID = TDSE.SYSTEM_ID and TDUE.SEQ = TDSE.SEQ)
		-------------------------------
		inner join T_DAINOU_UKEIRE_DETAIL TDUD on (TDUD.SYSTEM_ID = TDUE.SYSTEM_ID 
		 and TDUD.SEQ = TDUE.SEQ)
		inner join T_DAINOU_SHUKKA_DETAIL TDSD on (TDSD.SYSTEM_ID = TDUD.SYSTEM_ID 
		 and TDSD.SEQ = TDUD.SEQ and TDSD.ROW_NO = TDUD.ROW_NO)
		------------------------
		WHERE 
		 TDE.DELETE_FLG = 0 And TUE.DELETE_FLG = 0
		 --and TUE.DELETE_FLG = 0 and TDUE.DELETE_FLG and TDSE.DELETE_FLG = 0 and TDUD.DELETE_FLG and TDSD.DELETE_FLG				
 				AND TDE.DENPYOU_DATE >= /*data.DAINOU_ENTRY_DENPYOU_DATE_FROM*/ AND TDE.DENPYOU_DATE <= /*data.DAINOU_ENTRY_DENPYOU_DATE_TO*/
				AND TDE.KYOTEN_CD = /*data.M_KYOTEN_CD*/

				And (/*data.UKEIRE_ENTRY_TORIHIKISAKI_CD_FROM*/ is null or TDUE.TORIHIKISAKI_CD >= /*data.UKEIRE_ENTRY_TORIHIKISAKI_CD_FROM*/)
				And (/*data.UKEIRE_ENTRY_TORIHIKISAKI_CD_TO*/ is null or TDUE.TORIHIKISAKI_CD <=/*data.UKEIRE_ENTRY_TORIHIKISAKI_CD_TO*/)

				And (/*data.UKEIRE_ENTRY_GYOUSHA_CD_FROM*/ is null or TDUE.GYOUSHA_CD >= /*data.UKEIRE_ENTRY_GYOUSHA_CD_FROM*/)
				And (/*data.UKEIRE_ENTRY_GYOUSHA_CD_TO*/ is null or TDUE.GYOUSHA_CD <= /*data.UKEIRE_ENTRY_GYOUSHA_CD_TO*/)
				
				And (/*data.UKEIRE_ENTRY_GENBA_CD_FROM*/ is null or TDUE.GENBA_CD >= /*data.UKEIRE_ENTRY_GENBA_CD_FROM*/)
				And (/*data.UKEIRE_ENTRY_GENBA_CD_TO*/ is null or TDUE.GENBA_CD <= /*data.UKEIRE_ENTRY_GENBA_CD_TO*/)				
				
				And (/*data.UKEIRE_DETAIL_HINMEI_CD_FROM*/ is null or TDUD.HINMEI_CD >= /*data.UKEIRE_DETAIL_HINMEI_CD_FROM*/)
				And (/*data.UKEIRE_DETAIL_HINMEI_CD_TO*/ is null or TDUD.HINMEI_CD <= /*data.UKEIRE_DETAIL_HINMEI_CD_TO*/)

				And (/*data.SHUKKA_ENTRY_TORIHIKISAKI_CD_FROM*/ is null or TDSE.TORIHIKISAKI_CD >= /*data.SHUKKA_ENTRY_TORIHIKISAKI_CD_FROM*/)
				And (/*data.SHUKKA_ENTRY_TORIHIKISAKI_CD_TO*/ is null or TDSE.TORIHIKISAKI_CD <=/*data.SHUKKA_ENTRY_TORIHIKISAKI_CD_TO*/)

				And (/*data.SHUKKA_ENTRY_GYOUSHA_CD_FROM*/ is null or TDSE.GYOUSHA_CD >= /*data.SHUKKA_ENTRY_GYOUSHA_CD_FROM*/)
				And (/*data.SHUKKA_ENTRY_GYOUSHA_CD_TO*/ is null or TDSE.GYOUSHA_CD <=/*data.SHUKKA_ENTRY_GYOUSHA_CD_TO*/)

				And (/*data.SHUKKA_ENTRY_GENBA_CD_FROM*/ is null or TDSE.GENBA_CD >= /*data.SHUKKA_ENTRY_GENBA_CD_FROM*/)
				And (/*data.SHUKKA_ENTRY_GENBA_CD_TO*/ is null or TDSE.GENBA_CD <=/*data.SHUKKA_ENTRY_GENBA_CD_TO*/)

				And (/*data.SHUKKA_DETAIL_HINMEI_CD_FROM*/ is null or TDSD.HINMEI_CD >= /*data.SHUKKA_DETAIL_HINMEI_CD_FROM*/)
				And (/*data.SHUKKA_DETAIL_HINMEI_CD_TO*/ is null or TDSD.HINMEI_CD <=/*data.SHUKKA_DETAIL_HINMEI_CD_TO*/)

				And (/*data.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_FROM*/ is null or TUE.UNPAN_GYOUSHA_CD >= /*data.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_FROM*/)
				And (/*data.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_TO*/ is null or TUE.UNPAN_GYOUSHA_CD <=/*data.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_TO*/)

		ORDER BY
		 TDUE.GYOUSHA_CD ASC,
		 TDUE.TORIHIKISAKI_CD ASC,
		 TDUE.GENBA_CD ASC,
		 TDSE.GYOUSHA_CD ASC,
		 TDSE.TORIHIKISAKI_CD ASC,
		 TDSE.GENBA_CD ASC,
		 TDE.DAINOU_NUMBER ASC,
		 TDUD.ROW_NO ASC,
		 TDE.DENPYOU_DATE ASC