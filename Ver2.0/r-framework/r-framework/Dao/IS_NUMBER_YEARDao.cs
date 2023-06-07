using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 年連番Dao
    /// </summary>
    [Bean(typeof(S_NUMBER_YEAR))]
    public interface IS_NUMBER_YEARDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM S_NUMBER_YEAR")]
        S_NUMBER_YEAR[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_NUMBER_YEAR data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_NUMBER_YEAR data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        int Delete(S_NUMBER_YEAR data);

        /// <summary>
        /// 主キーをもとに削除されていない年連番のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("NUMBERED_YEAR = /*data.NUMBERED_YEAR*/ AND DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/ AND KYOTEN_CD = /*data.KYOTEN_CD*/")]
        S_NUMBER_YEAR GetNumberYearData(S_NUMBER_YEAR data);

        /// <summary>
        /// 年連番の最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(CURRENT_NUMBER),1) FROM S_NUMBER_YEAR WHERE NUMBERED_YEAR = /*data.NUMBERED_YEAR*/ AND DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/ AND KYOTEN_CD = /*data.KYOTEN_CD*/")]
        int GetMaxKey(S_NUMBER_YEAR data);

        /// <summary>
        /// 年連番の最小値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(CURRENT_NUMBER),1) FROM S_NUMBER_YEAR WHERE NUMBERED_YEAR = /*data.NUMBERED_YEAR*/ AND DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/ AND KYOTEN_CD = /*data.KYOTEN_CD*/")]
        int GetMinKey(S_NUMBER_YEAR data);

        /// <summary>
        /// 年連番の最大値+1を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(CURRENT_NUMBER),0)+1 FROM S_NUMBER_YEAR WHERE NUMBERED_YEAR = /*data.NUMBERED_YEAR*/ AND DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/ AND KYOTEN_CD = /*data.KYOTEN_CD*/")]
        int GetMaxPlusKey(S_NUMBER_YEAR data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, S_NUMBER_YEAR data);
    }
}
