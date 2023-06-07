﻿using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ReceiptPayManagement.ShukkinShuukeiChouhyou
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 出金集計表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_SHUKKIN_ENTRY))]
    internal interface IShukkinShuukeihyouDao : IS2Dao
    {
        /// <summary>
        /// 出金集計表に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [Sql("/*$sql*/")]
        DataTable GetShukkinShuukeiData(string sql);
    }
}
