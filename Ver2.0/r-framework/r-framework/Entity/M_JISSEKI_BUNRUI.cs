using System.Data.SqlTypes;

namespace r_framework.Entity
{
    /// <summary>
    /// êÊppñªÞ
    /// </summary>
    public class M_JISSEKI_BUNRUI : SuperEntity
    {
        /// <summary>ÀÑªÞCD</summary>
        public string JISSEKI_BUNRUI_CD { get; set; }
        /// <summary>ÀÑªÞ¼</summary>
        public string JISSEKI_BUNRUI_NAME { get; set; }
        /// <summary>ÀÑªÞªÌ</summary>
        public string JISSEKI_BUNRUI_NAME_RYAKU { get; set; }
        /// <summary>ÀÑªÞtKi</summary>
        public string JISSEKI_BUNRUI_FURIGANA { get; set; }
        /// <summary>ÀÑªÞõl</summary>
        public string JISSEKI_BUNRUI_BIKOU { get; set; }
        /// <summary>ítO</summary>
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
