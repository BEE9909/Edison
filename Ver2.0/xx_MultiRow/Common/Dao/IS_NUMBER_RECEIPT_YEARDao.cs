﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Function.ShougunCSCommon.Dao
{
    [Bean(typeof(S_NUMBER_RECEIPT_YEAR))]
    public interface IS_NUMBER_RECEIPT_YEARDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_NUMBER_RECEIPT_YEAR data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_NUMBER_RECEIPT_YEAR data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(S_NUMBER_RECEIPT_YEAR data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.NumberReceiptYear.IS_NUMBER_RECEIPT_YEARDao_GetDataForEntity.sql")]
        S_NUMBER_RECEIPT_YEAR[] GetDataForEntity(S_NUMBER_RECEIPT_YEAR data);
    }
}
