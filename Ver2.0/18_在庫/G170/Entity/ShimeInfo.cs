using r_framework.Entity;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// ÝÉ÷ßf[^NX
    /// </summary>
    public class ShimeInfo : SuperEntity
    {
        public string RET_SYSTEM_ID { get; set; } //VXeID
        public string RET_ZAIKO_SHIME_DATE { get; set; } //ÝÉ÷Àsú
        public string RET_GYOUSHA_CD { get; set; } //ÆÒCD
        public string RET_GENBA_CD { get; set; } //»êCD
        public string RET_GENBA_NAME_RYAKU { get; set; } //»ê¼
        public string RET_ZAIKO_HINMEI_CD { get; set; } //ÝÉCD
        public string RET_ZAIKO_HINMEI_RYAKU { get; set; } //ÝÉi¼
        public string RET_REMAIN_SUU { get; set; } //Oc
        public string RET_ENTER_SUU { get; set; } //óü
        public string RET_OUT_SUU { get; set; } //o×Ê
        public string RET_ADJUST_SUU { get; set; } //²®Ê
        public string RET_TOTAL_SUU { get; set; } //ÝÉc
        public string RET_TANKA { get; set; } //]¿P¿
        public string RET_MULT { get; set; } //ÝÉàz

    }
}