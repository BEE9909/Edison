﻿// $Id: EigyouTantoushaHoshuLogic.cs 18950 2014-04-10 09:53:01Z sc.h.hashimoto $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EigyouTantoushaHoshu.APP;
using EigyouTantoushaHoshu.Dto;
using EigyouTantoushaHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using Shougun.Core.Common.BusinessCommon;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace EigyouTantoushaHoshu.Logic
{
    /// <summary>
    /// 営業担当者保守画面のビジネスロジック
    /// </summary>
    public class EigyouTantoushaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "EigyouTantoushaHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_EIGYOU_DATA_SQL = "EigyouTantoushaHoshu.Sql.GetIchiranEigyoutantoushaDataSql.sql";

        private readonly string GET_EIGYOUTANTOUSHA_DATA_SQL = "EigyouTantoushaHoshu.Sql.GetEigyouTantoushaDataSql.sql";

        private readonly string GET_ICHIRAN_SHAIN_DATA_SQL = "EigyouTantoushaHoshu.Sql.GetIchiranShaindataSql.sql";

        /// <summary>
        /// 営業担当者保守画面Form
        /// </summary>
        private EigyouTantoushaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_EIGYOU_TANTOUSHA[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 営業担当者のDao
        /// </summary>
        private IM_EIGYOU_TANTOUSHADao daoEigyoutantousha;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao daoShain;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public EigyouTantoushaHoshuDto eigyoutantoushaHoshuDto { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public EigyouTantoushaHoshuLogic(EigyouTantoushaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.daoEigyoutantousha = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
            this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.eigyoutantoushaHoshuDto = new EigyouTantoushaHoshuDto();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            if (this.entitySysInfo != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                // プロパティ設定有無チェック
                if (this.eigyoutantoushaHoshuDto.PropertiesShainExistCheck())
                {
                    // 社員マスタの条件で取得
                    this.SearchResult = daoShain.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHAIN_DATA_SQL,
                                                                       this.eigyoutantoushaHoshuDto.ShainSearchString,
                                                                       this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    // 営業担当者マスタの条件で取得
                    this.SearchResult = daoEigyoutantousha.GetIchiranDataSqlFile(this.GET_ICHIRAN_EIGYOU_DATA_SQL,
                                                                  this.eigyoutantoushaHoshuDto.EigyoutantoushaSearchString,
                                                                  this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }

                this.SearchResultAll = daoEigyoutantousha.GetDataBySqlFile(this.GET_EIGYOUTANTOUSHA_DATA_SQL, new M_EIGYOU_TANTOUSHA());

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                int count = this.SearchResult.Rows == null ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var entityList = new M_EIGYOU_TANTOUSHA[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_EIGYOU_TANTOUSHA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_EIGYOU_TANTOUSHA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.EigyouTantoushaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                List<M_EIGYOU_TANTOUSHA> addList = new List<M_EIGYOU_TANTOUSHA>();
                if (this.form.Ichiran.DataSource != null)
                {
                    foreach (DataRow row in ((DataTable)this.form.Ichiran.DataSource).Rows)
                    {
                        M_EIGYOU_TANTOUSHA ent = new M_EIGYOU_TANTOUSHA();
                        ent.SHAIN_CD = row[Const.EigyouTantoushaHoshuConstans.SHAIN_CD].ToString();
                        ent.EIGYOU_TANTOUSHA_BIKOU = row[Const.EigyouTantoushaHoshuConstans.EIGYOU_TANTOUSHA_BIKOU].ToString();
                        dataBinderLogic.SetSystemProperty(ent, false);
                        MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), ent);
                        ent.DELETE_FLG = (bool)row[Const.EigyouTantoushaHoshuConstans.DELETE_FLG];
                        addList.Add(ent);
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 社員CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            LogUtility.DebugMethodStart();

            EigyouTantoushaHoshuValidator vali = new EigyouTantoushaHoshuValidator();
            bool result = vali.ShainCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

            LogUtility.DebugMethodEnd();

            return result;
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "営業担当者一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
                    multirowLocationLogic.multiRow = this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    CSVFileLogic csvLogic = new CSVFileLogic();

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);

                    #region 新しいCSV出力利用するように、余計なメッセージを削除
                    //msgLogic.MessageBoxShow("I000");
                    #endregion
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public bool CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetIchiran();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #region 登録/更新/削除
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                //独自チェックの記述例を書く


                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_EIGYOU_TANTOUSHA eigyoutantoushaEntity in this.entitys)
                        {
                            if (!String.IsNullOrEmpty(eigyoutantoushaEntity.SHAIN_CD))
                            {

                                M_EIGYOU_TANTOUSHA entity = this.daoEigyoutantousha.GetDataByCd(eigyoutantoushaEntity.SHAIN_CD);
                                if (entity == null)
                                {
                                    this.daoEigyoutantousha.Insert(eigyoutantoushaEntity);
                                }
                                else
                                {
                                    this.daoEigyoutantousha.Update(eigyoutantoushaEntity);
                                }
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_EIGYOU_TANTOUSHA eigyoutantoushaEntity in this.entitys)
                        {
                            M_EIGYOU_TANTOUSHA entity = this.daoEigyoutantousha.GetDataByCd(eigyoutantoushaEntity.SHAIN_CD);
                            if (entity != null)
                            {
                                this.daoEigyoutantousha.Update(eigyoutantoushaEntity);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Equals/GetHashCode/ToString
        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            EigyouTantoushaHoshuLogic localLogic = other as EigyouTantoushaHoshuLogic;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;

                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            M_EIGYOU_TANTOUSHA entityEigyoutantousha = new M_EIGYOU_TANTOUSHA();   // TODO:メインマスタEntity(表示する作成者等があるEntity)
            M_SHAIN entityShain = new M_SHAIN();                                   // TODO:サブマスタEntity

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // TODO : メインマスタのEntityを指定する
                    bool isExist = this.EntityExistCheck(entityEigyoutantousha, this.form.CONDITION_VALUE.DBFieldsName);

                    // TODO : true=メインマスタEntityのSetValue使用、false=サブマスタEntityのSetValue使用
                    if (isExist)
                    {
                        // 検索条件の設定(営業担当者マスタ)
                        entityEigyoutantousha.SetValue(this.form.CONDITION_VALUE);
                    }
                    else
                    {
                        // 検索条件の設定(社員マスタ)
                        entityShain.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }

            eigyoutantoushaHoshuDto.ShainSearchString = entityShain;
            eigyoutantoushaHoshuDto.EigyoutantoushaSearchString = entityEigyoutantousha;
        }

        /// <summary>
        /// Entity内のプロパティに指定プロパティが存在するかチェック
        /// </summary>
        /// <param name="entity">マスタEntity</param>
        /// <param name="dbFieldName">存在チェックしたいプロパティ名</param>
        /// <returns>true:プロパティあり、false:プロパティなし</returns>
        private bool EntityExistCheck(object entity, string dbFieldName)
        {
            bool result = false;

            // マスタEntityのプロパティ取得
            var properties = entity.GetType().GetProperties();

            // プロパティ名検索
            foreach (var property in properties)
            {
                if (property.Name == dbFieldName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            return table;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearCondition()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }
    }
}
