using r_framework.Const;
using r_framework.Dao;
using Seasar.Framework.Container.Factory;
namespace r_framework.Utility
{
    /// <summary>
    /// Dao初期化クラス(Lớp khởi tạo Dao)
    /// </summary>
    public static class DaoInitUtility
    {
        /// <summary>
        /// 指定したDaoをS2Containerから取得し返却(Nhận Dao được chỉ định từ S2Container và trả lại)
        /// </summary>
        public static T GetComponent<T>(string connectionID = Constans.DAO) where T : IS2Dao
        {
            return (T)SingletonS2ContainerFactory.Container.GetComponent(typeof(T));
        }
    }
}
