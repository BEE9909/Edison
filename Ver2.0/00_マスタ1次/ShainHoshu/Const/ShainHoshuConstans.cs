﻿// $Id: ShainHoshuConstans.cs 16130 2014-02-18 07:48:07Z sp.m.miki $

using System.Collections.ObjectModel;
namespace ShainHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ShainHoshuConstans
    {
        /// <summary>M_SHAINのSHAIN_CD</summary>
        public static readonly string SHAIN_CD = "SHAIN_CD";

        /// <summary>M_SHAINのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_SHAINのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>M_SHAINのBUSHO_CD</summary>
        public static readonly string BUSHO_CD = "BUSHO_CD";

        /// <summary>M_BUSHOのBUSHO_NAME_RYAKU</summary>
        public static readonly string BUSHO_NAME_RYAKU = "BUSHO_NAME_RYAKU";

        /// <summary>M_SHAINのLOGIN_ID</summary>
        public static readonly string LOGIN_ID = "LOGIN_ID";

        /// <summary>M_SHAINのPASSWORD</summary>
        public static readonly string PASSWORD = "PASSWORD";

        /// <summary>M_SHAINのMAIL_ADDRESS</summary>
        public static readonly string MAIL_ADDRESS = "MAIL_ADDRESS";

        //quoc-begin
        public static readonly string MOD_BTN_SANSHO = "MOD_BTN_SANSHO";
        public static readonly string MOD_BTN_ETSURAN = "MOD_BTN_ETSURAN";
        public static readonly string MOD_INEI_DATA = "MOD_INEI_DATA";
        //quoc-end
        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "SHAIN_CD", "SHAIN_NAME", "SHAIN_NAME_RYAKU" });

        /// <summary>
        /// メールアドレスの登録上限数
        /// </summary>
        public static readonly int REGIST_LIMIT_MAIL_ADDRESS = 2;
    }
}
