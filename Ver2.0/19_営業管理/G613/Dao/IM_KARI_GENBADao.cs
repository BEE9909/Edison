
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Dao
{
    /// <summary>
    /// 引合現場マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_GENBA))]
    public interface IM_KARI_GENBADao : IS2Dao
    {
        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/")]
        M_KARI_GENBA GetDataByCd(M_KARI_GENBA data);
        
    }
}