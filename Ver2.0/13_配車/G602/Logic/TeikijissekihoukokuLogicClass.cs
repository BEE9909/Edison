﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;
using System.Collections;
using CommonChouhyouPopup.App;
using Shougun.Core.Common.BusinessCommon.Xml;
using r_framework.Dto;

namespace Shougun.Core.Allocation.Teikijissekihoukoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class TeikijissekihoukokuLogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// UIForm
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// DTO
        /// </summary>
        private TeikijissekihoukokuDTOClass dto;

        /// <summary>
        /// 定期配車実績表のDao
        /// </summary>
        private TeikijissekihoukokuDAOClass dao;

        /// <summary>
        /// 会社名前のDao
        /// </summary>
        private IM_CORP_INFODao daoCorp;

        /// <summary>
        /// 検索条件
        /// </summary>
        public TeikijissekihoukokuDTOClass SearchString { get; set; }

        /// <summary>
        /// 月報詳細内容検索結果
        /// </summary>
        public DataTable SearchDetailResult;

        /// <summary>
        /// 年報詳細内容検索結果
        /// </summary>
        public DataTable SearchDetailResult_Y;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public BusinessBaseForm parentForm;

        //出力区分変量を設定

        private string syutsuRyouku_KBN;

        // 業者CD、業者名、現場CD、場名のタイトルを設定のため
        private static readonly string SearchConditionHeader = "業者CD,業者名,現場CD,現場名";

        // 廃棄物種類のタイトルを設定のため
        private static readonly string DetailHeader = ",廃棄物種類";

        // 日付のタイトルを設定のため
        private static readonly string DetailDateAndUnit = "日付,単位";

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.Teikijissekihoukoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        private string sysFormat = "#,###";
        #endregion

        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TeikijissekihoukokuLogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                // dto initial
                this.dto = new TeikijissekihoukokuDTOClass();

                // dao initial
                this.dao = DaoInitUtility.GetComponent<TeikijissekihoukokuDAOClass>();

                // 会社名dao initial
                this.daoCorp = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                msgLogic = new MessageBoxShowLogic();

                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion Constructor

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // 画面初期表示設定
                this.InitializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
                // サブファンクション非表示
                this.parentForm.ProcessButtonPanel.Visible = false;
                this.allControl = this.form.allControl;

                //拠点CD
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                string KYOTEN_CD = this.GetUserProfileValue(userProfile, "拠点CD");
                IM_KYOTENDao kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                var kyotenP = kyotenDao.GetDataByCd(KYOTEN_CD);
                //拠点名称
                if (kyotenP != null && KYOTEN_CD != string.Empty)
                {
                    this.form.txt_KyotenCD.Text = KYOTEN_CD.PadLeft(this.form.txt_KyotenCD.MaxLength, '0'); ;
                    this.form.txt_KyotenName.Text = kyotenP.KYOTEN_NAME_RYAKU;
                }
                else
                {
                    //拠点CD、拠点 : ブランク
                    this.form.txt_KyotenCD.Text = string.Empty;
                    this.form.txt_KyotenName.Text = string.Empty;
                }

                this.form.SHIMEBI.Text = String.Empty;

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

        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void InitializeScreen()
        {
            //「出力区分」／月報を選択する
            this.form.txt_Shuturyokukubun.Text = "1";

            //「期間From」／システム日付
            this.form.dtp_KikanFrom.Value = DateTime.Now;

            //「期間To」／作業開始日
            this.form.dtp_KikanTo.Value = DateTime.Now;

            this.form.TORIHIKISAKI_CD_From.Text = String.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU_From.Text = String.Empty;
            this.form.TORIHIKISAKI_CD_To.Text = String.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU_To.Text = String.Empty;

            //「業者CD1 From」／空白にする
            this.form.GYOUSHA_CD_From.Text = "";

            //「業者名1 From」／空白にする
            this.form.GYOUSHA_NAME_RYAKU_From.Text = "";

            //「業者CD2 From」／空白にする
            this.form.GYOUSHA_CD_To.Text = "";

            //「業者名2 From」／空白にする
            this.form.GYOUSHA_NAME_RYAKU_To.Text = "";

            //「現場CD1 From」／空白にする
            this.form.GENBA_CD_From.Text = "";

            //「現場名2 From」／空白にする
            this.form.GENBA_NAME_RYAKU_From.Text = "";

            //「現場CD1 From」／空白にする
            this.form.GENBA_CD_To.Text = "";

            //「現場名2 From」／空白にする
            this.form.GYOUSHA_NAME_RYAKU_To.Text = "";

            this.form.SHURUI_CD_From.Text = String.Empty;
            this.form.SHURUI_NAME_RYAKU_From.Text = String.Empty;
            this.form.SHURUI_CD_To.Text = String.Empty;
            this.form.SHURUI_NAME_RYAKU_To.Text = String.Empty;

            //「集計対象数量」／実績数量を選択する
            this.form.txt_Shuukeisuuryou.Text = "1";
        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタンイベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {

            //2014/01/15 削除 qiao start
            //// 「F5印刷ボタン」初期状態では非アクティブとする
            //parentForm.bt_func5.Enabled = false;
            //// 「F6CSV出力ボタン」初期状態では非アクティブとする
            //parentForm.bt_func6.Enabled = false;
            //2014/01/15 削除 qiao end

            //2014/01/15 修正 qiao start
            // 「Ｆ5 帳票印刷ボタン」イベントのイベント生成
            this.form.C_Regist(parentForm.bt_func5);
            parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);

            // 「Ｆ6 CSV出力ボタン」イベントのイベント生成
            this.form.C_Regist(parentForm.bt_func6);
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);

            // 「Ｆ9 実行ボタン」イベントのイベント生成
            //this.form.C_Regist(parentForm.bt_func9);
            //parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
            //2014/01/15 修正 qiao end

            // 「Ｆ12 ﾃﾞｰﾀ出力ボタン」イベントのイベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        #endregion

        /// <summary>
        /// 会社名を取得
        /// </summary>
        /// <returns></returns>
        private string GetCorpName()
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart();

                M_CORP_INFO condition = new M_CORP_INFO();
                condition.SYS_ID = 0;
                //会社名を検索結果を取得
                var entity = this.daoCorp.GetAllValidData(condition);
                if (entity != null && entity.Length > 0)
                {
                    returnVal = entity[0].CORP_NAME;
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        #region 実行処理
        /// <summary>
        /// 実行処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int result = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件を設定する
                SetSearchString();

                string csvKb = this.form.txt_Shuturyokukubun.Text.ToString();
                if (csvKb.Equals("1"))
                {
                    syutsuRyouku_KBN = "1";
                    // 月報の明細検索結果取得
                    this.SearchDetailResult = this.dao.GetReportDetailDataByMonth(this.SearchString);
                    //月報の 数量合計結果取得
                    //this.SearchResult = this.dao.GetReportDataByMonth(this.SearchString);
                    // 検索結果件数
                    result = this.SearchDetailResult.Rows.Count;
                }
                if (csvKb.Equals("2"))
                {

                    syutsuRyouku_KBN = "2";
                    // 年報の明細検索結果取得
                    this.SearchDetailResult_Y = this.dao.GetReportDetailDataByYear(this.SearchString);

                    result = this.SearchDetailResult_Y.Rows.Count;
                }

                return result;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
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

        #region 月報CSV出力
        /// <summary>
        /// 月報CSV出力
        /// </summary>
        void monthCsvOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var fileName = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(月報)_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                    {
                        // 年月明細
                        var resultByYMGG = SearchDetailResult.AsEnumerable()
                            .GroupBy(
                                r => r.Field<DateTime>("SAGYOU_DATE").ToString().Substring(0, 7),
                                (ym, ymGroup) => new
                                {
                                    ym,
                                    ymggGroups = ymGroup.GroupBy(
                                        r2 => string.Format("{0},{1},{2},{3}",
                                            r2.Field<string>("GYOUSHA_CD"),
                                            r2.Field<string>("GYOUSHA_NAME"),
                                            r2.Field<string>("GENBA_CD"),
                                            r2.Field<string>("GENBA_NAME")),
                                        (gg, ymggGroup) => new
                                        {
                                            gg,
                                            ymggGroup
                                        }
                                    ).ToList()
                                }
                            ).ToList();

                        // 月より、ループ
                        foreach (var grpYM in resultByYMGG)
                        {
                            foreach (var grpYMGG in grpYM.ymggGroups)
                            {
                                var grp = grpYMGG.ymggGroup;

                                // 固定項目名前を出力する
                                sw.WriteLine(SearchConditionHeader);
                                // 業者CD、業者名、現場CD、現場名を書き込み
                                sw.WriteLine(grpYMGG.gg);

                                sw.WriteLine();

                                // タイトル（廃棄物種類）
                                //sw.WriteLine(DetailHeader);
                                // タイトル（日付、単位）
                                //sw.WriteLine(DetailDateAndUnit);

                                //品名と単位名を取るのために、キーを設定
                                var hinmeis = grp.GroupBy(r => new
                                    {
                                        HINMEI_CD = r.Field<string>("HINMEI_CD"),
                                        UNIT_CD = r.Field<short>("UNIT_CD"),
                                    },
                                    (k, g) => new
                                    {
                                        HINMEI_CD = k.HINMEI_CD,
                                        HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                                        UNIT_CD = k.UNIT_CD,
                                        //UNIT_NAME = g.First().Field<string>("UNIT_NAME"),
                                        UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                                        GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                                        GENBA_CD = g.First().Field<string>("GENBA_CD"),
                                    }).OrderBy(r => r.UNIT_NAME)
                                    .ThenBy(r => r.UNIT_CD)
                                    .ToList();

                                // カラムヘッダを書き込む
                                // 品名
                                var strHinmeiName = string.Join(",", hinmeis.Select(r => r.HINMEI_NAME));
                                // 単位
                                var strUnitName = string.Join(",", hinmeis.Select(r => r.UNIT_NAME));
                                //項目、品名を書き込む
                                sw.WriteLine(DetailHeader + "," + strHinmeiName);
                                //項目、単位を書き込む
                                sw.WriteLine(DetailDateAndUnit + "," + strUnitName);

                                var ym = grpYM.ym;
                                var days = getDaysOfMonth(ym);

                                //毎日を取るのために、メソッドを設定する
                                var dates = Enumerable.Range(1, days)
                                    .Select(n => DateTime.Parse(string.Format("{0}/{1:D2}", ym, n)))
                                    .Select(d => new { DATE = d, DATE_DAY = d.ToString("yyyy/MM/dd(ddd)") }).ToList();

                                //日付、品名CD、業者CD、現場CDでをキーを設定する
                                //毎日廃棄物数量データある、取るのデータを書き込む。データない場合、0を書き込む
                                var result =
                                    dates.Select(d =>
                                        new
                                        {
                                            d.DATE,
                                            d.DATE_DAY,
                                            VALUES = hinmeis.Select(h =>
                                                grp.Where(
                                                    r => r.Field<DateTime>("SAGYOU_DATE") == d.DATE &&
                                                    r.Field<string>("HINMEI_CD").Equals(h.HINMEI_CD) &&
                                                    r.Field<short>("UNIT_CD").Equals(h.UNIT_CD) &&
                                                    r.Field<string>("GYOUSHA_CD").Equals(h.GYOUSHA_CD) &&
                                                    r.Field<string>("GENBA_CD").Equals(h.GENBA_CD)
                                                ).Sum(r3 => r3.Field<double>("Expr1"))
                                            )
                                        }
                                    ).Select(r =>
                                        new
                                        {
                                            r.DATE,
                                            r.DATE_DAY,
                                            r.VALUES,
                                            //VALUES_STR = r.VALUES.Select(r2 => string.Format("{0:F2}", r2)).ToList(),
                                            VALUES_STR = r.VALUES.Select(r2 => r2.ToString(sysFormat)).ToList(),
                                        }
                                    ).ToList();
                                //一ヶ月の廃棄物数量を合計する
                                var sums = result.Select(r => r.VALUES)
                                    .Aggregate((acc, r) => acc.Zip(r, (v1, v2) => v1 + v2))
                                    //.Select(r => string.Format("{0:F2}", r)).ToList();
                                   .Select(r => r.ToString(sysFormat)).ToList();

                                // 毎月で毎日の廃棄物数量を書き込む
                                foreach (var d in result)
                                {
                                    for(int i = 0; i < d.VALUES_STR.Count; i++)
                                    {
                                        // カンマ区切りがある数量値の場合、
                                        // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                        d.VALUES_STR[i] = "\"" + d.VALUES_STR[i] + "\"";
                                    }

                                    var strQulity = d.DATE_DAY + "," + "," + string.Join(",", d.VALUES_STR);
                                    sw.WriteLine(strQulity);
                                }
                                sw.WriteLine();

                                for(int i = 0; i < sums.Count; i++)
                                {
                                    // カンマ区切りがある数量値の場合、
                                    // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                    sums[i] = "\"" + sums[i] + "\"";
                                }

                                // 一ヶ月の廃棄物数量合計を書き込む
                                sw.WriteLine("合計," + "," + string.Join(",", sums));

                                sw.WriteLine();
                            }
                            sw.WriteLine();
                        }
                        msgLogic.MessageBoxShow("I000", "CSV出力");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("monthCsvOutput", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 年報CSV出力
        /// <summary>
        /// 年報CSV出力
        /// </summary>

        void yearCSVOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var fileName = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(年報)_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                    {
                        // 年月明細
                        var resultByGG = SearchDetailResult_Y.AsEnumerable()
                            .GroupBy(
                                r => string.Format("{0},{1},{2},{3}",
                                            r.Field<string>("GYOUSHA_CD"),
                                            r.Field<string>("GYOUSHA_NAME"),
                                            r.Field<string>("GENBA_CD"),
                                            r.Field<string>("GENBA_NAME"))).ToList();

                        //業者、現場より、ループ
                        foreach (var grp in resultByGG)
                        {
                            // ヘーダ固定項目名前を出力する
                            sw.WriteLine(SearchConditionHeader);
                            // 業者CD、業者名、現場CD、現場名を書き込み
                            sw.WriteLine(grp.Key);

                            sw.WriteLine();

                            //品名と単位名を取るのために、キーを設定
                            var hinmeis = grp.GroupBy(r => new
                                {
                                    HINMEI_CD = r.Field<string>("HINMEI_CD"),
                                    UNIT_CD = r.Field<short>("UNIT_CD"),
                                },
                                (k, g) => new
                                {
                                    HINMEI_CD = k.HINMEI_CD,
                                    HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                                    UNIT_CD = k.UNIT_CD,
                                    UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                                    GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                                    GENBA_CD = g.First().Field<string>("GENBA_CD"),
                                })
                                //.OrderBy(r => r.UNIT_CD)
                                //.ThenBy(r => r.UNIT_CD)
                                .ToList();

                            // カラムヘッダを書き込む
                            // 品名
                            var strHinmeiName = string.Join(",", hinmeis.Select(r => r.HINMEI_NAME));
                            // 単位
                            var strUnitName = string.Join(",", hinmeis.Select(r => r.UNIT_NAME));
                            //項目名、品名を書き込む
                            sw.WriteLine(DetailHeader + "," + strHinmeiName);
                            //項目名、単位を書き込む
                            sw.WriteLine(DetailDateAndUnit + "," + strUnitName);

                            //recordを取るのために、メソッドを設定する
                            var dates = grp.Select(r => r.Field<DateTime>("SAGYOU_DATE").ToString().Substring(0, 7)).Distinct().ToList();

                            //日付、品名CD、単位CD、業者CD、現場CDで条件を設定、数量を取得
                            //毎月廃棄物数量データある、取るのデータを書き込む。データない場合、0を書き込む
                            var result =
                                dates.Select(d =>
                                    new
                                    {
                                        DATE_MONTH = d,
                                        VALUES = hinmeis.Select(h =>
                                            grp.Where(
                                                r => r.Field<DateTime>("SAGYOU_DATE").ToString().Substring(0, 7) == d &&
                                                r.Field<string>("HINMEI_CD").Equals(h.HINMEI_CD) &&
                                                r.Field<short>("UNIT_CD").Equals(h.UNIT_CD) &&
                                                r.Field<string>("GYOUSHA_CD").Equals(h.GYOUSHA_CD) &&
                                                r.Field<string>("GENBA_CD").Equals(h.GENBA_CD)
                                            ).Sum(r3 => r3.Field<double>("Expr1"))
                                        )
                                    }
                                ).Select(r =>
                                    new
                                    {
                                        r.DATE_MONTH,
                                        r.VALUES,
                                        //VALUES_STR = r.VALUES.Select(r2 => string.Format("{0:F2}", r2)).ToList(),
                                        VALUES_STR = r.VALUES.Select(r2 => r2.ToString(sysFormat)).ToList(),
                                    }
                                ).ToList();
                            //一年内の廃棄物数量を合計する
                            var sums = result.Select(r => r.VALUES)
                                .Aggregate((acc, r) => acc.Zip(r, (v1, v2) => v1 + v2))
                                //.Select(r => string.Format("{0:F2}", r)).ToList();
                                .Select(r => r.ToString(sysFormat)).ToList();

                            // 毎月の廃棄物数量を書き込む
                            foreach (var d in result)
                            {
                                for(int i = 0; i < d.VALUES_STR.Count; i++)
                                {
                                    // カンマ区切りがある数量値の場合、
                                    // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                    d.VALUES_STR[i] = "\"" + d.VALUES_STR[i] + "\"";
                                }

                                var strQulity = d.DATE_MONTH + "," + "," + string.Join(",", d.VALUES_STR);
                                sw.WriteLine(strQulity);
                            }
                            sw.WriteLine();

                            for(int i = 0; i < sums.Count; i++)
                            {
                                // カンマ区切りがある数量値の場合、
                                // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                sums[i] = "\"" + sums[i] + "\"";
                            }

                            // 一年の廃棄物数量合計を書き込む
                            sw.WriteLine("合計," + "," + string.Join(",", sums));

                            sw.WriteLine();
                        }
                        sw.WriteLine();
                    }
                    msgLogic.MessageBoxShow("I000", "CSV出力");
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("yearCSVOutput", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region　帳票出力（月報）


        public void invoicePrint(ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart();

                #region- データ取得処理 -

                // 会社名
                string corpName = this.GetCorpName();

                DataRow rowTmp;
                string ctrlName = string.Empty;
                //年月ソート順でデータを取得
                var resultByYMGG = SearchDetailResult.AsEnumerable()
                            .GroupBy(
                                r => r.Field<DateTime>("SAGYOU_DATE").ToString("yyyy/MM/dd").Substring(0, 7),
                                (ym, ymGroup) => new
                                {
                                    ym,
                                    ymggGroups = ymGroup.GroupBy(
                                        r2 => string.Format("{0},{1},{2},{3}",
                                            r2.Field<string>("GYOUSHA_CD"),
                                            r2.Field<string>("GYOUSHA_NAME"),
                                            r2.Field<string>("GENBA_CD"),
                                            r2.Field<string>("GENBA_NAME")),
                                        (gg, ymggGroup) => new
                                        {
                                            gg,
                                            ymggGroup
                                        }
                                    ).ToList()
                                }
                            ).ToList();

                Dictionary<string, Dictionary<string, DataTable>> DataTablePageList = new Dictionary<string, Dictionary<string, DataTable>>();


                // 年月より、ループ
                foreach (var grpYM in resultByYMGG)
                {//現場より、ループ
                    foreach (var grpYMGG in grpYM.ymggGroups)
                    {
                        var grp = grpYMGG.ymggGroup;

                        //業者CD、業者名、現場CD、現場名を取得
                        string[] arrTmp = grpYMGG.gg.ToString().Split(',');
                        string gyoushaCD = arrTmp[0];
                        string gyoushaName = arrTmp[1];
                        string genbaCD = arrTmp[2];
                        string genbaName = arrTmp[3];

                        //「キー」の定義
                        string key = string.Format("{0}_{1}_{2}_{3}", grpYM.ym.Substring(0, 4), grpYM.ym.Substring(5, 2), gyoushaCD, genbaCD);

                        DataTablePageList[key] = new Dictionary<string, DataTable>();

                        #region - Header -

                        DataTable dtHeader = new DataTable();
                        dtHeader.TableName = "Header";

                        // 会社名
                        dtHeader.Columns.Add("CORP_RYAKU_NAME");
                        // 業者CD
                        dtHeader.Columns.Add("GYOUSHA_CD");
                        // 業者名
                        dtHeader.Columns.Add("GYOUSYA_NAME");
                        // 現場CD
                        dtHeader.Columns.Add("GENBA_CD");
                        // 現場名
                        dtHeader.Columns.Add("GENBA_NAME");


                        //品名と単位名を取るのため
                        var hinmeis = grp.GroupBy(r => new
                        {
                            HINMEI_CD = r.Field<string>("HINMEI_CD"),
                            UNIT_CD = r.Field<short>("UNIT_CD"),
                        },
                            (k, g) => new
                            {
                                HINMEI_CD = k.HINMEI_CD,
                                HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                                UNIT_CD = k.UNIT_CD,
                                UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                                GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                                GENBA_CD = g.First().Field<string>("GENBA_CD"),
                            }).OrderBy(r => r.HINMEI_CD)
                            .ThenBy(r => r.UNIT_CD)
                            .ToList();

                        //品名と単位のカラムを取得

                        for (int i = 0; i < hinmeis.Count; i++)
                        {
                            // 品名
                            ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                            dtHeader.Columns.Add(ctrlName);
                            // 単位
                            ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                            dtHeader.Columns.Add(ctrlName);
                        }

                        rowTmp = dtHeader.NewRow();

                        // 会社名8 
                        //rowTmp["CORP_RYAKU_NAME"] ="";
                        rowTmp["CORP_RYAKU_NAME"] = corpName;

                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = gyoushaCD;
                        // 業者名
                        rowTmp["GYOUSYA_NAME"] = gyoushaName;
                        // 現場CD
                        rowTmp["GENBA_CD"] = genbaCD;
                        // 現場名
                        rowTmp["GENBA_NAME"] = genbaName;

                        //品名と単位の値を取得
                        for (int i = 0; i < hinmeis.Count; i++)
                        {
                            // 品名
                            ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                            rowTmp[ctrlName] = hinmeis[i].HINMEI_NAME;
                            // 単位
                            ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                            rowTmp[ctrlName] = hinmeis[i].UNIT_NAME;
                        }

                        dtHeader.Rows.Add(rowTmp);

                        DataTablePageList[key]["Header"] = dtHeader;

                        #endregion - Header -

                        #region - Detail -

                        DataTable dtDetail = new DataTable();
                        dtDetail.TableName = "Detail";
                        //年月日を取得
                        var ym = grpYM.ym;
                        var days = getDaysOfMonth(ym);
                        //毎日を取るのために、メソッドを設定する
                        var dates = Enumerable.Range(1, days)
                            .Select(n => DateTime.Parse(string.Format("{0}/{1:D2}", ym, n)))
                            .Select(d => new { DATE = d, DATE_DAY = d.ToString("MM/dd(ddd)") }).ToList();

                        // 日付カラムセート
                        dtDetail.Columns.Add("DATE");

                        // 数量カラム作成
                        for (int j = 0; j < hinmeis.Count; j++)
                        {
                            ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                            dtDetail.Columns.Add(ctrlName);
                        }

                        // データ作成
                        foreach (var day in dates)
                        {
                            var newRow = dtDetail.NewRow();
                            newRow["DATE"] = day.DATE_DAY;

                            for (int i = 0; i < hinmeis.Count; i++)
                            {
                                var jisseki = grp.Where(r => r.Field<DateTime>("SAGYOU_DATE") == day.DATE
                                                          && r.Field<String>("GYOUSHA_CD") == gyoushaCD
                                                          && r.Field<String>("GENBA_CD") == genbaCD
                                                          && r.Field<String>("HINMEI_CD") == hinmeis[i].HINMEI_CD
                                                          && r.Field<Int16>("UNIT_CD") == hinmeis[i].UNIT_CD);
                                if (jisseki.Count() > 0)
                                {
                                    var suuryou = jisseki.Sum(r => r.Field<Double>("Expr1"));
                                    ctrlName = String.Format("HINMEI_SURYO_{0}", i + 1);
                                    newRow[ctrlName] = suuryou.ToString(sysFormat);
                                }
                            }

                            dtDetail.Rows.Add(newRow);
                        }

                        DataTablePageList[key]["Detail"] = dtDetail;

                        #endregion - Detail -

                        #region - Footer -

                        DataTable dtFooter = new DataTable();
                        dtFooter.TableName = "Footer";

                        for (int j = 0; j < hinmeis.Count; j++)
                        {
                            // 合計カラム作成
                            ctrlName = string.Format("GOUGEI_{0}", j + 1);
                            dtFooter.Columns.Add(ctrlName);
                        }

                        var sumRow = dtFooter.NewRow();
                        for (int j = 0; j < hinmeis.Count; j++)
                        {
                            // 合計計算
                            var sumCtrlName = string.Format("GOUGEI_{0}", j + 1);
                            ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                            var sum = dtDetail.AsEnumerable().Where(r => !String.IsNullOrEmpty(r.Field<String>(ctrlName)))
                                                             .Sum(r => Decimal.Parse(r.Field<String>(ctrlName)));
                            sumRow[sumCtrlName] = sum.ToString(sysFormat);
                        }

                        dtFooter.Rows.Add(sumRow);

                        DataTablePageList[key]["Footer"] = dtFooter;

                        #endregion - Footer -
                    }
                }

                //ReportInfoR429 reportInfo = new ReportInfoR429(WINDOW_ID.T_TEIKIHAISHA_ZISSEKI_HYOU_GEPPOU);
                reportInfo.DataTablePageList = DataTablePageList;

                #endregion- データ取得処理 -


            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        #endregion

        #region　帳票出力（年報）


        public void yearInvoicePrint(ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart();

                #region- データ取得処理 -
                // 会社名
                string corpName = this.GetCorpName();

                DataRow rowTmp;
                string ctrlName = string.Empty;
                //年月ソート順でデータを取得

                // 年月明細
                var resultByGG = SearchDetailResult_Y.AsEnumerable()
                    .GroupBy(
                        r => string.Format("{0},{1},{2},{3}",
                                    r.Field<string>("GYOUSHA_CD"),
                                    r.Field<string>("GYOUSHA_NAME"),
                                    r.Field<string>("GENBA_CD"),
                                    r.Field<string>("GENBA_NAME"))).ToList();


                Dictionary<string, Dictionary<string, DataTable>> DataTablePageList = new Dictionary<string, Dictionary<string, DataTable>>();

                foreach (var grp in resultByGG)
                {
                    //業者CD、業者名、現場CD、現場名を取得
                    string[] arrTmp = grp.Key.ToString().Split(',');
                    string gyoushaCD = arrTmp[0];
                    string gyoushaName = arrTmp[1];
                    string genbaCD = arrTmp[2];
                    string genbaName = arrTmp[3];
                    //「キー」の定義
                    string dtFrom = this.form.dtp_KikanFrom.Text.Substring(0, 7);
                    string dtTo = this.form.dtp_KikanTo.Text.Substring(0, 7);
                    string ki = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", dtFrom.Substring(0, 4), dtFrom.Substring(5, 2), dtTo.Substring(0, 4), dtTo.Substring(5, 2), gyoushaCD, genbaCD);

                    DataTablePageList[ki] = new Dictionary<string, DataTable>();
                    #region - Header -

                    DataTable dtHeader = new DataTable();
                    dtHeader.TableName = "Header";

                    // 会社名
                    dtHeader.Columns.Add("CORP_RYAKU_NAME");
                    // 業者CD
                    dtHeader.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dtHeader.Columns.Add("GYOUSYA_NAME");
                    // 現場CD
                    dtHeader.Columns.Add("GENBA_CD");
                    // 現場名
                    dtHeader.Columns.Add("GENBA_NAME");


                    //品名と単位名を取るのために、キーを設定
                    var hinmeis = grp.GroupBy(r => new
                        {
                            HINMEI_CD = r.Field<string>("HINMEI_CD"),
                            UNIT_CD = r.Field<short>("UNIT_CD"),
                        },
                        (k, g) => new
                        {
                            HINMEI_CD = k.HINMEI_CD,
                            HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                            UNIT_CD = k.UNIT_CD,
                            UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                            GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                            GENBA_CD = g.First().Field<string>("GENBA_CD"),
                        }).OrderBy(r => r.HINMEI_CD)
                        .ThenBy(r => r.UNIT_CD)
                        .ToList();

                    //品名と単位のカラムを取得

                    for (int i = 0; i < hinmeis.Count; i++)
                    {
                        // 品名
                        ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                        dtHeader.Columns.Add(ctrlName);
                        // 単位
                        ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                        dtHeader.Columns.Add(ctrlName);
                    }

                    rowTmp = dtHeader.NewRow();

                    // 会社名
                    rowTmp["CORP_RYAKU_NAME"] = corpName;

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = gyoushaCD;
                    // 業者名
                    rowTmp["GYOUSYA_NAME"] = gyoushaName;
                    // 現場CD
                    rowTmp["GENBA_CD"] = genbaCD;
                    // 現場名
                    rowTmp["GENBA_NAME"] = genbaName;

                    //品名と単位の値を取得
                    for (int i = 0; i < hinmeis.Count; i++)
                    {
                        // 品名
                        ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                        rowTmp[ctrlName] = hinmeis[i].HINMEI_NAME;
                        // 単位
                        ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                        rowTmp[ctrlName] = hinmeis[i].UNIT_NAME;
                    }

                    dtHeader.Rows.Add(rowTmp);

                    DataTablePageList[ki]["Header"] = dtHeader;

                    #endregion - Header -

                    #region - Detail -

                    DataTable dtDetail = new DataTable();
                    dtDetail.TableName = "Detail";

                    //2014/01/14 修正 qiao start
                    //var ymFrom = (DateTime)this.form.dtp_KikanFrom.Value;
                    //var ymTo = (DateTime)this.form.dtp_KikanTo.Value;
                    var ymFrom = DateTime.Parse(this.form.dtp_KikanFrom.Text);
                    var ymTo = DateTime.Parse(this.form.dtp_KikanTo.Text);
                    //2014/01/14 修正 qiao end

                    //recordを取るのために、メソッドを設定する
                    //var dates = grp.Select(r => r.Field<DateTime>("DENPYOU_DATE").ToString().Substring(0, 7)).Distinct().ToList();
                    int n1 = ymFrom.Year * 12 + ymFrom.Month - 1, n2 = ymTo.Year * 12 + ymTo.Month - 1;
                    var dates = Enumerable.Range(n1, n2 - n1 + 1)
                        .Select(n => new DateTime(n / 12, n % 12 + 1, 1))
                        .Select(d => new { DATE = d, DATE_DAY = d.ToString("yyyy/MM") }).ToList();

                    // 日付カラムセート
                    dtDetail.Columns.Add("DATE");

                    // 数量カラム作成
                    for (int j = 0; j < hinmeis.Count; j++)
                    {
                        ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                        dtDetail.Columns.Add(ctrlName);
                    }

                    // データ作成
                    foreach (var day in dates)
                    {
                        var newRow = dtDetail.NewRow();
                        newRow["DATE"] = day.DATE_DAY;

                        for (int i = 0; i < hinmeis.Count; i++)
                        {
                            var jisseki = grp.Where(r => r.Field<DateTime>("SAGYOU_DATE").ToString("yyyy/MM") == day.DATE_DAY
                                                      && r.Field<String>("GYOUSHA_CD") == gyoushaCD
                                                      && r.Field<String>("GENBA_CD") == genbaCD
                                                      && r.Field<String>("HINMEI_CD") == hinmeis[i].HINMEI_CD
                                                      && r.Field<Int16>("UNIT_CD") == hinmeis[i].UNIT_CD);
                            if (jisseki.Count() > 0)
                            {
                                var suuryou = jisseki.Sum(r => r.Field<Double>("Expr1"));
                                ctrlName = String.Format("HINMEI_SURYO_{0}", i + 1);
                                newRow[ctrlName] = suuryou.ToString(sysFormat);
                            }
                        }

                        dtDetail.Rows.Add(newRow);
                    }

                    DataTablePageList[ki]["Detail"] = dtDetail;

                    #endregion - Detail -

                    #region - Footer -

                    DataTable dtFooter = new DataTable();
                    dtFooter.TableName = "Footer";

                    for (int j = 0; j < hinmeis.Count; j++)
                    {
                        // 合計カラム作成
                        ctrlName = string.Format("GOUGEI_{0}", j + 1);
                        dtFooter.Columns.Add(ctrlName);
                    }

                    var sumRow = dtFooter.NewRow();
                    for (int j = 0; j < hinmeis.Count; j++)
                    {
                        // 合計計算
                        var sumCtrlName = string.Format("GOUGEI_{0}", j + 1);
                        ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                        var sum = dtDetail.AsEnumerable().Where(r => !String.IsNullOrEmpty(r.Field<String>(ctrlName)))
                                                         .Sum(r => Decimal.Parse(r.Field<String>(ctrlName)));
                        sumRow[sumCtrlName] = sum.ToString(sysFormat);
                    }

                    dtFooter.Rows.Add(sumRow);

                    DataTablePageList[ki]["Footer"] = dtFooter;

                    #endregion - Footer -

                }

                reportInfo.DataTablePageList = DataTablePageList;

                #endregion- データ取得処理 -


            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        #endregion 帳票出力（年報）

        #region Days取得処理
        /// <summary>
        /// Days取得処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int getDaysOfMonth(string month)
        {
            try
            {
                LogUtility.DebugMethodStart();
                int days = 0;
                string[] monthItem = month.Split('/');

                days = DateTime.DaysInMonth(int.Parse(monthItem[0]), int.Parse(monthItem[1]));

                return days;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
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
        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();

                TeikijissekihoukokuDTOClass searchCondition = new TeikijissekihoukokuDTOClass();

                // 拠点
                if (!string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
                {
                    if (this.form.txt_KyotenCD.Text != "99")
                    {
                        searchCondition.KyotenCD = this.form.txt_KyotenCD.Text;
                    }
                }

                // 部門
                if (!string.IsNullOrEmpty(this.form.txt_BumonCD.Text))
                {
                    searchCondition.BumonCD = this.form.txt_BumonCD.Text;
                }

                // 出力区分
                if (!string.IsNullOrEmpty(this.form.txt_Shuturyokukubun.Text))
                {
                    searchCondition.SYUTSURYOKUKUBUN = this.form.txt_Shuturyokukubun.Text;
                }

                // 期間From
                if (!string.IsNullOrEmpty(this.form.dtp_KikanFrom.Text))
                {
                    searchCondition.DENPYOU_DATE_FROM = this.form.dtp_KikanFrom.Text + "/" + "01";
                }

                // 期間To
                if (!string.IsNullOrEmpty(this.form.dtp_KikanTo.Text))
                {
                    int days = getDaysOfMonth(this.form.dtp_KikanTo.Text);
                    searchCondition.dtp_KikanTO = this.form.dtp_KikanTo.Text + "/" + days.ToString();
                }

                // 取引先CDFrom
                if (!String.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_From.Text))
                {
                    searchCondition.TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_From.Text;
                }

                // 取引先CDTo
                if (!String.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_To.Text))
                {
                    searchCondition.TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_To.Text;
                }

                // 業者ＣＤ_From
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_From.Text))
                {
                    searchCondition.GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_From.Text;
                }

                // 業者ＣＤ_To
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_To.Text))
                {
                    searchCondition.GYOUSHA_CD_TO = this.form.GYOUSHA_CD_To.Text;
                }

                // 業者名From
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU_From.Text))
                {
                    searchCondition.GYOUSHA_NAME_RYAKU_FROM = this.form.GYOUSHA_NAME_RYAKU_From.Text;
                }

                // 業者名To
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU_To.Text))
                {
                    searchCondition.GYOUSHA_NAME_RYAKU_TO = this.form.GYOUSHA_NAME_RYAKU_To.Text;
                }

                // 現場ＣＤ_From
                if (!string.IsNullOrEmpty(this.form.GENBA_CD_From.Text))
                {
                    searchCondition.GENBA_CD_FROM = this.form.GENBA_CD_From.Text;
                }

                // 現場ＣＤ_To
                if (!string.IsNullOrEmpty(this.form.GENBA_CD_To.Text))
                {
                    searchCondition.GENBA_CD_TO = this.form.GENBA_CD_To.Text;
                }

                // 現場名From
                if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU_From.Text))
                {
                    searchCondition.GENBA_NAME_RYAKU_FROM = this.form.GENBA_NAME_RYAKU_From.Text;
                }

                // 現場名To
                if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU_To.Text))
                {
                    searchCondition.GENBA_NAME_RYAKU_TO = this.form.GENBA_NAME_RYAKU_To.Text;
                }

                // 種類CDFrom
                if (!String.IsNullOrEmpty(this.form.SHURUI_CD_From.Text))
                {
                    searchCondition.SHURUI_CD_FROM = this.form.SHURUI_CD_From.Text;
                }

                // 種類CDTo
                if (!String.IsNullOrEmpty(this.form.SHURUI_CD_To.Text))
                {
                    searchCondition.SHURUI_CD_TO = this.form.SHURUI_CD_To.Text;
                }

                // 年
                if (!string.IsNullOrEmpty(this.form.dtp_KikanFrom.Text))
                {
                    searchCondition.YEAR = this.form.dtp_KikanFrom.Text.Substring(0, 4);
                }

                // 集計対象数量
                if (!string.IsNullOrEmpty(this.form.txt_Shuukeisuuryou.Text))
                {
                    searchCondition.SHUUKEISUURYOU = int.Parse(this.form.txt_Shuukeisuuryou.Text);
                }

                this.SearchString = searchCondition;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }


        }
        #endregion

        #region 検索条件チェック
        /// <summary>
        /// 検索条件チェック
        /// </summary>
        private bool isOKCheck()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 月報期間チェック
                if (int.Parse(this.form.txt_Shuturyokukubun.Text.ToString()) == 1)
                {
                    string yearFrom = this.form.dtp_KikanFrom.Text.Substring(0, 7);
                    string yearTo = this.form.dtp_KikanTo.Text.Substring(0, 7);
                    if (yearFrom.CompareTo(yearTo) == 1)
                    {//期間に関するエラー時にカーソルを期間Fromへ設定
                        this.form.dtp_KikanFrom.Focus();
                        msgLogic.MessageBoxShow("E043");
                        return returnVal;
                    }
                }

                // 年報期間チェック
                if (int.Parse(this.form.txt_Shuturyokukubun.Text.ToString()) == 2)
                {
                    string yearFrom = this.form.dtp_KikanFrom.Text.Substring(0, 7);
                    DateTime kikanFrom = Convert.ToDateTime(yearFrom);
                    string yearTo = this.form.dtp_KikanTo.Text.Substring(0, 7);
                    DateTime kikanTo = Convert.ToDateTime(yearTo);


                    if (yearFrom.CompareTo(yearTo) == 1)
                    {//期間に関するエラー時にカーソルを期間Fromへ設定
                        this.form.dtp_KikanFrom.Focus();
                        msgLogic.MessageBoxShow("E043");
                        return returnVal;
                    }

                    if (kikanTo.AddMonths(-12) >= kikanFrom)
                    {//期間に関するエラー時にカーソルを期間Fromへ設定
                        this.form.dtp_KikanFrom.Focus();
                        msgLogic.MessageBoxShow("E002", "期間", "12ヶ月以内の範囲");
                        return returnVal;
                    }

                    //TimeSpan ts = kikanTo - kikanFrom;
                    //int d = ts.Days;
                    //if (d / 365 >= 1 || d / 366 >= 1)
                    //{
                    //    msgLogic.MessageBoxShow("E107", "日付範囲の指定");
                    //    return returnVal;
                    //}
                    //else if (yearFrom.CompareTo(yearTo) == 1)
                    //{
                    //    msgLogic.MessageBoxShow("E043");
                    //    return returnVal;
                    //}


                }
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 「F5 帳票印刷ボタン」イベント処理

        /// <summary>
        /// 「F5 帳票印刷ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // FromToのチェックでエラーになった項目の検証フラグを元に戻す
                this.form.TORIHIKISAKI_CD_From.CausesValidation = true;
                this.form.TORIHIKISAKI_CD_To.CausesValidation = true;
                this.form.GYOUSHA_CD_From.CausesValidation = true;
                this.form.GYOUSHA_CD_To.CausesValidation = true;
                this.form.GENBA_CD_From.CausesValidation = true;
                this.form.GENBA_CD_To.CausesValidation = true;
                this.form.SHURUI_CD_From.CausesValidation = true;
                this.form.SHURUI_CD_To.CausesValidation = true;

                //2014/01/15 追加 qiao start
                if (!dataGet())
                {
                    //データが0件の場合
                    return;
                }
                //2014/01/15 追加 qiao start

                ReportInfoBase reportInfo;
                r_framework.Const.WINDOW_ID windowID;

                //string csvKb = this.form.txt_Shuturyokukubun.Text.ToString();

                if (syutsuRyouku_KBN == "1")
                {   // 月報
                    windowID = WINDOW_ID.R_TEIKI_HAISYAHYOU_TSUKI;
                    reportInfo = new ReportInfoR429(windowID);
                    //月報帳票データ
                    invoicePrint(reportInfo);

                    reportInfo.Create(@".\Template\R429_R430-Form.xml", "LAYOUT1", new DataTable());
                }
                else if (syutsuRyouku_KBN == "2")
                {   // 年報
                    windowID = WINDOW_ID.R_TEIKI_HAISYAHYOU_NEN;
                    reportInfo = new ReportInfoR430(windowID);
                    //年報帳票データ
                    yearInvoicePrint(reportInfo);

                    reportInfo.Create(@".\Template\R429_R430-Form.xml", "LAYOUT2", new DataTable());
                }
                else
                {
                    return;
                }

                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(reportInfo, windowID))
                {
                    formReportPrintPopup.ShowDialog();
                    formReportPrintPopup.Dispose();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion 「F5 帳票印刷ボタン」イベント処理

        #region 「F6 CSV出力ボタン」イベント処理
        /// <summary>
        /// 「F6 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // FromToのチェックでエラーになった項目の検証フラグを元に戻す
                this.form.TORIHIKISAKI_CD_From.CausesValidation = true;
                this.form.TORIHIKISAKI_CD_To.CausesValidation = true;
                this.form.GYOUSHA_CD_From.CausesValidation = true;
                this.form.GYOUSHA_CD_To.CausesValidation = true;
                this.form.GENBA_CD_From.CausesValidation = true;
                this.form.GENBA_CD_To.CausesValidation = true;
                this.form.SHURUI_CD_From.CausesValidation = true;
                this.form.SHURUI_CD_To.CausesValidation = true;
                
                //2014/01/15 追加 qiao start
                if (!dataGet())
                {
                    //データが0件の場合
                    return;
                }
                //2014/01/15 追加 qiao start

                var result = msgLogic.MessageBoxShow("C012");

                if (result == DialogResult.Yes)
                {
                    //string csvKb = this.form.txt_Shuturyokukubun.Text.ToString();

                    TeikijissekihoukokuDTOClass searchCondition = new TeikijissekihoukokuDTOClass();
                    searchCondition = this.SearchString;

                    if (syutsuRyouku_KBN != string.Empty && syutsuRyouku_KBN == "1")
                    {//出力区分1の場合
                        searchCondition.YEAR = null;

                        monthCsvOutput();

                    }
                    else if (syutsuRyouku_KBN != string.Empty && syutsuRyouku_KBN == "2")
                    {//出力区分２の場合
                        searchCondition.DENPYOU_DATE_FROM = null;
                        searchCondition.dtp_KikanTO = null;
                        yearCSVOutput();
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 「Ｆ9実行ボタン」イベント
        /// <summary>
        /// 「Ｆ9実行ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        //private void bt_func9_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        //画面期間は必須入力項目チェック
        //        if (!this.form.registCheck())
        //        {
        //            return;
        //        }

        //        if (!isOKCheck())
        //        {
        //            return;
        //        }
        //        else if (Search() == 0)
        //        {
        //            msgLogic.MessageBoxShow("C001");
        //            //「F5印刷ボタン」初期状態では非活性にする
        //            parentForm.bt_func5.Enabled = false;
        //            // 「F6CSV出力ボタン」初期状態では非活性にする
        //            parentForm.bt_func6.Enabled = false;
        //            return;
        //        }
        //        else
        //        {
        //            //「F5印刷ボタン」初期状態ではアクティブにする
        //            parentForm.bt_func5.Enabled = true;
        //            // 「F6CSV出力ボタン」初期状態ではアクティブにする
        //            parentForm.bt_func6.Enabled = true;
        //        }          

        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }

        //}

        /// <summary>
        /// 画面で検索データが変わるの場合、F5（印刷）とF6（CSV出力）ボタンが非活性になる
        /// </summary>
        //public void buttonSeigyou(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);
        //        this.parentForm.bt_func5.Enabled = false;
        //        this.parentForm.bt_func6.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }

        //}
        #endregion

        #region 実行
        //2014/01/15 追加 qiao start
        /// <summary>
        /// 「F5印刷」または「F6CSV」押した、データを出力する
        /// </summary>
        /// <returns></returns>
        private bool dataGet()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //画面期間は必須入力項目チェック
                if (!this.form.registCheck())
                {
                    return false;
                }

                if (!isOKCheck())
                {
                    return false;
                }
                else if (Search() == 0)
                {
                    msgLogic.MessageBoxShow("C001");
                    return false;
                }
                else
                {
                    // 最新のSYS_INFOを取得 TODO
                    M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                    if (sysInfo != null && sysInfo.Length > 0)
                    {
                        sysFormat = sysInfo[0].SYS_SUURYOU_FORMAT;
                        return true;
                    }
                    else
                    {
                        sysFormat = "#,###";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        //2014/01/15 追加 qiao end
        #endregion

        #region 「Ｆ12 閉じるボタン」イベント
        /// <summary>
        /// 「Ｆ12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        /// <summary>
        /// 取引先請求マスタを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先請求マスタ</returns>
        internal M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyu(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI_SEIKYUU ret = null;

            var torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            var torihikisakiList = torihikisakiDao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd });
            if (torihikisakiList.Count() == 1)
            {
                var torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                var torihikisakiSeikyuu = torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                if (torihikisakiSeikyuu != null)
                {
                    ret = torihikisakiSeikyuu;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #region 自動生成（実装なし）
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
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
        #endregion

    }
}
