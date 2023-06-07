using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_GYOUSHU))]
    public interface IM_CHIIKIBETSU_GYOUSHUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CHIIKIBETSU_GYOUSHU")]
        M_CHIIKIBETSU_GYOUSHU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuGyoushu.IM_CHIIKIBETSU_GYOUSHUDao_GetAllValidData.sql")]
        M_CHIIKIBETSU_GYOUSHU[] GetAllValidData(M_CHIIKIBETSU_GYOUSHU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_GYOUSHU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_GYOUSHU data);

        int Delete(M_CHIIKIBETSU_GYOUSHU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_GYOUSHU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_GYOUSHU data, bool deletechuFlg);
    }
}
