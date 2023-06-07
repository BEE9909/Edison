﻿using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Function.ShougunCSCommon.Dao
{
    /// <summary>
    /// 売掛金・買掛金一覧表Dao
    /// </summary>
	[Bean(typeof(M_TORIHIKISAKI))]
	public interface IT_KAKEKIN_ICHIRANDao : IS2Dao
    {
		/// <summary>
		/// 明細表示用一覧データテーブルの取得
		/// </summary>
		/// <param name="showCD">表示対象となる取引先CD</param>
		/// <param name="startDay">開始伝票日付</param>
		/// <param name="endDay">終了伝票日付</param>
		/// <returns name="DataTable">データテーブル</returns>
		[SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.KakekinIchiran.IT_KAKEKIN_ICHIRANDao_GetIchiranData.sql")]
		DataTable GetIchiranData(string showCD, string startDay, string endDay);

        /// <summary>
        /// 指定された範囲の取引先Listを取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <returns name="M_TORIHIKISAKI[]">取引先CDのリスト</returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.KakekinIchiran.IT_KAKEKIN_ICHIRANDao_GetTorihikisakiList.sql")]
        M_TORIHIKISAKI[] GetTorihikisakiList(string startCD, string endCD);
    }
}