﻿// $Id: DenManiHaikiShuruiHoshuConstans.cs 16193 2014-02-19 08:03:22Z sp.m.miki $
using System.Collections.ObjectModel;

namespace DenManiHaikiShuruiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class DenManiHaikiShuruiHoshuConstans
    {
        /// <summary>M_DENSHI_HAIKI_SHURUIのHAIKI_SHURUI_CD</summary>
        public static readonly string HAIKI_SHURUI_CD = "HAIKI_SHURUI_CD";

        /// <summary>M_DENSHI_HAIKI_SHURUI_SAIBUNRUIのHAIKI_SHURUI_SAIBUNRUI_CD</summary>
        public static readonly string HAIKI_SHURUI_SAIBUNRUI_CD = "HAIKI_SHURUI_SAIBUNRUI_CD";

        /// <summary>M_DENSHI_HAIKI_SHURUIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_DENSHI_HAIKI_SHURUIのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { "0100", "0110", "0111", "0112", "0120", "0200", "0210", "0211", "0220", "0221", "0222", "0300", "0310", "0311", "0312", "0320", "0330", "0340", "0400", "0401", "0500", "0501", "0600", "0601", "0602", "0603", "0604", "0605", "0606", "0607", "0608", "0700", "0710", "0711", "0800", "0810", "0811", "0900", "0910", "1000", "1100", "1200", "1210", "1220", "1221", "1222", "1300", "1310", "1311", "1312", "1313", "1314", "1315", "1316", "1317", "1320", "1321", "1322", "1323", "1400", "1401", "1500", "1501", "1502", "1600", "1700", "1800", "1900", "2000", "2010", "2020", "2021", "2022", "2100", "2200", "2300", "2410", "2420", "2430", "2440", "2450", "2460", "2470", "2510", "2520", "2521", "2522", "2530", "2531", "2532", "2540", "2550", "2551", "2560", "2561", "2562", "2610", "2620", "2630", "2640", "2650", "2660", "3000", "3010", "3011", "3012", "3100", "3101", "3102", "3103", "3104", "3105", "3106", "3107", "3108", "3109", "3110", "3112", "3500", "3510", "3520", "3600", "4000", "5010", "5011", "5012", "5013", "5014", "5020", "5021", "5022", "5023", "5024", "5025", "5026", "5027", "5030", "5031", "5032", "5033", "5034", "5035", "5036", "5040", "5041", "5050", "5051", "5060", "5061", "5062", "5063", "5064", "5065", "5066", "5067", "5068", "5070", "5071", "5072", "5080", "5081", "5082", "5090", "5091", "5110", "5120", "5130", "5140", "5141", "5142", "5143", "5144", "5150", "5151", "5152", "5153", "5154", "5155", "5156", "5157", "5158", "5161", "5162", "5163", "5164", "5170", "5171", "5180", "5181", "5182", "5190", "5210", "5220", "5221", "5222", "5230", "5240", "5241", "5242", "5243", "5244", "5250", "5260", "5270", "5281", "5282", "5283", "5284", "5285", "5286", "5287", "5290", "5291", "5292", "5293", "5310", "5311", "5312", "5313", "5314", "5315", "5316", "5317", "5318", "5319", "5321", "5322", "5323", "5330", "5331", "5332", "5340", "7000", "7010", "7100", "7110", "7200", "7210", "7300", "7400", "7410", "7411", "7412", "7413", "7421", "7422", "7423", "7424", "7425", "7426", "7427", "7428", "7429", "7430", "7440", "7510", "7511", "7520", "7521", "7530", "7531", "7540", "7550", "7551", "7552", "7553", "7554", "7555", "7556", "7557", "7558", "7559", "7561", "7562", "7563", "7564", "7565", "7610", "7620", "7630", "7640" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "HAIKI_SHURUI_CD", "HAIKI_SHURUI_NAME" });
    }
}
