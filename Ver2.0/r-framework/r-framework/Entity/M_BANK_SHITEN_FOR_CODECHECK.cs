using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.Table("M_BANK_SHITEN")]
    public class M_BANK_SHITEN_FOR_CODECHECK : SuperEntity
    {
        public const string TABLE = "M_BANK_SHITEN";

        public string BANK_CD { get; set; }

        public string BANK_SHITEN_CD { get; set; }

        public string BANK_SHITEN_NAME { get; set; }

        public string BANK_SHIETN_NAME_RYAKU { get; set; }

        public string BANK_SHITEN_FURIGANA { get; set; }

        public SqlInt16 KOUZA_SHURUI_CD { get; set; }

        public string KOUZA_SHURUI { get; set; }

        public string KOUZA_NO { get; set; }

        public string KOUZA_NAME { get; set; }

        public string BANK_SHITEN_BIKOU { get; set; }

        public SqlBoolean DELETE_FLG { get; set; }

        public string BANK_NAME { get; set; }

        public string BANK_NAME_RYAKU { get; set; }

        public string BANK_FURIGANA { get; set; }


    }
}