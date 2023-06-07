﻿using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 取引先マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IM_TORIHIKISAKIDao : MasterAccess.Base.IMasterAccessDao<M_TORIHIKISAKI>
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        // Thực hiện xử lý chèn dựa trên Thực thể
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        //Quá trình cập nhật dựa trên Thực thể
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_TORIHIKISAKI data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_TORIHIKISAKI")]
        M_TORIHIKISAKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// Thu thập thông tin trong khoảng thời gian áp dụng mà cờ xóa không được đặt
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.IM_TORIHIKISAKIDao_GetAllValidData.sql")]
        M_TORIHIKISAKI[] GetAllValidData(M_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),1) FROM M_TORIHIKISAKI  where ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 取引先コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(TORIHIKISAKI_CD),1) FROM M_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 取引先コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),0)+1 FROM M_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// 取引先コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.IM_TORIHIKISAKIDao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT TORIHIKISAKI_CD FROM M_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 1")]
        M_TORIHIKISAKI[] GetDateByChokuchiKbn1();

        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.IM_TORIHIKISAKIDao_GetTorihikisakiData.sql")]
        M_TORIHIKISAKI GetTorihikisakiData(string torihikisakiCd);

        /// <summary>
        /// 取引先コードをもとに削除されていない取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI GetDataByCd(string cd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TORIHIKISAKI data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_TORIHIKISAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("select M_TORIHIKISAKI.TORIHIKISAKI_CD AS CD,M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS NAME FROM M_TORIHIKISAKI /*$whereSql*/ group by M_TORIHIKISAKI.TORIHIKISAKI_CD,M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// 住所の一部データ書き換え機能
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="data">取引先マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_TORIHIKISAKI data, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// 取引先のCTI連動データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.IM_TORIHIKISAKIDao_GetCtiRenkeiData.sql")]
        DataTable GetCtiRenkeiData(string tel, string torihikisaki, string selectType);
    }
}