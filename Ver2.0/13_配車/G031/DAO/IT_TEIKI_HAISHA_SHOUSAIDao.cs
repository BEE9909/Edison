﻿// $Id: IT_TEIKI_HAISHA_SHOUSAIDao.cs 9420 2013-12-03 10:50:48Z sys_dev_23 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    [Bean(typeof(T_TEIKI_HAISHA_SHOUSAI))]
    public interface IT_TEIKI_HAISHA_SHOUSAIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_SHOUSAI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_SHOUSAI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_TEIKI_HAISHA_SHOUSAI data);

        /// <summary>
        /// 定期配車明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Sql.GetTeikiHaishaShousaiData.sql")]
        new DataTable GetDetailData(T_TEIKI_HAISHA_SHOUSAI data);

        /// <summary>
        /// 定期配車明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Sql.GetTeikiHaishaShousaiData.sql")]
        T_TEIKI_HAISHA_SHOUSAI[] GetDataForEntity(T_TEIKI_HAISHA_SHOUSAI data);
    }
}
