// $Id: GenchakuJikanHoshuConstans.cs 8301 2013-11-26 07:22:36Z sys_dev_24 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Master.GenchakuJikanHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    /// 
    ///
    ///lớp không đổi
    public class GenchakuJikanHoshuConstans
    {
        /// <summary>M_GENCHAKU_TIMEのGENCHAKU_TIME_CD</summary>
        public static readonly string GENCHAKU_TIME_CD = "GENCHAKU_TIME_CD";

        /// <summary>M_GENCHAKU_TIMEのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_GENCHAKU_TIMEのDELETE_FLGフラグ</summary>
        //public static readonly string DELETE_FLG = "DELETE_FLG";
        public static readonly string DELETE_FLG = "chb_delete";

        /*quoc-begin*/
        //MOD_UNTEN_SHA
        public static readonly string MOD_UNTEN_SHA = "MOD_UNTEN_SHA";

        //MOD_WARIATE_JUN
        public static readonly string MOD_WARIATE_JUN = "MOD_WARIATE_JUN";
        //MOD_NYUURYOKU_TANTOUSHA
        public static readonly string MOD_NYUURYOKU_TANTOUSHA = "MOD_NYUURYOKU_TANTOUSHA";
        //MOD_NINI_TORI_FUKA
        public static readonly string MOD_NINI_TORI_FUKA = "MOD_NINI_TORI_FUKA";
        //MOD_RYUUBEI_KANZANKEISUU
        public static readonly string MOD_RYUUBEI_KANZANKEISUU = "MOD_RYUUBEI_KANZANKEISUU";
        /*quoc-end*/
    }
}
