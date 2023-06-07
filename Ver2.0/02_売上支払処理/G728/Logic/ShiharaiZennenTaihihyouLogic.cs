﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou;

namespace Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou
{
    /// <summary>
    /// 支払集計表ロジッククラス
    /// </summary>
    internal class ShiharaiZennenTaihihyouLogic : IBuisinessLogic
    {
        #region 宣言
        /// <summary>
        /// 支払集計表画面クラス
        /// </summary>
        private ShiharaiZennenTaihihyouUIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 集計データを取得・設定します
        /// </summary>
        internal List<ShiharaiData> ShukeiDataList { get; private set; }

        /// <summary>
        /// 集計したデータを取得・設定します
        /// </summary>
        internal List<ShiharaiData> ShukeiDataSummaryList { get; private set; }

        /// <summary>
        /// 集計表帳票出力用DTOリストを取得・設定します
        /// </summary>
        internal List<ShiharaiZennenTaihihyouReportDto> ShukeiHyoReportDtoList { get; private set; }

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">支払集計表画面クラス</param>
        public ShiharaiZennenTaihihyouLogic(ShiharaiZennenTaihihyouUIForm targetForm)
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
            try
            {
                LogUtility.DebugMethodStart();

                this.HeaderInit();
                this.ButtonInit();
                this.EventInit();
                this.parentForm = (BusinessBaseForm)this.form.Parent;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// ヘッダを初期化します
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報を作成します
        /// </summary>
        /// <returns>ボタン情報</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, ShiharaiZennenTaihihyouConst.BUTTON_SETTING_XML);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return null;
            }
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.C_Regist(parentForm.bt_func5);
            this.form.C_Regist(parentForm.bt_func7);

            parentForm.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);
            parentForm.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);
            parentForm.bt_func4.Click += new EventHandler(this.form.ButtonFunc4_Clicked);
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            /// 20141226 Houkakou 「支払集計表」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.DATE_TO.MouseDoubleClick += new MouseEventHandler(DATE_TO_MouseDoubleClick);
            this.form.TORIHIKISAKI_CD_TO.MouseDoubleClick += new MouseEventHandler(TORIHIKISAKI_CD_TO_MouseDoubleClick);
            this.form.GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(GENBA_CD_TO_MouseDoubleClick);
            this.form.HINMEI_CD_TO.MouseDoubleClick += new MouseEventHandler(HINMEI_CD_TO_MouseDoubleClick);
            this.form.NIOROSHI_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIOROSHI_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.NIOROSHI_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIOROSHI_GENBA_CD_TO_MouseDoubleClick);
            this.form.NIZUMI_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIZUMI_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.NIZUMI_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIZUMI_GENBA_CD_TO_MouseDoubleClick);
            this.form.EIGYOU_TANTOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(EIGYOU_TANTOUSHA_CD_TO_MouseDoubleClick);
            this.form.NYUURYOKU_TANTOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(NYUURYOKU_TANTOUSHA_CD_TO_MouseDoubleClick);
            this.form.UNPAN_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(UNPAN_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.SHASHU_CD_TO.MouseDoubleClick += new MouseEventHandler(SHASHU_CD_TO_MouseDoubleClick);
            this.form.SHARYOU_CD_TO.MouseDoubleClick += new MouseEventHandler(SHARYOU_CD_TO_MouseDoubleClick);
            this.form.KEITAI_KBN_CD_TO.MouseDoubleClick += new MouseEventHandler(KEITAI_KBN_CD_TO_MouseDoubleClick);
            this.form.DAIKAN_CD_TO.MouseDoubleClick += new MouseEventHandler(DAIKAN_CD_TO_MouseDoubleClick);
            /// 20141226 Houkakou 「支払集計表」のダブルクリックを追加する　end

            this.form.SHURUI_CD_TO.MouseDoubleClick += new MouseEventHandler(SHURUI_CD_TO_MouseDoubleClick);
            this.form.BUNRUI_CD_TO.MouseDoubleClick += new MouseEventHandler(BUNRUI_CD_TO_MouseDoubleClick);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 検索
        /// <summary>
        /// 検索します
        /// </summary>
        /// <returns>件数</returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;

            try
            {
                var dao = DaoInitUtility.GetComponent<ShiharaiZennenTaihihyouDao>();

                this.ShukeiDataList = new List<ShiharaiData>();

                // 荷降が指定されたら受入・支払／支払が対象
                // 荷積が指定されたら出荷・支払／支払が対象
                // 台貫が指定されたら受入・出荷が対象

                if (this.form.FormDataDto.DenpyouShurui.ToString() == ShiharaiZennenTaihihyouConst.DENPYOU_SHURUI_CD_UKEIRE || this.form.FormDataDto.DenpyouShurui.ToString() == ShiharaiZennenTaihihyouConst.DENPYOU_SHURUI_CD_SUBETE)
                {
                    if (String.IsNullOrEmpty(this.form.FormDataDto.NizumiGyousha))
                    {
                        this.ShukeiDataList.AddRange(dao.GetShukeiHyoDataUkeire(this.form.FormDataDto));
                    }
                }
                if (this.form.FormDataDto.DenpyouShurui.ToString() == ShiharaiZennenTaihihyouConst.DENPYOU_SHURUI_CD_SHUKKA || this.form.FormDataDto.DenpyouShurui.ToString() == ShiharaiZennenTaihihyouConst.DENPYOU_SHURUI_CD_SUBETE)
                {
                    if (String.IsNullOrEmpty(this.form.FormDataDto.NioroshiGyousha))
                    {
                        this.ShukeiDataList.AddRange(dao.GetShukeiHyoDataShukka(this.form.FormDataDto));
                    }
                }
                // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) Start
                if (this.form.FormDataDto.DenpyouShurui.ToString() == ShiharaiZennenTaihihyouConst.DENPYOU_SHURUI_CD_URIAGE_SHIHARAI ||
                    this.form.FormDataDto.DenpyouShurui.ToString() == ShiharaiZennenTaihihyouConst.DENPYOU_SHURUI_CD_DAINOU ||
                    this.form.FormDataDto.DenpyouShurui.ToString() == ShiharaiZennenTaihihyouConst.DENPYOU_SHURUI_CD_SUBETE)
                {
                    if (String.IsNullOrEmpty(this.form.FormDataDto.DaikanKbn))
                    {
                        this.ShukeiDataList.AddRange(dao.GetShukeiHyoDataUriageShiharai(this.form.FormDataDto));
                    }
                }
                // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) End

                ret = this.ShukeiDataList.Count();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = - 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region Not Delete
        /// <summary>
        /// 登録します
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新します
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除します
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除します
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        /// 
        internal bool CSVPrint()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.ShukeiDataList.Count() > 0)
                {
                    var reportLogic = new ShiharaiZennenTaihihyouReportLogic();

                    this.CreateReportDtoList();
                    this.CreateSummaryData();
                    //this.CSV_CalcTotal(); 

                    // システム情報を取得して帳票出力用DTOにセット
                    var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                    var mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

                    // 自社情報マスタを取得して帳票出力用DTOにセット
                    var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                    var mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

                    this.ShukeiHyoReportDtoList.ForEach(u =>
                    {
                        u.SYS_INFO = mSysInfo;
                        u.CORP_INFO = mCorpInfo;
                    });


                    Creat_CSV(this.ConvertToDataTable(this.ShukeiHyoReportDtoList));
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateForm", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateForm", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        /// <summary>
        /// headによって対したCSV出力
        /// </summary>
        /// 
        public void Creat_CSV(DataTable dt)
        {
            try
            {
                DataTable csvDT = new DataTable();
                string head_dt = "";
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_1"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_1"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_1"].ToString());
                    head_dt += "CD_1";
                    head_dt += ",NAME_1";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_2"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_2"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_2"].ToString());
                    head_dt += ",CD_2";
                    head_dt += ",NAME_2";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_3"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_3"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_3"].ToString());
                    head_dt += ",CD_3";
                    head_dt += ",NAME_3";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_4"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_4"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_4"].ToString());
                    head_dt += ",CD_4";
                    head_dt += ",NAME_4";
                }

                csvDT.Columns.Add("今年支払金額");
                csvDT.Columns.Add("前年支払金額");
                csvDT.Columns.Add("差額");
                csvDT.Columns.Add("増加率");

                head_dt += ",FORMAT_KINGAKU,FORMAT_PAST_KINGAKU,FORMAT_SAGAKU,FORMAT_ZOKA_RITSU";
                string[] head_array = head_dt.Split(',');
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = csvDT.NewRow();
                    for (int n = 0; n < csvDT.Columns.Count; n++)
                    {

                        row[n] = dt.Rows[i][head_array[n]];
                    }
                    csvDT.Rows.Add(row);
                }

                // 出力先指定のポップアップを表示させる。
                if (this.errmessage.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    // CSV出力
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, "支払前年対比表", this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVPrint", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }
        }

        private void CSV_CalcTotal()
        {
            var allKingakuSum = 0m;
            var allPastKingakuSum = 0m;
            var allSagakuSum = 0m;
            var allZotaRitsuSum = 0m;
            var group1key = String.Empty;
            var group1kingakuSum = 0m;
            var group1PastkingakuSum = 0m;
            var group1SagakuSum = 0m;
            var group1ZotaRitsuSum = 0m;
            var group2key = String.Empty;
            var group2kingakuSum = 0m;
            var group2PastkingakuSum = 0m;
            var group2SagakuSum = 0m;
            var group2ZotaRitsuSum = 0m;
            var group3key = String.Empty;
            var group3kingakuSum = 0m;
            var group3PastkingakuSum = 0m;
            var group3SagakuSum = 0m;
            var group3ZotaRitsuSum = 0m;
            var group4key = String.Empty;
            var group4kingakuSum = 0m;
            var group4PastkingakuSum = 0m;
            var group4SagakuSum = 0m;
            var group4ZotaRitsuSum = 0m;
            foreach (var shukeiHyoReporDto in this.ShukeiHyoReportDtoList)
            {
                allKingakuSum = allKingakuSum + shukeiHyoReporDto.KINGAKU;
                allPastKingakuSum = allPastKingakuSum + shukeiHyoReporDto.PAST_KINGAKU;
                allSagakuSum = allSagakuSum + shukeiHyoReporDto.SAGAKU;

                shukeiHyoReporDto.ALL_KINGAKU_SUM = allKingakuSum;
                shukeiHyoReporDto.ALL_PAST_KINGAKU_SUM = allPastKingakuSum;
                shukeiHyoReporDto.ALL_SAGAKU_SUM = allSagakuSum;

                group1key = shukeiHyoReporDto.CD_1;
                group1kingakuSum = shukeiHyoReporDto.KINGAKU;
                group1PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                group1SagakuSum = shukeiHyoReporDto.SAGAKU;

                shukeiHyoReporDto.GROUP1_KEY = group1key;
                shukeiHyoReporDto.GROUP1_KINGAKU_SUM = group1kingakuSum;
                shukeiHyoReporDto.GROUP1_PAST_KINGAKU_SUM = group1PastkingakuSum;
                shukeiHyoReporDto.GROUP1_SAGAKU_SUM = group1SagakuSum;

                group2key = shukeiHyoReporDto.CD_2;
                group2kingakuSum = shukeiHyoReporDto.KINGAKU;
                group2PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                group2SagakuSum = shukeiHyoReporDto.SAGAKU;

                shukeiHyoReporDto.GROUP2_KEY = group2key;
                shukeiHyoReporDto.GROUP2_KINGAKU_SUM = group2kingakuSum;
                shukeiHyoReporDto.GROUP2_PAST_KINGAKU_SUM = group2PastkingakuSum;
                shukeiHyoReporDto.GROUP2_SAGAKU_SUM = group2SagakuSum;

                group3key = shukeiHyoReporDto.CD_3;
                group3kingakuSum = shukeiHyoReporDto.KINGAKU;
                group3PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                group3SagakuSum = shukeiHyoReporDto.SAGAKU;

                shukeiHyoReporDto.GROUP3_KEY = group3key;
                shukeiHyoReporDto.GROUP3_KINGAKU_SUM = group3kingakuSum;
                shukeiHyoReporDto.GROUP3_PAST_KINGAKU_SUM = group3PastkingakuSum;
                shukeiHyoReporDto.GROUP3_SAGAKU_SUM = group3SagakuSum;

                group4key = shukeiHyoReporDto.CD_4;
                group4kingakuSum = shukeiHyoReporDto.KINGAKU;
                group4PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                group4SagakuSum = shukeiHyoReporDto.SAGAKU;

                shukeiHyoReporDto.GROUP4_KEY = group4key;
                shukeiHyoReporDto.GROUP4_KINGAKU_SUM = group4kingakuSum;
                shukeiHyoReporDto.GROUP4_PAST_KINGAKU_SUM = group4PastkingakuSum;
                shukeiHyoReporDto.GROUP4_SAGAKU_SUM = group4SagakuSum;
            }

            this.ShukeiHyoReportDtoList.ForEach(u =>
            {
                u.GROUP1_ZOKA_RITSU_SUM = u.GROUP1_KINGAKU_SUM == 0 ? 0 : u.GROUP1_SAGAKU_SUM / u.GROUP1_KINGAKU_SUM * 100;
                u.GROUP2_ZOKA_RITSU_SUM = u.GROUP2_KINGAKU_SUM == 0 ? 0 : u.GROUP2_SAGAKU_SUM / u.GROUP2_KINGAKU_SUM * 100;
                u.GROUP3_ZOKA_RITSU_SUM = u.GROUP3_KINGAKU_SUM == 0 ? 0 : u.GROUP3_SAGAKU_SUM / u.GROUP3_KINGAKU_SUM * 100;
                u.GROUP4_ZOKA_RITSU_SUM = u.GROUP4_KINGAKU_SUM == 0 ? 0 : u.GROUP4_SAGAKU_SUM / u.GROUP4_KINGAKU_SUM * 100;
            });
        }
        #endregion

        #region [F7]表示
        /// <summary>
        /// 帳票を作成します
        /// </summary>
        internal bool CreateForm()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.ShukeiDataList.Count() > 0)
                {
                    var reportLogic = new ShiharaiZennenTaihihyouReportLogic();

                    this.CreateReportDtoList();
                    this.CreateSummaryData();
                    this.CalcTotal();

                    // システム情報を取得して帳票出力用DTOにセット
                    var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                    var mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

                    // 自社情報マスタを取得して帳票出力用DTOにセット
                    var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                    var mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

                    this.ShukeiHyoReportDtoList.ForEach(u =>
                    {
                        u.SYS_INFO = mSysInfo;
                        u.CORP_INFO = mCorpInfo;
                    });

                    this.CreateJoukenFieldData();

                    reportLogic.CreateReport(this.ConvertToDataTable(this.ShukeiHyoReportDtoList), this.form.FormDataDto);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateForm", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateForm", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 支払データから帳票出力用DTOリストを作成します
        /// </summary>
        private void CreateReportDtoList()
        {
            LogUtility.DebugMethodStart();

            var column1 = String.Empty;
            var cd1 = String.Empty;
            var name1 = String.Empty;
            var groupName1 = String.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(1) != null)
            {
                column1 = this.form.FormDataDto.Pattern.GetColumnSelect(1).KOUMOKU_RONRI_NAME;
                cd1 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(1).BUTSURI_NAME;
                name1 = this.GetColumnName(cd1);
                groupName1 = column1 + "合計";
            }
            var column2 = String.Empty;
            var cd2 = String.Empty;
            var name2 = String.Empty;
            var groupName2 = String.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(2) != null)
            {
                column2 = this.form.FormDataDto.Pattern.GetColumnSelect(2).KOUMOKU_RONRI_NAME;
                cd2 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(2).BUTSURI_NAME;
                name2 = this.GetColumnName(cd2);
                groupName2 = column2 + "合計";
            }
            var column3 = String.Empty;
            var cd3 = String.Empty;
            var name3 = String.Empty;
            var groupName3 = String.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(3) != null)
            {
                column3 = this.form.FormDataDto.Pattern.GetColumnSelect(3).KOUMOKU_RONRI_NAME;
                cd3 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(3).BUTSURI_NAME;
                name3 = this.GetColumnName(cd3);
                groupName3 = column3 + "合計";
            }
            var column4 = String.Empty;
            var cd4 = String.Empty;
            var name4 = String.Empty;
            var groupName4 = String.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(4) != null)
            {
                column4 = this.form.FormDataDto.Pattern.GetColumnSelect(4).KOUMOKU_RONRI_NAME;
                cd4 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(4).BUTSURI_NAME;
                name4 = this.GetColumnName(cd4);
                groupName4 = column4 + "合計";
            }

            this.ShukeiHyoReportDtoList = this.ShukeiDataList.Select(u => new ShiharaiZennenTaihihyouReportDto()
                                                             {
                                                                 COLUMN_1 = column1,
                                                                 COLUMN_2 = column2,
                                                                 COLUMN_3 = column3,
                                                                 COLUMN_4 = column4,
                                                                 CD_1 = this.ConvertToString(this.GetValue(u, cd1)),
                                                                 CD_2 = this.ConvertToString(this.GetValue(u, cd2)),
                                                                 CD_3 = this.ConvertToString(this.GetValue(u, cd3)),
                                                                 CD_4 = this.ConvertToString(this.GetValue(u, cd4)),
                                                                 NAME_1 = this.ConvertToString(this.GetValue(u, name1)),
                                                                 NAME_2 = this.ConvertToString(this.GetValue(u, name2)),
                                                                 NAME_3 = this.ConvertToString(this.GetValue(u, name3)),
                                                                 NAME_4 = this.ConvertToString(this.GetValue(u, name4)),
                                                                 UNIT_CD = this.ConvertToString(this.GetValue(u, "UNIT_CD")),
                                                                 UNIT_NAME = this.ConvertToString(this.GetValue(u, "UNIT_NAME")),
                                                                 KINGAKU = this.ConvertToDecimal(this.GetValue(u, "KINGAKU")),
                                                                 PAST_KINGAKU = this.ConvertToDecimal(this.GetValue(u, "PAST_KINGAKU")),
                                                                 SAGAKU = this.ConvertToDecimal(this.GetValue(u, "SAGAKU")),
                                                                 ZOKA_RITSU = this.ConvertToDecimal(this.GetValue(u, "ZOKA_RITSU")),
                                                                 GROUP1_KEY = String.Empty,
                                                                 GROUP1_NAME = groupName1,
                                                                 GROUP1_KINGAKU_SUM = 0m,
                                                                 GROUP1_PAST_KINGAKU_SUM = 0m,
                                                                 GROUP1_SAGAKU_SUM = 0m,
                                                                 GROUP1_ZOKA_RITSU_SUM = 0m,
                                                                 GROUP2_KEY = String.Empty,
                                                                 GROUP2_NAME = groupName2,
                                                                 GROUP2_KINGAKU_SUM = 0m,
                                                                 GROUP2_PAST_KINGAKU_SUM = 0m,
                                                                 GROUP2_SAGAKU_SUM = 0m,
                                                                 GROUP2_ZOKA_RITSU_SUM = 0m,
                                                                 GROUP3_KEY = String.Empty,
                                                                 GROUP3_NAME = groupName3,
                                                                 GROUP3_KINGAKU_SUM = 0m,
                                                                 GROUP3_PAST_KINGAKU_SUM = 0m,
                                                                 GROUP3_SAGAKU_SUM = 0m,
                                                                 GROUP3_ZOKA_RITSU_SUM = 0m,
                                                                 GROUP4_KEY = String.Empty,
                                                                 GROUP4_NAME = groupName4,
                                                                 GROUP4_KINGAKU_SUM = 0m,
                                                                 GROUP4_PAST_KINGAKU_SUM = 0m,
                                                                 GROUP4_SAGAKU_SUM = 0m,
                                                                 GROUP4_ZOKA_RITSU_SUM = 0m,
                                                             }).ToList();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CDのカラム名から名称のカラム名を取得します（台貫の場合はカラム名にCDがついていないので別で処理）
        /// </summary>
        /// <param name="cdColumnName">CDカラム名</param>
        /// <returns>名称カラム名</returns>
        private String GetColumnName(String cdColumnName)
        {
            LogUtility.DebugMethodStart(cdColumnName);

            var ret = String.Empty;
            ret = cdColumnName.Replace("_CD", "_NAME");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 帳票出力用DTOリストを集計します
        /// </summary>
        private void CreateSummaryData()
        {
            LogUtility.DebugMethodStart();

            // 集計処理
            this.ShukeiHyoReportDtoList = this.ShukeiHyoReportDtoList.GroupBy(u => new { u.CD_1, u.CD_2, u.CD_3, u.CD_4 })
                                                                     .Select(u => new ShiharaiZennenTaihihyouReportDto()
                                                                     {
                                                                         COLUMN_1 = u.FirstOrDefault().COLUMN_1,
                                                                         COLUMN_2 = u.FirstOrDefault().COLUMN_2,
                                                                         COLUMN_3 = u.FirstOrDefault().COLUMN_3,
                                                                         COLUMN_4 = u.FirstOrDefault().COLUMN_4,
                                                                         CD_1 = u.FirstOrDefault().CD_1,
                                                                         CD_2 = u.FirstOrDefault().CD_2,
                                                                         CD_3 = u.FirstOrDefault().CD_3,
                                                                         CD_4 = u.FirstOrDefault().CD_4,
                                                                         NAME_1 = u.FirstOrDefault().NAME_1,
                                                                         NAME_2 = u.FirstOrDefault().NAME_2,
                                                                         NAME_3 = u.FirstOrDefault().NAME_3,
                                                                         NAME_4 = u.FirstOrDefault().NAME_4,
                                                                         UNIT_CD = u.FirstOrDefault().UNIT_CD,
                                                                         UNIT_NAME = u.FirstOrDefault().UNIT_NAME,
                                                                         KINGAKU = u.Sum(k => k.KINGAKU),
                                                                         PAST_KINGAKU = u.Sum(k => k.PAST_KINGAKU),
                                                                         SAGAKU = u.Sum(k => k.SAGAKU),
                                                                         GROUP1_KEY = String.Empty,
                                                                         GROUP1_NAME = u.FirstOrDefault().GROUP1_NAME,
                                                                         GROUP1_KINGAKU_SUM = 0m,
                                                                         GROUP1_PAST_KINGAKU_SUM = 0m,
                                                                         GROUP1_SAGAKU_SUM = 0m,
                                                                         GROUP1_ZOKA_RITSU_SUM = 0m,
                                                                         GROUP2_KEY = String.Empty,
                                                                         GROUP2_NAME = u.FirstOrDefault().GROUP2_NAME,
                                                                         GROUP2_KINGAKU_SUM = 0m,
                                                                         GROUP2_PAST_KINGAKU_SUM = 0m,
                                                                         GROUP2_SAGAKU_SUM = 0m,
                                                                         GROUP2_ZOKA_RITSU_SUM = 0m,
                                                                         GROUP3_KEY = String.Empty,
                                                                         GROUP3_NAME = u.FirstOrDefault().GROUP3_NAME,
                                                                         GROUP3_KINGAKU_SUM = 0m,
                                                                         GROUP3_PAST_KINGAKU_SUM = 0m,
                                                                         GROUP3_SAGAKU_SUM = 0m,
                                                                         GROUP3_ZOKA_RITSU_SUM = 0m,
                                                                         GROUP4_KEY = String.Empty,
                                                                         GROUP4_NAME = u.FirstOrDefault().GROUP4_NAME,                            
                                                                         GROUP4_KINGAKU_SUM = 0m,
                                                                         GROUP4_PAST_KINGAKU_SUM = 0m,
                                                                         GROUP4_SAGAKU_SUM = 0m,
                                                                         GROUP4_ZOKA_RITSU_SUM = 0m,
                                                                     })
                                                                     .OrderBy(u => u.CD_1)
                                                                     .ThenBy(u => u.CD_2)
                                                                     .ThenBy(u => u.CD_3)
                                                                     .ThenBy(u => u.CD_4)
                                                                     .ToList();

            this.ShukeiHyoReportDtoList.ForEach(u => {
                u.ZOKA_RITSU = u.KINGAKU == 0 ? 0 : u.SAGAKU / u.KINGAKU * 100;
            });

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票出力用DTOリストの小計・合計計算をします
        /// </summary>
        private void CalcTotal()
        {
            var allKingakuSum = 0m;
            var allPastKingakuSum = 0m;
            var allSagakuSum = 0m;
            //var allZotaRitsuSum = 0m;
            var group1key = String.Empty;
            var group1kingakuSum = 0m;
            var group1PastkingakuSum = 0m;
            var group1SagakuSum = 0m;
            //var group1ZotaRitsuSum = 0m;
            var isGroup1keyChange = false;
            var group2key = String.Empty;
            var group2kingakuSum = 0m;
            var group2PastkingakuSum = 0m;
            var group2SagakuSum = 0m;
            //var group2ZotaRitsuSum = 0m;
            var isGroup2keyChange = false;
            var group3key = String.Empty;
            var group3kingakuSum = 0m;
            var group3PastkingakuSum = 0m;
            var group3SagakuSum = 0m;
            //var group3ZotaRitsuSum = 0m;
            var isGroup3keyChange = false;
            var group4key = String.Empty;
            var group4kingakuSum = 0m;
            var group4PastkingakuSum = 0m;
            var group4SagakuSum = 0m;
            //var group4ZotaRitsuSum = 0m;
            foreach (var shukeiHyoReporDto in this.ShukeiHyoReportDtoList)
            {
                allKingakuSum = allKingakuSum + shukeiHyoReporDto.KINGAKU;
                allPastKingakuSum = allPastKingakuSum + shukeiHyoReporDto.PAST_KINGAKU;
                allSagakuSum = allSagakuSum + shukeiHyoReporDto.SAGAKU;
                //allZotaRitsuSum = allZotaRitsuSum + shukeiHyoReporDto.ZOKA_RITSU;

                shukeiHyoReporDto.ALL_KINGAKU_SUM = allKingakuSum;
                shukeiHyoReporDto.ALL_PAST_KINGAKU_SUM = allPastKingakuSum;
                shukeiHyoReporDto.ALL_SAGAKU_SUM = allSagakuSum;
                //shukeiHyoReporDto.ALL_ZOKA_RITSU_SUM = allZotaRitsuSum;

                if (shukeiHyoReporDto.CD_1 != group1key)
                {
                    group1key = shukeiHyoReporDto.CD_1;
                    group1kingakuSum = shukeiHyoReporDto.KINGAKU;
                    group1PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                    group1SagakuSum = shukeiHyoReporDto.SAGAKU;
                    //group1ZotaRitsuSum = shukeiHyoReporDto.ZOKA_RITSU;
                    isGroup1keyChange = true;
                }
                else
                {
                    shukeiHyoReporDto.CD_1 = String.Empty;
                    shukeiHyoReporDto.NAME_1 = String.Empty;

                    group1kingakuSum = group1kingakuSum + shukeiHyoReporDto.KINGAKU;
                    group1PastkingakuSum = group1PastkingakuSum + shukeiHyoReporDto.PAST_KINGAKU;
                    group1SagakuSum = group1SagakuSum + shukeiHyoReporDto.SAGAKU;
                    //group1ZotaRitsuSum = group1ZotaRitsuSum + shukeiHyoReporDto.ZOKA_RITSU;
                    isGroup1keyChange = false;
                }
                shukeiHyoReporDto.GROUP1_KEY = group1key;
                shukeiHyoReporDto.GROUP1_KINGAKU_SUM = group1kingakuSum;
                shukeiHyoReporDto.GROUP1_PAST_KINGAKU_SUM = group1PastkingakuSum;
                shukeiHyoReporDto.GROUP1_SAGAKU_SUM = group1SagakuSum;
                //shukeiHyoReporDto.GROUP1_ZOKA_RITSU_SUM = group1ZotaRitsuSum;

                if (isGroup1keyChange || shukeiHyoReporDto.CD_2 != group2key)
                {
                    group2key = shukeiHyoReporDto.CD_2;
                    group2kingakuSum = shukeiHyoReporDto.KINGAKU;
                    group2PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                    group2SagakuSum = shukeiHyoReporDto.SAGAKU;
                    //group2ZotaRitsuSum = shukeiHyoReporDto.ZOKA_RITSU;
                    isGroup2keyChange = true;
                }
                else
                {
                    shukeiHyoReporDto.CD_2 = String.Empty;
                    shukeiHyoReporDto.NAME_2 = String.Empty;

                    group2kingakuSum = group2kingakuSum + shukeiHyoReporDto.KINGAKU;
                    group2PastkingakuSum = group2PastkingakuSum + shukeiHyoReporDto.PAST_KINGAKU;
                    group2SagakuSum = group2SagakuSum + shukeiHyoReporDto.SAGAKU;
                    //group2ZotaRitsuSum = group2ZotaRitsuSum + shukeiHyoReporDto.ZOKA_RITSU;
                    isGroup2keyChange = false;
                }
                shukeiHyoReporDto.GROUP2_KEY = group2key;
                shukeiHyoReporDto.GROUP2_KINGAKU_SUM = group2kingakuSum;
                shukeiHyoReporDto.GROUP2_PAST_KINGAKU_SUM = group2PastkingakuSum;
                shukeiHyoReporDto.GROUP2_SAGAKU_SUM = group2SagakuSum;
                //shukeiHyoReporDto.GROUP2_ZOKA_RITSU_SUM = group2ZotaRitsuSum;

                if (isGroup1keyChange || isGroup2keyChange || shukeiHyoReporDto.CD_3 != group3key)
                {
                    group3key = shukeiHyoReporDto.CD_3;
                    group3kingakuSum = shukeiHyoReporDto.KINGAKU;
                    group3PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                    group3SagakuSum = shukeiHyoReporDto.SAGAKU;
                    //group3ZotaRitsuSum = shukeiHyoReporDto.ZOKA_RITSU;
                    isGroup3keyChange = true;
                }
                else
                {
                    shukeiHyoReporDto.CD_3 = String.Empty;
                    shukeiHyoReporDto.NAME_3 = String.Empty;

                    group3kingakuSum = group3kingakuSum + shukeiHyoReporDto.KINGAKU;
                    group3PastkingakuSum = group3PastkingakuSum + shukeiHyoReporDto.PAST_KINGAKU;
                    group3SagakuSum = group3SagakuSum + shukeiHyoReporDto.SAGAKU;
                    //group3ZotaRitsuSum = group3ZotaRitsuSum + shukeiHyoReporDto.ZOKA_RITSU;
                    isGroup3keyChange = false;
                }
                shukeiHyoReporDto.GROUP3_KEY = group3key;
                shukeiHyoReporDto.GROUP3_KINGAKU_SUM = group3kingakuSum;
                shukeiHyoReporDto.GROUP3_PAST_KINGAKU_SUM = group3PastkingakuSum;
                shukeiHyoReporDto.GROUP3_SAGAKU_SUM = group3SagakuSum;
                //shukeiHyoReporDto.GROUP3_ZOKA_RITSU_SUM = group3ZotaRitsuSum;

                if (isGroup1keyChange || isGroup2keyChange || isGroup3keyChange || shukeiHyoReporDto.CD_4 != group4key)
                {
                    group4key = shukeiHyoReporDto.CD_4;
                    group4kingakuSum = shukeiHyoReporDto.KINGAKU;
                    group4PastkingakuSum = shukeiHyoReporDto.PAST_KINGAKU;
                    group4SagakuSum = shukeiHyoReporDto.SAGAKU;
                    //group4ZotaRitsuSum = shukeiHyoReporDto.ZOKA_RITSU;
                }
                else
                {
                    shukeiHyoReporDto.CD_4 = String.Empty;
                    shukeiHyoReporDto.NAME_4 = String.Empty;

                    group4kingakuSum = group4kingakuSum + shukeiHyoReporDto.KINGAKU;
                    group4PastkingakuSum = group4PastkingakuSum + shukeiHyoReporDto.PAST_KINGAKU;
                    group4SagakuSum = group4SagakuSum + shukeiHyoReporDto.SAGAKU;
                    //group4ZotaRitsuSum = group4ZotaRitsuSum + shukeiHyoReporDto.ZOKA_RITSU;
                }
                shukeiHyoReporDto.GROUP4_KEY = group4key;
                shukeiHyoReporDto.GROUP4_KINGAKU_SUM = group4kingakuSum;
                shukeiHyoReporDto.GROUP4_PAST_KINGAKU_SUM = group4PastkingakuSum;
                shukeiHyoReporDto.GROUP4_SAGAKU_SUM = group4SagakuSum;
                //shukeiHyoReporDto.GROUP4_ZOKA_RITSU_SUM = group4ZotaRitsuSum;
            }

            this.ShukeiHyoReportDtoList.ForEach(u => { 
                u.GROUP1_ZOKA_RITSU_SUM = u.GROUP1_KINGAKU_SUM == 0 ? 0 : u.GROUP1_SAGAKU_SUM / u.GROUP1_KINGAKU_SUM * 100;
                u.GROUP2_ZOKA_RITSU_SUM = u.GROUP2_KINGAKU_SUM == 0 ? 0 : u.GROUP2_SAGAKU_SUM / u.GROUP2_KINGAKU_SUM * 100;
                u.GROUP3_ZOKA_RITSU_SUM = u.GROUP3_KINGAKU_SUM == 0 ? 0 : u.GROUP3_SAGAKU_SUM / u.GROUP3_KINGAKU_SUM * 100;
                u.GROUP4_ZOKA_RITSU_SUM = u.GROUP4_KINGAKU_SUM == 0 ? 0 : u.GROUP4_SAGAKU_SUM / u.GROUP4_KINGAKU_SUM * 100;
            });
        }

        /// <summary>
        /// 帳票の条件欄を作成します
        /// </summary>
        private void CreateJoukenFieldData()
        {
            this.ShukeiHyoReportDtoList.ForEach(u =>
            {
                u.TITLE = "支払前年対比表（" + this.form.FormDataDto.Pattern.PATTERN_NAME + "）";
                u.KYOTEN = this.form.FormDataDto.KyotenName;
                u.HAKKOU_DATE = this.GetDBDateTime().ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
            });

            // 抽出条件文字列を作成（左側）
            var jouken1 = new StringBuilder();
            jouken1.AppendLine("[抽出条件]");
            jouken1.AppendLine("　[" + this.form.FormDataDto.DateShuruiName + "] " + this.form.FormDataDto.DateFrom.ToString("yyyy/MM/dd") + " ～ " + this.form.FormDataDto.DateTo.ToString("yyyy/MM/dd"));
            jouken1.AppendLine("　[伝票種類] " + this.form.FormDataDto.DenpyouShuruiName);
            jouken1.AppendLine("　[取引区分] " + this.form.FormDataDto.TorihikiKbnName);
            jouken1.AppendLine("　[確定区分] " + this.form.FormDataDto.KakuteiKbnName);
            jouken1.AppendLine("　[締処理状況] " + this.form.FormDataDto.ShimeKbnName);
            jouken1.Append(this.form.FormDataDto.Kyoten);
            jouken1.Append(this.form.FormDataDto.Torihikisaki);
            jouken1.Append(this.form.FormDataDto.Gyousha);
            jouken1.Append(this.form.FormDataDto.Genba);
            jouken1.Append(this.form.FormDataDto.Hinmei);
            jouken1.Append(this.form.FormDataDto.Shurui);
            jouken1.Append(this.form.FormDataDto.Bunrui);
            jouken1.Append(this.form.FormDataDto.NioroshiGyousha);
            jouken1.Append(this.form.FormDataDto.NioroshiGenba);
            jouken1.Append(this.form.FormDataDto.NizumiGyousha);
            jouken1.Append(this.form.FormDataDto.NizumiGenba);
            jouken1.Append(this.form.FormDataDto.EigyouTantousha);
            jouken1.Append(this.form.FormDataDto.NyuuryokuTantousha);
            jouken1.Append(this.form.FormDataDto.UnpanGyousha);
            jouken1.Append(this.form.FormDataDto.Shashu);
            jouken1.Append(this.form.FormDataDto.Sharyou);
            jouken1.Append(this.form.FormDataDto.KeitaiKbn);
            jouken1.Append(this.form.FormDataDto.DaikanKbn);
            jouken1.AppendLine(String.Empty);

            // 抽出条件文字列を作成（右側）
            var jouken2 = new StringBuilder();
            jouken2.AppendLine("[集計項目]");
            jouken2.Append("　[1] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(1) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(1).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(String.Empty);
            jouken2.Append("　[2] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(2) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(2).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(String.Empty);
            jouken2.Append("　[3] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(3) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(3).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(String.Empty);
            jouken2.Append("　[4] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(4) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(4).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(String.Empty);
            jouken2.AppendLine(String.Empty);
            jouken2.AppendLine("[明細項目]");
            jouken2.Append("　[1] ");
            jouken2.Append("今年支払金額");
            jouken2.AppendLine(String.Empty);
            jouken2.Append("　[2] ");
            jouken2.Append("前年支払金額");
            jouken2.AppendLine(String.Empty);
            jouken2.Append("　[3] ");
            jouken2.Append("差額");
            jouken2.AppendLine(String.Empty);
            jouken2.Append("　[4] ");
            jouken2.Append("増加率");
            jouken2.AppendLine(String.Empty);

            this.ShukeiHyoReportDtoList.ForEach(u =>
            {
                u.JOUKEN_1 = jouken1.ToString();
                u.JOUKEN_2 = jouken2.ToString();
            });
        }

        /// <summary>
        /// オブジェクトを文字列に変換します
        /// </summary>
        /// <param name="obj">対象のオブジェクト</param>
        /// <returns>変換した文字列（オブジェクトがnullの場合は空文字列）</returns>
        private string ConvertToString(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            var ret = String.Empty;

            if (obj != null)
            {
                ret = obj.ToString();
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを数値に変換します
        /// </summary>
        /// <param name="obj">対象のオブジェクト</param>
        /// <returns>変換した数値（オブジェクトがnullの場合は0）</returns>
        private int ConvertToInt32(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            var ret = 0;

            Int32.TryParse(obj.ToString(), out ret);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを数値に変換します
        /// </summary>
        /// <param name="obj">対象のオブジェクト</param>
        /// <returns>変換した数値（オブジェクトがnullの場合は0）</returns>
        private decimal ConvertToDecimal(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            var ret = 0m;

            Decimal.TryParse(obj.ToString(), out ret);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトから指定したプロパティの値を取得します
        /// </summary>
        /// <param name="targetObject">対象のオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>取得した値</returns>
        private object GetValue(object targetObject, string propertyName)
        {
            LogUtility.DebugMethodStart(targetObject, propertyName);

            object ret = null;

            if (!String.IsNullOrEmpty(propertyName))
            {
                ret = targetObject.GetType().GetProperty(propertyName).GetValue(targetObject, null);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// リストをDataTableに変換します
        /// </summary>
        /// <typeparam name="T">リストの型</typeparam>
        /// <param name="list">対象のリスト</param>
        /// <returns>変換したDataTable</returns>
        private DataTable ConvertToDataTable<T>(T list) where T : IList
        {
            LogUtility.DebugMethodStart(list);

            var ret = new DataTable(typeof(T).GetGenericArguments()[0].Name);
            typeof(T).GetGenericArguments()[0].GetProperties().ToList().ForEach(p => ret.Columns.Add(p.Name, p.PropertyType));
            foreach (var item in list)
            {
                var row = ret.NewRow();
                item.GetType().GetProperties().ToList().ForEach(p => row[p.Name] = p.GetValue(item, null));
                ret.Rows.Add(row);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 現場
        /// <summary>
        /// 現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場</returns>
        internal M_GENBA GetGenba(String gyoushaCd, String genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 荷積降業者
        /// <summary>
        /// 荷積降業者を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>荷積降現場</returns>
        internal M_GYOUSHA GetNizumioroshiGyousha(String gyoushaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd);

            M_GYOUSHA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            // 区分に関係なく取得する
            ret = dao.GetAllValidData(entity).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 荷積降現場
        /// <summary>
        /// 荷積降現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>荷積降現場</returns>
        internal M_GENBA GetNizumioroshiGenba(String gyoushaCd, String genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // 区分に関係なく取得する
            ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 入力担当者
        /// <summary>
        /// 入力担当者を取得します
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns>入力担当者</returns>
        internal M_SHAIN GetNyuuryokuTantousha(String shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            M_SHAIN ret = null;

            var dao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            ret = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = shainCd, NYUURYOKU_TANTOU_KBN = true, ISNOT_NEED_DELETE_FLG = true }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 車輌
        /// <summary>
        /// 車輌リストを取得します
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns>車輌リスト</returns>
        internal List<M_SHARYOU> GetSharyou(String sharyouCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(sharyouCd);

            var ret = new List<M_SHARYOU>();
            catchErr = true;

            try
            {
                var dao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                var keyEntity = new M_SHARYOU();
                keyEntity.SHARYOU_CD = sharyouCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                ret = dao.GetAllValidData(keyEntity).ToList();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSharyou", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharyou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.DATE_FROM;
            var ToTextBox = this.form.DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region TORIHIKISAKI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// TORIHIKISAKI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.TORIHIKISAKI_CD_FROM;
            var ToTextBox = this.form.TORIHIKISAKI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.TORIHIKISAKI_NAME_TO.Text = this.form.TORIHIKISAKI_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.GYOUSHA_CD_FROM;
            var ToTextBox = this.form.GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.GYOUSHA_NAME_TO.Text = this.form.GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.GENBA_CD_FROM;
            var ToTextBox = this.form.GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.GENBA_NAME_TO.Text = this.form.GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region HINMEI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// HINMEI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HINMEI_CD_FROM;
            var ToTextBox = this.form.HINMEI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.HINMEI_NAME_TO.Text = this.form.HINMEI_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIOROSHI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIOROSHI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIOROSHI_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.NIOROSHI_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIOROSHI_GYOUSHA_NAME_TO.Text = this.form.NIOROSHI_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIOROSHI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIOROSHI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIOROSHI_GENBA_CD_FROM;
            var ToTextBox = this.form.NIOROSHI_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIOROSHI_GENBA_NAME_TO.Text = this.form.NIOROSHI_GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIZUMI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIZUMI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIZUMI_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.NIZUMI_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIZUMI_GYOUSHA_NAME_TO.Text = this.form.NIZUMI_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIZUMI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIZUMI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIZUMI_GENBA_CD_FROM;
            var ToTextBox = this.form.NIZUMI_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIZUMI_GENBA_NAME_TO.Text = this.form.NIZUMI_GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region EIGYOU_TANTOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// EIGYOU_TANTOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.EIGYOU_TANTOUSHA_CD_FROM;
            var ToTextBox = this.form.EIGYOU_TANTOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.EIGYOU_TANTOUSHA_NAME_TO.Text = this.form.EIGYOU_TANTOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NYUURYOKU_TANTOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NYUURYOKU_TANTOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NYUURYOKU_TANTOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NYUURYOKU_TANTOUSHA_CD_FROM;
            var ToTextBox = this.form.NYUURYOKU_TANTOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NYUURYOKU_TANTOUSHA_NAME_TO.Text = this.form.NYUURYOKU_TANTOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region UNPAN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// UNPAN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.UNPAN_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.UNPAN_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.UNPAN_GYOUSHA_NAME_TO.Text = this.form.UNPAN_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHASHU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// SHASHU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHASHU_CD_FROM;
            var ToTextBox = this.form.SHASHU_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHASHU_NAME_TO.Text = this.form.SHASHU_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHARYOU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// SHARYOU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHARYOU_CD_FROM;
            var ToTextBox = this.form.SHARYOU_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHARYOU_NAME_TO.Text = this.form.SHARYOU_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region KEITAI_KBN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// KEITAI_KBN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEITAI_KBN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.KEITAI_KBN_CD_FROM;
            var ToTextBox = this.form.KEITAI_KBN_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            this.form.KEITAI_KBN_NAME_TO.Text = this.form.KEITAI_KBN_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region DAIKAN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// DAIKAN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAIKAN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.DAIKAN_CD_FROM;
            var ToTextBox = this.form.DAIKAN_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            this.form.DAIKAN_NAME_TO.Text = this.form.DAIKAN_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHURUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// SHURUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHURUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHURUI_CD_FROM;
            var ToTextBox = this.form.SHURUI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHURUI_NAME_TO.Text = this.form.SHURUI_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region BUNRUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// SHURUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUNRUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.BUNRUI_CD_FROM;
            var ToTextBox = this.form.BUNRUI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.BUNRUI_NAME_TO.Text = this.form.BUNRUI_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region DBサーバ日付
        /// <summary>
        /// DBサーバ日付を取得する
        /// </summary>
        /// <returns></returns>
        private DateTime GetDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        #endregion
    }
}
