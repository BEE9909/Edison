﻿// $Id: HaikiNameHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using HaikiNameHoshu.APP;
using HaikiNameHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace HaikiNameHoshu.Logic
{
    /// <summary>
    /// 廃棄物名称保守画面のビジネスロジック
    /// </summary>
    public class HaikiNameHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "HaikiNameHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_HAIKI_NAME_DATA_SQL = "HaikiNameHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_HAIKI_NAME_DATA_SQL = "HaikiNameHoshu.Sql.GetHaikiNameDataSql.sql";

        private readonly string CHECK_DELETE_HAIKI_NAME_SQL = "HaikiNameHoshu.Sql.CheckDeleteHaikiNameSql.sql";

        /// <summary>
        /// 廃棄物名称保守画面Form
        /// </summary>
        private HaikiNameHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_HAIKI_NAME[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 廃棄物名称のDao
        /// </summary>
        private IM_HAIKI_NAMEDao dao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;
        // VUNGUYEN 20150525 #1294 END

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(重複チェック用)
        /// </summary>
        public DataTable SearchResultCheck { get; set; }

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_HAIKI_NAME SearchString { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public HaikiNameHoshuLogic(HaikiNameHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();
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
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = true;
            this.form.Ichiran.AllowUserToAddRows = false;
            this.form.Ichiran.IsBrowsePurpose = true;

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HAIKI_NAME_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HAIKI_NAME_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_HAIKI_NAME_DATA_SQL, new M_HAIKI_NAME());

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
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

                var entityList = new M_HAIKI_NAME[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_HAIKI_NAME();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_HAIKI_NAME>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.HaikiNameHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var haikiNameEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_HAIKI_NAME> addList = new List<M_HAIKI_NAME>();
                foreach (var haikiNameEntity in haikiNameEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.HaikiNameHoshuConstans.HAIKI_NAME_CD) && n.Value.ToString().Equals(haikiNameEntity.HAIKI_NAME_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.HaikiNameHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), haikiNameEntity);
                            addList.Add(haikiNameEntity);
                            break;
                        }
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
        /// 廃棄物名称CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                HaikiNameHoshuValidator vali = new HaikiNameHoshuValidator();
                bool result = vali.HaikiNameCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var haikiNameCd = string.Empty;
                string[] strList;

                foreach (Row gcRwos in this.form.Ichiran.Rows)
                {
                    if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        if (gcRwos.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(gcRwos.Cells["CREATE_USER"].Value.ToString()))
                        {
                            continue;
                        }
                        haikiNameCd += gcRwos.Cells["HAIKI_NAME_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(haikiNameCd))
                {
                    haikiNameCd = haikiNameCd.Substring(0, haikiNameCd.Length - 1);
                    strList = haikiNameCd.Split(',');
                    DataTable dtTable = dao.GetDataBySqlFileCheck(this.CHECK_DELETE_HAIKI_NAME_SQL, strList);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E258", "廃棄物名称", "廃棄物名称CD", strName);

                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        //キー機能削除
        ///// <summary>
        ///// プレビュー
        ///// </summary>
        //public void Preview()
        //{
        //    LogUtility.DebugMethodStart();

        //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //    msgLogic.MessageBoxShow("C011", "廃棄物名称一覧表");

        //    MessageBox.Show("未実装");

        //    LogUtility.DebugMethodEnd();
        //}

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

                    // VUNGUYEN 20150525 #1294 START
                    CSVFileLogicCustom csvLogic = new CSVFileLogicCustom();
                    // VUNGUYEN 20150525 #1294 END

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);
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
                bool catchErr = SetIchiran();

                LogUtility.DebugMethodEnd(catchErr);
                return catchErr;
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
                        foreach (M_HAIKI_NAME haikiNameEntity in this.entitys)
                        {
                            M_HAIKI_NAME entity = this.dao.GetDataByCd(haikiNameEntity.HAIKI_NAME_CD);
                            if (entity == null)
                            {
                                //削除チェックが付けられている場合は、新規登録を行わない
                                if (haikiNameEntity.DELETE_FLG)
                                {
                                    continue;
                                }

                                this.dao.Insert(haikiNameEntity);
                            }
                            else
                            {
                                this.dao.Update(haikiNameEntity);
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
                        foreach (M_HAIKI_NAME haikiNameEntity in this.entitys)
                        {
                            M_HAIKI_NAME entity = this.dao.GetDataByCd(haikiNameEntity.HAIKI_NAME_CD);
                            if (entity != null)
                            {
                                this.dao.Update(haikiNameEntity);
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

            HaikiNameHoshuLogic localLogic = other as HaikiNameHoshuLogic;
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
        public bool SetIchiran()
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

                // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
                bool catchErr = this.SetFixedIchiran();
                if (catchErr)
                {
                    return true;
                }

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M243", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
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

            //削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //キー機能削除
            ////ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            //parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //表示時処理
            this.form.Shown += new System.EventHandler(this.form.Form_Shown);
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
            M_HAIKI_NAME entity = new M_HAIKI_NAME();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }

            this.SearchString = entity;
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

        /// <summary>
        /// テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可にする
        /// </summary>
        internal bool SetFixedIchiran()
        {
            try
            {
                foreach (Row row in this.form.Ichiran.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        this.SetFixedRow(row);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetFixedIchiran", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// テーブル固定値の変更不可処理を行います
        /// </summary>
        /// <param name="eRow">変更不可処理を行うための判定データが入力されているRow</param>
        internal void SetFixedRow(Row row)
        {
            var objValue = row[Const.HaikiNameHoshuConstans.HAIKI_NAME_CD].Value;
            bool catchErr = false;
            if (this.CheckFixedRow(row, out catchErr) && !catchErr)
            {
                foreach (var columnName in Const.HaikiNameHoshuConstans.fixedColumnList)
                {
                    row[columnName].ReadOnly = true;
                    row[columnName].Selectable = false;
                    row[columnName].UpdateBackColor(false);
                }
            }
            else if (catchErr)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// テーブル固定値のデータかどうかを調べる
        /// </summary>
        /// <param name="eRow">行データ</param>
        /// <returns>結果</returns>
        internal bool CheckFixedRow(Row row, out bool catchErr)
        {
            try
            {
                catchErr = false;
                var objValue = row[Const.HaikiNameHoshuConstans.HAIKI_NAME_CD].Value;

                if (row != null && objValue != null)
                {
                    var strCd = objValue.ToString();
                    if (Const.HaikiNameHoshuConstans.fixedRowList.Contains(strCd))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckFixedRow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.HAIKI_NAME_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["HAIKI_NAME_CD"]).Where(c => c.Value != null).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allEntityList.Contains(c.Value.ToString());
                                                c.UpdateBackColor(false);
                                            });
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
    }
}