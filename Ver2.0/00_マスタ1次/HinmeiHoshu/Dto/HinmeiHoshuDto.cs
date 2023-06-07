// $Id: HinmeiHoshuDto.cs 53932 2015-06-29 09:37:00Z chenzz@oec-h.com $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace HinmeiHoshu.Dto
{
    public class HinmeiHoshuDto
    {
        public M_UNIT UnitSearchString { get; set; }
        public M_SHURUI ShuruiSearchString { get; set; }
        public M_BUNRUI BunruiSearchString { get; set; }
        public M_HOUKOKUSHO_BUNRUI HoukokushoBunruiSearchString { get; set; }
        public M_HINMEI HinmeiSearchString { get; set; }
        public M_JISSEKI_BUNRUI JissekiBunruiSearchString { get; set; }
        public M_HAIKI_SHURUI HaikiShuruiSearchString { get; set; }
        public M_DENSHI_HAIKI_SHURUI DenshiHaikiShuruiSearchString { get; set; }

        /// <summary>
        /// 単位Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesUnitExistsCheck()
        {
            var ret = false;

            // 単位名称
            if (!String.IsNullOrEmpty(this.UnitSearchString.UNIT_NAME_RYAKU))
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 種類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesShuruiExistCheck()
        {
            // 種類名称
            if (!string.IsNullOrEmpty(this.ShuruiSearchString.SHURUI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 分類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesBunruiExistCheck()
        {
            // 分類名称
            if (!string.IsNullOrEmpty(this.BunruiSearchString.BUNRUI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 報告書分類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesHoukokushoBunruiExistCheck()
        {
            // 報告書分類名称
            if (!string.IsNullOrEmpty(this.HoukokushoBunruiSearchString.HOUKOKUSHO_BUNRUI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 報告書分類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesJissekiBunruiExistCheck()
        {
            // 報告書分類名称
            if (!string.IsNullOrEmpty(this.JissekiBunruiSearchString.JISSEKI_BUNRUI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 廃棄物種類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesHaikiShuruiExistCheck()
        {
            // 廃棄物種類名称
            if (!string.IsNullOrEmpty(this.HaikiShuruiSearchString.HAIKI_SHURUI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 電子廃棄物種類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesDenshiHaikiShuruiExistCheck()
        {
            // 電子廃棄物種類名称
            if (!string.IsNullOrEmpty(this.DenshiHaikiShuruiSearchString.HAIKI_SHURUI_NAME))
            {
                return true;
            }

            return false;
        }

        internal void GetHashCode(string p, int densyuKbnCd)
        {
            throw new NotImplementedException();
        }
    }
}
