﻿using System;
using System.Data;
using System.Linq;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Windows.Forms;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.ShukkinYoteiIchiran
{
    /// <summary>
    /// 出金予定一覧出力画面ロジッククラス
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.ShukkinYoteiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        internal MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderBaseForm targetHeader = (HeaderBaseForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool CSV(DtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                bool checkSql = false;
                var dao = DaoInitUtility.GetComponent<DaoClass>();
                DataTable dt;
                string[] csvHeader = new string[5];
                if (ConstClass.SHOSHIKI_T.ToString() == this.form.SHOSHIKI_NUM.Text)
                {
                    switch (this.form.SORT_1_KOUMOKU.Text)
                    {
                        case "1":
                            csvHeader[0] = "営業担当者CD";
                            csvHeader[1] = "営業担当者";
                            csvHeader[2] = "出金予定日";
                            csvHeader[3] = "取引先CD";
                            csvHeader[4] = "取引先";
                            break;
                        case "2":
                            csvHeader[0] = "取引先CD";
                            csvHeader[1] = "取引先";
                            csvHeader[2] = "出金予定日";
                            csvHeader[3] = "営業担当者CD";
                            csvHeader[4] = "営業担当者";
                            break;
                        case "3":
                            csvHeader[0] = "出金予定日";
                            csvHeader[1] = "取引先CD";
                            csvHeader[2] = "取引先";
                            csvHeader[3] = "営業担当者CD";
                            csvHeader[4] = "営業担当者";
                            break;
                    }
                    string[] csvHead = { csvHeader[0], csvHeader[1], csvHeader[2], csvHeader[3], csvHeader[4],
                                       "出金額", "締日", "精算日","支払方法"};
                    for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }
                    dt = dao.GetShukkinYoteiIchiranData(dto);
                    if (0 < dt.Rows.Count)
                    {
                        checkSql = true;
                    }
                }
                else
                {
                    if (this.form.SORT_1_KOUMOKU.Text.Equals("2"))
                    {
                        string[] csvHead = { "取引先CD", "取引先", "業者CD", "業者", "現場CD", "現場",
                                       "出金予定日", "営業担当者CD", "営業担当者", "出金額", "締日", "精算日"};
                        for (int i = 0; i < csvHead.Length; i++)
                        {
                            csvDT.Columns.Add(csvHead[i]);
                        }
                    }
                    else if (this.form.SORT_1_KOUMOKU.Text.Equals("3"))
                    {
                        string[] csvHead = { "出金予定日", "取引先CD", "取引先", "業者CD", "業者", "現場CD", "現場", "営業担当者CD", "営業担当者", 
                                             "出金額", "締日", "精算日","支払方法"};
                        for (int i = 0; i < csvHead.Length; i++)
                        {
                            csvDT.Columns.Add(csvHead[i]);
                        }
                    }
                    dt = dao.GetShukkinYoteiData(dto);
                    if (0 < dt.Rows.Count)
                    {
                        checkSql = true;
                    }
                }

                if (0 == dt.Rows.Count)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
                if (checkSql == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rowTmp = csvDT.NewRow();

                        if (ConstClass.SHOSHIKI_G.ToString() == this.form.SHOSHIKI_NUM.Text)
                        {
                            if (dt.Rows[i]["GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GYOUSHA_CD"].ToString()))
                            {
                                rowTmp["業者CD"] = dt.Rows[i]["GYOUSHA_CD"].ToString();
                            }

                            if (dt.Rows[i]["GYOUSHA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["業者"] = dt.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                            }

                            if (dt.Rows[i]["GENBA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GENBA_CD"].ToString()))
                            {
                                rowTmp["現場CD"] = dt.Rows[i]["GENBA_CD"].ToString();
                            }

                            if (dt.Rows[i]["GENBA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GENBA_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["現場"] = dt.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                            }

                            if (dt.Rows[i]["EIGYOU_TANTOU_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["EIGYOU_TANTOU_CD"].ToString()))
                            {
                                rowTmp["営業担当者CD"] = dt.Rows[i]["EIGYOU_TANTOU_CD"].ToString();
                            }
                        }
                        else
                        {
                            if (dt.Rows[i]["EIGYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["EIGYOUSHA_CD"].ToString()))
                            {
                                rowTmp["営業担当者CD"] = dt.Rows[i]["EIGYOUSHA_CD"].ToString();
                            }
                        }
                        if (dt.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                        {
                            rowTmp["取引先CD"] = dt.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        }

                        if (dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["取引先"] = dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["SHAIN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHAIN_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["営業担当者"] = dt.Rows[i]["SHAIN_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["SHUKKIN_YOTEI_BI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHUKKIN_YOTEI_BI"].ToString()))
                        {
                            rowTmp["出金予定日"] = dt.Rows[i]["SHUKKIN_YOTEI_BI"].ToString();
                        }

                        if (dt.Rows[i]["SHIMEBI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHIMEBI"].ToString()))
                        {
                            rowTmp["締日"] = dt.Rows[i]["SHIMEBI"].ToString();
                        }

                        if (dt.Rows[i]["SEISAN_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SEISAN_DATE"].ToString()))
                        {
                            rowTmp["精算日"] = dt.Rows[i]["SEISAN_DATE"].ToString();
                        }

                        if (dt.Rows[i]["SHUKKIN_GAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHUKKIN_GAKU"].ToString()))
                        {
                            rowTmp["出金額"] = Decimal.Parse(dt.Rows[i]["SHUKKIN_GAKU"].ToString()).ToString("#,##0");
                        }

                        if (!this.form.SHOSHIKI_NUM.Text.Equals("2") || (this.form.SHOSHIKI_NUM.Text.Equals("2") && this.form.SORT_1_KOUMOKU.Text.Equals("3")))
                        {
                            if (dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["支払方法"] = dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString();
                            }
                        }

                        csvDT.Rows.Add(rowTmp);
                    }
                    this.form.CsvReport(csvDT);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool Search(DtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                var dao = DaoInitUtility.GetComponent<DaoClass>();
                DataTable dt;
                if (ConstClass.SHOSHIKI_T.ToString() == this.form.SHOSHIKI_NUM.Text)
                {
                    dt = dao.GetShukkinYoteiIchiranData(dto);
                    if (0 < dt.Rows.Count)
                    {
                        var reportLogic = new ReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                }
                else
                {
                    dt = dao.GetShukkinYoteiData(dto);
                    if (0 < dt.Rows.Count)
                    {
                        var reportLogic = new ReportClass();
                        reportLogic.CreateReport_G(dt, dto);
                    }
                }

                if (0 == dt.Rows.Count)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタン設定を作成します
        /// </summary>
        /// <returns>ボタン設定</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] ret;

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ret = buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// ボタンを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // CSVボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            // 表示ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            /// 20141128 Houkakou 「出金予定一覧表」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.SHUKKIN_YOTEI_TO.MouseDoubleClick += new MouseEventHandler(SHUKKIN_YOTEI_TO_MouseDoubleClick);
            this.form.SEISAN_DATE_TO.MouseDoubleClick += new MouseEventHandler(SEISAN_DATE_TO_MouseDoubleClick);
            /// 20141128 Houkakou 「出金予定一覧表」のダブルクリックを追加する　end

            /// 20141203 Houkakou 「出金予定一覧表」の日付チェックを追加する　start
            this.form.SHUKKIN_YOTEI_FROM.Leave += new System.EventHandler(SHUKKIN_YOTEI_FROM_Leave);
            this.form.SHUKKIN_YOTEI_TO.Leave += new System.EventHandler(SHUKKIN_YOTEI_TO_Leave);
            this.form.SEISAN_DATE_FROM.Leave += new System.EventHandler(SEISAN_DATE_FROM_Leave);
            this.form.SEISAN_DATE_TO.Leave += new System.EventHandler(SEISAN_DATE_TO_Leave);
            /// 20141203 Houkakou 「出金予定一覧表」の日付チェックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// 20141128 Houkakou 「出金予定一覧表」のダブルクリックを追加する　start
        #region SHUKKIN_YOTEI_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKIN_YOTEI_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHUKKIN_YOTEI_FROM;
            var ToTextBox = this.form.SHUKKIN_YOTEI_TO;

            ToTextBox.Text = FromTextBox.Text;


            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SEISAN_DATE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEISAN_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SEISAN_DATE_FROM;
            var ToTextBox = this.form.SEISAN_DATE_TO;

            ToTextBox.Text = FromTextBox.Text;


            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「出金予定一覧表」のダブルクリックを追加する　end

        /// 20141203 Houkakou 「出金予定一覧表」の日付チェックを追加する　start
        #region SHUKKIN_YOTEI_FROM_Leaveイベント
        /// <summary>
        /// SHUKKIN_YOTEI_FROM_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SHUKKIN_YOTEI_FROM_Leave(object sender, EventArgs e)
        {
            this.form.SHUKKIN_YOTEI_FROM.IsInputErrorOccured = false;
            this.form.SHUKKIN_YOTEI_FROM.BackColor = Constans.NOMAL_COLOR;

            if (!string.IsNullOrEmpty(this.form.SHUKKIN_YOTEI_TO.Text))
            {
                this.form.SHUKKIN_YOTEI_TO.IsInputErrorOccured = false;
                this.form.SHUKKIN_YOTEI_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region SHUKKIN_YOTEI_TO_Leaveイベント
        /// <summary>
        /// SHUKKIN_YOTEI_TO_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SHUKKIN_YOTEI_TO_Leave(object sender, EventArgs e)
        {
            this.form.SHUKKIN_YOTEI_TO.IsInputErrorOccured = false;
            this.form.SHUKKIN_YOTEI_TO.BackColor = Constans.NOMAL_COLOR;

            if (!string.IsNullOrEmpty(this.form.SHUKKIN_YOTEI_FROM.Text))
            {
                this.form.SHUKKIN_YOTEI_FROM.IsInputErrorOccured = false;
                this.form.SHUKKIN_YOTEI_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region SEISAN_DATE_FROM_Leaveイベント
        /// <summary>
        /// SEISAN_DATE_FROM_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SEISAN_DATE_FROM_Leave(object sender, EventArgs e)
        {
            this.form.SEISAN_DATE_FROM.IsInputErrorOccured = false;
            this.form.SEISAN_DATE_FROM.BackColor = Constans.NOMAL_COLOR;

            if (!string.IsNullOrEmpty(this.form.SEISAN_DATE_TO.Text))
            {
                this.form.SEISAN_DATE_TO.IsInputErrorOccured = false;
                this.form.SEISAN_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region SEISAN_DATE_TO_Leaveイベント
        /// <summary>
        /// SEISAN_DATE_TO_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SEISAN_DATE_TO_Leave(object sender, EventArgs e)
        {
            this.form.SEISAN_DATE_TO.IsInputErrorOccured = false;
            this.form.SEISAN_DATE_TO.BackColor = Constans.NOMAL_COLOR;

            if (!string.IsNullOrEmpty(this.form.SEISAN_DATE_FROM.Text))
            {
                this.form.SEISAN_DATE_FROM.IsInputErrorOccured = false;
                this.form.SEISAN_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「出金予定一覧表」の日付チェックを追加する　end
    }
}
