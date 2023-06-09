using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_R18 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal MANIFEST_KBN { get; set; }
        public SqlDecimal SHOUNIN_FLAG { get; set; }
        public string HIKIWATASHI_DATE { get; set; }
        public SqlDecimal UPN_ENDREP_FLAG { get; set; }
        public SqlDecimal SBN_ENDREP_FLAG { get; set; }
        public SqlDecimal LAST_SBN_ENDREP_FLAG { get; set; }
        public string KAKIN_DATE { get; set; }
        public string REGI_DATE { get; set; }
        public string UPN_SBN_REP_LIMIT_DATE { get; set; }
        public string LAST_SBN_REP_LIMIT_DATE { get; set; }
        public string RESV_LIMIT_DATE { get; set; }
        public SqlDecimal SBN_ENDREP_KBN { get; set; }
        public string HST_SHA_EDI_MEMBER_ID { get; set; }
        public string HST_SHA_NAME { get; set; }
        public string HST_SHA_POST { get; set; }
        public string HST_SHA_ADDRESS1 { get; set; }
        public string HST_SHA_ADDRESS2 { get; set; }
        public string HST_SHA_ADDRESS3 { get; set; }
        public string HST_SHA_ADDRESS4 { get; set; }
        public string HST_SHA_TEL { get; set; }
        public string HST_SHA_FAX { get; set; }
        public string HST_JOU_NAME { get; set; }
        public string HST_JOU_POST_NO { get; set; }
        public string HST_JOU_ADDRESS1 { get; set; }
        public string HST_JOU_ADDRESS2 { get; set; }
        public string HST_JOU_ADDRESS3 { get; set; }
        public string HST_JOU_ADDRESS4 { get; set; }
        public string HST_JOU_TEL { get; set; }
        public string REGI_TAN { get; set; }
        public string HIKIWATASHI_TAN_NAME { get; set; }
        public string HAIKI_DAI_CODE { get; set; }
        public string HAIKI_CHU_CODE { get; set; }
        public string HAIKI_SHO_CODE { get; set; }
        public string HAIKI_SAI_CODE { get; set; }
        public string HAIKI_BUNRUI { get; set; }
        public string HAIKI_SHURUI { get; set; }
        public string HAIKI_NAME { get; set; }
        public SqlDecimal HAIKI_SUU { get; set; }
        public string HAIKI_UNIT_CODE { get; set; }
        public string SUU_KAKUTEI_CODE { get; set; }
        public SqlDecimal HAIKI_KAKUTEI_SUU { get; set; }
        public string HAIKI_KAKUTEI_UNIT_CODE { get; set; }
        public string NISUGATA_CODE { get; set; }
        public string NISUGATA_NAME { get; set; }
        public string NISUGATA_SUU { get; set; }
        public string SBN_SHA_MEMBER_ID { get; set; }
        public string SBN_SHA_NAME { get; set; }
        public string SBN_SHA_POST { get; set; }
        public string SBN_SHA_ADDRESS1 { get; set; }
        public string SBN_SHA_ADDRESS2 { get; set; }
        public string SBN_SHA_ADDRESS3 { get; set; }
        public string SBN_SHA_ADDRESS4 { get; set; }
        public string SBN_SHA_TEL { get; set; }
        public string SBN_SHA_FAX { get; set; }
        public string SBN_SHA_KYOKA_ID { get; set; }
        public string SAI_SBN_SHA_MEMBER_ID { get; set; }
        public string SAI_SBN_SHA_NAME { get; set; }
        public string SAI_SBN_SHA_POST { get; set; }
        public string SAI_SBN_SHA_ADDRESS1 { get; set; }
        public string SAI_SBN_SHA_ADDRESS2 { get; set; }
        public string SAI_SBN_SHA_ADDRESS3 { get; set; }
        public string SAI_SBN_SHA_ADDRESS4 { get; set; }
        public string SAI_SBN_SHA_TEL { get; set; }
        public string SAI_SBN_SHA_FAX { get; set; }
        public string SAI_SBN_SHA_KYOKA_ID { get; set; }
        public SqlDecimal SBN_WAY_CODE { get; set; }
        public string SBN_WAY_NAME { get; set; }
        public SqlDecimal SBN_SHOUNIN_FLAG { get; set; }
        public string SBN_END_DATE { get; set; }
        public string HAIKI_IN_DATE { get; set; }
        public SqlDecimal RECEPT_SUU { get; set; }
        public string RECEPT_UNIT_CODE { get; set; }
        public string UPN_TAN_NAME { get; set; }
        public string CAR_NO { get; set; }
        public string REP_TAN_NAME { get; set; }
        public string SBN_TAN_NAME { get; set; }
        public string SBN_END_REP_DATE { get; set; }
        public string SBN_REP_BIKOU { get; set; }
        public SqlDecimal KENGEN_CODE { get; set; }
        public string LAST_SBN_JOU_KISAI_FLAG { get; set; }
        public string FIRST_MANIFEST_FLAG { get; set; }
        public string LAST_SBN_END_DATE { get; set; }
        public string LAST_SBN_END_REP_DATE { get; set; }
        public string SHUSEI_DATE { get; set; }
        public SqlDecimal CANCEL_FLAG { get; set; }
        public string CANCEL_DATE { get; set; }
        public string LAST_UPDATE_DATE { get; set; }
        public SqlDecimal YUUGAI_CNT { get; set; }
        public SqlDecimal UPN_ROUTE_CNT { get; set; }
        public SqlDecimal LAST_SBN_PLAN_CNT { get; set; }
        public SqlDecimal LAST_SBN_CNT { get; set; }
        public SqlDecimal RENRAKU_CNT { get; set; }
        public SqlDecimal BIKOU_CNT { get; set; }
        public SqlDecimal FIRST_MANIFEST_CNT { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}