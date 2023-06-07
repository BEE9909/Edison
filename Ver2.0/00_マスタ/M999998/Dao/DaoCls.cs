using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.UriageHoshu.Dao 
{
    [Bean(typeof(M_URIAGE))]
    public interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_URIAGE")]
        M_URIAGE[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト(danh sách dữ liệu được truy xuất)</returns>
        [SqlFile("r-framework.Dao.SqlFile.Uriage.IM_URIAGEDao_GetAllValidData.sql")]
        M_URIAGE[] GetAllValidData(M_URIAGE data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_URIAGE data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
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
        [Query("GURUUPU_CD = /*guruupuCd*/ AND DENPYOU_KBN_CD = /*denpyouKBN*/")]
        M_URIAGE GetDataByCd(string guruupuCd,string denpyouKBN);

        /// <summary>
        /// コンテナ種類画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.UriageHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_URIAGE data,bool deletechuFlg);

        /// <summary>
        /// コンテナ種類画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.UriageHoshu.Sql.CheckDeleteMitsubisiSql.sql")]
        DataTable GetDataContena(string[] GURUUPU_CD);
    }
}
