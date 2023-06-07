﻿// $Id: KongouShuruiHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KongouShuruiHoshu.APP;
using KongouShuruiHoshu.Validator;
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

namespace KongouShuruiHoshu.Logic
{
    /// <summary>
    /// 混合種類保守画面のビジネスロジック
    /// </summary>
    public class KongouShuruiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "KongouShuruiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_KONGOU_DATA_SQL = "KongouShuruiHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_KONGOU_DATA_SQL = "KongouShuruiHoshu.Sql.GetKongouDataSql.sql";

        private readonly string GET_HAIKI_DATA_SQL = "KongouShuruiHoshu.Sql.GetHaikidataSql.sql";

        private readonly string CHECK_DELETE_KONGOUSHURUI_SQL = "KongouShuruiHoshu.Sql.CheckDeleteKongouShuruiSql.sql";

        /// <summary>
        /// 混合種類保守画面Form
        /// </summary>
        private KongouShuruiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_KONGOU_SHURUI[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 混合種類のDao
        /// </summary>
        private IM_KONGOU_SHURUIDao dao;

        /// <summary>
        /// 廃棄物区のDao
        /// </summary>
        private IM_HAIKI_KBNDao daoHaiki;

        private IM_KONGOU_HAIKIBUTSUDao daoKongouHaikibutsu;

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
        public M_KONGOU_SHURUI SearchString { get; set; }

        /// <summary>
        /// 検索結果(廃棄物区分)
        /// </summary>
        public DataTable SearchResultHaiki { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public KongouShuruiHoshuLogic(KongouShuruiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_KONGOU_SHURUIDao>();
            this.daoHaiki = DaoInitUtility.GetComponent<IM_HAIKI_KBNDao>();
            this.daoKongouHaikibutsu = DaoInitUtility.GetComponent<IM_KONGOU_HAIKIBUTSUDao>();
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
                this.form.HAIKI_KBN_CD.Text = Properties.Settings.Default.HaikiValue_Text;
                if (!string.IsNullOrWhiteSpace(this.form.HAIKI_KBN_CD.Text))
                {
                    M_HAIKI_KBN haikiKbn = this.daoHaiki.GetDataByCd(this.form.HAIKI_KBN_CD.Text);
                    if (haikiKbn != null)
                    {
                        this.form.HAIKI_KBN_NAME_RYAKU.Text = haikiKbn.HAIKI_KBN_NAME_RYAKU;
                    }
                }

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                LogUtility.DebugMethodEnd();
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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_KONGOU_DATA_SQL
                                                                       , this.SearchString
                                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_KONGOU_DATA_SQL
                                                                       , this.SearchString
                                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                M_KONGOU_SHURUI searchParams = new M_KONGOU_SHURUI();
                searchParams.HAIKI_KBN_CD = this.SearchString.HAIKI_KBN_CD;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_KONGOU_DATA_SQL, searchParams);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.HaikiValue_Text = this.form.HAIKI_KBN_CD.Text;

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

                var entityList = new M_KONGOU_SHURUI[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KONGOU_SHURUI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KONGOU_SHURUI>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KongouShuruiHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                foreach (DataRow row in dt.Rows)
                {
                    if (!DataTableUtility.IsDataRowChanged(row))
                    {
                        row.AcceptChanges();
                    }
                }

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var kongouEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_KONGOU_SHURUI> addList = new List<M_KONGOU_SHURUI>();
                foreach (var kongouEntity in kongouEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.KongouShuruiHoshuConstans.KONGOU_SHURUI_CD) && n.Value.ToString().Equals(kongouEntity.KONGOU_SHURUI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.KongouShuruiHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            kongouEntity.SetValue(this.form.HAIKI_KBN_CD);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kongouEntity);
                            addList.Add(kongouEntity);
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
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979

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
        /// 混合種類CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                KongouShuruiHoshuValidator vali = new KongouShuruiHoshuValidator();
                bool result = vali.KongouShuruiCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch, this.form.HAIKI_KBN_CD.Text);

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                var kongouShuruiCd = string.Empty;
                string[] strList;

                foreach (Row gcRwos in this.form.Ichiran.Rows)
                {
                    if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        kongouShuruiCd += gcRwos.Cells["KONGOU_SHURUI_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(kongouShuruiCd))
                {
                    kongouShuruiCd = kongouShuruiCd.Substring(0, kongouShuruiCd.Length - 1);
                    strList = kongouShuruiCd.Split(',');
                    DataTable dtTable = dao.GetDataBySqlFileCheck(this.CHECK_DELETE_KONGOUSHURUI_SQL, strList, int.Parse(this.form.HAIKI_KBN_CD.Text));
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E258", "混合種類", "混合種類CD", strName);

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
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
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
                msgLogic.MessageBoxShow("C011", "混合種類一覧表");

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

                LogUtility.DebugMethodEnd();
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
                //20150415 minhhoang edit #1748
                //ClearCondition();
                ClearConditionF7();
                //20150415 minhhoang end edit #1748
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
                        foreach (M_KONGOU_SHURUI kongouEntity in this.entitys)
                        {
                            M_KONGOU_SHURUI entity = this.dao.GetDataByCd(kongouEntity);
                            if (entity == null)
                            {
                                //
                                if (kongouEntity.DELETE_FLG)
                                {
                                    continue;
                                }

                                this.dao.Insert(kongouEntity);
                            }
                            else
                            {
                                this.dao.Update(kongouEntity);
                            }

                            this.UpdateRelationInfo(kongouEntity);
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
                        foreach (M_KONGOU_SHURUI kongouEntity in this.entitys)
                        {
                            M_KONGOU_SHURUI entity = this.dao.GetDataByCd(kongouEntity);
                            if (entity != null)
                            {
                                this.dao.Update(kongouEntity);
                            }

                            this.UpdateRelationInfo(kongouEntity);
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

            KongouShuruiHoshuLogic localLogic = other as KongouShuruiHoshuLogic;
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

                // テーブルデータの空判断
                if (table != null)
                {
                    table.BeginLoadData();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }
                }

                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M236", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
                LogUtility.DebugMethodEnd();
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
            M_KONGOU_SHURUI entity = new M_KONGOU_SHURUI();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }
            if (!string.IsNullOrEmpty(this.form.HAIKI_KBN_CD.Text))
            {
                // 検索条件の設定
                entity.SetValue(this.form.HAIKI_KBN_CD);
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
            this.form.HAIKI_KBN_CD.Text = string.Empty;
            this.form.HAIKI_KBN_NAME_RYAKU.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        #region 20150415 minhhoang edit #1748

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearConditionF7()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
        }

        #endregion

        /// <summary>
        /// 混合種類入力に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_KONGOU_SHURUI entity)
        {
            if (entity == null
                || string.IsNullOrEmpty(entity.KONGOU_SHURUI_CD))
            {
                return;
            }

            // 混合廃棄物
            M_KONGOU_HAIKIBUTSU cond = new M_KONGOU_HAIKIBUTSU();
            cond.KONGOU_SHURUI_CD = entity.KONGOU_SHURUI_CD;
            M_KONGOU_HAIKIBUTSU[] kongouHaiki = this.daoKongouHaikibutsu.GetAllValidData(cond);
            if (kongouHaiki != null)
            {
                foreach (M_KONGOU_HAIKIBUTSU target in kongouHaiki)
                {
                    this.daoKongouHaikibutsu.Update(target);
                }
            }
        }

        /// <summary>
        /// 廃棄物区分名称情報の取得
        /// </summary>
        [Transaction]
        public virtual bool SearchHaikiName()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultHaiki = daoHaiki.GetDataBySqlFile(this.GET_HAIKI_DATA_SQL, new M_HAIKI_KBN());

                if (this.SearchResultHaiki.Rows != null)
                {
                    this.SetHaikiName();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHaikiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 廃棄物区分名称の設定
        /// </summary>
        private void SetHaikiName()
        {
            if (this.SearchResultHaiki.Rows.Count == 0)
            {
                this.form.HAIKI_KBN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            foreach (DataRow row in this.SearchResultHaiki.Rows)
            {
                this.form.HAIKI_KBN_NAME_RYAKU.Text = string.Empty;

                if (this.form.HAIKI_KBN_CD.Text == row["HAIKI_KBN_CD"].ToString())
                {
                    this.form.HAIKI_KBN_NAME_RYAKU.Text = row["HAIKI_KBN_NAME_RYAKU"].ToString();
                    break;
                }
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
                var allEntityList = this.dao.GetAllData().Select(s => s.KONGOU_SHURUI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["KONGOU_SHURUI_CD"]).Where(c => c.Value != null).ToList().
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