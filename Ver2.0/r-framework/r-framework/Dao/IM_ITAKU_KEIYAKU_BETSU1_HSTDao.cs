using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_ITAKU_KEIYAKU_BETSU1_HST))]
    public interface IM_ITAKU_KEIYAKU_BETSU1_HSTDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ITAKU_KEIYAKU_BETSU1_HST")]
        M_ITAKU_KEIYAKU_BETSU1_HST[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_BETSU1_HST data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_KEIYAKU_BETSU1_HST data);

        int Delete(M_ITAKU_KEIYAKU_BETSU1_HST data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ITAKU_KEIYAKU_BETSU1_HST data);

        /// <summary>
        /// SystemId������
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        M_ITAKU_KEIYAKU_BETSU1_HST[] GetDataBySystemId(string systemId);

        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ and SEQ = /*data.SEQ*/")]
        M_ITAKU_KEIYAKU_BETSU1_HST GetDataByPrimaryKey(M_ITAKU_KEIYAKU_BETSU1_HST data);
    }
}