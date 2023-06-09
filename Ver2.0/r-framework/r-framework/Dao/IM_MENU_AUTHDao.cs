using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_MENU_AUTH))]
    public interface IM_MENU_AUTHDao : IS2Dao
    {

        [Sql("SELECT * FROM M_MENU_AUTH")]
        M_MENU_AUTH[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.MenuAuth.IM_MENU_AUTHDao_GetAllValidData.sql")]
        M_MENU_AUTH[] GetAllValidData(M_MENU_AUTH data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MENU_AUTH data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MENU_AUTH data);

        int Delete(M_MENU_AUTH data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MENU_AUTH data);
    }
}
