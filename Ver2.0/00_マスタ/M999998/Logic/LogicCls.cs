// $Id: LogicCls.cs 43108 2015-02-26 00:37:53Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.UriageHoshu.APP;
using Shougun.Core.Master.UriageHoshu.Const;
using Shougun.Core.Master.UriageHoshu.Dao;

namespace Shougun.Core.Master.UriageHoshu.Logic
{
    /// <summary>
    /// グループ画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.UriageHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// グループ画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// グループのエンティティ
        /// </summary>
        private M_URIAGE[] entitys;

        /// <summary>
        /// グループのDao
        /// </summary>
        private DaoCls dao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// メッセージ出力用のユーティリティ
        /// </summary>
        private MessageUtility MessageUtil;

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_URIAGE SearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>

        #endregion

        #region 初期化処理

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            // メッセージ出力用のユーティリティ
            MessageUtil = new MessageUtility();
            this.dao = DaoInitUtility.GetComponent<DaoCls>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd(targetForm);
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                //Nhận đối tượng biểu mẫu gốc
                parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                //Khởi tạo văn bản nút
                this.ButtonInit();

                // イベントの初期化処理
                //Xử lý khởi tạo sự kiện
                this.EventInit();

                this.allControl = this.form.allControl;

                // システム情報を取得し、初期値をセットする
                //Nhận thông tin hệ thống và đặt giá trị ban đầu
                //GetSysInfoInit();

                // 処理No（ESC)を入力不可にする
                //Tắt đầu vào của quy trình Không (ESC)
                this.parentForm.txb_process.Enabled = false;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    // システム情報を取得し、初期値をセットする
                    //Nhận thông tin hệ thống và đặt giá trị ban đầu
                    this.GetSysInfoInit();
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M204", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン初期化処理

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (MasterBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン設定の読込

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (MasterBaseForm)this.form.Parent;

                // 削除ボタン(F4)イベント生成
                //Tạo sự kiện nút xóa (F4)
                this.form.C_MasterRegist(parentForm.bt_func4);
                parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
                parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

                //CSV出力ボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

                //条件クリアボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);
                //parentForm.bt_func7.CausesValidation = false;

                //検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);
                //parentForm.bt_func8.CausesValidation = false;

                //登録ボタン(F9)イベント生成
                this.form.C_MasterRegist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);
                //parentForm.bt_func11.CausesValidation = false;

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);


                //quoc-begin
                parentForm.bt_process1.Click += new EventHandler(this.form.Chaneged);
                //quoc-end
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 参照モード表示

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

        #endregion

        #region 検索条件初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;
                //this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = false;
                //this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                //this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public bool SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_URIAGE entity = new M_URIAGE();

                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
                {
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                    {
                        //必要？
                        // 削除項目が選択された場合
                        //if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        //    this.form.CONDITION_VALUE.DBFieldsName.Equals("DELETE_FLG"))
                        //{
                        //    if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                        //        "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                        //        "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        //    {
                        //        this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
                        //    }
                        //    else if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                        //        "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                        //        "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        //    {
                        //        this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                        //    }
                        //    else
                        //    {
                        //        return false;
                        //    }
                        //}

                        //検索条件の設定
                        entity.SetValue(this.form.CONDITION_VALUE);
                    }
                }
                //quoc-begin
                if (parentForm.bt_process1.Text == "[1]売上")
                {
                    entity.DENPYOU_KBN_CD = 2;
                }
                else
                {
                    entity.DENPYOU_KBN_CD = 1;
                }
                //quoc-end
                this.SearchString = entity;
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索結果を一覧に設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #endregion

        #region 業務処理

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 論理削除処理

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                if (!isSelect)
                {
                    msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    using (Transaction tran = new Transaction())
                    {
                        var result = msgLogic.MessageBoxShow("C021");
                        if (result == DialogResult.Yes)
                        {
                            if (this.entitys != null)
                            {
                                foreach (M_URIAGE contenashuruiEntity in this.entitys)
                                {
                                    if (contenashuruiEntity.GURUUPU_CD == null)
                                    {
                                        msgLogic.MessageBoxShow("E075", "削除");
                                        return;
                                    }
                                    M_URIAGE entity = this.dao.GetDataByCd(contenashuruiEntity.GURUUPU_CD.ToString(), contenashuruiEntity.DENPYOU_KBN_CD.ToString());
                                    if (entity != null)
                                    {
                                        this.dao.Update(contenashuruiEntity);
                                    }
                                }
                            }
                            tran.Commit();
                            msgLogic.MessageBoxShow("I001", "削除");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //例外はここで処理
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラーの場合
                    LogUtility.Error(ConstCls.ExceptionErrMsg.HAITA, ex);
                }
                else
                {
                    LogUtility.Error(ConstCls.ExceptionErrMsg.REIGAI, ex);
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 物理削除処理

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        public void CSVOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_URIAGE), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                ClearCondition();
                SetIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region データ取得処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int count = 0;
            try
            {
                // エラーの場合、０件を戻る
                if (!SetSearchString())
                {
                    return 0;
                }

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                //this.SearchResult = ConstCls.ConvertGridViewDataTbl(this.SearchResult);
                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked; ;

                Properties.Settings.Default.Save();
                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            LogUtility.DebugMethodEnd(count);
            return count;
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual bool RegistData(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        if (this.entitys != null)
                        {
                            // トランザクション開始
                            foreach (M_URIAGE contenashuruiEntity in this.entitys)
                            {
                                M_URIAGE entity = this.dao.GetDataByCd(contenashuruiEntity.GURUUPU_CD.ToString(), contenashuruiEntity.DENPYOU_KBN_CD.ToString());
                                if (entity == null)
                                {
                                    this.dao.Insert(contenashuruiEntity);
                                }
                                else
                                {
                                    this.dao.Update(contenashuruiEntity);
                                }
                            }
                        }
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                return false;
            }

            LogUtility.DebugMethodEnd(errorFlag);
            return true;
        }

        #endregion

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var contenaCd = string.Empty;
            string[] strList;

            foreach (DataGridViewRow gcRwos in this.form.Ichiran.Rows)
            {
                if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    if (gcRwos.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(gcRwos.Cells["CREATE_USER"].Value.ToString()))
                    {
                        continue;
                    }
                    contenaCd += gcRwos.Cells["GURUUPU_CD"].Value.ToString() + ",";
                }
            }

            if (!string.IsNullOrEmpty(contenaCd))
            {
                contenaCd = contenaCd.Trim(',');
                strList = contenaCd.Split(',');
                DataTable dtTable = dao.GetDataContena(strList);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += "\n" + dr["NAME"].ToString();
                    }
          
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "グループ", "グループCD", strName);

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

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public void Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                // 検索項目を初期値にセットする
                this.form.CONDITION_VALUE.Text = "";
                this.form.CONDITION_VALUE.DBFieldsName = "";
                this.form.CONDITION_VALUE.ItemDefinedTypes = "";
                this.form.CONDITION_ITEM.Text = "";
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region グループCDの重複チェック

        /// <summary>
        /// グループCDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string GURUUPU_CD,string DENPYOU_KBN_CD, DataTable dtDetailList, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(GURUUPU_CD, DENPYOU_KBN_CD, dtDetailList);
                catchErr = true;
                // 画面で種類CD重複チェック
                //Gõ kiểm tra sao chép đĩa CD trên màn hình
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.GURUUPU_CD].Value.Equals(Convert.ToString(GURUUPU_CD)) && this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD].Value.Equals(Convert.ToString(DENPYOU_KBN_CD)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    return true;
                }

                // 検索結果で種類CD重複チェック
                //Nhập kiểm tra sao chép đĩa CD trong kết quả tìm kiếm
                //Kiểm tra có trong cờ xóa
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (GURUUPU_CD.Equals(dtDetailList.Rows[i]["GURUUPU_CD"]) && DENPYOU_KBN_CD.Equals(dtDetailList.Rows[i]["DENPYOU_KBN_CD"]))
                    {
                        return true;
                    }
                }

                // DBで種類CD重複チェック
                //Nhập kiểm tra sao chép đĩa CD trong DB
                M_URIAGE entity = this.dao.GetDataByCd(GURUUPU_CD,DENPYOU_KBN_CD);

                if (entity != null)
                {
                    return true;
                }

                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(GURUUPU_CD, DENPYOU_KBN_CD, dtDetailList);
            }
        }

        #endregion

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_URIAGE[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_URIAGE();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_URIAGE>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                //quoc-begin
                DataTable preDt = new DataTable();


                if (dt == null || dt.Rows.Count == 0)
                {
                    return false;
                }

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();
                //quoc
                //preDt = GetCloneDataTable(dt);
                // 変更分のみ取得
                //this.form.Ichiran.DataSource = dt.GetChanges();

                //quoc
               

                int count = 0;
                List<M_URIAGE> mUriageList = new List<M_URIAGE>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "" && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i]["CREATE_USER"].ToString())
                                && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null
                                || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                            {
                                isSelect = true;
                                count++;
                                continue;
                            }
                            isSelect = true;
                            var m_ContenaShuruiEntity = CreateEntityForDataGridRow(dt.Rows[i]);
                            var dataBinderEntry = new DataBinderLogic<M_URIAGE>(m_ContenaShuruiEntity);
                            dataBinderEntry.SetSystemProperty(m_ContenaShuruiEntity, true);
                            m_ContenaShuruiEntity.DELETE_FLG = true;
                            mUriageList.Add(m_ContenaShuruiEntity);

                        }
                    }

                    
                }

                if (isDelete)
                {
                    this.entitys = mUriageList.ToArray();
                    if (this.entitys.Length == 0 && count == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    // 変更分のみ取得
                    List<M_URIAGE> addList = new List<M_URIAGE>();
                    if (dt.GetChanges() == null)
                    {
                        this.entitys = new List<M_URIAGE>().ToArray();
                        return false;
                    }

                    // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!DataTableUtility.IsDataRowChanged(row))
                        {
                            row.AcceptChanges();
                        }
                    }

                    if (dt.GetChanges() == null)
                    {
                        return true;
                    }

                    // 変更したデータ取得
                    var rows = dt.GetChanges().Select("DELETE_FLG = 0");

                    // データ変更なし
                    if (rows.Length == 0)
                    {
                        this.entitys = new List<M_URIAGE>().ToArray();
                        return false;
                    }

                    var contenashuruiEntityList = CreateEntityForDataGrid(rows);
                    for (int i = 0; i < contenashuruiEntityList.Count; i++)
                    {
                        var contenashuruiEntity = contenashuruiEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (this.form.Ichiran.Rows[j].Cells[ConstCls.GURUUPU_CD].Value.Equals(Convert.ToString(contenashuruiEntity.GURUUPU_CD)) &&
                                     bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(contenashuruiEntity, false);
                                var dataBinderEntry = new DataBinderLogic<M_URIAGE>(contenashuruiEntity);
                                dataBinderEntry.SetSystemProperty(contenashuruiEntity, false);
                                addList.Add(contenashuruiEntity);
                                break;
                            }
                        }
                        if (addList.Count > 0)
                        {
                            this.entitys = addList.ToArray();
                        }
                        else
                        {
                            this.entitys = new List<M_URIAGE>().ToArray();
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isDelete);
            }
        }
        

        

        #endregion

        #region CreateEntityForDataGrid

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_URIAGE> CreateEntityForDataGrid(IEnumerable<DataRow> rows)
        {
            try
            {
                LogUtility.DebugMethodStart(rows);

                var entityList = rows.Select(r => CreateEntityForDataGridRow(r)).ToList();

                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGrid", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(rows);
            }
        }

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mUriage
        /// </returns>
        internal M_URIAGE CreateEntityForDataGridRow(DataRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_URIAGE mUriage = new M_URIAGE();

                // GURUUPU_CD
                if (!DBNull.Value.Equals(row.Field<string>("GURUUPU_CD")))
                {
                    mUriage.GURUUPU_CD = row.Field<string>("GURUUPU_CD");
                }

                // GURUUPU_MEI
                if (!DBNull.Value.Equals(row.Field<string>("GURUUPU_MEI")))
                {
                    mUriage.GURUUPU_MEI = row.Field<string>("GURUUPU_MEI");
                }
                // DELETE_FLG
                if (!DBNull.Value.Equals(row.Field<bool>("DELETE_FLG")))
                {
                    mUriage.DELETE_FLG = row.Field<bool>("DELETE_FLG");
                }
                else
                {
                    mUriage.DELETE_FLG = false;
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row.Field<byte[]>("TIME_STAMP")))
                {
                    mUriage.TIME_STAMP = row.Field<byte[]>("TIME_STAMP");
                }
                //quoc-begin
                //bt_process1
                if (parentForm.bt_process1.Text == "[1]支払")
                {
                    mUriage.DENPYOU_KBN_CD = 1;
                }
                else
                {
                    mUriage.DENPYOU_KBN_CD = 2;
                }
                //quoc-end
                return mUriage;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGridRow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(row);
            }
        }

        #endregion

        #region Mulit行メッセージを生成

        /// <summary>
        /// Mulit行メッセージを生成
        /// </summary>
        /// <param name="msgID">メッセージID</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private string CreateMulitMessage(string msgID, params string[] str)
        {
            // 整形済みメッセージ
            string msgResult = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(msgID, str);

                // メッセージ原本
                MessageUtil = new MessageUtility();
                string msg = MessageUtil.GetMessage("E001").MESSAGE;

                for (int i = 0; i < str.Length; i++)
                {
                    string msgTmp = string.Format(msg, str[i]);
                    if (!string.IsNullOrEmpty(msgResult))
                    {
                        msgResult += "\r\n";
                    }
                    msgResult += msgTmp;
                }

                return msgResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstCls.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(msgID, str);
            }
        }

        #endregion

        #region DataGridViewデータ件数チェック処理

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran.Rows.Count > 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ActionBeforeCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region NOT NULL制約を一時的に解除

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ColumnAllowDBNull", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region レコード選択チェック処理

        /// <summary>
        /// レコード選択チェック処理
        /// </summary>
        public bool isSelectFlg()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!isSelect)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("isSelectFlg", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            bool ret = true;
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.GURUUPU_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["GURUUPU_CD"]).Where(c => c.Value != null).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value.ToString()));
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EditableToPrimaryKey", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Title処理
        /// </summary>
        [Transaction]
        public virtual bool TitleInit()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.form.Parent;

                var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");

                //システム設定より画面Title取得
                if (parentForm.bt_process1.Text == "")
                {
                    switch (this.sysInfoEntity.DEFAULT_GURUUPU.Value.ToString())
                    {
                        case "1":
                            parentForm.bt_process1.Text = "[1]売上";
                            //this.form.DENPYOU_KBN_CD_1.Text = "2";
                            break;
                        case "2":
                            parentForm.bt_process1.Text = "[1]支払";
                            //this.form.DENPYOU_KBN_CD_1.Text = "1";
                            break;
                        default:
                            break;
                    }
                }

                if ("[1]売上".Equals(parentForm.bt_process1.Text))
                {
                    titleControl.Text = "グループ入力（売上）";
                    this.form.DENPYOU_KBN_CD_1.Text = "1";
                    parentForm.bt_process1.Text = "[1]支払";
                    parentForm.txb_process.Text = "1";
                }
                else if ("[1]支払".Equals(parentForm.bt_process1.Text))
                {
                    titleControl.Text = "グループ入力（支払）";
                    this.form.DENPYOU_KBN_CD_1.Text = "2";
                    parentForm.bt_process1.Text = "[1]売上";
                    parentForm.txb_process.Text = "2";
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TitleInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }
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

    }
}