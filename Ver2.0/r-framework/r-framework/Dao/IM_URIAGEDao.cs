using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_URIAGE))]
    public interface IM_URIAGEDao : IS2Dao
    {

        [Sql("SELECT * FROM M_URIAGE")]
        M_URIAGE[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Uriage.IM_URIAGEDao_GetAllValidData.sql")]
        M_URIAGE[] GetAllValidData(M_URIAGE data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_URIAGE data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_URIAGE data);

        int Delete(M_URIAGE data);

        [Sql("select M_URIAGE.GURUUPU_CD AS CD,M_URIAGE.GURUUPU_MEI AS NAME FROM M_URIAGE /*$whereSql*/ group by  M_URIAGE.GURUUPU_CD,M_URIAGE.GURUUPU_MEI")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_URIAGE data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GURUUPU_CD = /*cd*/")]
        M_URIAGE GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// Nhận dữ liệu danh sách cho màn hình chính
        /// </summary>
        /// <param name="path">SQLファイルパス(Đường dẫn tệp SQL)</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ(Áp dụng cờ)</param>
        /// <param name="deletechuFlg">削除フラグ(Xóa cờ)</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ(Cờ ngoài khả năng áp dụng)</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_URIAGE data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
