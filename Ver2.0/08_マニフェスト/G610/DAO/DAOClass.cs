﻿using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.PaperManifest.JissekiHokokuUnpan;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv
{
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv.Sql.GetManiData.sql")]
        DataTable GetManiData(SearchDto data);
    }    /// <summary>
    /// 実績報告書用Dao
    /// </summary>
    [Bean(typeof(T_JISSEKI_HOUKOKU_ENTRY))]
    public interface EntryDAO : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_ENTRY data);
    }

    /// <summary>
    /// 実績報告書_マニ明細
    /// </summary>
    [Bean(typeof(T_JISSEKI_HOUKOKU_MANIFEST_DETAIL))]
    public interface ManiDetailDAO : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_MANIFEST_DETAIL data);
    }

    /// <summary>
    /// 実績報告書_運搬
    /// </summary>
    [Bean(typeof(T_JISSEKI_HOUKOKU_UPN_DETAIL))]
    public interface UnpanDAO : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv.Sql.GetUnpanData.sql")]
        DataTable GetUnpanData(T_JISSEKI_HOUKOKU_UPN_DETAIL data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_UPN_DETAIL data);
    }
}
