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
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Function.ShougunCSCommon.Dao
{
    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface IT_UR_SH_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Insert(T_UR_SH_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC")]
        int Update(T_UR_SH_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UR_SH_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UrShDetail.IT_UR_SH_DETAILDao_GetDataForEntity.sql")]
        T_UR_SH_DETAIL[] GetDataForEntity(T_UR_SH_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// T_UKEIRE_ENTRY.SYSTEM_IDの最高値を取得する
        /// </summary>
        /// <param name="whereSql">絞り込み条件</param>
        /// <returns>SYSTEM_IDのMAX値</returns>
        [Sql("select ISNULL(MAX(SYSTEM_ID),1) FROM T_UKEIRE_ENTRY /*$whereSql*/")]
        SqlInt64 getMaxSystemId(string whereSql);
    }
}
