﻿// $Id: DenManiTantoushaHoshuConstans.cs 703 2013-08-23 15:35:27Z gai $
using System.Collections.ObjectModel;

namespace DenManiYuugaiBusshitsuHoshu.Const
{
    /// <summary>
    /// 定数
    /// </summary>
    public class DenManiYuugaiBusshitsuHoshuConstans
    {
        /// <summary>M_DENSHI_YUUGAI_BUSSHITSUのYUUGAI_BUSSHITSU_CD</summary>
        public static readonly string YUUGAI_BUSSHITSU_CD = "YUUGAI_BUSSHITSU_CD";

        /// <summary>M_DENSHI_YUUGAI_BUSSHITSUのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>画面表示項目の削除フラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "33" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "YUUGAI_BUSSHITSU_CD", "YUUGAI_BUSSHITSU_NAME"});
    }
}
