using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MITSUMORI_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt16 MITSUMORI_SHOSHIKI_KBN { get; set; }
        public SqlInt16 PEGE_TOTAL { get; set; }
        public SqlInt16 JOKYO_FLG { get; set; }
        public string SINKOU_DATE { get; set; }
        public string JUCHU_DATE { get; set; }
        public string SICHU_DATE { get; set; }
        public SqlInt64 MITSUMORI_NUMBER { get; set; }
        public string MITSUMORI_DATE { get; set; }
        public SqlInt16 INJI_KYOTEN1_CD { get; set; }
        public SqlInt16 INJI_KYOTEN2_CD { get; set; }
        public SqlBoolean HIKIAI_TORIHIKISAKI_FLG { get; set; }
        //quoc
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME { get; set; }
        public SqlBoolean TORIHIKISAKI_INJI { get; set; }
        public SqlBoolean HIKIAI_GYOUSHA_FLG { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public SqlBoolean GYOUSHA_INJI { get; set; }
        public SqlBoolean HIKIAI_GENBA_FLG { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public SqlBoolean GENBA_INJI { get; set; }
        public string SHAIN_CD { get; set; }
        public string SHAIN_NAME { get; set; }
        public string TORIHIKISAKI_KEISHOU { get; set; }
        public string GYOUSHA_KEISHOU { get; set; }
        public string GENBA_KEISHOU { get; set; }
        public string KENMEI { get; set; }
        public string MITSUMORI_1 { get; set; }
        public string MITSUMORI_2 { get; set; }
        public string MITSUMORI_3 { get; set; }
        public string MITSUMORI_4 { get; set; }
        public string BIKOU_1 { get; set; }
        public string BIKOU_2 { get; set; }
        public string BIKOU_3 { get; set; }
        public string BIKOU_4 { get; set; }
        public string BIKOU_5 { get; set; }
        public SqlBoolean MITSUMORI_INJI_DATE { get; set; }
        public SqlBoolean BUSHO_NAME_INJI { get; set; }
        public string SHANAI_BIKOU { get; set; }
        public SqlInt16 ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 ZEI_KBN_CD { get; set; }
        public SqlDecimal KINGAKU_TOTAL { get; set; }
        public SqlDecimal SHOUHIZEI_RATE { get; set; }
        public SqlDecimal TAX_SOTO { get; set; }
        public SqlDecimal TAX_UCHI { get; set; }
        public SqlDecimal TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal TAX_UCHI_TOTAL { get; set; }
        public SqlDecimal SHOUHIZEI_TOTAL { get; set; }
        public SqlDecimal GOUKEI_KINGAKU_TOTAL { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //quoc-begin
        public string MOD_URIAGE_GURUUPU_CD { get; set; }
        public string MOD_URIAGE_GURUUPU_NAME { get; set; }
        public string MOD_SHIHARAI_GURUUPU_CD { get; set; }
        public string MOD_SHIHARAI_GURUUPU_NAME { get; set; }

        public SqlInt16 MOD_KAKUIN_INJI { get; set; }
        public SqlInt16 MOD_GOUKEI_KINGAKU_INJI { get; set; }

        public string MOD_TENWABANGOU { get; set; }

        public SqlInt16 MOD_FAX_SOUFUJOU_INJI { get; set; }

        public string MOD_BIKOU1 { get; set; }
        public string MOD_BIKOU2 { get; set; }
        public string MOD_BIKOU3 { get; set; }
        public string MOD_BIKOU4 { get; set; }
        public string MOD_BIKOU5 { get; set; }
        public string MOD_BIKOU6 { get; set; }
        public string MOD_BIKOU7 { get; set; }
        public string MOD_BIKOU8 { get; set; }
        public string MOD_BIKOU9 { get; set; }
        public string MOD_BIKOU10 { get; set; }
        public string MOD_BIKOU11 { get; set; }
        public string MOD_BIKOU12 { get; set; }

        public string MOD_BIKOU13 { get; set; }
        public string MOD_BIKOU14 { get; set; }
        public string MOD_BIKOU15 { get; set; }

        public string MOD_BIKOU16 { get; set; }
        public string MOD_BIKOU17 { get; set; }
        public string MOD_BIKOU18 { get; set; }
        public string MOD_BIKOU19 { get; set; }

        public string MOD_BIKOU20 { get; set; }
        public string MOD_BIKOU21 { get; set; }
        public string MOD_BIKOU22 { get; set; }
        public string MOD_BIKOU23 { get; set; }
        public string MOD_BIKOU24 { get; set; }
        public string MOD_BIKOU25 { get; set; }

        public string MOD_SHOUNIN_SHA_CD { get; set; }
        public string MOD_SHOUNIN_SHA_NAME { get; set; }
        public string MOD_TXT_STATUS { get; set; }

        public string MOD_SHOUNIN_NICHIJI { get; set; }
        //quoc-end


    }
}