using r_framework.Entity;
using System;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// ÝÉ÷ßÎÛf[^NX
    /// </summary>
    public class ShimeTargetInfo : SuperEntity
    {
        public string RET_GYOUSHA_CD { get; set; }          // ÆÒCD
        public string RET_GENBA_CD { get; set; }            // »êCD
        public string RET_GENBA_NAME { get; set; }          // »ê¼
        public string RET_ZAIKO_HINMEI_CD { get; set; }     // ÝÉCD
        public string RET_ZAIKO_HINMEI_NAME { get; set; }   // ÝÉi¼
        public decimal RET_JYUURYOU { get; set; }             // dÊ
        public decimal RET_TANKA { get; set; }                // P¿
        public decimal RET_KINGAKU { get; set; }              // àz
        public DateTime RET_DENPYOU_DATE { get; set; }      // `[út
        public int RET_TARGET_FLG { get; set; }             // ÎÛf[^tO

    }
}