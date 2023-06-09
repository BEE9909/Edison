// $Id: IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao.cs 4688 2013-10-24 00:47:47Z sys_dev_20 $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SHIHARAI))]
    public interface IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_HIKIAI_TORIHIKISAKI_SHIHARAI")]
        M_HIKIAI_TORIHIKISAKI_SHIHARAI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_TORIHIKISAKI_SHIHARAI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_TORIHIKISAKI_SHIHARAI data);

        int Delete(M_HIKIAI_TORIHIKISAKI_SHIHARAI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_TORIHIKISAKI_SHIHARAI data);

        /// <summary>
        ///取引先コードをもとに取引先_支払情報マスタのデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_HIKIAI_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);

        /// <summary>
        /// 住所の一部データ書き換え機能
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="data">取引先支払情報マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_HIKIAI_TORIHIKISAKI_SHIHARAI data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}
