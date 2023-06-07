﻿// $Id:NyuushukkinKbnHoshuLogic.cs 199 2013-06-25 10:02:50Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using NyuushukkinKbnHoshu.APP;
using NyuushukkinKbnHoshu.Validator;
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

namespace NyuushukkinKbnHoshu.Logic
{
    /// <summary>
    /// 入出金区分画面のビジネスロジック
    /// </summary>
    public class NyuushukkinKbnHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "NyuushukkinKbnHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_NYUUSHUKKINKBN_DATA_SQL = "NyuushukkinKbnHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_NYUUSHUKKIN_KBN_DATA_SQL = "NyuushukkinKbnHoshu.Sql.GetNyuushukkinKbnHoshuDataSql.sql";

        private readonly string CHECK_DELETE_NYUUSHUKKIN_KBN_SQL = "NyuushukkinKbnHoshu.Sql.CheckDeleteNyuushukkinKbnSql.sql";

        /// <summary>
        /// 入出金区分画面Form
        /// </summary>
        private NyuushukkinKbnHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_NYUUSHUKKIN_KBN[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 入出金区分のDao
        /// </summary>
        private IM_NYUUSHUKKIN_KBNDao dao;

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
        public M_NYUUSHUKKIN_KBN SearchString { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public NyuushukkinKbnHoshuLogic(NyuushukkinKbnHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
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

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (MasterBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

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

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_NYUUSHUKKINKBN_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_NYUUSHUKKINKBN_DATA_SQL
                                            , this.SearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_NYUUSHUKKIN_KBN_DATA_SQL, new M_NYUUSHUKKIN_KBN());

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
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_NYUUSHUKKIN_KBN[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_NYUUSHUKKIN_KBN();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_NYUUSHUKKIN_KBN>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.NyuushukkinKbnHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var nyuushukkinEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_NYUUSHUKKIN_KBN> addList = new List<M_NYUUSHUKKIN_KBN>();
                foreach (var nyuushukkinkEntity in nyuushukkinEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD) && n.Value.ToString().Equals(Convert.ToString(nyuushukkinkEntity.NYUUSHUKKIN_KBN_CD)))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.NyuushukkinKbnHoshuConstans.DELETE_FLG) && (n.FormattedValue == null || bool.Parse(n.FormattedValue.ToString()) == isDelete))))
                        {
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), nyuushukkinkEntity);
                            addList.Add(nyuushukkinkEntity);
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;
                this.entitys = addList.ToArray();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var nyuushukkinKbnCd = string.Empty;
            string[] strList;

            foreach (Row gcRwos in this.form.Ichiran.Rows)
            {
                if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    nyuushukkinKbnCd += gcRwos.Cells["NYUUSHUKKIN_KBN_CD"].Value.ToString() + ",";
                }
            }

            if (!string.IsNullOrEmpty(nyuushukkinKbnCd))
            {
                nyuushukkinKbnCd = nyuushukkinKbnCd.Substring(0, nyuushukkinKbnCd.Length - 1);
                strList = nyuushukkinKbnCd.Split(',');
                DataTable dtTable = dao.GetDataBySqlFileCheck(this.CHECK_DELETE_NYUUSHUKKIN_KBN_SQL, strList);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += Environment.NewLine + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "入出金区分", "入出金区分CD", strName);

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

        /// <summary>
        /// 取り消し処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 入出金区分CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                NyuushukkinKbnHoshuValidator vali = new NyuushukkinKbnHoshuValidator();
                return vali.NyuushukkinKbnCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
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

                msgLogic.MessageBoxShow("C011", "入出金区分一覧表");

                MessageBox.Show("未実装");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                SetSearchString();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
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

                // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
                this.SetFixedIchiran();

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M267", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
                        foreach (M_NYUUSHUKKIN_KBN nyuushukkinEntity in this.entitys)
                        {
                            M_NYUUSHUKKIN_KBN m = this.dao.GetDataByCd(nyuushukkinEntity.NYUUSHUKKIN_KBN_CD.Value);
                            if (m == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (nyuushukkinEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(nyuushukkinEntity);
                            }
                            else
                            {
                                this.dao.Update(nyuushukkinEntity);
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
                        foreach (M_NYUUSHUKKIN_KBN nyuushukkinEntity in this.entitys)
                        {
                            M_NYUUSHUKKIN_KBN entity = this.dao.GetDataByCd(nyuushukkinEntity.NYUUSHUKKIN_KBN_CD.Value);
                            if (entity != null)
                            {
                                this.dao.Update(nyuushukkinEntity);
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
        /// インスタンスが等しいかどうか判定
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

            NyuushukkinKbnHoshuLogic localLogic = other as NyuushukkinKbnHoshuLogic;
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

            //ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

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

            //検索条件イベント生成
            this.form.CONDITION_VALUE.KeyPress += new KeyPressEventHandler(CONDITION_VALUE_KeyPress);

            // CD表示変更
            //this.form.Ichiran.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.CdFormatting);

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
        private Boolean SetSearchString()
        {
            M_NYUUSHUKKIN_KBN entity = new M_NYUUSHUKKIN_KBN();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }
            this.SearchString = entity;
            return true;
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

        ///// <summary>
        ///// CD表示変更処理
        ///// </summary>
        ///// <param name="e"></param>
        //public void CdFormatting(CellFormattingEventArgs e)
        //{
        //    if ((e.Value == null) || (string.IsNullOrEmpty(e.Value.ToString())))
        //    {
        //        return;
        //    }

        //    // ゼロパディング後、テキストへ設定
        //    e.Value = String.Format("{0:D" + NyuushukkinKbnHoshuConstans.CD_MAXLENGTH + "}", int.Parse(e.Value.ToString()));
        //}

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
            var objValue = row[Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD].Value;

            bool catchErr = false;
            if (this.CheckFixedRow(row, out catchErr) && !catchErr)
            {
                foreach (var columnName in Const.NyuushukkinKbnHoshuConstans.fixedColumnList)
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
                var objValue = row[Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD].Value;
                int iValue;

                if (row != null && objValue != null && int.TryParse(objValue.ToString(), out iValue))
                {
                    var strCd = objValue.ToString();
                    if (Const.NyuushukkinKbnHoshuConstans.fixedRowList.Contains(iValue))
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
                var allEntityList = this.dao.GetAllData().Select(s => s.NYUUSHUKKIN_KBN_CD).Where(s => !s.Value.Equals(DBNull.Value)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["NYUUSHUKKIN_KBN_CD"]).Where(c => !c.Value.Equals(DBNull.Value)).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allEntityList.Contains((Int16)c.Value);
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

        /// <summary>
        /// 検索条件が数字のみ入力.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD))
            {
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Tab && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        #region 検索条件チェック

        /// <summary>
        /// 検索文字列が検索項目に対して不正な文字かのチェックを行う
        /// </summary>
        /// <returns>True：正常　False：不正</returns>
        public bool CheckSearchString()
        {
            // SetSearchStringメソッド中のentity.SetValueで値によってはFormatでシステムエラーになるためチェックを行う。
            // 現在は、数値項目のみエラーが発生するため、該当項目のみチェックを行っている。
            // 汎用的に行うのであればSetValueで扱っている全ての型に対してチェックを行う。

            LogUtility.DebugMethodStart();

            bool retVal = true;

            if (string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName) || string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
            {
                return retVal;
            }

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                /* チェック実行 */
                if (this.form.CONDITION_VALUE.ItemDefinedTypes.ToLower() == DB_TYPE.SMALLINT.ToTypeString())
                {
                    short dummy = 0;
                    retVal = short.TryParse(this.form.CONDITION_VALUE.Text, out dummy);
                }
            }

            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        #endregion
    }
}