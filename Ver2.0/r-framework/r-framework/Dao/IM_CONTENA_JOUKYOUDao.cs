using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CONTENA_JOUKYOU))]
    public interface IM_CONTENA_JOUKYOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_JOUKYOU")]
        M_CONTENA_JOUKYOU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ContenaJoukyou.IM_CONTENA_JOUKYOUDao_GetAllValidData.sql")]
        M_CONTENA_JOUKYOU[] GetAllValidData(M_CONTENA_JOUKYOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_JOUKYOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA_JOUKYOU data);

        int Delete(M_CONTENA_JOUKYOU data);

        [Sql("select M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD AS CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU AS NAME FROM M_CONTENA_JOUKYOU /*$whereSql*/ group by M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CONTENA_JOUKYOU data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("CONTENA_JOUKYOU_CD = /*cd*/")]
        M_CONTENA_JOUKYOU GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CONTENA_JOUKYOU data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}