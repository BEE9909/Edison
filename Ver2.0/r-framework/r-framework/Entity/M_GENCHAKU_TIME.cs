using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENCHAKU_TIME : SuperEntity
    {
        public SqlInt16 GENCHAKU_TIME_CD { get; set; }
        /*M192_Quoc_Begin*/
        public SqlInt16 MOD_WARIATE_JUN { get; set; }
        public SqlBoolean MOD_UNTEN_SHA { get; set; }

        public SqlBoolean MOD_NYUURYOKU_TANTOUSHA { get; set; }
        public SqlBoolean MOD_NINI_TORI_FUKA { get; set; }
        public SqlDecimal MOD_RYUUBEI_KANZANKEISUU { get; set; }
        public string MOD_HAIKI_NAME_CD { get; set; }
        public string MOD_NISUGATA_CD { get; set; }
        public string MOD_SHOBUN_HOUHOU_CD { get; set; }

        public SqlDecimal MOD_GENYO_RITSU { get; set; }

        public SqlBoolean MOD_KYOBASHI { get; set; }
        /*M192_Quoc_End*/
        public string GENCHAKU_TIME_NAME { get; set; }
        public string GENCHAKU_TIME_NAME_RYAKU { get; set; }
        public SqlInt16 GENCHAKU_PRIORITY { get; set; }
        public SqlInt32 GENCHAKU_BACK_COLOR { get; set; }
        public string GENCHAKU_TIME_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        
        

    }
}