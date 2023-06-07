using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_TORIHIKI_KBN))]
    public interface IM_TORIHIKI_KBNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_TORIHIKI_KBN")]
        M_TORIHIKI_KBN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.TorihikiKbn.IM_TORIHIKI_KBNDao_GetAllValidData.sql")]
        M_TORIHIKI_KBN[] GetAllValidData(M_TORIHIKI_KBN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKI_KBN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKI_KBN data);

        int Delete(M_TORIHIKI_KBN data);

        [Sql("select M_TORIHIKI_KBN.TORIHIKI_KBN_CD AS CD,M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU AS NAME FROM M_TORIHIKI_KBN /*$whereSql*/ group by M_TORIHIKI_KBN.TORIHIKI_KBN_CD,M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TORIHIKI_KBN data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKI_KBN_CD = /*cd*/")]
        M_TORIHIKI_KBN GetDataByCd(short cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_TORIHIKI_KBN data, bool deletechuFlg);
    }
}