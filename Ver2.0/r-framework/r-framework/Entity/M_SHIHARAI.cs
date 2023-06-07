using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHIHARAI : SuperEntity
    {
        public string GURUUPU_CD { get; set; }
        public string GURUUPU_MEI { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

    }
}
