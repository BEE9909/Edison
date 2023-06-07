﻿// $Id: LogicClass.cs 57296 2015-07-30 12:28:39Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Inspection.KenshuMeisaiNyuryoku;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Report;
using Shougun.Core.Scale.Keiryou;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using Shougun.Function.ShougunCSCommon.Utility;
using Seasar.Dao;
using Shougun.Core.SalesPayment.DenpyouHakou.Report;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou_invoice.Report;

namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        #region 定数
        /// <summary>
        /// ボタン設定ファイルのパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.SalesPayment.SyukkaNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// 日連番更新区分
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN hiRenbanRegistKbn { get; set; }

        /// <summary>
        /// 年連番更新区分
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN nenRenbanRegistKbn { get; set; }

        /// <summary>
        /// ROW_NO
        /// </summary>
        internal const string CELL_NAME_ROW_NO = "ROW_NO";

        /// <summary>品名CD</summary>
        internal const string CELL_NAME_HINMEI_CD = "HINMEI_CD";

        /// <summary>品名</summary>
        internal const string CELL_NAME_HINMEI_NAME = "HINMEI_NAME";

        /// <summary>正味重量</summary>
        internal const string CELL_NAME_NET_JYUURYOU = "NET_JYUURYOU";

        /// <summary>伝票区分CD</summary>
        internal const string CELL_NAME_DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>伝票区分名</summary>
        internal const string CELL_NAME_DENPYOU_KBN_NAME = "DENPYOU_KBN_NAME";

        /// <summary>金額</summary>
        internal const string CELL_NAME_KINGAKU = "KINGAKU";

        /// <summary>調整重量</summary>
        internal const string CELL_NAME_CHOUSEI_JYUURYOU = "CHOUSEI_JYUURYOU";

        /// <summary>調整(%)</summary>
        internal const string CELL_NAME_CHOUSEI_PERCENT = "CHOUSEI_PERCENT";

        /// <summary>容器kg</summary>
        internal const string CELL_NAME_YOUKI_JYUURYOU = "YOUKI_JYUURYOU";

        /// <summary>割振重量</summary>
        internal const string CELL_NAME_WARIFURI_JYUURYOU = "WARIFURI_JYUURYOU";

        /// <summary>割振(%)</summary>
        internal const string CELL_NAME_WARIFURI_PERCENT = "WARIFURI_PERCENT";

        /// <summary>総重量</summary>
        internal const string CELL_NAME_STAK_JYUURYOU = "STACK_JYUURYOU";

        /// <summary>空車重量</summary>
        internal const string CELL_NAME_EMPTY_JYUURYOU = "EMPTY_JYUURYOU";

        /// <summary>容器数量</summary>
        internal const string CELL_NAME_YOUKI_SUURYOU = "YOUKI_SUURYOU";

        /// <summary>容器CD</summary>
        internal const string CELL_NAME_YOUKI_CD = "YOUKI_CD";

        /// <summary>容器名</summary>
        internal const string CELL_NAME_YOUKI_NAME_RYAKU = "YOUKI_NAME_RYAKU";

        /// <summary>システムID</summary>
        internal const string CELL_NAME_SYSTEM_ID = "SYSTEM_ID";

        /// <summary>明細システムID</summary>
        internal const string CELL_NAME_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";

        /// <summary>割振重量計算用のグルーピングNo</summary>
        internal const string CELL_NAME_warihuriNo = "warihuriNo";

        /// <summary>warihuriNo内の行番</summary>
        internal const string CELL_NAME_warihuriRowNo = "warihuriRowNo";

        /// <summary>単位CD</summary>
        internal const string CELL_NAME_UNIT_CD = "UNIT_CD";

        /// <summary>単位名</summary>
        internal const string CELL_NAME_UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        /// <summary>数量</summary>
        internal const string CELL_NAME_SUURYOU = "SUURYOU";

        /// <summary>単価</summary>
        internal const string CELL_NAME_TANKA = "TANKA";

        /// <summary>確定区分</summary>
        internal const string CELL_NAME_KAKUTEI_KBN = "KAKUTEI_KBN";

        /// <summary>売上支払日</summary>
        internal const string CELL_NAME_URIAGESHIHARAI_DATE = "URIAGESHIHARAI_DATE";

        /// <summary>締処理状況</summary>
        internal const string CELL_NAME_JOUKYOU = "JOUKYOU";

        /// <summary>マニフェスト番号</summary>
        internal const string CELL_NAME_MANIFEST_ID = "MANIFEST_ID";

        // 2次
        /// <summary>荷姿数量</summary>
        internal const string CELL_NAME_NISUGATA_SUURYOU = "NISUGATA_SUURYOU";

        /// <summary>荷姿単位CD</summary>
        internal const string CELL_NAME_NISUGATA_UNIT_CD = "NISUGATA_UNIT_CD";

        /// <summary>荷姿単位名</summary>
        internal const string CELL_NAME_NISUGATA_NAME_RYAKU = "NISUGATA_NAME_RYAKU";

        /// <summary>在庫品名CD</summary>
        internal const string CELL_NAME_ZAIKO_HINMEI_CD = "ZAIKO_HINMEI_CD";

        /// <summary>在庫品名</summary>
        //internal const string CELL_NAME_ZAIKO_HINMEI_RYAKU = "ZAIKO_HINMEI_RYAKU";
        internal const string CELL_NAME_ZAIKO_HINMEI_NAME = "ZAIKO_HINMEI_NAME";

        ///// <summary>在庫単位CD</summary>
        //internal const string CELL_NAME_ZAIKO_UNIT_CD = "ZAIKO_UNIT_CD";

        ///// <summary>在庫単位名</summary>
        //internal const string CELL_NAME_ZAIKO_UNIT_NAME = "ZAIKO_UNIT_NAME";

        /// <summary>在庫単価</summary>
        internal const string CELL_NAME_ZAIKO_TANKA = "ZAIKO_TANKA";

        ///// <summary>在庫金額</summary>
        //internal const string CELL_NAME_ZAIKO_KINGAKU = "ZAIKO_KINGAKU";

        /// <summary>明細備考</summary>
        internal const string CELL_NAME_MEISAI_BIKOU = "MEISAI_BIKOU";

        /// <summary>消費税率</summary>
        internal const string CELL_NAME_SHOUHIZEI_RATE = "CELL_SHOUHIZEI_RATE";

        /// <summary>税区分CD</summary>
        internal const string CELL_NAME_HINMEI_ZEI_KBN_CD = "HINMEI_ZEI_KBN_CD";

        /// <summary>
        /// 明細行に売上と支払が混在している場合
        /// </summary>
        internal const int URIAGE_SHIHARAI_MIXED = 0;

        /// <summary>
        /// 明細行に売上のみある場合
        /// </summary>
        internal const int URIAGE_ONLY = 1;

        /// <summary>
        /// 明細行に支払のみある場合
        /// </summary>
        internal const int SHIHARAI_ONLY = 2;

        // 仕切書種類
        enum DENPYO_SHIKIRISHO_KIND
        {
            SEIKYUU = 1,
            SHIHARAI,
            SOUSAI
        }

        // 伝票発行区分
        const string DEF_HAKKOU_KBN_KOBETSU = "1";
        const string DEF_HAKKOU_KBN_SOUSAI = "2";
        const string DEF_HAKKOU_KBN_ALL = "3";

        /// <summary>
        /// 端数処理種別
        /// </summary>
        private enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }

        /// <summary>
        /// システム単価書式コード
        /// </summary>
        private enum SysTankaFormatCd : int
        {
            BLANK = 1,      // 1の位がゼロなら空白表示
            NONE,           // 1の位がゼロならゼロ表示
            ONEPOINT,       // 小数点第１位まで表示
            TWOPOINT,       // 小数点第２位まで表示
            THREEPOINT,     // 小数点第３位まで表示
        }

        /// <summary>
        /// システム数量書式コード
        /// </summary>
        private enum SysSuuryouFormatCd : int
        {
            BLANK = 1,      // 1の位がゼロなら空白表示
            NONE,           // 1の位がゼロならゼロ表示
            ONEPOINT,       // 小数点第１位まで表示
            TWOPOINT,       // 小数点第２位まで表示
            THREEPOINT,     // 小数点第３位まで表示
        }

        // No.3822-->
        private System.Collections.Specialized.StringCollection DenpyouCtrl = new System.Collections.Specialized.StringCollection();
        private System.Collections.Specialized.StringCollection DetailCtrl = new System.Collections.Specialized.StringCollection();
        // No.3822<--

        /// <summary>
        /// 滞留登録された出荷伝票に設定する画面区分(新規)
        /// </summary>
        private static readonly WINDOW_TYPE tairyuuWindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

        #endregion

        /// <summary>
        /// Form
        /// </summary>
        private Shougun.Core.SalesPayment.SyukkaNyuuryoku.UIForm form;

        /// <summary>
        /// フッター
        /// </summary>
        public BusinessBaseForm footerForm;

        /// <summary>
        /// ヘッダー
        /// </summary>
        private UIHeaderForm headerForm;

        /// <summary>
        /// 出荷入力専用DBアクセッサー
        /// </summary>
        internal Shougun.Core.SalesPayment.SyukkaNyuuryoku.Accessor.DBAccessor accessor;

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        /// <summary>
        /// DTO
        /// </summary>
        internal DTOClass dto;

        /// <summary>
        /// 画面表示時点DTO
        /// </summary>
        internal DTOClass beforDto;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary>
        /// SHUKKA_ENTRY用DataBinder
        /// </summary>
        internal DataBinderLogic<T_SHUKKA_ENTRY> shukkaEntryDataBinder;

        /// <summary>
        /// SHUKKA_DETAIL用DataBinder
        /// </summary>
        internal DataBinderLogic<T_SHUKKA_DETAIL> shukkaDetailDataBinder;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        public MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 重量系の情報をまとめたリスト
        /// 明細行と重量系との関係が同期されないため
        /// このオブジェクトを使ってコントロールする
        /// </summary>
        internal List<List<JyuuryouDto>> jyuuryouDtoList = new List<List<JyuuryouDto>>();

        /// <summary>
        /// 伝票区分全件
        /// </summary>
        Dictionary<short, M_DENPYOU_KBN> denpyouKbnDictionary = new Dictionary<short, M_DENPYOU_KBN>();

        /// <summary>
        /// 容器全件
        /// </summary>
        Dictionary<string, M_YOUKI> youkiDictionary = new Dictionary<string, M_YOUKI>();

        /// <summary>
        /// 単位区分全件
        /// </summary>
        Dictionary<short, M_UNIT> unitDictionary = new Dictionary<short, M_UNIT>();

        /// <summary>
        /// サブファンクションから呼ばれたか判断するフラグ
        /// </summary>
        internal bool isSubFunctionCall = true;

        /// <summary>
        /// UIFormの入力コントロール名一覧
        /// </summary>
        private string[] inputUiFormControlNames =
            { "KEIZOKU_NYUURYOKU_VALUE","KEIZOKU_NYUURYOKU_ON","KEIZOKU_NYUURYOKU_OFF","ENTRY_NUMBER", "RENBAN", "KAKUTEI_KBN", "UKETSUKE_NUMBER", "KEIRYOU_NUMBER", "TAIRYUU_BIKOU", "DENPYOU_BIKOU",
                "nextButton", "previousButton", "DENPYOU_DATE", "URIAGE_DATE", "SHIHARAI_DATE", "SHARYOU_CD", "NYUURYOKU_TANTOUSHA_CD",
                "TORIHIKISAKI_CD", "TORIHIKISAKI_SEARCH_BUTTON", "SHASHU_CD", "UNPAN_GYOUSHA_CD", 
                "UNPAN_GYOUSHA_SEARCH_BUTTON", "NYUURYOKU_TANTOU_KBN", "GYOUSHA_CD", "GENBA_SEARCH_BUTTON", "UNTENSHA_CD", "UNTEN_KBN",
                "NINZUU_CNT", "GENBA_CD", "customPopupOpenButton1", "KEITAI_KBN_CD", "NIZUMI_GYOUSHA_CD", 
                "NIZUMI_GHOUSHA_KBN", "NIZUMI_GYOUSHA_SEARCH_BUTTON", "DAIKAN_KBN", "NIZUMI_GENBA_CD", "customPopupOpenButton2",
                "SAISHUU_SHOBUNJOU_KBN", "MANIFEST_SHURUI_CD", "CONTENA_SOUSA_CD", "MANIFEST_TEHAI_CD",
                "EIGYOU_TANTOUSHA_CD", "syukkaNyuuryokuDetail1", "EIGYOU_TANTOU_KBN", "KAKUTEI_KBN_NAME", "TORIHIKISAKI_NAME_RYAKU",
                "SEIKYUU_SHIMEBI1", "SEIKYUU_SHIMEBI2", "SEIKYUU_SHIMEBI3", "SHIHARAI_SHIMEBI1", "SHIHARAI_SHIMEBI2",
                "SHIHARAI_SHIMEBI3", "GYOUSHA_NAME_RYAKU", "GENBA_NAME_RYAKU", "NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_NAME",
                "EIGYOU_TANTOUSHA_NAME", "NYUURYOKU_TANTOUSHA_NAME", "SHARYOU_NAME_RYAKU", "SHASHU_NAME", "UNPAN_GYOUSHA_NAME",
                "UNTENSHA_NAME", "KEITAI_KBN_NAME_RYAKU", "DAIKAN_KBN_NAME", "CONTENA_JOUKYOU_NAME_RYAKU", "MANIFEST_SHURUI_NAME_RYAKU",
                "MANIFEST_TEHAI_NAME_RYAKU", "EIGYOU_TANTOUSHA_SEARCH_BUTTON", "NYUURYOKU_TANTOUSHA_SEARCH_BUTTON", "SHARYOU_SEARCH_BUTTON",
                "SHASHU_SEARCH_BUTTON", "UNTENSHA_SEARCH_BUTTON", "KEITAI_KBN_SEARCH_BUTTON", "CONTENA_SOUSA_SEARCH_BUTTON",
                "MANIFEST_SHURUI_SEARCH_BUTTON", "MANIFEST_TEHAI_SEARCH_BUTTON", "GYOUSHA_SEARCH_BUTTON", "NIZUMI_GENBA_SEARCH_BUTTON",
                "URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON","SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON",
                //2次
                "txtUri","txtShi","txtShimeZaiko","txtKensyuu","KUUSHA_JYURYO",
                "KENSHU_MUST_KBN"
            };

        /// <summary>
        /// UIFormの入力コントロール名一覧（参照用）
        /// </summary>
        private string[] refUiFormControlNames =
            { "KEIZOKU_NYUURYOKU_VALUE","KEIZOKU_NYUURYOKU_ON","KEIZOKU_NYUURYOKU_OFF","ENTRY_NUMBER", "RENBAN", "KAKUTEI_KBN", "UKETSUKE_NUMBER", "KEIRYOU_NUMBER", "TAIRYUU_BIKOU", "DENPYOU_BIKOU",
                "DENPYOU_DATE", "URIAGE_DATE", "SHIHARAI_DATE", "SHARYOU_CD", "NYUURYOKU_TANTOUSHA_CD",
                "TORIHIKISAKI_CD", "TORIHIKISAKI_SEARCH_BUTTON", "SHASHU_CD", "UNPAN_GYOUSHA_CD", 
                "UNPAN_GYOUSHA_SEARCH_BUTTON", "NYUURYOKU_TANTOU_KBN", "GYOUSHA_CD", "GENBA_SEARCH_BUTTON", "UNTENSHA_CD", "UNTEN_KBN",
                "NINZUU_CNT", "GENBA_CD", "customPopupOpenButton1", "KEITAI_KBN_CD", "NIZUMI_GYOUSHA_CD", 
                "NIZUMI_GHOUSHA_KBN", "NIZUMI_GYOUSHA_SEARCH_BUTTON", "DAIKAN_KBN", "NIZUMI_GENBA_CD", "customPopupOpenButton2",
                "SAISHUU_SHOBUNJOU_KBN", "MANIFEST_SHURUI_CD", "CONTENA_SOUSA_CD", "MANIFEST_TEHAI_CD",
                "EIGYOU_TANTOUSHA_CD", "syukkaNyuuryokuDetail1", "EIGYOU_TANTOU_KBN", "KAKUTEI_KBN_NAME", "TORIHIKISAKI_NAME_RYAKU",
                "SEIKYUU_SHIMEBI1", "SEIKYUU_SHIMEBI2", "SEIKYUU_SHIMEBI3", "SHIHARAI_SHIMEBI1", "SHIHARAI_SHIMEBI2",
                "SHIHARAI_SHIMEBI3", "GYOUSHA_NAME_RYAKU", "GENBA_NAME_RYAKU", "NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_NAME",
                "EIGYOU_TANTOUSHA_NAME", "NYUURYOKU_TANTOUSHA_NAME", "SHARYOU_NAME_RYAKU", "SHASHU_NAME", "UNPAN_GYOUSHA_NAME",
                "UNTENSHA_NAME", "KEITAI_KBN_NAME_RYAKU", "DAIKAN_KBN_NAME", "CONTENA_JOUKYOU_NAME_RYAKU", "MANIFEST_SHURUI_NAME_RYAKU",
                "MANIFEST_TEHAI_NAME_RYAKU", "EIGYOU_TANTOUSHA_SEARCH_BUTTON", "NYUURYOKU_TANTOUSHA_SEARCH_BUTTON", "SHARYOU_SEARCH_BUTTON",
                "SHASHU_SEARCH_BUTTON", "UNTENSHA_SEARCH_BUTTON", "KEITAI_KBN_SEARCH_BUTTON", "CONTENA_SOUSA_SEARCH_BUTTON",
                "MANIFEST_SHURUI_SEARCH_BUTTON", "MANIFEST_TEHAI_SEARCH_BUTTON", "GYOUSHA_SEARCH_BUTTON", "NIZUMI_GENBA_SEARCH_BUTTON",
                "URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON","SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON",
                //2次
                "txtUri","txtShi","txtShimeZaiko","txtKensyuu","KUUSHA_JYURYO",
                "KENSHU_MUST_KBN"
            };

        // No.3822-->
        /// <summary>
        /// タブストップ用
        /// </summary>
        private string[] tabUiFormControlNames =
            {   "KEIZOKU_NYUURYOKU_VALUE","KYOTEN_CD","NYUURYOKU_TANTOUSHA_CD","ENTRY_NUMBER",
                "RENBAN", "KAKUTEI_KBN", "UKETSUKE_NUMBER", "KEIRYOU_NUMBER","DENPYOU_DATE",
                "URIAGE_DATE", "SHIHARAI_DATE", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME","SHASHU_CD",
                "SHASHU_NAME","SHARYOU_CD", "SHARYOU_NAME_RYAKU","UNTENSHA_CD","NINZUU_CNT",
                "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU", "GENBA_CD","GENBA_NAME_RYAKU", "TORIHIKISAKI_CD",
                "TORIHIKISAKI_NAME_RYAKU","NIZUMI_GYOUSHA_CD", "NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_CD", "NIZUMI_GENBA_NAME",
                "KEITAI_KBN_CD", "DAIKAN_KBN", "MANIFEST_SHURUI_CD", "MANIFEST_TEHAI_CD","EIGYOU_TANTOUSHA_CD",
                "DENPYOU_BIKOU", "TAIRYUU_BIKOU",
            };
        private string[] tabDetailControlNames =
            {   "EMPTY_JYUURYOU","STACK_JYUURYOU","WARIFURI_JYUURYOU", "WARIFURI_PERCENT","CHOUSEI_JYUURYOU",
                "CHOUSEI_PERCENT", "YOUKI_NAME_RYAKU","YOUKI_CD","YOUKI_SUURYOU","YOUKI_JYUURYOU",
                "HINMEI_CD","SUURYOU","UNIT_CD","TANKA",
                "KINGAKU","NISUGATA_SUURYOU","NISUGATA_UNIT_CD","MANIFEST_ID","MEISAI_BIKOU",
            };
        // No.3822<--

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames = { "KYOTEN_CD", "KYOTEN_NAME_RYAKU", "CreateUser", "CreateDate", "LastUpdateUser", "LastUpdateDate" };

        /// <summary>
        /// Detailの入力コントロール名一覧
        /// </summary>
        //private string[] inputDetailControlNames =
        //        { "ROW_NO", "JOUKYOU", "URIAGESHIHARAI_DATE", "EMPTY_JYUURYOU", "STACK_JYUURYOU", "WARIFURI_JYUURYOU", "WARIFURI_PERCENT", "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT", "YOUKI_NAME_RYAKU", "YOUKI_JYUURYOU", "NET_JYUURYOU", "SUURYOU", "KINGAKU", "MEISAI_BIKOU", "UNIT_CD", "HINMEI_CD", "DENPYOU_KBN_NAME", "HINMEI_NAME", "YOUKI_CD", "YOUKI_SUURYOU", "KAKUTEI_KBN", "TANKA", "DETAIL_SYSTEM_ID", "UNIT_NAME_RYAKU", "MANIFEST_ID",
        //            "WARIFURI_PERCENT", "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT", "YOUKI_NAME_RYAKU", "YOUKI_JYUURYOU", "NET_JYUURYOU", "SUURYOU", "KINGAKU",
        //            "MEISAI_BIKOU", "UNIT_CD", "HINMEI_CD", "DENPYOU_KBN_NAME", "HINMEI_NAME", "YOUKI_CD", "YOUKI_SUURYOU", "KAKUTEI_KBN", "TANKA",
        //            "DETAIL_SYSTEM_ID", "UNIT_NAME_RYAKU", "MANIFEST_ID",
        //            //2次
        //            "NISUGATA_SUURYOU","NISUGATA_UNIT_CD","NISUGATA_NAME_RYAKU","ZAIKO_HINMEI_CD","ZAIKO_UNIT_NAME",
        //            "ZAIKO_HINMEI_CD","ZAIKO_HINMEI_RYAKU","ZAIKO_TANKA","ZAIKO_KINGAKU","ZAIKO_UNIT_CD"
        //        };
        private string[] inputDetailControlNames = new string[] {
            "ROW_NO", "JOUKYOU", "URIAGESHIHARAI_DATE", "EMPTY_JYUURYOU", "STACK_JYUURYOU", "WARIFURI_JYUURYOU", "WARIFURI_PERCENT",
            "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT", "YOUKI_NAME_RYAKU", "YOUKI_JYUURYOU", "NET_JYUURYOU", "SUURYOU", "KINGAKU",
            "MEISAI_BIKOU", "UNIT_CD", "HINMEI_CD", "DENPYOU_KBN_NAME", "HINMEI_NAME", "YOUKI_CD", "YOUKI_SUURYOU", "KAKUTEI_KBN", "TANKA",
            "DETAIL_SYSTEM_ID", "UNIT_NAME_RYAKU", "MANIFEST_ID",
            //2次
            "NISUGATA_SUURYOU", "NISUGATA_UNIT_CD", "NISUGATA_NAME_RYAKU",
            "ZAIKO_HINMEI_CD","ZAIKO_HINMEI_NAME", "ZAIKO_TANKA"
        };

        /// <summary>
        /// 入出金更新用DTO
        /// </summary>
        private NyuuShukkinDTOClass nyuuShukkinDto = new NyuuShukkinDTOClass();

        private string zenkaiUketsuke;

        private string zenkaiKeiryo;

        ///// <summary>
        ///// 在庫明細用単位CD
        ///// </summary>
        //private string zaikoUnitCd = string.Empty;

        ///// <summary>
        ///// TODO: 在庫明細入力呼び出し用（仮）
        ///// </summary>
        //static List<T_ZAIKO_SHUKKA_DETAIL> test;

        /// <summary>
        /// 出荷受付入力エンティティ（更新用に保持）
        /// </summary>
        internal T_UKETSUKE_SK_ENTRY tUketsukeSkEntry;

        /// <summary>
        /// 登録処理中フラグ
        /// </summary>
        internal bool IsRegist { get; set; }

        /// <summary>
        /// 数量処理FLG(伝票上部の項目で、値を変更しても、数量が自動たで修正されないことため)
        /// </summary>
        internal bool IsSuuryouKesannFlg { get; set; }

        // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
        /// <summary>
        /// 車輌休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workclosedsharyouDao;

        /// <summary>
        /// 運転者休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workcloseduntenshaDao;

        /// <summary>
        /// 搬入先休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workclosedhannyuusakiDao;
        // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end

        // 4935_7 出荷入力 jyokou 20150505 str
        internal bool isRegistered = false;
        // 4935_7 出荷入力 jyokou 20150505 end

        /// <summary>
        /// Detail項目で最初のセルフォーカス位置のセル名
        /// </summary>
        internal string firstIndexDetailCellName = "EMPTY_JYUURYOU";

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal bool hasShow = false;
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        //出荷(検収済)データの業者・現場を変更した際に、取引先で単価更新のPOP表示有無をチェック
        internal bool bolPOPTan = false;


        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        private GET_SYSDATEDao dao;

        // MAILAN #158992 START
        internal bool isTankaMessageShown = false;
        internal bool isCheckTankaFromChild = false;
        // MAILAN #158992 END

        //仕切書の明細合計金額表示用フラグ
        //品名内税or明細毎内税(品名税なし)の明細があれば、合計金額をブランクで表示
        internal int SHIKIRISHO_UR_UTIZEI = 0;
        internal int SHIKIRISHO_SH_UTIZEI = 0;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(Shougun.Core.SalesPayment.SyukkaNyuuryoku.UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム
            this.form = targetForm;

            // dto
            this.dto = new DTOClass();

            // Accessor
            this.accessor = new Shougun.Core.SalesPayment.SyukkaNyuuryoku.Accessor.DBAccessor();
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();

            // Utility
            this.controlUtil = new ControlUtility();

            this.jyuuryouDtoList = new List<List<JyuuryouDto>>();

            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();
            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end

            this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();

            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();

            this.msgLogic = new MessageBoxShowLogic();

            zenkaiUketsuke = string.Empty;
            zenkaiKeiryo = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // LogicClassで初期化が必要な場合はここに記載
                this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;
                this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;

                footerForm = (BusinessBaseForm)this.form.Parent;
                headerForm = (UIHeaderForm)footerForm.headerForm;
                headerForm.logic = this;    // No.3822

                shukkaEntryDataBinder = new DataBinderLogic<T_SHUKKA_ENTRY>(this.dto.entryEntity);
                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.form.allControl.ToList());
                allControl.AddRange(controlUtil.GetAllControls(this.headerForm));
                allControl.AddRange(controlUtil.GetAllControls(this.footerForm));
                shukkaEntryDataBinder.AllControl = allControl.ToArray();

                shukkaDetailDataBinder = new DataBinderLogic<T_SHUKKA_DETAIL>(this.dto.detailEntity);

                this.ChangeEnabledForInputControl(false);

                // 月次処理中・月次締済み、請求・精算締済みチェックを行い締済みの場合は参照モードに切り替え
                if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    || this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                {
                    DateTime getsujiShoriCheckDate = this.dto.entryEntity.DENPYOU_DATE.Value;
                    GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                    // 月次処理中チェック
                    if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("E224", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                    }
                    // 月次処理ロックチェック
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("E222", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                    }
                    else if (CheckAllShimeStatus())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("I011", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                    }
                }

                // DisplayInitでTemplateを書き換えているので、データをセットする前に実行すること
                // 滞留伝票の場合、WindowTypeが変更され、DisplayInitメソッドに影響があるため、このタイミングで実行する
                this.DisplayInit();

                // タブオーダーデータ取得
                GetStatus();   // No.3822

                // No.2334-->
                if (this.dto.entryEntity.TAIRYUU_KBN)
                {
                    this.form.TairyuuNewFlg = true;
                    if (!this.dto.entryEntity.SHUKKA_NUMBER.IsNull)
                    {
                        this.form.ShukkaNumber = long.Parse(this.dto.entryEntity.SHUKKA_NUMBER.ToString());
                    }
                    if (!this.dto.entryEntity.UKETSUKE_NUMBER.IsNull)
                    {
                        this.form.UketukeNumber = long.Parse(this.dto.entryEntity.UKETSUKE_NUMBER.ToString());
                    }
                    if (!this.dto.entryEntity.KEIRYOU_NUMBER.IsNull)
                    {
                        this.form.KeiryouNumber = long.Parse(this.dto.entryEntity.KEIRYOU_NUMBER.ToString());
                    }
                    // 滞留一覧から削除で開かれた場合は、モードを変更しない
                    if (HadChangedWindowTypeTairyuu(this.form.WindowType))
                    {
                        this.form.WindowType = tairyuuWindowType;
                    }
                    this.form.HeaderFormInit();
                }
                else
                {
                    this.form.TairyuuNewFlg = false;
                }
                // No.2334<--

                this.EntryDataInit();

                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    // データ読み込み時、総重量、空車重量、割振、調整項目の活性制御がされないため、このタイミングで活性制御を行う
                    if (!this.WarifuriReadOnlyCheck(row))
                    {
                        return false;
                    }
                    // 単価と金額の活性制御
                    this.form.SetIchranReadOnly(row.Index);
                }

                if (!this.NumberingRowNo())
                {
                    return false;
                }

                if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                {
                    // 削除モード時には全コントロールをReadOnlyにする
                    this.ChangeEnabledForInputControl(true);
                    this.form.SHARYOU_NAME_RYAKU.Enabled = false;
                }
                else if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    // 検収データ用の制御
                    if (this.form.KENSHU_MUST_KBN.Checked && (this.kenshuZumi.Equals(this.form.txtKensyuu.Text)))
                    {
                        // 明細行は編集不可とする仕様
                        foreach (Row row in this.form.gcMultiRow1.Rows)
                        {
                            foreach (var detaiControlName in inputDetailControlNames)
                            {
                                row.Cells[detaiControlName].Enabled = false;
                            }
                        }

                        // 検収有無
                        this.form.KENSHU_MUST_KBN.Enabled = false;
                        // 要検収と差異が出てしまうので売上、支払日付も非活性にする。(暫定対応)
                        this.form.URIAGE_DATE.Enabled = false;
                        this.form.SHIHARAI_DATE.Enabled = false;
                        // 2017/06/08 DIQ 標準修正 #100076 検収済みの場合、[伝票日付]を編集不可にする。START
                        this.form.DENPYOU_DATE.Enabled = false;
                        // 2017/06/08 DIQ 標準修正 #100076 検収済みの場合、[伝票日付]を編集不可にする。END
                    }
                }
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {

                    // 売上消費税率設定
                    if (!this.SetUriageShouhizeiRate()) { return false; }
                    if (!this.SetShiharaiShouhizeiRate()) { return false; }

                    this.form.isArgumentUketsukeNumber = true;
                    this.form.isArgumentKeiryouNumber = true;

                    // 新規で受付番号がセットされている場合
                    if (this.form.UketukeNumber > -1)
                    {
                        this.form.UKETSUKE_NUMBER.Text = this.form.UketukeNumber.ToString();
                        if (this.form.TairyuuNewFlg == false)   // No.2334
                        {
                            this.form.UketukeNumber = -1;   // 一回呼んだら初期化しておく
                        }
                    }
                    else if (this.form.KeiryouNumber > -1)
                    {
                        this.form.KEIRYOU_NUMBER.Text = this.form.KeiryouNumber.ToString();
                        if (!this.GetKeiryouNumber())
                        {
                            return false;
                        }
                        if (this.form.TairyuuNewFlg == false)   // No.2334
                        {
                            this.form.KeiryouNumber = -1;   // 一回呼んだら初期化しておく
                        }
                    }
                    this.form.isArgumentUketsukeNumber = false;
                    this.form.isArgumentKeiryouNumber = false;
                }

                // 伝票発行ポップアップDTO初期化
                this.form.denpyouHakouPopUpDTO = new Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass();

                this.SetControlProperties();
                // 検索ポップアップの設定
                this.SetSearchButtonInfo();

                // 継続入力初期化
                if (string.IsNullOrEmpty(this.form.KEIZOKU_NYUURYOKU_VALUE.Text))
                {
                    // 初期値設定
                    // 空以外の時には前の情報を引き継ぐ仕様
                    this.form.KEIZOKU_NYUURYOKU_VALUE.Text = SalesPaymentConstans.KEIZOKU_NYUURYOKU_OFF;
                }

                // 車輌選択ポップアップ選択中フラグ初期化
                this.form.isSelectingSharyouCd = false;

                // Entryデータがある場合は車輌項目のデザインを初期化
                if (!WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType))
                {
                    if (this.dto.entryEntity != null
                        && !this.dto.entryEntity.SYSTEM_ID.IsNull
                        && !this.dto.entryEntity.SYSTEM_ID.IsNull
                        && !string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                    {
                        this.CheckShokuchiSharyou();
                    }
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "新規";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "修正";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "参照";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "削除";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.White;
                        break;
                }

                this.form.IsLoading = false;
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("WindowInit", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInit", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <param name="registeredFlag">登録後処理かどうか。デフォルト：false</param>
        public bool ButtonInit(bool registeredFlag = false)
        {
            try
            {
                LogUtility.DebugMethodStart(registeredFlag);
                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, footerForm, this.form.WindowType);

                // 初期化
                foreach (var button in buttonSetting)
                {
                    var cont = controlUtil.FindControl(footerForm, button.ButtonName);
                    if (!string.IsNullOrEmpty(cont.Text)) cont.Enabled = true;
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        // 4935_7 出荷入力 jyokou 20150505 str
                        //this.footerForm.bt_process4.Enabled = false;
                        this.footerForm.bt_process4.Enabled = true;
                        // 4935_7 出荷入力 jyokou 20150505 end
                        this.footerForm.bt_process5.Enabled = false;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 修正モードの場合、登録後に初めて活性化するボタンがあるので制御
                        if (registeredFlag)
                        {
                            // 登録後処理
                            this.footerForm.bt_func1.Enabled = true;
                            this.footerForm.bt_func2.Enabled = true;
                            this.footerForm.bt_func3.Enabled = true;
                            this.footerForm.bt_func5.Enabled = false;
                            this.footerForm.bt_func6.Enabled = false;
                            this.footerForm.bt_func9.Enabled = false;
                            this.footerForm.bt_func10.Enabled = false;
                            this.footerForm.bt_func11.Enabled = false;
                            this.footerForm.bt_process1.Enabled = false;
                            this.footerForm.bt_process2.Enabled = false;
                            this.footerForm.bt_process3.Enabled = false;
                            this.footerForm.bt_process4.Enabled = true;
                            if (this.form.KENSHU_MUST_KBN.Checked)
                            {
                                this.footerForm.bt_process5.Enabled = true;
                            }
                            else
                            {
                                this.footerForm.bt_process5.Enabled = false;
                            }
                        }
                        else
                        {
                            this.footerForm.bt_func1.Enabled = true;
                            this.footerForm.bt_func2.Enabled = true; // No.2819
                            this.footerForm.bt_func3.Enabled = false;
                            this.footerForm.bt_func5.Enabled = true;
                            this.footerForm.bt_func6.Enabled = false;
                            this.footerForm.bt_func9.Enabled = true;
                            // 2017/06/08 DIQ 標準修正 #100076 検収済みの場合、[明細行]の追加/削除を行えないようにする。START
                            if (this.form.KENSHU_MUST_KBN.Checked && (this.kenshuZumi.Equals(this.form.txtKensyuu.Text)))
                            {
                                this.footerForm.bt_func10.Enabled = false;
                                this.footerForm.bt_func11.Enabled = false;
                            }
                            else
                            {
                                this.footerForm.bt_func10.Enabled = true;
                                this.footerForm.bt_func11.Enabled = true;
                            }
                            // 2017/06/08 DIQ 標準修正 #100076 検収済みの場合、[明細行]の追加/削除を行えないようにする。END
                            this.footerForm.bt_process1.Enabled = false;
                            this.footerForm.bt_process2.Enabled = true;
                            this.footerForm.bt_process3.Enabled = false;
                            this.footerForm.bt_process4.Enabled = true;
                            if (this.form.KENSHU_MUST_KBN.Checked)
                            {
                                this.footerForm.bt_process5.Enabled = true;
                            }
                            else
                            {
                                this.footerForm.bt_process5.Enabled = false;
                            }
                        }
                        break;

                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = true; // No.2819
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func6.Enabled = false;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func9.Enabled = false;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = true;
                        this.footerForm.bt_process3.Enabled = false;
                        this.footerForm.bt_process4.Enabled = false;
                        if (this.form.KENSHU_MUST_KBN.Checked)
                        {
                            // 「検収入力」ボタンが非活性になると検収入力の参照が出来ないため、あえて、活性状態にする。
                            this.footerForm.bt_process5.Enabled = true;
                        }
                        else
                        {
                            this.footerForm.bt_process5.Enabled = false;
                        }
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = false;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func6.Enabled = false;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func9.Enabled = true;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        this.footerForm.bt_process4.Enabled = false;
                        this.footerForm.bt_process5.Enabled = false;
                        break;

                    default:
                        break;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ButtonInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// イベント初期化処理
        /// </summary>
        public bool EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //ボタン(F1)イベント
                footerForm.bt_func1.Click += new EventHandler(this.form.Weight_Click);

                // 新規ボタン(F2)イベント
                footerForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);

                // 修正ボタン(F3)イベント
                footerForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);

                //予定一覧(F4)イベント
                footerForm.bt_func4.Click += new EventHandler(this.form.UketsukeDenpyo);

                // 滞留ボタン(F5)イベント
                this.form.C_Regist(footerForm.bt_func5);
                footerForm.bt_func5.Click += new EventHandler(this.form.TairyuuRegist);
                footerForm.bt_func5.ProcessKbn = PROCESS_KBN.NEW;

                // 一覧ボタン(F7)イベント
                footerForm.bt_func7.Click += new EventHandler(this.form.ShowDenpyouIchiran);

                // 登録ボタン(F9)イベント
                this.form.C_Regist(footerForm.bt_func9);
                footerForm.bt_func9.Click += new EventHandler(this.form.Regist);
                footerForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 行挿入ボタン(F10)イベント
                footerForm.bt_func10.Click += new EventHandler(this.form.AddRow);

                // 行挿入ボタン(F11)イベント
                footerForm.bt_func11.Click += new EventHandler(this.form.RemoveRow);

                // 閉じるボタン(F12)イベント生成
                footerForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                //プロセスボタンイベント生成
                footerForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);
                footerForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
                footerForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);
                footerForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);
                footerForm.bt_process5.Click += new EventHandler(this.form.bt_process5_Click);

                // コントロールのイベント
                this.form.TORIHIKISAKI_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIZUMI_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIZUMI_GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.UNPAN_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.SHARYOU_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.SHARYOU_CD.TextChanged += new EventHandler(this.SHARYOU_CD_TextChanged);

                // 全てのコントロールのEnterイベントに追加
                foreach (Control ctrl in this.form.Controls)
                {
                    ctrl.Enter -= new EventHandler(this.GetControlEnter);
                    ctrl.Enter += new EventHandler(this.GetControlEnter);
                }
                foreach (Control ctrl in this.headerForm.Controls)
                {
                    ctrl.Enter -= new EventHandler(this.GetControlEnter);
                    ctrl.Enter += new EventHandler(this.GetControlEnter);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全コントロールのEnterイベントで必ず通る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetControlEnter(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if ((ctrl is TextBox || ctrl is GrapeCity.Win.MultiRow.GcMultiRow))
            {
                this.form.beforbeforControlName = this.form.beforControlName;
                this.form.beforControlName = ctrl.Name;
            }
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
        /// 表示制御
        /// </summary>
        private void DisplayInit()
        {
            this.form.KEITAI_KBN_CD.PopupDataHeaderTitle = new string[] { "形態区分CD", "形態区分名" };
            this.form.KEITAI_KBN_CD.PopupDataSource = this.CreateKeitaiKbnPopupDataSource();
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupDataHeaderTitle = this.form.KEITAI_KBN_CD.PopupDataHeaderTitle;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupDataSource = this.form.KEITAI_KBN_CD.PopupDataSource;

            this.InitShouhizeiRatePopupSetting();

            this.form.ENTRY_NUMBER.ReadOnly = false;
            this.form.RENBAN.ReadOnly = false;
            this.form.UKETSUKE_NUMBER.ReadOnly = false;
            this.form.KEIRYOU_NUMBER.ReadOnly = false;

            this.ChangePropertyForGC(null, new string[] { CELL_NAME_STAK_JYUURYOU, CELL_NAME_EMPTY_JYUURYOU }, "ReadOnly", false);

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 重量系
                    this.ChangePropertyForGC(null, new string[] { CELL_NAME_WARIFURI_JYUURYOU, CELL_NAME_WARIFURI_PERCENT, CELL_NAME_CHOUSEI_JYUURYOU, CELL_NAME_CHOUSEI_PERCENT }, "ReadOnly", true);
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 重量系
                    this.form.ENTRY_NUMBER.ReadOnly = true;
                    this.form.RENBAN.ReadOnly = true;
                    this.form.UKETSUKE_NUMBER.ReadOnly = true;
                    this.form.KEIRYOU_NUMBER.ReadOnly = true;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    // 削除モードの場合、
                    // すべてをReadOnlyにしたいので初期化の最後に実施
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// 必須チェックの設定を初期化します
        /// </summary>
        internal bool RequiredSettingInit()
        {
            try
            {
                // Entry
                this.headerForm.KYOTEN_CD.RegistCheckMethod = null;
                this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = null;
                this.form.TORIHIKISAKI_CD.RegistCheckMethod = null;
                this.form.GYOUSHA_CD.RegistCheckMethod = null;
                //this.form.MANIFEST_SHURUI_CD.RegistCheckMethod = null;
                //this.form.MANIFEST_TEHAI_CD.RegistCheckMethod = null;
                this.form.URIAGE_DATE.RegistCheckMethod = null;
                this.form.SHIHARAI_DATE.RegistCheckMethod = null;
                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.ReadOnly = true;
                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = null;
                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.ReadOnly = true;
                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = null;
                this.form.DENPYOU_DATE.RegistCheckMethod = null;
                this.form.KAKUTEI_KBN.RegistCheckMethod = null;

                // Detail
                this.form.gcMultiRow1.SuspendLayout();

                foreach (var o in this.form.gcMultiRow1.Rows)
                {
                    var obj2 = controlUtil.FindControl(o.Cells.ToArray(), new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU, CELL_NAME_URIAGESHIHARAI_DATE });
                    foreach (var target in obj2)
                    {
                        PropertyUtility.SetValue(target, "RegistCheckMethod", null);
                    }
                }

                this.form.gcMultiRow1.ResumeLayout();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("RequiredSettingInit", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RequiredSettingInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }

        /// <summary>
        /// 必須チェックの設定を動的に生成
        /// </summary>
        /// <param name="tairyuuKbn">滞留登録かどうか</param>
        internal bool SetRequiredSetting(bool tairyuuKbn)
        {
            try
            {
                // 初期化
                if (!this.RequiredSettingInit())
                {
                    return false;
                }

                // 設定
                SelectCheckDto existCheck = new SelectCheckDto();
                existCheck.CheckMethodName = "必須チェック";
                Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                excitChecks.Add(existCheck);

                if (tairyuuKbn)
                {
                    // 滞留登録
                    this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = excitChecks;
                    this.form.DENPYOU_DATE.RegistCheckMethod = excitChecks;
                    this.headerForm.KYOTEN_CD.RegistCheckMethod = excitChecks;
                }
                else
                {
                    // 登録
                    this.headerForm.KYOTEN_CD.RegistCheckMethod = excitChecks;
                    this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = excitChecks;
                    this.form.TORIHIKISAKI_CD.RegistCheckMethod = excitChecks;
                    this.form.GYOUSHA_CD.RegistCheckMethod = excitChecks;
                    this.form.DENPYOU_DATE.RegistCheckMethod = excitChecks;
                    this.form.KAKUTEI_KBN.RegistCheckMethod = excitChecks;

                    // 初期バージョンでは以下の必須チェックは要らないと判断する
                    //this.form.MANIFEST_SHURUI_CD.RegistCheckMethod = excitChecks;
                    //this.form.MANIFEST_TEHAI_CD.RegistCheckMethod = excitChecks;

                    // 売上日付、支払日付は動的に必須チェックが変わるため、初期カラーに戻す
                    // もし、画面独自に色の制御をしていたら以下の処理も変更すること。
                    this.form.URIAGE_DATE.IsInputErrorOccured = false;
                    this.form.URIAGE_DATE.UpdateBackColor();
                    this.form.SHIHARAI_DATE.IsInputErrorOccured = false;
                    this.form.SHIHARAI_DATE.UpdateBackColor();

                    short kakuteiKbn = 0;
                    if (!string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                    {
                        short.TryParse(this.form.KAKUTEI_KBN.Text, out kakuteiKbn);
                    }
                    if (this.form.URIAGE_DATE.Visible)
                    {
                        if (kakuteiKbn == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                        {
                            // 伝票毎締めの場合
                            if (GetRowsDenpyouKbnCdMixed() != SHIHARAI_ONLY)
                            {
                                // 明細行に、伝票区分が「支払」以外の行も存在する場合
                                this.form.URIAGE_DATE.RegistCheckMethod = excitChecks;
                                // 必須チェックのため一時的にReadOnlyをはずす
                                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.ReadOnly = false;
                                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = excitChecks;
                            }
                        }
                    }
                    if (this.form.SHIHARAI_DATE.Visible)
                    {
                        if (kakuteiKbn == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                        {
                            // 伝票毎締めの場合
                            if (GetRowsDenpyouKbnCdMixed() != URIAGE_ONLY)
                            {
                                // 明細行に、伝票区分が「売上」以外の行も存在する場合
                                this.form.SHIHARAI_DATE.RegistCheckMethod = excitChecks;
                                // 必須チェックのため一時的にReadOnlyをはずす
                                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.ReadOnly = false;
                                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = excitChecks;
                            }
                        }
                    }

                    this.form.gcMultiRow1.SuspendLayout();

                    foreach (var o in this.form.gcMultiRow1.Rows)
                    {
                        string[] registCheckTarget;
                        if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN == SalesPaymentConstans.SHUKKA_KAKUTEI_USE_KBN_YES)
                        {
                            // 売上支払日付は動的に必須チェックが変わるため、初期カラーに戻す
                            // もし、画面独自に色の制御をしていたら以下の処理も変更すること。
                            var urShDate = o.Cells[CELL_NAME_URIAGESHIHARAI_DATE];
                            if (urShDate != null && urShDate.Visible)
                            {
                                var cell = urShDate as ICustomAutoChangeBackColor;
                                cell.IsInputErrorOccured = false;
                                cell.UpdateBackColor();
                            }

                            var kakuteiKbnCell = o.Cells[CELL_NAME_KAKUTEI_KBN];
                            if (kakuteiKbnCell != null && kakuteiKbnCell.Value != null && (bool)kakuteiKbnCell.Value)
                            {
                                registCheckTarget = new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU, CELL_NAME_URIAGESHIHARAI_DATE };
                            }
                            else
                            {
                                registCheckTarget = new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU };
                            }
                        }
                        else
                        {
                            registCheckTarget = new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU, CELL_NAME_URIAGESHIHARAI_DATE };
                        }

                        var obj2 = controlUtil.FindControl(o.Cells.ToArray(), registCheckTarget);
                        foreach (var target in obj2)
                        {
                            var visible = target.GetType().GetProperty("Visible");
                            if (visible != null && (bool)visible.GetValue(target, null))
                            {
                                PropertyUtility.SetValue(target, "RegistCheckMethod", excitChecks);
                            }
                        }
                    }

                    this.form.gcMultiRow1.ResumeLayout();
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetRequiredSetting", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRequiredSetting", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// Logic内で定義されているEntityすべての最新情報を取得する
        /// </summary>
        /// <returns>true:正常値、false:エラー発生</returns>
        public bool GetAllEntityData(out bool catchErr)
        {
            catchErr = false;
            try
            {
                // 検収入力用のデータは初期化のタイミングが難しいのでここで初期化
                this.dto.kenshuNyuuryokuDto = new KenshuNyuuryokuDTOClass();

                // 更新前データを保持しておく
                this.beforDto = new DTOClass();

                // 画面のモードに依存しないデータの取得
                this.dto.sysInfoEntity = CommonShogunData.SYS_INFO;

                // システム設定が設定されていない場合を想定しデフォルト値を設定
                this.SetSysInfoDefaultValue();

                // TODO: CommonShogunDataのCreateメソッドをちゃんとログイン時に呼んでいるか確認
                if (!this.IsRequireData())
                {
                    return true;
                }

                var entrys = accessor.GetShukkaEntry(this.form.ShukkaNumber, this.form.SEQ);
                if (entrys == null || entrys.Length < 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return false;
                }
                else
                {
                    this.form.IsLoading = true;

                    this.dto.entryEntity = entrys[0];
                    // 検収入力用にデータのコピー
                    if (copyModeCheck())
                    {
                        this.dto.kenshuNyuuryokuDto.shukkaEntryEntity = this.dto.ShukkaEntryClone(true);
                    }
                    else
                    {
                        this.dto.kenshuNyuuryokuDto.shukkaEntryEntity = this.dto.ShukkaEntryClone(false);
                    }
                }

                var details = accessor.GetShukkaDetail(this.dto.entryEntity.SYSTEM_ID, this.dto.entryEntity.SEQ);
                if (details == null || details.Length < 1)
                {
                    this.dto.detailEntity = new T_SHUKKA_DETAIL[] { };
                }
                else
                {
                    this.dto.detailEntity = details;
                }

                // 検収明細
                var kenshuDetailEntity = this.accessor.GetKenshuDetail(this.dto.entryEntity.SYSTEM_ID, this.dto.entryEntity.SEQ);
                // 複写の場合は検収データを読み込まない
                if (!copyModeCheck())
                {
                    if (kenshuDetailEntity != null && kenshuDetailEntity.Count > 0)
                    {
                        this.dto.kenshuNyuuryokuDto.kenshuDetailList = kenshuDetailEntity;
                    }
                }
                // 取引先請求
                this.dto.torihikisakiSeikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
                var torihikisakiSeikyuu = this.accessor.GetTorihikisakiSeikyuu(this.dto.entryEntity.TORIHIKISAKI_CD);
                if (torihikisakiSeikyuu != null)
                {
                    this.dto.torihikisakiSeikyuuEntity = torihikisakiSeikyuu;
                }

                // 取引先支払
                this.dto.torihikisakiShiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
                var torhikisakiShiharai = this.accessor.GetTorihikisakiShiharai(this.dto.entryEntity.TORIHIKISAKI_CD);
                if (torhikisakiShiharai != null)
                {
                    this.dto.torihikisakiShiharaiEntity = torhikisakiShiharai;
                }

                // 形態区分
                this.dto.keitaiKbnEntity = new M_KEITAI_KBN();
                if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull)
                {
                    var keitaiKbn = this.accessor.GetkeitaiKbn((short)this.dto.entryEntity.KEITAI_KBN_CD, true);
                    if (keitaiKbn != null)
                    {
                        this.dto.keitaiKbnEntity = keitaiKbn;
                    }
                }

                // 拠点
                this.dto.kyotenEntity = new M_KYOTEN();
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    var kyotens = this.accessor.GetAllDataByCodeForKyoten((short)this.dto.entryEntity.KYOTEN_CD);
                    if (kyotens != null && 0 < kyotens.Length)
                    {
                        this.dto.kyotenEntity = kyotens[0];
                    }
                }

                // マニフェスト
                this.dto.manifestEntrys = this.accessor.GetManifestEntry(this.dto.detailEntity);
                // マニフェスト種類
                this.dto.manifestShuruiEntity = new M_MANIFEST_SHURUI();
                if (!this.dto.entryEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    var manifestShurui = this.accessor.GetManifestShurui(this.dto.entryEntity.MANIFEST_SHURUI_CD);
                    if (manifestShurui != null)
                    {
                        this.dto.manifestShuruiEntity = manifestShurui;
                    }
                }
                // マニフェスト手配
                this.dto.manifestTehaiEntity = new M_MANIFEST_TEHAI();
                if (!this.dto.entryEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    var manifestTehai = this.accessor.GetManifestTehai(this.dto.entryEntity.MANIFEST_TEHAI_CD);
                    if (manifestTehai != null)
                    {
                        this.dto.manifestTehaiEntity = manifestTehai;
                    }
                }

                // 在庫管理の場合のみ設定する
                if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
                {
                    // 20150508 在庫データを設定する前に、既存データをクリアする(不具合一覧233) Start
                    this.dto.detailZaikoShukkaDetails = new Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_SHUKKA_DETAIL>>();
                    this.dto.detailZaikoHinmeiHuriwakes = new Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>>();
                    // 20150508 在庫データを設定する前に、既存データをクリアする(不具合一覧233) End

                    // 2次
                    // 在庫
                    foreach (T_SHUKKA_DETAIL detail in details)
                    {
                        // 在庫明細
                        T_ZAIKO_SHUKKA_DETAIL zaikoShukkaDetailEntity = new T_ZAIKO_SHUKKA_DETAIL();
                        zaikoShukkaDetailEntity.SYSTEM_ID = detail.SYSTEM_ID;
                        zaikoShukkaDetailEntity.DETAIL_SYSTEM_ID = detail.DETAIL_SYSTEM_ID;
                        zaikoShukkaDetailEntity.SEQ = detail.SEQ;

                        var zaikoShukkaDetails = this.accessor.GetZaikoShukkaDetails(zaikoShukkaDetailEntity);
                        if (zaikoShukkaDetails != null)
                        {
                            //this.dto.detailZaikoShukkaDetails.Add(zaikoEntitys);
                            this.dto.detailZaikoShukkaDetails[detail] = zaikoShukkaDetails;
                        }

                        // 20150415 在庫品名振分処理追加(修正後のG051からコピー) Start
                        // 在庫品名振分
                        T_ZAIKO_HINMEI_HURIWAKE zaikoHinmeiHuriwakeEntity = new T_ZAIKO_HINMEI_HURIWAKE();
                        zaikoHinmeiHuriwakeEntity.SYSTEM_ID = detail.SYSTEM_ID;
                        zaikoHinmeiHuriwakeEntity.DETAIL_SYSTEM_ID = detail.DETAIL_SYSTEM_ID;
                        zaikoHinmeiHuriwakeEntity.SEQ = detail.SEQ;
                        zaikoHinmeiHuriwakeEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

                        var zaikoHinmeiHuriwakes = this.accessor.GetZaikoHinmeiHuriwakes(zaikoHinmeiHuriwakeEntity);
                        if (zaikoHinmeiHuriwakes != null)
                        {
                            this.dto.detailZaikoHinmeiHuriwakes[detail] = zaikoHinmeiHuriwakes;
                        }
                        // 20150411 在庫品名振分処理追加(修正後のG051からコピー) End
                    }
                }

                this.beforDto = this.dto.Clone();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetAllEntityData", ex1);
                msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetAllEntityData", ex);
                msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }

        }

        #region データ初期化
        /// <summary>
        /// データ初期化処理
        /// </summary>
        private void EntryDataInit()
        {
            // 20150415 在庫単位削除 Start
            // 在庫単位が削除したので、関連処理も削除
            //// 2次
            //// 略称から単位CDを取得しておく
            //M_UNIT DaoEntity = new M_UNIT();
            //DaoEntity.UNIT_NAME_RYAKU = "kg";
            //M_UNIT[] DaoEntities = this.accessor.GetUnitCd(DaoEntity);
            //if (DaoEntities != null)
            //{
            //    if (DaoEntities.Length > 0)
            //    {
            //        this.zaikoUnitCd = String.Format("{0:D2}", int.Parse(DaoEntities[0].UNIT_CD.ToString()));
            //    }
            //}
            // 20150415 在庫単位削除 End

            // DBには無い値などの設定
            denpyouKbnDictionary.Clear();
            youkiDictionary.Clear();
            unitDictionary.Clear();

            var denpyous = this.accessor.GetAllDenpyouKbn();
            var youkis = this.accessor.GetAllYouki();
            var units = this.accessor.GetAllUnit();

            foreach (var denpyou in denpyous)
            {
                denpyouKbnDictionary.Add((short)denpyou.DENPYOU_KBN_CD, denpyou);
            }

            foreach (var youki in youkis)
            {
                youkiDictionary.Add(youki.YOUKI_CD, youki);
            }

            foreach (var unit in units)
            {
                unitDictionary.Add((short)unit.UNIT_CD, unit);
            }

            SqlInt32 renbanValue = -1;
            // 画面毎に設定が異なるコントロールの初期化(コピペしやすいようにするため)
            // 受付番号
            this.form.ENTRY_NUMBER.DBFieldsName = "SHUKKA_NUMBER";
            this.form.ENTRY_NUMBER.ItemDefinedTypes = DB_TYPE.BIGINT.ToTypeString();

            // 連番ラベル、連番
            if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN == SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN_HIRENBAN)
            {
                this.form.RENBAN_LABEL.Text = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.HIRENBAN);
                this.form.RENBAN.DBFieldsName = "DATE_NUMBER";
                this.form.RENBAN.ItemDefinedTypes = DB_TYPE.INT.ToTypeString();
                if (!this.dto.entryEntity.DATE_NUMBER.IsNull)
                {
                    this.form.RENBAN.Text = this.dto.entryEntity.DATE_NUMBER.ToString();
                }
                else
                {
                    this.form.RENBAN.Text = "";
                }
                renbanValue = this.dto.entryEntity.DATE_NUMBER;
            }
            else if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN == SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN_NENRENBAN)
            {
                this.form.RENBAN_LABEL.Text = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.NENRENBAN);
                this.form.RENBAN.DBFieldsName = "YEAR_NUMBER";
                this.form.RENBAN.ItemDefinedTypes = DB_TYPE.INT.ToTypeString();
                if (!this.dto.entryEntity.YEAR_NUMBER.IsNull)
                {
                    this.form.RENBAN.Text = this.dto.entryEntity.YEAR_NUMBER.ToString();
                }
                else
                {
                    this.form.RENBAN.Text = "";
                }
                renbanValue = this.dto.entryEntity.YEAR_NUMBER;
            }

            // 日付系初期値設定
            if (!SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.form.KEIZOKU_NYUURYOKU_VALUE.Text))
            {
                this.form.DENPYOU_DATE.Value = this.footerForm.sysDate;
                this.form.URIAGE_DATE.Value = this.footerForm.sysDate;
                this.form.SHIHARAI_DATE.Value = this.footerForm.sysDate;
            }

            long systemId = -1;
            int seq = -1;

            if (!this.dto.entryEntity.SYSTEM_ID.IsNull) systemId = (long)this.dto.entryEntity.SYSTEM_ID;
            if (!this.dto.entryEntity.SEQ.IsNull) seq = (int)this.dto.entryEntity.SEQ;

            // 締処理状況判定用データ取得
            DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);
            DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);
            T_ZAIKO_SHUKKA_DETAIL zaikoShukkaDetail = this.accessor.GetZaikoShukkaData(systemId, seq);

            // システム設定の確定利用区分による初期表示
            if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN == SalesPaymentConstans.SHUKKA_KAKUTEI_USE_KBN_YES)
            {
                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                {
                    // 確定フラグ
                    this.form.KAKUTEI_KBN_LABEL.Visible = true;
                    this.form.KAKUTEI_KBN.Visible = true;
                    this.form.KAKUTEI_KBN_NAME.Visible = true;

                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = true;
                    this.form.URIAGE_DATE.Visible = true;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = true;
                    this.form.SHIHARAI_DATE.Visible = true;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = true;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = true;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "columnHeaderCell1", "columnHeaderCell22", "columnHeaderCell3" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", false);
                }
                else
                {
                    this.form.KAKUTEI_KBN_LABEL.Visible = false;
                    this.form.KAKUTEI_KBN.Visible = false;
                    this.form.KAKUTEI_KBN_NAME.Visible = false;

                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = false;
                    this.form.URIAGE_DATE.Visible = false;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = false;
                    this.form.SHIHARAI_DATE.Visible = false;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = false;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = false;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "columnHeaderCell1", "columnHeaderCell22", "columnHeaderCell3" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", true);
                }
            }
            else if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN == SalesPaymentConstans.SHUKKA_KAKUTEI_USE_KBN_NO)
            {
                // 確定フラグ
                this.form.KAKUTEI_KBN_LABEL.Visible = false;
                this.form.KAKUTEI_KBN.Visible = false;
                this.form.KAKUTEI_KBN_NAME.Visible = false;

                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = true;
                    this.form.URIAGE_DATE.Visible = true;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = true;
                    this.form.SHIHARAI_DATE.Visible = true;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = true;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = true;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "columnHeaderCell1", "columnHeaderCell22", "columnHeaderCell3" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", false);
                }
                else
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = false;
                    this.form.URIAGE_DATE.Visible = false;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = false;
                    this.form.SHIHARAI_DATE.Visible = false;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = false;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = false;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "columnHeaderCell1", "columnHeaderCell22", "columnHeaderCell3" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", true);

                    // 明細の確定区分のみ改めて非表示
                    this.form.gcMultiRow1.SuspendLayout();
                    var newTemplate = this.form.gcMultiRow1.Template;
                    var obj1 = controlUtil.FindControl(newTemplate.ColumnHeaders[0].Cells.ToArray(), new string[] { "columnHeaderCell1" });
                    foreach (var o in obj1)
                    {
                        PropertyUtility.SetValue(o, "Value", string.Empty);
                    }
                    var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { "KAKUTEI_KBN" });
                    foreach (var o in obj2)
                    {
                        PropertyUtility.SetValue(o, "Visible", false);
                    }
                    this.form.gcMultiRow1.Template = newTemplate;
                    this.form.gcMultiRow1.ResumeLayout();
                }
            }

            // 割振値、調整値の表示形式初期化
            CalcValueFormatSettingInit();

            // 20150417 マニ登録形態区分を明細のレイアウト調整に移動
            //// マニ登録形態区分
            //if (this.dto.sysInfoEntity.SYS_MANI_KEITAI_KBN == SalesPaymentConstans.SYS_MANI_KEITAI_KBN_DENPYOU)
            //{
            //    this.ChangePropertyForGC(new string[] { "columnHeaderCell21" }, new string[] { "MANIFEST_ID" }, "Visible", false);
            //}
            //else
            //{
            //    this.ChangePropertyForGC(new string[] { "columnHeaderCell21" }, new string[] { "MANIFEST_ID" }, "Visible", true);
            //}

            this.ClearEntryData();

            // 領収書番号の初期化
            if (this.dto.sysInfoEntity.SYS_RECEIPT_RENBAN_HOUHOU_KBN == 1)
            {
                this.form.RECEIPT_NUMBER_LABEL.Text = "領収書番号(日連番)";
            }
            else
            {
                this.form.RECEIPT_NUMBER_LABEL.Text = "領収書番号(年連番)";
            }

            // モードによる制御
            if (this.IsRequireData())
            {
                /**
                 * Entry
                 */
                // 拠点
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    headerForm.KYOTEN_CD.Text = this.dto.entryEntity.KYOTEN_CD.ToString().PadLeft(headerForm.KYOTEN_CD.MaxLength, '0');
                }
                if (!string.IsNullOrEmpty(this.dto.kyotenEntity.KYOTEN_NAME_RYAKU))
                {
                    headerForm.KYOTEN_NAME_RYAKU.Text = this.dto.kyotenEntity.KYOTEN_NAME_RYAKU.ToString();
                }

                // 登録者情報
                if (!string.IsNullOrEmpty(this.dto.entryEntity.CREATE_USER))
                {
                    headerForm.CreateUser.Text = this.dto.entryEntity.CREATE_USER;
                }
                if (!this.dto.entryEntity.CREATE_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.CREATE_DATE.ToString()))
                {
                    headerForm.CreateDate.Text = this.dto.entryEntity.CREATE_DATE.ToString();
                }

                // 更新者情報
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UPDATE_USER))
                {
                    headerForm.LastUpdateUser.Text = this.dto.entryEntity.UPDATE_USER;
                }
                if (!this.dto.entryEntity.UPDATE_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.URIAGE_DATE.ToString()))
                {
                    headerForm.LastUpdateDate.Text = this.dto.entryEntity.UPDATE_DATE.ToString();
                }
                // ヘッダー End

                // 詳細 Start
                this.form.ENTRY_NUMBER.Text = this.dto.entryEntity.SHUKKA_NUMBER.ToString();
                // 連番
                if (!renbanValue.IsNull)
                {
                    this.form.RENBAN.Text = renbanValue.ToString();
                }
                else
                {
                    this.form.RENBAN.Text = "";
                }
                // 確定区分
                if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN == SalesPaymentConstans.SHUKKA_KAKUTEI_USE_KBN_YES)
                {
                    // 新規でも複写時は確定区分のセットが必要
                    if (copyModeCheck())
                    {
                        this.form.KAKUTEI_KBN.Text = this.dto.entryEntity.KAKUTEI_KBN.ToString();
                    }
                }

                // 確定区分
                if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN == SalesPaymentConstans.SHUKKA_KAKUTEI_USE_KBN_YES)
                {
                    if (this.dto.entryEntity.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                    {
                        // 確定名
                        this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI);
                    }
                    else if (this.dto.entryEntity.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI)
                    {
                        // 確定名
                        this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI);
                    }
                }

                if (!WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                {
                    // 受付番号
                    if (!this.dto.entryEntity.UKETSUKE_NUMBER.IsNull)
                    {
                        this.form.UKETSUKE_NUMBER.Text = this.dto.entryEntity.UKETSUKE_NUMBER.ToString();
                    }

                    // 計量番号
                    if (!this.dto.entryEntity.KEIRYOU_NUMBER.IsNull)
                    {
                        this.form.KEIRYOU_NUMBER.Text = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                    }
                }

                // 伝票日付
                if (!this.dto.entryEntity.DENPYOU_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_DATE.ToString()))
                {
                    this.form.DENPYOU_DATE.Text = this.dto.entryEntity.DENPYOU_DATE.ToString();
                }

                // 入力担当者
                if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.form.WindowType || WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType)
                {
                    if (CommonShogunData.LOGIN_USER_INFO != null
                        && !string.IsNullOrEmpty(CommonShogunData.LOGIN_USER_INFO.SHAIN_CD)
                        && CommonShogunData.LOGIN_USER_INFO.NYUURYOKU_TANTOU_KBN)
                    {
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();
                        strNyuryokuTantousyaName = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME.ToString();    // No.3279
                    }
                    else
                    {
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                        strNyuryokuTantousyaName = string.Empty;  // No.3279
                    }
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_CD.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD;
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
                    strNyuryokuTantousyaName = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
                }

                // 売上日付
                if (!this.dto.entryEntity.URIAGE_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.URIAGE_DATE.ToString()))
                {
                    this.form.URIAGE_DATE.Value = (DateTime)this.dto.entryEntity.URIAGE_DATE;
                }
                else
                {
                    this.form.URIAGE_DATE.Value = string.Empty;
                }

                // 支払日付
                if (!this.dto.entryEntity.SHIHARAI_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.SHIHARAI_DATE.ToString()))
                {
                    this.form.SHIHARAI_DATE.Value = (DateTime)this.dto.entryEntity.SHIHARAI_DATE;
                }
                else
                {
                    this.form.SHIHARAI_DATE.Value = string.Empty;
                }

                // 売上消費税
                if (!this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE.IsNull)
                {
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE.ToString();
                }

                // 支払消費税
                if (!this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE.IsNull)
                {
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE.ToString();
                }

                // 車輌
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHARYOU_CD))
                {
                    this.form.SHARYOU_CD.Text = this.dto.entryEntity.SHARYOU_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHARYOU_NAME))
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = this.dto.entryEntity.SHARYOU_NAME.ToString();
                }

                // 取引先
                if (!string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_CD))
                {
                    this.form.TORIHIKISAKI_CD.Text = this.dto.entryEntity.TORIHIKISAKI_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_NAME))
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.dto.entryEntity.TORIHIKISAKI_NAME.ToString();
                }
                // 車種
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHASHU_CD))
                {
                    this.form.SHASHU_CD.Text = this.dto.entryEntity.SHASHU_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHASHU_NAME))
                {
                    this.form.SHASHU_NAME.Text = this.dto.entryEntity.SHASHU_NAME.ToString();
                }
                // 売上締日
                if (!this.dto.torihikisakiSeikyuuEntity.SHIMEBI1.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI1.Text = this.dto.torihikisakiSeikyuuEntity.SHIMEBI1.ToString();
                }
                if (!this.dto.torihikisakiSeikyuuEntity.SHIMEBI2.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI2.Text = this.dto.torihikisakiSeikyuuEntity.SHIMEBI2.ToString();
                }
                if (!this.dto.torihikisakiSeikyuuEntity.SHIMEBI3.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI3.Text = this.dto.torihikisakiSeikyuuEntity.SHIMEBI3.ToString();
                }
                // 支払締日
                if (!this.dto.torihikisakiShiharaiEntity.SHIMEBI1.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI1.Text = this.dto.torihikisakiShiharaiEntity.SHIMEBI1.ToString();
                }
                if (!this.dto.torihikisakiShiharaiEntity.SHIMEBI2.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = this.dto.torihikisakiShiharaiEntity.SHIMEBI2.ToString();
                }
                if (!this.dto.torihikisakiShiharaiEntity.SHIMEBI3.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = this.dto.torihikisakiShiharaiEntity.SHIMEBI3.ToString();
                }
                // 運搬業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNPAN_GYOUSHA_CD))
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = this.dto.entryEntity.UNPAN_GYOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNPAN_GYOUSHA_NAME))
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = this.dto.entryEntity.UNPAN_GYOUSHA_NAME.ToString();
                }
                // 業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_CD))
                {
                    this.form.GYOUSHA_CD.Text = this.dto.entryEntity.GYOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_NAME))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = this.dto.entryEntity.GYOUSHA_NAME.ToString();
                }
                // 運転者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNTENSHA_CD))
                {
                    this.form.UNTENSHA_CD.Text = this.dto.entryEntity.UNTENSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNTENSHA_NAME))
                {
                    this.form.UNTENSHA_NAME.Text = this.dto.entryEntity.UNTENSHA_NAME.ToString();
                }
                // 人数
                if (!this.dto.entryEntity.NINZUU_CNT.IsNull)
                {
                    this.form.NINZUU_CNT.Text = this.dto.entryEntity.NINZUU_CNT.ToString();

                }
                // 現場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GENBA_CD))
                {
                    this.form.GENBA_CD.Text = this.dto.entryEntity.GENBA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GENBA_NAME))
                {
                    this.form.GENBA_NAME_RYAKU.Text = this.dto.entryEntity.GENBA_NAME.ToString();
                }
                strGenbaName = "";//クリア
                if (this.form.GYOUSHA_CD.Text != "" && this.form.GENBA_CD.Text != "")
                {
                    //印刷用現場名
                    bool catchErr = false;
                    var genbaEntity = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr) { throw new Exception(""); }
                    if (genbaEntity != null)
                    {
                        strGenbaName = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;
                    }
                }

                // No.3875-->
                this.form.KUUSHA_JYURYO.Text = string.Empty;
                M_SHARYOU[] sharyouEntitys = null;
                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END
                if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                {
                    if (!sharyouEntitys[0].KUUSHA_JYURYO.IsNull)
                    {
                        this.form.KUUSHA_JYURYO.Text = sharyouEntitys[0].KUUSHA_JYURYO.ToString();
                    }
                }
                // No.3875<--

                // 形態区分
                if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull)
                {
                    this.form.KEITAI_KBN_CD.Text = this.dto.entryEntity.KEITAI_KBN_CD.ToString().PadLeft(this.form.KEITAI_KBN_CD.MaxLength, '0');
                }
                // 形態区分名
                if (!string.IsNullOrEmpty(this.dto.keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU))
                {
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = this.dto.keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU;
                }
                // 台貫
                if (!this.dto.entryEntity.DAIKAN_KBN.IsNull)
                {
                    this.form.DAIKAN_KBN.Text = this.dto.entryEntity.DAIKAN_KBN.ToString();
                }
                this.form.DAIKAN_KBN_NAME.Text = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(this.form.DAIKAN_KBN.Text.ToString()));
                // 荷積業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GYOUSHA_CD))
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GYOUSHA_NAME))
                {
                    this.form.NIZUMI_GYOUSHA_NAME.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_NAME.ToString();
                }

                // 荷積現場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GENBA_CD))
                {
                    this.form.NIZUMI_GENBA_CD.Text = this.dto.entryEntity.NIZUMI_GENBA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GENBA_NAME))
                {
                    this.form.NIZUMI_GENBA_NAME.Text = this.dto.entryEntity.NIZUMI_GENBA_NAME.ToString();
                }
                // マニフェスト種類
                if (!this.dto.entryEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    this.form.MANIFEST_SHURUI_CD.Text = this.dto.entryEntity.MANIFEST_SHURUI_CD.ToString().PadLeft(this.form.MANIFEST_SHURUI_CD.MaxLength, '0');
                }
                if (!string.IsNullOrEmpty(this.dto.manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU))
                {
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = this.dto.manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU.ToString();
                }
                // マニフェスト手配
                if (!this.dto.entryEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    this.form.MANIFEST_TEHAI_CD.Text = this.dto.entryEntity.MANIFEST_TEHAI_CD.ToString().PadLeft(this.form.MANIFEST_TEHAI_CD.MaxLength, '0');
                }
                if (!string.IsNullOrEmpty(this.dto.manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                {
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = this.dto.manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU.ToString();
                }
                // 営業担当者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.EIGYOU_TANTOUSHA_CD))
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = this.dto.entryEntity.EIGYOU_TANTOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME))
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME.ToString();
                }
                // 伝票備考
                if (!string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_BIKOU))
                {
                    this.form.DENPYOU_BIKOU.Text = this.dto.entryEntity.DENPYOU_BIKOU.ToString();
                }
                // 滞留備考
                if (!string.IsNullOrEmpty(this.dto.entryEntity.TAIRYUU_BIKOU))
                {
                    this.form.TAIRYUU_BIKOU.Text = this.dto.entryEntity.TAIRYUU_BIKOU.ToString();

                }
                // 締処理状況(売上)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                    {
                        this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = SalesPaymentConstans.MISHIME;
                    }
                    else
                    {
                        this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = SalesPaymentConstans.SHIMEZUMI;
                    }
                }
                else
                {
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = SalesPaymentConstans.MISHIME;
                }

                // 締処理状況(支払)
                if (seisanData != null && 0 < seisanData.Rows.Count)
                {
                    if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                    {
                        this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = SalesPaymentConstans.MISHIME;
                    }
                    else
                    {
                        this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = SalesPaymentConstans.SHIMEZUMI;
                    }
                }
                else
                {
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = SalesPaymentConstans.MISHIME;
                }

                // 領収書番号(日連番)
                if (!this.dto.entryEntity.RECEIPT_NUMBER.IsNull)
                {
                    this.form.RECEIPT_NUMBER_DAY.Text = this.dto.entryEntity.RECEIPT_NUMBER.ToString();
                }
                // 領収書番号(年連番)
                if (!this.dto.entryEntity.RECEIPT_NUMBER_YEAR.IsNull)
                {
                    this.form.RECEIPT_NUMBER_YEAR.Text = this.dto.entryEntity.RECEIPT_NUMBER_YEAR.ToString();
                }
                // 領収書番号(表示用)
                if (this.dto.sysInfoEntity.SYS_RECEIPT_RENBAN_HOUHOU_KBN == 1)
                {
                    this.form.RECEIPT_NUMBER.Text = this.form.RECEIPT_NUMBER_DAY.Text;
                }
                else
                {
                    this.form.RECEIPT_NUMBER.Text = this.form.RECEIPT_NUMBER_YEAR.Text;
                }

                // 正味合計
                if (!this.dto.entryEntity.NET_TOTAL.IsNull)
                {
                    this.form.NET_TOTAL.Text = this.dto.entryEntity.NET_TOTAL.ToString();
                    CustomTextBoxLogic customTextBoxLogic = new CustomTextBoxLogic(this.form.NET_TOTAL);
                    customTextBoxLogic.Format(this.form.NET_TOTAL);
                }

                // 画面に表示されない品名別金額を算出
                decimal hinmeiUriageKingakuTotal = 0;
                decimal hinmeiShiharaiKingakuTotal = 0;

                if (!this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.IsNull)
                {
                    hinmeiUriageKingakuTotal = (decimal)this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL;
                }
                if (!this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.IsNull)
                {
                    hinmeiShiharaiKingakuTotal = (decimal)this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL;
                }

                // 差引額計算用
                decimal uriageKingakuTotal = 0;
                decimal shiharaiKingakuTotal = 0;

                // 売上金額合計
                if (!this.dto.entryEntity.URIAGE_AMOUNT_TOTAL.IsNull)
                {
                    this.form.URIAGE_KINGAKU_TOTAL.Text =
                        CommonCalc.DecimalFormat((decimal)this.dto.entryEntity.URIAGE_AMOUNT_TOTAL + hinmeiUriageKingakuTotal);
                    // 差額計算用
                    uriageKingakuTotal =
                        (decimal)this.dto.entryEntity.URIAGE_AMOUNT_TOTAL + hinmeiUriageKingakuTotal;
                }
                // 支払金額合計
                if (!this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL.IsNull)
                {
                    this.form.SHIHARAI_KINGAKU_TOTAL.Text =
                        CommonCalc.DecimalFormat((decimal)this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL + hinmeiShiharaiKingakuTotal);
                    // 差額計算用
                    shiharaiKingakuTotal =
                        (decimal)this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL + hinmeiShiharaiKingakuTotal;
                }
                // 差額
                if (this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN == SalesPaymentConstans.SHUKKA_CALC_BASE_KBN_URIAGE)
                {
                    this.form.SAGAKU.Text = CommonCalc.DecimalFormat(uriageKingakuTotal - shiharaiKingakuTotal);
                }
                else if (this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN == SalesPaymentConstans.SHUKKA_CALC_BASE_KBN_SHIHARAI)
                {
                    this.form.SAGAKU.Text = CommonCalc.DecimalFormat(shiharaiKingakuTotal - uriageKingakuTotal);
                }

                // 排他制御用
                this.form.ENTRY_TIME_STAMP.Text = ConvertStrByte.ByteToString(this.dto.entryEntity.TIME_STAMP);

                //2次
                //取引区分(売)
                if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD == 1)
                {
                    //1.現金
                    this.form.txtUri.Text = "現金";
                }
                else if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD == 2)
                {
                    //2.掛け
                    this.form.txtUri.Text = "掛け";
                }
                else
                {
                    this.form.txtUri.Text = "";
                }
                //取引区分(支)
                if (this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD == 1)
                {
                    //1.現金
                    this.form.txtShi.Text = "現金";
                }
                else if (this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD == 2)
                {
                    //2.掛け
                    this.form.txtShi.Text = "掛け";
                }
                else
                {
                    this.form.txtShi.Text = "";
                }

                // 締処理状況（在庫）
                //if (zaikoData != null) // No.2198
                if (zaikoShukkaDetail != null && zaikoShukkaDetail.DELETE_FLG.IsFalse)    // No.2198
                {
                    if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                    {
                        this.form.txtShimeZaiko.Text = SalesPaymentConstans.MISHIME;
                    }
                    else
                    {
                        this.form.txtShimeZaiko.Text = SalesPaymentConstans.SHIMEZUMI;
                    }
                }
                else
                {
                    this.form.txtShimeZaiko.Text = SalesPaymentConstans.MISHIME;
                }

                // 要検収
                if (!this.dto.entryEntity.KENSHU_MUST_KBN.IsNull)
                {
                    this.form.KENSHU_MUST_KBN.Checked = this.dto.entryEntity.KENSHU_MUST_KBN.Value;
                }

                //検収状況セット
                this.SetKenshuDetail();

                // 詳細 End

                /**
                 * Detail
                 */
                // テンプレートをいじる処理は、データ設定前に実行
                this.ExecuteAlignmentForDetail();

                this.form.gcMultiRow1.BeginEdit(false);
                this.form.gcMultiRow1.Rows.Clear();
                // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
                // Entity数Rowを作ると最終行が無いので、Etity + 1でループさせる
                for (int i = 1; i < this.dto.detailEntity.Length + 1; i++)
                {
                    this.form.gcMultiRow1.Rows.Add();
                }
                shukkaDetailDataBinder.CreateDataTableForEntity(this.form.gcMultiRow1, this.dto.detailEntity);

                // MultiRowへ設定
                // Dictionary関連修正
                // 行単位で在庫明細Dictionaryにセットする
                this.dto.rowZaikoShukkaDetails = new Dictionary<Row, List<T_ZAIKO_SHUKKA_DETAIL>>();
                // 20150508 在庫データを設定する前に、既存データをクリアする(不具合一覧233) Start
                this.dto.rowZaikoHinmeiHuriwakes = new Dictionary<Row, List<T_ZAIKO_HINMEI_HURIWAKE>>();
                // 20150508 在庫データを設定する前に、既存データをクリアする(不具合一覧233) End

                int k = 0;
                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    short denpyouCd = 0;
                    ICustomControl denpyouCdCell = (ICustomControl)row.Cells[CELL_NAME_DENPYOU_KBN_CD];
                    if (short.TryParse(denpyouCdCell.GetResultText(), out denpyouCd)
                        && denpyouKbnDictionary.ContainsKey(denpyouCd))
                    {
                        row.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[denpyouCd].DENPYOU_KBN_NAME_RYAKU;
                    }

                    ICustomControl youkiCdCell = (ICustomControl)row.Cells[CELL_NAME_YOUKI_CD];
                    if (!string.IsNullOrEmpty(youkiCdCell.GetResultText())
                        && youkiDictionary.ContainsKey(youkiCdCell.GetResultText()))
                    {
                        row.Cells[CELL_NAME_YOUKI_NAME_RYAKU].Value = youkiDictionary[youkiCdCell.GetResultText()].YOUKI_NAME_RYAKU;
                    }

                    short unitCd = 0;
                    ICustomControl unitCdCell = (ICustomControl)row.Cells[CELL_NAME_UNIT_CD];
                    if (short.TryParse(unitCdCell.GetResultText(), out unitCd)
                        && unitDictionary.ContainsKey(unitCd))
                    {
                        row.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = unitDictionary[unitCd].UNIT_NAME_RYAKU;
                    }

                    // マニフェスト.交付番号
                    if (!WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                    {
                        if (row.Cells[CELL_NAME_SYSTEM_ID].Value != null
                             && row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value != null)
                        {
                            string whereStrForMani = "RENKEI_DENSHU_KBN_CD = " + SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA + " AND RENKEI_SYSTEM_ID = " + row.Cells[CELL_NAME_SYSTEM_ID].Value + " AND RENKEI_MEISAI_SYSTEM_ID = " + row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value;
                            var manifestEntry = this.dto.manifestEntrys.Select(whereStrForMani);
                            if (manifestEntry != null && 0 < manifestEntry.Length)
                            {
                                // 一件しか取れないはずなので、最初の要素を取得
                                row.Cells[CELL_NAME_MANIFEST_ID].Value = manifestEntry[0][CELL_NAME_MANIFEST_ID];
                            }
                        }
                    }

                    if (k < this.dto.detailEntity.Length)
                    {
                        T_SHUKKA_DETAIL detail = this.dto.detailEntity[k];
                        // 金額
                        if (detail != null && !detail.KINGAKU.IsNull && !detail.HINMEI_KINGAKU.IsNull)
                        {
                            row.Cells[CELL_NAME_KINGAKU].Value = detail.KINGAKU.Value + detail.HINMEI_KINGAKU.Value;
                        }
                        else
                        {
                            row.Cells[CELL_NAME_KINGAKU].Value = null;
                        }

                        // 確定区分
                        if (detail.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                        {
                            row.Cells[CELL_NAME_KAKUTEI_KBN].Value = true;
                        }
                        else
                        {
                            row.Cells[CELL_NAME_KAKUTEI_KBN].Value = false;
                        }

                        //2次
                        //荷姿単位名
                        short nisugataCd = 0;
                        ICustomControl NisugataUnitCdCell = (ICustomControl)row.Cells[CELL_NAME_NISUGATA_UNIT_CD];
                        if (short.TryParse(NisugataUnitCdCell.GetResultText(), out nisugataCd)
                            && unitDictionary.ContainsKey(nisugataCd))
                        {
                            row.Cells[CELL_NAME_NISUGATA_NAME_RYAKU].Value = unitDictionary[nisugataCd].UNIT_NAME_RYAKU;
                        }

                        // No.4578-->
                        // 20150415 go 在庫情報取得と設定修正(修正後のG051のように改修) Start
                        //在庫情報取得
                        if (row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value != null)
                        {
                            // 在庫明細
                            //T_ZAIKO_SHUKKA_DETAIL newEntity = new T_ZAIKO_SHUKKA_DETAIL();
                            List<T_ZAIKO_SHUKKA_DETAIL> zaikoEntity = this.dto.GetZaikoShukkaListByDetail(
                                SqlInt64.Parse(systemId.ToString()),
                                SqlInt64.Parse(row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value.ToString()),
                                SqlInt32.Parse(seq.ToString()));
                            //int zaikoCount = 0;
                            //if (zaikoEntity != null)
                            //{
                            //    // Dictionary関連修正
                            //    // 行単位で在庫明細Dictionaryにセットする
                            //    this.dto.rowZaikoShukkaDetails.Add(row, zaikoEntity);
                            //    foreach (T_ZAIKO_SHUKKA_DETAIL entity in zaikoEntity)
                            //    {
                            //        newEntity = entity;
                            //        zaikoCount += 1;
                            //    }
                            //}
                            //else
                            //{
                            //    // Dictionary関連修正
                            //    // 在庫明細データがなければ空のListをDictionaryにセットしておく
                            //    this.dto.rowZaikoShukkaDetails.Add(row, new List<T_ZAIKO_SHUKKA_DETAIL>());
                            //}
                            this.dto.rowZaikoShukkaDetails[row] =
                                zaikoEntity != null ? zaikoEntity : new List<T_ZAIKO_SHUKKA_DETAIL>();

                            ////複数在庫明細
                            //if (zaikoEntity != null && zaikoCount > 1)
                            //{
                            //    //金額を合計
                            //    SqlDecimal goukeiKingaku = 0;
                            //    foreach (T_ZAIKO_SHUKKA_DETAIL entity in zaikoEntity)
                            //    {
                            //        if (!entity.KINGAKU.Equals(SqlDecimal.Null))
                            //        {
                            //            goukeiKingaku += entity.KINGAKU;
                            //        }
                            //    }
                            //    this.MultiZaikoKakunou(row, goukeiKingaku);
                            //}
                            //else if (zaikoEntity != null && zaikoCount == 1)
                            //{
                            //    //単体在庫明細
                            //    this.SimpleZaikoKakunou(row, newEntity);
                            //}

                            // 在庫品名振分
                            List<T_ZAIKO_HINMEI_HURIWAKE> zaikoHinmeiHuriwakes = this.dto.GetZaikoHinmeiHuriwakeListByDetail(
                                SqlInt64.Parse(systemId.ToString()),
                                SqlInt64.Parse(row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value.ToString()),
                                SqlInt32.Parse(seq.ToString()));
                            this.dto.rowZaikoHinmeiHuriwakes[row] =
                                zaikoHinmeiHuriwakes != null ? zaikoHinmeiHuriwakes : new List<T_ZAIKO_HINMEI_HURIWAKE>();
                            // 在庫品名格納
                            if (!this.ZaikoHinmeiKakunou(row))
                            {
                                throw new Exception("");
                            }
                        }
                        // 20150415 go 在庫情報取得と設定修正(修正後のG051のように改修) End
                        // No.4578<--
                    }

                    // 締処理状況設定
                    string whereStr = string.Empty;
                    whereStr = "DETAIL_SYSTEM_ID = " + row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value;
                    DataRow[] shimeDetails = null;

                    if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == denpyouCd)
                    {
                        shimeDetails = seikyuuData.Select(whereStr);
                        if (shimeDetails != null && 0 < shimeDetails.Length)
                        {
                            row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.SHIMEZUMI_DETAIL;
                        }
                        else
                        {
                            row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.MISHIME_DETAIL;
                        }
                    }
                    else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == denpyouCd)
                    {
                        shimeDetails = seisanData.Select(whereStr);
                        if (shimeDetails != null && 0 < shimeDetails.Length)
                        {
                            row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.SHIMEZUMI_DETAIL;
                        }
                        else
                        {
                            row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.MISHIME_DETAIL;
                        }
                    }

                    // 単位kgの品名数量設定
                    if (!SetHinmeiSuuryou(LogicClass.CELL_NAME_UNIT_CD, row, false))
                    {
                        throw new Exception("");
                    }

                    k++;
                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(this.form.gcMultiRow1);

                this.SetJyuuryouDataToDtoList();    // 重量値系同期のためのデータをセット
            }
            else
            {
                // 新規モードの初期化処理
                this.ChangePropertyForGC(new string[] { "" },
                    new string[] { CELL_NAME_WARIFURI_JYUURYOU, CELL_NAME_CHOUSEI_JYUURYOU, CELL_NAME_WARIFURI_PERCENT, CELL_NAME_CHOUSEI_PERCENT },
                    "Readonly", true);

                // No.4089-->
                this.form.KAKUTEI_KBN.Text = this.dto.sysInfoEntity.SHUKKA_KAKUTEI_FLAG.ToString();
                this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(Int16.Parse(this.form.KAKUTEI_KBN.Text));
                // No.4089<--
            }

            //if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.ShukkaNumber != -1)     // No.2334
            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.ShukkaNumber != -1 && this.form.TairyuuNewFlg == false)   // No.2334
            {
                // 複写モード（新規モード、受入番号あり）の初期化処理
                // 受入番号
                this.form.ENTRY_NUMBER.Text = "";
                // 日付連番
                this.form.RENBAN.Text = "";
                // 計量番号
                this.form.KEIRYOU_NUMBER.Text = "";
                // 日付系初期値設定
                this.form.DENPYOU_DATE.Value = this.footerForm.sysDate;
                this.form.URIAGE_DATE.Value = this.footerForm.sysDate;
                this.form.SHIHARAI_DATE.Value = this.footerForm.sysDate;
                // 取引先チェック及びＤＴＯセット
                // 20160120 chenzz 12114の不具合一覧についての修正(販売管理(入力)no.31) start
                string torihikisakiNmae = this.dto.entryEntity.TORIHIKISAKI_NAME;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiNmae;
                // 20160120 chenzz 12114の不具合一覧についての修正(販売管理(入力)no.31) end
                // 領収書番号
                this.form.RECEIPT_NUMBER.Text = string.Empty;
                this.form.RECEIPT_NUMBER_DAY.Text = string.Empty;
                this.form.RECEIPT_NUMBER_YEAR.Text = string.Empty;
                // 検収状況
                this.form.txtKensyuu.Text = string.Empty;
                // 要検収を有効とする
                this.form.KENSHU_MUST_KBN.Enabled = true;
            }

            // 在庫明細のDictionaryに最初の1行の分を作成
            AddRowDic(this.form.gcMultiRow1.Rows.Count() - 1);

            //ThangNguyen [Add] 20150826 #10907 Start
            this.CheckTorihikisakiShokuchi();
            this.CheckGyoushaShokuchi();
            this.CheckGenbaShokuchi();
            this.CheckNizumiGyoushaShokuchi();
            this.CheckNizumiGenbaShokuchi();
            this.CheckUpanGyoushaShokuchi();
            //ThangNguyen [Add] 20150826 #10907 End
        }
        #endregion

        /// <summary>
        /// 重量取込処理
        /// </summary>
        public bool SetJyuuryou(bool WeightDisplaySwitch)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 重量取込処理中にグローバル変数が変わる可能性があるのでローカル変数に待避
                string beforCtrlName = this.form.beforControlName;

                if ((!this.headerForm.label1.Visible && WeightDisplaySwitch)
                    || string.IsNullOrEmpty(this.headerForm.label1.Text))
                {
                    return true;
                }

                if (this.form.gcMultiRow1 == null
                    || this.form.gcMultiRow1.Rows == null
                    || this.form.gcMultiRow1.RowCount < 1)
                {
                    return true;
                }

                string emptyJyuuryouOfPreviousRow = string.Empty;
                this.form.gcMultiRow1.Focus();
                this.form.gcMultiRow1.BeginEdit(false);

                // フォーカス位置を保持
                var seveRowFocus = 0;
                var seveCellFocus = 0;
                foreach (var cell in this.form.gcMultiRow1.SelectedCells)
                {
                    seveRowFocus = cell.RowIndex;
                    seveCellFocus = cell.CellIndex;

                    if (this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Visible && this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Selectable)
                    {
                        break;
                    }
                }

                // #19192の対応
                if (this.form == null || this.form.IsDisposed || this.form.gcMultiRow1 == null)
                {
                    // [F1]と[F9]を同時に押したとき、既にformが消えているのに以降の処理が実行されるため
                    // formの生存確認を実行。
                    return true;
                }

                // フォーカスを品名CDにいったん退避する→単価更新されてしまうので、明細備考にフォーカスを退避する
                this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(this.form.gcMultiRow1.CurrentRow.Index, CELL_NAME_MEISAI_BIKOU);

                if (this.form.KeizokuKeiryouFlg)
                {
                    Row row = null;

                    // 滞留登録一覧で継続計量を設定して修正モードで開いた場合
                    // 継続計量でも、初回の一度しかここを通さない

                    // 最終行を取得
                    for (int i = 0; i < this.form.gcMultiRow1.RowCount; i++)
                    {
                        row = this.form.gcMultiRow1.Rows[i];

                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value))
                                || !string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value)))
                        {
                            // 割り振りが入っている行は省く
                            continue;
                        }

                        // Rowsの後ろからチェック
                        if (row.IsNewRow)
                        {
                            this.form.gcMultiRow1.Rows.Add();
                            // indexがずれるので再取得
                            row = this.form.gcMultiRow1.Rows[i];
                        }

                        // 最後の空車重量がない場合
                        if (string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                            && !string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            this.ChangeTenyuuryoku(row, false);
                            break;
                        }
                        else
                        {
                            // 総重・空車・正味・割振のない行の場合
                            if (string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value))
                                )
                            {
                                // 全て空白の場合はそのままその行に登録
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows.Add();
                                row = this.form.gcMultiRow1.Rows[i + 1];    // indexがずれるので再取得
                            }

                            row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            break;
                        }
                    }

                    if (row != null)
                    {
                        //this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_MEISAI_BIKOU);
                        Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                        this.CalcStackOrEmptyJyuuryou(targetRow);
                        if (!this.CalcDetaiKingaku(row))
                        {
                            throw new Exception("");
                        }
                    }

                    this.form.KeizokuKeiryouFlg = true;

                }
                else
                {
                    for (int i = 0; i < this.form.gcMultiRow1.RowCount; i++)
                    {
                        Row row = this.form.gcMultiRow1.Rows[i];

                        if (row.IsNewRow)
                        {
                            this.form.gcMultiRow1.Rows.Add();
                            // indexがずれるので再取得
                            row = this.form.gcMultiRow1.Rows[i];
                        }

                        // 総重量、空車重量のどちらにも値有り
                        if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value)))
                        {
                            // 次行の総重量にセットするための変数
                            emptyJyuuryouOfPreviousRow = row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString();
                        }

                        // RowNo再振り
                        if (!this.NumberingRowNo())
                        {
                            return false;
                        }

                        // 割振りがある行は省く
                        if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_WARIFURI_JYUURYOU].Value))
                            || !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_WARIFURI_PERCENT].Value)))
                        {
                            continue;
                        }

                        // 総重量、空車重量の両方が無い場合、
                        if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_NET_JYUURYOU].Value)))
                            {
                                continue;
                            }

                            if (string.IsNullOrEmpty(emptyJyuuryouOfPreviousRow))
                            {
                                // 重量値取り込み
                                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            }
                            else
                            {
                                // 重量値取り込み
                                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = emptyJyuuryouOfPreviousRow;
                                row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                                this.ChangeTenyuuryoku(row, false);
                            }
                            // 正味重量、金額計算
                            Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                            this.CalcStackOrEmptyJyuuryou(targetRow);
                            if (!this.CalcDetaiKingaku(row))
                            {
                                throw new Exception("");
                            }
                            break;
                        }

                        // 総重量のみ値有り
                        if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            this.ChangeTenyuuryoku(row, false);
                            // 正味重量、金額計算
                            Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                            this.CalcStackOrEmptyJyuuryou(targetRow);
                            if (!this.CalcDetaiKingaku(row))
                            {
                                throw new Exception("");
                            }
                            break;
                        }

                        // 空車重量のみ値有り
                        if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            continue;
                        }
                    }
                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

                // 次の項目へフォーカスを移動
                //if (this.form.gcMultiRow1.SelectedCells.Count.Equals(0))
                //{
                //    this.form.gcMultiRow1.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Cells.Cast<DataGridViewCell>().ToList().ForEach(c => seveCellFocus = c.ColumnIndex));
                //}
                if (beforCtrlName == this.form.gcMultiRow1.Name)
                {
                    if (this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Visible)
                    {
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(seveRowFocus, seveCellFocus);
                    }
                }
                else if (this.form.Contains(this.form.Controls[beforCtrlName]))
                {
                    // Formの処理前のコントロールにフォーカスを戻す
                    if (!string.IsNullOrEmpty(beforCtrlName))
                    {
                        this.form.Controls[beforCtrlName].Focus();
                    }
                }
                else if (this.headerForm.Contains(this.headerForm.Controls[beforCtrlName]))
                {
                    // HeaderFormの処理前のコントロールにフォーカスを戻す
                    if (!string.IsNullOrEmpty(beforCtrlName))
                    {
                        this.headerForm.Controls[beforCtrlName].Focus();
                    }
                }
                ret = true;

            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("SetJyuuryou", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetJyuuryou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 数値の表示形式初期化（システム設定に基づき）
        /// </summary>
        internal void CalcValueFormatSettingInit()
        {
            LogUtility.DebugMethodStart();

            this.form.gcMultiRow1.SuspendLayout();
            var newTemplate = this.form.gcMultiRow1.Template;

            // 割振割合(%)
            var obj1 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_WARIFURI_PERCENT });
            string FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
            foreach (var o in obj1)
            {
                PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
            }

            // 割振値
            var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_WARIFURI_JYUURYOU });
            FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA);
            foreach (var o in obj2)
            {
                PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
            }

            // 調整割合(%)
            var obj3 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_PERCENT });
            FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_CD,
                (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_KETA);
            foreach (var o in obj3)
            {
                PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
            }

            // 調整値
            var obj4 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_JYUURYOU });
            FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_CD,
                (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_KETA);
            foreach (var o in obj4)
            {
                PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
            }

            // 単価
            var obj5 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_TANKA });
            int TankaFormatCd = (int)this.dto.sysInfoEntity.SYS_TANKA_FORMAT_CD;
            foreach (var o in obj5)
            {
                if ((SysTankaFormatCd)TankaFormatCd == SysTankaFormatCd.BLANK || (SysTankaFormatCd)TankaFormatCd == SysTankaFormatCd.NONE)
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '-' });
                }
                else
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', '-' });
                }
            }

            // 数量
            var obj6 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_SUURYOU });
            int SuuryouFormatCd = (int)this.dto.sysInfoEntity.SYS_SUURYOU_FORMAT_CD;
            foreach (var o in obj6)
            {
                if ((SysSuuryouFormatCd)SuuryouFormatCd == SysSuuryouFormatCd.BLANK || (SysSuuryouFormatCd)SuuryouFormatCd == SysSuuryouFormatCd.NONE)
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '-' });
                }
                else
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', '-' });
                }
            }

            // 荷姿数量
            var obj7 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_NISUGATA_SUURYOU });
            SuuryouFormatCd = (int)this.dto.sysInfoEntity.SYS_SUURYOU_FORMAT_CD;
            foreach (var o in obj7)
            {
                if ((SysSuuryouFormatCd)SuuryouFormatCd == SysSuuryouFormatCd.BLANK || (SysSuuryouFormatCd)SuuryouFormatCd == SysSuuryouFormatCd.NONE)
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '-' });
                }
                else
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', '-' });
                }
            }

            this.form.gcMultiRow1.Template = newTemplate;
            this.form.gcMultiRow1.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 小数点以下の端数の表示形式を編集
        /// </summary>
        /// <param name="calcCD"></param>
        /// <param name="hasuKeta"></param>
        /// <returns></returns>
        internal string SetFormat(int calcCD, short hasuKeta)
        {
            LogUtility.DebugMethodStart(calcCD, hasuKeta);
            string returnValue = "#,##0";
            int hasuKetaExe = hasuKeta - 2;

            if (hasuKetaExe > 0
                && ((fractionType)calcCD == fractionType.CEILING || (fractionType)calcCD == fractionType.FLOOR || (fractionType)calcCD == fractionType.ROUND))
            {
                returnValue = returnValue + ".";
                if (hasuKetaExe > 1)
                {
                    for (int i = 1; i <= hasuKetaExe - 1; ++i)
                    {
                        returnValue = returnValue + "0";
                    }
                }
                returnValue = returnValue + "0";    // No.3443
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }
        #endregion

        #region 業務処理

        /// <summary>
        /// Entity作成と登録処理
        /// </summary>
        /// <param name="taiyuuKbn">滞留登録区分</param>
        /// <param name="errorFlag"></param>
        /// <returns>true:成功, false:失敗</returns>
        public bool CreateEntityAndUpdateTables(bool taiyuuKbn, bool errorFlag, out bool catchErr)
        {
            catchErr = false;
            try
            {
                var uketsukeExist = false;
                if (null != this.tUketsukeSkEntry)
                {
                    // 出荷受付の更新前にデータが重複していないかチェックを行う
                    var systemId = this.tUketsukeSkEntry.SYSTEM_ID.ToString();
                    var checkEntity = this.accessor.GetUketsukeSkEntry(systemId);
                    if (checkEntity == null || (this.tUketsukeSkEntry.SEQ != checkEntity.SEQ))
                    {
                        // 重複していた場合は登録を行わない
                        uketsukeExist = true;
                    }
                }

                if (uketsukeExist == false)
                {
                    // 削除モード時はモードを変更すると削除できなくなるのでモードを変更しない
                    if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && this.form.TairyuuNewFlg == true)
                    {
                        // 滞留一覧からの新規データはUPDATEに戻す
                        this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }

                    // CreateEntityとそれぞれの更新処理でDB更新が発生するため、UIFormから
                    // 排他制御する
                    using (Transaction tran = new Transaction())
                    {
                        switch (this.form.WindowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                // 受入系
                                this.CreateEntity(taiyuuKbn);
                                if (!taiyuuKbn)
                                {
                                    // 入出金系
                                    this.CreateNyuuShukkinEntity();
                                }
                                this.Regist(errorFlag);

                                // キャッシャ連動「1.する」の場合
                                if (this.form.denpyouHakouPopUpDTO.Kyasya == CommonConst.CASHER_LINK_KBN_USE)
                                {
                                    // キャッシャ情報送信
                                    this.SendCasher();
                                }
                                break;

                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                this.CreateEntity(taiyuuKbn);
                                if (!taiyuuKbn)
                                {
                                    // 入出金系
                                    this.CreateNyuuShukkinEntity();
                                }
                                this.Update(errorFlag);

                                // キャッシャ連動「1.する」かつ滞留登録の場合
                                if ((this.form.TairyuuNewFlg == true) && (this.form.denpyouHakouPopUpDTO.Kyasya == CommonConst.CASHER_LINK_KBN_USE))
                                {
                                    // キャッシャ情報送信
                                    this.SendCasher();
                                }
                                break;

                            case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                                this.CreateEntity(taiyuuKbn);
                                this.LogicalDelete();
                                break;

                            default:
                                break;
                        }
                        // コミット
                        tran.Commit();
                    }
                }
                else
                {
                    // 重複している場合はエラー表示を行う
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShowError("該当の受付データに変更があります。\n再度入力し直してください。");
                    return false;
                }

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                catchErr = true;
                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex1);

                var causeNo = ((SqlException)ex1.InnerException).Number;

                // 一意エラーの場合
                if (causeNo == 2627)
                {
                    msgLogic.MessageBoxShow("E080", "");
                    catchErr = true;
                    return false;
                }

                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            this.dto.entryEntity.DELETE_FLG = true;
            // 20141118 Houkakou 「更新日、登録日の見直し」　start
            this.dto.entryEntity.UPDATE_DATE = this.beforDto.entryEntity.UPDATE_DATE;
            this.dto.entryEntity.UPDATE_PC = this.beforDto.entryEntity.UPDATE_PC;
            this.dto.entryEntity.UPDATE_USER = this.beforDto.entryEntity.UPDATE_USER;
            // 20141118 Houkakou 「更新日、登録日の見直し」　end
            this.accessor.UpdateShukkaEntry(this.dto.entryEntity);

            // 20141118 Houkakou 「更新日、登録日の見直し」　start
            this.dto.entryEntity.DELETE_FLG = true;
            this.dto.entryEntity.SEQ = this.dto.entryEntity.SEQ + 1;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dto.entryEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.dto.entryEntity.UPDATE_PC = SystemInformation.ComputerName;
            this.dto.entryEntity.UPDATE_USER = SystemProperty.UserName;
            this.accessor.InsertShukkaEntry(this.dto.entryEntity);

            for (int row = 0; row < this.dto.detailEntity.Length; row++)
            {
                this.dto.detailEntity[row].SEQ = this.dto.entryEntity.SEQ;
            }
            this.accessor.InsertShukkaDetail(this.dto.detailEntity);

            // 在庫系の更新
            // 在庫管理の場合のみ設定する
            if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
            {
                // Dictionary関連修正
                foreach (Row dr in this.form.gcMultiRow1.Rows)
                {
                    List<T_ZAIKO_SHUKKA_DETAIL> zaikoShukkaDetailsList = this.dto.rowZaikoShukkaDetails[dr];
                    foreach (T_ZAIKO_SHUKKA_DETAIL entity in zaikoShukkaDetailsList)
                    {
                        entity.SEQ = this.dto.entryEntity.SEQ;
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        entity.UPDATE_PC = SystemInformation.ComputerName;
                        entity.UPDATE_USER = SystemProperty.UserName;
                    }
                    // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する Start
                    List<T_ZAIKO_HINMEI_HURIWAKE> zaikoHinmeiHuriwakesList = this.dto.rowZaikoHinmeiHuriwakes[dr];
                    foreach (T_ZAIKO_HINMEI_HURIWAKE entity in zaikoHinmeiHuriwakesList)
                    {
                        entity.SEQ = this.dto.entryEntity.SEQ;
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        entity.UPDATE_PC = SystemInformation.ComputerName;
                        entity.UPDATE_USER = SystemProperty.UserName;
                    }
                    // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する End
                }
                this.accessor.InsertZaikoShukkaDetails(this.dto.rowZaikoShukkaDetails);
                this.accessor.UpdateZaikoShukkaDetails(this.beforDto.detailZaikoShukkaDetails);
                // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する Start
                this.accessor.InsertZaikoHinmeiHuriwakes(this.dto.rowZaikoHinmeiHuriwakes);
                this.accessor.UpdateZaikoHinmeiHuriwakes(this.beforDto.detailZaikoHinmeiHuriwakes);
                // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する End
            }
            // 20141118 Houkakou 「更新日、登録日の見直し」　end

            // 受付番号がある場合は、受付のデータ更新
            if (!this.dto.entryEntity.UKETSUKE_NUMBER.IsNull)
            {
                var dtUketsuke = this.accessor.GetUketsukeSK(this.dto.entryEntity.UKETSUKE_NUMBER.ToString());
                if (dtUketsuke.Rows.Count > 0)
                {
                    // 複数の伝票に対して連携されている場合、伝票の更新は行わない。
                    var dtUketsukeRenkeiData = this.accessor.GetUketsukeSKRenkei(this.dto.entryEntity.UKETSUKE_NUMBER.ToString(), this.dto.entryEntity.SHUKKA_NUMBER.ToString());
                    if (dtUketsukeRenkeiData == null || dtUketsukeRenkeiData.Rows.Count < 1)
                    {
                        var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                        var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                        this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                        if (null != this.tUketsukeSkEntry)
                        {
                            if (!String.IsNullOrEmpty(this.tUketsukeSkEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSkEntry.UNTENSHA_CD))
                            {
                                this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                            }
                            else
                            {
                                this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                            }
                        }
                    }
                }
            }

            LogUtility.DebugMethodStart();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertShukkaEntry(this.dto.entryEntity);
            this.accessor.InsertShukkaDetail(this.dto.detailEntity);
            if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberDay(this.dto.numberDay);
            }
            else if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberDay(this.dto.numberDay);
            }
            if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberYear(this.dto.numberYear);
            }
            else if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberYear(this.dto.numberYear);
            }

            // S_NUMBER_RECEIPTの更新
            if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceipt(this.dto.numberReceipt);
            }
            else if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceipt(this.dto.numberReceipt);
            }

            // S_NUMBER_RECEIPT_YEARの更新
            if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceiptYear(this.dto.numberReceiptYear);
            }
            else if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceiptYear(this.dto.numberReceiptYear);
            }

            // 入出金系の更新
            // Insertメソッド内で更新に必要なキーのチェックをしているので、ここでのチェックは必要なし
            this.accessor.InsertNyuukinSumEntry(this.nyuuShukkinDto.nyuukinSumEntry);
            this.accessor.InsertNyuukinSumDetails(this.nyuuShukkinDto.nyuukinSumDetails);
            this.accessor.InsertNyuukinEntry(this.nyuuShukkinDto.nyuukinEntry);
            this.accessor.InsertNyuukinDetails(this.nyuuShukkinDto.nyuukinDetials);
            this.accessor.InsertShukkinEntry(this.nyuuShukkinDto.shukkinEntry);
            this.accessor.InsertShukkinDetails(this.nyuuShukkinDto.shukkinDetails);

            // 在庫系の更新
            // 在庫管理の場合のみ設定する
            if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
            {
                // Dictionary関連修正
                this.accessor.InsertZaikoShukkaDetails(this.dto.rowZaikoShukkaDetails);
                // No.4578-->
                // 20150409 go 在庫品名振分処理追加 Start
                this.accessor.InsertZaikoHinmeiHuriwakes(this.dto.rowZaikoHinmeiHuriwakes);
                // 20150409 go 在庫品名振分処理追加 End
                // No.4578<--
            }

            // 収集受付の更新
            if (null != this.tUketsukeSkEntry)
            {
                this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 検索(使用しない)
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertShukkaEntry(this.dto.entryEntity);
            this.accessor.UpdateShukkaEntry(this.beforDto.entryEntity);
            this.accessor.InsertShukkaDetail(this.dto.detailEntity);
            this.accessor.InsertKenshuDetail(this.dto.kenshuNyuuryokuDto.kenshuDetailList);
            if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberDay(this.dto.numberDay);
            }
            else if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberDay(this.dto.numberDay);
            }
            if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberYear(this.dto.numberYear);
            }
            else if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberYear(this.dto.numberYear);
            }

            // S_NUMBER_RECEIPTの更新
            if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceipt(this.dto.numberReceipt);
            }
            else if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceipt(this.dto.numberReceipt);
            }

            // S_NUMBER_RECEIPT_YEARの更新
            if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceiptYear(this.dto.numberReceiptYear);
            }
            else if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceiptYear(this.dto.numberReceiptYear);
            }

            // 入出金系の更新
            // Insertメソッド内で更新に必要なキーのチェックをしているので、ここでのチェックは必要なし
            this.accessor.InsertNyuukinSumEntry(this.nyuuShukkinDto.nyuukinSumEntry);
            this.accessor.InsertNyuukinSumDetails(this.nyuuShukkinDto.nyuukinSumDetails);
            this.accessor.InsertNyuukinEntry(this.nyuuShukkinDto.nyuukinEntry);
            this.accessor.InsertNyuukinDetails(this.nyuuShukkinDto.nyuukinDetials);
            this.accessor.InsertShukkinEntry(this.nyuuShukkinDto.shukkinEntry);
            this.accessor.InsertShukkinDetails(this.nyuuShukkinDto.shukkinDetails);

            // 在庫系の更新
            // 在庫管理の場合のみ設定する
            if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
            {
                // 2次
                // Dictionary関連修正
                this.accessor.InsertZaikoShukkaDetails(this.dto.rowZaikoShukkaDetails);
                this.accessor.UpdateZaikoShukkaDetails(this.beforDto.detailZaikoShukkaDetails);
                // No.4578-->
                // 20150409 go 在庫品名振分処理追加 Start
                this.accessor.InsertZaikoHinmeiHuriwakes(this.dto.rowZaikoHinmeiHuriwakes);
                // 20150409 go 在庫品名振分処理追加 End
                // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する Start
                this.accessor.UpdateZaikoHinmeiHuriwakes(this.beforDto.detailZaikoHinmeiHuriwakes);
                // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する End
                // No.4578<--
            }

            // 出荷受付の更新
            var beforeUketsukeNumber = this.beforDto.entryEntity.UKETSUKE_NUMBER;
            var uketsukeNumber = this.dto.entryEntity.UKETSUKE_NUMBER;

            if (beforeUketsukeNumber.IsNull && !uketsukeNumber.IsNull)
            {
                // 更新後だけ受付番号がセットされている場合は、更新後データに紐付けられている受付データの配車状況を更新する
                if (null != this.tUketsukeSkEntry)
                {
                    this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
                }
            }
            else if (!beforeUketsukeNumber.IsNull && uketsukeNumber.IsNull)
            {
                // 更新前だけ受付番号がセットされている場合は、更新前データに紐付けられている受付の配車状況を更新する
                // 更新前データに紐付けられている受付データは取得してから更新する
                var dtUketsuke = this.accessor.GetUketsukeSK(beforeUketsukeNumber.ToString());
                if (dtUketsuke.Rows.Count > 0)
                {
                    var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                    var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                    this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                    if (null != this.tUketsukeSkEntry)
                    {
                        if (!String.IsNullOrEmpty(this.tUketsukeSkEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSkEntry.UNTENSHA_CD))
                        {
                            this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                        }
                        else
                        {
                            this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                        }
                    }
                }
            }
            else if (!beforeUketsukeNumber.IsNull && !uketsukeNumber.IsNull && beforeUketsukeNumber != uketsukeNumber)
            {
                // 両方の受付番号がセットされている場合は、両方のデータに紐付けられている受付データの配車状況を更新する
                // 先に更新後データに紐付けられている受付の配車状況を更新
                if (null != this.tUketsukeSkEntry)
                {
                    this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
                }
                // 更新前データに紐付けられている受付データは取得してから更新する
                var dtUketsuke = this.accessor.GetUketsukeSK(beforeUketsukeNumber.ToString());
                if (dtUketsuke.Rows.Count > 0)
                {
                    var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                    var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                    this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                    if (null != this.tUketsukeSkEntry)
                    {
                        if (!String.IsNullOrEmpty(this.tUketsukeSkEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSkEntry.UNTENSHA_CD))
                        {
                            this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                        }
                        else
                        {
                            this.UpdateHaishaJokyo(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// MultiRowのデータに対しROW_NOを採番します
        /// </summary>
        public bool NumberingRowNo()
        {
            bool ret = false;
            try
            {
                if (!this.form.notEditingOperationFlg)
                {
                    this.form.gcMultiRow1.BeginEdit(false);
                }

                foreach (Row dr in this.form.gcMultiRow1.Rows)
                {
                    dr.Cells[CELL_NAME_ROW_NO].Value = dr.Index + 1;
                }

                if (!this.form.notEditingOperationFlg)
                {
                    this.form.gcMultiRow1.EndEdit();
                    this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NumberingRowNo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("NumberingRowNo", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;

        }

        /// <summary>
        /// 総重量、空車重量の状態によって、割振、調整の入力制限を変更する
        /// </summary>
        internal bool ChangeWarihuriAndChouseiInputStatus()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                    && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                {
                    this.ChangeTenyuuryoku(targetRow, false);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value))
                        || !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value)))
                        // 同一行にすでに割振りがある場合は、割振、調整を使用可能にする
                        this.ChangeTenyuuryoku(targetRow, false);
                    else
                        this.ChangeTenyuuryoku(targetRow, true);
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeWarihuriAndChouseiInputStatus", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeWarihuriAndChouseiInputStatus", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 手入力変更処理
        /// </summary>
        /// <param name="tenyuuryokuFlag">ture: 手入力可, false:手入力不可</param>
        internal void ChangeTenyuuryoku(Row targetRow, bool isReadOnly)
        {
            LogUtility.DebugMethodStart(targetRow, isReadOnly);

            if (targetRow == null)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            /**
             * 手入力可能とする
             */
            targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnly;

            targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(false);    // No.2076
            targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(false);    // No.2076
            targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);    // No.2076
            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);    // No.2076

            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細の金額、重量計算
        /// 金額、重量計算をまとめて処理します
        /// </summary>
        internal bool CalcDetail()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 割振計算
                if (!this.ExecuteWarifuri(true))
                {
                    return false;
                }

                // 調整kg(片方だけ計算すればいいはず)
                if (!this.CalcChouseiJyuuryou())
                {
                    return false;
                }

                // 容器重量
                if (!this.CalcYoukiJyuuryou())
                {
                    return false;
                }

                // 合計系金額計算
                if (!this.CalcTotalValues())
                {
                    return false;
                }

                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CalcDetail", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 総重量または空車重量計算
        /// </summary>
        internal bool CalcStackOrEmptyJyuuryou()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal chouseiJyuuryou = 0;
                decimal youkiJyuuryou = 0;

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);

                // 総重量・空車重量のどちらか片方でも入力されていなければ
                // 正味重量に値をセットしない
                if (targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null
                    && targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
                {
                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = stackJyuuryou - emptyJyuuryou - chouseiJyuuryou - youkiJyuuryou;
                }
                else
                {
                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
                }
                targetRow.Cells[CELL_NAME_SUURYOU].ReadOnly = false;

                if (targetRow.Cells[CELL_NAME_UNIT_CD].Value != null && targetRow.Cells[CELL_NAME_UNIT_CD].Value.ToString() == "3")
                {
                    targetRow.Cells[CELL_NAME_SUURYOU].Value = targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value;
                    // 単位：Kgの場合は読み取り専用
                    targetRow.Cells[CELL_NAME_SUURYOU].ReadOnly = true;
                }

                // ReadOnlyを変更するとBackColorが変わらない場合がある
                var cell = targetRow.Cells[CELL_NAME_SUURYOU] as ICustomAutoChangeBackColor;
                cell.UpdateBackColor();

                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CalcStackOrEmptyJyuuryou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 総重量または空車重量計算
        /// </summary>
        internal void CalcStackOrEmptyJyuuryou(Row targetRow)
        {
            LogUtility.DebugMethodStart();

            if (targetRow == null)
            {
                return;
            }

            this.form.gcMultiRow1.BeginEdit(false);

            decimal stackJyuuryou = 0;
            decimal emptyJyuuryou = 0;
            decimal chouseiJyuuryou = 0;
            decimal youkiJyuuryou = 0;

            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);

            // 総重量・空車重量のどちらか片方でも入力されていなければ
            // 正味重量に値をセットしない
            if (targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null
                && targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
            {
                targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = stackJyuuryou - emptyJyuuryou - chouseiJyuuryou - youkiJyuuryou;
            }
            else
            {
                targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
            }

            targetRow.Cells[CELL_NAME_SUURYOU].ReadOnly = false;

            if (targetRow.Cells[CELL_NAME_UNIT_CD].Value != null && targetRow.Cells[CELL_NAME_UNIT_CD].Value.ToString() == "3")
            {
                targetRow.Cells[CELL_NAME_SUURYOU].Value = targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value;
                // 単位：Kgの場合は読み取り専用
                targetRow.Cells[CELL_NAME_SUURYOU].ReadOnly = true;
            }
            else
            {
                if (targetRow.Cells[CELL_NAME_UNIT_CD].Value != null && targetRow.Cells[CELL_NAME_UNIT_CD].Value.ToString() == "1"
                    && targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value != null)
                {
                    decimal ton = (stackJyuuryou - emptyJyuuryou - chouseiJyuuryou - youkiJyuuryou) / 1000;
                    // 単位tの場合は正味重量/1000＝数量とする
                    targetRow.Cells[CELL_NAME_SUURYOU].Value = ton;
                }
            }

            // ReadOnlyを変更するとBackColorが変わらない場合がある
            var cell = targetRow.Cells[CELL_NAME_SUURYOU] as ICustomAutoChangeBackColor;
            cell.UpdateBackColor();

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 合計系の計算
        /// </summary>
        internal bool CalcTotalValues()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                decimal netTotal = 0;
                decimal uriageKingakuTotal = 0;
                decimal shiharaiKingakuTotal = 0;
                foreach (Row dr in this.form.gcMultiRow1.Rows)
                {
                    decimal kingaku = 0;
                    decimal netJyuuryou = 0;

                    decimal.TryParse(Convert.ToString(dr.Cells[CELL_NAME_KINGAKU].EditedFormattedValue), out kingaku);
                    decimal.TryParse(Convert.ToString(dr.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);

                    // 正味重量計算
                    netTotal += netJyuuryou;

                    // 売上金額合計、支払金額合計計算
                    switch (Convert.ToString(dr.Cells[CELL_NAME_DENPYOU_KBN_CD].Value))
                    {
                        case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR:
                            uriageKingakuTotal += kingaku;
                            break;

                        case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                            shiharaiKingakuTotal += kingaku;
                            break;

                        default:
                            break;
                    }
                }
                this.form.NET_TOTAL.Text = netTotal.ToString("N");
                CustomTextBoxLogic customTextBoxLogic = new CustomTextBoxLogic(this.form.NET_TOTAL);
                customTextBoxLogic.Format(this.form.NET_TOTAL);
                this.form.URIAGE_KINGAKU_TOTAL.Text = uriageKingakuTotal.ToString();
                this.form.SHIHARAI_KINGAKU_TOTAL.Text = shiharaiKingakuTotal.ToString();

                // 差額計算
                this.CalcSagaku();

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcTotalValues", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTotalValues", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 調整Kg更新後の計算
        /// </summary>
        internal bool CalcChouseiJyuuryou()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    LogUtility.DebugMethodEnd();
                    return true;
                }

                #region 変更チェック
                int warifuriNo = -1;
                short warifuriRowNo = -1;

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                }

                if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                    && warifuriNo < this.jyuuryouDtoList.Count
                    && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                {
                    // 割振系が更新されていないかチェック
                    if ((Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)
                        .Equals(Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiJyuuryou)))
                        && (Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)
                        .Equals(Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiPercent)))
                        && (Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value)
                        .Equals(Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].youkiJyuuryou))))
                    {
                        LogUtility.DebugMethodEnd();
                        return true;
                    }
                }
                #endregion

                this.form.gcMultiRow1.BeginEdit(false);
                var criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)))
                {
                    // 紐付くデータの削除
                    targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = string.Empty;
                    if (criterionNet != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value))
                        && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value)))
                    {
                        decimal netJyuuryou = 0;
                        decimal youkiJyuuryou = 0;
                        var netTryPaseResult = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);
                        var youkiTryPaseResult = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                        if (!netTryPaseResult && !youkiTryPaseResult)
                        {
                            this.form.gcMultiRow1.EndEdit();
                            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                            LogUtility.DebugMethodEnd();
                            return true;
                        }

                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = (netJyuuryou - youkiJyuuryou);
                    }
                }
                else
                {
                    decimal chouseiJyuuryou = 0;  // 調整kg
                    decimal youkiJyuuryou = 0;    // 容器重量
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                    criterionNet = criterionNet - youkiJyuuryou;

                    // 調整%計算
                    decimal chouseiPercent = 0;
                    if (criterionNet != 0)
                    {
                        if (decimal.TryParse(Convert.ToString((chouseiJyuuryou / criterionNet) * 100), out chouseiPercent))
                        {
                            chouseiPercent = (CommonCalc.FractionCalc((decimal)(chouseiJyuuryou / criterionNet) * 100, (int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_CD, (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_KETA));
                            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = chouseiPercent;

                            // 正味重量計算
                            targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou;
                        }
                    }

                    if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                        && warifuriNo < this.jyuuryouDtoList.Count
                        && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                    {
                        this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiJyuuryou = chouseiJyuuryou;
                        this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiPercent = chouseiPercent;
                        this.jyuuryouDtoList[warifuriNo][warifuriRowNo].youkiJyuuryou = youkiJyuuryou;
                    }
                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcChouseiJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcChouseiJyuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 調整%更新後の計算
        /// </summary>
        internal bool CalcChouseiPercent()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                this.form.gcMultiRow1.BeginEdit(false);

                var criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)))
                {
                    // 紐付くデータの削除
                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = string.Empty;
                    if (criterionNet != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value))
                        && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value)))
                    {
                        decimal netJyuuryou = 0;
                        decimal youkiJyuuryou = 0;
                        decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);
                        decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);

                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = (netJyuuryou - youkiJyuuryou);
                    }
                }
                else
                {
                    decimal chouseiPercent = 0;  // 調整%
                    decimal youkiJyuuryou = 0;    // 容器重量
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value), out chouseiPercent);
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                    criterionNet = criterionNet - youkiJyuuryou;

                    // 調整Kg計算
                    decimal criterionNetCalcResult = (decimal)(criterionNet * (chouseiPercent / 100));
                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = criterionNetCalcResult;

                    // 正味重量計算
                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = (decimal)criterionNet - criterionNetCalcResult;

                    #region jyuuryouDtoListに調整データをセット
                    int warifuriNo = -1;
                    short warifuriRowNo = -1;

                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                    {
                        int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                    {
                        short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                    }

                    if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                        && warifuriNo < this.jyuuryouDtoList.Count
                        && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                    {
                        this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiJyuuryou = criterionNetCalcResult;
                        this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiPercent = chouseiPercent;
                        this.jyuuryouDtoList[warifuriNo][warifuriRowNo].youkiJyuuryou = youkiJyuuryou;
                    }
                    #endregion
                }
                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcChouseiPercent", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcChouseiPercent", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 容器欄初期化
        /// </summary>
        internal bool InitYoukiItem()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                string youkiCd = Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_CD].Value);
                if (string.IsNullOrEmpty(youkiCd))
                {
                    targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = string.Empty;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("InitYoukiItem", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitYoukiItem", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 容器数量更新後計算
        /// </summary>
        internal bool CalcYoukiSuuryou()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                M_YOUKI youki = this.accessor.GetYouki((Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_CD].Value)));

                // 容器数量用処理
                decimal youkiJyuryou = 0;     // 容器重量(容器)
                decimal youkiSuuryou = 0;     // 容器数量(出荷明細)

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].Value), out youkiSuuryou);

                if (!decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].Value), out youkiSuuryou))
                {
                    targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = null;
                }
                else if (youki != null)
                {
                    decimal tempJyuryou = 0;
                    decimal.TryParse(Convert.ToString(youki.YOUKI_JYURYO), out tempJyuryou);
                    youkiJyuryou = tempJyuryou;

                    // 容器重量設定(出荷明細)
                    // 容器重量を計算
                    decimal youkiJyuuryouForCell = youkiJyuryou * youkiSuuryou;   // 正味重量の計算があるため変数に設定
                    targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = youkiJyuuryouForCell;
                }

                if (!this.CalcYoukiJyuuryou())
                {
                    return false;
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcYoukiSuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcYoukiSuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 容器重量更新後計算
        /// </summary>
        internal bool CalcYoukiJyuuryou()
        {
            bool ret = false;

            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                // 容器重量用処理
                decimal youkiJyuryou = 0;     // 容器重量(容器)
                decimal stakJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warifuriJyuuryou = 0;
                decimal netJyuuryou = 0;

                if (
                    !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stakJyuuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou)
                    )
                {
                    return true;
                }

                if ((0 <= stakJyuuryou && 0 <= emptyJyuuryou)
                    || 0 <= warifuriJyuuryou)
                {
                    var criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                    decimal chouseiJyuuryou = 0;
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
                    // 総重量・空車重量のどちらか片方でも入力されていなければ
                    // 正味重量に値をセットしない
                    if (targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null
                        && targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou - youkiJyuryou;
                    }
                    else if (targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou - youkiJyuryou;
                    }
                    else
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcYoukiJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcYoukiJyuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 割振での重量書き込み禁止判断
        /// </summary>
        /// <param name="dr">Row</param>
        internal bool WarifuriReadOnlyCheck(Row dr)
        {
            try
            {
                #region コントロールのReadOnly初期化
                // 総重量、空車重量
                dr.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = false;
                dr.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = false;
                dr.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);
                dr.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);

                // 割振、調整
                this.ChangeTenyuuryoku(dr, true);
                #endregion

                object checkTargetValue = dr.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value;
                object chouseiValue = dr.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value;
                if (string.IsNullOrEmpty(Convert.ToString(checkTargetValue)))
                {
                    checkTargetValue = dr.Cells[CELL_NAME_WARIFURI_PERCENT].Value;
                }
                if (string.IsNullOrEmpty(Convert.ToString(chouseiValue)))
                {
                    chouseiValue = dr.Cells[CELL_NAME_CHOUSEI_PERCENT].Value;
                }

                int warihuriRowNow = -1;
                int.TryParse(Convert.ToString(dr.Cells[CELL_NAME_warihuriRowNo].Value), out warihuriRowNow);

                if ((!string.IsNullOrEmpty(Convert.ToString(checkTargetValue))
                    || !string.IsNullOrEmpty(Convert.ToString(chouseiValue)))
                    && 0 <= warihuriRowNow)
                {
                    // 総重量、空車重量
                    dr.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = true;
                    dr.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = true;
                    dr.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);
                    dr.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);

                    // 割振、調整
                    this.ChangeTenyuuryoku(dr, false);
                }

                object checkTargetValue1 = dr.Cells[CELL_NAME_STAK_JYUURYOU].Value;
                object checkTargetValue2 = dr.Cells[CELL_NAME_EMPTY_JYUURYOU].Value;
                if ((string.IsNullOrEmpty(Convert.ToString(checkTargetValue1)) ||
                    string.IsNullOrEmpty(Convert.ToString(checkTargetValue2)))
                    && warihuriRowNow < 1)
                {
                    this.ChangeTenyuuryoku(dr, true);
                }

                // 総重量、空車重量に値が入力されていた場合、割振などは活性化
                if (!string.IsNullOrEmpty(Convert.ToString(checkTargetValue1)) &&
                    !string.IsNullOrEmpty(Convert.ToString(checkTargetValue2)))
                {
                    this.ChangeTenyuuryoku(dr, false);
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WarifuriReadOnlyCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WarifuriReadOnlyCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 割振計算処理
        /// </summary>
        /// <param name="isWarihuriJyuuryou">ture: 割振kg基点, false: 割振(%)基点</param>
        internal bool ExecuteWarifuri(bool isWarihuriJyuuryou)
        {
            bool ret = false;
            try
            {
                /**
                 * warifuriNo   ：jyuuryouDtoListのindex
                 * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
                 * **/

                LogUtility.DebugMethodStart(isWarihuriJyuuryou);

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                int warifuriNo = -1;
                short warifuriRowNo = -1;

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                }

                if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                    && warifuriNo < this.jyuuryouDtoList.Count
                    && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                {
                    // 割振系が更新されていないかチェック
                    if (isWarihuriJyuuryou)
                    {
                        if (Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value)
                            == Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].warifuriJyuuryou))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value)
                            == Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].warifuriPercent))
                        {
                            return true;
                        }
                    }
                }

                // jyuuryouDtoListを初期化
                this.SetJyuuryouDataToDtoList();

                warifuriNo = -1;
                warifuriRowNo = -1;

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                }

                object checkTargetValue = null;

                if (isWarihuriJyuuryou)
                {
                    checkTargetValue = targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value;
                }
                else
                {
                    checkTargetValue = targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value;
                }

                if (warifuriNo < 0 || warifuriRowNo < 0)
                {
                    return true;
                }

                if (string.IsNullOrEmpty(Convert.ToString(checkTargetValue)))
                {
                    // 削除時の処理

                    /**
                     * 割振が設定されていない場合は、「総重量」「空車重量」を編集可にする。
                     */
                    targetRow.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = false;
                    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = false;

                    targetRow.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);    // No.2076
                    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);    // No.2076

                    // 重量値リストの更新
                    // 対象のJyuuryouDtoより下を削除
                    if (warifuriNo < this.jyuuryouDtoList.Count)
                    {
                        var jyuuryouDtos = this.jyuuryouDtoList[warifuriNo];
                        int i = warifuriRowNo + 1;
                        int beforeUpdateCount = jyuuryouDtos.Count;

                        // 現在の選択行より下の行に割振の値がある行があるかを調べる
                        bool isExist = false;
                        for (int j = jyuuryouDtos.Count; j > i; j--)
                        {
                            if (jyuuryouDtos[j - 1].warifuriJyuuryou != null)
                            {
                                isExist = true;
                            }
                        }

                        while (i < jyuuryouDtos.Count)
                        {
                            jyuuryouDtos.RemoveAt(i);
                        }

                        if (0 < warifuriRowNo)
                        {
                            // 自分自身を削除
                            jyuuryouDtos.RemoveAt(warifuriRowNo);
                            // 再計算のため1つ上の割振を削除
                            // ただし、削除対象行よりも下に割振がされている行が存在する場合は、1つ上の割振は削除しない
                            int afterUpdateCount = jyuuryouDtos.Count;
                            if (beforeUpdateCount <= afterUpdateCount + 1 || !isExist)
                            {
                                jyuuryouDtos[warifuriRowNo - 1].warifuriJyuuryou = null;
                                jyuuryouDtos[warifuriRowNo - 1].warifuriPercent = null;
                                // 仕様変更のため調整も削除
                                jyuuryouDtos[warifuriRowNo - 1].chouseiJyuuryou = null;
                                jyuuryouDtos[warifuriRowNo - 1].chouseiPercent = null;
                            }
                        }
                        else
                        {
                            // 先頭行の場合は自分の割振kgと割振%を削除するだけ
                            jyuuryouDtos[warifuriRowNo].warifuriJyuuryou = null;
                            jyuuryouDtos[warifuriRowNo].warifuriPercent = null;
                            // 仕様変更のため調整も削除
                            jyuuryouDtos[warifuriRowNo].chouseiJyuuryou = null;
                            jyuuryouDtos[warifuriRowNo].chouseiPercent = null;
                        }
                    }

                    // 再計算処理
                    foreach (var jyuuryouDtoList in this.jyuuryouDtoList)
                    {
                        JyuuryouDto.CalcJyuuryouDtoForAdd(
                            jyuuryouDtoList,
                            isWarihuriJyuuryou,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
                    }
                }
                else
                {
                    /**
                     * 割振が設定されている場合は、「総重量」「空車重量」を編集不可にする。
                     */
                    targetRow.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = true;
                    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = true;

                    targetRow.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);    // No.2076
                    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);    // No.2076

                    // 追加時の処理
                    decimal stackJyuuryou = 0;
                    decimal emptyJyuuryou = 0;
                    decimal warifuriJyuuryou = 0;
                    decimal warifuriPercent = 0;

                    JyuuryouDto jyuuryouDto = new JyuuryouDto();

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou))
                    {
                        jyuuryouDto.stackJyuuryou = stackJyuuryou;
                    }

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                    {
                        jyuuryouDto.emptyJyuuryou = emptyJyuuryou;
                    }

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou))
                    {
                        jyuuryouDto.warifuriJyuuryou = warifuriJyuuryou;
                    }

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value), out warifuriPercent))
                    {
                        jyuuryouDto.warifuriPercent = warifuriPercent;
                    }

                    // 重量リストの更新
                    this.AddJyuuryouDataList(
                        warifuriNo,
                        warifuriRowNo,
                        jyuuryouDto,
                        isWarihuriJyuuryou
                    );

                }

                // 重量リストを使って重量値の更新
                this.SetJyuuryouDataToMultiRow();

                // 調整kg, 容器重量は移動しないため、重量値を再計算
                this.SetJyuuryouDataToDtoList();
                foreach (var jyuuryouDtoList in this.jyuuryouDtoList)
                {
                    JyuuryouDto.CalcJyuuryouDtoForAdd(
                        jyuuryouDtoList,
                        isWarihuriJyuuryou,
                        (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                        (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA,
                        (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                        (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
                }
                this.SetJyuuryouDataToMultiRow();
                if (!this.NumberingRowNo())
                {
                    return false;
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("ExecuteWarifuri", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("ExecuteWarifuri", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 割振重量入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateWarifuriJyuuryou(out bool catchErr)
        {
            bool returnVal = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                int warifuriNo = -1;
                short warifuriRowNo = -1;
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                }
                if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                    && warifuriNo < this.jyuuryouDtoList.Count
                    && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                {
                    // 割振系が更新されていないかチェック
                    if (Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value)
                        == Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].warifuriJyuuryou))
                    {
                        returnVal = true;
                        return returnVal;
                    }
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warihuriJyuuryou = 0;
                decimal netJyuuryou = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warihuriJyuuryou))
                {
                    if (warihuriJyuuryou == 0)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_WARIFURI_JYUURYOU);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "0以上");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }
                else if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value)))
                {
                    // Null or 空は許容しているのでスルー
                    returnVal = true;
                    return returnVal;
                }

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                {
                    // 全部値がある場合にだけチェック
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value = string.Empty;
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                    else if ((stackJyuuryou - emptyJyuuryou) <= warihuriJyuuryou)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_WARIFURI_JYUURYOU);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", (stackJyuuryou - emptyJyuuryou).ToString() + "未満");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }
                else if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou))
                {
                    List<JyuuryouDto> list = this.jyuuryouDtoList[warifuriNo];

                    if (decimal.TryParse(Convert.ToString(list[0].stackJyuuryou), out stackJyuuryou)
                        && decimal.TryParse(Convert.ToString(list[0].emptyJyuuryou), out emptyJyuuryou))
                    {
                        decimal tmpTotal = 0;
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (i >= warifuriRowNo) continue;
                            if (list[i].warifuriJyuuryou == null) continue;

                            tmpTotal += (decimal)list[i].warifuriJyuuryou;
                        }

                        if ((stackJyuuryou - emptyJyuuryou - tmpTotal) < warihuriJyuuryou)
                        {
                            CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                            this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_WARIFURI_JYUURYOU);
                            new MessageBoxShowLogic().MessageBoxShow("E048", (stackJyuuryou - emptyJyuuryou - tmpTotal).ToString() + "以下");
                            return returnVal;
                        }
                    }
                }
                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateWarifuriJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateWarifuriJyuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 差額計算
        /// </summary>
        internal void CalcSagaku()
        {
            LogUtility.DebugMethodStart();

            decimal uriageTotal = 0;
            decimal shiharaiTotal = 0;

            decimal.TryParse(Convert.ToString(this.form.URIAGE_KINGAKU_TOTAL.Text), out uriageTotal);
            decimal.TryParse(Convert.ToString(this.form.SHIHARAI_KINGAKU_TOTAL.Text), out shiharaiTotal);

            if (this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN == SalesPaymentConstans.SHUKKA_CALC_BASE_KBN_URIAGE)
            {
                this.form.SAGAKU.Text = Convert.ToString(uriageTotal - shiharaiTotal);
            }
            else if (this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN == SalesPaymentConstans.SHUKKA_CALC_BASE_KBN_SHIHARAI)
            {
                this.form.SAGAKU.Text = Convert.ToString(shiharaiTotal - uriageTotal);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 割振%入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateWarifuriPercent(out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value)))
                {
                    return true;
                }

                int warifuriNo = -1;
                short warifuriRowNo = -1;
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                }
                if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                    && warifuriNo < this.jyuuryouDtoList.Count
                    && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                {
                    // 割振系が更新されていないかチェック
                    if (Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value)
                        == Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].warifuriPercent))
                    {
                        returnVal = true;
                        return returnVal;
                    }
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                {
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value = string.Empty;
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }

                decimal warifuriPercent = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value), out warifuriPercent))
                {
                    if (100 <= warifuriPercent)
                    {
                        //this.form.RegistErrorFlag = true;
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_WARIFURI_PERCENT);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "100未満");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }

                    if (warifuriPercent == 0)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_WARIFURI_PERCENT);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "0以上");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }

                    decimal tmpTotal = 0;
                    List<JyuuryouDto> list = this.jyuuryouDtoList[warifuriNo];
                    if (list.Count > 1 && warifuriRowNo != 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (i >= warifuriRowNo) continue;
                            if (list[i].warifuriPercent == null) continue;

                            tmpTotal += (decimal)list[i].warifuriPercent;
                        }
                        if ((100 - tmpTotal) < warifuriPercent)
                        {
                            CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                            this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_WARIFURI_PERCENT);
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E048", (100 - tmpTotal).ToString() + "以下");
                            return returnVal;
                        }
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateWarifuriPercent", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateWarifuriPercent", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        /// <summary>
        /// 調整kg入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateChouseiJyuuryou(out bool catchErr)
        {
            bool returnVal = false;
            catchErr = false;
            try
            {

                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                int warifuriNo = -1;
                short warifuriRowNo = -1;
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                }
                if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                    && warifuriNo < this.jyuuryouDtoList.Count
                    && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                {
                    // 割振系が更新されていないかチェック
                    if (Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)
                        == Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiJyuuryou))
                    {
                        returnVal = true;
                        return returnVal;
                    }
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warihuriJyuuryou = 0;
                decimal chouseiJyuuryou = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou))
                {
                    if (chouseiJyuuryou == 0)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_JYUURYOU);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "1以上");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }
                else if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)))
                {
                    // Null or 空は許容しているのでスルー
                    returnVal = true;
                    return returnVal;
                }

                /**
                 * 総重量-空車重量の値　又は　割振Kgが入力されている場合
                 */
                if ((decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou)))
                {
                    decimal youkiJyuuryou = 0;
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                    // 全部値がある場合にだけチェック
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = string.Empty;
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                    else if ((stackJyuuryou - emptyJyuuryou - youkiJyuuryou) <= chouseiJyuuryou)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_JYUURYOU);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", (stackJyuuryou - emptyJyuuryou - youkiJyuuryou).ToString() + "未満");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }
                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warihuriJyuuryou))
                {
                    if (warihuriJyuuryou <= chouseiJyuuryou)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_JYUURYOU);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", (warihuriJyuuryou.ToString() + "未満"));
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }
                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateChouseiJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateChouseiJyuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 調整(%)入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateChouseiPercent(out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)))
                {
                    return true;
                }

                int warifuriNo = -1;
                short warifuriRowNo = -1;
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);
                }
                if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                    && warifuriNo < this.jyuuryouDtoList.Count
                    && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                {
                    // 割振系が更新されていないかチェック
                    if (Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)
                        == Convert.ToString(this.jyuuryouDtoList[warifuriNo][warifuriRowNo].chouseiPercent))
                    {
                        returnVal = true;
                        return returnVal;
                    }
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                {
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = string.Empty;
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }

                decimal chouseiPercent = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value), out chouseiPercent))
                {
                    if (100 <= chouseiPercent)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_PERCENT);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "100未満");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }

                    if (chouseiPercent == 0)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_PERCENT);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "1以上");
                        //this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateChouseiPercent", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateChouseiPercent", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 品名入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateHinmeiName(out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_NAME].EditedFormattedValue)))
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_HINMEI_NAME);

                        var cell = targetRow.Cells[CELL_NAME_HINMEI_NAME] as ICustomAutoChangeBackColor;
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "品名");
                        this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateHinmeiName", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateHinmeiName", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 重量値のフォーマットチェック
        /// </summary>
        /// <param name="targetRow">対象のRow</param>
        /// <param name="cellName">対象のCell名</param>
        /// <returns></returns>
        internal bool ValidateJyuryouFormat(Row targetRow, string cellName, out bool catchErr)
        {
            bool returnVal = false;
            catchErr = false;
            try
            {
                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[cellName].EditedFormattedValue)))
                {
                    decimal tempStackJyuuryou = 0;
                    if (!decimal.TryParse(targetRow.Cells[cellName].EditedFormattedValue.ToString(), out tempStackJyuuryou))
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, cellName);

                        var cell = targetRow.Cells[cellName] as ICustomAutoChangeBackColor;
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E084", targetRow.Cells[cellName].EditedFormattedValue.ToString());
                        return returnVal;
                    }
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateJyuryouFormat", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateJyuryouFormat", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 品名コードより品名再取得
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <returns>品名略称</returns>
        //internal string SearchHinmei(string hinmeiCd)
        //{
        //    LogUtility.DebugMethodStart(hinmeiCd);

        //    string returnValue = string.Empty;
        //    M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(hinmeiCd);
        //    if (hinmei != null && !string.IsNullOrEmpty(hinmei.HINMEI_NAME_RYAKU))
        //    {
        //        returnValue = hinmei.HINMEI_NAME_RYAKU;
        //    }

        //    LogUtility.DebugMethodEnd();
        //    return returnValue;
        //}

        /// <summary>
        /// 単位CD検索&設定
        /// </summary>
        /// <param name="hinmeiChangedFlg">品名CDが更新された後の処理かどうか</param>
        internal bool SearchAndCalcForUnit(bool hinmeiChangedFlg, Row targetRow)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(hinmeiChangedFlg, targetRow);

                if (targetRow == null)
                {
                    return true;
                }

                // 単価前回値取得
                var oldTanka = targetRow.Cells[CELL_NAME_TANKA].Value == null ? string.Empty : targetRow.Cells[CELL_NAME_TANKA].Value.ToString();

                M_UNIT targetUnit = null;

                if (hinmeiChangedFlg)
                {
                    // 品名CD更新時
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                    {
                        return true;
                    }

                    M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                    if (hinmei == null || string.IsNullOrEmpty(hinmei.HINMEI_CD))
                    {
                        return true;
                    }

                    if (targetRow.Cells[CELL_NAME_UNIT_CD].Value == null
                        || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_UNIT_CD].Value.ToString()))
                    {
                        M_UNIT[] units = null;
                        short UnitCd = 0;
                        if (short.TryParse(hinmei.UNIT_CD.ToString(), out UnitCd))
                            units = this.accessor.GetUnit(UnitCd);

                        if (units == null)
                        {
                            return true;
                        }
                        else
                        {
                            targetUnit = units[0];
                        }

                        if (string.IsNullOrEmpty(targetUnit.UNIT_NAME))
                        {
                            return true;
                        }

                        targetRow.Cells[CELL_NAME_UNIT_CD].Value = targetUnit.UNIT_CD.ToString();
                        targetRow.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = targetUnit.UNIT_NAME_RYAKU.ToString();
                    }
                }
                else
                {
                    // 単位CD更新時
                }

                /**
                 * 数量設定
                 */
                if (!this.CalcSuuryou(targetRow))     // 正味重量が変更になるタイミングあるため数量計算をメソッド化
                {
                    return false;
                }

                short denpyouKbn = 0;
                short unitCd = 0;
                if (!short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbn)
                    || !short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value), out unitCd))
                {
                    return true;
                }

                /**
                 * 単価設定
                 */
                var updateTanka = string.Empty; // MAILAN #158992 START
                var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                    (short)SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA,
                    Convert.ToInt16(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value),
                    this.form.TORIHIKISAKI_CD.Text,
                    this.form.GYOUSHA_CD.Text,
                    this.form.GENBA_CD.Text,
                    this.form.UNPAN_GYOUSHA_CD.Text,
                    null,
                    null,
                    Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value),
                    Convert.ToInt16(targetRow.Cells[CELL_NAME_UNIT_CD].Value),
                    this.form.DENPYOU_DATE.Text
                    );

                // 個別品名単価から情報が取れない場合は基本品名単価の検索
                if (kobetsuhinmeiTanka == null)
                {
                    var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                        (short)SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA,
                        Convert.ToInt16(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value),
                        this.form.UNPAN_GYOUSHA_CD.Text,
                        null,
                        null,
                        Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value),
                        Convert.ToInt16(targetRow.Cells[CELL_NAME_UNIT_CD].Value),
                        this.form.DENPYOU_DATE.Text
                        );
                    if (kihonHinmeiTanka != null)
                    {
                        updateTanka = kihonHinmeiTanka.TANKA.Value.ToString(); // MAILAN #158992 START
                    }
                    else
                    {
                        updateTanka = string.Empty; // MAILAN #158992 START
                    }
                }
                else
                {
                    updateTanka = kobetsuhinmeiTanka.TANKA.Value.ToString(); // MAILAN #158992 START
                }

                // MAILAN #158992 START
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    decimal oldTankaValue = -1;
                    decimal updateTankaValue = -1;
                    if (oldTanka != null && !string.IsNullOrEmpty(oldTanka.ToString()))
                    {
                        oldTankaValue = decimal.Parse(oldTanka.ToString());
                    }
                    if (updateTanka != null && !string.IsNullOrEmpty(updateTanka.ToString()))
                    {
                        updateTankaValue = decimal.Parse(updateTanka.ToString());
                    }

                    if (!oldTankaValue.Equals(updateTankaValue))
                    {
                        if (!this.isTankaMessageShown)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            if (msgLogic.MessageBoxShow("C127") == DialogResult.Yes)
                            {
                                targetRow.Cells[CELL_NAME_TANKA].Value = updateTanka;
                            }
                            else
                            {
                                this.ResetTankaCheck();
                                return false;
                            }
                            this.isTankaMessageShown = true;
                        }
                        else
                        {
                            targetRow.Cells[CELL_NAME_TANKA].Value = updateTanka;
                        }
                    }
                }
                // MAILAN #158992 END
                else // ban chuan
                {
                    targetRow.Cells[CELL_NAME_TANKA].Value = updateTanka.ToString();
                }

                /**
                 * 金額設定
                 */
                var newTanka = targetRow.Cells[CELL_NAME_TANKA].Value == null ? string.Empty : targetRow.Cells[CELL_NAME_TANKA].Value.ToString();

                // 単価に変更があった場合のみ再計算
                if (!oldTanka.Equals(newTanka))
                {
                    if (!this.CalcDetaiKingaku(targetRow))
                    {
                        return false;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 数量計算
        /// </summary>
        internal bool CalcSuuryou(Row targetRow)
        {
            bool ret = false;

            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return true;
                }

                if (targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value == null
                    || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString())
                    || targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value == null
                    || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))
                {
                    // 割振で作られた行は総、空車重量が入っていないためCELL_NAME_STAK_JYUURYOUなどでは
                    // チェックしきれないのでチェック
                    int parentJyuuryou = 0;
                    if (!targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly && 
                        int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_warihuriRowNo].Value), out parentJyuuryou)
                        && parentJyuuryou > 0)
                    {
                        // 割振で生成された行
                        // 割振で生成された行は正味重量が既に計算されているので以降の処理を続行してOK
                    }
                    else
                    {
                        // 総重量、空車重量双方ともに値が入力（０入力も含む）されているとき、数量を設定
                        // それ以外のときは設定しない
                        return true;
                    }
                }

                /**
                 * 数量設定
                 */
                if (string.Compare(SalesPaymentConstans.UNIT_CD_KG,
                    Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value), true) == 0)
                {
                    if (!this.IsRegist && !this.IsSuuryouKesannFlg)
                    {
                        // 数量に入力がない場合のみ、正味重量を数量にセットする
                        targetRow.Cells[CELL_NAME_SUURYOU].Value = targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value;
                    }
                }
                // No.2275
                else if (string.Compare(SalesPaymentConstans.UNIT_CD_TON,
                    Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value), true) == 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value)))
                    {
                        if (!this.IsRegist && !this.IsSuuryouKesannFlg)
                        {
                            decimal kg = Convert.ToDecimal(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value);
                            decimal ton = kg / 1000;
                            // 数量に入力がない場合のみ、正味重量を数量にセットする
                            targetRow.Cells[CELL_NAME_SUURYOU].Value = ton;
                        }
                    }
                }
                this.CalcDetaiKingaku(targetRow);//#159982
                targetRow.Cells[CELL_NAME_SUURYOU].UpdateBackColor(false);

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcSuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcSuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 明細金額計算
        /// </summary>
        internal bool CalcDetaiKingaku(Row targetRow, bool kingakuFlg = true)
        {
            /* 登録実行時に金額計算のチェック(CheckDetailKingakuメソッド)が実行されます。 　　         */
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(targetRow, kingakuFlg);

                if (targetRow == null)
                {
                    return true;
                }

                if (this.form.IsLoading)
                {
                    return true;
                }

                decimal suuryou = 0;
                decimal tanka = 0;
                // 金額端数の初期値は四捨五入としておく
                short kingakuHasuuCd = 3;

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_SUURYOU].FormattedValue), out suuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_TANKA].FormattedValue), out tanka);

                // 金額端数取得
                if (targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null)
                {
                    if (targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                    {
                        short.TryParse(Convert.ToString(this.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                    else if (targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                    {
                        short.TryParse(Convert.ToString(this.dto.torihikisakiShiharaiEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                }

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_SUURYOU].FormattedValue), out suuryou)
                && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_TANKA].FormattedValue), out tanka))
                {
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }

                    targetRow.Cells[CELL_NAME_KINGAKU].Value = kingaku;
                }
                else
                {
                    if (kingakuFlg)
                    {
                        targetRow.Cells[CELL_NAME_KINGAKU].Value = null;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcDetaiKingaku", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetaiKingaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 消費税率を売上支払日から取得して設定(明細用)
        /// </summary>
        /// <param name="targetRow"></param>
        internal bool SetShouhizeiRateForDetail(Row targetRow)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(targetRow);
                if (targetRow == null)
                {
                    return true;
                }

                DateTime uriageShiharaiDate = this.footerForm.sysDate;
                if (targetRow[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                    && DateTime.TryParse(targetRow[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString(), out uriageShiharaiDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageShiharaiDate.Date);
                    if (shouhizeiRate != null && !shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        targetRow[CELL_NAME_SHOUHIZEI_RATE].Value = shouhizeiRate.SHOUHIZEI_RATE;
                    }
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetShouhizeiRateForDetail", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShouhizeiRateForDetail", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 取引先の締日から明細.売上支払日を変更する
        /// UIFormから使用する場合はこちらを呼び出してください
        /// </summary>
        //internal void ChangeNearSeikyuDateForAllRow()
        //{
        //    LogUtility.DebugMethodStart();

        //    foreach (var targetRow in this.form.gcMultiRow1.Rows)
        //    {
        //        if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value)))
        //        {
        //            targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = this.GetNearSeikyuDateForDetail(targetRow);
        //        }
        //    }
        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// 取引先の締日から明細.売上支払日を変更する
        /// gcMultiRow1から使用する場合はこちらから呼び出してください
        /// </summary>
        //internal void ChangeNearSeikyuDateForSelectedRow()
        //{
        //    LogUtility.DebugMethodStart();

        //    Row targetRow = this.form.gcMultiRow1.CurrentRow;

        //    if (targetRow == null)
        //    {
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value)))
        //    {
        //        return;
        //    }

        //    this.form.gcMultiRow1.BeginEdit(false);
        //    targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = this.GetNearSeikyuDateForDetail(targetRow);
        //    this.form.gcMultiRow1.EndEdit();
        //    this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// 明細.売上支払日から近くの締日からを請求日(支払日)計算する
        /// </summary>
        /// <param name="targetRow">明細行</param>
        /// <returns>請求日または支払日</returns>
        //private DateTime GetNearSeikyuDateForDetail(Row targetRow)
        //{
        //    LogUtility.DebugMethodStart(targetRow);
        //    DateTime returnVal = (DateTime)targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;

        //    if (targetRow == null)
        //    {
        //        return returnVal;
        //    }

        //    // 締日取得
        //    List<short> shimebiList = new List<short>();
        //    short denpyouKbnCd = 0;
        //    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbnCd);
        //    if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
        //    {
        //        // 請求先
        //        if (this.dto.torihikisakiSeikyuuEntity == null)
        //        {
        //            return returnVal;
        //        }

        //        if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI1);
        //        if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI2);
        //        if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI3);
        //    }
        //    else if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
        //    {
        //        // 支払先
        //        if (this.dto.torihikisakiShiharaiEntity == null)
        //        {
        //            return returnVal;
        //        }

        //        if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI1);
        //        if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI2);
        //        if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI3);
        //    }
        //    if (shimebiList.Count < 1)
        //    {
        //        return returnVal;
        //    }

        //    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value)))
        //    {
        //        var seikyuuUtil = new SeiKyuuUtility();
        //        returnVal = (seikyuuUtil.GetNearSeikyuDate((DateTime)targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value, shimebiList.ToArray())).Date;
        //    }

        //    LogUtility.DebugMethodEnd();
        //    return returnVal;

        //}

        /// <summary>
        /// 売上日付を近々の締日に変更
        /// </summary>
        //internal void ChangeSeikyuuShimeDate()
        //{
        //    if (this.dto.torihikisakiSeikyuuEntity == null)
        //    {
        //        return;
        //    }

        //    List<short> shimebiList = new List<short>();
        //    if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI1);
        //    if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI2);
        //    if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI3);

        //    if (shimebiList.Count < 1)
        //    {
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(Convert.ToString(this.form.URIAGE_DATE.Value)))
        //    {
        //        var seikyuuUtil = new SeiKyuuUtility();
        //        this.form.URIAGE_DATE.Text = (seikyuuUtil.GetNearSeikyuDate((DateTime)this.form.URIAGE_DATE.Value, shimebiList.ToArray())).Date.ToString();
        //    }
        //}

        /// <summary>
        /// 支払日付を近々の締日に変更
        /// </summary>
        //internal void ChangeShiharaiShimeDate()
        //{
        //    if (this.dto.torihikisakiShiharaiEntity == null)
        //    {
        //        return;
        //    }

        //    List<short> shimebiList = new List<short>();
        //    if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI1);
        //    if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI2);
        //    if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI3);

        //    if (shimebiList.Count < 1)
        //    {
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(Convert.ToString(this.form.SHIHARAI_DATE.Value)))
        //    {
        //        var seikyuuUtil = new SeiKyuuUtility();
        //        this.form.SHIHARAI_DATE.Text = (seikyuuUtil.GetNearSeikyuDate((DateTime)this.form.SHIHARAI_DATE.Value, shimebiList.ToArray())).Date.ToString();
        //    }
        //}

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        public virtual void CheckKyotenCd()
        {
            // 初期化
            this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

            if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(string.Format("{0:#,0}", this.headerForm.KYOTEN_CD.Text), out kyoteCd))
            {
                return;
            }

            var kyotens = this.accessor.GetDataByCodeForKyoten(kyoteCd);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "拠点");
                this.headerForm.KYOTEN_CD.Focus();
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }
        }

        private string nizumiGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷積業者CDの存在チェック
        /// </summary>
        public virtual bool CheckNizumiGyoushaCd(out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputNioroshiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNioroshiGyoushaCd) || !this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd) ||
                       (this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd) && string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text)))     // No.4095(ID変更無い場合でもNAMEが空の場合はチェックするように変更)
                       || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
                    this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;
                    //this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                    //this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    //this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                    //this.form.NIZUMI_GENBA_NAME.TabStop = false;
                    //this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;
                    if (!this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                        this.form.NIZUMI_GENBA_NAME.TabStop = false;
                        this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;
                    }
                    // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                        this.form.NIZUMI_GENBA_NAME.TabStop = false;
                        this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }
                    else
                    {
                        var gyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (catchErr) { return ret; }
                        if (gyousha != null)
                        {
                            // PKは1つなので複数ヒットしない
                            // 20151026 BUNN #12040 STR
                            if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue
                                && (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue
                                || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            // 20151026 BUNN #12040 END
                            {
                                // 荷積業者名
                                this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                if (gyousha.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                    //this.form.NIZUMI_GYOUSHA_NAME.TabStop = true;
                                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");    // No.3822
                                    this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                                    this.form.NIZUMI_GYOUSHA_NAME.Focus();
                                }
                                else
                                {
                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }

                                // 入力済の荷積現場との関連チェック
                                bool isContinue = false;
                                M_GENBA genba = new M_GENBA();
                                if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(this.form.NIZUMI_GYOUSHA_CD.Text);
                                    if (genbaEntityList != null || genbaEntityList.Length >= 1)
                                    {
                                        foreach (M_GENBA genbaEntity in genbaEntityList)
                                        {
                                            // 20151026 BUNN #12040 STR
                                            if (this.form.NIZUMI_GENBA_CD.Text.Equals(genbaEntity.GENBA_CD)
                                                && (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue))
                                            // 20151026 BUNN #12040 END
                                            {
                                                isContinue = true;
                                                genba = genbaEntity;
                                                ret = true;
                                                break;
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            // 一致するものがないので、入力されている現場CDを消す
                                            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                                            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                                        }
                                        else
                                        {
                                            // 一致する現場CDがあれば、現場名を再設定する
                                            if (genba.SHOKUCHI_KBN.IsTrue)
                                            {
                                                this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                                this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                                                this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                                this.form.NIZUMI_GENBA_NAME.Focus();
                                            }
                                            else
                                            {
                                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // エラーメッセージ
                                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                //msgLogic.MessageBoxShow("E058", "");
                                msgLogic.MessageBoxShow("E020", "荷卸業者");
                                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                this.form.NIZUMI_GYOUSHA_CD.Focus();
                                isError = true;
                            }
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "荷積業者");
                            this.form.NIZUMI_GYOUSHA_CD.Focus();
                            isError = true;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        private string nizumiGenbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷積現場CDの存在チェック
        /// </summary>
        internal bool CheckNizumiGenbaCd(out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputNIZUMIGenbaCd = this.form.NIZUMI_GENBA_CD.Text;

                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNIZUMIGenbaCd) || !this.tmpNizumiGenbaCd.Equals(inputNIZUMIGenbaCd)) ||
                    (this.tmpNizumiGenbaCd.Equals(inputNIZUMIGenbaCd) && string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))     // No.4095(ID変更無い場合でもNAMEが空の場合はチェックするように変更)
                    || this.form.isFromSearchButton)
                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                {
                    // 初期化
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                    this.form.NIZUMI_GENBA_NAME.TabStop = false;
                    this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

                    if (string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                    {
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        if (this.form.oldShokuchiKbn)
                        {
                            ret = true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                        {
                            msgLogic.MessageBoxShow("E051", "荷積業者");
                            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                            return ret;
                        }

                        //var genbaEntityList = this.accessor.GetGenba(this.form.NIZUMI_GENBA_CD.Text);
                        var genbaEntityList = this.accessor.GetGenbaList(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        M_GENBA genba = new M_GENBA();

                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            msgLogic.MessageBoxShow("E020", "荷積現場");
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                        }
                        else
                        {
                            //genba = this.accessor.GetGenba(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text);
                            genba = genbaEntityList[0];
                            // 荷積業者名入力チェック
                            if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                            {
                                // エラーメッセージ
                                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                //msgLogic.MessageBoxShow("E051", "荷積業者");
                                msgLogic.MessageBoxShow("E020", "荷積現場");
                                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                                this.form.NIZUMI_GENBA_CD.Focus();
                                isError = true;
                            }
                            // 荷積業者と荷積現場の関連チェック
                            else if (genba == null)
                            {
                                // 一致するデータがないのでエラー
                                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                //msgLogic.MessageBoxShow("E062", "荷積業者");
                                msgLogic.MessageBoxShow("E020", "荷積現場");
                                // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                this.form.NIZUMI_GENBA_CD.Focus();
                                isError = true;
                            }
                            else
                            {
                                // 業者設定
                                var gyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                                if (catchErr) { return ret; }
                                if (gyousha != null)
                                {
                                    // PKは1つなので複数ヒットしない
                                    // 20151026 BUNN #12040 STR
                                    if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue
                                        && (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue
                                        || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    // 20151026 BUNN #12040 END
                                    {
                                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        // 荷卸業者名
                                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        if (gyousha.SHOKUCHI_KBN.IsTrue)
                                        {
                                            this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                            this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");
                                            this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                                        }
                                    }
                                }

                                // 事業場区分、現場区分チェック
                                // 20151026 BUNN #12040 STR
                                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                                // 20151026 BUNN #12040 END
                                {
                                    this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                                    // 諸口区分チェック
                                    if (genba.SHOKUCHI_KBN.IsTrue)
                                    {
                                        // 荷積現場名編集可
                                        this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                        this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                        //this.form.NIZUMI_GENBA_NAME.TabStop = true;
                                        this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                                        this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                        this.form.NIZUMI_GENBA_NAME.Focus();
                                    }

                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }
                                else
                                {
                                    // 一致するデータがないのでエラー
                                    this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                                    // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                    //msgLogic.MessageBoxShow("E058", "");
                                    msgLogic.MessageBoxShow("E020", "荷積現場");
                                    // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                    this.form.NIZUMI_GENBA_CD.Focus();
                                    isError = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        private string unpanGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 運搬業者CDの存在チェック
        /// </summary>
        public virtual bool CheckUnpanGyoushaCd(out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputUnpanGyoushaCd) || !this.tmpUnpanGyoushaCd.Equals(inputUnpanGyoushaCd)) || this.form.isFromSearchButton || this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured)
                {
                    // 初期化
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
                    this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
                    this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;
                    var gyousha = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr) { return ret; }
                    if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                    {
                        if (gyousha != null)
                        {
                            // 20151026 BUNN #12040 STR
                            if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue && gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                            // 20151026 BUNN #12040 END
                            {
                                M_SHARYOU[] sharyouEntitys = null;
                                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
                                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
                                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END
                                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                                if (sharyouEntitys == null)
                                {
                                    if (!this.form.oldSharyouShokuchiKbn)
                                    {
                                        // 車輌・車種をクリア
                                        this.form.SHARYOU_CD.Text = string.Empty;
                                        this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                                        this.form.KUUSHA_JYURYO.Text = string.Empty;
                                    }
                                    else
                                    {
                                        // 車輌名を編集可
                                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
                                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                                    }
                                }
                                else if (sharyouEntitys != null)
                                {
                                    var sharyouEntity = sharyouEntitys[0];
                                    this.form.SHARYOU_CD.Text = sharyouEntity.SHARYOU_CD;
                                    this.form.oldSharyouShokuchiKbn = false;
                                    this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
                                    this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                                    this.form.KUUSHA_JYURYO.Text = sharyouEntity.KUUSHA_JYURYO.IsNull ? string.Empty : sharyouEntity.KUUSHA_JYURYO.ToString();

                                    // 運転者情報セット
                                    var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
                                    if (untensha != null)
                                    {
                                        this.form.UNTENSHA_CD.Text = untensha.SHAIN_CD;
                                        this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;
                                    }
                                    else
                                    {
                                        this.form.UNTENSHA_CD.Text = string.Empty;
                                        this.form.UNTENSHA_NAME.Text = string.Empty;
                                    }

                                    // 車輌情報セット
                                    var shashuEntity = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
                                    if (shashuEntity != null)
                                    {
                                        this.form.SHASHU_CD.Text = shashuEntity.SHASHU_CD;
                                        this.form.SHASHU_NAME.Text = shashuEntity.SHASHU_NAME_RYAKU;
                                    }
                                    else
                                    {
                                        this.form.SHASHU_CD.Text = string.Empty;
                                        this.form.SHASHU_NAME.Text = string.Empty;
                                    }
                                }

                                this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                // 諸口区分チェック
                                if (gyousha.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 運搬業者名編集可
                                    this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                    //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                                    this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
                                    this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                                }
                                else
                                {
                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E020", "業者");
                                this.form.UNPAN_GYOUSHA_CD.Focus();
                                this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                                isError = true;
                            }
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "業者");
                            this.form.UNPAN_GYOUSHA_CD.Focus();
                            this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                            isError = true;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.tmpUnpanGyoushaCd) && !this.form.oldSharyouShokuchiKbn)
                        {
                            this.form.SHARYOU_CD.Text = string.Empty;
                            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                            this.form.KUUSHA_JYURYO.Text = string.Empty;
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpUnpanGyoushaCd.Equals(inputUnpanGyoushaCd))
                        {
                            //検収済みの場合は、単価再設定をしない
                            if (this.form.KENSHU_MUST_KBN.Checked && (this.kenshuZumi.Equals(this.form.txtKensyuu.Text)))
                            {
                                DialogResult dr = msgLogic.MessageBoxShowInformation("単価に関連する項目の変更が行われました。検収入力画面で登録した単価の確認を行ってください。");
                            }
                            else
                            {
                                // 明細行すべての単価を再設定
                                //this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r => this.SearchAndCalcForUnit(false, r));
                                var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();
                                foreach (Row dr in list)
                                {
                                    if (!this.SearchAndCalcForUnit(false, dr))
                                    {
                                        return false;
                                    }
                                }
                                this.ResetTankaCheck(); // MAILAN #158992 START

                                // 合計金額の再計算
                                if (!this.CalcTotalValues())
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        /// <summary>
        /// 入力担当者チェック
        /// </summary>
        internal bool CheckNyuuryokuTantousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                strNyuryokuTantousyaName = string.Empty;  // No.3279

                if (string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    // 入力担当者CDがなければ既にエラーが表示されているはずなので何もしない
                    return true;
                }

                var shainEntity = this.accessor.GetShain(this.form.NYUURYOKU_TANTOUSHA_CD.Text);
                if (shainEntity == null)
                {
                    return true;
                }
                else if (shainEntity.NYUURYOKU_TANTOU_KBN.IsFalse)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.NYUURYOKU_TANTOUSHA_CD.Focus();
                    return true;
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    strNyuryokuTantousyaName = shainEntity.SHAIN_NAME;    // No.3279
                }
                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private string torihikisakiHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki(out bool catchErr)
        {
            catchErr = false;
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                ret = true;
                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var oldTorihikisakiCd = this.tmpTorihikisakiCd;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                //if ((String.IsNullOrEmpty(inputTorihikisakiCd) || !this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd)) || this.form.isFromSearchButton)   // No.4095
                if (this.form.isInputError || (String.IsNullOrEmpty(inputTorihikisakiCd) || !this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) ||
                    (this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) && string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text)))     // No.4095(ID変更無い場合でもNAMEが空の場合はチェックするように変更)
                    || this.form.isFromSearchButton)
                {
                    //　初期化
                    //this.tmpTorihikisakiCd = string.Empty;
                    this.form.isInputError = false;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                    this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;
                    this.form.SEIKYUU_SHIMEBI1.Text = string.Empty;
                    this.form.SEIKYUU_SHIMEBI2.Text = string.Empty;
                    this.form.SEIKYUU_SHIMEBI3.Text = string.Empty;
                    this.form.SHIHARAI_SHIMEBI1.Text = string.Empty;
                    this.form.SHIHARAI_SHIMEBI2.Text = string.Empty;
                    this.form.SHIHARAI_SHIMEBI3.Text = string.Empty;
                    this.form.txtUri.Text = string.Empty;
                    this.form.txtShi.Text = string.Empty;

                    if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                    {
                        var torihikisakiEntity = this.accessor.GetTorihikisaki(inputTorihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (catchErr)
                        {
                            return false;
                        }
                        if (null == torihikisakiEntity)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "取引先");
                            this.form.TORIHIKISAKI_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            if (CheckTorihikisakiAndKyotenCd(torihikisakiEntity, this.form.TORIHIKISAKI_CD.Text))
                            {
                                // 取引先の拠点と入力された拠点コードの関連チェックOK
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                                this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;
                            }
                            else
                            {
                                this.form.isInputError = true;
                                this.form.TORIHIKISAKI_CD.Focus();
                                isError = true;
                                ret = false;
                            }
                        }

                        if (ret)
                        {
                            // 取引先名
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                            this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;

                            // 諸口区分チェック
                            if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 取引先名編集可
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                                //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = true;
                                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");    // No.3822
                                this.form.TORIHIKISAKI_NAME_RYAKU.Tag = this.torihikisakiHintText;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Focus();

                                ret = false;
                            }
                            else
                            {
                                if (!this.form.oldShokuchiKbn)
                                {
                                    ret = false;
                                }
                            }

                            // 請求締日チェック
                            this.CheckSeikyuuShimebi();

                            // 支払い締日チェック
                            this.CheckShiharaiShimebi();

                            //取引区分チェック
                            this.CheckTorihikiKBN();
                        }
                    }
                    else
                    {
                        // 関連項目クリア
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }

                    if (!isError)
                    {
                        if (!oldTorihikisakiCd.Equals(inputTorihikisakiCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }

                        if (!oldTorihikisakiCd.Equals(inputTorihikisakiCd))
                        {

                            //検収済みの場合は、単価再設定をしない
                            if (this.form.KENSHU_MUST_KBN.Checked && (this.kenshuZumi.Equals(this.form.txtKensyuu.Text)))
                            {
                                DialogResult dr = msgLogic.MessageBoxShowInformation("単価に関連する項目の変更が行われました。検収入力画面で登録した単価の確認を行ってください。");
                                this.bolPOPTan = true;
                            }
                            else
                            {
                                if (!this.isCheckTankaFromChild) // MAILAN #158992 START
                                {
                                    // 明細行すべての単価を再設定
                                    var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                                    foreach (Row dr in list)
                                    {
                                        if (!this.SearchAndCalcForUnit(false, dr))
                                        {
                                            return false; // MAILAN #158992 START
                                        }
                                    }
                                    this.ResetTankaCheck(); // MAILAN #158992 START

                                    // 合計金額の再計算
                                    if (!this.CalcTotalValues())
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("CheckTorihikisaki", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckTorihikisaki", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        /// <summary>
        /// 取引先の拠点コードと入力された拠点コードの関連チェック
        /// </summary>
        /// <param name="torihikisakiEntity">取引先エンティティ</param>
        /// <param name="TorihikisakiCd">取引先CD</param>
        /// <returns>True：チェックOK False：チェックNG</returns>
        internal bool CheckTorihikisakiAndKyotenCd(M_TORIHIKISAKI torihikisakiEntity, string TorihikisakiCd)
        {
            bool returnVal = false;

            if (string.IsNullOrEmpty(TorihikisakiCd))
            {
                // 取引先の入力がない場合はチェック対象外
                returnVal = true;
                return returnVal;
            }

            if (torihikisakiEntity == null)
            {
                // 取引先マスタを引数の取引先CDで取得しなおす
                bool catchErr = false;
                torihikisakiEntity = this.accessor.GetTorihikisaki(TorihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { return returnVal; }
            }

            if (torihikisakiEntity != null)
            {
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    if (SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text) == torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD
                        || torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString().Equals(SalesPaymentConstans.KYOTEN_ZENSHA))
                    {
                        // 入力画面の拠点コードと取引先の拠点コードが等しいか、取引先の拠点コードが99（全社)の場合
                        returnVal = true;
                    }
                    else
                    {
                        // 入力画面の拠点コードと取引先の拠点コードが等しくない場合
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E146");
                        this.form.TORIHIKISAKI_CD.Focus();
                    }
                }
                else
                {   // 拠点が指定されていない場合
                    returnVal = true;   // No.2865
                }
            }
            else
            {
                returnVal = true;
            }

            return returnVal;
        }

        /// <summary>
        /// 請求締日チェック
        /// </summary>
        internal void CheckSeikyuuShimebi()
        {
            LogUtility.DebugMethodStart();
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.form.SHIHARAI_SHIMEBI1.Text = "";
                this.form.SHIHARAI_SHIMEBI2.Text = "";
                this.form.SHIHARAI_SHIMEBI3.Text = "";
                return;
            }

            var torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);
            if (torihikisakiSeikyuuEntity != null)
            {
                // 締日1
                if (!torihikisakiSeikyuuEntity.SHIMEBI1.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI1.Text = torihikisakiSeikyuuEntity.SHIMEBI1.ToString();
                }

                // 締日2
                if (!torihikisakiSeikyuuEntity.SHIMEBI2.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI2.Text = torihikisakiSeikyuuEntity.SHIMEBI2.ToString();
                }

                // 締日3
                if (!torihikisakiSeikyuuEntity.SHIMEBI3.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI3.Text = torihikisakiSeikyuuEntity.SHIMEBI3.ToString();
                }
            }

            // DBアクセスしないようここで設定
            this.dto.torihikisakiSeikyuuEntity = torihikisakiSeikyuuEntity;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 支払締日チェック
        /// </summary>
        internal void CheckShiharaiShimebi()
        {
            LogUtility.DebugMethodStart();
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.form.SHIHARAI_SHIMEBI1.Text = "";
                this.form.SHIHARAI_SHIMEBI2.Text = "";
                this.form.SHIHARAI_SHIMEBI3.Text = "";
                return;
            }

            var torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
            if (torihikisakiShiharaiEntity != null)
            {
                // 締日1
                if (!torihikisakiShiharaiEntity.SHIMEBI1.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI1.Text = torihikisakiShiharaiEntity.SHIMEBI1.ToString();
                }

                // 締日2
                if (!torihikisakiShiharaiEntity.SHIMEBI2.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = torihikisakiShiharaiEntity.SHIMEBI2.ToString();
                }

                // 締日3
                if (!torihikisakiShiharaiEntity.SHIMEBI3.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = torihikisakiShiharaiEntity.SHIMEBI3.ToString();
                }
            }

            // DBアクセスをなくすためここで設定
            this.dto.torihikisakiShiharaiEntity = torihikisakiShiharaiEntity;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引区分チェック
        /// </summary>
        internal void CheckTorihikiKBN()
        {
            LogUtility.DebugMethodStart();

            string seikyuuKBN;
            string shiharaiKBN;

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    seikyuuKBN = this.accessor.GetTrihikisakiKBN_Seikyuu(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuuKBN == "1")
                    {
                        //1.現金
                        this.form.txtUri.Text = "現金";
                    }
                    else if (seikyuuKBN == "2")
                    {
                        //2.掛け
                        this.form.txtUri.Text = "掛け";
                    }
                    else
                    {
                        this.form.txtUri.Text = "";
                    }

                    shiharaiKBN = this.accessor.GetTrihikisakiKBN_Shiharai(this.form.TORIHIKISAKI_CD.Text);
                    if (shiharaiKBN == "1")
                    {
                        //1.現金
                        this.form.txtShi.Text = "現金";
                    }
                    else if (shiharaiKBN == "2")
                    {
                        //2.掛け
                        this.form.txtShi.Text = "掛け";
                    }
                    else
                    {
                        this.form.txtShi.Text = "";
                    }
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        private string gyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha(out bool catchErr)
        {
            catchErr = false;
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputGyoushaCd = this.form.GYOUSHA_CD.Text;

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                this.bolPOPTan = false;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                //if ((String.IsNullOrEmpty(inputGyoushaCd) || !this.tmpGyousyaCd.Equals(inputGyoushaCd)) || this.form.isFromSearchButton)     // No.4095
                if (this.form.isInputError || (String.IsNullOrEmpty(inputGyoushaCd) || !this.tmpGyousyaCd.Equals(inputGyoushaCd) ||
                    (this.tmpGyousyaCd.Equals(inputGyoushaCd) && string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text)))     // No.4095(ID変更無い場合でもNAMEが空の場合はチェックするように変更)
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    //this.tmpGyousyaCd = string.Empty;
                    this.form.isInputError = false;
                    this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                    this.form.GYOUSHA_NAME_RYAKU.Tag = String.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.TabStop = false;
                    // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    //this.form.GENBA_CD.Text = String.Empty;
                    //this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                    //this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                    //this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                    //this.form.GENBA_NAME_RYAKU.TabStop = false;
                    if (!this.tmpGyousyaCd.Equals(inputGyoushaCd))
                    {
                        this.form.GENBA_CD.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                        this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                        this.form.GENBA_NAME_RYAKU.TabStop = false;
                    }
                    // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    if (String.IsNullOrEmpty(inputGyoushaCd))
                    {
                        // 同時に現場コードもクリア
                        this.form.GENBA_CD.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                        this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                        this.form.GENBA_NAME_RYAKU.TabStop = false;
                        strGenbaName = string.Empty;   // No.3279

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }
                    else
                    {
                        var gyoushaEntity = this.accessor.GetGyousha(inputGyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (catchErr)
                        {
                            return false;
                        }
                        if (null == gyoushaEntity)
                        {
                            // エラーメッセージ
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            //msgLogic.MessageBoxShow("E011", "業者マスタ");
                            msgLogic.MessageBoxShow("E020", "業者");
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                            this.form.GYOUSHA_CD.Focus();
                            this.form.isInputError = true;
                            isError = true;
                            ret = false;
                        }
                        else if (false == gyoushaEntity.GYOUSHAKBN_SHUKKA)
                        {
                            // エラーメッセージ
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            //msgLogic.MessageBoxShow("E058");
                            msgLogic.MessageBoxShow("E020", "業者");
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                            this.form.GYOUSHA_CD.Focus();
                            this.form.isInputError = true;
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            // 業者名
                            this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;

                            // 諸口区分チェック
                            if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 業者名編集可
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GYOUSHA_NAME_RYAKU.Focus();

                                ret = false;
                            }
                            else
                            {
                                if (!this.form.oldShokuchiKbn)
                                {
                                    ret = false;
                                }
                            }

                            // 取引先を取得
                            var torihikisakiEntity = this.accessor.GetTorihikisaki(gyoushaEntity.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }
                            if (null != torihikisakiEntity)
                            {
                                this.form.TORIHIKISAKI_CD.Text = gyoushaEntity.TORIHIKISAKI_CD;
                                this.isCheckTankaFromChild = true; // MAILAN #158992 START
                                // 取引先チェック呼び出し
                                ret = this.CheckTorihikisaki(out catchErr);
                                if (catchErr)
                                {
                                    throw new Exception("");
                                }
                            }

                            if (true == ret)
                            {
                                // 現場が入力されていれば現場との関連チェック
                                var genbaCd = this.form.GENBA_CD.Text;
                                if (!String.IsNullOrEmpty(genbaCd))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(inputGyoushaCd);
                                    var genbaEntity = genbaEntityList.Where(g => g.GENBA_CD == genbaCd).FirstOrDefault();
                                    if (null != genbaEntity)
                                    {
                                        // 現場チェック呼び出し
                                        ret = this.CheckGenba(out catchErr);
                                        if (catchErr)
                                        {
                                            throw new Exception("");
                                        }
                                    }
                                    else
                                    {
                                        // 一致するものがなければ、入力されている現場を消す
                                        this.form.GENBA_CD.Text = String.Empty;
                                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                                    }
                                }
                            }
                            // 諸口区分チェック
                            this.form.isSetShokuchiForcus = false;
                            if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 現場を再設定
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GYOUSHA_NAME_RYAKU.Focus();
                                this.form.isSetShokuchiForcus = true;
                            }
                        }
                    }

                    if (!isError)
                    {
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (!this.tmpGyousyaCd.Equals(inputGyoushaCd) && this.form.validateFlag)
                        {
                            bool flag = false;
                            foreach (Row row in this.form.gcMultiRow1.Rows)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value)))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!this.hasShow && this.form.gcMultiRow1.Rows.Count > 1 && flag)
                            {
                                msgLogic = new MessageBoxShowLogic();
                                if (this.form.KENSHU_MUST_KBN.Checked)
                                {
                                    DialogResult dr = msgLogic.MessageBoxShowInformation("品名の再読込は実行されません。品名を入力し直す場合は、検収入力画面で直接修正してください。");
                                }
                                else
                                {

                                    // currentCellが単価再読み込みや、再計算の対象だった場合、
                                    // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                                    if (this.form.gcMultiRow1.CurrentCell != null
                                        && (this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_TANKA)
                                        || this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_KINGAKU)))
                                    {
                                        rowindex = this.form.gcMultiRow1.CurrentRow.Index;
                                        cellindex = this.form.gcMultiRow1.CurrentCell.CellIndex;
                                        this.form.gcMultiRow1.CurrentCell = null;
                                        isChageCurrentCell = true;
                                    }

                                    DialogResult dr = msgLogic.MessageBoxShow("C097", "業者");
                                    if (dr == DialogResult.OK || dr == DialogResult.Yes)
                                    {
                                        this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r => this.GetHinmeiForPop(r));
                                    }
                                }
                            }
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end

                        if (!this.tmpGyousyaCd.Equals(inputGyoushaCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }

                        if (!this.tmpGyousyaCd.Equals(inputGyoushaCd))
                        {
                            //検収済み且つ、取引先の方でPOPを出していない場合は、単価再設定をしない
                            if (this.form.KENSHU_MUST_KBN.Checked && (this.kenshuZumi.Equals(this.form.txtKensyuu.Text)))
                            {
                                if (!this.bolPOPTan)
                                {
                                    DialogResult dr = msgLogic.MessageBoxShowInformation("単価に関連する項目の変更が行われました。検収入力画面で登録した単価の確認を行ってください。");
                                }
                            }
                            else
                            {
                                // 明細行すべての単価を再設定
                                var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                                foreach (Row dr in list)
                                {
                                    if (!this.SearchAndCalcForUnit(false, dr))
                                    {
                                        return false; // MAILAN #158992 START
                                    }
                                }
                                this.ResetTankaCheck(); // MAILAN #158992 START

                                // 合計金額の再計算
                                if (!this.CalcTotalValues())
                                {
                                    return false;
                                }
                            }
                        }

                        //ThangNguyen [Add] 20150818 #11065 Start
                        if (this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly == false && this.form.GYOUSHA_CD.Text != "" && this.form.GYOUSHA_NAME_RYAKU.Text != "")
                        {
                            //this.form.TORIHIKISAKI_NAME_RYAKU.BackColor = SystemColors.Window;
                            //this.form.GYOUSHA_CD.Focus();
                        }
                        //ThangNguyen [Add] 20150818 #11065 End
                    }
                }
                else
                {
                    ret = false;
                }

                if (isChageCurrentCell)
                {
                    this.form.gcMultiRow1.CurrentCell = this.form.gcMultiRow1.Rows[rowindex].Cells[cellindex];
                }

            }
            catch (SQLRuntimeException ex2)
            {
                if (!string.IsNullOrEmpty(ex2.Message))
                {
                    LogUtility.Error("CheckGyousha", ex2);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                catchErr = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckGyousha", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        private string genbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputGenbaCd = this.form.GENBA_CD.Text;
                var inputGyoushaCd = this.form.GYOUSHA_CD.Text;
                bool isContinue = false;

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                this.bolPOPTan = false;

                var gyoushaEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    return false;
                }

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if (this.form.isInputError || (String.IsNullOrEmpty(inputGenbaCd) || !this.tmpGenbaCd.Equals(inputGenbaCd)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    //this.tmpGenbaCd = string.Empty;
                    this.form.isInputError = false;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                    this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
                    this.form.GENBA_NAME_RYAKU.TabStop = false;

                    if (String.IsNullOrEmpty(inputGenbaCd))
                    {
                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(inputGyoushaCd))
                        {
                            msgLogic.MessageBoxShow("E051", "業者");
                            this.form.GENBA_CD.Text = string.Empty;
                            this.form.GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                            return ret;
                        }

                        //var genbaEntityList = this.accessor.GetGenba(inputGenbaCd);
                        var genbaEntityList = this.accessor.GetGenbaList(inputGyoushaCd, inputGenbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            // エラーメッセージ
                            this.form.isInputError = true;
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            //msgLogic.MessageBoxShow("E011", "現場マスタ");
                            msgLogic.MessageBoxShow("E020", "現場");
                            // 20150922 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                            this.form.GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            M_GENBA genba = new M_GENBA();
                            //foreach (M_GENBA genbaEntity in genbaEntityList)
                            //{
                            //    if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                            //    {
                            //        isContinue = true;
                            //        genba = genbaEntity;
                            //        this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            //        strGenbaName = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;   // No.3279
                            //        break;
                            //    }
                            //}
                            //if (!isContinue)
                            //{
                            //    // 一致するものがないのでエラー
                            //    this.form.GENBA_CD.IsInputErrorOccured = true;
                            //    msgLogic.MessageBoxShow("E062", "業者");
                            //    this.form.GENBA_CD.Focus();
                            //    ret = false;
                            //}
                            //else if (null == gyoushaEntity)
                            //{
                            //    ret = false;
                            //}
                            genba = genbaEntityList[0];
                            this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                            strGenbaName = genba.GENBA_NAME1 + genba.GENBA_NAME2;

                            if (null == gyoushaEntity)
                            {
                                ret = false;
                            }
                            // 業者の諸口区分チェック
                            else if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 業者名編集可
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GENBA_NAME_RYAKU.Focus();
                            }
                            else
                            {
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                            }

                            // 取引先を取得
                            M_TORIHIKISAKI torihikisakiEntity = null;
                            if (!string.IsNullOrEmpty(genba.TORIHIKISAKI_CD))
                            {
                                //torihikisakiEntity = this.accessor.GetTorihikisaki(genba.TORIHIKISAKI_CD);

                                r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
                                var keyEntity = new M_TORIHIKISAKI();
                                keyEntity.TORIHIKISAKI_CD = genba.TORIHIKISAKI_CD;
                                torihikisakiEntity = torihikisakiDao.GetAllValidData(keyEntity).FirstOrDefault();

                                if (torihikisakiEntity != null)
                                {
                                    // 取引先設定
                                    this.form.TORIHIKISAKI_CD.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                                    if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                                    {
                                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                                    }
                                    else
                                    {
                                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                                    }
                                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                                    this.form.pressedEnterOrTab = false;
                                    this.isCheckTankaFromChild = true; // MAILAN #158992 START
                                    ret = this.CheckTorihikisaki(out catchErr);
                                    if (catchErr)
                                    {
                                        throw new Exception("");
                                    }
                                }
                            }

                            // TODO: 【2次】営業担当者チェックの呼び出し

                            // 現場：諸口区分チェック
                            this.form.isSetShokuchiForcus = false;
                            if (genba.SHOKUCHI_KBN.IsTrue)
                            {
                                // 現場名編集可
                                this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME1;
                                this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                                //this.form.GENBA_NAME_RYAKU.TabStop = true;
                                this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
                                this.form.GENBA_NAME_RYAKU.Tag = genbaHintText;
                                this.form.GENBA_CD.Focus();
                                this.form.isSetShokuchiForcus = true;
                            }

                            //// Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                            if (ret)
                                this.MoveToNextControlForShokuchikbnCheck(this.form.GENBA_CD);

                            ret = true;

                            // マニ種類の自動表示
                            // 初期化
                            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;

                            if (!genba.MANIFEST_SHURUI_CD.IsNull)
                            {
                                var manifestShuruiEntity = this.accessor.GetManifestShurui(genba.MANIFEST_SHURUI_CD);
                                if (manifestShuruiEntity != null && !string.IsNullOrEmpty(manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU))
                                {
                                    this.form.MANIFEST_SHURUI_CD.Text = Convert.ToString(genba.MANIFEST_SHURUI_CD);
                                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
                                }
                            }

                            // マニ手配の自動表示
                            // 初期化
                            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;

                            if (!genba.MANIFEST_TEHAI_CD.IsNull)
                            {
                                var manifestTehaiEntity = this.accessor.GetManifestTehai(genba.MANIFEST_TEHAI_CD);
                                if (manifestTehaiEntity != null && !string.IsNullOrEmpty(manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                                {
                                    this.form.MANIFEST_TEHAI_CD.Text = Convert.ToString(genba.MANIFEST_TEHAI_CD);
                                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                                }
                            }
                        }
                    }

                    if (!isError)
                    {
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (!this.tmpGenbaCd.Equals(inputGenbaCd) && this.form.validateFlag)
                        {
                            bool flag = false;
                            foreach (Row row in this.form.gcMultiRow1.Rows)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value)))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (this.form.gcMultiRow1.Rows.Count > 1 && flag)
                            {
                                msgLogic = new MessageBoxShowLogic();
                                if (this.form.KENSHU_MUST_KBN.Checked)
                                {
                                    DialogResult dr = msgLogic.MessageBoxShowInformation("品名の再読込は実行されません。品名を入力し直す場合は、検収入力画面で直接修正してください。");
                                }
                                else
                                {
                                    // currentCellが単価再読み込みや、再計算の対象だった場合、
                                    // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                                    if (this.form.gcMultiRow1.CurrentCell != null
                                        && (this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_TANKA)
                                        || this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_KINGAKU)))
                                    {
                                        rowindex = this.form.gcMultiRow1.CurrentRow.Index;
                                        cellindex = this.form.gcMultiRow1.CurrentCell.CellIndex;
                                        this.form.gcMultiRow1.CurrentCell = null;
                                        isChageCurrentCell = true;
                                    }

                                    DialogResult dr = msgLogic.MessageBoxShow("C097", "現場");
                                    if (dr == DialogResult.OK || dr == DialogResult.Yes)
                                    {
                                        this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r => this.GetHinmeiForPop(r));
                                    }
                                }
                            }
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end

                        if (!this.tmpGenbaCd.Equals(inputGenbaCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }

                        if (!this.tmpGenbaCd.Equals(inputGenbaCd))
                        {
                            //検収済み且つ、取引先の方でPOPを出していない場合は、単価再設定をしない
                            if (this.form.KENSHU_MUST_KBN.Checked && (this.kenshuZumi.Equals(this.form.txtKensyuu.Text)))
                            {
                                if (!this.bolPOPTan)
                                {
                                    DialogResult dr = msgLogic.MessageBoxShowInformation("単価に関連する項目の変更が行われました。検収入力画面で登録した単価の確認を行ってください。");
                                }
                            }
                            else
                            {
                                // 明細行すべての単価を再設定
                                var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                                foreach (Row dr in list)
                                {
                                    if (!this.SearchAndCalcForUnit(false, dr))
                                    {
                                        return false; // MAILAN #158992 START
                                    }
                                }
                                this.ResetTankaCheck(); // MAILAN #158992 START

                                // 合計金額の再計算
                                if (!this.CalcTotalValues())
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    ret = false;
                }

                if (isChageCurrentCell)
                {
                    this.form.gcMultiRow1.CurrentCell = this.form.gcMultiRow1.Rows[rowindex].Cells[cellindex];
                }
            }
            catch (SQLRuntimeException ex2)
            {
                if (!string.IsNullOrEmpty(ex2.Message))
                {
                    LogUtility.Error("CheckGenba", ex2);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckGenba", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        /// <summary>
        /// 営業担当者チェック
        /// </summary>
        internal bool CheckEigyouTantousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    // 営業担当者CDがなければ既にエラーが表示されているので何もしない。
                    return true;
                }

                var shainEntity = this.accessor.GetShain(this.form.EIGYOU_TANTOUSHA_CD.Text);
                if (shainEntity == null)
                {
                    return true;
                }
                else if (shainEntity.EIGYOU_TANTOU_KBN.Equals(SqlBoolean.False))
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "営業担当者");
                    this.form.EIGYOU_TANTOUSHA_CD.Focus();
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckEigyouTantousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyouTantousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal string tmpTorihikisakiCd = string.Empty;
        internal string tmpGyousyaCd = string.Empty;
        internal string tmpNizumiGyoushaCd = string.Empty;
        internal string tmpNizumiGenbaCd = string.Empty;
        internal string tmpUnpanGyoushaCd = string.Empty;
        internal string tmpGenbaCd = string.Empty;  // No.3587
        internal string tmpKeitaiKbnCd = string.Empty;
        internal string tmpUntenshaCd = string.Empty;
        private string sharyouCd = string.Empty;
        private string shaShuCd = string.Empty;
        private string unpanGyousha = string.Empty;
        private Color sharyouCdBackColor = Color.FromArgb(255, 235, 160);
        private Color sharyouCdBackColorBlue = Color.FromArgb(0, 255, 255);
        internal string searchSendParamKeyNameForSharyouCd = "key002";
        private string sharyouHinttext = "全角10桁以内で入力してください";
        internal string tmpDenpyouDate = string.Empty;


        /// <summary>
        /// 取引先CD初期セット
        /// </summary>
        internal void TorihikisakiCdSet()
        {
            tmpTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
        }

        /// <summary>
        /// 業者CD初期セット
        /// </summary>
        internal void GyousyaCdSet()
        {
            tmpGyousyaCd = this.form.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積業者CD初期セット
        /// </summary>
        internal void NizumiGyoushaCdSet()
        {
            tmpNizumiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場CD初期セット
        /// </summary>
        internal void NizumiGenbaCdSet()
        {
            tmpNizumiGenbaCd = this.form.NIZUMI_GENBA_CD.Text;
        }

        /// <summary>
        /// 荷積現場CD初期セット
        /// </summary>
        internal void UnpanGyoushaCdSet()
        {
            tmpUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
        }

        // No.3587-->
        /// <summary>
        /// 現場CD初期セット
        /// </summary>
        internal void GenbaCdSet()
        {
            tmpGenbaCd = this.form.GENBA_CD.Text;
        }
        // No.3587<--

        // No.3312-->
        /// <summary>
        /// 車輌CD初期セット
        /// </summary>
        internal void ShayouCdSet()
        {
            sharyouCd = this.form.SHARYOU_CD.Text;
        }
        // No.3312<--

        /// <summary>
        /// 車種Cd初期セット
        /// </summary>
        internal void ShashuCdSet()
        {
            shaShuCd = this.form.SHASHU_CD.Text;
        }

        /// <summary>
        /// 運転者CD初期セット
        /// </summary>
        internal void UntenshaCdSet()
        {
            tmpUntenshaCd = this.form.UNTENSHA_CD.Text;
        }

        /// <summary>
        /// 形態区分CD初期セット
        /// </summary>
        internal void KeitaiKbnCdSet()
        {
            tmpKeitaiKbnCd = this.form.KEITAI_KBN_CD.Text;
        }

        /// <summary>
        /// 伝票日付初期セット
        /// </summary>
        internal void DenpyouDateSet()
        {
            tmpDenpyouDate = this.form.DENPYOU_DATE.Text;
        }

        public void SHARYOU_CD_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.form.KUUSHA_JYURYO.Text = string.Empty;
                this.form.isSelectingSharyouCd = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                return;
            }
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        internal void CheckSharyou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_SHARYOU[] sharyouEntitys = null;

                // 何もしないとポップアップが起動されてしまう可能性があるため
                // 変更されたかチェックする
                if (sharyouCd.Equals(this.form.SHARYOU_CD.Text))
                {
                    // 複数ヒットするCDを入力→ポップアップで何もしない→一度ポップアップを閉じて再度ポップアップからデータを選択
                    // したときに色が戻らない問題の対策のため、存在チェックだけは実施する。
                    // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
                    sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
                    // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END
                    if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                    {
                        // 一意に識別できる場合は色を戻す
                        this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                        this.form.oldSharyouShokuchiKbn = false;
                        this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;
                    }
                    return;
                }

                // 初期化
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                    this.form.KUUSHA_JYURYO.Text = string.Empty;        // No.3875
                }
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                this.form.oldSharyouShokuchiKbn = false;
                this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    sharyouCd = string.Empty;
                    this.form.isSelectingSharyouCd = false;
                    return;
                }

                sharyouCd = this.form.SHARYOU_CD.Text;
                unpanGyousha = this.form.UNPAN_GYOUSHA_CD.Text;

                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
                //sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
                sharyouEntitys = this.accessor.GetSharyouMod(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null);
                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END

                // No.3875-->
                this.form.KUUSHA_JYURYO.Text = string.Empty;
                if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                {
                    if (!sharyouEntitys[0].KUUSHA_JYURYO.IsNull)
                    {
                        this.form.KUUSHA_JYURYO.Text = sharyouEntitys[0].KUUSHA_JYURYO.ToString();
                    }
                }
                // No.3875<--

                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    // 車輌名を編集可
                    this.ChangeShokuchiSharyouDesign();
                    // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                    if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                    {
                        this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                        this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                    }
                    this.form.SHARYOU_NAME_RYAKU.Focus();

                    this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

                    if (!this.form.isSelectingSharyouCd)
                    {
                        this.form.isSelectingSharyouCd = true;
                        return;
                    }
                    return;
                }
                else
                {
                    this.form.oldSharyouShokuchiKbn = false;
                }

                // ポップアップから戻ってきたときに運搬業者名が無いため取得
                bool catchErr = false;
                M_GYOUSHA unpanGyousya = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { return; }
                if (unpanGyousya != null)
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousya.GYOUSHA_NAME_RYAKU;
                }

                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
                {
                    M_SHARYOU sharyou = new M_SHARYOU();

                    // 運搬業者チェック
                    if (sharyouEntitys.Length == 1)
                    {
                        bool isCheck = false;
                        foreach (M_SHARYOU sharyouEntity in sharyouEntitys)
                        {
                            if (sharyouEntity.GYOUSHA_CD.Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                            {
                                isCheck = true;
                                sharyou = sharyouEntity;
                                // 諸口区分チェック
                                if (unpanGyousya != null)
                                {
                                    if (unpanGyousya.SHOKUCHI_KBN.IsTrue)
                                    {
                                        // 運搬業者名編集可
                                        this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                        //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                                        this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
                                        this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                                    }
                                }
                                break;
                            }
                        }

                        if (isCheck)
                        {
                            // 車輌データセット
                            SetSharyou(sharyou);
                            return;
                        }
                        else
                        {
                            // エラーメッセージ
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E062", "運搬業者");
                            this.form.SHARYOU_CD.Focus();
                            return;
                        }
                    }
                    else if (sharyouEntitys.Length > 1)
                    {
                        // 複数レコード
                        // 車輌名を編集可
                        this.form.oldSharyouShokuchiKbn = true;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                        //this.form.SHARYOU_NAME_RYAKU.TabStop = true;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");    // No.3822
                        this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                        // 自由入力可能であるため車輌名の色を変更
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColorBlue;

                        sharyouCd = string.Empty;
                        unpanGyousha = string.Empty;
                        this.form.isSelectingSharyouCd = true;
                        this.form.SHARYOU_CD.Focus();

                        this.form.FocusOutErrorFlag = true;

                        // この時は車輌CDを検索条件に含める
                        this.PopUpConditionsSharyouSwitch(true);

                        // 検索ポップアップ起動
                        CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);
                        this.PopUpConditionsSharyouSwitch(false);

                        // PopUpでF12押下された場合
                        //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                        // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                        this.ChangeShokuchiSharyouDesign();
                        if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                        {
                            //NHU MOD 20170506 #104593 S
                            this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                            //NHU MOD 20170506 #104593 E
                            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                        }

                        this.form.FocusOutErrorFlag = false;
                        return;
                    }
                }
                else
                {
                    if (sharyouEntitys.Length > 1)
                    {
                        // 複数レコード
                        // 車輌名を編集可
                        this.form.oldSharyouShokuchiKbn = true;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                        //this.form.SHARYOU_NAME_RYAKU.TabStop = true;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");    // No.3822
                        this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                        // 自由入力可能であるため車輌名の色を変更
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColorBlue;

                        if (!this.form.isSelectingSharyouCd)
                        {
                            sharyouCd = string.Empty;
                            unpanGyousha = string.Empty;
                            this.form.isSelectingSharyouCd = true;
                            this.form.SHARYOU_CD.Focus();

                            this.form.FocusOutErrorFlag = true;

                            // この時は車輌CDを検索条件に含める
                            this.PopUpConditionsSharyouSwitch(true);

                            // 検索ポップアップ起動
                            CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);
                            this.PopUpConditionsSharyouSwitch(false);

                            // PopUpでF12押下された場合
                            //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                            {
                                // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                                this.ChangeShokuchiSharyouDesign();
                                if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                                {
                                    this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                                    this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                                }
                            }

                            this.form.FocusOutErrorFlag = false;
                            return;
                        }
                        else
                        {
                            // ポップアアップから戻ってきて車輌名へ遷移した場合
                            // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                            this.ChangeShokuchiSharyouDesign();
                            this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                        }

                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        SetSharyou(sharyouEntitys[0]);
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("CheckSharyou", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckSharyou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!this.form.SHARYOU_CD.PopupSearchSendParams.Contains(sharyouParam))
                {
                    this.form.SHARYOU_CD.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = this.form.SHARYOU_CD.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (this.form.SHARYOU_CD.PopupSearchSendParams[i].Control == "SHARYOU_CD" &&
                        this.form.SHARYOU_CD.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        this.form.SHARYOU_CD.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 車輌情報をセット
        /// </summary>
        /// <param name="sharyouEntity"></param>
        private void SetSharyou(M_SHARYOU sharyouEntity)
        {
            this.form.SHARYOU_CD.Text = sharyouEntity.SHARYOU_CD;
            this.sharyouCd = sharyouEntity.SHARYOU_CD;

            this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
            this.form.UNTENSHA_CD.Text = sharyouEntity.SHAIN_CD;
            this.form.SHASHU_CD.Text = sharyouEntity.SHASYU_CD;
            this.form.UNPAN_GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;

            // No.3875-->
            if (!sharyouEntity.KUUSHA_JYURYO.IsNull)
            {
                this.form.KUUSHA_JYURYO.Text = sharyouEntity.KUUSHA_JYURYO.ToString();
            }
            else
            {
                this.form.KUUSHA_JYURYO.Text = string.Empty;
            }
            // No.3875<--

            // 運転者情報セット
            var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
            if (untensha != null)
            {
                this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;
            }
            else
            {
                this.form.UNTENSHA_NAME.Text = string.Empty;
            }

            //車種情報セット
            var shashu = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
            if (shashu != null)
            {
                this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
            }
            else
            {
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME.Text = string.Empty;
            }

            this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

            bool catchErr = false;
            bool ret = this.CheckUnpanGyoushaCd(out catchErr);
            if (catchErr)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// 形態区分選択ポップアップ用DataSource生成
        /// デザイナのプロパティ設定からでは絞り込み条件が作れないため、
        /// DataSourceを渡す方法でポップアップを表示する。
        /// </summary>
        /// <returns></returns>
        internal DataTable CreateKeitaiKbnPopupDataSource()
        {
            var allKeitaiKbn = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>().GetAllValidData(new M_KEITAI_KBN());
            var dt = EntityUtility.EntityToDataTable(allKeitaiKbn);

            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            sortedDt.Columns.Add(dt.Columns["KEITAI_KBN_CD"].ColumnName, dt.Columns["KEITAI_KBN_CD"].DataType);
            sortedDt.Columns.Add(dt.Columns["KEITAI_KBN_NAME_RYAKU"].ColumnName, dt.Columns["KEITAI_KBN_NAME_RYAKU"].DataType);

            foreach (DataRow r in dt.Rows)
            {
                if (r["DENSHU_KBN_CD"] != null
                    && (r["DENSHU_KBN_CD"].ToString().Equals(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA.ToString())
                        || r["DENSHU_KBN_CD"].ToString().Equals(SalesPaymentConstans.DENSHU_KBN_CD_KYOTU.ToString()))
                    )
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
            }

            return sortedDt;
        }

        /// <summary>
        /// 形態区分チェック処理
        /// </summary>
        internal void CheckKeitaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text) || !this.tmpKeitaiKbnCd.Equals(this.form.KEITAI_KBN_CD.Text)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                    {
                        return;
                    }

                    short keitaiKbnCd;

                    if (!short.TryParse(this.form.KEITAI_KBN_CD.Text, out keitaiKbnCd))
                    {
                        return;
                    }

                    M_KEITAI_KBN kakuteiKbn = this.accessor.GetkeitaiKbn(keitaiKbnCd);
                    if (kakuteiKbn == null)
                    {
                        // エラーメッセージ
                        this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                        this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "形態区分");
                        this.form.KEITAI_KBN_CD.Focus();
                        tmpKeitaiKbnCd = string.Empty;
                        return;
                    }

                    var denshuKbnCd = (DENSHU_KBN)Enum.ToObject(typeof(DENSHU_KBN), (int)kakuteiKbn.DENSHU_KBN_CD);

                    switch (denshuKbnCd)
                    {
                        case DENSHU_KBN.SHUKKA:
                        case DENSHU_KBN.KYOUTSUU:
                            this.form.KEITAI_KBN_NAME_RYAKU.Text = kakuteiKbn.KEITAI_KBN_NAME_RYAKU;
                            break;

                        default:
                            // エラーメッセージ
                            this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                            this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                            this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "形態区分");
                            this.form.KEITAI_KBN_CD.Focus();
                            tmpKeitaiKbnCd = string.Empty;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeitaiKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 台貫区分チェック
        /// </summary>
        internal void CheckDaikanKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.DAIKAN_KBN_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.DAIKAN_KBN.Text))
                {
                    return;
                }

                string daikanKbnName = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(this.form.DAIKAN_KBN.Text.ToString()));
                if (string.IsNullOrEmpty(daikanKbnName))
                {
                    // エラーメッセージ
                    this.form.DAIKAN_KBN.IsInputErrorOccured = true;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.DAIKAN_KBN.Focus();
                }
                else
                {
                    this.form.DAIKAN_KBN_NAME.Text = daikanKbnName;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDaikanKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 伝票日付チェック
        /// </summary>
        internal bool CheckDenpyouDate()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var inputDenpyouDate = this.form.DENPYOU_DATE.Text;

                // 伝票日付が空じゃないかつ変更があった場合
                if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.tmpDenpyouDate.Equals(inputDenpyouDate))
                {
                    //検収済みの場合は、単価再設定をしない
                    if (this.form.KENSHU_MUST_KBN.Checked && (this.kenshuZumi.Equals(this.form.txtKensyuu.Text)))
                    {
                        DialogResult dr = msgLogic.MessageBoxShowInformation("単価に関連する項目の変更が行われました。検収入力画面で登録した単価の確認を行ってください。");
                    }
                    else
                    {
                        // 明細行すべての単価を再設定
                        var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                        foreach (Row dr in list)
                        {
                            if (!this.SearchAndCalcForUnit(false, dr))
                            {
                                return false;
                            }
                        }
                        this.ResetTankaCheck(); // MAILAN #158992 START

                        // 合計金額の再計算
                        if (!this.CalcTotalValues())
                        {
                            return false;
                        }
                    }
                }

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDenpyouDate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 帳票(領収書)出力
        /// </summary>
        internal bool Print()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                DataTable reportData = CreateReportData();

                // G335\Templateにxmlを配置している
                // 現在表示されている一覧をレポート情報として生成
                ReportInfoR339 reportInfo = new ReportInfoR339(this.form.WindowId, reportData);
                reportInfo.CreateReport();
                reportInfo.Title = "領収書";

                // 印刷ポップアップ表示
                FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo, "R339");
                //reportPopup.ShowDialog(); // No.1143
                // 印刷設定の取得
                //reportPopup.SetPrintSetting(SalesPaymentConstans.RYOUSYUUSHO);
                // 印刷アプリ初期動作(直印刷)
                reportPopup.PrintInitAction = 1;
                // 印刷実行
                reportPopup.PrintXPS(true, true);        // No.1143
                reportPopup.Dispose();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("Print", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 帳票出力用データ作成
        /// </summary>
        /// <returns>帳票用DataTable</returns>
        internal DataTable CreateReportData()
        {
            LogUtility.DebugMethodStart();
            DataTable reportTable = new DataTable();

            // Colum定義
            reportTable.Columns.Add("GYOUSHA_CD");
            reportTable.Columns["GYOUSHA_CD"].ReadOnly = false;
            reportTable.Columns.Add("GYOUSHA_NAME1");
            reportTable.Columns["GYOUSHA_NAME1"].ReadOnly = false;
            reportTable.Columns.Add("KEISHOU1");
            reportTable.Columns["KEISHOU1"].ReadOnly = false;
            reportTable.Columns.Add("GYOUSHA_NAME2");
            reportTable.Columns["GYOUSHA_NAME2"].ReadOnly = false;
            reportTable.Columns.Add("KEISHOU2");
            reportTable.Columns["KEISHOU2"].ReadOnly = false;
            reportTable.Columns.Add("DENPYOU_DATE");
            reportTable.Columns["DENPYOU_DATE"].ReadOnly = false;
            reportTable.Columns.Add("RECEIPT_NUMBER");
            reportTable.Columns["RECEIPT_NUMBER"].ReadOnly = false;
            reportTable.Columns.Add("KINGAKU_TOTAL");
            reportTable.Columns["KINGAKU_TOTAL"].ReadOnly = false;
            reportTable.Columns.Add("TADASHIGAKI");
            reportTable.Columns["TADASHIGAKI"].ReadOnly = false;
            reportTable.Columns.Add("CORP_RYAKU_NAME");
            reportTable.Columns["CORP_RYAKU_NAME"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_NAME");
            reportTable.Columns["KYOTEN_NAME"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_POST");
            reportTable.Columns["KYOTEN_POST"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_ADDRESS1");
            reportTable.Columns["KYOTEN_ADDRESS1"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_ADDRESS2");                 // No.3710
            reportTable.Columns["KYOTEN_ADDRESS2"].ReadOnly = false;    // No.3710
            reportTable.Columns.Add("KYOTEN_TEL");
            reportTable.Columns["KYOTEN_TEL"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_FAX");
            reportTable.Columns["KYOTEN_FAX"].ReadOnly = false;
            reportTable.Columns.Add("ZEINUKI_KINGAKU");
            reportTable.Columns["ZEINUKI_KINGAKU"].ReadOnly = false;
            reportTable.Columns.Add("SYOUHIZEI_RITU");
            reportTable.Columns["SYOUHIZEI_RITU"].ReadOnly = false;
            reportTable.Columns.Add("SYOUHIZEI");
            reportTable.Columns["SYOUHIZEI"].ReadOnly = false;
            reportTable.Columns.Add("DENPYOU_NUMBER");
            reportTable.Columns["DENPYOU_NUMBER"].ReadOnly = false;
            //小計②
            reportTable.Columns.Add("HIKAZEI_ZEINUKI_KINGAKU");
            reportTable.Columns["HIKAZEI_ZEINUKI_KINGAKU"].ReadOnly = false;
            reportTable.Columns.Add("HIKAZEI_SYOUHIZEI_RITU");
            reportTable.Columns["HIKAZEI_SYOUHIZEI_RITU"].ReadOnly = false;
            reportTable.Columns.Add("HIKAZEI_SYOUHIZEI");
            reportTable.Columns["HIKAZEI_SYOUHIZEI"].ReadOnly = false;
            //登録番号
            reportTable.Columns.Add("TOUROKU_NO");
            reportTable.Columns["TOUROKU_NO"].ReadOnly = false;

            // 取引先マスタ検索
            M_TORIHIKISAKI TorihikisakiEntity = new M_TORIHIKISAKI();
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                bool catchErr = false;
                TorihikisakiEntity = accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr, false);
                if (catchErr) { throw new Exception(""); }
            }

            // 拠点マスタ検索
            short kyoteCd = -1;
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyoteCd);
            }
            M_KYOTEN[] kyotenEntitys = this.accessor.GetAllDataByCodeForKyoten(kyoteCd);

            // データセット
            DataRow row = reportTable.NewRow();
            row["GYOUSHA_CD"] = this.form.TORIHIKISAKI_CD.Text;
            if (TorihikisakiEntity != null)
            {
                // 諸口区分チェック
                if (TorihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                {
                    if (this.dto.entryEntity.TORIHIKISAKI_NAME.Length > 20)
                    {
                        //20150921 hoanghm #12520 start
                        //row["GYOUSHA_NAME1"] = this.dto.entryEntity.TORIHIKISAKI_NAME.Substring(0, 20);
                        //row["GYOUSHA_NAME2"] = this.dto.entryEntity.TORIHIKISAKI_NAME.Substring(20);
                        string gyoushaName1 = string.Empty;
                        string gyoushaName2 = string.Empty;
                        ReportUtility.SubString(this.dto.entryEntity.TORIHIKISAKI_NAME, 40, ref gyoushaName1, ref gyoushaName2);
                        row["GYOUSHA_NAME1"] = gyoushaName1;
                        row["GYOUSHA_NAME2"] = gyoushaName2;
                        //20150921 hoanghm #12520 end
                    }
                    else
                    {
                        row["GYOUSHA_NAME1"] = this.dto.entryEntity.TORIHIKISAKI_NAME;
                    }
                }
                else
                {
                    row["GYOUSHA_NAME1"] = TorihikisakiEntity.TORIHIKISAKI_NAME1;
                    row["GYOUSHA_NAME2"] = TorihikisakiEntity.TORIHIKISAKI_NAME2;
                }
            }
            row["KEISHOU1"] = this.form.denpyouHakouPopUpDTO.Keisyou_1;
            row["KEISHOU2"] = this.form.denpyouHakouPopUpDTO.Keisyou_2;
            row["DENPYOU_DATE"] = this.form.DENPYOU_DATE.Text;
            if (this.dto.sysInfoEntity.SYS_RECEIPT_RENBAN_HOUHOU_KBN == 1)
            {
                row["RECEIPT_NUMBER"] = this.dto.entryEntity.RECEIPT_NUMBER;
            }
            else
            {
                row["RECEIPT_NUMBER"] = this.dto.entryEntity.RECEIPT_NUMBER_YEAR;
            }
            row["KINGAKU_TOTAL"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki;
            row["TADASHIGAKI"] = this.form.denpyouHakouPopUpDTO.Tadasi_Kaki;
            row["CORP_RYAKU_NAME"] = "";
            M_CORP_INFO entCorpInfo = CommonShogunData.CORP_INFO;
            if (entCorpInfo != null)
            {
                if (!string.IsNullOrEmpty(entCorpInfo.CORP_NAME))
                {
                    row["CORP_RYAKU_NAME"] = entCorpInfo.CORP_NAME;
                }
                //登録番号
                if (!string.IsNullOrEmpty(entCorpInfo.TOUROKU_NO))
                {
                    row["TOUROKU_NO"] = "登録番号：" + entCorpInfo.TOUROKU_NO;
                }
            }
            if (kyotenEntitys != null && kyotenEntitys.Length > 0)
            {
                row["KYOTEN_NAME"] = kyotenEntitys[0].KYOTEN_NAME;
                row["KYOTEN_POST"] = kyotenEntitys[0].KYOTEN_POST;
                row["KYOTEN_ADDRESS1"] = kyotenEntitys[0].KYOTEN_ADDRESS1;
                row["KYOTEN_ADDRESS2"] = kyotenEntitys[0].KYOTEN_ADDRESS2;  // No.3710
                row["KYOTEN_TEL"] = kyotenEntitys[0].KYOTEN_TEL;
                row["KYOTEN_FAX"] = kyotenEntitys[0].KYOTEN_FAX;
            }

            row["ZEINUKI_KINGAKU"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU;
            decimal zeiritu;
            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
            {
                zeiritu = this.ToDecimalForUriageShouhizeiRate();
            }
            else
            {
                var shouhizeiRate = this.accessor.GetShouhizeiRate(((DateTime)this.dto.entryEntity.URIAGE_DATE).Date);
                zeiritu = (decimal)(shouhizeiRate.SHOUHIZEI_RATE);
            }
            row["SYOUHIZEI_RITU"] = string.Format("{0:0%}", zeiritu);
            row["SYOUHIZEI"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI;

            if (this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU == "0")
            {
                row["ZEINUKI_KINGAKU"] = "";
                row["SYOUHIZEI"] = "";
            }

            row["DENPYOU_NUMBER"] = "出荷番号No." + this.dto.entryEntity.SHUKKA_NUMBER;

            //小計②
            row["HIKAZEI_SYOUHIZEI_RITU"] = "非課税";
            if (this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU != "0")
            {
                row["HIKAZEI_ZEINUKI_KINGAKU"] = this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU;
                row["HIKAZEI_SYOUHIZEI"] = 0;
            }

            reportTable.Rows.Add(row);

            LogUtility.DebugMethodEnd();
            return reportTable;
        }

        string strTorihikisakiName = "";
        string strTorihikisakiName2 = "";
        string strTorihikisakiKeishou = "";
        string strTorihikisakiKeishou2 = "";
        string strNyuryokuTantousyaName = "";

        /// <summary>
        /// 印刷用現場名
        /// </summary>
        string strGenbaName = "";
        M_KYOTEN entKyotenInfo;

        /// <summary>
        /// 仕切書印刷処理
        /// </summary>
        /// <returns></returns>
        internal bool PrintShikirisyo()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                DataTable reportTable = new DataTable();

                DataRow rowHeader;
                rowHeader = reportTable.NewRow();

                //明細のデータに伝票区分が売上/支払のデータが存在するか確認する。
                bool bExistDenpyoKbnUriage = false;
                bool bExistDenpyoKbnShiharai = false;

                foreach (Row dr in this.form.gcMultiRow1.Rows)
                {
                    if (!dr.IsNewRow)
                    {
                        string strDenpyouKbn = dr[CELL_NAME_DENPYOU_KBN_CD].Value.ToString();
                        if (strDenpyouKbn == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR)
                        {
                            bExistDenpyoKbnUriage = true;
                        }
                        else if (strDenpyouKbn == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR)
                        {
                            bExistDenpyoKbnShiharai = true;
                        }

                        if (bExistDenpyoKbnUriage && bExistDenpyoKbnShiharai)
                        {
                            break;
                        }
                    }
                }

                if (bExistDenpyoKbnShiharai || bExistDenpyoKbnUriage)
                {
                    //DBアクセスを無駄に行わないように先に固定の情報は取得する。
                    bool catchErr = false;
                    M_TORIHIKISAKI entTorihikisaki = accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr, false);
                    if (catchErr) { throw new Exception(""); }

                    if (entTorihikisaki != null)
                    {
                        // 諸口区分チェック
                        if (entTorihikisaki.SHOKUCHI_KBN.IsTrue)
                        {
                            //ThangNguyen [Update] 2015086 #12394 Start
                            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                            byte[] byteArray = encoding.GetBytes(this.form.TORIHIKISAKI_NAME_RYAKU.Text);
                            if (byteArray.Length > 40)
                            {
                                strTorihikisakiName = encoding.GetString(byteArray, 0, 40);
                                //strTorihikisakiName2 = encoding.GetString(byteArray, 41, byteArray.Length - 1);
                                strTorihikisakiName2 = this.form.TORIHIKISAKI_NAME_RYAKU.Text.Replace(strTorihikisakiName, "");
                            }
                            else
                            {
                                strTorihikisakiName = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                            }
                            //ThangNguyen [Update] 2015086 #12394 End
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(entTorihikisaki.TORIHIKISAKI_NAME1))
                            {
                                strTorihikisakiName = entTorihikisaki.TORIHIKISAKI_NAME1;
                                strTorihikisakiName2 = entTorihikisaki.TORIHIKISAKI_NAME2;
                            }
                            else
                            {
                                strTorihikisakiName = "";
                            }
                        }
                        if (!string.IsNullOrEmpty(entTorihikisaki.TORIHIKISAKI_KEISHOU1))
                        {
                            strTorihikisakiKeishou = entTorihikisaki.TORIHIKISAKI_KEISHOU1;
                        }
                        else
                        {
                            strTorihikisakiKeishou = "";
                        }
                        if (!string.IsNullOrEmpty(entTorihikisaki.TORIHIKISAKI_KEISHOU2))
                        {
                            strTorihikisakiKeishou2 = entTorihikisaki.TORIHIKISAKI_KEISHOU2;
                        }
                        else
                        {
                            strTorihikisakiKeishou2 = "";
                        }
                    }

                    M_SHAIN entShain = accessor.GetShain(this.form.NYUURYOKU_TANTOUSHA_CD.Text);
                    if (entShain != null)
                    {
                        if (!string.IsNullOrEmpty(entShain.SHAIN_NAME))
                        {
                            strNyuryokuTantousyaName = entShain.SHAIN_NAME;
                        }
                        else
                        {
                            strNyuryokuTantousyaName = "";
                        }

                    }
                    M_GENBA entGenba = accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr, false);
                    if (catchErr) { throw new Exception(""); }
                    if (entGenba != null)
                    {
                        // 諸口区分チェック
                        if (entGenba.SHOKUCHI_KBN.IsTrue)
                        {
                            strGenbaName = this.form.GENBA_NAME_RYAKU.Text;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(entGenba.GENBA_NAME1))
                            {
                                //strGenbaName = entGenba.GENBA_NAME1;
                                strGenbaName = entGenba.GENBA_NAME1 + entGenba.GENBA_NAME2; // No.3837
                            }
                            else
                            {
                                strGenbaName = "";
                            }
                        }
                    }

                    short kyotenCd;
                    if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
                    {
                        M_KYOTEN[] kyotens = accessor.GetAllDataByCodeForKyoten(kyotenCd);
                        if (kyotens != null && kyotens.Count() > 0)
                        {
                            // 拠点CDで絞り込んだら一件しか取れないはず
                            entKyotenInfo = kyotens[0];
                        }
                    }

                    string strSeikyuHakkouKbn = this.form.denpyouHakouPopUpDTO.Seikyu_Hakou_Kbn;
                    string strShiharaiHakkouKbn = this.form.denpyouHakouPopUpDTO.Shiharai_Hakou_Kbn;
                    string strHakkouKbn = this.form.denpyouHakouPopUpDTO.Hakou_Kbn;
                    //請求の伝票発行区分を確認する。
                    if (bExistDenpyoKbnUriage && strSeikyuHakkouKbn == "1")
                    {
                        if (strHakkouKbn != DEF_HAKKOU_KBN_SOUSAI)
                        {
                            PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND.SEIKYUU);
                        }
                    }

                    //支払の伝票発行区分を確認する。
                    if (bExistDenpyoKbnShiharai && strShiharaiHakkouKbn == "1")
                    {
                        if (strHakkouKbn != DEF_HAKKOU_KBN_SOUSAI)
                        {
                            PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND.SHIHARAI);
                        }
                    }

                    if (strSeikyuHakkouKbn == "1" || strShiharaiHakkouKbn == "1")
                    {
                        if (strHakkouKbn != DEF_HAKKOU_KBN_KOBETSU)
                        {
                            PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND.SOUSAI);
                        }
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("PrintShikirisyo", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("PrintShikirisyo", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 指定した種類の仕切書を印刷する。
        /// </summary>
        /// <returns></returns>
        private void PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND type)
        {

            DataTable dtHeader = CreateHeaderTable(type);
            DataTable dtDetail = CreateDetailTable(type);
            DataTable dtFooter = CreateFooterTable(type);

            switch (type)
            {
                case DENPYO_SHIKIRISHO_KIND.SEIKYUU:
                case DENPYO_SHIKIRISHO_KIND.SHIHARAI:
                    ReportInfoR765 reportInfo_invoice = new ReportInfoR765(this.form.WindowId);
                    reportInfo_invoice.DataTableList.Add("Header", dtHeader);
                    reportInfo_invoice.DataTableList.Add("Detail", dtDetail);
                    reportInfo_invoice.DataTableList.Add("Footer", dtFooter);
                    reportInfo_invoice.Create(@".\Template\R765-Form.xml", "LAYOUT1", new DataTable());
                    reportInfo_invoice.Title = "仕切書";
                    FormReportPrintPopup popup_invoice = new FormReportPrintPopup(reportInfo_invoice, "R765", WINDOW_ID.T_SHUKKA);
                    // 印刷アプリ初期動作(直印刷)
                    popup_invoice.PrintInitAction = 1;
                    // 印刷実行
                    popup_invoice.PrintXPS(true, true);
                    break;
                case DENPYO_SHIKIRISHO_KIND.SOUSAI:
                    ReportInfoR338 reportInfo = new ReportInfoR338(this.form.WindowId);
                    reportInfo.DataTableList.Add("Header", dtHeader);
                    reportInfo.DataTableList.Add("Detail", dtDetail);
                    reportInfo.DataTableList.Add("Footer", dtFooter);
                    reportInfo.Create(@".\Template\R338-Form.xml", "LAYOUT1", new DataTable());
                    reportInfo.Title = "仕切書";
                    FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, "R338", WINDOW_ID.T_SHUKKA);
                    // 印刷アプリ初期動作(直印刷)
                    popup.PrintInitAction = 1;
                    // 印刷実行
                    popup.PrintXPS(true, true);
                    break;
            }
        }

        /// <summary>
        /// 仕切書ヘッダー部データ受渡用文字列作成
        /// </summary>
        /// <returns>ヘッダー部データ受渡用文字列</returns>
        private DataTable CreateHeaderTable(DENPYO_SHIKIRISHO_KIND Type)
        {
            DataTable dtHeader = new DataTable();
            DataRow rowTmp;
            dtHeader.TableName = "Header";

            // タイトル名
            dtHeader.Columns.Add("TITLE");
            // 担当名
            dtHeader.Columns.Add("TANTOU");
            // お取引先CD
            dtHeader.Columns.Add("TORIHIKISAKICD");
            // お取引先名
            dtHeader.Columns.Add("TORIHIKISAKIMEI");
            // お取引先名2
            dtHeader.Columns.Add("TORIHIKISAKIMEI2");
            // お取引先名敬称
            dtHeader.Columns.Add("TORIHIKISAKIKEISHOU");
            // お取引先名敬称2
            dtHeader.Columns.Add("TORIHIKISAKIKEISHOU2");
            // 伝票No
            dtHeader.Columns.Add("DENPYOUNO");
            // 乗員
            dtHeader.Columns.Add("JYOUIN");
            // 車番
            dtHeader.Columns.Add("SHABAN");
            // 車輌CD
            dtHeader.Columns.Add("SHARYOUCD");  // No.3837
            // 伝票日付
            dtHeader.Columns.Add("DENPYOUDATE");

            rowTmp = dtHeader.NewRow();

            System.Text.StringBuilder sBuilder;

            sBuilder = new StringBuilder();

            //タイトル名
            string iCalcBaseKbn = this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.ToString();
            if ((this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG || this.dto.entryEntity.KENSHU_DATE.IsNull) && (false == this.BlankKenshuDetailOutput()))
            {
                if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                {
                    sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3);
                }
                else if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
                {
                    sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3);
                }
                else
                {
                    if (iCalcBaseKbn == "1")
                    {
                        sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3);
                    }
                    else
                    {
                        sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3);
                    }
                }
            }
            else
            {
                if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                {
                    sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE1);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE2);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE3);
                }
                else if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
                {
                    sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE1);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE2);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE3);
                }
                else
                {
                    if (iCalcBaseKbn == "1")
                    {
                        sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE1);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE2);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE3);
                    }
                    else
                    {
                        sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE1);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE2);
                        sBuilder.Append(",");
                        sBuilder.Append(this.dto.sysInfoEntity.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE3);
                    }
                }
            }

            rowTmp["TITLE"] = sBuilder.ToString();

            // 担当名
            rowTmp["TANTOU"] = strNyuryokuTantousyaName;

            //お取引先CD
            rowTmp["TORIHIKISAKICD"] = this.form.TORIHIKISAKI_CD.Text;

            //お取引先名
            rowTmp["TORIHIKISAKIMEI"] = strTorihikisakiName;

            //お取引先名2
            rowTmp["TORIHIKISAKIMEI2"] = strTorihikisakiName2;

            //お取引先名敬称
            rowTmp["TORIHIKISAKIKEISHOU"] = strTorihikisakiKeishou;

            //お取引先名敬称
            rowTmp["TORIHIKISAKIKEISHOU2"] = strTorihikisakiKeishou2;

            //伝票No
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                // 新規
                rowTmp["DENPYOUNO"] = this.dto.entryEntity.SHUKKA_NUMBER;
            else
                // 新規以外
                rowTmp["DENPYOUNO"] = this.form.ENTRY_NUMBER.Text;

            //乗員
            rowTmp["JYOUIN"] = this.form.NINZUU_CNT.Text;

            //車番
            rowTmp["SHABAN"] = this.form.SHARYOU_NAME_RYAKU.Text;

            //車輌CD
            rowTmp["SHARYOUCD"] = this.form.SHARYOU_CD.Text;    // No.3837

            //伝票日付
            DateTime Date = (DateTime)this.form.DENPYOU_DATE.Value;
            rowTmp["DENPYOUDATE"] = Date.ToString("yyyy/MM/dd");

            dtHeader.Rows.Add(rowTmp);

            return dtHeader;
        }

        /// <summary>
        /// 仕切書明細部データ受渡用文字列作成
        /// </summary>
        /// <returns>仕切書明細部データ受渡用文字列</returns>
        private DataTable CreateDetailTable(DENPYO_SHIKIRISHO_KIND Type)
        {
            // 検収伝票出力フラグ取得
            var kenshuOutput = this.BlankKenshuDetailOutput();

            // 売上税計算区分CD
            int seikyuZeikeisanKbn = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn, out seikyuZeikeisanKbn);
            // 売上税区分CD
            int uriageZeiKbnCd = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn, out uriageZeiKbnCd);
            // 支払税計算区分CD
            int shiharaiZeiKeisanKbnCd = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn, out shiharaiZeiKeisanKbnCd);
            // 支払税区分CD
            int shiharaiZeiKbnCd = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn, out shiharaiZeiKbnCd);
            SHIKIRISHO_UR_UTIZEI = 0;
            SHIKIRISHO_SH_UTIZEI = 0;

            // カラム生成
            DataTable dtDetail = new DataTable();
            DataRow rowTmp;
            dtDetail.TableName = "Detail";
            // No
            dtDetail.Columns.Add("NUMBER");
            // 総重量
            dtDetail.Columns.Add("SOU_JYUURYOU");
            // 空車重量
            dtDetail.Columns.Add("KUUSHA_JYUURYOU");
            // 調整
            dtDetail.Columns.Add("CHOUSEI");
            // 容器引
            dtDetail.Columns.Add("YOUKIBIKI");
            // 正味
            dtDetail.Columns.Add("SHOUMI");
            // 数量
            dtDetail.Columns.Add("SUURYOU");
            // 数量単位名
            dtDetail.Columns.Add("FHN_SUURYOU_TANI");
            // 品名CD
            dtDetail.Columns.Add("FHN_HINMEICD");
            // 品名
            dtDetail.Columns.Add("HINMEI");
            // 単価
            dtDetail.Columns.Add("TANKA");
            // 金額
            dtDetail.Columns.Add("KINGAKU");

            foreach (Row dr in this.form.gcMultiRow1.Rows)
            {
                if (dr.IsNewRow || string.IsNullOrEmpty((string)dr.Cells["ROW_NO"].Value.ToString()))
                {
                    continue;
                }

                rowTmp = dtDetail.NewRow();

                //No
                rowTmp["NUMBER"] = dr.Cells[CELL_NAME_ROW_NO].Value.ToString();

                //総重量
                rowTmp["SOU_JYUURYOU"] = dr.Cells[CELL_NAME_STAK_JYUURYOU].DisplayText;

                //空車重量
                rowTmp["KUUSHA_JYUURYOU"] = dr.Cells[CELL_NAME_EMPTY_JYUURYOU].DisplayText;

                //調整
                rowTmp["CHOUSEI"] = dr.Cells[CELL_NAME_CHOUSEI_JYUURYOU].DisplayText;

                //容器引
                rowTmp["YOUKIBIKI"] = dr.Cells[CELL_NAME_YOUKI_JYUURYOU].DisplayText;

                //正味
                rowTmp["SHOUMI"] = dr.Cells[CELL_NAME_NET_JYUURYOU].DisplayText;

                //数量
                rowTmp["SUURYOU"] = dr.Cells[CELL_NAME_SUURYOU].DisplayText;

                //数量単位名
                if (!string.IsNullOrEmpty(dr.Cells[CELL_NAME_UNIT_CD].DisplayText))
                {
                    short UnitCd = 0;
                    if (short.TryParse(dr.Cells[CELL_NAME_UNIT_CD].Value.ToString(), out UnitCd))
                    {
                        M_UNIT[] unit = this.accessor.GetAllUnit(UnitCd);
                        if (unit != null && unit.Count() > 0)
                        {
                            M_UNIT entUnit = unit[0];
                            string strUnitName = entUnit.UNIT_NAME;
                            rowTmp["FHN_SUURYOU_TANI"] = strUnitName;
                        }
                    }
                }
                else
                {
                    rowTmp["FHN_SUURYOU_TANI"] = "";
                }

                //品名CD
                rowTmp["FHN_HINMEICD"] = dr.Cells[CELL_NAME_HINMEI_CD].DisplayText;

                //品名
                // 20151021 katen #13337 品名手入力に関する機能修正 start
                rowTmp["HINMEI"] = dr.Cells[CELL_NAME_HINMEI_NAME].DisplayText; ;
                //if (!string.IsNullOrEmpty(dr.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                //{
                //    M_HINMEI entHinmei = this.accessor.GetHinmeiDataByCd(dr.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                //    string strHinmeiName = entHinmei.HINMEI_NAME;
                //    rowTmp["HINMEI"] = strHinmeiName;
                //}
                //else
                //{
                //    rowTmp["HINMEI"] = "";
                //}
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                //単価と金額
                if (kenshuOutput == true)
                {
                    // 要検収かつ、検収入力確定が行われていない場合、「単価」「金額」を空欄とした検収伝票を出力する
                    rowTmp["TANKA"] = "";
                    rowTmp["KINGAKU"] = "";
                }
                else
                {
                    string strUrShCalcBaseKbn = this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.ToString(); // 差引基準を取得
                    int iDenpyoKbnCd = int.Parse(dr.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString());  // 伝票区分を取得
                    if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                    {
                        // 請求仕切書の場合
                        if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                        {
                            // 伝票区分=1(売上)
                            rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                            rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                            int temp;
                            if (int.TryParse(Convert.ToString(dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out temp))
                            {
                                //品名内税
                                if (dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Equals(CommonConst.ZEI_KBN_UCHI.ToString()))
                                {
                                    SHIKIRISHO_UR_UTIZEI = 1;
                                }
                            }
                            else
                            {
                                //品名税なし、明細毎内税
                                if (seikyuZeikeisanKbn.Equals(CommonConst.ZEI_KEISAN_KBN_MEISAI) && uriageZeiKbnCd.Equals(CommonConst.ZEI_KBN_UCHI))
                                {
                                    SHIKIRISHO_UR_UTIZEI = 1;
                                }
                            }
                        }
                        else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                        {
                            // 伝票区分=2(支払)
                            rowTmp["TANKA"] = "";
                            rowTmp["KINGAKU"] = "";
                        }
                    }
                    else if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
                    {
                        // 支払仕切書の場合
                        if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                        {
                            // 伝票区分=1(売上)
                            rowTmp["TANKA"] = "";
                            rowTmp["KINGAKU"] = "";
                        }
                        else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                        {
                            // 伝票区分=2(支払)
                            rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                            rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                            int temp;
                            if (int.TryParse(Convert.ToString(dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out temp))
                            {
                                //品名内税
                                if (dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Equals(CommonConst.ZEI_KBN_UCHI.ToString()))
                                {
                                    SHIKIRISHO_SH_UTIZEI = 1;
                                }
                            }
                            else
                            {
                                //品名税なし、明細毎内税
                                if (shiharaiZeiKeisanKbnCd.Equals(CommonConst.ZEI_KEISAN_KBN_MEISAI) && shiharaiZeiKbnCd.Equals(CommonConst.ZEI_KBN_UCHI))
                                {
                                    SHIKIRISHO_SH_UTIZEI = 1;
                                }
                            }
                        }
                    }
                    else if (Type == DENPYO_SHIKIRISHO_KIND.SOUSAI)
                    {
                        //相殺仕切書の場合
                        if (strUrShCalcBaseKbn == "1")
                        {
                            // 差引基準=1(売上)

                            if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                            {
                                // 伝票区分=1(売上)
                                rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                                rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                            }
                            else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                            {
                                // 伝票区分=2(支払)
                                rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                                decimal decKingaku = 0;
                                decimal.TryParse(dr.Cells[CELL_NAME_KINGAKU].Value.ToString(), out decKingaku);
                                rowTmp["KINGAKU"] = CommonCalc.DecimalFormat(decKingaku * -1);
                            }
                        }
                        else if (strUrShCalcBaseKbn == "2")
                        {
                            // 差引基準=2(支払)

                            if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                            {
                                // 伝票区分=1(売上)
                                rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                                decimal decKingaku = 0;
                                decimal.TryParse(dr.Cells[CELL_NAME_KINGAKU].Value.ToString(), out decKingaku);
                                rowTmp["KINGAKU"] = CommonCalc.DecimalFormat(decKingaku * -1);
                            }
                            else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                            {
                                // 伝票区分=2(支払)
                                rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                                rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                            }
                        }
                    }
                }

                dtDetail.Rows.Add(rowTmp);

            }

            //データがない場合でも5行分データがないと出力されないため、空行を作成する。
            int iNumEmpLine = dtDetail.Rows.Count % 5;
            if (iNumEmpLine > 0)
            {
                for (int ii = 0; ii < 5 - iNumEmpLine; ii++)
                {
                    rowTmp = dtDetail.NewRow();
                    rowTmp["NUMBER"] = "";
                    rowTmp["SOU_JYUURYOU"] = "";
                    rowTmp["KUUSHA_JYUURYOU"] = "";
                    rowTmp["CHOUSEI"] = "";
                    rowTmp["YOUKIBIKI"] = "";
                    rowTmp["SHOUMI"] = "";
                    rowTmp["SUURYOU"] = "";
                    rowTmp["FHN_SUURYOU_TANI"] = "";
                    rowTmp["FHN_HINMEICD"] = "";
                    rowTmp["HINMEI"] = "";
                    rowTmp["TANKA"] = "";
                    rowTmp["KINGAKU"] = "";

                    dtDetail.Rows.Add(rowTmp);
                }
            }

            return dtDetail;
        }

        /// <summary>
        /// 仕切書フッター部データ受渡用文字列作成
        /// </summary>
        /// <returns>仕切書フッター部データ受渡用文字列</returns>
        private DataTable CreateFooterTable(DENPYO_SHIKIRISHO_KIND Type)
        {
            DataTable dtFooter = new DataTable();
            DataRow rowTmp;
            dtFooter.TableName = "Footer";

            // 現場名
            dtFooter.Columns.Add("GENBA");
            // 正味合計
            dtFooter.Columns.Add("SHOUMI_GOUKEI");
            // 合計金額
            dtFooter.Columns.Add("GOUKEI_KINGAKU");
            // 備考
            dtFooter.Columns.Add("BIKOU");

            // 上段の請求・支払いラベル
            dtFooter.Columns.Add("SEIKYUU_SHIHARAI1");
            // 上段の請求・前回残高
            dtFooter.Columns.Add("UP_ZENKAI_ZANDAKA");
            // 上段の請求・伝票額（税抜）
            dtFooter.Columns.Add("UP_DENPYOUGAKU");
            // 上段の請求・消費税
            dtFooter.Columns.Add("UP_SHOUHIZEI");
            // 上段の請求・合計（税込）
            dtFooter.Columns.Add("UP_GOUKEI_ZEIKOMI");
            // 上段の請求・御精算額
            dtFooter.Columns.Add("UP_SEISANGAKU");
            // 上段の請求・差引残高
            dtFooter.Columns.Add("UP_SASHIHIKIZANDAKA");
            // 下段の請求・支払いラベル
            dtFooter.Columns.Add("SEIKYUU_SHIHARAI2");
            // 下段の請求・前回残高
            dtFooter.Columns.Add("DOWN_ZENKAI_ZANDAKA");
            // 下段の請求・伝票額（税抜）
            dtFooter.Columns.Add("DOWN_DENPYOUGAKU");
            // 下段の請求・消費税
            dtFooter.Columns.Add("DOWN_SHOUHIZEI");
            // 下段の請求・合計（税込）
            dtFooter.Columns.Add("DOWN_GOUKEI_ZEIKOMI");
            // 下段の請求・御精算額
            dtFooter.Columns.Add("DOWN_SEISANGAKU");
            // 下段の請求・差引残高
            dtFooter.Columns.Add("DOWN_SASHIHIKIZANDAKA");

            // 計量情報計量証明項目1
            dtFooter.Columns.Add("KEIRYOU_JYOUHOU1");
            // 計量情報計量証明項目2
            dtFooter.Columns.Add("KEIRYOU_JYOUHOU2");
            // 計量情報計量証明項目3
            dtFooter.Columns.Add("KEIRYOU_JYOUHOU3");

            // 会社名
            dtFooter.Columns.Add("CORP_RYAKU_NAME");
            // 拠点
            dtFooter.Columns.Add("KYOTEN_NAME");
            // 拠点郵便番号
            dtFooter.Columns.Add("KYOTEN_POST");    // No.3048
            // 拠点住所1
            dtFooter.Columns.Add("KYOTEN_ADDRESS1");
            // 拠点住所2
            dtFooter.Columns.Add("KYOTEN_ADDRESS2");
            // 拠点電話
            dtFooter.Columns.Add("KYOTEN_TEL");
            // 拠点FAX
            dtFooter.Columns.Add("KYOTEN_FAX");

            // 相殺後金額
            dtFooter.Columns.Add("SOUSAI_KINGAKU");

            //登録番号
            dtFooter.Columns.Add("TOUROKU_NO");
            //相殺ラベル①
            dtFooter.Columns.Add("SOUSAI_LBL1");
            //相殺ラベル②
            dtFooter.Columns.Add("SOUSAI_LBL2");
            //課税ラベル
            dtFooter.Columns.Add("KAZEI_LBL");
            //取引先ラベル
            dtFooter.Columns.Add("TORIHIKI_LBL");
 
            rowTmp = dtFooter.NewRow();

            //現場
            rowTmp["GENBA"] = strGenbaName;

            //正味合計
            rowTmp["SHOUMI_GOUKEI"] = this.form.NET_TOTAL.Text;

            //合計金額
            rowTmp["GOUKEI_KINGAKU"] = string.Empty;
            if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
            {
                if (SHIKIRISHO_UR_UTIZEI == 0)
                {
                    rowTmp["GOUKEI_KINGAKU"] = this.form.URIAGE_KINGAKU_TOTAL.Text;
                }
            }
            else if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
            {
                if (SHIKIRISHO_SH_UTIZEI == 0)
                {
                    rowTmp["GOUKEI_KINGAKU"] = this.form.SHIHARAI_KINGAKU_TOTAL.Text;
                }
            }

            //備考
            rowTmp["BIKOU"] = this.form.DENPYOU_BIKOU.Text;           // No.2613
            //rowTmp["BIKOU"] = InsertReturn(this.form.DENPYOU_BIKOU.Text, 5);    // No.2613-->No.3837により改行不要

            string strPrefixForSeikyu = "";
            string strPrefixForShiharai = "";
            string strLabelKeyForSeikyu = "";
            string strLabelKeyForShiharai = "";
            string strLabelSousaiKbn = "";
            decimal decTopSeisangaku = 0;
            decimal decBottomSeisangaku = 0;
            string strUrShCalcBaseKbn = this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.ToString();
            if (strUrShCalcBaseKbn == "1")
            {
                strPrefixForSeikyu = "UP";
                strPrefixForShiharai = "DOWN";
                strLabelKeyForSeikyu = "SEIKYUU_SHIHARAI1";
                strLabelKeyForShiharai = "SEIKYUU_SHIHARAI2";
            }
            else
            {
                strPrefixForSeikyu = "DOWN";
                strPrefixForShiharai = "UP";
                strLabelKeyForShiharai = "SEIKYUU_SHIHARAI1";
                strLabelKeyForSeikyu = "SEIKYUU_SHIHARAI2";
            }

            if (Type == DENPYO_SHIKIRISHO_KIND.SOUSAI)
            {
                #region 相殺
                /*********請求*********/
                {
                    //ラベル
                    rowTmp[strLabelKeyForSeikyu] = "請求";

                    //前回残高
                    rowTmp[strPrefixForSeikyu + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka;

                    //伝票額(税抜)
                    rowTmp[strPrefixForSeikyu + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Kingaku;

                    //消費税
                    rowTmp[strPrefixForSeikyu + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Zeigaku;

                    //合計(税込)
                    rowTmp[strPrefixForSeikyu + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    int isSeikyu_Seisan_Kbn = 1;

                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka, out decZenkai_Zentaka);
                    int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn, out isSeikyu_Seisan_Kbn);
                    string strGoseisangaku = CommonCalc.DecimalFormat(decSousaiKingaku + decNyushukkinKingaku);
                    rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = strGoseisangaku;

                    string baseKbn = this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.ToString();

                    // 支払精算区分「1.する」の場合
                    if (isSeikyu_Seisan_Kbn == 1)
                    {
                        if (baseKbn == "1")
                        {
                            decBottomSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decTopSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }
                    else
                    {
                        if (baseKbn == "1")
                        {
                            decTopSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decBottomSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }

                    //差引残高
                    rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = this.form.denpyouHakouPopUpDTO.Seikyu_Sagaku_Zentaka;

                }

                /*********支払*********/
                {
                    //ラベル
                    rowTmp[strLabelKeyForShiharai] = "支払";

                    //前回残高
                    rowTmp[strPrefixForShiharai + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka;

                    //伝票額(税抜)
                    rowTmp[strPrefixForShiharai + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Kingaku;

                    //消費税
                    rowTmp[strPrefixForShiharai + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Zeigaku;

                    //合計(税込)
                    rowTmp[strPrefixForShiharai + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    int isShiharai_Seisan_Kbn = 1;

                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka, out decZenkai_Zentaka);
                    int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Seisan_Kbn, out isShiharai_Seisan_Kbn);
                    string strGoseisangaku = CommonCalc.DecimalFormat(decSousaiKingaku + decNyushukkinKingaku);
                    rowTmp[strPrefixForShiharai + "_SEISANGAKU"] = strGoseisangaku;

                    string baseKbn = this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.ToString();

                    // 支払精算区分「1.する」の場合
                    if (isShiharai_Seisan_Kbn == 1)
                    {
                        if (baseKbn == "1")
                        {
                            decBottomSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decTopSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }
                    else
                    {
                        if (baseKbn == "1")
                        {
                            decBottomSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decTopSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }

                    //差引残高
                    rowTmp[strPrefixForShiharai + "_SASHIHIKIZANDAKA"] = this.form.denpyouHakouPopUpDTO.Shiharai_Sagaku_Zentaka;
                }               
                #endregion 相殺
            }
            else
            {
                #region 売上or支払
                strPrefixForSeikyu = "UP";
                strPrefixForShiharai = "DOWN";
                strLabelKeyForSeikyu = "SEIKYUU_SHIHARAI1";
                strLabelKeyForShiharai = "SEIKYUU_SHIHARAI2";
                /*********上段表示*********/
                if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                {
                    #region 仕切書(売上)
                    //ラベル
                    rowTmp[strLabelKeyForSeikyu] = "請求";
                    //前回残高
                    rowTmp[strPrefixForSeikyu + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka;
                    //伝票額(税抜)
                    rowTmp[strPrefixForSeikyu + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Kingaku;
                    //消費税
                    rowTmp[strPrefixForSeikyu + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Zeigaku;
                    //合計(税込)
                    rowTmp[strPrefixForSeikyu + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    string strGoseisangaku = string.Empty;
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka, out decZenkai_Zentaka);

                    //相殺する
                    if (this.form.denpyouHakouPopUpDTO.Sosatu == "1")
                    {
                        rowTmp["SOUSAI_LBL1"] = "相殺金額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decSousaiKingaku;

                        rowTmp["SOUSAI_LBL2"] = "御請求額";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decSousaiKingaku - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }
                    else
                    {
                        rowTmp["SOUSAI_LBL1"] = "御請求額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decKonkai_Torihiki;
                        rowTmp["SOUSAI_LBL2"] = "差引残高";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }

                    /*********下段表示*********/
                    rowTmp[strLabelKeyForShiharai] = "内訳";
                    rowTmp["KAZEI_LBL"] = string.Format("{0:0%}", Decimal.Parse(this.form.denpyouHakouPopUpDTO.Seikyu_Syohizei_Ritu)) + "対象";
                    if (this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU != "0")
                    {
                        //課税金額
                        rowTmp[strPrefixForShiharai + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU;
                        //課税消費税
                        rowTmp[strPrefixForShiharai + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI;
                    }
                    if (this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU != "0")
                    {
                        //非課税額
                        rowTmp[strPrefixForShiharai + "_SEISANGAKU"] = this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU;
                        //非課税消費税
                        rowTmp[strPrefixForShiharai + "_SASHIHIKIZANDAKA"] = string.Empty;
                    }
                    //取引先
                    rowTmp["TORIHIKI_LBL"] = "取引先";

                    #endregion 仕切書(売上)

                }
                if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
                {
                    #region 仕切書(支払)
                    /*********上段表示*********/
                    //ラベル
                    rowTmp[strLabelKeyForSeikyu] = "支払";
                    //前回残高
                    rowTmp[strPrefixForSeikyu + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka;
                    //伝票額(税抜)
                    rowTmp[strPrefixForSeikyu + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Kingaku;
                    //消費税
                    rowTmp[strPrefixForSeikyu + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Zeigaku;
                    //合計(税込)
                    rowTmp[strPrefixForSeikyu + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    string strGoseisangaku = string.Empty;

                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka, out decZenkai_Zentaka);

                    //相殺する
                    if (this.form.denpyouHakouPopUpDTO.Sosatu == "1")
                    {
                        rowTmp["SOUSAI_LBL1"] = "相殺金額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decSousaiKingaku;

                        rowTmp["SOUSAI_LBL2"] = "御精算額";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decSousaiKingaku - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }
                    else
                    {
                        rowTmp["SOUSAI_LBL1"] = "御精算額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decKonkai_Torihiki;
                        rowTmp["SOUSAI_LBL2"] = "差引残高";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }

                    /*********下段表示*********/
                    rowTmp[strLabelKeyForShiharai] = "内訳";
                    rowTmp["KAZEI_LBL"] = string.Format("{0:0%}", Decimal.Parse(this.form.denpyouHakouPopUpDTO.Shiharai_Syohizei_Ritu)) + "対象";
                    if (this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU_SHIHARAI != "0")
                    {
                        //課税金額
                        rowTmp[strPrefixForShiharai + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU_SHIHARAI;
                        //課税消費税
                        rowTmp[strPrefixForShiharai + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI_SHIHARAI;
                    }
                    if (this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU_SHIHARAI != "0")
                    {
                        //非課税額
                        rowTmp[strPrefixForShiharai + "_SEISANGAKU"] = this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU_SHIHARAI;
                        //非課税消費税
                        rowTmp[strPrefixForShiharai + "_SASHIHIKIZANDAKA"] = string.Empty;
                    }
                    //取引先
                    rowTmp["TORIHIKI_LBL"] = "取引先";
                    // 取引先支払情報
                    var torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
                    if (torihikisakiShiharaiEntity != null)
                    {
                        if (!string.IsNullOrEmpty(torihikisakiShiharaiEntity.TOUROKU_NO))
                        {
                            rowTmp["TORIHIKI_LBL"] = "取引先(登録番号：" + torihikisakiShiharaiEntity.TOUROKU_NO + ")";
                        }
                    }

                    #endregion 仕切書(支払)
                }
                #endregion 売上or支払
            }

            //会社名
            rowTmp["CORP_RYAKU_NAME"] = "";
            M_CORP_INFO entCorpInfo = CommonShogunData.CORP_INFO;
            if (entCorpInfo != null)
            {
                if (!string.IsNullOrEmpty(entCorpInfo.CORP_NAME))
                {
                    rowTmp["CORP_RYAKU_NAME"] = entCorpInfo.CORP_NAME;
                }
                if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                {
                    if (!string.IsNullOrEmpty(entCorpInfo.TOUROKU_NO))
                    {
                        rowTmp["TOUROKU_NO"] = entCorpInfo.TOUROKU_NO;
                    }
                }
            }

            rowTmp["KYOTEN_NAME"] = "";
            rowTmp["KYOTEN_POST"] = ""; // No.3048
            rowTmp["KYOTEN_ADDRESS1"] = "";
            rowTmp["KYOTEN_ADDRESS2"] = "";
            rowTmp["KYOTEN_TEL"] = "";
            rowTmp["KYOTEN_FAX"] = "";
            rowTmp["KEIRYOU_JYOUHOU1"] = "";
            rowTmp["KEIRYOU_JYOUHOU2"] = "";
            rowTmp["KEIRYOU_JYOUHOU3"] = "";

            if (entKyotenInfo != null)
            {
                //拠点
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_NAME))
                {
                    rowTmp["KYOTEN_NAME"] = entKyotenInfo.KYOTEN_NAME;
                }

                //No.3048-->
                //拠点FAX
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_POST))
                {
                    rowTmp["KYOTEN_POST"] = entKyotenInfo.KYOTEN_POST;
                }
                // No.3048<--

                //拠点住所1
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_ADDRESS1))
                {
                    rowTmp["KYOTEN_ADDRESS1"] = entKyotenInfo.KYOTEN_ADDRESS1;
                }

                //拠点住所2
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_ADDRESS2))
                {

                    rowTmp["KYOTEN_ADDRESS2"] = entKyotenInfo.KYOTEN_ADDRESS2;
                }

                //拠点電話
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_TEL))
                {
                    rowTmp["KYOTEN_TEL"] = entKyotenInfo.KYOTEN_TEL;
                }

                //拠点FAX
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_FAX))
                {
                    rowTmp["KYOTEN_FAX"] = entKyotenInfo.KYOTEN_FAX;
                }

                //計量情報計量証明項目1
                if (!string.IsNullOrEmpty(entKyotenInfo.KEIRYOU_SHOUMEI_1))
                {
                    rowTmp["KEIRYOU_JYOUHOU1"] = entKyotenInfo.KEIRYOU_SHOUMEI_1;
                }

                //計量情報計量証明項目2
                if (!string.IsNullOrEmpty(entKyotenInfo.KEIRYOU_SHOUMEI_2))
                {
                    rowTmp["KEIRYOU_JYOUHOU2"] = entKyotenInfo.KEIRYOU_SHOUMEI_2;
                }

                //計量情報計量証明項目3
                if (!string.IsNullOrEmpty(entKyotenInfo.KEIRYOU_SHOUMEI_3))
                {
                    rowTmp["KEIRYOU_JYOUHOU3"] = entKyotenInfo.KEIRYOU_SHOUMEI_3;
                }
            }

            //相殺後金額
            if (Type == DENPYO_SHIKIRISHO_KIND.SOUSAI && Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.SOUSATU_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Sosatu))
            {
                decimal decSousaigoKingaku = decTopSeisangaku - decBottomSeisangaku;

                string baseKbn = this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.ToString();
                if (baseKbn == "1")
                {
                    if (decSousaigoKingaku == 0)
                    {
                        strLabelSousaiKbn = "金額";
                    }
                    else if (decSousaigoKingaku.ToString().Contains("-"))
                    {
                        strLabelSousaiKbn = "支払金額";
                    }
                    else
                    {
                        strLabelSousaiKbn = "請求金額";
                    }
                }
                else
                {
                    if (decSousaigoKingaku == 0)
                    {
                        strLabelSousaiKbn = "金額";
                    }
                    else if (decSousaigoKingaku.ToString().Contains("-"))
                    {
                        strLabelSousaiKbn = "請求金額";
                    }
                    else
                    {
                        strLabelSousaiKbn = "支払金額";
                    }
                }

                string strSousaigoKingaku = CommonCalc.DecimalFormat(decSousaigoKingaku);
                strSousaigoKingaku = strSousaigoKingaku.Replace("-", "");
                rowTmp["SOUSAI_KINGAKU"] = "相殺後" + strLabelSousaiKbn + ":" + strSousaigoKingaku;
            }
            else
            {
                rowTmp["SOUSAI_KINGAKU"] = "";
            }

            dtFooter.Rows.Add(rowTmp);

            return dtFooter;
        }

        //---計量票追加---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 帳票(計量票)出力
        /// </summary>
        internal bool PrintKeiryouhyou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // サブファンクションから呼ばれたときだけ確認のポップアップを上げる
                if (isSubFunctionCall)
                {
                    DialogResult ret = msgLogic.MessageBoxShow("C047", "計量票");
                    if (ret == DialogResult.No)
                    {
                        return true;
                    }
                }

                ReportInfoR354_R549_R550_R680_R681 reportInfo = new ReportInfoR354_R549_R550_R680_R681(this.form.WindowId);
                reportInfo.sysDate = this.footerForm.sysDate;
                if (!string.IsNullOrEmpty(Convert.ToString(this.form.DENPYOU_DATE.Value)))
                {
                    reportInfo.DenpyouDate = Convert.ToDateTime(this.form.DENPYOU_DATE.Value);
                }
                else
                {
                    reportInfo.DenpyouDate = Convert.ToDateTime(this.footerForm.sysDate);
                }
                string layoutName = string.Empty;
                string projectId = string.Empty;

                //システム設定[M_SYS_INFO].計量情報計量票レイアウト区分[KEIRYOU_LAYOUT_KBN]
                //システム設定[M_SYS_INFO].計量情報計量票品数区分[KEIRYOU_GOODS_KBN]
                //this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN = 1;
                //this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN = 1;
                if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.IsNull)
                {
                    return true;
                }
                switch ((int)this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN)
                {
                    case 1:
                        // A4縦
                        reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.Normal;
                        layoutName = "LAYOUT1";
                        projectId = "R354";
                        break;
                    case 2:
                        // A4横
                        if (this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN == 1)
                        {
                            // 単品目
                            if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 1)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH;
                                layoutName = "LAYOUT3";
                                projectId = "R550";
                            }
                            // 単品目 取引先なし
                            else if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 2)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH_NoTorihikisaki;
                                layoutName = "LAYOUT5";
                                projectId = "R680";
                            }
                        }
                        else if (this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN == 2)
                        {
                            // 複数品目
                            if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 1)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH;
                                layoutName = "LAYOUT2";
                                projectId = "R549";
                            }
                            // 複数品目 取引先なし
                            else if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 2)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH_NoTorihikisaki;
                                layoutName = "LAYOUT4";
                                projectId = "R681";
                            }
                        }
                        else
                        {
                            // なければなにもできないので終了
                            return true;
                        }
                        break;

                    default:
                        // なければなにもできないので終了
                        return true;
                }

                // データセット
                reportInfo = CreateKeiryouReport(reportInfo);
                reportInfo.Create(@".\Template\R354_R549_R550_R680_R681-Form.xml", layoutName, new DataTable());
                reportInfo.Title = "計量票";

                FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo, projectId);

                // 印刷設定の取得
                //reportPopup.SetPrintSetting(SalesPaymentConstans.KEIRYOUHYOU);

                // 印刷アプリ初期動作(直印刷)
                reportPopup.PrintInitAction = 1;

                // 印刷実行
                reportPopup.PrintXPS(true, true);
                //reportPopup.ShowDialog();
                reportPopup.Dispose();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("PrintKeiryouhyou", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("PrintKeiryouhyou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private ReportInfoR354_R549_R550_R680_R681 CreateKeiryouReport(ReportInfoR354_R549_R550_R680_R681 reportInfo)
        {
            bool catchErr = false;
            // 取引先マスタ検索
            M_TORIHIKISAKI toriEntity = new M_TORIHIKISAKI();
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                toriEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, out catchErr);
                if (catchErr) { throw new Exception(""); }
            }
            // 業者マスタ検索
            M_GYOUSHA gyousEntity = new M_GYOUSHA();
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                gyousEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, out catchErr);
                if (catchErr) { throw new Exception(""); }
            }
            // 現場マスタ検索
            M_GENBA genbaEntity = new M_GENBA();
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                genbaEntity = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, out catchErr);
                if (catchErr) { throw new Exception(""); }
            }
            // 拠点マスタ検索(拠点マスタ.拠点CD＝0の拠点マスタ)
            M_KYOTEN kyotenEntitys = null;
            short kyotenCd;
            if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
            {
                M_KYOTEN[] kyotens = accessor.GetAllDataByCodeForKyoten(kyotenCd);
                if (kyotens != null && kyotens.Count() > 0)
                {
                    // 拠点CDで絞り込んだら一件しか取れないはず
                    kyotenEntitys = kyotens[0];
                }
            }

            // 品名マスタ取得用
            M_HINMEI targetHinmei = null;

            // 調整合計
            decimal chosei = 0;     //画面．明細．調整Kgの合計
            decimal youki = 0;      //画面．明細．容器重量の合計
            foreach (Row row in this.form.gcMultiRow1.Rows)
            {
                if (row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value != null)
                {
                    decimal choseiJyuuryou = 0;
                    if (decimal.TryParse(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].FormattedValue.ToString(), out choseiJyuuryou))
                    {
                        chosei += choseiJyuuryou;
                    }
                }
                if (row.Cells[CELL_NAME_YOUKI_JYUURYOU].Value != null)
                {
                    decimal youkiJyuuryou = 0;
                    if (decimal.TryParse(row.Cells[CELL_NAME_YOUKI_JYUURYOU].FormattedValue.ToString(), out youkiJyuuryou))
                    {
                        youki += youkiJyuuryou;
                    }
                }
            }

            // データセット
            DataRow rowTmp;
            // Header部
            DataTable dataTableTmpH;
            dataTableTmpH = new DataTable();
            dataTableTmpH.TableName = "Header";
            // Detail部
            DataTable dataTableTmpD;
            dataTableTmpD = new DataTable();
            dataTableTmpD.TableName = "Detail";
            // Footer部
            DataTable dataTableTmpF;
            dataTableTmpF = new DataTable();
            dataTableTmpF.TableName = "Footer";
            //20150619 #10535 hoanghm start
            //DateTime Date = (DateTime)this.form.DENPYOU_DATE.Value;
            DateTime Date = this.footerForm.sysDate;
            if (this.form.DENPYOU_DATE.Value != null)
            {
                Date = (DateTime)this.form.DENPYOU_DATE.Value;
            }
            //20150619 #10535 hoanghm end

            switch (reportInfo.OutputType)
            {
                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.Normal:    // A4 縦三つ切り

                    #region - A4 縦三つ切り -

                    // 受入、出荷入力から出力する場合は取引先欄に業者情報を出力する。
                    reportInfo.DispTypeForNormal = ReportInfoR354_R549_R550_R680_R681.DispTypeForNormalDef.Gyousha;

                    // Header部

                    //タイトル名
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    //担当名
                    dataTableTmpH.Columns.Add("TANTOU");
                    //業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    //業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    //業者名敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    //伝票No
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    //乗員
                    dataTableTmpH.Columns.Add("JYOUIN");
                    //車番
                    dataTableTmpH.Columns.Add("SHABAN");
                    //伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");

                    rowTmp = dataTableTmpH.NewRow();

                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;

                    //rowTmp["TANTOU"] = this.form.NYUURYOKU_TANTOUSHA_NAME.Text; // No.3279
                    rowTmp["TANTOU"] = strNyuryokuTantousyaName; // No.3279

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            // No.2996-->
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                            // No.2996<--
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }

                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull
                        && !this.dto.entryEntity.SHUKKA_NUMBER.ToString().Equals(this.form.ShukkaNumber.ToString()))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.SHUKKA_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    rowTmp["JYOUIN"] = this.form.NINZUU_CNT.Text;
                    rowTmp["SHABAN"] = this.form.SHARYOU_NAME_RYAKU.Text;
                    //20150619 #10535 hoanghm start
                    //rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    //20150619 #10535 hoanghm end

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部
                    dataTableTmpD.Columns.Add("ROW_NO");
                    dataTableTmpD.Columns.Add("STACK_JYUURYOU");
                    dataTableTmpD.Columns.Add("EMPTY_JYUURYOU");
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    dataTableTmpD.Columns.Add("KEIRYOU_TIME");

                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        // 未確定行は無視
                        if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                        {
                            continue;
                        }

                        rowTmp = dataTableTmpD.NewRow();

                        //No
                        rowTmp["ROW_NO"] = row.Cells[CELL_NAME_ROW_NO].Value;
                        //総重量
                        rowTmp["STACK_JYUURYOU"] = row.Cells[CELL_NAME_STAK_JYUURYOU].DisplayText;
                        //空車重量
                        rowTmp["EMPTY_JYUURYOU"] = row.Cells[CELL_NAME_EMPTY_JYUURYOU].DisplayText;
                        //調整
                        rowTmp["NET_CHOUSEI"] = row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].DisplayText;
                        //容器引
                        rowTmp["YOUKI_JYUURYOU"] = row.Cells[CELL_NAME_YOUKI_JYUURYOU].DisplayText;
                        //正味
                        rowTmp["NET_JYUURYOU"] = row.Cells[CELL_NAME_NET_JYUURYOU].DisplayText;
                        //品名CD
                        rowTmp["HINMEI_CD"] = row.Cells[CELL_NAME_HINMEI_CD].DisplayText;
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        // 品名マスタ．品名
                        rowTmp["HINMEI_NAME"] = row.Cells[CELL_NAME_HINMEI_NAME].DisplayText;
                        //targetHinmei = null;
                        //if (row.Cells[CELL_NAME_HINMEI_CD].Value != null)
                        //{
                        //    if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                        //    {
                        //        targetHinmei = this.accessor.GetHinmeiDataByCd(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                        //    }
                        //}
                        //if (targetHinmei != null)
                        //{
                        //    rowTmp["HINMEI_NAME"] = targetHinmei.HINMEI_NAME;
                        //}
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                        //計量時間（この画面からは空文字とする）
                        rowTmp["KEIRYOU_TIME"] = string.Empty;

                        dataTableTmpD.Rows.Add(rowTmp);
                    }

                    // Footer部
                    dataTableTmpF.Columns.Add("GENBA_CD");
                    dataTableTmpF.Columns.Add("GENBA_NAME");
                    dataTableTmpF.Columns.Add("NET_JYUURYOU_TOTAL");
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    //現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    //現場名
                    //rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text;    // No.3279
                    //rowTmp["GENBA_NAME"] = strGenbaName;    // No.3279
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の現場
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text;
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }
                    //正味合計
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)//if does not exits detail then set blank
                    {
                        rowTmp["NET_JYUURYOU_TOTAL"] = this.form.NET_TOTAL.Text;
                    }
                    //備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;

                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }
                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - A4 縦三つ切り -

                    break;

                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH:   // 三つ切り 複数品目

                    #region - 三つ切り 複数品目 -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 取引先CD
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_CD");
                    // 取引先名
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_NAME");
                    // 取引先敬称
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_KEISYOU");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU"); // No.3169により追加
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // 総重量
                    dataTableTmpH.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpH.Columns.Add("EMPTY_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmpH.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmpH.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    //20150619 #10535 hoanghm start
                    //rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    //20150619 #10535 hoanghm end
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull
                        && !this.dto.entryEntity.SHUKKA_NUMBER.ToString().Equals(this.form.ShukkaNumber.ToString()))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.SHUKKA_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 取引先CD
                    rowTmp["TORIHIKISAKI_CD"] = this.form.TORIHIKISAKI_CD.Text;
                    // 取引先マスタ．取引先名1
                    rowTmp["TORIHIKISAKI_NAME"] = string.Empty;
                    if (toriEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の取引先
                        rowTmp["TORIHIKISAKI_NAME"] = this.form.TORIHIKISAKI_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (toriEntity.TORIHIKISAKI_NAME1 != null)
                        {
                            // No.2996-->
                            if (toriEntity.TORIHIKISAKI_NAME2 != null)
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1 + "\n" + toriEntity.TORIHIKISAKI_NAME2;
                            }
                            else
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1;
                            }
                            // No.2996<--
                        }
                    }
                    // 取引先マスタ．取引先敬称1
                    rowTmp["TORIHIKISAKI_KEISYOU"] = string.Empty;
                    if (toriEntity.TORIHIKISAKI_KEISHOU1 != null)
                    {
                        if (toriEntity.TORIHIKISAKI_KEISHOU2 != null)
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1 + "\n" + toriEntity.TORIHIKISAKI_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1;
                        }
                    }
                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            // No.2996-->
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                            // No.2996<--
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            // No.2996-->
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                            // No.2996<--
                        }
                    }

                    // No.3169で追加-->
                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }
                    // No.3169で追加<--

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    // 総重量、空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                    rowTmp["STACK_JYUURYOU"] = GetJuryoCol(0);
                    rowTmp["EMPTY_JYUURYOU"] = GetJuryoCol(1);

                    // 総重量計量時間、空車重量計量時間(この画面からは空文字とする)
                    rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;

                    //バーコード（02＋伝票番号＋差額）
                    rowTmp["BARCODE"] = "02"
                            + ((this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull) ? this.dto.entryEntity.SHUKKA_NUMBER.ToString().PadLeft(4, '0') : this.form.ENTRY_NUMBER.Text.PadLeft(4, '0'))
                            + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部

                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    // 計量時間
                    dataTableTmpD.Columns.Add("KEIRYOU_TIME");

                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        // 未確定行は無視
                        if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                        {
                            continue;
                        }

                        rowTmp = dataTableTmpD.NewRow();

                        // 20160126 chenzz #13337 品名手入力に関する機能修正 start
                        rowTmp["HINMEI_CD"] = row.Cells[CELL_NAME_HINMEI_CD].DisplayText;
                        // 品名マスタ．品名
                        //targetHinmei = null;
                        //if (row.Cells[CELL_NAME_HINMEI_CD].Value != null)
                        //{
                        //    if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                        //    {
                        //        targetHinmei = this.accessor.GetHinmeiDataByCd(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                        //    }
                        //}
                        //if (targetHinmei != null)
                        //{
                        //    rowTmp["HINMEI_NAME"] = targetHinmei.HINMEI_NAME;
                        //}
                        rowTmp["HINMEI_NAME"] = row.Cells[CELL_NAME_HINMEI_NAME].DisplayText;
                        // 20160126 chenzz #13337 品名手入力に関する機能修正 end
                        rowTmp["NET_CHOUSEI"] = row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].DisplayText;
                        rowTmp["YOUKI_JYUURYOU"] = row.Cells[CELL_NAME_YOUKI_JYUURYOU].DisplayText;
                        rowTmp["NET_JYUURYOU"] = row.Cells[CELL_NAME_NET_JYUURYOU].DisplayText;

                        // 計量時間(この画面からは空文字とする)
                        rowTmp["KEIRYOU_TIME"] = string.Empty;

                        dataTableTmpD.Rows.Add(rowTmp);
                    }

                    // Footer部

                    // 調整合計
                    dataTableTmpF.Columns.Add("NET_CHOSEI_TOTAL");
                    // 容器引合計
                    dataTableTmpF.Columns.Add("YOUKI_JYUURYOU_TOTAL");
                    // 正味合計
                    dataTableTmpF.Columns.Add("NET_JYUURYOU_TOTAL");
                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    //20150619 #10534 3桁のカンマ区切りを表示するようにする hoanghm start
                    // 調整合計
                    //rowTmp["NET_CHOSEI_TOTAL"] = chosei;
                    if (chosei > 0)// if value is zero then set blank
                    {
                        rowTmp["NET_CHOSEI_TOTAL"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引合計
                    //rowTmp["YOUKI_JYUURYOU_TOTAL"] = youki;
                    if (youki > 0)// if value is zero then set blank
                    {
                        rowTmp["YOUKI_JYUURYOU_TOTAL"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    //20150619 #10534 3桁のカンマ区切りを表示するようにする hoanghm end
                    // 正味合計
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)//if does not exits detail then set blank
                    {
                        rowTmp["NET_JYUURYOU_TOTAL"] = this.form.NET_TOTAL.Text;
                    }
                    // 伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;
                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 複数品目 -

                    break;

                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH:   // 三つ切り 単品目

                    #region - 三つ切り 単品目 -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 取引先CD
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_CD");
                    // 取引先名
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_NAME");
                    // 取引先敬称
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_KEISYOU");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU"); // No.3169により追加
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    //20150619 #10535 hoanghm start
                    //rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    //20150619 #10535 hoanghm end
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull
                        && !this.dto.entryEntity.SHUKKA_NUMBER.ToString().Equals(this.form.ShukkaNumber.ToString()))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.SHUKKA_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 取引先CD
                    rowTmp["TORIHIKISAKI_CD"] = this.form.TORIHIKISAKI_CD.Text;
                    // 取引先マスタ．取引先名1
                    rowTmp["TORIHIKISAKI_NAME"] = string.Empty;
                    if (toriEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の取引先
                        rowTmp["TORIHIKISAKI_NAME"] = this.form.TORIHIKISAKI_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (toriEntity.TORIHIKISAKI_NAME1 != null)
                        {
                            // No.2996-->
                            if (toriEntity.TORIHIKISAKI_NAME2 != null)
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1 + "\n" + toriEntity.TORIHIKISAKI_NAME2;
                            }
                            else
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1;
                            }
                            // No.2996<--
                        }
                    }
                    // 取引先マスタ．取引先敬称1
                    rowTmp["TORIHIKISAKI_KEISYOU"] = string.Empty;
                    if (toriEntity.TORIHIKISAKI_KEISHOU1 != null)
                    {
                        if (toriEntity.TORIHIKISAKI_KEISHOU2 != null)
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1 + "\n" + toriEntity.TORIHIKISAKI_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1;
                        }
                    }
                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            // No.2996-->
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                            // No.2996<--
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            // No.2996-->
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                            // No.2996<--
                        }
                    }

                    // No.3169で追加-->
                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }
                    // No.3169で追加<--

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    //バーコード（02＋伝票番号＋差額）
                    rowTmp["BARCODE"] = "02"
                            + ((this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull) ? this.dto.entryEntity.SHUKKA_NUMBER.ToString().PadLeft(4, '0') : this.form.ENTRY_NUMBER.Text.PadLeft(4, '0'))
                            + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部なし
                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 総重量
                    dataTableTmpD.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpD.Columns.Add("EMPTY_JYUURYOU");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmpD.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmpD.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // 計量時間
                    dataTableTmpD.Columns.Add("NET_JYUURYOU_TIME");

                    rowTmp = dataTableTmpD.NewRow();

                    // 未確定行は無視
                    rowTmp["HINMEI_CD"] = string.Empty;
                    rowTmp["HINMEI_NAME"] = string.Empty;
                    rowTmp["STACK_JYUURYOU"] = string.Empty;
                    rowTmp["EMPTY_JYUURYOU"] = string.Empty;
                    rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["NET_JYUURYOU_TIME"] = string.Empty;
                    if (this.form.gcMultiRow1.Rows.Count > 1)
                    {
                        // 20160126 chenzz #13337 品名手入力に関する機能修正 start
                        // 品名CD
                        rowTmp["HINMEI_CD"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_CD].Value;
                        // 品名マスタ．品名
                        //targetHinmei = null;
                        //if (this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_CD].Value != null)
                        //{
                        //    if (!string.IsNullOrEmpty(this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                        //    {
                        //        targetHinmei = this.accessor.GetHinmeiDataByCd(this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                        //    }
                        //}
                        //if (targetHinmei != null)
                        //{
                        //    rowTmp["HINMEI_NAME"] = targetHinmei.HINMEI_NAME;
                        //}
                        rowTmp["HINMEI_NAME"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_NAME].Value;
                        // 20160126 chenzz #13337 品名手入力に関する機能修正 end
                        // 総重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["STACK_JYUURYOU"] = GetJuryoCol(0);
                        // 空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["EMPTY_JYUURYOU"] = GetJuryoCol(1);
                        // 総重量計量時間(この画面からは空文字とする)
                        rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                        // 空車重量計量時間(この画面からは空文字とする)
                        rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;

                    }

                    //20150619 #10534 3桁のカンマ区切りを表示するようにする hoanghm start
                    // 調整
                    //rowTmp["NET_CHOUSEI"] = Decimal.Parse(chosei.ToString());
                    if (chosei > 0)// if value is zero then set blank
                    {
                        rowTmp["NET_CHOUSEI"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引
                    //rowTmp["YOUKI_JYUURYOU"] = Decimal.Parse(youki.ToString());
                    if (youki > 0)// if value is zero then set blank
                    {
                        rowTmp["YOUKI_JYUURYOU"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    //20150619 #10534 3桁のカンマ区切りを表示するようにする hoanghm end

                    // 正味
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)//if does not exits detail then set blank
                    {
                        rowTmp["NET_JYUURYOU"] = this.form.NET_TOTAL.Text;
                    }

                    dataTableTmpD.Rows.Add(rowTmp);

                    // Footer部

                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["DENPYOU_BIKOU"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;

                    //伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 単品目 -

                    break;
                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH_NoTorihikisaki:   // 三つ切り 複数品目 取引先なし

                    #region - 三つ切り 複数品目 取引先なし -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU"); // No.3169により追加
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // 総重量
                    dataTableTmpH.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpH.Columns.Add("EMPTY_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmpH.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmpH.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull
                        && !this.dto.entryEntity.SHUKKA_NUMBER.ToString().Equals(this.form.ShukkaNumber.ToString()))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.SHUKKA_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }

                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    // 総重量、空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                    rowTmp["STACK_JYUURYOU"] = GetJuryoCol(0);
                    rowTmp["EMPTY_JYUURYOU"] = GetJuryoCol(1);

                    // 総重量計量時間、空車重量計量時間（この画面からは空文字とする）
                    rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;

                    //バーコード（02＋伝票番号＋差額）
                    rowTmp["BARCODE"] = "02"
                        + ((this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull) ? this.dto.entryEntity.SHUKKA_NUMBER.ToString().PadLeft(4, '0') : this.form.ENTRY_NUMBER.Text.PadLeft(4, '0'))
                        + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部

                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    // 計量時間
                    dataTableTmpD.Columns.Add("KEIRYOU_TIME");

                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        // 未確定行は無視
                        if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                        {
                            continue;
                        }

                        rowTmp = dataTableTmpD.NewRow();

                        rowTmp["HINMEI_CD"] = row.Cells[CELL_NAME_HINMEI_CD].DisplayText;
                        // 品名マスタ．品名
                        rowTmp["HINMEI_NAME"] = row.Cells[CELL_NAME_HINMEI_NAME].DisplayText;
                        rowTmp["NET_CHOUSEI"] = row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].DisplayText;
                        rowTmp["YOUKI_JYUURYOU"] = row.Cells[CELL_NAME_YOUKI_JYUURYOU].DisplayText;
                        rowTmp["NET_JYUURYOU"] = row.Cells[CELL_NAME_NET_JYUURYOU].DisplayText;

                        // 計量時間(この画面からは空文字とする)
                        rowTmp["KEIRYOU_TIME"] = string.Empty;

                        dataTableTmpD.Rows.Add(rowTmp);
                    }

                    // Footer部

                    // 調整合計
                    dataTableTmpF.Columns.Add("NET_CHOSEI_TOTAL");
                    // 容器引合計
                    dataTableTmpF.Columns.Add("YOUKI_JYUURYOU_TOTAL");
                    // 正味合計
                    dataTableTmpF.Columns.Add("NET_JYUURYOU_TOTAL");
                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    // 調整合計
                    if (chosei > 0)
                    {
                        rowTmp["NET_CHOSEI_TOTAL"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引合計
                    if (youki > 0)
                    {
                        rowTmp["YOUKI_JYUURYOU_TOTAL"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 正味合計
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)
                    {
                        rowTmp["NET_JYUURYOU_TOTAL"] = this.form.NET_TOTAL.Text;
                    }
                    // 伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;
                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 複数品目 取引先なし -

                    break;

                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH_NoTorihikisaki:   // 三つ切り 単品目 取引先なし

                    #region - 三つ切り 単品目 取引先なし -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU");
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull
                        && !this.dto.entryEntity.SHUKKA_NUMBER.ToString().Equals(this.form.ShukkaNumber.ToString()))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.SHUKKA_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }

                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    //バーコード（02＋伝票番号＋差額）
                    rowTmp["BARCODE"] = "02"
                            + ((this.dto.entryEntity != null && !this.dto.entryEntity.SHUKKA_NUMBER.IsNull) ? this.dto.entryEntity.SHUKKA_NUMBER.ToString().PadLeft(4, '0') : this.form.ENTRY_NUMBER.Text.PadLeft(4, '0'))
                            + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部なし
                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 総重量
                    dataTableTmpD.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpD.Columns.Add("EMPTY_JYUURYOU");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmpD.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmpD.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // 計量時間
                    dataTableTmpD.Columns.Add("NET_JYUURYOU_TIME");

                    rowTmp = dataTableTmpD.NewRow();

                    // 未確定行は無視
                    rowTmp["HINMEI_CD"] = string.Empty;
                    rowTmp["HINMEI_NAME"] = string.Empty;
                    rowTmp["STACK_JYUURYOU"] = string.Empty;
                    rowTmp["EMPTY_JYUURYOU"] = string.Empty;
                    rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["NET_JYUURYOU_TIME"] = string.Empty;
                    if (this.form.gcMultiRow1.Rows.Count > 1)
                    {
                        // 品名CD
                        rowTmp["HINMEI_CD"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_CD].Value;
                        // 品名マスタ．品名
                        rowTmp["HINMEI_NAME"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_NAME].Value;
                        // 総重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["STACK_JYUURYOU"] = GetJuryoCol(0);
                        // 空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["EMPTY_JYUURYOU"] = GetJuryoCol(1);
                        // 総重量計量時間(この画面からは空文字とする)
                        rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                        // 空車重量計量時間(この画面からは空文字とする)
                        rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;

                    }

                    // 調整
                    if (chosei > 0)
                    {
                        rowTmp["NET_CHOUSEI"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引
                    if (youki > 0)
                    {
                        rowTmp["YOUKI_JYUURYOU"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }

                    // 正味
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)
                    {
                        rowTmp["NET_JYUURYOU"] = this.form.NET_TOTAL.Text;
                    }

                    dataTableTmpD.Rows.Add(rowTmp);

                    // Footer部

                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["DENPYOU_BIKOU"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;

                    //伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 単品目 取引先なし -

                    break;
            }

            // Header部
            reportInfo.DataTableList.Add("Header", dataTableTmpH);
            // Detail部
            reportInfo.DataTableList.Add("Detail", dataTableTmpD);
            // Footer部
            reportInfo.DataTableList.Add("Footer", dataTableTmpF);

            return reportInfo;

        }
        //---計量票追加---------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 確定区分チェック
        /// 入力CDから名称を表示する処理も実施
        /// </summary>
        internal void CheckKakuteiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                {
                    this.form.KAKUTEI_KBN_NAME.Text = string.Empty;
                    return;
                }

                short kakuteiKbn = 0;
                short.TryParse(this.form.KAKUTEI_KBN.Text, out kakuteiKbn);

                switch (kakuteiKbn)
                {
                    case SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI:
                    case SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI:
                        this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(kakuteiKbn);
                        break;

                    default:
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058");
                        this.form.KAKUTEI_KBN.Focus();
                        break;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKakuteiKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 明細に新規行を追加
        /// </summary>
        internal void AddNewRow()
        {
            LogUtility.DebugMethodStart();

            if ((Row)this.form.gcMultiRow1.CurrentRow != null)
            {
                this.form.gcMultiRow1.EndEdit();
                Row selectedRows = (Row)this.form.gcMultiRow1.CurrentRow;

                int iSaveRowIndex = this.form.gcMultiRow1.CurrentRow.Index;
                this.form.gcMultiRow1.Rows.Insert(this.form.gcMultiRow1.CurrentRow.Index);
                this.form.gcMultiRow1.ClearSelection();

                this.form.gcMultiRow1.AddSelection(iSaveRowIndex);

                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                // 行番号採番
                if (!this.NumberingRowNo())
                {
                    return;
                }
                // jyuuryouDtoを初期化
                this.SetJyuuryouDataToDtoList();

                // 新規行の活性制御がうまくいかないため、このタイミングで活性制御を明示的に呼び出す
                if (!this.WarifuriReadOnlyCheck(this.form.gcMultiRow1.CurrentRow))
                {
                    return;
                }
            }
            LogUtility.DebugMethodStart();
        }

        /// <summary>
        /// 明細のカレント行を削除
        /// </summary>
        internal bool RemoveSelectedRow()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                if ((Row)this.form.gcMultiRow1.CurrentRow != null)
                {
                    // 重量値計算用オブジェクトが最新じゃないかもしれないので再取得
                    this.SetJyuuryouDataToDtoList();

                    this.form.gcMultiRow1.BeginEdit(false);
                    Row selectedRows = (Row)this.form.gcMultiRow1.CurrentRow;
                    if (!selectedRows.IsNewRow)
                    {
                        int warifuriNo = 0;
                        short warifuriRowNo = 0;

                        int.TryParse(Convert.ToString(selectedRows.Cells[CELL_NAME_warihuriNo].Value), out warifuriNo);
                        short.TryParse(Convert.ToString(selectedRows.Cells[CELL_NAME_warihuriRowNo].Value), out warifuriRowNo);

                        // 再計算のためデータを削除
                        if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                            && warifuriNo < this.jyuuryouDtoList.Count
                            && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                        {
                            if (warifuriNo < this.jyuuryouDtoList.Count)
                            {
                                var jyuuryouDtos = this.jyuuryouDtoList[warifuriNo];
                                int i = warifuriRowNo + 1;
                                while (i < jyuuryouDtos.Count)
                                {
                                    jyuuryouDtos[i].warifuriJyuuryou = null;
                                    jyuuryouDtos[i].warifuriPercent = null;
                                    i++;
                                }

                                if (0 < warifuriRowNo)
                                {
                                    // 自分自身を削除
                                    jyuuryouDtos.RemoveAt(warifuriRowNo);
                                    // 再計算のため1つ上の割振を削除
                                    jyuuryouDtos[warifuriRowNo - 1].warifuriJyuuryou = null;
                                    jyuuryouDtos[warifuriRowNo - 1].warifuriPercent = null;
                                }
                                else
                                {
                                    // 先頭行の場合は自分の割振kgと割振%を削除するだけ

                                    // 先頭行の場合はこの値を消す？
                                    this.jyuuryouDtoList.RemoveAt(warifuriNo);
                                }
                            }

                        }

                        // 行削除の後に現在のCellのフォーカスアウトチェックが走ってしまうので、FocusOutCheckMethodを削除
                        var currentCell = this.form.gcMultiRow1.CurrentCell as ICustomControl;
                        if (currentCell != null)
                        {
                            currentCell.FocusOutCheckMethod = null;
                        }

                        // 行削除
                        int iSaveIndex = this.form.gcMultiRow1.CurrentRow.Index;
                        this.form.gcMultiRow1.Rows.Remove(selectedRows);
                        this.form.gcMultiRow1.ClearSelection();
                        this.form.gcMultiRow1.AddSelection(iSaveIndex);

                        // 再計算処理
                        foreach (var jyuuryouDtoList in this.jyuuryouDtoList)
                        {
                            JyuuryouDto.CalcJyuuryouDtoForAdd(
                                jyuuryouDtoList,
                                true,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
                        }
                        // 重量リストを使って重量値の更新
                        this.SetJyuuryouDataToMultiRow();
                    }
                    this.form.gcMultiRow1.EndEdit();
                    this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                    // 行番号採番
                    if (!this.NumberingRowNo())
                    {
                        return false;
                    }
                    this.form.gcMultiRow1.ResumeLayout();

                    // 計算
                    if (!this.CalcDetail()) { return false; }

                    // 行削除時、総重量、空車重量、割振、調整項目の活性制御がされないため、このタイミングで活性制御を行う
                    foreach (var row in this.form.gcMultiRow1.Rows)
                    {
                        if (!this.WarifuriReadOnlyCheck(row))
                        {
                            return false;
                        }
                    }
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("RemoveSelectedRow", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("RemoveSelectedRow", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 伝票区分設定
        /// 明細の品名から伝票区分を設定する
        /// </summary>
        internal bool SetDenpyouKbn()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.gcMultiRow1.CurrentRow;

            if (targetRow == null)
            {
                return true;
            }

            // 初期化
            targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty;
            targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

            if (targetRow.Cells[CELL_NAME_HINMEI_CD].Value == null
                || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
            {
                return true;
            }

            var targetHimei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

            if (targetHimei == null || string.IsNullOrEmpty(targetHimei.HINMEI_CD))
            {
                // 存在しない品名が選択されている場合
                return true;
            }

            switch (targetHimei.DENPYOU_KBN_CD.ToString())
            {
                case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR:
                case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                    targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = (short)targetHimei.DENPYOU_KBN_CD;
                    targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[(short)targetHimei.DENPYOU_KBN_CD].DENPYOU_KBN_NAME_RYAKU;
                    break;

                default:
                    // ポップアップを打ち上げ、ユーザに選択してもらう
                    CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                    CustomControlExtLogic.PopUp((ICustomControl)this.form.gcMultiRow1.Rows[pos.RowIndex].Cells[CELL_NAME_DENPYOU_KBN_CD]);

                    var denpyouKbnCd = targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value;
                    if (denpyouKbnCd == null
                        || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
                    {
                        // ポップアップでキャンセルが押された
                        // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = string.Empty;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

                        //ポップアップキャンセルフラグをTrueにする。
                        this.form.bCancelDenpyoPopup = true;

                        return false;
                    }

                    break;
            }

            LogUtility.DebugMethodStart();

            return true;
        }

        /// <summary>
        /// 検収明細入力画面表示
        /// </summary>
        internal void OpenKenshuMeisaiNyuuryoku()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.form.WindowType)
                    && !WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType))
                {
                    return;
                }

                // 画面に渡す引数をセット
                this.SetKenshuNyuuryokuDTOClass();

                // 検収明細入力画面(G157)をモーダル表示
                var result = FormManager.OpenFormModal("G157", this.dto.kenshuNyuuryokuDto);

                // 日付等の状態セット
                this.SetKenshuDateStatus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("OpenKenshuMeisaiNyuuryoku", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("OpenKenshuMeisaiNyuuryoku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運賃入力画面表示
        /// </summary>
        internal void OpenUnchinNyuuryoku(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 4935_7 出荷入力 jyokou 20150505 str
                // 運賃入力画面(G153)をモーダル表示
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    bool isTaiyuu = !string.IsNullOrEmpty(this.form.ENTRY_NUMBER.Text);
                    this.form.Regist(sender, e);
                    if (isRegistered)
                    {
                        if (isTaiyuu)
                        {
                            T_SHUKKA_ENTRY entry = this.CreateUnchiDateEntity();
                            FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.dto.entryEntity.SHUKKA_NUMBER, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, entry);
                        }
                        else
                        {
                            FormManager.OpenFormModal("G153", WINDOW_TYPE.NEW_WINDOW_FLAG, this.dto.entryEntity.SHUKKA_NUMBER, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA);
                        }
                    }
                }
                else
                {
                    T_SHUKKA_ENTRY entry = this.CreateUnchiDateEntity();
                    FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.dto.entryEntity.SHUKKA_NUMBER, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, entry);
                }
                //// 運賃入力画面(G153)をモーダル表示
                //FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, this.dto.entryEntity.SYSTEM_ID);
                // 4935_7 出荷入力 jyokou 20150505 end
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("OpenUnchinNyuuryoku", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("OpenUnchinNyuuryoku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運賃入力画面データ移行
        /// </summary>
        internal T_SHUKKA_ENTRY CreateUnchiDateEntity()
        {
            LogUtility.DebugMethodStart();
            T_SHUKKA_ENTRY entry = new T_SHUKKA_ENTRY();

            //拠点CD
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                entry.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            }
            //伝票日付
            if (this.form.DENPYOU_DATE.Value != null)
            {
                entry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
            }
            // 運搬業者CD
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                entry.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
            {
                entry.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
            }
            // 車輌CD
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                entry.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
            {
                entry.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
            }
            // 車種CD
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
            {
                entry.SHASHU_CD = this.form.SHASHU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_NAME.Text))
            {
                entry.SHASHU_NAME = this.form.SHASHU_NAME.Text;
            }
            // 運転者CD
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                entry.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_NAME.Text))
            {
                entry.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
            }
            // 形態区分CD
            if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
            {
                entry.KEITAI_KBN_CD = SqlInt16.Parse(this.form.KEITAI_KBN_CD.Text);
            }
            // 業者CD
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                entry.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text))
            {
                entry.GYOUSHA_NAME = this.form.GYOUSHA_NAME_RYAKU.Text;
            }
            // 現場CD
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                entry.GENBA_CD = this.form.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU.Text))
            {
                entry.GENBA_NAME = this.form.GENBA_NAME_RYAKU.Text;
            }
            // 荷積業者CD
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                entry.NIZUMI_GYOUSHA_CD = this.form.NIZUMI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text))
            {
                entry.NIZUMI_GYOUSHA_NAME = this.form.NIZUMI_GYOUSHA_NAME.Text;
            }
            // 荷積現場CD
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                entry.NIZUMI_GENBA_CD = this.form.NIZUMI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))
            {
                entry.NIZUMI_GENBA_NAME = this.form.NIZUMI_GENBA_NAME.Text;
            }
            // 正味合計
            if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text))
            {
                entry.NET_TOTAL = SqlDecimal.Parse(Convert.ToDouble(this.form.NET_TOTAL.Text).ToString());
            }

            LogUtility.DebugMethodEnd();
            return entry;
        }

        /// <summary>
        /// カレント行が割振行であるかないか判定
        /// </summary>
        /// <returns>True:カレント行が割振行, Flase:カレント行が割振行ではない</returns>
        internal bool JudgeWarihuri(out bool catchErr)
        {
            catchErr = false;
            try
            {
                Row selectedRow = (Row)this.form.gcMultiRow1.CurrentRow;
                if (selectedRow != null)
                {
                    if ((selectedRow["WARIFURI_PERCENT"].Value != null) || (selectedRow["WARIFURI_JYUURYOU"].Value != null))
                    {
                        // 行挿入の性質が「選択行の上に行を挿入する」ため、warihuriRowNoが0のもについては行追加OKとする
                        if (selectedRow[CELL_NAME_warihuriRowNo].Value != null
                            && 0 == Convert.ToInt16(selectedRow[CELL_NAME_warihuriRowNo].Value.ToString()))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("JudgeWarihuri", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("JudgeWarihuri", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal void CheckUntensha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return;
                }

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(this.form.UNTENSHA_CD.Text) || !this.tmpUntenshaCd.Equals(this.form.UNTENSHA_CD.Text)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.UNTENSHA_NAME.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                    {
                        // 運転者CDがなければ既にエラーが表示されているので何もしない。
                        return;
                    }

                    var shainEntity = this.accessor.GetShain(this.form.UNTENSHA_CD.Text);
                    if (shainEntity == null)
                    {
                        // エラーメッセージ
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "社員");
                        this.form.UNTENSHA_CD.Focus();
                        this.tmpUntenshaCd = string.Empty;
                        return;
                    }
                    else if (shainEntity.UNTEN_KBN.Equals(SqlBoolean.False))
                    {
                        // エラーメッセージ
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運転者");
                        this.form.UNTENSHA_CD.Focus();
                        this.tmpUntenshaCd = string.Empty;
                    }
                    else
                    {
                        this.form.UNTENSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUntensha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 品名コードの存在チェック（伝種区分が出荷、または共通のみ可）
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns>true: 入力された品名コードが存在する, false: 入力された品名コードが存在しない</returns>
        internal bool CheckHinmeiCd(Row targetRow, out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                if ((targetRow.Cells["HINMEI_CD"].Value == null) || (string.IsNullOrEmpty(targetRow.Cells["HINMEI_CD"].Value.ToString())))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                if (hinmei == null || string.IsNullOrEmpty(hinmei.HINMEI_CD))
                {
                    // 品名コードがマスタに存在しない場合
                    // ただし、部品で存在チェックが行われるため、実際にここを通ることはない
                    return returnVal;
                }
                else
                {
                    // 品名コードがマスタに存在する場合
                    if ((hinmei.DENSHU_KBN_CD != SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA
                        && hinmei.DENSHU_KBN_CD != SalesPaymentConstans.DENSHU_KBN_CD_KYOTU))
                    {
                        // 入力された品名コードに紐づく伝種区分が出荷、共通以外の場合はエラーメッセージ表示
                        targetRow.Cells[CELL_NAME_HINMEI_CD].Value = null;
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_UNIT_CD].Value = null;
                        targetRow.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = null;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058", "");

                        return returnVal;
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHinmeiCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal bool GetHinmei(Row targetRow, out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                if ((targetRow.Cells[CELL_NAME_HINMEI_CD].Value == null) || (string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString())))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_KOBETSU_HINMEI_TANKA kobetsuHinmeiTanka = this.accessor.GetKobetsuHinmeiTankaDataByCd(this.form.TORIHIKISAKI_CD.Text, this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), this.form.DENPYOU_DATE.Text);
                if (kobetsuHinmeiTanka != null)
                {
                    M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                    if (hinmeis != null && hinmeis.Count() > 0)
                    {
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;
                    }
                    else
                    {
                        returnVal = true;
                    }
                }
                else
                {
                    M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                    if (hinmeis != null && hinmeis.Count() > 0)
                    {
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;

                        // 警告メッセージを表示する。
                        this.msgLogic.MessageBoxShowWarn("個別単価（契約単価）が未登録の品名ＣＤをセットしました。");
                    }
                    else
                    {
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetHinmei", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHinmei", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        internal bool GetHinmeiForPop(Row targetRow)
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            if ((targetRow.Cells[CELL_NAME_HINMEI_CD].Value == null) || (string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString())))
            {
                // 品名コードの入力がない場合
                return returnVal;
            }

            M_KOBETSU_HINMEI kobetsuHinmeis = this.accessor.GetKobetsuHinmeiDataByCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA);
            if (kobetsuHinmeis != null)
            {
                targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
            }
            else
            {
                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                if (hinmeis != null && hinmeis.Count() > 0)
                {
                    targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;
                }
            }
            LogUtility.DebugMethodEnd();
            return returnVal;
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        #endregion

        #region ユーティリティ

        /// <summary>
        /// WINDOWTYPEからデータ取得が必要かどうか判断します
        /// </summary>
        /// <returns>True:データ取得が必要, Flase:データ取得が不必要</returns>
        private bool IsRequireData()
        {
            if (WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType))
            {
                return true;
            }
            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType)
                && this.form.ShukkaNumber != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// コントロールから登録用のEntityを作成する
        /// </summary>
        public void CreateEntity(bool tairyuuKbnFlag)
        {
            LogUtility.DebugMethodStart(tairyuuKbnFlag);

            this.nyuuShukkinDto = new NyuuShukkinDTOClass();
            this.dto.numberReceipt = new S_NUMBER_RECEIPT();
            this.dto.numberReceiptYear = new S_NUMBER_RECEIPT_YEAR();

            if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) ||
                this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.dto.entryEntity = new T_SHUKKA_ENTRY();
                shukkaEntryDataBinder.Entitys = new T_SHUKKA_ENTRY[] { this.dto.entryEntity };
                this.dto.numberDay = new S_NUMBER_DAY();
                this.dto.numberYear = new S_NUMBER_YEAR();
                this.dto.numberReceipt = new S_NUMBER_RECEIPT();
                this.dto.numberReceiptYear = new S_NUMBER_RECEIPT_YEAR();
            }

            /**
             * Entry
             */
            //// Entry(単純系)
            //shukkaEntryDataBinder.CreateEntityForControl();

            // Entry(複雑系)
            // 日連番取得
            S_NUMBER_DAY[] numberDays = null;

            // 201400704 syunrei EV004994_領収書の№について　start
            //DateTime denpyouDate = DateTime.Now;  // 伝票日付
            DateTime denpyouDate = Convert.ToDateTime(this.form.DENPYOU_DATE.Value);
            // 201400704 syunrei EV004994_領収書の№について　end

            short kyotenCd = -1;    // 拠点CD
            short.TryParse(this.headerForm.KYOTEN_CD.Text.ToString(), out kyotenCd);
            if (DateTime.TryParse(this.form.DENPYOU_DATE.Value.ToString(), out denpyouDate)
                && -1 < kyotenCd)
            {
                numberDays = this.accessor.GetNumberDay(denpyouDate.Date, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, kyotenCd);
            }

            // 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
            S_NUMBER_YEAR[] numberYeas = null;
            SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
            if (-1 < kyotenCd)
            {
                numberYeas = this.accessor.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, kyotenCd);
            }

            // S_NUMBER_RECEIPTの更新

            // 領収証番号採番
            if (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
                var numberReceipt = this.accessor.GetNumberReceipt(denpyouDate, kyotenCd);
                if (numberReceipt == null)
                {
                    this.dto.numberReceipt.CURRENT_NUMBER = 1;
                }
                else
                {
                    int numberReceiptCurrentNumber = -1;
                    int.TryParse(Convert.ToString(numberReceipt.CURRENT_NUMBER), out numberReceiptCurrentNumber);
                    this.dto.numberReceipt.CURRENT_NUMBER = numberReceiptCurrentNumber + 1;
                    this.dto.numberReceipt.TIME_STAMP = numberReceipt.TIME_STAMP;
                }

                this.dto.numberReceipt.NUMBERED_DAY = denpyouDate.Date;
                this.dto.numberReceipt.KYOTEN_CD = kyotenCd;
                this.dto.numberReceipt.DELETE_FLG = false;
                var dataBinderNumberReceipt = new DataBinderLogic<S_NUMBER_RECEIPT>(this.dto.numberReceipt);
                dataBinderNumberReceipt.SetSystemProperty(this.dto.numberReceipt, false);
            }

            // S_NUMBER_RECEIPT_YEARの更新

            // 領収証番号採番
            if (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
                var numberReceiptYear = this.accessor.GetNumberReceiptYear(denpyouDate, kyotenCd);
                if (numberReceiptYear == null)
                {
                    this.dto.numberReceiptYear.CURRENT_NUMBER = 1;
                }
                else
                {
                    int numberReceiptYearCurrentNumber = -1;
                    int.TryParse(Convert.ToString(numberReceiptYear.CURRENT_NUMBER), out numberReceiptYearCurrentNumber);
                    this.dto.numberReceiptYear.CURRENT_NUMBER = numberReceiptYearCurrentNumber + 1;
                    this.dto.numberReceiptYear.TIME_STAMP = numberReceiptYear.TIME_STAMP;
                }

                this.dto.numberReceiptYear.NUMBERED_YEAR = (SqlInt16)denpyouDate.Year;
                this.dto.numberReceiptYear.KYOTEN_CD = kyotenCd;
                this.dto.numberReceiptYear.DELETE_FLG = false;
                var dataBinderNumberReceiptYear = new DataBinderLogic<S_NUMBER_RECEIPT_YEAR>(this.dto.numberReceiptYear);
                dataBinderNumberReceiptYear.SetSystemProperty(this.dto.numberReceiptYear, false);
            }

            // モードに依存する処理
            byte[] numberDayTimeStamp = null;
            byte[] numberYearTimeStamp = null;
            //int timeStampCount = 0;
            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // SYSTEM_IDの採番
                    SqlInt64 systemId = this.accessor.CreateSystemIdForShukka();
                    this.dto.entryEntity.SYSTEM_ID = systemId;

                    // 出荷番号の採番
                    this.dto.entryEntity.SHUKKA_NUMBER = this.accessor.CreateSukkaNumber();

                    // 日連番
                    if (numberDays == null || numberDays.Length < 1)
                    {
                        this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                        this.dto.entryEntity.DATE_NUMBER = 1;
                    }
                    else
                    {
                        this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                        this.dto.entryEntity.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                        numberDayTimeStamp = numberDays[0].TIME_STAMP;
                    }
                    // 年連番
                    if (numberYeas == null || numberYeas.Length < 1)
                    {
                        this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                        this.dto.entryEntity.YEAR_NUMBER = 1;
                    }
                    else
                    {
                        this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                        this.dto.entryEntity.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                        numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                    }

                    this.dto.entryEntity.SEQ = 1;
                    this.dto.entryEntity.DELETE_FLG = false;

                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 画面表示時にSYSTEM_IDを取得しているため採番は割愛
                    // 日連番
                    short beforeKotenCd = -1;
                    short.TryParse(beforDto.entryEntity.KYOTEN_CD.ToString(), out beforeKotenCd);
                    if ((beforeKotenCd != kyotenCd
                        || beforDto.entryEntity.KYOTEN_CD != kyotenCd)
                        || !beforDto.entryEntity.DENPYOU_DATE.Equals((SqlDateTime)denpyouDate))
                    {
                        if (numberDays == null || numberDays.Length < 1)
                        {
                            this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = 1;
                        }
                        else
                        {
                            this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                            numberDayTimeStamp = numberDays[0].TIME_STAMP;
                        }
                    }
                    else
                    {
                        this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;   // DB更新時に使用
                        this.dto.entryEntity.DATE_NUMBER = this.beforDto.entryEntity.DATE_NUMBER;
                        numberDayTimeStamp = numberDays[0].TIME_STAMP;
                    }
                    // 年連番
                    SqlInt32 beforNumberedYear = CorpInfoUtility.GetCurrentYear((DateTime)beforDto.entryEntity.DENPYOU_DATE, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                    if ((beforeKotenCd != kyotenCd
                        || beforDto.entryEntity.KYOTEN_CD != kyotenCd)
                        || (numberYeas == null || numberYeas.Length < 1 || beforNumberedYear.Value != numberYeas[0].NUMBERED_YEAR.Value))
                    {
                        if (numberYeas == null || numberYeas.Length < 1)
                        {
                            this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = 1;
                        }
                        else
                        {
                            this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                            numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                        }
                    }
                    else
                    {
                        this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;   // DB更新時に使用
                        this.dto.entryEntity.YEAR_NUMBER = this.beforDto.entryEntity.YEAR_NUMBER;
                        numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                    }

                    this.dto.entryEntity.SHUKKA_NUMBER = SqlInt64.Parse(this.form.ENTRY_NUMBER.Text);
                    this.dto.entryEntity.SYSTEM_ID = this.beforDto.entryEntity.SYSTEM_ID;     // 更新されないはず
                    this.dto.entryEntity.SEQ = this.beforDto.entryEntity.SEQ + 1;
                    this.dto.entryEntity.DELETE_FLG = false;
                    // 更新前伝票は論理削除
                    this.beforDto.entryEntity.DELETE_FLG = true;
                    // 排他制御用
                    this.beforDto.entryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.ENTRY_TIME_STAMP.Text);
                    // 2次
                    // 論理削除対象は既存データなのでsaveZaikoEntityをそのまま使用する
                    //foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in this.beforDto.detailZaikoShukkaDetails)
                    foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in this.beforDto.detailZaikoShukkaDetails.Values)
                    {
                        foreach (T_ZAIKO_SHUKKA_DETAIL entity in entityList)
                        {
                            entity.DELETE_FLG = true;
                            // 自動設定
                            var dataBinderZaikoShukkaDetail = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
                            dataBinderZaikoShukkaDetail.SetSystemProperty(entity, false);
                        }
                    }

                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.dto.entryEntity.DELETE_FLG = true;
                    this.dto.entryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.ENTRY_TIME_STAMP.Text);
                    // 20141118 Houkakou 「更新日、登録日の見直し」　start
                    // 2次
                    // 論理削除対象は既存データなのでsaveZaikoEntityをそのまま使用する
                    //foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in this.beforDto.detailZaikoShukkaDetails)
                    foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in this.beforDto.detailZaikoShukkaDetails.Values)
                    {
                        foreach (T_ZAIKO_SHUKKA_DETAIL entity in entityList)
                        {
                            entity.DELETE_FLG = true;
                            // 自動設定
                            var dataBinderZaikoShukkaDetail = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
                            dataBinderZaikoShukkaDetail.SetSystemProperty(entity, false);
                        }
                    }
                    // 20141118 Houkakou 「更新日、登録日の見直し」　end

                    break;
                default:
                    break;
            }

            // 滞留区分
            // 削除モード時は書き換えない
            if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType)
            {
                this.dto.entryEntity.TAIRYUU_KBN = tairyuuKbnFlag;
            }

            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.dto.entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            }

            if (!string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
            {
                this.dto.entryEntity.KAKUTEI_KBN = SqlInt16.Parse(this.form.KAKUTEI_KBN.Text);
            }
            else
            {
                this.dto.entryEntity.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI;
            }

            if (this.form.DENPYOU_DATE.Value != null)
            {
                this.dto.entryEntity.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
            }

            if (this.form.URIAGE_DATE.Value != null)
            {
                this.dto.entryEntity.URIAGE_DATE = ((DateTime)this.form.URIAGE_DATE.Value).Date;
            }

            if (this.form.SHIHARAI_DATE.Value != null)
            {
                this.dto.entryEntity.SHIHARAI_DATE = ((DateTime)this.form.SHIHARAI_DATE.Value).Date;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.dto.entryEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.TORIHIKISAKI_NAME = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.GYOUSHA_NAME = this.form.GYOUSHA_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.dto.entryEntity.GENBA_CD = this.form.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.GENBA_NAME = this.form.GENBA_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.NIZUMI_GYOUSHA_CD = this.form.NIZUMI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text))
            {
                this.dto.entryEntity.NIZUMI_GYOUSHA_NAME = this.form.NIZUMI_GYOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                this.dto.entryEntity.NIZUMI_GENBA_CD = this.form.NIZUMI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))
            {
                this.dto.entryEntity.NIZUMI_GENBA_NAME = this.form.NIZUMI_GENBA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_NAME.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME = this.form.EIGYOU_TANTOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
            {
                this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD = this.form.NYUURYOKU_TANTOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_NAME.Text))
            {
                this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                this.dto.entryEntity.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
            {
                this.dto.entryEntity.SHASHU_CD = this.form.SHASHU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_NAME.Text))
            {
                this.dto.entryEntity.SHASHU_NAME = this.form.SHASHU_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
            {
                this.dto.entryEntity.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                this.dto.entryEntity.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_NAME.Text))
            {
                this.dto.entryEntity.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NINZUU_CNT.Text))
            {
                this.dto.entryEntity.NINZUU_CNT = SqlInt16.Parse(this.form.NINZUU_CNT.Text);
            }
            if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
            {
                this.dto.entryEntity.KEITAI_KBN_CD = SqlInt16.Parse(this.form.KEITAI_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DAIKAN_KBN.Text))
            {
                this.dto.entryEntity.DAIKAN_KBN = SqlInt16.Parse(this.form.DAIKAN_KBN.Text);
            }
            if (!string.IsNullOrEmpty(this.form.MANIFEST_SHURUI_CD.Text))
            {
                this.dto.entryEntity.MANIFEST_SHURUI_CD = SqlInt16.Parse(this.form.MANIFEST_SHURUI_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.MANIFEST_TEHAI_CD.Text))
            {
                this.dto.entryEntity.MANIFEST_TEHAI_CD = SqlInt16.Parse(this.form.MANIFEST_TEHAI_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DENPYOU_BIKOU.Text))
            {
                this.dto.entryEntity.DENPYOU_BIKOU = this.form.DENPYOU_BIKOU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TAIRYUU_BIKOU.Text))
            {
                this.dto.entryEntity.TAIRYUU_BIKOU = this.form.TAIRYUU_BIKOU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
            {
                this.dto.entryEntity.UKETSUKE_NUMBER = SqlInt64.Parse(this.form.UKETSUKE_NUMBER.Text);
            }
            if (!string.IsNullOrEmpty(this.form.KEIRYOU_NUMBER.Text))
            {
                this.dto.entryEntity.KEIRYOU_NUMBER = SqlInt64.Parse(this.form.KEIRYOU_NUMBER.Text);
            }
            if (!this.dto.numberReceipt.CURRENT_NUMBER.IsNull)
            {
                this.dto.entryEntity.RECEIPT_NUMBER = this.dto.numberReceipt.CURRENT_NUMBER;
            }
            else if (!string.IsNullOrEmpty(this.form.RECEIPT_NUMBER_DAY.Text))
            {
                // 修正モードで表示、かつ領収書を発行しない場合
                this.dto.entryEntity.RECEIPT_NUMBER = SqlInt32.Parse(this.form.RECEIPT_NUMBER_DAY.Text);
            }
            if (!this.dto.numberReceiptYear.CURRENT_NUMBER.IsNull)
            {
                this.dto.entryEntity.RECEIPT_NUMBER_YEAR = this.dto.numberReceiptYear.CURRENT_NUMBER;
            }
            else if (!string.IsNullOrEmpty(this.form.RECEIPT_NUMBER_YEAR.Text))
            {
                // 修正モードで表示、かつ領収書を発行しない場合
                this.dto.entryEntity.RECEIPT_NUMBER_YEAR = SqlInt32.Parse(this.form.RECEIPT_NUMBER_YEAR.Text);
            }

            if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text))
            {
                this.dto.entryEntity.NET_TOTAL = SqlDecimal.Parse(Convert.ToDouble(this.form.NET_TOTAL.Text).ToString());
            }

            // 新出荷入力で登録できる項目
            // こちらの画面には存在しないので、NULLで更新しないように更新前DTOからセットしておく
            // 総重量
            //this.dto.entryEntity.STACK_JYUURYOU = SqlDecimal.Parse(Convert.ToDouble(this.beforDto.entryEntity.STACK_JYUURYOU).ToString());
            this.dto.entryEntity.STACK_JYUURYOU = this.beforDto.entryEntity.STACK_JYUURYOU;
            // 総重量時間
            //this.dto.entryEntity.STACK_KEIRYOU_TIME = Convert.ToDateTime(this.beforDto.entryEntity.STACK_KEIRYOU_TIME);
            this.dto.entryEntity.STACK_KEIRYOU_TIME = this.beforDto.entryEntity.STACK_KEIRYOU_TIME;
            // 空車重量
            //this.dto.entryEntity.EMPTY_JYUURYOU = SqlDecimal.Parse(Convert.ToDouble(this.beforDto.entryEntity.EMPTY_JYUURYOU).ToString());
            this.dto.entryEntity.EMPTY_JYUURYOU = this.beforDto.entryEntity.EMPTY_JYUURYOU;
            // 空車重量時間
            //this.dto.entryEntity.EMPTY_KEIRYOU_TIME = Convert.ToDateTime(this.beforDto.entryEntity.EMPTY_KEIRYOU_TIME.ToString());
            this.dto.entryEntity.EMPTY_KEIRYOU_TIME = this.beforDto.entryEntity.EMPTY_KEIRYOU_TIME;
            // 合計金額
            //this.dto.entryEntity.KINGAKU_TOTAL = SqlDecimal.Parse(Convert.ToDouble(this.beforDto.entryEntity.KINGAKU_TOTAL).ToString());
            this.dto.entryEntity.KINGAKU_TOTAL = this.beforDto.entryEntity.KINGAKU_TOTAL;

            // 確定フラグのデフォ値を設定
            if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN == SalesPaymentConstans.SHUKKA_KAKUTEI_USE_KBN_NO)
            {
                // EntryはClearメソッドでデフォ値を設定しているためここでは設定しない

                // Detail
                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    row.Cells[CELL_NAME_KAKUTEI_KBN].Value = true;
                }
            }

            /**
             * 確定フラグの制御
             *
             * ■システム設定の確定条件:伝票単位の場合
             * 　Detailの確定フラグ：Entryの確定フラグをセット
             * 　Detailの売上/支払日付：Entryの売上 or 支払日付をセット
             *
             * ■システム設定の確定条件：明細単位の場合
             * 　Entryの確定フラグ：Detailの確定フラグに1つでも未確定があったら未確定にする
             * 　　　　　　　　　　 上記以外は確定でセット
             * 　Entryの売上日付：Detailの伝票区分：売上の中で、日付が一番古い日付
             * 　Entryの支払日付：Detailの伝票区分：支払の中で日付が一番古い日付
             */
            if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
            {
                // 伝票単位
                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                    {
                        continue;
                    }

                    if (!this.dto.entryEntity.KAKUTEI_KBN.IsNull)
                    {
                        row.Cells[CELL_NAME_KAKUTEI_KBN].Value = (bool)(this.dto.entryEntity.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI);
                    }
                    else
                    {
                        row.Cells[CELL_NAME_KAKUTEI_KBN].Value = false;
                    }

                    if (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                    {
                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())
                            && !this.dto.entryEntity.URIAGE_DATE.IsNull)
                        {
                            row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = (DateTime)this.dto.entryEntity.URIAGE_DATE;
                            // 売上消費税率
                            // 直接指定されていればそちらを参照する
                            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
                            {
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = this.ToDecimalForUriageShouhizeiRate();
                            }
                            else
                            {
                                var shouhizeiRate = this.accessor.GetShouhizeiRate(((DateTime)this.dto.entryEntity.URIAGE_DATE).Date);
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = shouhizeiRate.SHOUHIZEI_RATE;
                            }
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())
                            && !this.dto.entryEntity.SHIHARAI_DATE.IsNull)
                        {
                            row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = (DateTime)this.dto.entryEntity.SHIHARAI_DATE;
                            // 支払消費税率
                            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
                            {
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = this.ToDecimalForShiharaiShouhizeiRate();
                            }
                            else
                            {
                                var shouhizeiRate = this.accessor.GetShouhizeiRate(((DateTime)this.dto.entryEntity.URIAGE_DATE).Date);
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = shouhizeiRate.SHOUHIZEI_RATE;
                            }
                        }
                    }
                }
            }
            else
            {
                // 明細単位
                short tempKakuteiKbn = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI;
                SqlDateTime tempUriageDate = SqlDateTime.Null;
                SqlDateTime tempShiharaiDate = SqlDateTime.Null;
                decimal tempUriageShouhizeiRate = 0;
                decimal tempShiharaiShouhizeiRate = 0;
                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                    {
                        continue;
                    }

                    if (row.Cells[CELL_NAME_KAKUTEI_KBN].Value == null
                        || !(bool)row.Cells[CELL_NAME_KAKUTEI_KBN].Value)
                    {
                        tempKakuteiKbn = SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI;
                    }

                    DateTime tempUrShDate;
                    if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                        && DateTime.TryParse(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString(), out tempUrShDate)
                        && (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())))
                    {
                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            if (tempUriageDate.IsNull)
                            {
                                tempUriageDate = tempUrShDate.Date;
                            }
                            // 一番最後の日付かチェック
                            else if (tempUriageDate < tempUrShDate.Date)
                            {
                                tempUriageDate = tempUrShDate.Date;
                            }
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            if (tempShiharaiDate.IsNull)
                            {
                                tempShiharaiDate = tempUrShDate.Date;
                            }
                            // 一番最後の日付かチェック
                            else if (tempShiharaiDate < tempUrShDate.Date)
                            {
                                tempShiharaiDate = tempUrShDate.Date;
                            }
                        }
                    }
                }

                this.dto.entryEntity.KAKUTEI_KBN = tempKakuteiKbn;
                this.dto.entryEntity.URIAGE_DATE = tempUriageDate;
                // 念のため画面にもセット
                if (!tempUriageDate.IsNull)
                {
                    this.form.URIAGE_DATE.Value = tempUriageDate;
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempUriageDate).Date);
                    if (shouhizeiEntity != null
                        && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                    {
                        tempUriageShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
                else
                {
                    this.form.URIAGE_DATE.Value = string.Empty;
                }

                this.dto.entryEntity.SHIHARAI_DATE = tempShiharaiDate;
                // 念のため画面にもセット
                if (!tempShiharaiDate.IsNull)
                {
                    this.form.SHIHARAI_DATE.Value = tempShiharaiDate;
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempShiharaiDate).Date);
                    if (shouhizeiEntity != null
                        && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                    {
                        tempShiharaiShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
                else
                {
                    this.form.SHIHARAI_DATE.Value = string.Empty;
                }

                // 消費税率
                this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = tempUriageShouhizeiRate;
                this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = tempShiharaiShouhizeiRate;
                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = tempUriageShouhizeiRate.ToString();
                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = tempShiharaiShouhizeiRate.ToString();
            }

            // 売上消費税
            this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = 0;

            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
            {
                this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = this.ToDecimalForUriageShouhizeiRate();
            }
            else
            {
                DateTime uriageDate;
                if (this.form.URIAGE_DATE.Value != null
                    && DateTime.TryParse(this.form.URIAGE_DATE.Value.ToString(), out uriageDate))
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(uriageDate.Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
            }

            // 税金系はURIAGE_KINGAKU_TOTALを使うため、一番最後に実行する

            // 支払消費税
            this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = 0;

            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
            {
                this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = this.ToDecimalForShiharaiShouhizeiRate();
            }
            else
            {

                DateTime shiharaiDate;
                if (this.form.SHIHARAI_DATE.Value != null
                    && DateTime.TryParse(this.form.SHIHARAI_DATE.Value.ToString(), out shiharaiDate))
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(shiharaiDate.Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
            }

            /**
             * 伝票発行画面にて取得したデータ
             */
            // 売上税計算区分CD
            int seikyuZeikeisanKbn = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn, out seikyuZeikeisanKbn))
            {
                this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD = (SqlInt16)seikyuZeikeisanKbn;
            }
            // 売上税区分CD
            int uriageZeiKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn, out uriageZeiKbnCd))
                this.dto.entryEntity.URIAGE_ZEI_KBN_CD = (SqlInt16)uriageZeiKbnCd;
            // 売上取引区分CD
            int uriageTorihikiKbnCd = 0;
            if (this.form.denpyouHakouPopUpDTO != null && !string.IsNullOrEmpty(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn))
            {
                if (int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn, out uriageTorihikiKbnCd))
                {
                    this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD = (SqlInt16)uriageTorihikiKbnCd;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.form.txtUri.Text))
                {
                    if (SalesPaymentConstans.STR_TORIHIKI_KBN_1.Equals(this.form.txtUri.Text))
                    {
                        this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD = SalesPaymentConstans.TORIHIKI_KBN_CD_1;
                    }
                    else if (SalesPaymentConstans.STR_TORIHIKI_KBN_2.Equals(this.form.txtUri.Text))
                    {
                        this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD = SalesPaymentConstans.TORIHIKI_KBN_CD_2;
                    }
                }
            }
            // 支払税計算区分CD
            int shiharaiZeiKeisanKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn, out shiharaiZeiKeisanKbnCd))
            {
                this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = (SqlInt16)shiharaiZeiKeisanKbnCd;
            }
            // 支払税区分CD
            int shiharaiZeiKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn, out shiharaiZeiKbnCd))
            {
                this.dto.entryEntity.SHIHARAI_ZEI_KBN_CD = (SqlInt16)shiharaiZeiKbnCd;
            }
            // 支払取引区分CD
            int ShiharaiTorihikiKbnCd = 0;
            if (this.form.denpyouHakouPopUpDTO != null && !string.IsNullOrEmpty(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn))
            {
                if (int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn, out ShiharaiTorihikiKbnCd))
                {
                    this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD = (SqlInt16)ShiharaiTorihikiKbnCd;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.form.txtShi.Text))
                {
                    if (SalesPaymentConstans.STR_TORIHIKI_KBN_1.Equals(this.form.txtShi.Text))
                    {
                        this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD = SalesPaymentConstans.TORIHIKI_KBN_CD_1;
                    }
                    else if (SalesPaymentConstans.STR_TORIHIKI_KBN_2.Equals(this.form.txtShi.Text))
                    {
                        this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD = SalesPaymentConstans.TORIHIKI_KBN_CD_2;
                    }

                }
            }

            var dataBinderShukkaEntry = new DataBinderLogic<T_SHUKKA_ENTRY>(this.dto.entryEntity);
            dataBinderShukkaEntry.SetSystemProperty(this.dto.entryEntity, false);

            // 修正、削除モードの場合、Create情報は前の伝票のデータを引き継ぐ
            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.dto.entryEntity.CREATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                // ----20141118 Houkakou 「更新日、登録日の見直し」　start
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    // 20141118 Houkakou 「更新日、登録日の見直し」　end
                    this.dto.entryEntity.CREATE_USER = this.beforDto.entryEntity.CREATE_USER;
                    this.dto.entryEntity.CREATE_DATE = this.beforDto.entryEntity.CREATE_DATE;
                    this.dto.entryEntity.CREATE_PC = this.beforDto.entryEntity.CREATE_PC;
                    break;

                default:
                    break;
            }

            #region 検収系(計算しないで良い項目)
            /**
             * 検収系(計算しないで良い項目)
             */

            // 要検収
            this.dto.entryEntity.KENSHU_MUST_KBN = this.form.KENSHU_MUST_KBN.Checked;

            // 検収日付
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_DATE.IsNull)
            {
                this.dto.entryEntity.KENSHU_DATE = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_DATE;
            }

            // 検収売上日付
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_DATE.IsNull)
            {
                this.dto.entryEntity.KENSHU_URIAGE_DATE = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_DATE;
            }

            // 検収時正味合計
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_NET_TOTAL.IsNull)
            {
                this.dto.entryEntity.KENSHU_NET_TOTAL = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_NET_TOTAL;
            }

            // 検収売上消費税率
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE.IsNull)
            {
                this.dto.entryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE;
            }

            // 検収売上金額合計
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL.IsNull)
            {
                this.dto.entryEntity.KENSHU_URIAGE_AMOUNT_TOTAL = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL;
            }
            else
            {
                this.dto.entryEntity.KENSHU_URIAGE_AMOUNT_TOTAL = 0;
            }

            // 検収支払日付
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE.IsNull)
            {
                this.dto.entryEntity.KENSHU_SHIHARAI_DATE = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE;
            }

            // 検収支払消費税率
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE.IsNull)
            {
                this.dto.entryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE;
            }

            // 検収支払金額合計
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL.IsNull)
            {
                this.dto.entryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL;
            }
            else
            {
                this.dto.entryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL = 0;
            }
            #endregion

            // 最終更新者
            this.dto.entryEntity.UPDATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;

            // 更新前伝票
            // 20141118 Houkakou 「更新日、登録日の見直し」　start
            //var dataBinderBeforShukkaEntry = new DataBinderLogic<T_SHUKKA_ENTRY>(this.beforDto.entryEntity);
            //dataBinderBeforShukkaEntry.SetSystemProperty(this.beforDto.entryEntity, true);
            // 20141118 Houkakou 「更新日、登録日の見直し」　end

            /**
             * Detail
             */
            // FWのDataBinderLogic.CreateEntityForDataTableでは対応できないため自前で値を設定する
            List<T_SHUKKA_DETAIL> shukkaDetailEntitys = new List<T_SHUKKA_DETAIL>();

            SqlInt64 detailSysId = -1;
            decimal HimeiUrKingakuTotal = 0;
            decimal HimeiShKingakuTotal = 0;
            decimal HinmeiUrTaxSotoTotal = 0;
            decimal HinmeiShTaxSotoTotal = 0;
            decimal HinmeiUrTaxUchiTotal = 0;
            decimal HinmeiShTaxUchiTotal = 0;
            decimal UrTaxSotoTotal = 0;
            decimal ShTaxSotoTotal = 0;
            decimal UrTaxUchiTotal = 0;
            decimal ShTaxUchiTotal = 0;
            int TaxHasuuCdSeikyuu = -1;
            int TaxHasuuCdShiharai = -1;

            // 取引先CDが無い場合、取引先請求 or 取引先支払の税区分CDがなく、端数計算処理で落ちてしまうため、
            // ここで税計算区分CDを設定
            if (this.dto.torihikisakiSeikyuuEntity != null
                && !this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD.IsNull)
            {
                TaxHasuuCdSeikyuu = this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD.Value;
            }

            if (this.dto.torihikisakiShiharaiEntity != null
                && !this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD.IsNull)
            {
                TaxHasuuCdShiharai = this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD.Value;
            }

            foreach (Row dr in this.form.gcMultiRow1.Rows)
            {
                if (dr.IsNewRow || string.IsNullOrEmpty((string)dr.Cells["ROW_NO"].Value.ToString()))
                {
                    continue;
                }

                T_SHUKKA_DETAIL temp = new T_SHUKKA_DETAIL();

                // 20150112 在庫明細設定部分を明細設定の最後に移動(修正後のG051と同様処理) Start
                // モードに依存する処理
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 新規の場合は、既にEntryで採番しているので、それに+1する
                        detailSysId = this.accessor.CreateSystemIdForShukka();
                        temp.DETAIL_SYSTEM_ID = detailSysId;
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // DETAIL_SYSTEM_IDの採番
                        if (dr.Cells["DETAIL_SYSTEM_ID"].Value == null
                            || string.IsNullOrEmpty(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                        {
                            // 修正モードでT_SHUKKA_DETAILが初めて登録されるパターンもあるはずなので、
                            // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                            detailSysId = this.accessor.CreateSystemIdForShukka();
                        }
                        else
                        {
                            // 既に登録されていればそのまま使う
                            detailSysId = SqlInt64.Parse(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                        }
                        temp.DETAIL_SYSTEM_ID = detailSysId;
                        break;
                    default:
                        break;
                }
                // 20150112 在庫明細設定部分を明細設定の最後に移動 End

                temp.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID.Value;
                temp.SEQ = this.dto.entryEntity.SEQ;
                temp.SHUKKA_NUMBER = this.dto.entryEntity.SHUKKA_NUMBER;
                if (!string.IsNullOrEmpty(dr.Cells["ROW_NO"].Value.ToString()))
                {
                    temp.ROW_NO = SqlInt16.Parse(dr.Cells["ROW_NO"].Value.ToString());
                }

                if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN == SalesPaymentConstans.SHUKKA_KAKUTEI_USE_KBN_YES)
                {
                    if (dr.Cells["KAKUTEI_KBN"].Value != null && !string.IsNullOrEmpty(dr.Cells["KAKUTEI_KBN"].Value.ToString())
                        && Convert.ToBoolean(dr.Cells["KAKUTEI_KBN"].Value.ToString()) == true)
                    {
                        temp.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI;
                    }
                    else
                    {
                        temp.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI;
                    }
                }
                else
                {
                    temp.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI;
                }

                if (dr.Cells["URIAGESHIHARAI_DATE"].Value != null
                    && !string.IsNullOrEmpty(dr.Cells["URIAGESHIHARAI_DATE"].Value.ToString()))
                {
                    temp.URIAGESHIHARAI_DATE = (DateTime)dr.Cells["URIAGESHIHARAI_DATE"].Value;
                }

                decimal tempStackJyuryou = 0;
                if (dr.Cells["STACK_JYUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["STACK_JYUURYOU"].Value.ToString(), out tempStackJyuryou))
                {
                    temp.STACK_JYUURYOU = tempStackJyuryou;
                }

                decimal tempEmptyJyuuryou = 0;
                if (dr.Cells["EMPTY_JYUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["EMPTY_JYUURYOU"].Value.ToString(), out tempEmptyJyuuryou))
                {
                    temp.EMPTY_JYUURYOU = tempEmptyJyuuryou;
                }

                decimal tempWarifuriJyuuryou = 0;
                if (dr.Cells["WARIFURI_JYUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["WARIFURI_JYUURYOU"].Value.ToString(), out tempWarifuriJyuuryou))
                {
                    temp.WARIFURI_JYUURYOU = tempWarifuriJyuuryou;
                }

                decimal tempWarifuriPercent = 0;
                if (dr.Cells["WARIFURI_PERCENT"].Value != null
                    && decimal.TryParse(dr.Cells["WARIFURI_PERCENT"].Value.ToString(), out tempWarifuriPercent))
                {
                    temp.WARIFURI_PERCENT = tempWarifuriPercent;
                }

                decimal tempChouseiJyuuryou = 0;
                if (dr.Cells["CHOUSEI_JYUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["CHOUSEI_JYUURYOU"].Value.ToString(), out tempChouseiJyuuryou))
                {
                    temp.CHOUSEI_JYUURYOU = tempChouseiJyuuryou;
                }

                decimal tempChouseiPercent = 0;
                if (dr.Cells["CHOUSEI_PERCENT"].Value != null
                    && decimal.TryParse(dr.Cells["CHOUSEI_PERCENT"].Value.ToString(), out tempChouseiPercent))
                {
                    temp.CHOUSEI_PERCENT = tempChouseiPercent;
                }
                if (dr.Cells["YOUKI_CD"].Value != null)
                {
                    temp.YOUKI_CD = dr.Cells["YOUKI_CD"].Value.ToString();
                }

                decimal tempYoukiSuuryou = 0;
                if (dr.Cells["YOUKI_SUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["YOUKI_SUURYOU"].Value.ToString(), out tempYoukiSuuryou))
                {
                    temp.YOUKI_SUURYOU = tempYoukiSuuryou;
                }

                decimal tempYoukiJyuuryou = 0;
                if (dr.Cells["YOUKI_JYUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["YOUKI_JYUURYOU"].Value.ToString(), out tempYoukiJyuuryou))
                {
                    temp.YOUKI_JYUURYOU = tempYoukiJyuuryou;
                }
                if (dr.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrEmpty(dr.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                {
                    temp.DENPYOU_KBN_CD = SqlInt16.Parse(dr.Cells["DENPYOU_KBN_CD"].Value.ToString());
                }
                if (dr.Cells["HINMEI_CD"].Value != null)
                {
                    temp.HINMEI_CD = dr.Cells["HINMEI_CD"].Value.ToString();
                }
                if (dr.Cells["HINMEI_NAME"].Value != null)
                {
                    temp.HINMEI_NAME = dr.Cells["HINMEI_NAME"].Value.ToString();
                }

                decimal tempNetJyuuryou = 0;
                if (dr.Cells["NET_JYUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["NET_JYUURYOU"].Value.ToString(), out tempNetJyuuryou))
                {
                    temp.NET_JYUURYOU = tempNetJyuuryou;
                }

                decimal tempSuuryou = 0;
                if (dr.Cells["SUURYOU"].Value != null
                    && decimal.TryParse(dr.Cells["SUURYOU"].Value.ToString(), out tempSuuryou))
                {
                    temp.SUURYOU = tempSuuryou;
                }
                if (dr.Cells["UNIT_CD"].Value != null && !string.IsNullOrEmpty(dr.Cells["UNIT_CD"].Value.ToString()))
                {
                    temp.UNIT_CD = SqlInt16.Parse(dr.Cells["UNIT_CD"].Value.ToString());
                }
                decimal tanka = 0;
                if (dr.Cells["TANKA"].Value != null)
                {
                    if (decimal.TryParse(dr.Cells["TANKA"].Value.ToString(), out tanka))
                    {
                        temp.TANKA = tanka;
                    }
                    else
                    {
                        temp.TANKA = SqlDecimal.Null;
                    }
                }
                else
                {
                    temp.TANKA = SqlDecimal.Null;
                }

                // 明細で選択された品名の情報を取得
                short hinmeiZeiKbnCd = 0;
                if (dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value != null
                    && short.TryParse(dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value.ToString(), out hinmeiZeiKbnCd))
                {
                    temp.HINMEI_ZEI_KBN_CD = hinmeiZeiKbnCd;
                }

                if (temp.HINMEI_ZEI_KBN_CD.IsNull || temp.HINMEI_ZEI_KBN_CD == 0)
                {
                    if (dr.Cells["KINGAKU"].Value != null && !string.IsNullOrEmpty(dr.Cells["KINGAKU"].Value.ToString()))
                    {
                        decimal kingaku = 0;
                        decimal.TryParse(dr.Cells["KINGAKU"].Value.ToString(), out kingaku);
                        temp.KINGAKU = kingaku;
                    }
                }
                else
                {
                    temp.KINGAKU = 0;
                }

                // 品名別税区分CD、品名別金額
                if (temp.HINMEI_ZEI_KBN_CD != 0 && !temp.HINMEI_ZEI_KBN_CD.IsNull)
                {
                    if (dr.Cells["KINGAKU"].Value != null && !string.IsNullOrEmpty(dr.Cells["KINGAKU"].Value.ToString()))
                    {
                        decimal hinmeiKingaku = 0;
                        decimal.TryParse(dr.Cells["KINGAKU"].Value.ToString(), out hinmeiKingaku);
                        temp.HINMEI_KINGAKU = hinmeiKingaku;

                        if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == temp.DENPYOU_KBN_CD)
                        {
                            HimeiUrKingakuTotal += hinmeiKingaku;
                        }
                        else if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == temp.DENPYOU_KBN_CD)
                        {
                            HimeiShKingakuTotal += hinmeiKingaku;
                        }
                    }
                }
                else
                {
                    temp.HINMEI_KINGAKU = 0;
                }

                // 明細毎消費税合計を計算
                // この時点で明細.品名のデータは検索済みなので、品名データ取得処理はしない

                decimal meisaiKingaku = 0;
                decimal.TryParse(Convert.ToString(dr.Cells["KINGAKU"].Value), out meisaiKingaku);

                temp.TAX_SOTO = 0;          // 消費税外税初期値
                temp.TAX_UCHI = 0;          // 消費税内税初期値
                temp.HINMEI_TAX_SOTO = 0;   // 品名別消費税外税初期値
                temp.HINMEI_TAX_UCHI = 0;   // 品名別消費税内税初期値

                decimal detailShouhizeiRate = 0;
                if (!temp.URIAGESHIHARAI_DATE.IsNull)
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)temp.URIAGESHIHARAI_DATE).Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        detailShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }

                // もし消費税率が設定されていればそちらを優先して使う
                decimal tempShouhizeiRate = 0;
                if (dr.Cells[CELL_NAME_SHOUHIZEI_RATE].Value != null
                    && !string.IsNullOrEmpty(dr.Cells[CELL_NAME_SHOUHIZEI_RATE].Value.ToString())
                    && decimal.TryParse(dr.Cells[CELL_NAME_SHOUHIZEI_RATE].Value.ToString(), out tempShouhizeiRate))
                {
                    detailShouhizeiRate = tempShouhizeiRate;
                }

                if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == temp.DENPYOU_KBN_CD)
                {
                    if (!temp.HINMEI_ZEI_KBN_CD.IsNull
                        && temp.HINMEI_ZEI_KBN_CD != 0)
                    {
                        // TODO: 明細毎消費税合計は品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (temp.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                temp.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu);
                                HinmeiUrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                temp.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.HINMEI_TAX_UCHI, TaxHasuuCdSeikyuu);
                                HinmeiUrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn)
                        {
                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                UrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税外
                                temp.TAX_SOTO
                                    = CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu);

                                break;

                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                UrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税内
                                temp.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.TAX_UCHI, TaxHasuuCdSeikyuu);
                                break;

                            default:
                                break;
                        }
                    }
                }
                else if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == temp.DENPYOU_KBN_CD)
                {
                    if (!temp.HINMEI_ZEI_KBN_CD.IsNull
                        && temp.HINMEI_ZEI_KBN_CD != 0)
                    {
                        switch (temp.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                temp.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        TaxHasuuCdShiharai);
                                HinmeiShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                temp.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.HINMEI_TAX_UCHI, TaxHasuuCdShiharai);
                                HinmeiShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn)
                        {
                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                ShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税外
                                temp.TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        TaxHasuuCdShiharai);

                                break;

                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                ShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税内
                                temp.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.TAX_UCHI, TaxHasuuCdShiharai);
                                break;

                            default:
                                break;
                        }
                    }
                }

                if (dr.Cells["MEISAI_BIKOU"].Value != null)
                {
                    temp.MEISAI_BIKOU = dr.Cells["MEISAI_BIKOU"].Value.ToString();
                }
                // TODO: 2次
                temp.NISUGATA_SUURYOU = 0;
                temp.NISUGATA_UNIT_CD = 0;
                decimal nisugataSuuryou;
                Int16 nisugataUnitCd;
                //20150921 hoanghm #12518 start
                //if (decimal.TryParse(Convert.ToString(dr.Cells["NISUGATA_SUURYOU"].Value), out nisugataSuuryou))
                //{
                //    temp.NISUGATA_SUURYOU = SqlDecimal.Parse(dr.Cells["NISUGATA_SUURYOU"].Value.ToString());
                //}
                //if (Int16.TryParse(Convert.ToString(dr.Cells["NISUGATA_UNIT_CD"].Value), out nisugataUnitCd))
                //{
                //    temp.NISUGATA_UNIT_CD = SqlInt16.Parse(dr.Cells["NISUGATA_UNIT_CD"].Value.ToString());
                //}
                if (decimal.TryParse(Convert.ToString(dr.Cells["NISUGATA_SUURYOU"].Value), out nisugataSuuryou))
                {
                    temp.NISUGATA_SUURYOU = nisugataSuuryou;
                }
                else
                {
                    temp.NISUGATA_SUURYOU = SqlDecimal.Null;
                }
                if (Int16.TryParse(Convert.ToString(dr.Cells["NISUGATA_UNIT_CD"].Value), out nisugataUnitCd))
                {
                    temp.NISUGATA_UNIT_CD = nisugataUnitCd;
                }
                else
                {
                    temp.NISUGATA_UNIT_CD = SqlInt16.Null;
                }
                //20150921 hoanghm #12518 end

                // 在庫管理の場合のみ設定する
                if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
                {
                    // 20150412 在庫明細部分をここに移動(修正後のG051と同様処理) Start
                    // Dictionary関連修正
                    // Dictionaryから行に該当するリストを抽出
                    List<T_ZAIKO_SHUKKA_DETAIL> zaikoShukkaDetailsList = this.dto.rowZaikoShukkaDetails[dr];
                    // No.4578-->
                    // 20150409 go 在庫品名振分処理追加 Start
                    List<T_ZAIKO_HINMEI_HURIWAKE> zaikoHinmeiHuriwakesList = this.dto.rowZaikoHinmeiHuriwakes[dr];
                    // 20150409 go 在庫品名振分処理追加 End
                    // No.4578<--
                    // モードに依存する処理
                    switch (this.form.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            // 2次
                            // 在庫明細追加用設定(更新の場合、旧データの論理削除はEntry単位の処理で行っている)
                            foreach (T_ZAIKO_SHUKKA_DETAIL entity in zaikoShukkaDetailsList)
                            {
                                //systemidとseqは入力テーブルと同じ内容をセットする
                                entity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                                entity.SEQ = this.dto.entryEntity.SEQ;

                                //明細システムIDセット
                                entity.DETAIL_SYSTEM_ID = detailSysId;

                                //伝種区分セット
                                entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

                                var dataBinderZaikoShukkaDetail = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
                                dataBinderZaikoShukkaDetail.SetSystemProperty(entity, false);
                            }

                            // No.4578-->
                            // 20150409 go 在庫品名振分処理追加 Start
                            // 在庫品名振分追加用設定
                            foreach (T_ZAIKO_HINMEI_HURIWAKE entity in zaikoHinmeiHuriwakesList)
                            {
                                // 20150421 在庫量と在庫金額再計算判定修正(有価在庫不具合一覧107関連) Start
                                // 在庫量と在庫金額再計算
                                //if (entity.SYSTEM_ID.IsNull || entity.SYSTEM_ID.Value == null ||
                                //    entity.SEQ.IsNull || entity.SEQ.Value == null ||
                                //    entity.DETAIL_SYSTEM_ID.IsNull || entity.DETAIL_SYSTEM_ID.Value == null)
                                //{
                                entity.ZAIKO_RYOU = Convert.ToDecimal(tempNetJyuuryou) * Convert.ToDecimal(entity.ZAIKO_HIRITSU.Value) / 100;
                                entity.ZAIKO_KINGAKU = entity.ZAIKO_TANKA * entity.ZAIKO_RYOU;
                                //}
                                // 20150421 在庫量と在庫金額再計算判定修正(有価在庫不具合一覧107関連) End

                                //systemidとseqは入力テーブルと同じ内容をセットする
                                entity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                                entity.SEQ = this.dto.entryEntity.SEQ;

                                //明細システムIDセット
                                entity.DETAIL_SYSTEM_ID = detailSysId;

                                //伝種区分セット
                                entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

                                var dataBinderZaikoHinmeiHuriwake = new DataBinderLogic<T_ZAIKO_HINMEI_HURIWAKE>(entity);
                                dataBinderZaikoHinmeiHuriwake.SetSystemProperty(entity, false);
                            }
                            // 20150409 go 在庫品名振分処理追加 End
                            // No.4578<--
                            break;
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            // 2次
                            // 在庫明細追加用設定(旧データの論理削除はEntry単位の処理で行っている)
                            // Dictionary関連修正
                            foreach (T_ZAIKO_SHUKKA_DETAIL entity in zaikoShukkaDetailsList)
                            {
                                //systemidとseqは入力テーブルと同じ内容をセットする
                                entity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                                entity.SEQ = this.dto.entryEntity.SEQ;

                                //明細システムIDセット
                                entity.DETAIL_SYSTEM_ID = detailSysId;

                                //伝種区分セット
                                entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

                                // 20141118 Houkakou 「更新日、登録日の見直し」 Start
                                //var dataBinderZaikoShukkaDetail = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
                                //dataBinderZaikoShukkaDetail.SetSystemProperty(entity, false);
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                entity.UPDATE_PC = SystemInformation.ComputerName;
                                entity.UPDATE_USER = SystemProperty.UserName;
                                // 20141118 Houkakou 「更新日、登録日の見直し」 End
                            }

                            // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する Start
                            // 在庫品名振分追加用設定
                            foreach (T_ZAIKO_HINMEI_HURIWAKE entity in zaikoHinmeiHuriwakesList)
                            {
                                //systemidとseqは入力テーブルと同じ内容をセットする
                                entity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                                entity.SEQ = this.dto.entryEntity.SEQ;

                                //明細システムIDセット
                                entity.DETAIL_SYSTEM_ID = detailSysId;

                                //伝種区分セット
                                entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                entity.UPDATE_PC = SystemInformation.ComputerName;
                                entity.UPDATE_USER = SystemProperty.UserName;
                            }
                            // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する Start
                            break;
                        default:
                            break;
                    }
                    // 20150412 在庫明細部分をここに移動 End
                }

                var dbLogic = new DataBinderLogic<T_SHUKKA_DETAIL>(temp);
                dbLogic.SetSystemProperty(temp, false);

                shukkaDetailEntitys.Add(temp);

                // 在庫明細用カウンタ
                //rowno++;
            }

            this.dto.detailEntity = new T_SHUKKA_DETAIL[shukkaDetailEntitys.Count];
            this.dto.detailEntity = shukkaDetailEntitys.ToArray<T_SHUKKA_DETAIL>();

            /**
             * 合計値系
             */
            // 明細の集計結果系
            // 品名別売上金額合計
            this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = HimeiUrKingakuTotal;
            // entityの値を使って計算するため、処理の最後に計算
            decimal uriageTotal = 0;
            decimal.TryParse(this.form.URIAGE_KINGAKU_TOTAL.Text, out uriageTotal);
            this.dto.entryEntity.URIAGE_AMOUNT_TOTAL = uriageTotal - this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.Value;

            /**
             * 売上の税金系計算
             */
            // 売上伝票毎消費税外税、品名別売上消費税外税合計
            if (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                this.dto.entryEntity.URIAGE_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)(this.dto.entryEntity.URIAGE_AMOUNT_TOTAL * this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE),
                        TaxHasuuCdSeikyuu);
            }
            else
            {
                this.dto.entryEntity.URIAGE_TAX_SOTO = 0;
            }

            if (!this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL
                    = CommonCalc.FractionCalc(
                        HinmeiUrTaxSotoTotal,
                        TaxHasuuCdSeikyuu);

            // 売上伝票毎消費税内税、品名別売上消費税内税合計
            if (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                // 金額計算
                this.dto.entryEntity.URIAGE_TAX_UCHI
                    = (this.dto.entryEntity.URIAGE_AMOUNT_TOTAL
                        - (this.dto.entryEntity.URIAGE_AMOUNT_TOTAL / (this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE + 1)));
                // 端数処理
                this.dto.entryEntity.URIAGE_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.URIAGE_TAX_UCHI,
                        TaxHasuuCdSeikyuu);
            }
            else
            {
                this.dto.entryEntity.URIAGE_TAX_UCHI = 0;
            }

            // 金額計算
            this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = HinmeiUrTaxUchiTotal;

            // 端数処理
            if (!this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL,
                        TaxHasuuCdSeikyuu);

            // 売上伝票毎消費税外税合計
            this.dto.entryEntity.URIAGE_TAX_SOTO_TOTAL = UrTaxSotoTotal;

            // 売上伝票毎消費税内税合計
            this.dto.entryEntity.URIAGE_TAX_UCHI_TOTAL = UrTaxUchiTotal;

            // 品名別支払金額合計
            this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = HimeiShKingakuTotal;
            decimal shiharaiTotal = 0;
            decimal.TryParse(this.form.SHIHARAI_KINGAKU_TOTAL.Text, out shiharaiTotal);
            this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL = shiharaiTotal - this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value;

            /**
             * 支払の税金系計算
             */
            // 支払伝票毎消費税外税、品名別支払消費税外税合計
            if (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                this.dto.entryEntity.SHIHARAI_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL * (decimal)this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE,
                        TaxHasuuCdShiharai);
            }
            else
            {
                this.dto.entryEntity.SHIHARAI_TAX_SOTO = 0;
            }

            if (!this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL
                    = CommonCalc.FractionCalc(
                        HinmeiShTaxSotoTotal,
                        TaxHasuuCdShiharai);

            // 支払伝票毎消費税内税、品名別支払消費税内税合計
            if (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                // 金額計算
                this.dto.entryEntity.SHIHARAI_TAX_UCHI
                    = this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL
                        - (this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL / (this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE + 1));
                // 端数処理
                this.dto.entryEntity.SHIHARAI_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.SHIHARAI_TAX_UCHI,
                        TaxHasuuCdShiharai);
            }
            else
            {
                this.dto.entryEntity.SHIHARAI_TAX_UCHI = 0;
            }

            // 金額計算
            this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = HinmeiShTaxUchiTotal;
            // 端数処理
            if (!this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL,
                        TaxHasuuCdShiharai);

            // 支払明細毎消費税外税合計
            this.dto.entryEntity.SHIHARAI_TAX_SOTO_TOTAL = ShTaxSotoTotal;

            // 支払明細毎消費税内税合計
            this.dto.entryEntity.SHIHARAI_TAX_UCHI_TOTAL = ShTaxUchiTotal;

            #region 検収系(計算が必要な項目)
            decimal kenshuUriageTaxSoto = 0;
            decimal kenshuUriageTaxUchi = 0;
            decimal kenshuShiharaiTaxSoto = 0;
            decimal kenshuShiharaiTaxUchi = 0;
            decimal kenshuUriageShouhizeiRate = 0;
            decimal kenshuShiharaiShouhizeiRate = 0;

            decimal.TryParse(Convert.ToString(this.dto.entryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE), out kenshuUriageShouhizeiRate);
            decimal.TryParse(Convert.ToString(this.dto.entryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE), out kenshuShiharaiShouhizeiRate);

            /**
             * 検収系(計算が必要な項目)
             * 税区分や端数処理CDを使うため、Entryの金額計算系の後で処理する
             */
            if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_DATE.IsNull)
            {
                // 売上
                switch (this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn)
                {
                    case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                        // 検収売上伝票毎消費税外税
                        kenshuUriageTaxSoto
                            = CommonCalc.FractionCalc(
                                (decimal)(this.dto.entryEntity.KENSHU_URIAGE_AMOUNT_TOTAL * kenshuUriageShouhizeiRate),
                                TaxHasuuCdSeikyuu);
                        break;

                    case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                        // 検収売上伝票毎消費税内税
                        // 金額計算
                        kenshuUriageTaxUchi
                            = ((decimal)this.dto.entryEntity.KENSHU_URIAGE_AMOUNT_TOTAL
                                - ((decimal)this.dto.entryEntity.KENSHU_URIAGE_AMOUNT_TOTAL / (kenshuUriageShouhizeiRate + 1)));
                        // 端数処理
                        kenshuUriageTaxUchi
                            = CommonCalc.FractionCalc(
                                kenshuUriageTaxUchi,
                                TaxHasuuCdSeikyuu);
                        break;

                    default:
                        break;
                }

                // 支払
                switch (this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn)
                {
                    case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                        // 検収支払伝票毎消費税外税
                        kenshuShiharaiTaxSoto
                            = CommonCalc.FractionCalc(
                                (decimal)(this.dto.entryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL * kenshuShiharaiShouhizeiRate),
                                TaxHasuuCdSeikyuu);
                        break;

                    case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                        // 検収支払伝票毎消費税内税
                        // 金額計算
                        kenshuShiharaiTaxUchi
                            = ((decimal)this.dto.entryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL
                                - ((decimal)this.dto.entryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL / (kenshuShiharaiShouhizeiRate + 1)));
                        // 端数処理
                        kenshuShiharaiTaxUchi
                            = CommonCalc.FractionCalc(
                                kenshuShiharaiTaxUchi,
                                TaxHasuuCdShiharai);
                        break;

                    default:
                        break;
                }
            }

            this.dto.entryEntity.KENSHU_URIAGE_TAX_SOTO = kenshuUriageTaxSoto;
            this.dto.entryEntity.KENSHU_URIAGE_TAX_UCHI = kenshuUriageTaxUchi;
            this.dto.entryEntity.KENSHU_SHIHARAI_TAX_SOTO = kenshuShiharaiTaxSoto;
            this.dto.entryEntity.KENSHU_SHIHARAI_TAX_UCHI = kenshuShiharaiTaxUchi;

            // 更新用検収明細の作成と、検収入力用の金額を計算
            decimal keinshuUriageTaxSotoTotal = 0;
            decimal keinshuUriageTaxUchiTotal = 0;
            decimal keinshuHinmeiUriageTaxSotoTotal = 0;
            decimal keinshuHinmeiUriageTaxUchiTotal = 0;
            decimal keinshuShiharaiTaxSotoTotal = 0;
            decimal keinshuShiharaiTaxUchiTotal = 0;
            decimal keinshuHinmeiShiharaiTaxSotoTotal = 0;
            decimal keinshuHinmeiShiharaiTaxUchiTotal = 0;
            decimal kensyuHinmeiUriageKingakuTotal = 0;
            decimal kensyuHinmeiShiharaiKingakuTotal = 0;

            if (!this.dto.entryEntity.KENSHU_DATE.IsNull)
            {
                foreach (var kensyuNyuuryoku in this.dto.kenshuNyuuryokuDto.kenshuDetailList)
                {
                    // key系
                    kensyuNyuuryoku.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                    kensyuNyuuryoku.SEQ = this.dto.entryEntity.SEQ;
                    if (!kensyuNyuuryoku.ROW_NO.IsNull)
                    {
                        // 対象のT_SHUKKA_DETAILは行番号で判定
                        // 対象が無いとnullになるので注意
                        var shukkaDetail = this.dto.detailEntity.Where(detail => (short)detail.ROW_NO == (short)kensyuNyuuryoku.ROW_NO).FirstOrDefault<T_SHUKKA_DETAIL>();
                        if (shukkaDetail != null)
                        {
                            // この時点でT_SHUKKA_DETAILのDETIAL_SYSTEM_IDがnullの状態はありえない。
                            // もしnullになる場合は、DETIAL_SYSTEM_IDの採番の箇所を見直す必要がある。
                            kensyuNyuuryoku.DETAIL_SYSTEM_ID = shukkaDetail.DETAIL_SYSTEM_ID;
                        }
                    }
                    if (kensyuNyuuryoku.KENSHU_SYSTEM_ID.IsNull)
                    {
                        // 自動採番
                        kensyuNyuuryoku.KENSHU_SYSTEM_ID = this.accessor.CreateSystemIdForShukka();
                    }
                    else
                    {
                        // 既に設定済みなら何もしない
                    }
                    kensyuNyuuryoku.SHUKKA_NUMBER = this.dto.entryEntity.SHUKKA_NUMBER;

                    // 金額
                    decimal kingaku = 0;
                    if (!kensyuNyuuryoku.KINGAKU.IsNull)
                    {
                        kingaku = kensyuNyuuryoku.KINGAKU.Value;
                    }

                    // 品名金額
                    decimal hinmeiKingaku = 0;
                    if (!kensyuNyuuryoku.HINMEI_KINGAKU.IsNull)
                    {
                        hinmeiKingaku = kensyuNyuuryoku.HINMEI_KINGAKU.Value;
                    }

                    // 伝票区分CD
                    short denpyouKbnCd = -1;
                    if (!kensyuNyuuryoku.DENPYOU_KBN_CD.IsNull)
                    {
                        denpyouKbnCd = kensyuNyuuryoku.DENPYOU_KBN_CD.Value;
                    }

                    // 品名
                    M_HINMEI targetHinmei = null;
                    if (!string.IsNullOrEmpty(kensyuNyuuryoku.HINMEI_CD))
                    {
                        targetHinmei = this.accessor.GetHinmeiDataByCd(kensyuNyuuryoku.HINMEI_CD);
                    }

                    // 品名税区分CD
                    if (targetHinmei != null && !targetHinmei.ZEI_KBN_CD.IsNull && targetHinmei.ZEI_KBN_CD != 0)
                    {
                        #region HINMEI○○付く
                        // HINMEI○○系
                        if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                        {
                            // 売上
                            kensyuHinmeiUriageKingakuTotal += hinmeiKingaku;

                            switch (targetHinmei.ZEI_KBN_CD.ToString())
                            {
                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                    decimal taxSoto =
                                        this.CalcTaxForUriageDetial(
                                            hinmeiKingaku,
                                            kenshuUriageShouhizeiRate,
                                            TaxHasuuCdSeikyuu,
                                            targetHinmei.ZEI_KBN_CD.ToString());

                                    // 合計
                                    keinshuHinmeiUriageTaxSotoTotal += taxSoto;
                                    // 外税
                                    kensyuNyuuryoku.HINMEI_TAX_SOTO = taxSoto;

                                    break;

                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                    decimal taxUchi =
                                        this.CalcTaxForUriageDetial(
                                            hinmeiKingaku,
                                            kenshuUriageShouhizeiRate,
                                            TaxHasuuCdSeikyuu,
                                            targetHinmei.ZEI_KBN_CD.ToString());
                                    // 合計
                                    keinshuHinmeiUriageTaxUchiTotal += taxUchi;
                                    // 内税
                                    kensyuNyuuryoku.HINMEI_TAX_UCHI = taxUchi;

                                    break;

                                default:
                                    break;
                            }
                        }
                        else if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                        {
                            // 支払
                            kensyuHinmeiShiharaiKingakuTotal += hinmeiKingaku;

                            switch (targetHinmei.ZEI_KBN_CD.ToString())
                            {
                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                    decimal taxSoto =
                                        this.CalcTaxForUriageDetial(
                                            hinmeiKingaku,
                                            kenshuShiharaiShouhizeiRate,
                                            TaxHasuuCdShiharai,
                                            targetHinmei.ZEI_KBN_CD.ToString());

                                    // 合計
                                    keinshuHinmeiShiharaiTaxSotoTotal += taxSoto;
                                    // 外税
                                    kensyuNyuuryoku.HINMEI_TAX_SOTO = taxSoto;

                                    break;

                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                    decimal taxUchi =
                                        this.CalcTaxForUriageDetial(
                                            hinmeiKingaku,
                                            kenshuShiharaiShouhizeiRate,
                                            TaxHasuuCdShiharai,
                                            targetHinmei.ZEI_KBN_CD.ToString());
                                    // 合計
                                    keinshuHinmeiShiharaiTaxUchiTotal += taxUchi;
                                    // 内税
                                    kensyuNyuuryoku.HINMEI_TAX_UCHI = taxUchi;

                                    break;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region HINMEI○○付かない
                        // HINMEI○○付かない系
                        if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                        {
                            // 売上
                            switch (this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn)
                            {
                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                    decimal taxSoto =
                                        this.CalcTaxForUriageDetial(
                                            kingaku,
                                            kenshuUriageShouhizeiRate,
                                            TaxHasuuCdSeikyuu,
                                            this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);

                                    // 合計
                                    keinshuUriageTaxSotoTotal += taxSoto;
                                    // 外税
                                    kensyuNyuuryoku.TAX_SOTO = taxSoto;

                                    break;

                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                    decimal taxUchi =
                                        this.CalcTaxForUriageDetial(
                                            kingaku,
                                            kenshuUriageShouhizeiRate,
                                            TaxHasuuCdSeikyuu,
                                            this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                    // 合計
                                    keinshuUriageTaxUchiTotal += taxUchi;
                                    // 内税
                                    kensyuNyuuryoku.TAX_UCHI = taxUchi;

                                    break;

                                default:
                                    break;
                            }
                        }
                        else if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                        {
                            // 支払
                            switch (this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn)
                            {
                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_1:
                                    decimal taxSoto =
                                        this.CalcTaxForUriageDetial(
                                            kingaku,
                                            kenshuShiharaiShouhizeiRate,
                                            TaxHasuuCdShiharai,
                                            this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);

                                    // 合計
                                    keinshuShiharaiTaxSotoTotal += taxSoto;
                                    // 外税
                                    kensyuNyuuryoku.TAX_SOTO = taxSoto;

                                    break;

                                case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                    decimal taxUchi =
                                        this.CalcTaxForUriageDetial(
                                            kingaku,
                                            kenshuShiharaiShouhizeiRate,
                                            TaxHasuuCdShiharai,
                                            this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                    // 合計
                                    keinshuShiharaiTaxUchiTotal += taxUchi;
                                    // 内税
                                    kensyuNyuuryoku.TAX_UCHI = taxUchi;

                                    break;
                            }
                        }
                        #endregion
                    }

                }
            }

            // 検収品名別売上金額合計
            this.dto.entryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL = kensyuHinmeiUriageKingakuTotal;

            // 検収売上明細毎消費税外税合計
            this.dto.entryEntity.KENSHU_URIAGE_TAX_SOTO_TOTAL = keinshuUriageTaxSotoTotal;

            // 検収売上明細毎消費税内税合計
            this.dto.entryEntity.KENSHU_URIAGE_TAX_UCHI_TOTAL = keinshuUriageTaxUchiTotal;

            // 検収品名別売上消費税外税合計
            this.dto.entryEntity.KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL = keinshuHinmeiUriageTaxSotoTotal;

            // 検収品名別売上消費税内税合計
            this.dto.entryEntity.KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL = keinshuHinmeiUriageTaxUchiTotal;

            // 支払

            // 検収品名別支払金額合計
            this.dto.entryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL = kensyuHinmeiShiharaiKingakuTotal;

            // 検収支払明細毎消費税外税合計
            this.dto.entryEntity.KENSHU_SHIHARAI_TAX_SOTO_TOTAL = keinshuShiharaiTaxSotoTotal;

            // 検収支払明細毎消費税内税合計
            this.dto.entryEntity.KENSHU_SHIHARAI_TAX_UCHI_TOTAL = keinshuShiharaiTaxUchiTotal;

            // 検収品名別支払消費税外税合計
            this.dto.entryEntity.KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL = keinshuHinmeiShiharaiTaxSotoTotal;

            // 検収品名別支払消費税内税合計
            this.dto.entryEntity.KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL = keinshuHinmeiShiharaiTaxUchiTotal;
            #endregion

            // S_NUMBER_YEAR
            this.dto.numberYear.NUMBERED_YEAR = numberedYear;
            this.dto.numberYear.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
            this.dto.numberYear.KYOTEN_CD = kyotenCd;
            this.dto.numberYear.CURRENT_NUMBER = this.dto.entryEntity.YEAR_NUMBER;
            this.dto.numberYear.DELETE_FLG = false;
            if (numberYearTimeStamp != null)
            {
                this.dto.numberYear.TIME_STAMP = numberYearTimeStamp;
            }
            var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(this.dto.numberYear);
            dataBinderNumberYear.SetSystemProperty(this.dto.numberYear, false);

            // S_NUMBER_DAY
            this.dto.numberDay.NUMBERED_DAY = denpyouDate.Date;
            this.dto.numberDay.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
            this.dto.numberDay.KYOTEN_CD = kyotenCd;
            this.dto.numberDay.CURRENT_NUMBER = this.dto.entryEntity.DATE_NUMBER;
            this.dto.numberDay.DELETE_FLG = false;
            if (numberDayTimeStamp != null)
            {
                this.dto.numberDay.TIME_STAMP = numberDayTimeStamp;
            }
            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(this.dto.numberDay);
            dataBinderNumberDay.SetSystemProperty(this.dto.numberDay, false);

            ////T_ZAIKO_SHUKKA_DETAIL
            //timeStampCount = 0;
            //foreach (T_ZAIKO_SHUKKA_DETAIL entity in this.dto.saveZaikoEntity)
            //{
            //    //SEQ
            //    entity.SEQ += 1;

            //    // 自動設定
            //    var dataBinderZaikoShukkaDetailt = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
            //    dataBinderZaikoShukkaDetailt.SetSystemProperty(entity, true);
            //}
            ////T_KENSHU_DETAIL
            //timeStampCount = 0;
            //foreach (T_KENSHU_DETAIL entity in this.dto.kenshuDetailList)
            //{
            //    //SEQ
            //    entity.SEQ += 1;

            //    // 自動設定
            //    var dataBinderKenshuDetail = new DataBinderLogic<T_KENSHU_DETAIL>(entity);
            //    dataBinderKenshuDetail.SetSystemProperty(entity, true);
            //}

            LogUtility.DebugMethodEnd(tairyuuKbnFlag);
        }

        /// <summary>
        /// 入金、出金伝票作成
        /// </summary>
        internal void CreateNyuuShukkinEntity()
        {

            /**
             * 取引区分：掛け
             * 精算区分：どちらでも
             */
            if ((Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.SEISAN_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn)
                && Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn))
                || (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.SEISAN_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn)
                && Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn)
                && Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.SOUSATU_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Sosatu))
                )
            {
                /**
                 * 入金一括入力 + 入金入力
                 */
                T_NYUUKIN_SUM_ENTRY nyuukinSumEntry = new T_NYUUKIN_SUM_ENTRY();
                List<T_NYUUKIN_SUM_DETAIL> nyuukinSumDetailList = new List<T_NYUUKIN_SUM_DETAIL>();
                T_NYUUKIN_ENTRY nyuukinEntry = new T_NYUUKIN_ENTRY();
                List<T_NYUUKIN_DETAIL> nyuukinDetaiList = new List<T_NYUUKIN_DETAIL>();

                // 入金系以外から登録された場合に立てる
                nyuukinSumEntry.SEISAN_SOUSAI_CREATE_KBN = true;

                // SYSTEM_ID採番
                nyuukinSumEntry.SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                nyuukinSumEntry.SEQ = 1;
                nyuukinSumEntry.NYUUKIN_NUMBER = this.commonAccesser.createDenshuNumber(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                nyuukinEntry.SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                nyuukinEntry.SEQ = 1;
                short kyotenCd = 0;
                if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
                {
                    nyuukinEntry.KYOTEN_CD = kyotenCd;
                    nyuukinSumEntry.KYOTEN_CD = kyotenCd;
                }

                // 入金番号採番
                nyuukinEntry.NYUUKIN_NUMBER = nyuukinSumEntry.NYUUKIN_NUMBER;
                nyuukinEntry.NYUUKIN_SUM_SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
                nyuukinSumEntry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
                nyuukinEntry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
                nyuukinEntry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                if (this.dto.torihikisakiSeikyuuEntity != null)
                {
                    nyuukinSumEntry.NYUUKINSAKI_CD = this.dto.torihikisakiSeikyuuEntity.NYUUKINSAKI_CD;

                    nyuukinEntry.NYUUKINSAKI_CD = this.dto.torihikisakiSeikyuuEntity.NYUUKINSAKI_CD;
                }
                nyuukinSumEntry.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                nyuukinEntry.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                // 入金額合計は明細を作成した後に計算する
                nyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL = 0;
                nyuukinEntry.CHOUSEI_AMOUNT_TOTAL = 0;
                nyuukinSumEntry.KARIUKEKIN_WARIATE_TOTAL = 0;
                nyuukinSumEntry.DELETE_FLG = false;
                nyuukinEntry.DELETE_FLG = false;

                // 入金一括入力
                var dataBinderNyuukinSumEntry = new DataBinderLogic<T_NYUUKIN_SUM_ENTRY>(nyuukinSumEntry);
                dataBinderNyuukinSumEntry.SetSystemProperty(nyuukinSumEntry, false);

                // 入金入力

                var dataBinderNyuukinEntry = new DataBinderLogic<T_NYUUKIN_ENTRY>(nyuukinEntry);
                dataBinderNyuukinEntry.SetSystemProperty(nyuukinEntry, false);

                /**
                 * 入金一括明細 + 入金明細
                 */
                short rowCount = 1;

                // 相殺明細
                decimal seikyuuSousaiKingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Sousatu_Kingaku, out seikyuuSousaiKingaku)
                    && seikyuuSousaiKingaku != 0)
                {
                    T_NYUUKIN_SUM_DETAIL nyuukinSumDetailForSousai = new T_NYUUKIN_SUM_DETAIL();
                    nyuukinSumDetailForSousai.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinSumDetailForSousai.SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
                    nyuukinSumDetailForSousai.SEQ = nyuukinSumEntry.SEQ;
                    nyuukinSumDetailForSousai.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinSumDetailForSousai.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_SOUSAI;
                    nyuukinSumDetailForSousai.KINGAKU = seikyuuSousaiKingaku;
                    var dataBinderNyuukinSumSousai = new DataBinderLogic<T_NYUUKIN_SUM_DETAIL>(nyuukinSumDetailForSousai);
                    dataBinderNyuukinSumSousai.SetSystemProperty(nyuukinSumDetailForSousai, false);

                    T_NYUUKIN_DETAIL nyuukinDetailForSeikyuuSousai = new T_NYUUKIN_DETAIL();
                    nyuukinDetailForSeikyuuSousai.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                    nyuukinDetailForSeikyuuSousai.SEQ = nyuukinEntry.SEQ;
                    nyuukinDetailForSeikyuuSousai.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinDetailForSeikyuuSousai.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinDetailForSeikyuuSousai.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_SOUSAI;
                    nyuukinDetailForSeikyuuSousai.KINGAKU = seikyuuSousaiKingaku;

                    var dataBinderNyuukinSousai = new DataBinderLogic<T_NYUUKIN_DETAIL>(nyuukinDetailForSeikyuuSousai);
                    dataBinderNyuukinSousai.SetSystemProperty(nyuukinDetailForSeikyuuSousai, false);

                    nyuukinSumDetailList.Add(nyuukinSumDetailForSousai);
                    nyuukinDetaiList.Add(nyuukinDetailForSeikyuuSousai);
                    rowCount++;
                }

                // 現金明細
                decimal seikyuuNyuukingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Nyusyu_Kingaku, out seikyuuNyuukingaku)
                    && seikyuuNyuukingaku != 0)
                {
                    T_NYUUKIN_SUM_DETAIL nyuukinSumDetailForSeikyuuKingaku = new T_NYUUKIN_SUM_DETAIL();
                    nyuukinSumDetailForSeikyuuKingaku.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinSumDetailForSeikyuuKingaku.SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
                    nyuukinSumDetailForSeikyuuKingaku.SEQ = nyuukinSumEntry.SEQ;
                    nyuukinSumDetailForSeikyuuKingaku.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinSumDetailForSeikyuuKingaku.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_GENKIN;
                    nyuukinSumDetailForSeikyuuKingaku.KINGAKU = seikyuuNyuukingaku;
                    var dataBinderNyuukinSumSousai = new DataBinderLogic<T_NYUUKIN_SUM_DETAIL>(nyuukinSumDetailForSeikyuuKingaku);
                    dataBinderNyuukinSumSousai.SetSystemProperty(nyuukinSumDetailForSeikyuuKingaku, false);

                    T_NYUUKIN_DETAIL nyuukinDetailForSeikyuuKingaku = new T_NYUUKIN_DETAIL();
                    nyuukinDetailForSeikyuuKingaku.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                    nyuukinDetailForSeikyuuKingaku.SEQ = nyuukinEntry.SEQ;
                    nyuukinDetailForSeikyuuKingaku.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinDetailForSeikyuuKingaku.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinDetailForSeikyuuKingaku.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_GENKIN;
                    nyuukinDetailForSeikyuuKingaku.KINGAKU = seikyuuNyuukingaku;

                    var dataBinderNyuukinKingaku = new DataBinderLogic<T_NYUUKIN_DETAIL>(nyuukinDetailForSeikyuuKingaku);
                    dataBinderNyuukinKingaku.SetSystemProperty(nyuukinDetailForSeikyuuKingaku, false);

                    nyuukinSumDetailList.Add(nyuukinSumDetailForSeikyuuKingaku);
                    nyuukinDetaiList.Add(nyuukinDetailForSeikyuuKingaku);
                }

                // 入金額計算
                nyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL = seikyuuNyuukingaku;
                nyuukinEntry.NYUUKIN_AMOUNT_TOTAL = seikyuuNyuukingaku;
                nyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL = seikyuuSousaiKingaku;
                nyuukinEntry.CHOUSEI_AMOUNT_TOTAL = seikyuuSousaiKingaku;

                // セット
                this.nyuuShukkinDto.nyuukinSumEntry = nyuukinSumEntry;
                this.nyuuShukkinDto.nyuukinEntry = nyuukinEntry;
                this.nyuuShukkinDto.nyuukinSumDetails = nyuukinSumDetailList;
                this.nyuuShukkinDto.nyuukinDetials = nyuukinDetaiList;
            }

            // 出金
            if ((Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.SEISAN_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Seisan_Kbn)
                && Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn))
                || (Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.SEISAN_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn)
                && Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn)
                && Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.SOUSATU_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Sosatu))
                )
            {
                /**
                 * 出金入力
                 */

                T_SHUKKIN_ENTRY shukkinEntry = new T_SHUKKIN_ENTRY();
                List<T_SHUKKIN_DETAIL> shukkinDetaiList = new List<T_SHUKKIN_DETAIL>();

                // 出金系以外から登録された場合に立てる
                shukkinEntry.SEISAN_SOUSAI_CREATE_KBN = true;

                // SYSTEM_ID採番
                shukkinEntry.SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                shukkinEntry.SEQ = 1;
                short kyotenCd = 0;
                if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
                {
                    shukkinEntry.KYOTEN_CD = kyotenCd;
                }

                // 出金番号採番
                shukkinEntry.SHUKKIN_NUMBER = this.commonAccesser.createDenshuNumber(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                shukkinEntry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
                shukkinEntry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                shukkinEntry.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                // 出金額合計は明細を作成した後に計算
                shukkinEntry.CHOUSEI_AMOUNT_TOTAL = 0;
                shukkinEntry.DELETE_FLG = false;

                var dataBinderNyuukinEntry = new DataBinderLogic<T_SHUKKIN_ENTRY>(shukkinEntry);
                dataBinderNyuukinEntry.SetSystemProperty(shukkinEntry, false);

                /**
                 * 出金明細
                 */
                short rowCount = 1;

                // 相殺明細
                decimal shiharaiSousaiKingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Sousatu_Kingaku, out shiharaiSousaiKingaku)
                    && shiharaiSousaiKingaku != 0)
                {
                    T_SHUKKIN_DETAIL shukkinDetailForShiharaiSousai = new T_SHUKKIN_DETAIL();
                    shukkinDetailForShiharaiSousai.SYSTEM_ID = shukkinEntry.SYSTEM_ID;
                    shukkinDetailForShiharaiSousai.SEQ = shukkinEntry.SEQ;
                    shukkinDetailForShiharaiSousai.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                    shukkinDetailForShiharaiSousai.ROW_NUMBER = (SqlInt16)rowCount;
                    shukkinDetailForShiharaiSousai.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_SOUSAI;
                    shukkinDetailForShiharaiSousai.KINGAKU = shiharaiSousaiKingaku;

                    var dataBinderShiharaiSousai = new DataBinderLogic<T_SHUKKIN_DETAIL>(shukkinDetailForShiharaiSousai);
                    dataBinderShiharaiSousai.SetSystemProperty(shukkinDetailForShiharaiSousai, false);

                    shukkinDetaiList.Add(shukkinDetailForShiharaiSousai);
                    rowCount++;
                }

                // 現金明細
                decimal shiharaiShukkingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Nyusyu_Kingaku, out shiharaiShukkingaku)
                    && shiharaiShukkingaku != 0)
                {
                    T_SHUKKIN_DETAIL shukkinDetailForShiharaiKingaku = new T_SHUKKIN_DETAIL();
                    shukkinDetailForShiharaiKingaku.SYSTEM_ID = shukkinEntry.SYSTEM_ID;
                    shukkinDetailForShiharaiKingaku.SEQ = shukkinEntry.SEQ;
                    shukkinDetailForShiharaiKingaku.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                    shukkinDetailForShiharaiKingaku.ROW_NUMBER = (SqlInt16)rowCount;
                    shukkinDetailForShiharaiKingaku.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_GENKIN;
                    shukkinDetailForShiharaiKingaku.KINGAKU = shiharaiShukkingaku;

                    var dataBinderShukkinKingaku = new DataBinderLogic<T_SHUKKIN_DETAIL>(shukkinDetailForShiharaiKingaku);
                    dataBinderShukkinKingaku.SetSystemProperty(shukkinDetailForShiharaiKingaku, false);

                    shukkinDetaiList.Add(shukkinDetailForShiharaiKingaku);
                }

                // 入金額計算
                shukkinEntry.SHUKKIN_AMOUNT_TOTAL = shiharaiShukkingaku;
                shukkinEntry.CHOUSEI_AMOUNT_TOTAL = shiharaiSousaiKingaku;

                // セット
                this.nyuuShukkinDto.shukkinEntry = shukkinEntry;
                this.nyuuShukkinDto.shukkinDetails = shukkinDetaiList;
            }

        }

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

            LogicClass localLogic = other as LogicClass;
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
        /// 確定利用区分依存のコントロールの表示を切り替える処理
        /// </summary>
        /// <param name="flag">各コントロールのVisibleにそのまま設定される</param>
        private void ChangeVisibleForKakuteiUse(bool flag)
        {
            // 確定フラグ
            this.form.KAKUTEI_KBN_LABEL.Visible = flag;
            this.form.KAKUTEI_KBN.Visible = flag;
            this.form.KAKUTEI_KBN_NAME.Visible = flag;

            // 売上日付
            this.form.URIAGE_DATE_LABEL.Visible = flag;
            this.form.URIAGE_DATE.Visible = flag;

            // 売上消費税
            this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = flag;
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = flag;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = flag;

            // 支払日付
            this.form.SHIHARAI_DATE_LABEL.Visible = flag;
            this.form.SHIHARAI_DATE.Visible = flag;

            // 支払消費税
            this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = flag;
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = flag;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = flag;

            // 売上締処理状況
            this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = flag;
            this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = flag;

            // 支払締処理状況
            this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = flag;
            this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = flag;
        }

        /// <summary>
        /// 明細の制御(システム設定情報用)
        /// </summary>
        /// <param name="headerNames">非表示にするカラム名一覧</param>
        /// <param name="cellNames">非表示にするセル名一覧</param>
        /// <param name="visibleFlag">各カラム、セルのVisibleに設定するbool</param>
        private void ChangePropertyForGC(string[] headerNames, string[] cellNames, string propertyName, bool visibleFlag)
        {
            this.form.gcMultiRow1.SuspendLayout();

            var newTemplate = this.form.gcMultiRow1.Template;

            if (headerNames != null && 0 < headerNames.Length)
            {
                var obj1 = controlUtil.FindControl(newTemplate.ColumnHeaders[0].Cells.ToArray(), headerNames);
                foreach (var o in obj1)
                {
                    PropertyUtility.SetValue(o, propertyName, visibleFlag);
                }
            }

            if (cellNames != null && 0 < cellNames.Length)
            {
                var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), cellNames);
                foreach (var o in obj2)
                {
                    PropertyUtility.SetValue(o, propertyName, visibleFlag);
                }
            }

            this.form.gcMultiRow1.Template = newTemplate;

            this.form.gcMultiRow1.ResumeLayout();
        }

        /// <summary>
        /// 基準正味を取得
        /// ①割振重量があれば割振重量をreturn
        /// ②割振重量がなければ総重量-空車重量をreturn
        /// </summary>
        /// <returns></returns>
        private decimal? GetCriterionNetForCurrent()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.gcMultiRow1.CurrentRow;
            decimal warifuriJyuuryou = 0;
            if (targetRow == null)
            {
                return warifuriJyuuryou;
            }

            if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou))
            {
                return warifuriJyuuryou;
            }
            else
            {
                decimal stakJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                var resultStackJyuuryouTryPase = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stakJyuuryou);
                var resultEmptyJyuuryouTrypase = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
                if (resultStackJyuuryouTryPase && resultEmptyJyuuryouTrypase)
                {
                    return stakJyuuryou - emptyJyuuryou;
                }
            }

            LogUtility.DebugMethodEnd();
            return null;
        }

        /// <summary>
        /// DetailからJyuuryouDtoリストへ値を設定
        /// </summary>
        internal void SetJyuuryouDataToDtoList()
        {
            /**
             * warifuriNo   ：jyuuryouDtoListのindex
             * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
             * **/

            LogUtility.DebugMethodStart();

            if (this.form.gcMultiRow1.Rows.Count(r => !r.IsNewRow) < 1)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            this.form.gcMultiRow1.BeginEdit(false);
            this.jyuuryouDtoList = new List<List<JyuuryouDto>>();
            short i = -1;    // 行カウント用
            int warihuriNo = 0;     // 内部的に使う割振用No
            bool isValidJyuuryouList = false;
            List<JyuuryouDto> jyuuryouList = new List<JyuuryouDto>();

            for (int j = 0; j < this.form.gcMultiRow1.RowCount; j++)
            {
                Row row = this.form.gcMultiRow1.Rows[j];

                if (row.IsNewRow)
                {
                    // 新規行の前までの情報をセット
                    i = -1;
                    this.jyuuryouDtoList.Add(jyuuryouList);
                    warihuriNo++;
                    jyuuryouList = new List<JyuuryouDto>();
                    continue;
                }

                i++;

                // 必要なデータを数値に変換
                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warihuriJyuuryou = 0;
                decimal warihuriPercent = 0;
                decimal chouseiJyuuryou = 0;
                decimal chouseiPercent = 0;
                decimal youkiJyuuryou = 0;
                decimal netJyuuryou = 0;

                JyuuryouDto jyuuryouDto = new JyuuryouDto();

                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou))
                {
                    isValidJyuuryouList = true;
                    jyuuryouDto.stackJyuuryou = stackJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                {
                    isValidJyuuryouList = true;
                    jyuuryouDto.emptyJyuuryou = emptyJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warihuriJyuuryou))
                {
                    jyuuryouDto.warifuriJyuuryou = warihuriJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value), out warihuriPercent))
                {
                    jyuuryouDto.warifuriPercent = warihuriPercent;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou))
                {
                    jyuuryouDto.chouseiJyuuryou = chouseiJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value), out chouseiPercent))
                {
                    jyuuryouDto.chouseiPercent = chouseiPercent;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou))
                {
                    jyuuryouDto.youkiJyuuryou = youkiJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou))
                {
                    jyuuryouDto.netJyuuryou = netJyuuryou;
                }

                row.Cells[CELL_NAME_warihuriNo].Value = warihuriNo;
                row.Cells[CELL_NAME_warihuriRowNo].Value = i;

                jyuuryouList.Add(jyuuryouDto);

                if (j + 1 < this.form.gcMultiRow1.RowCount
                    && (!string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1.Rows[j + 1].Cells[CELL_NAME_STAK_JYUURYOU].Value))
                        || !string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1.Rows[j + 1].Cells[CELL_NAME_EMPTY_JYUURYOU].Value))))
                {
                    // 総重量または空車重量が入っている箇所からは割振が振りなおされると判断する
                    i = -1;
                    // ここまでの情報を1セットとして格納
                    this.jyuuryouDtoList.Add(jyuuryouList);
                    warihuriNo++;
                    jyuuryouList = new List<JyuuryouDto>();
                }
                else if (j + 1 == this.form.gcMultiRow1.RowCount)
                {
                    // 最終行もセット
                    this.jyuuryouDtoList.Add(jyuuryouList);
                }
            }

            if (!isValidJyuuryouList)
            {
                // 有効な総重量 or 空車重量がなかった場合
                this.jyuuryouDtoList = new List<List<JyuuryouDto>>();
            }

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// JyuuryouDtoリストからMultiRowへ値を設定
        /// </summary>
        private void SetJyuuryouDataToMultiRow()
        {
            LogUtility.DebugMethodStart();

            if (this.jyuuryouDtoList == null
                || this.jyuuryouDtoList.Count < 1)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            if (this.form.gcMultiRow1.Rows.Count < 1)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            this.form.gcMultiRow1.BeginEdit(false);

            // MultiRowの重量値系を初期化
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                row.Cells[CELL_NAME_STAK_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_WARIFURI_PERCENT].Value = null;
                row.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
            }

            // 最初にMultiRowのサイズを増やしておく
            int l = 0;
            foreach (var countList in this.jyuuryouDtoList)
            {
                l += countList.Count;
            }

            while (this.form.gcMultiRow1.Rows.Count < l + 1)
            {
                this.form.gcMultiRow1.Rows.Add();
            }

            int i = 0;      // MultiRowのindex
            for (int j = 0; j < this.jyuuryouDtoList.Count; j++)
            {
                var jyuuryouDtos = this.jyuuryouDtoList[j];

                if (jyuuryouDtos == null
                    || jyuuryouDtos.Count < 1)
                {
                    continue;
                }

                if (this.form.gcMultiRow1.Rows.Count <= i)
                {
                    // MultiRowの配列が無くなったら終わり
                    break;
                }

                Boolean KakuteiKbn = false;
                DateTime? UriageShiharaiDt = null;
                string shouhizeiRate = null;
                for (int k = 0; k < jyuuryouDtos.Count; k++)
                {
                    var jyuuryouDto = jyuuryouDtos[k];

                    if (jyuuryouDto == null)
                    {
                        continue;
                    }

                    if (this.form.gcMultiRow1.Rows.Count <= i)
                    {
                        // MultiRowの配列が無くなったら
                        // break;
                    }

                    // MultiRowへ設定
                    Row row = this.form.gcMultiRow1.Rows[i];
                    if (row == null)
                    {
                        continue;
                    }

                    #region 総重量、空車重量、正味重量設定
                    row.Cells[CELL_NAME_STAK_JYUURYOU].Value = jyuuryouDto.stackJyuuryou;
                    row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = jyuuryouDto.emptyJyuuryou;
                    row.Cells[CELL_NAME_NET_JYUURYOU].Value = jyuuryouDto.netJyuuryou;
                    if (row.Cells[CELL_NAME_UNIT_CD].Value != null && row.Cells[CELL_NAME_UNIT_CD].Value.ToString() == "3")
                    {
                        if (row.Cells[CELL_NAME_NET_JYUURYOU].Value != null && !String.IsNullOrEmpty(row.Cells[CELL_NAME_NET_JYUURYOU].Value.ToString()))
                        {
                            row.Cells[CELL_NAME_SUURYOU].Value = jyuuryouDto.netJyuuryou;
                            row.Cells[CELL_NAME_SUURYOU].ReadOnly = true;
                        }
                        else
                        {
                            row.Cells[CELL_NAME_SUURYOU].ReadOnly = false;
                        }
                    }
                    else
                    {
                        row.Cells[CELL_NAME_SUURYOU].ReadOnly = false;
                    }
                    #endregion

                    #region 割振設定
                    row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value = jyuuryouDto.warifuriJyuuryou;
                    row.Cells[CELL_NAME_WARIFURI_PERCENT].Value = jyuuryouDto.warifuriPercent;
                    if (jyuuryouDto.warifuriJyuuryou > 0 || jyuuryouDto.warifuriPercent > 0)
                    {
                        // 割振った最初の行の確定区分と支払/売上日を引き継ぐ
                        if (k == 0)
                        {
                            if (row.Cells[CELL_NAME_KAKUTEI_KBN].Value != null)
                                KakuteiKbn = (Boolean)row.Cells[CELL_NAME_KAKUTEI_KBN].Value;
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString()))
                                UriageShiharaiDt = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                            if (row[CELL_NAME_SHOUHIZEI_RATE].Value != null
                                && !string.IsNullOrEmpty(row[CELL_NAME_SHOUHIZEI_RATE].Value.ToString()))
                                shouhizeiRate = row[CELL_NAME_SHOUHIZEI_RATE].Value.ToString();
                        }
                        if (row.Cells[CELL_NAME_KAKUTEI_KBN].Value == null)
                            row.Cells[CELL_NAME_KAKUTEI_KBN].Value = KakuteiKbn;
                        if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value == null)
                            if (UriageShiharaiDt != null)
                                row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = UriageShiharaiDt;
                        if (row[CELL_NAME_SHOUHIZEI_RATE].Value == null)
                            row[CELL_NAME_SHOUHIZEI_RATE].Value = shouhizeiRate;
                    }
                    else
                    {
                        if (row.Cells[CELL_NAME_NET_JYUURYOU].Value == null)
                        {
                            // 割振重量、正味重量ともに0ならば、引き継いだ確定区分と支払/売上日をクリア
                            row.Cells[CELL_NAME_KAKUTEI_KBN].Value = null;
                            row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = null;
                            row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = null;
                        }
                    }
                    row.Cells[CELL_NAME_warihuriNo].Value = j;
                    row.Cells[CELL_NAME_warihuriRowNo].Value = k;
                    if (!this.CalcSuuryou(row))              // 数量計算
                    {
                        throw new Exception("");
                    }
                    if (!this.IsRegist)
                    {
                        if (!this.CalcDetaiKingaku(row, false))         // 金額計算
                        {
                            throw new Exception("");
                        }
                    }
                    #endregion

                    #region 調整クリア処理
                    // (総重量、空車重量)、割振のいずれも設定されていない場合は、調整はクリアする
                    bool isChouseiPossibleValue = (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null && row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null)
                                                    || row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value != null || row.Cells[CELL_NAME_WARIFURI_PERCENT].Value != null;
                    if (!isChouseiPossibleValue)
                    {
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = string.Empty;
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = string.Empty;
                    }
                    else
                    {
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = jyuuryouDto.chouseiJyuuryou;
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = jyuuryouDto.chouseiPercent;
                    }
                    #endregion

                    #region ReadOnly制御
                    // 割振kg,割振%が設定されている場合は入力可能にしておかないと変更できない
                    // TODO: 重量取込ボタンとかの状態によって入力可能状態を変更しなければいけないかも
                    if (row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value.ToString()))
                    {
                        row.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly = false;
                    }
                    if (row.Cells[CELL_NAME_WARIFURI_PERCENT].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value.ToString()))
                    {
                        row.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly = false;
                    }
                    if (row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value.ToString()))
                    {
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = false;
                    }
                    if (row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value.ToString()))
                    {
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = false;
                    }

                    // 総重量、空車重量の入力制限制御
                    // 割振が設定されている行だったら編集不可とする
                    bool isReadOnlyForStackJyuuryou = false;
                    if (
                        (row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value.ToString()))
                        || (row.Cells[CELL_NAME_WARIFURI_PERCENT].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value.ToString()))
                        || (row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value.ToString()))
                        || (row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value.ToString()))
                        )
                    {
                        isReadOnlyForStackJyuuryou = true;
                        isReadOnlyForStackJyuuryou = true;
                    }

                    row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;
                    row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;

                    row.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);    // No.2076
                    row.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);    // No.2076

                    // 割振、調整のReadOnlyにデフォルト値が設定されるためここで新たに設定する
                    bool isReadOnlyForWarihuriAndChousei = true;
                    if (
                        (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                        && (row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))
                        )
                    {
                        isReadOnlyForWarihuriAndChousei = false;
                    }

                    // 割振計算された行用
                    if (0 < k && isReadOnlyForStackJyuuryou)
                    {
                        isReadOnlyForWarihuriAndChousei = false;
                    }

                    row.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly = isReadOnlyForWarihuriAndChousei;
                    row.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly = isReadOnlyForWarihuriAndChousei;
                    row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnlyForWarihuriAndChousei;
                    row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnlyForWarihuriAndChousei;

                    row.Cells[CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(false);    // No.2076
                    row.Cells[CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(false);    // No.2076
                    row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);    // No.2076
                    row.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);    // No.2076
                    #endregion

                    i++;
                }
            }

            bool isEmptyRow = true;
            Row[] cloneRows = new Row[this.form.gcMultiRow1.RowCount];
            this.form.gcMultiRow1.Rows.CopyTo(cloneRows, 0);
            foreach (var row in cloneRows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                // 初期化
                isEmptyRow = true;

                foreach (var cell in row.Cells)
                {
                    if (cell.Name.Equals(CELL_NAME_warihuriNo)
                        || cell.Name.Equals(CELL_NAME_warihuriRowNo)
                        || cell.Name.Equals(CELL_NAME_ROW_NO))
                    {
                        // nullになりえない or 非表示項目は判定から除外
                        continue;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                    {
                        isEmptyRow = false;
                    }
                }

                if (isEmptyRow)
                {
                    var index = 0 < row.Index ? row.Index - 1 : 0;
                    this.form.gcMultiRow1.Rows.RemoveAt(row.Index);

                    // 削除対象行の一つ上の行で一部のセルが青くなってしまう対策
                    var terget = this.form.gcMultiRow1.Rows[index];
                    terget.Cells[CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(false);
                    terget.Cells[CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(false);
                    terget.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);
                    terget.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);
                }
            }

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 重量値用リストから値を削除
        /// 引数の数値を元に配列内のデータを削除する
        /// </summary>
        /// <param name="warifuriNo">割振グルーピング番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        /// <param name="warifuriRowNo">割振グルーピング内の行番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        private void RemoveJyuuryouDataList(int warifuriNo, short warifuriRowNo)
        {
            /**
             * warifuriNo   ：jyuuryouDtoListのindex
             * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
             * **/

            LogUtility.DebugMethodStart(warifuriNo, warifuriRowNo);

            if (warifuriNo < 0 || warifuriRowNo < 0)
            {
                // 範囲外は弾く
                return;
            }
            if (warifuriNo <= this.jyuuryouDtoList.Count)
            {
                // 範囲外は弾く
                LogUtility.DebugMethodEnd();
                return;
            }

            var jyuuyouDtoList = this.jyuuryouDtoList[warifuriNo];
            if (warifuriRowNo <= jyuuyouDtoList.Count)
            {
                // 範囲外は弾く
                LogUtility.DebugMethodEnd();
                return;
            }

            jyuuyouDtoList.Remove(jyuuyouDtoList[warifuriRowNo]);

            // TODO: 総重量、空車重量、割振kgの再計算

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// JyuuryouDtoをthis.jyuuryouDtoListに追加します
        /// 指定されたwarifuriNoとwarifuriRowNoの後に追加します
        /// </summary>
        /// <param name="warifuriNo">割振グルーピング番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        /// <param name="warifuriRowNo">割振グルーピング内の行番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        /// <param name="targetJyuuryouDto"></param>
        /// <param name="jyuuryouTargetFlag">計算方法の対象を決定するフラグ。true: 割振重量, false: 割振(%)</param>
        private void AddJyuuryouDataList(int warifuriNo, short warifuriRowNo, JyuuryouDto targetJyuuryouDto,
            bool jyuuryouTargetFlag)
        {
            /**
             * warifuriNo   ：jyuuryouDtoListのindex
             * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
             * **/
            LogUtility.DebugMethodStart(warifuriNo, warifuriRowNo, targetJyuuryouDto, jyuuryouTargetFlag);

            if (targetJyuuryouDto == null)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            // warifuriNoが範囲外の場合は新規追加するのかどうか判定
            if (warifuriNo < 0 || warifuriRowNo < 0
                || this.jyuuryouDtoList.Count <= warifuriNo)
            {
                // 範囲外が指定されたら何もしない
                LogUtility.DebugMethodEnd();
                return;
            }

            var jyuuyouDtoList = this.jyuuryouDtoList[warifuriNo];
            if (jyuuyouDtoList.Count <= warifuriRowNo)
            {
                // 範囲外は弾く
                LogUtility.DebugMethodEnd();
                return;
            }

            jyuuyouDtoList[warifuriRowNo] = targetJyuuryouDto;

            // 修正箇所から同グループ内の行は再計算が必要なので削除する
            int i = warifuriRowNo + 1;
            while (i < jyuuyouDtoList.Count)
            {
                jyuuyouDtoList.RemoveAt(i);
            }

            // 再計算処理
            foreach (var jyuuryouDtoList in this.jyuuryouDtoList)
            {
                JyuuryouDto.CalcJyuuryouDtoForAdd(
                    jyuuryouDtoList,
                    jyuuryouTargetFlag,
                    (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                    (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA,
                    (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                    (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Detail必須チェック
        /// Datailが一行以上入力されているかチェックする
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        internal bool CheckRequiredDataForDeital(out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    returnVal = true;
                    break;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// ユーザ入力コントロールの活性制御
        /// </summary>
        /// <param name="isLock">ロック状態に設定するbool</param>
        internal void ChangeEnabledForInputControl(bool isLock)
        {
            LogUtility.DebugMethodStart();

            // UIFormのコントロールを制御
            List<string> formControlNameList = new List<string>();
            if (this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
            {
                // 画面モードが参照用の場合
                formControlNameList.AddRange(refUiFormControlNames);
            }
            else
            {
                formControlNameList.AddRange(inputUiFormControlNames);
            }
            formControlNameList.AddRange(inputHeaderControlNames);
            foreach (var controlName in formControlNameList)
            {
                Control control = controlUtil.FindControl(this.form, controlName);

                if (control == null)
                {
                    // headerを検索
                    control = controlUtil.FindControl(this.headerForm, controlName);
                }

                if (control == null)
                {
                    continue;
                }

                var enabledProperty = control.GetType().GetProperty("Enabled");
                var readOnlyProperty = control.GetType().GetProperty("ReadOnly");

                if (enabledProperty != null)
                {
                    bool readOnlyValue = false;
                    if (readOnlyProperty != null)
                    {
                        readOnlyValue = (bool)readOnlyProperty.GetValue(control, null);
                    }
                    // 車輌CD等、ReadOnlyが動的に変わる箇所の対策としてReadOnlyを判定する
                    if (!readOnlyValue)
                    {
                        enabledProperty.SetValue(control, !isLock, null);
                    }
                }
            }

            // Detailのコントロールを制御
            foreach (Row row in this.form.gcMultiRow1.Rows)
            {
                foreach (var detaiControlName in inputDetailControlNames)
                {
                    row.Cells[detaiControlName].Enabled = !isLock;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細のレイアウト調整
        /// 非表示にしたコントロールが空白で表示されるため調整する
        /// </summary>
        internal void ExecuteAlignmentForDetail()
        {
            LogUtility.DebugMethodStart();

            bool zaikoHinmeiVisable = true;
            bool maniNumberVisible = true;

            this.form.gcMultiRow1.SuspendLayout();
            var newTemplate = this.form.gcMultiRow1.Template;
            // 初期化
            // 確
            var kakuteiHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell1"];
            var kakuteiCell = newTemplate.Row.Cells["KAKUTEI_KBN"];
            // 状況
            var joukyouHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell22"];
            var joukyouCell = newTemplate.Row.Cells["JOUKYOU"];
            // 売上/支払日
            var urShiHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell3"];
            // 20150417 在庫品名関連設定追加 Start
            // 在庫品名CD
            var zaikoHinmeiCdHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader6"];
            zaikoHinmeiCdHeader.Location = new Point(907, 0);//1015
            var zaikoHinmeiCdCell = newTemplate.Row.Cells["ZAIKO_HINMEI_CD"];
            zaikoHinmeiCdCell.Location = new Point(907, 0);//1015
            // 在庫品名
            var zaikoHinmeiNameHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader7"];
            zaikoHinmeiNameHeader.Location = new Point(981, 0);//1089
            var zaikoHinmeiNameCell = newTemplate.Row.Cells["ZAIKO_HINMEI_NAME"];
            zaikoHinmeiNameCell.Location = new Point(981, 0);//1089
            // 在庫単価(元々表示されない)
            var zaikoTankaCell = newTemplate.Row.Cells["ZAIKO_TANKA"];
            zaikoTankaCell.Location = new Point(1145, 0);
            // 20150417 在庫品名関連設定追加 End
            // 20150417 マニフェスト番号位置調整 Start
            // マニフェスト番号
            var maniNumberHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell21"];
            maniNumberHeader.Location = new Point(907, 20);//1015
            var maniNumberCell = newTemplate.Row.Cells["MANIFEST_ID"];
            maniNumberCell.Location = new Point(907, 21);//1015
            // 20150417 マニフェスト番号位置調整 End

            // 明細備考
            var meisaiBikouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell2"];
            meisaiBikouHeader.Location = new Point(89, 20);
            meisaiBikouHeader.Size = new Size(141, 20);//239
            var meisaiBikouCell = newTemplate.Row.Cells["MEISAI_BIKOU"];
            meisaiBikouCell.Location = new Point(89, 21);
            meisaiBikouCell.Size = new Size(141, 21);//239

            // 総重量
            var stackJyuuryouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell4"];
            stackJyuuryouHeader.Location = new Point(192, 0);
            stackJyuuryouHeader.Size = new Size(68, 20);
            var stackJyuuryouCell = newTemplate.Row.Cells["STACK_JYUURYOU"];
            stackJyuuryouCell.Location = new Point(192, 0);
            stackJyuuryouCell.Size = new Size(68, 21);

            // 空車重量
            var emptyJyuuryouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell7"];
            emptyJyuuryouHeader.Location = new Point(260, 0);
            emptyJyuuryouHeader.Size = new Size(68, 20);
            var emptyJyuuryouCell = newTemplate.Row.Cells["EMPTY_JYUURYOU"];
            emptyJyuuryouCell.Location = new Point(260, 0);
            emptyJyuuryouCell.Size = new Size(68, 21);

            // 位置調整
            if (!kakuteiHedader.Visible
                && !joukyouHedader.Visible
                && !urShiHedader.Visible)
            {
                // 明細備考
                meisaiBikouHeader.Location = joukyouHedader.Location;
                meisaiBikouHeader.Size = new Size(joukyouHedader.Width + meisaiBikouHeader.Width, meisaiBikouHeader.Height);
                meisaiBikouCell.Location = joukyouCell.Location;
                meisaiBikouCell.Size = new Size(joukyouCell.Width + meisaiBikouCell.Width, meisaiBikouCell.Height);

                // 総重量
                stackJyuuryouHeader.Location = kakuteiHedader.Location;
                stackJyuuryouHeader.Size = new Size(meisaiBikouHeader.Width / 2, stackJyuuryouHeader.Height);
                stackJyuuryouCell.Location = kakuteiCell.Location;
                stackJyuuryouCell.Size = new Size(meisaiBikouCell.Width / 2, stackJyuuryouCell.Height);

                // 空車重量
                emptyJyuuryouHeader.Location = new Point(stackJyuuryouHeader.Left + stackJyuuryouHeader.Width, emptyJyuuryouHeader.Top);
                emptyJyuuryouHeader.Size = new Size(meisaiBikouHeader.Width - stackJyuuryouHeader.Width, emptyJyuuryouHeader.Height);
                emptyJyuuryouCell.Location = new Point(stackJyuuryouCell.Left + stackJyuuryouCell.Width, emptyJyuuryouCell.Top);
                emptyJyuuryouCell.Size = new Size(meisaiBikouCell.Width - stackJyuuryouCell.Width, emptyJyuuryouCell.Height);
            }

            this.form.gcMultiRow1.Template.Width = maniNumberHeader.Left + maniNumberHeader.Width;
            // 20150417 在庫品名とマニフェスト番号の位置調整 Start
            // 在庫使用区分
            if (this.dto.sysInfoEntity.ZAIKO_KANRI != 1)
            {
                zaikoHinmeiVisable = false;
            }
            // マニ登録形態区分
            if (this.dto.sysInfoEntity.SYS_MANI_KEITAI_KBN == SalesPaymentConstans.SYS_MANI_KEITAI_KBN_DENPYOU)
            {
                maniNumberVisible = false;
            }

            // 在庫品名とマニフェスト番号両方とも表示されない場合、グリッドの幅を詰まる
            if (!zaikoHinmeiVisable && !maniNumberVisible)
            {
                // 在庫品名CD
                zaikoHinmeiCdHeader.Visible = false;
                zaikoHinmeiCdHeader.Location = new Point(0, 0);
                zaikoHinmeiCdCell.Visible = false;
                zaikoHinmeiCdCell.Location = new Point(0, 0);
                // 在庫品名
                zaikoHinmeiNameHeader.Visible = false;
                zaikoHinmeiNameHeader.Location = new Point(0, 0);
                zaikoHinmeiNameCell.Visible = false;
                zaikoHinmeiNameCell.Location = new Point(0, 0);
                // 在庫単価(元々表示されない)
                zaikoTankaCell.Location = new Point(0, 0);
                // マニフェスト番号
                maniNumberHeader.Visible = false;
                maniNumberHeader.Location = new Point(0, 0);
                maniNumberCell.Visible = false;
                maniNumberCell.Location = new Point(0, 0);

                // 荷姿数量ヘッダの位置まで幅を詰まる
                var nisugataHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader2"];
                this.form.gcMultiRow1.Template.Width = nisugataHeader.Left + nisugataHeader.Width;
            }
            // 20150420 在庫品名又はマニフェスト番号どちが表示されない場合、
            //          タイトル文字を空文字にし、セルをReadonlyに設定する(有価在庫不具合一覧105、114) Start
            // 在庫品名だけ表示されない場合
            else if (!zaikoHinmeiVisable)
            {
                var zaikoHinmeiCdCellLocation = zaikoHinmeiCdCell.Location;

                // 在庫品名CD
                zaikoHinmeiCdHeader.Value = string.Empty;
                zaikoHinmeiCdHeader.Size = maniNumberHeader.Size; // マニと同じサイズで設定
                zaikoHinmeiCdHeader.BringToFront();
                zaikoHinmeiCdCell.Visible = false;
                zaikoHinmeiCdCell.Location = new Point(0, 0);
                // 在庫品名
                zaikoHinmeiNameHeader.Visible = false;
                zaikoHinmeiNameHeader.Location = new Point(0, 0);
                zaikoHinmeiNameCell.Visible = false;
                zaikoHinmeiNameCell.Location = new Point(0, 0);
                // 在庫単価(元々表示されない)
                zaikoTankaCell.Location = new Point(0, 0);

                // 在庫単価番号をダミーセルで入れ替え
                Cell filler = newTemplate.Row.Cells.FirstOrDefault(cell => cell.Name == zaikoHinmeiCdCell.Name + "_filler");
                if (filler == null)
                {
                    filler = new GcCustomTextBoxCell();
                    newTemplate.Row.Cells.Add(filler);
                }
                filler.Location = zaikoHinmeiCdCellLocation;
                filler.Size = maniNumberCell.Size; // マニと同じサイズで設定
                filler.Name = zaikoHinmeiCdCell.Name + "_filler";
                filler.ReadOnly = true;
                filler.BringToFront();
            }
            // マニフェスト番号だけ表示されない場合
            else if (!maniNumberVisible)
            {
                var maniNumberCellLocation = maniNumberCell.Location;

                // マニフェスト番号
                maniNumberHeader.Value = string.Empty;
                maniNumberHeader.BringToFront();
                maniNumberCell.Visible = false;
                maniNumberCell.Location = new Point(0, 0);

                // マニフェスト番号をダミーセルで入れ替え
                Cell filler = newTemplate.Row.Cells.FirstOrDefault(cell => cell.Name == maniNumberCell.Name + "_filler");
                if (filler == null)
                {
                    filler = new GcCustomTextBoxCell();
                    newTemplate.Row.Cells.Add(filler);
                }
                filler.Location = maniNumberCellLocation;
                filler.Size = maniNumberCell.Size;
                filler.Name = maniNumberCell.Name + "_filler";
                filler.ReadOnly = true;
                filler.BringToFront();
            }
            // 20150420 在庫品名又はマニフェスト番号どちが表示されない場合、
            //          タイトル文字を空文字にし、セルをReadonlyに設定する(有価在庫不具合一覧105、114) End
            // 20150417 在庫品名とマニフェスト番号の位置調整 End

            this.form.gcMultiRow1.Template = newTemplate;
            this.form.gcMultiRow1.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定した出荷番号のデータが存在するか返す
        /// </summary>
        /// <param name="shukkaNumber">出荷番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistShukkaData(long shukkaNumber, out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (0 <= shukkaNumber)
                {
                    var shukkaEntrys = this.accessor.GetShukkaEntry(shukkaNumber, this.form.SEQ);
                    if (shukkaEntrys != null
                        && 0 < shukkaEntrys.Length)
                    {
                        returnVal = true;
                    }
                }
                else if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    returnVal = true;
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsExistShukkaData", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistShukkaData", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        /// <summary>
        /// 滞留登録された出荷伝票用の権限チェックを行う
        /// </summary>
        /// <param name="shukkaNumber">出荷番号</param>
        /// <param name="seq">SEQ</param>
        /// <returns>true:権限有, false:権限無</returns>
        internal bool HasAuthorityTairyuu(long shukkaNumber, string seq, out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                // 出荷入力
                var entrys = accessor.GetShukkaEntry(shukkaNumber, seq);
                if (entrys == null || entrys.Length < 1)
                {
                    // 対象伝票が無い場合、権限有(未チェック)とみなす。
                    return true;
                }

                if (!entrys[0].TAIRYUU_KBN)
                {
                    // 滞留登録されていなければ、権限有(未チェック)とみなす。
                    return true;
                }

                // 滞留登録された出荷伝票用にWindowTypeが変更対象か判定(削除モード以外は新規モードに変更するため)
                if (HadChangedWindowTypeTairyuu(this.form.WindowType))
                {
                    // 滞留登録された出荷伝票用の権限チェック
                    return r_framework.Authority.Manager.CheckAuthority("G053", tairyuuWindowType, false);
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HasAuthorityTairyuu", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HasAuthorityTairyuu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        /// <summary>
        /// 滞留登録された出荷伝票用に画面区分の変更有無を判定
        /// </summary>
        /// <param name="windowType"></param>
        /// <returns></returns>
        private bool HadChangedWindowTypeTairyuu(WINDOW_TYPE windowType)
        {
            // 滞留一覧から削除で開かれた場合は、モードを変更しない
            return WINDOW_TYPE.DELETE_WINDOW_FLAG != windowType && WINDOW_TYPE.REFERENCE_WINDOW_FLAG != windowType;
        }

        /// <summary>
        /// 重量値、金額値用フォーマット
        /// </summary>
        /// <param name="sender"></param>
        internal void ToAmountValue(object sender)
        {
            try
            {
                LogUtility.DebugMethodStart(sender);

                if (sender == null)
                {
                    return;
                }

                var value = PropertyUtility.GetTextOrValue(sender);
                if (!string.IsNullOrEmpty(value))
                {
                    PropertyUtility.SetTextOrValue(sender, FormatUtility.ToAmountValue(value));
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ToAmountValue", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToAmountValue", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 重量値、金額値用フォーマット(Detial用)
        /// </summary>
        /// <param name="sender"></param>
        internal void ToAmountValueForDetail(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (sender == null)
            {
                return;
            }

            var value = PropertyUtility.GetTextOrValue(this.form.gcMultiRow1[e.RowIndex, e.CellIndex]);
            if (!string.IsNullOrEmpty(value))
            {
                this.form.gcMultiRow1.SetValue(e.RowIndex, e.CellIndex, FormatUtility.ToAmountValue(value));
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上明細毎消費税を計算する(外、内税両方)
        /// </summary>
        /// <param name="hinmei">明細.品名</param>
        /// <param name="kingaku">明細.金額</param>
        /// <param name="zeiKbn">伝票発行画面.請求税区分</param>
        /// <returns></returns>
        private decimal CalcTaxForUriageDetial(decimal kingaku, decimal uriageShouhizeiRate, int hasuuCd, string zeiKbn)
        {
            decimal returnVal = 0;

            // TODO: 税区分はConstクラスの値で判定
            switch (zeiKbn)
            {
                // 一般的な税区分を使用
                case "1":
                    returnVal = CommonCalc.FractionCalc((kingaku * uriageShouhizeiRate), hasuuCd);
                    break;

                case "2":
                    returnVal = kingaku - (kingaku / (uriageShouhizeiRate + 1));
                    // 端数処理
                    returnVal
                        = CommonCalc.FractionCalc(returnVal, hasuuCd);
                    break;

                default:
                    break;
            }

            return returnVal;
        }

        /// <summary>
        /// 伝票発行ポップアップ用連携オブジェクトを生成する
        /// </summary>
        /// <returns></returns>
        internal Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass CreateParameterDTOClass()
        {
            // 一度画面で選択されている場合を考慮し、formのParameterDTOClassで初期化
            Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass returnVal = this.form.denpyouHakouPopUpDTO;

            /**
             * 共通部分
             */

            // 新規、修正共通で設定
            returnVal.Torihikisaki_Cd = this.form.TORIHIKISAKI_CD.Text.ToString();
            returnVal.Gyousha_Cd = this.form.GYOUSHA_CD.Text.ToString();

            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType || this.form.TairyuuNewFlg)
            {
                // DBの情報を復元
                this.form.denpyouHakouPopUpDTO.Tairyuu_Kbn = (Boolean)this.dto.entryEntity.TAIRYUU_KBN;
                this.form.denpyouHakouPopUpDTO.System_Id = this.dto.entryEntity.SYSTEM_ID.ToString();
            }

            this.form.denpyouHakouPopUpDTO.Kakute_Kbn = this.form.KAKUTEI_KBN.Text.Replace("0", "");

            List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass> meisaiDtoList = this.createMeisaiDtoList();
            returnVal.Tenpyo_Cnt = meisaiDtoList;

            /**
             * 検収有、無で変化する部分
             */
            if (this.form.KENSHU_MUST_KBN.Checked)
            {
                var uriageDate = this.GetUriageDateForDenpyouHakou();
                var shiharaiDate = this.GetShiharaiDateForDenpyouHakou();

                if (this.dto.kenshuNyuuryokuDto.kenshuDetailList != null
                    && this.dto.kenshuNyuuryokuDto.kenshuDetailList.Count > 0)
                {
                    // 検収済みの場合は仕切書/計量票出力を行わない
                    returnVal.Print_Enable = true;
                }

                // 検収有
                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.URIAGE_DATE.IsNull)
                {
                    returnVal.Uriage_Date = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.URIAGE_DATE.ToString();
                }
                else
                {
                    // 前回残高計算のためダミーの日付をセット
                    if (uriageDate != null)
                    {
                        returnVal.Uriage_Date = (uriageDate.Value.Date).ToString();
                    }
                }

                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SHIHARAI_DATE.IsNull)
                {
                    returnVal.Shiharai_Date = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SHIHARAI_DATE.ToString();
                }
                else
                {
                    // 前回残高計算のためダミーの日付をセット
                    if (shiharaiDate != null)
                    {
                        returnVal.Shiharai_Date = (shiharaiDate.Value.Date).ToString();
                    }
                }

                // 売上金額合計
                SqlDecimal kenshuUriageAmountTotal = 0;
                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL.IsNull)
                {
                    kenshuUriageAmountTotal = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL;
                }

                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL.IsNull)
                {
                    kenshuUriageAmountTotal += this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL;
                }

                returnVal.Uriage_Amount_Total = kenshuUriageAmountTotal.ToString();

                // 支払金額合計
                SqlDecimal kenshuSiharaiAmoutTotal = 0;
                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL.IsNull)
                {
                    kenshuSiharaiAmoutTotal = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL;
                }

                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL.IsNull)
                {
                    kenshuSiharaiAmoutTotal += this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL;
                }

                returnVal.Shiharai_Amount_Total = kenshuSiharaiAmoutTotal.ToString();

                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE.IsNull)
                {
                    returnVal.Uriage_Shouhizei_Rate = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE.ToString();
                }

                if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE.IsNull)
                {
                    returnVal.Shiharai_Shouhizei_Rate = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE.ToString();
                }

            }
            else
            {
                // 検収無
                var uriageDate = this.GetUriageDateForDenpyouHakou();
                var shiharaiDate = this.GetShiharaiDateForDenpyouHakou();
                if (uriageDate != null)
                {
                    returnVal.Uriage_Date = (uriageDate.Value.Date).ToString();
                }
                if (shiharaiDate != null)
                {
                    returnVal.Shiharai_Date = (shiharaiDate.Value.Date).ToString();
                }
                returnVal.Uriage_Amount_Total = this.form.URIAGE_KINGAKU_TOTAL.Text.ToString();
                returnVal.Shiharai_Amount_Total = this.form.SHIHARAI_KINGAKU_TOTAL.Text.ToString();

                // 消費税率をセット
                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                {
                    if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
                    {
                        this.form.denpyouHakouPopUpDTO.Uriage_Shouhizei_Rate = this.ToDecimalForUriageShouhizeiRate().ToString();
                    }
                    if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
                    {
                        this.form.denpyouHakouPopUpDTO.Shiharai_Shouhizei_Rate = this.ToDecimalForShiharaiShouhizeiRate().ToString();
                    }
                }
                else
                {
                    // 明細単位
                    SqlDateTime tempUriageDate = SqlDateTime.Null;
                    SqlDateTime tempShiharaiDate = SqlDateTime.Null;
                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                        {
                            continue;
                        }

                        DateTime tempUrShDate;
                        if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                            && DateTime.TryParse(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString(), out tempUrShDate)
                            && (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())))
                        {
                            if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                            {
                                if (tempUriageDate.IsNull)
                                {
                                    tempUriageDate = tempUrShDate.Date;
                                }
                                // 一番最後の日付かチェック
                                else if (tempUriageDate < tempUrShDate.Date)
                                {
                                    tempUriageDate = tempUrShDate.Date;
                                }
                            }
                            else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                            {
                                if (tempShiharaiDate.IsNull)
                                {
                                    tempShiharaiDate = tempUrShDate.Date;
                                }
                                // 一番最後の日付かチェック
                                else if (tempShiharaiDate < tempUrShDate.Date)
                                {
                                    tempShiharaiDate = tempUrShDate.Date;
                                }
                            }
                        }
                    }

                    // 消費税セット
                    if (!tempUriageDate.IsNull)
                    {
                        var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempUriageDate).Date);
                        if (shouhizeiEntity != null
                            && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                        {
                            this.form.denpyouHakouPopUpDTO.Uriage_Shouhizei_Rate = shouhizeiEntity.SHOUHIZEI_RATE.ToString();
                        }
                    }
                    if (!tempShiharaiDate.IsNull)
                    {
                        var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempShiharaiDate).Date);
                        if (shouhizeiEntity != null
                            && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                        {
                            this.form.denpyouHakouPopUpDTO.Shiharai_Shouhizei_Rate = shouhizeiEntity.SHOUHIZEI_RATE.ToString();
                        }
                    }
                }
            }

            #region 月次処理 - 月次ロック用

            returnVal.DenpyouDate = string.Empty;
            returnVal.BeforeDenpyouDate = string.Empty;

            // 伝票日付
            returnVal.DenpyouDate = this.form.DENPYOU_DATE.Value.ToString();

            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                // 画面表示時の伝票日付
                returnVal.BeforeDenpyouDate = this.beforDto.entryEntity.DENPYOU_DATE.ToString();
            }

            #endregion

            return returnVal;
        }

        /// <summary>
        /// 売上日付取得(伝票発行ポップアップ用)
        /// 明細毎に日付が設定される場合、明細行の中でもっとも古い日付を取得する
        /// </summary>
        /// <returns>取得できない場合はnullを返す</returns>
        private DateTime? GetUriageDateForDenpyouHakou()
        {
            // Detailの日付をチェック
            if (this.dto.sysInfoEntity.UKEIRE_KAKUTEI_USE_KBN == SalesPaymentConstans.UKEIRE_KAKUTEI_USE_KBN_YES)
            {
                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_MEISAI)
                {
                    // もっとも古い日付を検索
                    DateTime? targetDateTime = null;
                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        if (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value == null)
                        {
                            continue;
                        }
                        else if (!SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            continue;
                        }

                        if (targetDateTime == null)
                        {
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                targetDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                continue;
                            }
                        }
                        else
                        {
                            // 比較
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                var tempDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                if (tempDateTime < targetDateTime)
                                {
                                    // 日付の古いほうをセット
                                    targetDateTime = tempDateTime;
                                }
                            }
                        }
                    }

                    return targetDateTime;
                }
            }

            // Entryの日付をチェック
            if (this.form.URIAGE_DATE.Value != null
                && this.form.URIAGE_DATE.Visible)
            {
                return (DateTime)this.form.URIAGE_DATE.Value;
            }
            return null;
        }

        /// <summary>
        /// 支払日付取得(伝票発行ポップアップ用)
        /// 明細毎に日付が設定される場合、明細行の中でもっとも古い日付を取得する
        /// </summary>
        /// <returns>取得できない場合はnullを返す</returns>
        private DateTime? GetShiharaiDateForDenpyouHakou()
        {
            // Detailの日付をチェック
            if (this.dto.sysInfoEntity.UKEIRE_KAKUTEI_USE_KBN == SalesPaymentConstans.UKEIRE_KAKUTEI_USE_KBN_YES)
            {
                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_MEISAI)
                {
                    // もっとも古い日付を検索
                    DateTime? targetDateTime = null;
                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        if (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value == null)
                        {
                            continue;
                        }
                        else if (!SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            continue;
                        }

                        if (targetDateTime == null)
                        {
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                targetDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                continue;
                            }
                        }
                        else
                        {
                            // 比較
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                var tempDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                if (tempDateTime < targetDateTime)
                                {
                                    // 日付の古いほうをセット
                                    targetDateTime = tempDateTime;
                                }
                            }
                        }
                    }

                    return targetDateTime;
                }
            }

            // Entryの日付をチェック
            if (this.form.SHIHARAI_DATE.Value != null
                && this.form.SHIHARAI_DATE.Visible)
            {
                return (DateTime)this.form.SHIHARAI_DATE.Value;
            }
            return null;
        }

        /// <summary>
        /// 伝票明細リスト生成処理
        /// </summary>
        /// <returns></returns>
        private List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass> createMeisaiDtoList()
        {
            List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass> returnVal = new List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass>();

            if ((this.form.KENSHU_MUST_KBN.Checked) && (false == this.BlankKenshuDetailOutput()))
            {
                // 検収伝票を出力する場合
                if (this.dto.kenshuNyuuryokuDto.kenshuDetailList != null
                    && this.dto.kenshuNyuuryokuDto.kenshuDetailList.Count > 0)
                {
                    foreach (var kenshuDetail in this.dto.kenshuNyuuryokuDto.kenshuDetailList)
                    {
                        Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass meisaiDtoClass = new Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass();
                        meisaiDtoClass.Kakutei_Kbn = this.form.KAKUTEI_KBN.Text;
                        if (!kenshuDetail.DENPYOU_KBN_CD.IsNull
                            && !string.IsNullOrEmpty(kenshuDetail.DENPYOU_KBN_CD.ToString()))
                        {
                            meisaiDtoClass.Uriageshiharai_Kbn = kenshuDetail.DENPYOU_KBN_CD.ToString();
                        }
                        meisaiDtoClass.Hinmei_Cd = kenshuDetail.HINMEI_CD;
                        decimal kingaku = 0;
                        decimal hinmeiKingaku = 0;
                        decimal.TryParse(kenshuDetail.KINGAKU.ToString(), out kingaku);
                        decimal.TryParse(kenshuDetail.HINMEI_KINGAKU.ToString(), out hinmeiKingaku);
                        meisaiDtoClass.Kingaku = (kingaku + hinmeiKingaku).ToString();
                        if (!kenshuDetail.HINMEI_ZEI_KBN_CD.IsNull)
                        {
                            meisaiDtoClass.ZeiKbn = kenshuDetail.HINMEI_ZEI_KBN_CD.ToString();
                        }
                        else
                        {
                            meisaiDtoClass.ZeiKbn = string.Empty;
                        }
                        returnVal.Add(meisaiDtoClass);
                    }
                }
            }
            else if (this.form.KENSHU_MUST_KBN.Checked && this.BlankKenshuDetailOutput())
            {
                // 要検収 + 未検収の場合は空データを作成
                // 「単価」「金額」を空欄とした検収伝票を出力する
                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass meisaiDtoClass = new Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass();
                    // 確定区分
                    if (this.dto.sysInfoEntity.UKEIRE_KAKUTEI_USE_KBN == SalesPaymentConstans.UKEIRE_KAKUTEI_USE_KBN_YES)
                    {
                        if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                        {
                            meisaiDtoClass.Kakutei_Kbn = this.form.KAKUTEI_KBN.Text;
                        }
                        else if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                        {
                            meisaiDtoClass.Kakutei_Kbn = Convert.ToString(row.Cells[CELL_NAME_KAKUTEI_KBN].Value);
                        }
                    }
                    meisaiDtoClass.Uriageshiharai_Kbn = Convert.ToString(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value);
                    meisaiDtoClass.Hinmei_Cd = Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value);
                    meisaiDtoClass.Kingaku = "0";
                    int temp;
                    if (int.TryParse(Convert.ToString(row.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out temp))
                    {
                        meisaiDtoClass.ZeiKbn = Convert.ToString(row.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value);
                    }
                    else
                    {
                        meisaiDtoClass.ZeiKbn = string.Empty;
                    }
                    returnVal.Add(meisaiDtoClass);
                }
            }
            else
            {
                // 出荷伝票
                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass meisaiDtoClass = new Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass();
                    // 確定区分
                    if (this.dto.sysInfoEntity.UKEIRE_KAKUTEI_USE_KBN == SalesPaymentConstans.UKEIRE_KAKUTEI_USE_KBN_YES)
                    {
                        if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                        {
                            meisaiDtoClass.Kakutei_Kbn = this.form.KAKUTEI_KBN.Text;
                        }
                        else if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                        {
                            meisaiDtoClass.Kakutei_Kbn = Convert.ToString(row.Cells[CELL_NAME_KAKUTEI_KBN].Value);
                        }
                    }
                    meisaiDtoClass.Uriageshiharai_Kbn = Convert.ToString(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value);
                    meisaiDtoClass.Hinmei_Cd = Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value);
                    meisaiDtoClass.Kingaku = Convert.ToString(row.Cells[CELL_NAME_KINGAKU].Value);
                    int temp;
                    if (int.TryParse(Convert.ToString(row.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out temp))
                    {
                        meisaiDtoClass.ZeiKbn = Convert.ToString(row.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value);
                    }
                    else
                    {
                        meisaiDtoClass.ZeiKbn = string.Empty;
                    }
                    returnVal.Add(meisaiDtoClass);
                }
            }
            return returnVal;
        }

        /// <summary>
        /// 項目クリア処理
        /// </summary>
        /// <returns></returns>
        internal void ClearEntryData()
        {
            // 新規モード
            /**
             * Entry
             */
            // ヘッダー Start
            // 拠点
            headerForm.KYOTEN_CD.Text = string.Empty;
            headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
            const string KYOTEN_CD = "拠点CD";
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, KYOTEN_CD);
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text.ToString()))
            {
                this.headerForm.KYOTEN_CD.Text = this.headerForm.KYOTEN_CD.Text.ToString().PadLeft(this.headerForm.KYOTEN_CD.MaxLength, '0');
                CheckKyotenCd();
            }

            // 登録者情報
            headerForm.CreateUser.Text = string.Empty;
            headerForm.CreateDate.Text = string.Empty;

            // 更新者情報
            headerForm.LastUpdateUser.Text = string.Empty;
            headerForm.LastUpdateDate.Text = string.Empty;
            // ヘッダー End

            // 詳細 Start
            this.form.ENTRY_NUMBER.Text = string.Empty;
            // 連番
            this.form.RENBAN.Text = string.Empty;
            // 確定区分は1（確定）
            this.form.KAKUTEI_KBN.Text = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI.ToString();
            this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI);
            // 受付番号
            this.form.UKETSUKE_NUMBER.Text = string.Empty;
            // 計量番号
            this.form.KEIRYOU_NUMBER.Text = string.Empty;

            // 消費税率
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = string.Empty;
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = string.Empty;

            // 入力担当者
            if (CommonShogunData.LOGIN_USER_INFO != null
                && !string.IsNullOrEmpty(CommonShogunData.LOGIN_USER_INFO.SHAIN_CD)
                && CommonShogunData.LOGIN_USER_INFO.NYUURYOKU_TANTOU_KBN)
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();
                strNyuryokuTantousyaName = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME.ToString();  // No.3279
            }
            else
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                strNyuryokuTantousyaName = string.Empty;  // No.3279
                this.form.NYUURYOKU_TANTOUSHA_NAME.ReadOnly = true;
            }
            // 車輌
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
            this.form.KUUSHA_JYURYO.Text = string.Empty;  // No.3875
            this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;

            // 取引先
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;
            // 車種
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHARYOU_CD.BackColor = SystemColors.Window;
            this.form.SHASHU_NAME.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
            // 売上締日
            this.form.SEIKYUU_SHIMEBI1.Text = string.Empty;
            this.form.SEIKYUU_SHIMEBI2.Text = string.Empty;
            this.form.SEIKYUU_SHIMEBI3.Text = string.Empty;
            // 支払締日
            this.form.SHIHARAI_SHIMEBI1.Text = string.Empty;
            this.form.SHIHARAI_SHIMEBI2.Text = string.Empty;
            this.form.SHIHARAI_SHIMEBI3.Text = string.Empty;
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;
            // 業者
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.form.GYOUSHA_NAME_RYAKU.Tag = string.Empty;
            // 運転者
            this.form.UNTENSHA_CD.Text = string.Empty;
            this.form.UNTENSHA_NAME.Text = string.Empty;
            this.form.UNTENSHA_NAME.ReadOnly = true;
            // 人数
            this.form.NINZUU_CNT.Text = string.Empty;
            // 現場
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.ReadOnly = true;
            this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
            // 形態区分
            SqlInt16 KeitaiKbnCd = this.accessor.GetKeitaiKbnCd(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA);
            if (KeitaiKbnCd > 0)
            {
                this.form.KEITAI_KBN_CD.Text = KeitaiKbnCd.ToString();
                this.form.KEITAI_KBN_NAME_RYAKU.Text = this.accessor.GetKeitaiKbnNameRyaku(KeitaiKbnCd);
            }
            else
            {
                this.form.KEITAI_KBN_CD.Text = string.Empty;
                this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
            }
            // 台貫
            this.form.DAIKAN_KBN.Text = SalesPaymentConstans.DAIKAN_KBN_JISHA;
            this.form.DAIKAN_KBN_NAME.Text = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(this.form.DAIKAN_KBN.Text.ToString()));
            // 荷積業者
            this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
            this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
            // 荷積現場
            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
            this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

            // No.3815-->
            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.TairyuuNewFlg == false)
            {
                bool catchErr = false;
                // 滞留以外の新規の場合
                // 荷積業者
                const string NIZUMI_GYOUSHA_CD = "荷積業者CD";
                this.form.NIZUMI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NIZUMI_GYOUSHA_CD);
                if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text.ToString()))
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = this.form.NIZUMI_GYOUSHA_CD.Text.ToString().PadLeft(this.form.NIZUMI_GYOUSHA_CD.MaxLength, '0');
                    CheckNizumiGyoushaCd(out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                }
                // 荷積現場
                const string NIZUMI_GENBA_CD = "荷積現場CD";
                this.form.NIZUMI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NIZUMI_GENBA_CD);
                if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text.ToString()))
                {
                    this.form.NIZUMI_GENBA_CD.Text = this.form.NIZUMI_GENBA_CD.Text.ToString().PadLeft(this.form.NIZUMI_GENBA_CD.MaxLength, '0');
                    CheckNizumiGenbaCd(out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                }
            }
            // No.3815<--

            // マニフェスト種類
            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
            // マニフェスト手配
            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
            // 営業担当者
            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.ReadOnly = true;
            // 伝票備考
            this.form.DENPYOU_BIKOU.Text = string.Empty;
            // 滞留備考
            this.form.TAIRYUU_BIKOU.Text = string.Empty;
            // 締処理状況(売上)
            this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = string.Empty;
            // 締処理状況(支払)
            this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = string.Empty;
            // 領収書番号
            this.form.RECEIPT_NUMBER.Text = string.Empty;
            this.form.RECEIPT_NUMBER_DAY.Text = string.Empty;
            this.form.RECEIPT_NUMBER_YEAR.Text = string.Empty;
            // 正味合計
            this.form.NET_TOTAL.Text = string.Empty;
            // 売上金額合計
            this.form.URIAGE_KINGAKU_TOTAL.Text = string.Empty;
            // 支払金額合計
            this.form.SHIHARAI_KINGAKU_TOTAL.Text = string.Empty;
            // 差額
            this.form.SAGAKU.Text = string.Empty;
            //2次
            //取引区分（売）
            this.form.txtUri.Text = string.Empty;
            //取引区分（支）
            this.form.txtShi.Text = string.Empty;
            //締処理状況（在庫）
            this.form.txtShimeZaiko.Text = string.Empty;
            //検収状況
            this.form.txtKensyuu.Text = string.Empty;
            // 要検収
            this.form.KENSHU_MUST_KBN.Checked = false;
            // 詳細 End

            /**
             * Detail
             */
            // テンプレートをいじる処理は、データ設定前に実行
            this.ExecuteAlignmentForDetail();
            this.form.gcMultiRow1.BeginEdit(false);
            this.form.gcMultiRow1.Rows.Clear();
            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
        }

        /// <summary>
        /// 画面の各コントロールのプロパティを設定
        /// </summary>
        internal void SetControlProperties()
        {
            //// 2013/11/15以降のバグ修正分のみを設定

            // /**
            //  * ヘッダー
            //  */
            //// 拠点
            //this.headerForm.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            //this.headerForm.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            //this.headerForm.KYOTEN_NAME_RYAKU.Enabled = true;
            //// 拠点マスタ存在チェック設定 Begin
            //SelectCheckDto kyotenSelectCheckDtoForMasterCheck = new SelectCheckDto();
            //kyotenSelectCheckDtoForMasterCheck.CheckMethodName = "拠点マスタコードチェックandセッティング";
            //Collection<SelectCheckDto> kyotenFocusOutCheckMethods = new Collection<SelectCheckDto>();
            //kyotenFocusOutCheckMethods.Add(kyotenSelectCheckDtoForMasterCheck);
            //this.headerForm.KYOTEN_CD.FocusOutCheckMethod = kyotenFocusOutCheckMethods;
            //// 拠点マスタ存在チェック設定 End
            //// 拠点必須チェック設定 Begin
            //SelectCheckDto kyotenSelectCheckDtoForRegistCheck = new SelectCheckDto();
            //kyotenSelectCheckDtoForRegistCheck.CheckMethodName = "必須チェック";
            //Collection<SelectCheckDto> kyotenRegistCheckMethods = new Collection<SelectCheckDto>();
            //kyotenRegistCheckMethods.Add(kyotenSelectCheckDtoForRegistCheck);
            //this.headerForm.KYOTEN_CD.RegistCheckMethod = kyotenRegistCheckMethods;
            //// 拠点必須チェック設定 End

            // /**
            //  * メインフォーム
            //  */
            //// 受入番号
            //this.form.ENTRY_NUMBER.TextAlign = HorizontalAlignment.Right;
            //// 連番
            //this.form.RENBAN.TextAlign = HorizontalAlignment.Right;
            //// 確定フラグ
            //this.form.KAKUTEI_KBN.TextAlign = HorizontalAlignment.Right;
            //// 受付番号
            //this.form.UKETSUKE_NUMBER.TextAlign = HorizontalAlignment.Right;
            //// 計量番号
            //this.form.KEIRYOU_NUMBER.TextAlign = HorizontalAlignment.Right;
            //// 支払日付
            //this.form.SHIHARAI_DATE.Tag = "支払日付を入力してください";
            //// 取引先
            //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            //// 現場
            //this.form.GENBA_NAME_RYAKU.TabStop = false;
            //// 業者
            //this.form.GYOUSHA_NAME_RYAKU.TabStop = false;
            //// 業者検索ポップアップ設定 Begin
            //Collection<PopupSearchSendParamDto> gyoushaPopupSearchSendParams = new Collection<PopupSearchSendParamDto>();

            //// 出荷区分条件
            //PopupSearchSendParamDto shukkaKbnConditionForGyousha = new PopupSearchSendParamDto();
            //shukkaKbnConditionForGyousha.And_Or = CONDITION_OPERATOR.AND;
            //shukkaKbnConditionForGyousha.KeyName = "GYOUSHAKBN_SHUKKA";
            //shukkaKbnConditionForGyousha.Value = "True";
            //gyoushaPopupSearchSendParams.Add(shukkaKbnConditionForGyousha);

            //// 取引先条件
            //PopupSearchSendParamDto torihikisakiConditionForGyousha = new PopupSearchSendParamDto();
            //torihikisakiConditionForGyousha.And_Or = CONDITION_OPERATOR.AND;
            //torihikisakiConditionForGyousha.Control = this.form.TORIHIKISAKI_CD.Name;
            //torihikisakiConditionForGyousha.KeyName = "TORIHIKISAKI_CD";
            //gyoushaPopupSearchSendParams.Add(torihikisakiConditionForGyousha);

            //this.form.TORIHIKISAKI_CD.PopupSearchSendParams = gyoushaPopupSearchSendParams;

            //// 業者検索ポップアップ設定 End

            //// 荷降業者
            //this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;
            //this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.PopupWindowId = WINDOW_ID.M_GYOUSHA;
            //// 荷降現場
            //this.form.NIZUMI_GENBA_NAME.TabStop = false;

            //// 運搬業者
            //this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
            //// 車輌CD
            //this.form.SHARYOU_CD.PopupSetFormField = "SHARYOU_CD, SHARYOU_NAME_RYAKU, UNPAN_GYOUSHA_CD, UNPAN_GYOUSHA_NAME, SHASHU_CD, SHASHU_NAME, UNTENSHA_CD, UNTENSHA_NAME";
            //this.form.SHARYOU_CD.PopupWindowName = "車両選択共通ポップアップ";
            //this.form.SHARYOU_NAME_RYAKU.TabStop = false;
            //// 車輌検索ポップアップ設定 Begin
            //Collection<PopupSearchSendParamDto> sharyouCdPopupSearchSendParams = new Collection<PopupSearchSendParamDto>();

            //// 運搬業者条件
            //PopupSearchSendParamDto unpanGyoushaCdConditionForSharyouCd = new PopupSearchSendParamDto();
            //unpanGyoushaCdConditionForSharyouCd.And_Or = CONDITION_OPERATOR.AND;
            //unpanGyoushaCdConditionForSharyouCd.Control = this.form.UNPAN_GYOUSHA_CD.Name;
            //unpanGyoushaCdConditionForSharyouCd.KeyName = "key001";
            //sharyouCdPopupSearchSendParams.Add(unpanGyoushaCdConditionForSharyouCd);

            //// 車種条件
            //PopupSearchSendParamDto shashuCdConditionForSharyouCd = new PopupSearchSendParamDto();
            //shashuCdConditionForSharyouCd.And_Or = CONDITION_OPERATOR.AND;
            //shashuCdConditionForSharyouCd.Control = this.form.SHASHU_CD.Name;
            //shashuCdConditionForSharyouCd.KeyName = "key003";
            //sharyouCdPopupSearchSendParams.Add(shashuCdConditionForSharyouCd);

            //// 車輌条件
            //PopupSearchSendParamDto sharyouCdConditionForSharyouCd = new PopupSearchSendParamDto();
            //sharyouCdConditionForSharyouCd.And_Or = CONDITION_OPERATOR.AND;
            //sharyouCdConditionForSharyouCd.Control = this.form.SHARYOU_CD.Name;
            //sharyouCdConditionForSharyouCd.KeyName = "key002";
            //sharyouCdPopupSearchSendParams.Add(sharyouCdConditionForSharyouCd);

            //this.form.SHARYOU_CD.PopupSearchSendParams = sharyouCdPopupSearchSendParams;
            //// 車輌検索ポップアップ設定 End

            //// 人数
            //this.form.NINZUU_CNT.Tag = "半角2桁以内で入力してください";
            //this.form.NINZUU_CNT.TextAlign = HorizontalAlignment.Right;
            //// 形態区分
            //this.form.KEITAI_KBN_CD.ZeroPaddengFlag = false;
            //// マニ種類
            //this.form.MANIFEST_SHURUI_CD.ZeroPaddengFlag = false;
            //// コンテナ
            //this.form.CONTENA_SOUSA_CD.ZeroPaddengFlag = false;
            //// マニ手配
            //this.form.MANIFEST_TEHAI_CD.ZeroPaddengFlag = false;
            //// 支払金額合計
            //this.form.SHIHARAI_AMOUNT_TOTAL_LABEL.Font = new Font("ＭＳ ゴシック", 9F);
            //// 売上金額合計
            //this.form.URIAGE_AMOUNT_TOTAL_LABEL.Font = new Font("ＭＳ ゴシック", 9F);

            // /**
            //  * 明細
            //  */
            //this.form.gcMultiRow1.ScrollBars = ScrollBars.Vertical;
        }

        /// <summary>
        /// 検索ボタンの設定をする
        /// デザインのマージ対策
        /// レイアウトの調整をするとぬめぬめ動くと思われるので、
        /// ポップアップの設定だけをセッティング
        /// </summary>
        internal void SetSearchButtonInfo()
        {
            // 2013/11/15以降のバグ修正分のみを設定

            // 業者
            this.form.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.GYOUSHA_CD.PopupGetMasterField;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = this.form.GYOUSHA_CD.PopupSetFormField;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.GYOUSHA_CD.PopupMultiSelect;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.GYOUSHA_CD.PopupSearchSendParams;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupWindowId = this.form.GYOUSHA_CD.PopupWindowId;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupWindowName = this.form.GYOUSHA_CD.PopupWindowName;
            this.form.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = this.form.GYOUSHA_CD.popupWindowSetting;

            // 入力担当者
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.NYUURYOKU_TANTOUSHA_CD.PopupGetMasterField;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupSetFormField = this.form.NYUURYOKU_TANTOUSHA_CD.PopupSetFormField;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.NYUURYOKU_TANTOUSHA_CD.PopupMultiSelect;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.NYUURYOKU_TANTOUSHA_CD.PopupSearchSendParams;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupWindowId = this.form.NYUURYOKU_TANTOUSHA_CD.PopupWindowId;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupWindowName = this.form.NYUURYOKU_TANTOUSHA_CD.PopupWindowName;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.popupWindowSetting = this.form.NYUURYOKU_TANTOUSHA_CD.popupWindowSetting;

            // 車輌
            this.form.SHARYOU_SEARCH_BUTTON.PopupGetMasterField = this.form.SHARYOU_CD.PopupGetMasterField;
            this.form.SHARYOU_SEARCH_BUTTON.PopupSetFormField = this.form.SHARYOU_CD.PopupSetFormField;
            this.form.SHARYOU_SEARCH_BUTTON.PopupMultiSelect = this.form.SHARYOU_CD.PopupMultiSelect;
            this.form.SHARYOU_SEARCH_BUTTON.PopupSearchSendParams = this.form.SHARYOU_CD.PopupSearchSendParams;
            this.form.SHARYOU_SEARCH_BUTTON.PopupWindowId = this.form.SHARYOU_CD.PopupWindowId;
            this.form.SHARYOU_SEARCH_BUTTON.PopupWindowName = this.form.SHARYOU_CD.PopupWindowName;
            this.form.SHARYOU_SEARCH_BUTTON.popupWindowSetting = this.form.SHARYOU_CD.popupWindowSetting;

            // 車種
            this.form.SHASHU_SEARCH_BUTTON.PopupGetMasterField = this.form.SHASHU_CD.PopupGetMasterField;
            this.form.SHASHU_SEARCH_BUTTON.PopupSetFormField = this.form.SHASHU_CD.PopupSetFormField;
            this.form.SHASHU_SEARCH_BUTTON.PopupMultiSelect = this.form.SHASHU_CD.PopupMultiSelect;
            this.form.SHASHU_SEARCH_BUTTON.PopupSearchSendParams = this.form.SHASHU_CD.PopupSearchSendParams;
            this.form.SHASHU_SEARCH_BUTTON.PopupWindowId = this.form.SHASHU_CD.PopupWindowId;
            this.form.SHASHU_SEARCH_BUTTON.PopupWindowName = this.form.SHASHU_CD.PopupWindowName;
            this.form.SHASHU_SEARCH_BUTTON.popupWindowSetting = this.form.SHASHU_CD.popupWindowSetting;

            // 運転者
            this.form.UNTENSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.UNTENSHA_CD.PopupGetMasterField;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupSetFormField = this.form.UNTENSHA_CD.PopupSetFormField;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.UNTENSHA_CD.PopupMultiSelect;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.UNTENSHA_CD.PopupSearchSendParams;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupWindowId = this.form.UNTENSHA_CD.PopupWindowId;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupWindowName = this.form.UNTENSHA_CD.PopupWindowName;
            this.form.UNTENSHA_SEARCH_BUTTON.popupWindowSetting = this.form.UNTENSHA_CD.popupWindowSetting;

            // 形態区分
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupGetMasterField = this.form.KEITAI_KBN_CD.PopupGetMasterField;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupSetFormField = this.form.KEITAI_KBN_CD.PopupSetFormField;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupMultiSelect = this.form.KEITAI_KBN_CD.PopupMultiSelect;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupSearchSendParams = this.form.KEITAI_KBN_CD.PopupSearchSendParams;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupWindowId = this.form.KEITAI_KBN_CD.PopupWindowId;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupWindowName = this.form.KEITAI_KBN_CD.PopupWindowName;
            this.form.KEITAI_KBN_SEARCH_BUTTON.popupWindowSetting = this.form.KEITAI_KBN_CD.popupWindowSetting;

            // マニ種類
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupGetMasterField = this.form.MANIFEST_SHURUI_CD.PopupGetMasterField;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupSetFormField = this.form.MANIFEST_SHURUI_CD.PopupSetFormField;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupMultiSelect = this.form.MANIFEST_SHURUI_CD.PopupMultiSelect;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupSearchSendParams = this.form.MANIFEST_SHURUI_CD.PopupSearchSendParams;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupWindowId = this.form.MANIFEST_SHURUI_CD.PopupWindowId;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupWindowName = this.form.MANIFEST_SHURUI_CD.PopupWindowName;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.popupWindowSetting = this.form.MANIFEST_SHURUI_CD.popupWindowSetting;

            // マニ手配
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupGetMasterField = this.form.MANIFEST_TEHAI_CD.PopupGetMasterField;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupSetFormField = this.form.MANIFEST_TEHAI_CD.PopupSetFormField;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupMultiSelect = this.form.MANIFEST_TEHAI_CD.PopupMultiSelect;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupSearchSendParams = this.form.MANIFEST_TEHAI_CD.PopupSearchSendParams;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupWindowId = this.form.MANIFEST_TEHAI_CD.PopupWindowId;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupWindowName = this.form.MANIFEST_TEHAI_CD.PopupWindowName;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.popupWindowSetting = this.form.MANIFEST_TEHAI_CD.popupWindowSetting;

            // 営業担当者
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.EIGYOU_TANTOUSHA_CD.PopupGetMasterField;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupSetFormField = this.form.EIGYOU_TANTOUSHA_CD.PopupSetFormField;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.EIGYOU_TANTOUSHA_CD.PopupMultiSelect;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.EIGYOU_TANTOUSHA_CD.PopupSearchSendParams;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupWindowId = this.form.EIGYOU_TANTOUSHA_CD.PopupWindowId;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupWindowName = this.form.EIGYOU_TANTOUSHA_CD.PopupWindowName;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.popupWindowSetting = this.form.EIGYOU_TANTOUSHA_CD.popupWindowSetting;

            // 各CDのフォーカスアウト処理を通すため、検索ポップアップから戻ってきたら各CDへフォーカスする
            this.form.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToTorihikisakiCd";
            // TODO: 業者、現場の検索ボタン名がおかしいため後で修正
            this.form.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGyoushaCd";
            this.form.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGenbaCd";
            this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNiZumiGyoushaCd";
            this.form.NIZUMI_GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNiZumiGenbaCd";
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToEigyouTantoushaCd";
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNyuuryokuTantoushaCd";
            //this.form.SHARYOU_CD.PopupAfterExecuteMethod = "MoveToSharyouCd";
            this.form.SHARYOU_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToSharyouCd";
            this.form.SHASHU_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToShashuCd";
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToUnpanGyoushaCd";
            this.form.UNTENSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToUntenshaCd";
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToKeitaiKbnCd";
            // TODO: コンテナのコントロール名が全体的におかしいので後で修正
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToManiShuruiCd";
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToManiTehaiCd";

        }

        /// <summary>
        /// M_SYS_INFOでデフォルト値を設定
        /// 当該画面で必須かつ、Nullのものに値を設定する
        /// </summary>
        private void SetSysInfoDefaultValue()
        {
            // システム連番方法区分
            if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN.IsNull)
            {
                this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN = 1;
            }

            // システム確定登録単位区分
            if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN.IsNull)
            {
                this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN = 1;
            }

            // 20150417 在庫使用区分初期化追加 Start
            // 在庫使用区分
            if (this.dto.sysInfoEntity.ZAIKO_KANRI.IsNull)
            {
                this.dto.sysInfoEntity.ZAIKO_KANRI = 1;
            }
            // 20150417 在庫使用区分初期化追加 End

            // システムマニ登録形態区分
            if (this.dto.sysInfoEntity.SYS_MANI_KEITAI_KBN.IsNull)
            {
                this.dto.sysInfoEntity.SYS_MANI_KEITAI_KBN = 1;
            }

            // 受入情報確定利用区分
            if (this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_KAKUTEI_USE_KBN = 1;
            }

            // 受入情報差引基準
            if (this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN = 1;
            }

            // 受入情報割振値端数CD
            if (this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD = 1;
            }

            // 受入情報割振値端数処理桁
            if (this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA = 1;
            }

            // 受入情報割振割合端数CD
            if (this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD = 1;
            }

            // 受入情報割振割合端数処理桁
            if (this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA = 1;
            }

            // 受入情報調整値端数CD
            if (this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_CD.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_CD = 1;
            }

            // 受入情報調整値端数処理桁
            if (this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_KETA.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_KETA = 1;
            }

            // 受入情報調整割合端数CD
            if (this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_CD.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_CD = 1;
            }

            // 受入情報調整割合端数処理桁
            if (this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_KETA.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_KETA = 1;
            }
        }

        /// <summary>
        /// 諸口区分用プレビューキーダウンイベント
        /// 諸口区分が存在する取引先、業者、現場で使用する
        /// ※例外として車輌でも使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDownForShokuchikbnCheck(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    this.form.pressedEnterOrTab = true;
                    break;

                default:
                    this.form.pressedEnterOrTab = false;
                    break;
            }
        }

        /// <summary>
        /// 諸口区分用フォーカス移動処理
        /// </summary>
        /// <param name="control"></param>
        private void MoveToNextControlForShokuchikbnCheck(ICustomControl control)
        {
            if (this.form.pressedEnterOrTab)
            {
                var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                this.form.SelectNextControl((Control)control, !isPressShift, true, true, true);
            }

            // マウス操作を考慮するためpressedEnterOrTabを初期化
            this.form.pressedEnterOrTab = false;
        }

        /// <summary>
        /// 調整kg, 調整%の入力制御
        /// </summary>
        internal bool ChangeInputStatusForChousei()
        {
            try
            {
                var row = this.form.gcMultiRow1.CurrentRow;

                if (row == null)
                {
                    return true;
                }

                bool isReadOnlyForStackJyuuryou = false;
                if (
                    (row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value.ToString()))
                    || (row.Cells[CELL_NAME_WARIFURI_PERCENT].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value.ToString()))
                    || (row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value.ToString()))
                    || (row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value.ToString()))
                    )
                {
                    isReadOnlyForStackJyuuryou = true;
                    isReadOnlyForStackJyuuryou = true;
                }

                row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;
                row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;

                row.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);    // No.2076
                row.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);    // No.2076

                // 割振、調整のReadOnlyにデフォルト値が設定されるためここで新たに設定する
                bool isReadOnlyForWarihuriAndChousei = true;
                if (
                    (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                    || (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                    )
                {
                    isReadOnlyForWarihuriAndChousei = false;
                }

                // 割振計算された行用
                int k = 0;
                if (int.TryParse(Convert.ToString(row.Cells[CELL_NAME_warihuriRowNo].Value), out k)
                    && 0 < k)
                {
                    isReadOnlyForWarihuriAndChousei = false;
                }

                row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnlyForWarihuriAndChousei;
                row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnlyForWarihuriAndChousei;

                row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);    // No.2076
                row.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);    // No.2076

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeInputStatusForChousei", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 受付番号からデータ取得
        /// </summary>
        internal bool GetUketsukeNumber()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                {
                    return true;
                }

                // 受付（出荷）からデータ取得
                DataTable dt = this.accessor.GetUketsukeSK(this.form.UKETSUKE_NUMBER.Text);
                if (dt.Rows.Count == 0)
                {
                    // データなし

                    // メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");

                    // 入力受付番号クリア
                    this.form.UKETSUKE_NUMBER.Text = ""; // No.3163

                    //フォーカスを受付番号にする
                    this.form.UKETSUKE_NUMBER.Focus();

                    // 処理終了
                    return true;
                }

                if (!this.RenkeiCheck(this.form.UKETSUKE_NUMBER.Text))
                {
                    this.form.UKETSUKE_NUMBER.Text = string.Empty;
                    return true;
                }

                var haishaJokyoCd = dt.Rows[0]["HAISHA_JOKYO_CD"].ToString();

                // 配車状況が「1:受注」「2:配車」「3:計上」以外は遷移できない
                if (SalesPaymentConstans.HAISHA_JOKYO_CD_CANCEL.Equals(haishaJokyoCd) || SalesPaymentConstans.HAISHA_JOKYO_CD_NASHI.Equals(haishaJokyoCd))
                {
                    // メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E101", "伝票", "売上");

                    // 入力受付番号クリア
                    this.form.UKETSUKE_NUMBER.Text = "";

                    //フォーカスを受付番号にする
                    this.form.UKETSUKE_NUMBER.Focus();

                    // 処理終了
                    return true;
                }

                // 登録時に配車状況を変更するためにエンティティを保存
                var systemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                var seq = dt.Rows[0]["SEQ"].ToString();
                this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                //伝票日付、売上日付、支払日付
                if (!string.IsNullOrEmpty(dt.Rows[0]["SAGYOU_DATE"].ToString()))
                {
                    this.form.DENPYOU_DATE.Text = dt.Rows[0]["SAGYOU_DATE"].ToString();
                    this.form.URIAGE_DATE.Text = dt.Rows[0]["SAGYOU_DATE"].ToString();
                    this.form.SHIHARAI_DATE.Text = dt.Rows[0]["SAGYOU_DATE"].ToString();

                    // 消費税率の設定
                    DateTime uriageDate = this.footerForm.sysDate.Date;
                    if (DateTime.TryParse(this.form.URIAGE_DATE.Text, out uriageDate))
                    {
                        var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageDate);
                        if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                        {
                            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                        }
                    }

                    DateTime shiharaiDate = this.footerForm.sysDate.Date;
                    if (DateTime.TryParse(this.form.SHIHARAI_DATE.Text, out shiharaiDate))
                    {
                        var shiharaiShouhizeiRate = this.accessor.GetShouhizeiRate(shiharaiDate);
                        if (!shiharaiShouhizeiRate.SHOUHIZEI_RATE.IsNull)
                        {
                            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = shiharaiShouhizeiRate.SHOUHIZEI_RATE.ToString();
                        }
                    }
                }
                //取引先CD
                if (string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                {
                    this.form.TORIHIKISAKI_CD.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.TORIHIKISAKI_CD.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                }
                //業者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.GYOUSHA_CD.Text = string.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.GYOUSHA_CD.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    this.form.GYOUSHA_NAME_RYAKU.Text = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                }
                //現場CD
                if (string.IsNullOrEmpty(dt.Rows[0]["GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GENBA_NAME_RYAKU"].ToString()))
                {
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    strGenbaName = string.Empty;   // No.3279
                }
                else
                {
                    this.form.GENBA_CD.Text = dt.Rows[0]["GENBA_CD"].ToString();
                    this.form.GENBA_NAME_RYAKU.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();

                    // No.3279-->
                    bool catchErr = false;
                    M_GENBA entGenba = accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
                    {
                        return false;
                    }

                    if (entGenba != null)
                    {
                        // 諸口区分チェック
                        if (entGenba.SHOKUCHI_KBN.IsTrue)
                        {
                            strGenbaName = this.form.GENBA_NAME_RYAKU.Text;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(entGenba.GENBA_NAME1))
                            {
                                strGenbaName = entGenba.GENBA_NAME1 + entGenba.GENBA_NAME2;
                            }
                            else
                            {
                                strGenbaName = "";
                            }
                        }

                        // 要検収のデフォルト値をセット
                        if (!entGenba.KENSHU_YOUHI.IsNull)
                        {
                            this.form.KENSHU_MUST_KBN.Checked = entGenba.KENSHU_YOUHI.Value;
                        }
                    }
                    // No.3279<--
                }
                //運搬業者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                    this.form.UNPAN_GYOUSHA_NAME.Text = dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString();
                }
                //荷積業者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                    this.form.NIZUMI_GYOUSHA_NAME.Text = dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString();
                }
                //荷積現場CD
                if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString()))
                {
                    this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.NIZUMI_GENBA_CD.Text = dt.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                    this.form.NIZUMI_GENBA_NAME.Text = dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString();
                }
                //営業担当者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString();
                }
                //車輌CD
                if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_CD"].ToString()))
                {
                    //コードが無しの場合は未入力状態
                    this.form.SHARYOU_CD.Text = "";
                    this.form.SHARYOU_NAME_RYAKU.Text = "";
                }
                else if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString()))
                {
                    this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                    this.CheckSharyouUketsuke_NASHI(dt);
                }
                else
                {
                    this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                    this.form.SHARYOU_NAME_RYAKU.Text = dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                }
                //車種CD
                if (string.IsNullOrEmpty(dt.Rows[0]["SHASHU_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString()))
                {
                    this.form.SHASHU_CD.Text = string.Empty;
                    this.form.SHASHU_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.SHASHU_CD.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                    this.form.SHASHU_NAME.Text = dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                }
                //運転者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.UNTENSHA_CD.Text = string.Empty;
                    this.form.UNTENSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.UNTENSHA_CD.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                    this.form.UNTENSHA_NAME.Text = dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString();
                }

                // 空車重量
                // 受付読み込み時はイベントを発生させない
                this.form.KUUSHA_JYURYO.TextChanged -= new EventHandler(this.form.KUUSHA_JYURYO_TextChanged);
                this.form.KUUSHA_JYURYO.Text = string.Empty;
                M_SHARYOU[] sharyouEntitys = null;
                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END
                if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                {
                    if (!sharyouEntitys[0].KUUSHA_JYURYO.IsNull)
                    {
                        this.form.KUUSHA_JYURYO.Text = sharyouEntitys[0].KUUSHA_JYURYO.ToString();
                    }
                }
                this.form.KUUSHA_JYURYO.TextChanged += new EventHandler(this.form.KUUSHA_JYURYO_TextChanged);

                //マニ種類CD
                if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString()))
                {
                    this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.MANIFEST_SHURUI_CD.Text = dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString();
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString();
                }
                //マニ手配CD
                if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString()))
                {
                    this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.MANIFEST_TEHAI_CD.Text = dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString();
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString();
                }
                //計量番号
                this.form.KEIRYOU_NUMBER.Text = string.Empty;
                this.form.KeiryouNumber = -1;

                int maxRows = this.form.gcMultiRow1.Rows.Count;
                int dataCount = 0;
                for (int i = (maxRows - 1); i < (maxRows - 1) + dt.Rows.Count; i++)
                {
                    // DETAIL_SYSTEM_IDが0（SQLでnullは0としている）の場合は明細なし
                    if (int.Parse(dt.Rows[dataCount]["DETAIL_SYSTEM_ID"].ToString()) > 0)
                    {
                        // 行追加
                        this.form.gcMultiRow1.Rows.Add();

                        this.form.gcMultiRow1.Rows[i]["ROW_NO"].Value = dt.Rows[dataCount]["DETAIL_ROW_NO"].ToString();
                        //品名CD
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_NAME_RYAKU"].ToString()))
                        {
                            this.form.gcMultiRow1.Rows[i]["HINMEI_CD"].Value = "";
                            this.form.gcMultiRow1.Rows[i]["HINMEI_NAME"].Value = "";
                            this.form.gcMultiRow1.Rows[i][CELL_NAME_HINMEI_ZEI_KBN_CD].Value = "";
                        }
                        else
                        {
                            this.form.gcMultiRow1.Rows[i]["HINMEI_CD"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_CD"];
                            this.form.gcMultiRow1.Rows[i]["HINMEI_NAME"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_NAME_RYAKU"];
                            this.form.gcMultiRow1.Rows[i][CELL_NAME_HINMEI_ZEI_KBN_CD].Value = dt.Rows[dataCount]["DETAIL_HINMEI_ZEI_KBN_CD"];

                            // 20150427 受付から横連携した時、品名の在庫品名・比率を設定する Start
                            this.ZaikoHinmeiHuriwakesSearch(this.form.gcMultiRow1.Rows[i]);
                            // 20150427 受付から横連携した時、品名の在庫品名・比率を設定する End
                        }
                        //伝票区分CD
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"].ToString()))
                        {
                            this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_CD"].Value = "";
                            this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_NAME"].Value = "";
                        }
                        else
                        {
                            this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_CD"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"];
                            this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_NAME"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"];
                        }
                        //数量
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_SUURYOU"].ToString()))
                        {
                            this.form.gcMultiRow1.Rows[i]["SUURYOU"].Value = "";
                        }
                        else
                        {
                            this.form.gcMultiRow1.Rows[i]["SUURYOU"].Value = dt.Rows[dataCount]["DETAIL_SUURYOU"];
                        }
                        //単位CD
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_UNIT_NAME_RYAKU"].ToString()))
                        {
                            this.form.gcMultiRow1.Rows[i]["UNIT_CD"].Value = "";
                            this.form.gcMultiRow1.Rows[i]["UNIT_NAME_RYAKU"].Value = "";
                        }
                        else
                        {
                            //string tempUnit = String.Format("{0:D3}", int.Parse(dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString()));    // No.2715
                            //this.form.gcMultiRow1.Rows[i]["UNIT_CD"].Value = tempUnit;    // No.2715
                            this.form.gcMultiRow1.Rows[i]["UNIT_CD"].Value = dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString();    // No.2715
                            this.form.gcMultiRow1.Rows[i]["UNIT_NAME_RYAKU"].Value = dt.Rows[dataCount]["DETAIL_UNIT_NAME_RYAKU"];
                        }
                        //単価
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_TANKA"].ToString()))
                        {
                            this.form.gcMultiRow1.Rows[i]["TANKA"].Value = "";
                        }
                        else
                        {
                            this.form.gcMultiRow1.Rows[i]["TANKA"].Value = dt.Rows[dataCount]["DETAIL_TANKA"];
                        }
                        //金額
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_KINGAKU"].ToString()))
                        {
                            this.form.gcMultiRow1.Rows[i]["KINGAKU"].Value = "";
                        }
                        else
                        {
                            this.form.gcMultiRow1.Rows[i]["KINGAKU"].Value = dt.Rows[dataCount]["DETAIL_KINGAKU"];
                        }
                        //明細備考
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_MEISAI_BIKOU"].ToString()))
                        {
                            this.form.gcMultiRow1.Rows[i]["MEISAI_BIKOU"].Value = "";
                        }
                        else
                        {
                            this.form.gcMultiRow1.Rows[i]["MEISAI_BIKOU"].Value = dt.Rows[dataCount]["DETAIL_MEISAI_BIKOU"];
                        }

                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_TANKA"].ToString()))
                        {
                            this.SearchAndCalcForUnit(false, this.form.gcMultiRow1.Rows[i]);
                        }
                    }
                    dataCount += 1;
                }
                this.ResetTankaCheck(); // MAILAN #158992 START

                // 行番号採番
                if (!this.NumberingRowNo())
                {
                    return false;
                }

                // 設定後処理
                SettingsAfterDisplayData(dt);

                // 最後に空車重量のセットを行う
                if (this.form.gcMultiRow1.Rows.Count > 0)
                {
                    this.form.KUUSHA_JYURYO.TextChanged -= new EventHandler(this.form.KUUSHA_JYURYO_TextChanged);
                    var kuushaJuuryou = this.form.KUUSHA_JYURYO.Text;
                    this.form.gcMultiRow1.Rows[0][CELL_NAME_EMPTY_JYUURYOU].Value = kuushaJuuryou;
                    this.form.KUUSHA_JYURYO.TextChanged += new EventHandler(this.form.KUUSHA_JYURYO_TextChanged);
                }

                //20150925 hoanghm #13120 start
                //// 新規行追加
                //this.form.SetEmptyAddNewRow();
                //20150925 hoanghm #13120 end

                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    this.form.SetIchranReadOnly(row.Index);
                }

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("GetUketsukeNumber", ex1);
                    msgLogic.MessageBoxShow("E093", "");
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("GetUketsukeNumber", ex);
                    msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 計量番号からデータ取得
        /// </summary>
        internal bool GetKeiryouNumber()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.KEIRYOU_NUMBER.Text))
                {
                    return true;
                }

                DataTable dt = this.accessor.GetKeiryou(this.form.KEIRYOU_NUMBER.Text);
                if (dt.Rows.Count > 0)
                {
                    //拠点CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["KYOTEN_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["KYOTEN_NAME_RYAKU"].ToString()))
                    {
                        this.headerForm.KYOTEN_CD.Text = string.Empty;
                        this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        string tempKyoten = String.Format("{0:D2}", int.Parse(dt.Rows[0]["KYOTEN_CD"].ToString()));
                        this.headerForm.KYOTEN_CD.Text = tempKyoten;
                        this.headerForm.KYOTEN_NAME_RYAKU.Text = dt.Rows[0]["KYOTEN_NAME_RYAKU"].ToString();
                    }
                    //伝票日付、売上日付、支払日付
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DENPYOU_DATE"].ToString()))
                    {
                        this.form.DENPYOU_DATE.Text = dt.Rows[0]["DENPYOU_DATE"].ToString();
                        this.form.URIAGE_DATE.Text = dt.Rows[0]["DENPYOU_DATE"].ToString();
                        this.form.SHIHARAI_DATE.Text = dt.Rows[0]["DENPYOU_DATE"].ToString();

                        // 消費税率の設定
                        DateTime uriageDate = this.footerForm.sysDate.Date;
                        if (DateTime.TryParse(this.form.URIAGE_DATE.Text, out uriageDate))
                        {
                            var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageDate);
                            if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                            {
                                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                            }
                        }

                        DateTime shiharaiDate = this.footerForm.sysDate.Date;
                        if (DateTime.TryParse(this.form.SHIHARAI_DATE.Text, out shiharaiDate))
                        {
                            var shiharaiShouhizeiRate = this.accessor.GetShouhizeiRate(shiharaiDate);
                            if (!shiharaiShouhizeiRate.SHOUHIZEI_RATE.IsNull)
                            {
                                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = shiharaiShouhizeiRate.SHOUHIZEI_RATE.ToString();
                            }
                        }
                    }
                    //取引先CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                    {
                        this.form.TORIHIKISAKI_CD.Text = string.Empty;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_CD.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    }
                    //業者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.GYOUSHA_CD.Text = string.Empty;
                        this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GYOUSHA_CD.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                        this.form.GYOUSHA_NAME_RYAKU.Text = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    //現場CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GENBA_NAME_RYAKU"].ToString()))
                    {
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        strGenbaName = string.Empty;   // No.3279
                    }
                    else
                    {
                        this.form.GENBA_CD.Text = dt.Rows[0]["GENBA_CD"].ToString();
                        this.form.GENBA_NAME_RYAKU.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();

                        // No.3279-->
                        bool catchErr = false;
                        M_GENBA entGenba = accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (catchErr)
                        {
                            return false;
                        }
                        if (entGenba != null)
                        {
                            // 諸口区分チェック
                            if (entGenba.SHOKUCHI_KBN.IsTrue)
                            {
                                strGenbaName = this.form.GENBA_NAME_RYAKU.Text;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(entGenba.GENBA_NAME1))
                                {
                                    strGenbaName = entGenba.GENBA_NAME1 + entGenba.GENBA_NAME2;
                                }
                                else
                                {
                                    strGenbaName = "";
                                }
                            }

                            // 要検収のデフォルト値を設定
                            if (!entGenba.KENSHU_YOUHI.IsNull)
                            {
                                this.form.KENSHU_MUST_KBN.Checked = entGenba.KENSHU_YOUHI.Value;
                            }
                        }
                        // No.3279<--
                    }
                    //運搬業者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                        this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_CD.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        this.form.UNPAN_GYOUSHA_NAME.Text = dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    //荷積業者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                        this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.NIZUMI_GYOUSHA_CD.Text = dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                        this.form.NIZUMI_GYOUSHA_NAME.Text = dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    //荷積現場CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString()))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.NIZUMI_GENBA_CD.Text = dt.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                        this.form.NIZUMI_GENBA_NAME.Text = dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString();
                    }
                    //営業担当者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EIGYOU_TANTOUSHA_CD.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString();
                    }
                    //入力担当者CD
                    M_SHAIN shainDto = this.accessor.GetShain(SystemProperty.Shain.CD);
                    if (shainDto != null)
                    {
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = shainDto.SHAIN_CD;
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = shainDto.SHAIN_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                    }
                    //車輌CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_CD"].ToString()))
                    {
                        //コードが無しの場合は未入力状態
                        this.form.SHARYOU_CD.Text = "";
                        this.form.SHARYOU_NAME_RYAKU.Text = "";
                    }
                    else if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString()))
                    {
                        this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                        this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                        this.CheckSharyouUketsuke_NASHI(dt);
                    }
                    else
                    {
                        this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                        this.form.SHARYOU_NAME_RYAKU.Text = dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                    }
                    //車種CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["SHASHU_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString()))
                    {
                        this.form.SHASHU_CD.Text = string.Empty;
                        this.form.SHASHU_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.SHASHU_CD.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                        this.form.SHASHU_NAME.Text = dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                    }
                    //運転者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.UNTENSHA_CD.Text = string.Empty;
                        this.form.UNTENSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.UNTENSHA_CD.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                        this.form.UNTENSHA_NAME.Text = dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString();
                    }
                    //人数
                    this.form.NINZUU_CNT.Text = string.Empty;
                    //形態区分
                    if (string.IsNullOrEmpty(dt.Rows[0]["KEITAI_KBN_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["KEITAI_KBN_NAME_RYAKU"].ToString()))
                    {
                        this.form.KEITAI_KBN_CD.Text = string.Empty;
                        this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.KEITAI_KBN_CD.Text = dt.Rows[0]["KEITAI_KBN_CD"].ToString();
                        this.form.KEITAI_KBN_NAME_RYAKU.Text = dt.Rows[0]["KEITAI_KBN_NAME_RYAKU"].ToString();
                    }
                    // 台貫
                    this.form.DAIKAN_KBN.Text = SalesPaymentConstans.DAIKAN_KBN_JISHA;
                    this.form.DAIKAN_KBN_NAME.Text = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(this.form.DAIKAN_KBN.Text.ToString()));
                    //マニ種類CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString()))
                    {
                        this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                        this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.MANIFEST_SHURUI_CD.Text = dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString();
                        this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString();
                    }
                    //マニ手配CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString()))
                    {
                        this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                        this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.MANIFEST_TEHAI_CD.Text = dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString();
                        this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString();
                    }
                    //受付番号
                    this.form.UKETSUKE_NUMBER.Text = string.Empty;
                    this.form.UketukeNumber = -1;
                    //伝票備考
                    if (string.IsNullOrEmpty(dt.Rows[0]["DENPYOU_BIKOU"].ToString()))
                    {
                        this.form.DENPYOU_BIKOU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.DENPYOU_BIKOU.Text = dt.Rows[0]["DENPYOU_BIKOU"].ToString();
                    }
                    //滞留備考
                    if (string.IsNullOrEmpty(dt.Rows[0]["TAIRYUU_BIKOU"].ToString()))
                    {
                        this.form.TAIRYUU_BIKOU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.TAIRYUU_BIKOU.Text = dt.Rows[0]["TAIRYUU_BIKOU"].ToString();
                    }

                    int maxRows = this.form.gcMultiRow1.Rows.Count;
                    int dataCount = 0;
                    for (int i = (maxRows - 1); i < (maxRows - 1) + dt.Rows.Count; i++)
                    {
                        // DETAIL_SYSTEM_IDが0（SQLでnullは0としている）の場合は明細なし
                        if (int.Parse(dt.Rows[dataCount]["DETAIL_SYSTEM_ID"].ToString()) > 0)
                        {
                            // 行追加
                            this.form.gcMultiRow1.Rows.Add();

                            this.form.gcMultiRow1.Rows[i]["ROW_NO"].Value = dt.Rows[dataCount]["DETAIL_ROW_NO"].ToString();
                            //総重量
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_STACK_JYUURYOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["STACK_JYUURYOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["STACK_JYUURYOU"].Value = dt.Rows[dataCount]["DETAIL_STACK_JYUURYOU"].ToString();
                            }
                            //空車重量
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_EMPTY_JYUURYOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["EMPTY_JYUURYOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["EMPTY_JYUURYOU"].Value = dt.Rows[dataCount]["DETAIL_EMPTY_JYUURYOU"].ToString();
                            }
                            //割振重量
                            this.form.gcMultiRow1.Rows[i]["WARIFURI_JYUURYOU"].Value = "";
                            //割振%
                            this.form.gcMultiRow1.Rows[i]["WARIFURI_PERCENT"].Value = "";
                            //調整重量
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_CHOUSEI_JYUURYOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["CHOUSEI_JYUURYOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["CHOUSEI_JYUURYOU"].Value = dt.Rows[dataCount]["DETAIL_CHOUSEI_JYUURYOU"].ToString();
                            }
                            //調整%
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_CHOUSEI_PERCENT"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["CHOUSEI_PERCENT"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["CHOUSEI_PERCENT"].Value = dt.Rows[dataCount]["DETAIL_CHOUSEI_PERCENT"].ToString();
                            }
                            //容器CD
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_YOUKI_CD"].ToString())
                                || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_YOUKI_NAME_RYAKU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["YOUKI_CD"].Value = "";
                                this.form.gcMultiRow1.Rows[i]["YOUKI_NAME_RYAKU"].Value = "";
                            }
                            else
                            {
                                string tempYouki = String.Format("{0:D3}", int.Parse(dt.Rows[dataCount]["DETAIL_YOUKI_CD"].ToString()));
                                this.form.gcMultiRow1.Rows[i]["YOUKI_CD"].Value = tempYouki;
                                this.form.gcMultiRow1.Rows[i]["YOUKI_NAME_RYAKU"].Value = dt.Rows[dataCount]["DETAIL_YOUKI_NAME_RYAKU"].ToString();
                            }
                            //容器数量
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_YOUKI_SUURYOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["YOUKI_SUURYOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["YOUKI_SUURYOU"].Value = dt.Rows[dataCount]["DETAIL_YOUKI_SUURYOU"].ToString();
                            }
                            //容器重量
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_YOUKI_JYUURYOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["YOUKI_JYUURYOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["YOUKI_JYUURYOU"].Value = dt.Rows[dataCount]["DETAIL_YOUKI_JYUURYOU"].ToString();
                            }
                            //伝票区分CD
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"].ToString())
                                || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_CD"].Value = "";
                                this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_NAME"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_CD"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"];
                                this.form.gcMultiRow1.Rows[i]["DENPYOU_KBN_NAME"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"];
                            }
                            //品名CD
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_CD"].ToString())
                                || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_NAME"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["HINMEI_CD"].Value = "";
                                this.form.gcMultiRow1.Rows[i]["HINMEI_NAME"].Value = "";
                                this.form.gcMultiRow1.Rows[i]["HINMEI_ZEI_KBN_CD"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["HINMEI_CD"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_CD"];
                                this.form.gcMultiRow1.Rows[i]["HINMEI_NAME"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_NAME"];
                                this.form.gcMultiRow1.Rows[i]["HINMEI_ZEI_KBN_CD"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_ZEI_KBN_CD"];

                                // 20150427 計量から横連携した時、品名の在庫品名・比率を設定する Start
                                this.ZaikoHinmeiHuriwakesSearch(this.form.gcMultiRow1.Rows[i]);
                                // 20150427 計量から横連携した時、品名の在庫品名・比率を設定する End
                            }
                            //正味重量
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_NET_JYUURYOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["NET_JYUURYOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["NET_JYUURYOU"].Value = dt.Rows[dataCount]["DETAIL_NET_JYUURYOU"].ToString();
                            }
                            //荷姿数量
                            this.form.gcMultiRow1.Rows[i]["NISUGATA_SUURYOU"].Value = "";
                            //荷姿単位CD
                            this.form.gcMultiRow1.Rows[i]["NISUGATA_UNIT_CD"].Value = "";
                            this.form.gcMultiRow1.Rows[i]["NISUGATA_NAME_RYAKU"].Value = "";
                            //明細備考
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["MEISAI_BIKOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["MEISAI_BIKOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["MEISAI_BIKOU"].Value = dt.Rows[dataCount]["MEISAI_BIKOU"].ToString();
                            }
                            // 数量
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_SUURYOU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["SUURYOU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["SUURYOU"].Value = dt.Rows[dataCount]["DETAIL_SUURYOU"];
                            }
                            // 単位CD
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_UNIT_NAME_RYAKU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["UNIT_CD"].Value = "";
                                this.form.gcMultiRow1.Rows[i]["UNIT_NAME_RYAKU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["UNIT_CD"].Value = dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString();
                                this.form.gcMultiRow1.Rows[i]["UNIT_NAME_RYAKU"].Value = dt.Rows[dataCount]["DETAIL_UNIT_NAME_RYAKU"];
                            }
                            // 単価
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_TANKA"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["TANKA"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["TANKA"].Value = dt.Rows[dataCount]["DETAIL_TANKA"];
                            }
                            // 金額
                            decimal kingaku = 0;
                            decimal hinmeiKingaku = 0;
                            decimal.TryParse(Convert.ToString(dt.Rows[dataCount]["DETAIL_KINGAKU"]), out kingaku);
                            decimal.TryParse(Convert.ToString(dt.Rows[dataCount]["DETAIL_HINMEI_KINGAKU"]), out hinmeiKingaku);
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_KINGAKU"].ToString()) &&
                                string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_KINGAKU"].ToString()))
                            {
                                this.form.gcMultiRow1.Rows[i]["KINGAKU"].Value = "";
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows[i]["KINGAKU"].Value = kingaku + hinmeiKingaku;
                            }
                            // マニフェスト
                            this.form.gcMultiRow1.Rows[i]["MANIFEST_ID"].Value = "";
                        }

                        dataCount += 1;
                    }

                    // 行番号採番
                    if (!this.NumberingRowNo())
                    {
                        return false;
                    }

                    // 設定後処理
                    SettingsAfterDisplayData(dt);

                    // 明細項目の活性制御
                    foreach (var row in this.form.gcMultiRow1.Rows)
                    {
                        // 総重量、空車重量、割振、調整項目の活性制御
                        this.WarifuriReadOnlyCheck(row);
                        // 数量の活性制御（単位kgの品名数量設定）
                        if (!SetHinmeiSuuryou(LogicClass.CELL_NAME_UNIT_CD, row, true))
                        {
                            continue;
                        }
                        // 単価と金額の活性制御
                        this.form.SetIchranReadOnly(row.Index);
                    }

                }
                // 20140605 katen 不具合No.4654 start‏
                else
                {
                    // データなし

                    // メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");

                    // 入力計量番号クリア
                    this.form.KEIRYOU_NUMBER.Text = "";

                    //フォーカスを計量番号にする
                    this.form.KEIRYOU_NUMBER.Focus();

                    // 20140609 katen 不具合No.4654 start‏
                    this.form.KeiryouNumber = -1;
                    // 20140609 katen 不具合No.4654 end‏

                }
                // 20140605 katen 不具合No.4653 end‏
                ret = true;

            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("GetKeiryouNumber", ex1);
                    msgLogic.MessageBoxShow("E093", "");
                }
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("GetKeiryouNumber", ex);
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }
        /// <summary>
        /// 番号入力データ表示後処理
        /// </summary>
        internal void SettingsAfterDisplayData(DataTable dt)
        {
            LogUtility.DebugMethodStart();

            // 諸口区分チェック
            if (dt.Rows[0]["TORIHIKISAKI_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                // 取引先名編集可
                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = true;
                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");    // No.3822
            }
            if (dt.Rows[0]["NIZUMI_GYOUSHA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                //this.form.NIZUMI_GYOUSHA_NAME.TabStop = true;
                this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");    // No.3822
            }
            if (dt.Rows[0]["NIZUMI_GENBA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                //this.form.NIZUMI_GENBA_NAME.TabStop = true;
                this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
            }
            if (dt.Rows[0]["UNPAN_GYOUSHA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
            }
            if (dt.Rows[0]["GYOUSHA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                // 業者名編集可
                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
            }
            if (dt.Rows[0]["GENBA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                // 現場名編集可
                this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                //this.form.GENBA_NAME_RYAKU.TabStop = true;
                this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
            }
            // 取引先セット時
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                //エンティティを更新するため請求支払のチェックはそのまま呼ぶ

                // 請求締日チェック
                this.CheckSeikyuuShimebi();

                // 支払い締日チェック
                this.CheckShiharaiShimebi();

                //取引区分チェック
                this.CheckTorihikiKBN();
            }

            // 明細の計算処理
            // jyuuryouDtoを初期化
            this.SetJyuuryouDataToDtoList();

            // 合計系金額計算
            if (!this.CalcTotalValues())
            {
                throw new Exception("");
            }

            LogUtility.DebugMethodEnd();
        }
        internal void CheckSharyouUketsuke_NASHI(DataTable dt)
        {
            // 車輌名を編集可
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
            // 自由入力可能であるため車輌名の色を変更
            this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
            this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
            // マスタに存在しない場合、ユーザに車輌名を自由入力させる
            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
        }

        // No.4578-->
        // 20150415 既存在庫明細処理（仮）削除(修正後のG051と同様修正) Start
        ///// <summary>
        ///// TODO: 在庫明細入力戻り値取得用（仮）
        ///// </summary>
        ///// <param name="zaikoHinmeiCD"></param>
        ///// <param name="zaikoHinmeiName"></param>
        ///// <param name="zaikoUnitName"></param>
        ///// <param name="zaikoTanka"></param>
        ///// <param name="zaikoKingaku"></param>
        //static void GetReturnZaiko(string zaikoHinmeiCD, string zaikoHinmeiName
        //            , string zaikoUnitName, decimal zaikoTanka, decimal zaikoKingaku)
        //{

        //    // TODO:戻りが1件しか来ていないため修正要請中。

        //    //LogUtility.DebugMethodStart();

        //    //this.dto.zaikoUkeireDetailList = (List<T_ZAIKO_UKEIRE_DETAIL>)val.GetValue(callForm, null);

        //    ////Entity格納
        //    //if (this.dto.zaikoUkeireDetailList == null)
        //    //{
        //    //    return;
        //    //}
        //    //else if (this.dto.zaikoUkeireDetailList.Count > 1)
        //    //{
        //    //    //金額を合計
        //    //    SqlDecimal goukeiKingaku = 0;
        //    //    foreach (T_ZAIKO_UKEIRE_DETAIL entity in this.dto.zaikoUkeireDetailList)
        //    //    {
        //    //        if (!entity.KINGAKU.Equals(SqlDecimal.Null))
        //    //        {
        //    //            goukeiKingaku += entity.KINGAKU;
        //    //        }
        //    //    }

        //    //    //Entityの数が2以上
        //    //    MultiZaikoKakunou(row,goukeiKingaku);
        //    //}
        //    //else if (this.dto.zaikoUkeireDetailList.Count == 1)
        //    //{
        //    //    //Entityの数が1
        //    //    SimpleZaikoKakunou(row, this.dto.zaikoUkeireDetailList[0]);
        //    //}

        //    //LogUtility.DebugMethodEnd();
        //}
        // 20150415 既存在庫明細処理（仮）削除 End

        // 20150415 既存在庫明細画面遷移処理削除(修正後のG051と同様修正) Start
        //internal void ZaikoGamenSeni(Row row)
        //{
        //    LogUtility.DebugMethodStart(row);

        //    //引数の用意
        //    string hinmeiCD = "";
        //    string hinmeiName = "";
        //    decimal suuryou = 0;
        //    decimal netJuuryou = 0;
        //    decimal nisugataSuuryou = 0;
        //    string nisugataUnitName = "";
        //    decimal kingaku = 0;

        //    if (row.Cells["HINMEI_CD"].Value != null)
        //    {
        //        hinmeiCD = row.Cells["HINMEI_CD"].Value.ToString();
        //    }
        //    if (row.Cells["HINMEI_NAME"].Value != null)
        //    {
        //        hinmeiName = row.Cells["HINMEI_NAME"].Value.ToString();
        //    }
        //    if (row.Cells["SUURYOU"].Value != null)
        //    {
        //        suuryou = decimal.Parse(row.Cells["SUURYOU"].Value.ToString());
        //    }
        //    if (row.Cells["NET_JYUURYOU"].Value != null)
        //    {
        //        netJuuryou = decimal.Parse(row.Cells["NET_JYUURYOU"].Value.ToString());
        //    }
        //    if (row.Cells["NISUGATA_SUURYOU"].Value != null)
        //    {
        //        nisugataSuuryou = decimal.Parse(row.Cells["NISUGATA_SUURYOU"].Value.ToString());
        //    }
        //    if (row.Cells["NISUGATA_NAME_RYAKU"].Value != null)
        //    {
        //        nisugataUnitName = row.Cells["NISUGATA_NAME_RYAKU"].Value.ToString();
        //    }
        //    if (row.Cells["KINGAKU"].Value != null)
        //    {
        //        kingaku = decimal.Parse(row.Cells["KINGAKU"].Value.ToString());
        //    }

        //    List<T_ZAIKO_SHUKKA_DETAIL> lstZaikoShukka = dto.rowZaikoShukkaDetails[row];

        //    // 在庫画面(G165)をモーダル表示
        //    var assembly = Assembly.LoadFrom("ZaikoMeisaiNyuuryoku.dll");
        //    var callHeader = (HeaderBaseForm)assembly.CreateInstance(
        //            "Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.APP.F18_G165HeaderForm",
        //            false,
        //            BindingFlags.CreateInstance,
        //            null,
        //            null,
        //            null,
        //            null
        //    );
        //    var callForm = (SuperForm)assembly.CreateInstance(
        //            "Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.APP.F18_G165Form",
        //            false,
        //            BindingFlags.CreateInstance,
        //            null,
        //            new object[] { hinmeiCD, hinmeiName, suuryou, netJuuryou, nisugataSuuryou, nisugataUnitName, kingaku, lstZaikoShukka },
        //            null,
        //            null
        //          );
        //    if (callForm.IsDisposed)
        //    {
        //        return;
        //    }

        //    var businessForm = new BusinessBaseForm(callForm, callHeader);
        //    var ret = businessForm.ShowDialog();
        //    businessForm.Dispose();

        //    ////戻り値
        //    PropertyInfo val = callForm.GetType().GetProperty("RetZaikoShukkaDetail");

        //    List<T_ZAIKO_SHUKKA_DETAIL> retZaikoEntity = new List<T_ZAIKO_SHUKKA_DETAIL>();
        //    retZaikoEntity = (List<T_ZAIKO_SHUKKA_DETAIL>)val.GetValue(callForm, null);

        //    //Entity格納
        //    if (retZaikoEntity == null)
        //    {
        //        return;
        //    }
        //    else if (retZaikoEntity.Count > 1)
        //    {
        //        //金額を合計
        //        SqlDecimal goukeiKingaku = 0;
        //        foreach (T_ZAIKO_SHUKKA_DETAIL entity in retZaikoEntity)
        //        {
        //            if (!entity.KINGAKU.Equals(SqlDecimal.Null))
        //            {
        //                goukeiKingaku += entity.KINGAKU;
        //            }
        //        }

        //        //Entityの数が2以上
        //        MultiZaikoKakunou(row, goukeiKingaku);
        //    }
        //    else if (retZaikoEntity.Count == 1)
        //    {
        //        //Entityの数が1
        //        SimpleZaikoKakunou(row, retZaikoEntity[0]);
        //    }

        //    //データ保存用
        //    this.dto.rowZaikoShukkaDetails[row] = retZaikoEntity;

        //    LogUtility.DebugMethodEnd();
        //}

        //internal void SimpleZaikoKakunou(Row row, T_ZAIKO_SHUKKA_DETAIL entity)
        //{
        //    LogUtility.DebugMethodStart(row, entity);

        //    //在庫品名CD
        //    row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value = entity.ZAIKO_HINMEI_CD;
        //    //在庫品名
        //    row.Cells[CELL_NAME_ZAIKO_HINMEI_RYAKU].Value = this.accessor.GetZaikoHinmei(entity.ZAIKO_HINMEI_CD.ToString());
        //    //在庫単位CD・在庫単位名
        //    row.Cells[CELL_NAME_ZAIKO_UNIT_CD].Value = this.zaikoUnitCd;
        //    row.Cells[CELL_NAME_ZAIKO_UNIT_NAME].Value = "kg";
        //    //在庫単価
        //    row.Cells[CELL_NAME_ZAIKO_TANKA].Value = entity.TANKA;
        //    //在庫金額
        //    row.Cells[CELL_NAME_ZAIKO_KINGAKU].Value = entity.KINGAKU;

        //    LogUtility.DebugMethodEnd();
        //}

        //internal void MultiZaikoKakunou(Row row, SqlDecimal goukeiKingaku)
        //{
        //    LogUtility.DebugMethodStart(row, goukeiKingaku);

        //    //在庫品名CD
        //    row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value = "";
        //    //在庫品名
        //    row.Cells[CELL_NAME_ZAIKO_HINMEI_RYAKU].Value = "複数在庫";
        //    //在庫単位CD・在庫単位名
        //    row.Cells[CELL_NAME_ZAIKO_UNIT_CD].Value = this.zaikoUnitCd;
        //    row.Cells[CELL_NAME_ZAIKO_UNIT_NAME].Value = "kg";
        //    //在庫単価
        //    row.Cells[CELL_NAME_ZAIKO_TANKA].Value = "";
        //    //在庫金額
        //    row.Cells[CELL_NAME_ZAIKO_KINGAKU].Value = goukeiKingaku;

        //    LogUtility.DebugMethodEnd();
        //}
        // 20150415 既存在庫明細画面遷移処理削除 End

        // 20150415 在庫品名振分画面遷移処理(修正後のG051からコピー) Start
        /// <summary>
        /// 在庫品名振分画面遷移
        /// </summary>
        /// <param name="row"></param>
        internal bool ZaikoHinmeiHuriwakesGamenSeni(Row row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(row);

                // 在庫管理の場合のみ設定する
                if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
                {
                    // 引数の用意
                    short denshuKbnCd = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA.Value; // 出荷
                    string hinmeiCd = string.Empty;
                    string hinmeiName = string.Empty;
                    decimal netJuuryou = decimal.Zero;

                    DataTable zaikoTable = new DataTable();
                    zaikoTable.Columns.AddRange(new DataColumn[] {
                    new DataColumn("ZAIKO_HINMEI_CD", typeof(string)),
                    new DataColumn("ZAIKO_HINMEI_NAME", typeof(string)),
                    new DataColumn("ZAIKO_HIRITSU", typeof(short)),
                    new DataColumn("ZAIKO_TANKA", typeof(decimal)),
                    new DataColumn("ZAIKO_RYOU", typeof(decimal))
                });

                    if (!Convert.IsDBNull(row.Cells["HINMEI_CD"].Value) && row.Cells["HINMEI_CD"].Value != null)
                    {
                        hinmeiCd = row.Cells["HINMEI_CD"].Value.ToString();
                    }
                    if (!Convert.IsDBNull(row.Cells["HINMEI_NAME"].Value) && row.Cells["HINMEI_NAME"].Value != null)
                    {
                        hinmeiName = row.Cells["HINMEI_NAME"].Value.ToString();
                    }
                    if (!Convert.IsDBNull(row.Cells["NET_JYUURYOU"].Value) && row.Cells["NET_JYUURYOU"].Value != null)
                    {
                        netJuuryou = Convert.ToDecimal(row.Cells["NET_JYUURYOU"].Value);
                    }
                    foreach (var entity in dto.rowZaikoHinmeiHuriwakes[row])
                    {
                        // 20150421 在庫量再計算判定修正(有価在庫不具合一覧107) Start
                        //var zaikoRyou = decimal.Zero;
                        //if (entity.ZAIKO_RYOU.IsNull)
                        //{
                        //    // 正味重量を利用して、在庫量を計算する。
                        //    // 画面遷移するため、元の明細データを変更しない。
                        //    zaikoRyou = netJuuryou * Convert.ToDecimal(entity.ZAIKO_HIRITSU.Value) / 100;
                        //}
                        //else
                        //{
                        //    zaikoRyou = entity.ZAIKO_RYOU.Value;
                        //}
                        zaikoTable.Rows.Add(
                            entity.ZAIKO_HINMEI_CD,
                            entity.ZAIKO_HINMEI_NAME,
                            entity.ZAIKO_HIRITSU.Value,
                            entity.ZAIKO_TANKA.Value,
                            netJuuryou * Convert.ToDecimal(entity.ZAIKO_HIRITSU.Value) / 100
                            );
                        // 20150421 在庫量再計算判定修正(有価在庫不具合一覧107) End
                    }

                    var args = new object[] { denshuKbnCd, hinmeiCd, hinmeiName, netJuuryou, zaikoTable };
                    // 在庫画面(G633)をモーダル表示
                    var assembly = Assembly.LoadFrom("ZaikoHinmeiHuriwake.dll");
                    var shougunForm = assembly.CreateInstance(
                        "Shougun.Core.Stock.ZaikoHinmeiHuriwake.G633",
                        false,
                        BindingFlags.CreateInstance,
                        null,
                        null,
                        null,
                        null
                        );
                    using (var businessForm = (shougunForm as IShougunForm).CreateForm(args))
                    {
                        // 画面表示
                        if (businessForm.ShowDialog() == DialogResult.OK)
                        {
                            // 戻り値
                            var returnParam = shougunForm.GetType().GetProperty("ZaikoTable");
                            var returnZaikoHinmeiHuriwakes = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                            foreach (var dr in (returnParam.GetValue(shougunForm, null) as DataTable).AsEnumerable())
                            {
                                var entity = new T_ZAIKO_HINMEI_HURIWAKE();

                                // 20150504 「単価」を取得できないシステムエラーの一時対応(在庫不具合一覧235) Start
                                entity.ZAIKO_HINMEI_CD = Convert.IsDBNull(dr["ZAIKO_HINMEI_CD"]) ? string.Empty : Convert.ToString(dr["ZAIKO_HINMEI_CD"]);
                                entity.ZAIKO_HINMEI_NAME = Convert.IsDBNull(dr["ZAIKO_HINMEI_NAME"]) ? string.Empty : Convert.ToString(dr["ZAIKO_HINMEI_NAME"]);
                                entity.ZAIKO_HIRITSU = Convert.IsDBNull(dr["ZAIKO_HIRITSU"]) ? (short)0 : Convert.ToInt16(dr["ZAIKO_HIRITSU"]);
                                entity.ZAIKO_TANKA = Convert.IsDBNull(dr["ZAIKO_TANKA"]) ? decimal.Zero : Convert.ToDecimal(dr["ZAIKO_TANKA"]);
                                entity.ZAIKO_RYOU = Convert.IsDBNull(dr["ZAIKO_RYOU"]) ? decimal.Zero : Convert.ToDecimal(dr["ZAIKO_RYOU"]);
                                // 20150504 「単価」を取得できないシステムエラーの一時対応(在庫不具合一覧235) End

                                returnZaikoHinmeiHuriwakes.Add(entity);
                            }

                            // データ保存用
                            this.dto.rowZaikoHinmeiHuriwakes[row] = returnZaikoHinmeiHuriwakes;
                            if (!this.ZaikoHinmeiKakunou(row))
                            {
                                throw new Exception("");
                            }
                        }

                        businessForm.Dispose();
                    }
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoHinmeiHuriwakesGamenSeni", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeiHuriwakesGamenSeni", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }
        // 20150415 在庫品名振分画面遷移処理 End

        // 20150411 在庫品名振分検索処理 Start
        /// <summary>
        /// 在庫品名振分検索
        /// </summary>
        /// <param name="row"></param>
        /// <remarks>品名変更後、複数在庫品名振分検索</remarks>
        internal void ZaikoHinmeiHuriwakesSearch(Row row)
        {
            LogUtility.DebugMethodStart(row);

            // 在庫管理の場合のみ設定する
            if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
            {
                if (row != null &&
                    !Convert.IsDBNull(row.Cells[CELL_NAME_HINMEI_CD].Value) &&
                    row.Cells[CELL_NAME_HINMEI_CD].Value != null &&
                    !string.IsNullOrEmpty(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                {
                    var zaikoHiritsus = this.accessor.GetZaikoHiritsus(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                    var zaikoHinmeiHuriwakes = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                    if (zaikoHiritsus != null)
                    {
                        foreach (var dr in zaikoHiritsus.AsEnumerable())
                        {
                            var entity = new T_ZAIKO_HINMEI_HURIWAKE();

                            // 20150504 「単価」を取得できないシステムエラーの一時対応(在庫不具合一覧235) Start
                            //          在庫比率は適用期間内で、在庫品名は適用期間外の場合
                            entity.ZAIKO_HINMEI_CD = Convert.IsDBNull(dr["ZAIKO_HINMEI_CD"]) ? string.Empty : Convert.ToString(dr["ZAIKO_HINMEI_CD"]);
                            entity.ZAIKO_HINMEI_NAME = Convert.IsDBNull(dr["ZAIKO_HINMEI_NAME"]) ? string.Empty : Convert.ToString(dr["ZAIKO_HINMEI_NAME"]);
                            entity.ZAIKO_HIRITSU = Convert.IsDBNull(dr["ZAIKO_HIRITSU"]) ? (short)0 : Convert.ToInt16(dr["ZAIKO_HIRITSU"]);
                            entity.ZAIKO_TANKA = Convert.IsDBNull(dr["ZAIKO_TANKA"]) ? decimal.Zero : Convert.ToDecimal(dr["ZAIKO_TANKA"]);
                            //entity.ZAIKO_RYOU = decimal.Zero; // 在庫量を設定しない
                            // 20150504 「単価」を取得できないシステムエラーの一時対応(在庫不具合一覧235) End

                            zaikoHinmeiHuriwakes.Add(entity);
                        }
                    }

                    this.dto.rowZaikoHinmeiHuriwakes[row] = zaikoHinmeiHuriwakes;
                    if (!this.ZaikoHinmeiKakunou(row))
                    {
                        throw new Exception("");
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }
        // 20150408 在庫品名振分検索処理 End

        // 20150412 在庫品名単独設定追加(修正後のG051からコピー) Start
        /// <summary>
        /// 在庫品名単独設定
        /// </summary>
        /// <param name="row"></param>
        /// <remarks>在庫品名手変更後、1件在庫品名情報検索</remarks>
        internal bool ZaikoHinmeiSingleSearch(Row row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(row);

                // 在庫管理の場合のみ設定する
                if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
                {
                    if (row != null)
                    {
                        var zaikoHinmeiHuriwakes = new List<T_ZAIKO_HINMEI_HURIWAKE>();

                        if (!Convert.IsDBNull(row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value) &&
                            row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value != null &&
                            !string.IsNullOrEmpty(row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value.ToString()))
                        {

                            if (Convert.IsDBNull(row.Cells[CELL_NAME_ZAIKO_HINMEI_NAME].Value) || row.Cells[CELL_NAME_ZAIKO_HINMEI_NAME].Value == null ||
                                Convert.IsDBNull(row.Cells[CELL_NAME_ZAIKO_TANKA].Value) || row.Cells[CELL_NAME_ZAIKO_TANKA].Value == null)
                            {
                                // 在庫品名又は単価はnullの場合、再検索を行う。
                                var zaikoHinmeis = this.accessor.GetAllValidZaikoHinmeiData(row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value.ToString());
                                if (zaikoHinmeis != null && zaikoHinmeis.Length > 0)
                                {
                                    var entity = new T_ZAIKO_HINMEI_HURIWAKE();

                                    entity.ZAIKO_HINMEI_CD = zaikoHinmeis[0].ZAIKO_HINMEI_CD;
                                    entity.ZAIKO_HINMEI_NAME = zaikoHinmeis[0].ZAIKO_HINMEI_NAME_RYAKU;
                                    entity.ZAIKO_TANKA = zaikoHinmeis[0].ZAIKO_TANKA;
                                    entity.ZAIKO_HIRITSU = 100; // 比率を100%で設定する
                                    //entity.ZAIKO_RYOU = decimal.Zero; // 在庫量を設定しない

                                    zaikoHinmeiHuriwakes.Add(entity);
                                }
                            }
                            else
                            {
                                var entity = new T_ZAIKO_HINMEI_HURIWAKE();

                                entity.ZAIKO_HINMEI_CD = Convert.ToString(row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value);
                                entity.ZAIKO_HINMEI_NAME = Convert.ToString(row.Cells[CELL_NAME_ZAIKO_HINMEI_NAME].Value);
                                entity.ZAIKO_TANKA = Convert.ToDecimal(row.Cells[CELL_NAME_ZAIKO_TANKA].Value);
                                entity.ZAIKO_HIRITSU = 100; // 比率を100%で設定する
                                //entity.ZAIKO_RYOU = decimal.Zero; // 在庫量を設定しない

                                zaikoHinmeiHuriwakes.Add(entity);
                            }
                        }

                        this.dto.rowZaikoHinmeiHuriwakes[row] = zaikoHinmeiHuriwakes;
                        if (!this.ZaikoHinmeiKakunou(row))
                        {
                            return false;
                        }
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeiSingleSearch", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }
        // 20150412 在庫品名単独設定追加 End

        // 20150411 在庫品名クリア処理(修正後のG051からコピー) Start
        /// <summary>
        /// 在庫品名クリア
        /// </summary>
        /// <param name="row"></param>
        /// <remarks>品名をクリアする時、在庫品名情報もクリアする</remarks>
        internal bool ZaikoHinmeiHuriwakesClear(Row row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(row);

                // 在庫管理の場合のみ設定する
                if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
                {
                    // 前の在庫品名振分データを廃棄し、空リストを設定する。
                    this.dto.rowZaikoHinmeiHuriwakes[row] = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                    // 空リストを設定した上、在庫品名(略称)を(実際空文字に)設定する
                    if (!this.ZaikoHinmeiKakunou(row))
                    {
                        return false;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoHinmeiHuriwakesClear", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeiHuriwakesClear", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }
        // 20150411 在庫品名クリア処理 End

        // 20150408 在庫品名(略称)処理(修正後のG051からコピー) Start
        /// <summary>
        /// 在庫品名格納
        /// </summary>
        /// <param name="row"></param>
        /// <remarks>
        /// 存在しない場合は空文字
        /// 1件の場合は略称
        /// 又は複数件の場合は"複数在庫品目"文字
        /// </remarks>
        internal bool ZaikoHinmeiKakunou(Row row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(row);

                // 在庫管理の場合のみ設定する
                if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
                {
                    List<T_ZAIKO_HINMEI_HURIWAKE> entities = null;
                    bool entitiesGot = this.dto.rowZaikoHinmeiHuriwakes.TryGetValue(row, out entities);
                    if (!entitiesGot || entities == null || entities.Count <= 0) // Countは0以下の可能性はないはずが、IFを全部カバーするため「<=」を使う
                    {
                        row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value = string.Empty;
                        row.Cells[CELL_NAME_ZAIKO_HINMEI_NAME].Value = string.Empty;
                        row.Cells[CELL_NAME_ZAIKO_TANKA].Value = decimal.Zero;
                    }
                    else if (entities.Count == 1)
                    {
                        row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value = entities[0].ZAIKO_HINMEI_CD;
                        row.Cells[CELL_NAME_ZAIKO_HINMEI_NAME].Value = entities[0].ZAIKO_HINMEI_NAME;
                        row.Cells[CELL_NAME_ZAIKO_TANKA].Value = entities[0].ZAIKO_TANKA;
                    }
                    else
                    {

                        row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].Value = string.Empty;
                        row.Cells[CELL_NAME_ZAIKO_HINMEI_NAME].Value = "複数在庫品目";
                        row.Cells[CELL_NAME_ZAIKO_TANKA].Value = decimal.Zero;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoHinmeiKakunou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeiKakunou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        // 20150408 在庫品名(略称)設定 End

        // 20150412 在庫品名設定前チェック(修正後のG051からコピー) Start
        /// <summary>
        /// 在庫品名設定前チェック
        /// </summary>
        /// <param name="row"></param>
        /// <param name="allowEmpty"></param>
        /// <returns></returns>
        /// <remarks>
        /// 品名未設定の場合、在庫品名振分画面への遷移と在庫品名手変更を阻止する。
        /// </remarks>
        internal bool ZaikoChangeCheck(Row row, bool allowEmpty = true)
        {
            LogUtility.DebugMethodStart(row, allowEmpty);
            bool returnVal = true;

            // 在庫管理の場合のみチェックする
            if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
            {
                bool isEmpty =
                    Convert.IsDBNull(row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].EditedFormattedValue) ||
                    row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].EditedFormattedValue == null ||
                    string.IsNullOrEmpty(row.Cells[CELL_NAME_ZAIKO_HINMEI_CD].EditedFormattedValue.ToString());

                // 空値不可の場合
                if (!allowEmpty || !isEmpty)
                {
                    if (Convert.IsDBNull(row.Cells[CELL_NAME_HINMEI_CD].Value) ||
                    row.Cells[CELL_NAME_HINMEI_CD].Value == null ||
                    string.IsNullOrEmpty(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                    {
                        // 品名CD必須入力チェック
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E051", "品名");
                        returnVal = false;
                    }
                }

            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }
        // 20150412 在庫品名設定前チェック End

        // 20150414 在庫品名登録前チェック(修正後のG051からコピー) Start
        /// <summary>
        /// 在庫品名登録前チェック
        /// </summary>
        /// <returns></returns>
        internal bool ZaikoRegistCheck(out bool catchErr)
        {
            catchErr = false;
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 在庫管理の場合のみチェックする
                if (this.dto.sysInfoEntity.ZAIKO_KANRI.Value == 1)
                {
                    // 20150420 判定対象修正(有価在庫不具合一覧109) Start
                    // 在庫を設定したか判定
                    //var zaikoSetted =
                    //    this.dto.detailZaikoHinmeiHuriwakes.Sum(rowEntities => rowEntities.Value == null ? 0 : rowEntities.Value.Count) > 0;
                    var zaikoSetted =
                        this.dto.rowZaikoHinmeiHuriwakes.Sum(row => row.Value == null ? 0 : row.Value.Count) > 0;
                    // 20150420 判定対象修正(有価在庫不具合一覧109) End

                    // 現場自社区分
                    bool jishaKbn = false;
                    // 削除フラグ、適用期間の範囲は考慮しない
                    var genba = this.accessor.GetGenba(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, string.Empty, string.Empty, out catchErr, false);
                    if (catchErr)
                    {
                        return false;
                    }
                    if (genba != null && !genba.JISHA_KBN.IsNull)
                    {
                        jishaKbn = genba.JISHA_KBN.IsTrue;
                    }

                    // 在庫が設定していないが、または(設定した且つ)現場は自社の場合
                    returnVal = !zaikoSetted || jishaKbn;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoRegistCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoRegistCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }
        // 20150414 在庫品名登録前チェック End
        // No.4578<--

        /**
         * 検収状況の状態
         */
        internal string miKenshu = "未検収";
        internal string kenshuZumi = "検収済";
        internal string kenshuHuyou = "検収不要";

        /// <summary>
        /// 検収状況セット
        /// this.dto.kenshuDetailListが設定されていることを前提として実装
        /// </summary>
        internal void SetKenshuDetail()
        {
            // 初期化
            this.form.txtKensyuu.Text = string.Empty;

            if (this.form.KENSHU_MUST_KBN == null)
            {
                return;
            }

            if (this.form.KENSHU_MUST_KBN.Checked)
            {
                if (this.dto.entryEntity.KENSHU_DATE.IsNull)
                {
                    //検収データがない場合
                    this.form.txtKensyuu.Text = this.miKenshu;
                }
                else
                {
                    //検収データがある場合
                    this.form.txtKensyuu.Text = this.kenshuZumi;
                }
            }
            else
            {
                //"検収不要"
                this.form.txtKensyuu.Text = this.kenshuHuyou;
            }

            // 日付等の状態セット
            this.SetKenshuDateStatus();
        }

        /// <summary>
        /// 行追加時のDictionary追加処理
        /// </summary>
        /// <param name="index"></param>
        internal void AddRowDic(int index)
        {
            //Dictionary関連修正
            this.dto.rowZaikoShukkaDetails[this.form.gcMultiRow1.Rows[index]] = new List<T_ZAIKO_SHUKKA_DETAIL>();
            this.dto.rowZaikoHinmeiHuriwakes[this.form.gcMultiRow1.Rows[index]] = new List<T_ZAIKO_HINMEI_HURIWAKE>();
        }

        /// <summary>
        /// 行削除時のDictionary削除処理
        /// </summary>
        /// <param name="index"></param>
        internal void RemoveRowDic(int index)
        {
            //Dictionary関連修正
            this.dto.rowZaikoShukkaDetails[this.form.gcMultiRow1.Rows[index]] = null;
            this.dto.rowZaikoShukkaDetails.Remove(this.form.gcMultiRow1.Rows[index]);

            this.dto.rowZaikoHinmeiHuriwakes[this.form.gcMultiRow1.Rows[index]] = null;
            this.dto.rowZaikoHinmeiHuriwakes.Remove(this.form.gcMultiRow1.Rows[index]);
        }

        /// <summary>
        /// 営業担当者の表示（現場マスタ、業者マスタ、取引先マスタを元に）
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        internal void SetEigyouTantousha(string genbaCd, string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(genbaCd, gyoushaCd, torihikisakiCd);

            M_GENBA genbaEntity = new M_GENBA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                // 業者CD入力あり
                if (!string.IsNullOrEmpty(genbaCd))
                {
                    // 現場CD入力あり
                    bool catchErr = false;
                    genbaEntity = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr) { throw new Exception(""); }
                    if (genbaEntity != null)
                    {
                        // コードに対応する現場マスタが存在する
                        eigyouTantouCd = genbaEntity.EIGYOU_TANTOU_CD;
                        if (!string.IsNullOrEmpty(eigyouTantouCd))
                        {
                            // 現場マスタに営業担当者の設定がある場合
                            shainEntity = this.accessor.GetShain(eigyouTantouCd);
                            if (shainEntity != null)
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できた場合
                                if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                                {
                                    // 取得した社員マスタの社員名略が設定されている場合
                                    this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                                }
                                else
                                {
                                    // 取得した社員マスタの社員名略が設定されていない場合
                                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                                }
                            }
                            else
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できない場合
                                GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                            }
                        }
                        else
                        {
                            // 現場マスタに営業担当者の設定がない場合
                            GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                        }
                    }
                }
                else
                {
                    // 現場CD入力なし
                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                }
            }
            else
            {
                // 業者CD入力なし
                GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタの営業担当者コードからの営業担当者取得(業者CD入力あり、業者マスタに存在することが前提)
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfGyousha(string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, torihikisakiCd);

            M_GYOUSHA gyoushaEntity = new M_GYOUSHA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;
            bool catchErr = false;
            gyoushaEntity = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
            if (catchErr) { throw new Exception(""); }
            if (gyoushaEntity != null)
            {
                // コードに対応する業者マスタが存在する
                eigyouTantouCd = gyoushaEntity.EIGYOU_TANTOU_CD;
                if (!string.IsNullOrEmpty(eigyouTantouCd))
                {
                    // 業者マスタに営業担当者の設定がある場合
                    shainEntity = this.accessor.GetShain(eigyouTantouCd);
                    if (shainEntity != null)
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できた場合
                        if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                        {
                            // 取得した社員マスタの社員名略が設定されている場合
                            this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                            this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            // 取得した社員マスタの社員名略が設定されていない場合
                            GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                        }
                    }
                    else
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できない場合
                        GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                    }
                }
                else
                {
                    // 業者マスタに営業担当者の設定がない場合
                    GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                }
            }
            else
            {
                // コードに対応する業者マスタが存在しない
                // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先マスタの営業担当者コードからの営業担当者取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfTorihikisaki(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                // 取引先CD入力あり
                bool catchErr = false;
                torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { throw new Exception(""); }
                if (torihikisakiEntity != null)
                {
                    // コードに対応する取引先マスタが存在する
                    eigyouTantouCd = torihikisakiEntity.EIGYOU_TANTOU_CD;
                    if (!string.IsNullOrEmpty(eigyouTantouCd))
                    {
                        // 取引先マスタに営業担当者の設定がある場合
                        shainEntity = this.accessor.GetShain(eigyouTantouCd);
                        if (shainEntity != null)
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できた場合
                            if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                            {
                                // 取得した社員マスタの社員名略が設定されている場合
                                this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                                this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                            }
                            else
                            {
                                // 取得した社員マスタの社員名略が設定されていない場合
                                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                            }
                        }
                        else
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できない場合
                            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                        }
                    }
                    else
                    {
                        // 取引先マスタに営業担当者の設定がない場合
                        this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                    }
                }
                else
                {
                    // コードに対応する取引先マスタが存在しない
                    // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    return;
                }
            }
            else
            {
                // 取引先CD入力なし
                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 要検収のデフォルト値を設定
        /// </summary>
        internal bool SetDefultKenshuMustKbn()
        {
            try
            {
                // 必要な情報がセットされているかチェック
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text)
                    || string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    return true;
                }

                bool catchErr = false;
                var genba = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    return false;
                }
                if (genba == null)
                {
                    return true;
                }

                // 現場.検収要否のデフォルト値としてセット
                if (this.form.KENSHU_MUST_KBN.Enabled == true)
                {
                    if (!genba.KENSHU_YOUHI.IsNull)
                    {
                        this.form.KENSHU_MUST_KBN.Checked = genba.KENSHU_YOUHI.Value;
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetDefultKenshuMustKbn", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetDefultKenshuMustKbn", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }

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

        /// <summary>
        /// 指定された出荷番号の次に大きい番号を取得
        /// </summary>
        /// <param name="ShukkaNumber"></param>
        /// <param name="nextEmptyCheck">true:画面の出荷番号が入力済み、false:画面の出荷番号が未入力</param>
        /// <returns></returns>
        internal long GetNextShukkaNumber(long ShukkaNumber, out bool catchErr, bool nextEmptyCheck)
        {
            long returnValue = 0;
            catchErr = false;
            try
            {
                // No.3341-->
                string KyotenCD = this.headerForm.KYOTEN_CD.Text;
                returnValue = this.accessor.GetNextShukkaNumber(ShukkaNumber, KyotenCD);
                if (returnValue == 0)
                {
                    returnValue = this.accessor.GetNextShukkaNumber(0, KyotenCD);
                    if (returnValue == ShukkaNumber && nextEmptyCheck)
                    {
                        returnValue = 0;
                    }
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNextShukkaNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextShukkaNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            // No.3341 <--
            return returnValue;
        }

        /// <summary>
        /// 指定された出荷番号の次に小さい番号を取得
        /// </summary>
        /// <param name="ShukkaNumber"></param>
        /// <param name="preEmptyCheck">true:画面の出荷番号が入力済み、false:画面の出荷番号が未入力</param>
        /// <returns></returns>
        internal long GetPreShukkaNumber(long ShukkaNumber, bool preEmptyCheck)
        {
            // No.3341-->
            string KyotenCD = this.headerForm.KYOTEN_CD.Text;
            long returnValue = this.accessor.GetPreShukkaNumber(ShukkaNumber, KyotenCD);
            if (returnValue == 0 || !preEmptyCheck)
            {
                long max = this.accessor.GetMaxShukkaNumber();
                returnValue = this.accessor.GetPreShukkaNumber(max + 1, KyotenCD);
                if (returnValue == ShukkaNumber && preEmptyCheck)
                {
                    returnValue = 0;
                }
            }
            // No.3341<--
            return returnValue;
        }

        // No.1767
        /// <summary>
        /// 指定された出荷番号の次に小さい番号を取得
        /// </summary>
        /// <returns></returns>
        internal long GetMaxShukkaNumber(out bool catchErr)
        {
            catchErr = false;
            long returnValue = 0;

            try
            {
                returnValue = this.accessor.GetMaxShukkaNumber();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetMaxShukkaNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetMaxShukkaNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue, catchErr);
            }
            return returnValue;
        }

        /// <summary>
        /// 明細行に入力されている伝票区分の状況を取得
        /// </summary>
        /// <returns>0:売上と支払が混在 1:売上のみ 2:支払のみ</returns>
        internal int GetRowsDenpyouKbnCdMixed()
        {
            LogUtility.DebugMethodStart();

            int returnValue = URIAGE_SHIHARAI_MIXED;
            int currentRowdenKbn = URIAGE_SHIHARAI_MIXED;
            short denpyouKbnCd = 0;

            // 最初の明細行の状態を取得
            short.TryParse(Convert.ToString(this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbnCd);
            switch (denpyouKbnCd)
            {
                case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE:
                    returnValue = URIAGE_ONLY;
                    break;
                case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI:
                    returnValue = SHIHARAI_ONLY;
                    break;
            }

            // 各明細行の伝票区分を参照
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                short.TryParse(Convert.ToString(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbnCd);
                switch (denpyouKbnCd)
                {
                    case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE:
                        currentRowdenKbn = URIAGE_ONLY;
                        break;
                    case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI:
                        currentRowdenKbn = SHIHARAI_ONLY;
                        break;
                }
                // 最初の行と違う伝票区分が存在すれば、その時点で抜ける
                if (currentRowdenKbn != returnValue)
                {
                    returnValue = URIAGE_SHIHARAI_MIXED;
                    break;
                }
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }

        /// <summary>
        /// 入力情報から確定伝票かどうかを判断
        /// </summary>
        /// <returns>true : 確定、false : 未確定</returns>
        internal bool IsKakuteiDenpyou()
        {
            bool returnValue = false;

            /**
             * 確定フラグの制御
             *
             * ■システム設定の確定条件:伝票単位の場合
             * 　Detailの確定フラグ：Entryの確定フラグをチェック
             *
             * ■システム設定の確定条件：明細単位の場合
             * 　Entryの確定フラグ：Detailの確定フラグに1つでも未確定があったら未確定
             * 　　　　　　　　　　 上記以外は確定
             */
            if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
            {
                returnValue = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI.ToString().Equals(this.form.KAKUTEI_KBN.Text);

            }
            else
            {
                // 明細単位
                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                    {
                        continue;
                    }

                    if (row.Cells[CELL_NAME_KAKUTEI_KBN].Value == null
                        || !(bool)row.Cells[CELL_NAME_KAKUTEI_KBN].Value)
                    {
                        returnValue = false;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source)
        {
            string result = string.Empty;

            // 該当コントロールの最大桁数を取得
            object obj;
            decimal charactersNumber;
            string text = PropertyUtility.GetTextOrValue(source);
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
                // 最大桁数が取得できない場合はそのまま
                return text;

            charactersNumber = (decimal)obj;
            if (charactersNumber == 0 || source == null || string.IsNullOrEmpty(text))
                // 最大桁数が0または入力値が空の場合はそのまま
                return text;

            var strCharactersUmber = text;
            if (strCharactersUmber.Contains("."))
                // 小数点を含む場合はそのまま
                return text;

            // ゼロサプレスした値を返す
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            string format = sb.Append('#', (int)charactersNumber).ToString();
            long val = 0;
            if (long.TryParse(text, out val))
                result = val == 0 ? "0" : val.ToString(format);
            else
                // 入力値が数値ではない場合はそのまま
                result = text;

            return result;
        }

        /// <summary>
        /// 明細行の重量項目取得（計量票などの出力用）
        /// </summary>
        /// <param name="JuryoOption">0：総重量、1：空車重量</param>
        /// <returns>該当重量項目の値</returns>
        private string GetJuryoCol(int JuryoOption)
        {
            LogUtility.DebugMethodStart(JuryoOption);

            string returnVal = string.Empty;
            switch (JuryoOption)
            {
                case 0:
                    // 総重量取得（明細行のうち最後の行）
                    foreach (var row in this.form.gcMultiRow1.Rows.Reverse())
                        if (row.Cells[CELL_NAME_STAK_JYUURYOU].FormattedValue != null)
                            if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].FormattedValue.ToString()))
                            {
                                returnVal = row.Cells[CELL_NAME_STAK_JYUURYOU].FormattedValue.ToString();
                                break;
                            }
                    break;
                case 1:
                    // 空車重量取得（明細行のうち最初の行）
                    foreach (var row in this.form.gcMultiRow1.Rows)
                        if (row.Cells[CELL_NAME_EMPTY_JYUURYOU].FormattedValue != null)
                            if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_EMPTY_JYUURYOU].FormattedValue.ToString()))
                            {
                                returnVal = row.Cells[CELL_NAME_EMPTY_JYUURYOU].FormattedValue.ToString();
                                break;
                            }
                    break;
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// 売上日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetUriageShouhizeiRate()
        {
            try
            {
                DateTime uriageDate = this.footerForm.sysDate.Date;
                if (DateTime.TryParse(this.form.URIAGE_DATE.Text, out uriageDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageDate);
                    if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = string.Empty;
                }

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex1);
                msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 支払日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetShiharaiShouhizeiRate()
        {
            try
            {
                DateTime shiharaiDate = this.footerForm.sysDate.Date;
                if (DateTime.TryParse(this.form.SHIHARAI_DATE.Text, out shiharaiDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(shiharaiDate);
                    if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = string.Empty;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 売上、支払消費税率のポップアップ設定初期化
        /// </summary>
        internal void InitShouhizeiRatePopupSetting()
        {
            /**
             * 売上消費税率テキストボックスの設定
             */
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowId = WINDOW_ID.M_SHOUHIZEI;
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowName = "マスタ共通ポップアップ";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupGetMasterField = "SHOUHIZEI_RATE";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupSetFormField = "URIAGE_SHOUHIZEI_RATE_VALUE";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle = new string[] { "消費税率" };

            // 表示情報作成
            var shouhizeiRates = this.accessor.GetAllShouhizeiRate();
            var dt = EntityUtility.EntityToDataTable(shouhizeiRates);

            var displayShouhizei = new DataTable();
            foreach (var col in this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()))
            {
                displayShouhizei.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);

            }

            foreach (DataRow row in dt.Rows)
            {
                displayShouhizei.Rows.Add(displayShouhizei.Columns.OfType<DataColumn>().Select(s => row[s.ColumnName]).ToArray());
            }

            displayShouhizei.TableName = "消費税率";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataSource = displayShouhizei;

            /**
             * ポップアップの設定
             */
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupGetMasterField = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupGetMasterField;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupSetFormField = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupSetFormField;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowId = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowId;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowName = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowName;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataHeaderTitle = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataSource = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataSource;

            /**
             * 支払消費税率テキストボックスの設定
             */
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowId = WINDOW_ID.M_SHOUHIZEI;
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowName = "マスタ共通ポップアップ";
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupGetMasterField = "SHOUHIZEI_RATE";
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupSetFormField = "SHIHARAI_SHOUHIZEI_RATE_VALUE";
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle = new string[] { "消費税率" }; ;
            // 売上消費税率と同様のマスタを参照するためデータソースは売上消費税のを流用
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataSource = displayShouhizei;

            /**
             * ポップアップの設定
             */
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupGetMasterField = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupGetMasterField;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupSetFormField = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupSetFormField;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowId = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowId;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowName = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowName;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataHeaderTitle = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataSource = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataSource;
        }

        /// <summary>
        /// UIFormの売上消費税率をパーセント表記で取得する
        /// </summary>
        /// <returns>パーセント表示の売上消費税率</returns>
        internal string ToPercentForUriageShouhizeiRate(out bool catchErr)
        {
            string returnVal = string.Empty;
            catchErr = false;

            try
            {

                if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
                {
                    decimal shouhizeiRate = 0;
                    if (!this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Contains("%")
                        && decimal.TryParse(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text, out shouhizeiRate))
                    {
                        returnVal = shouhizeiRate.ToString("P");
                    }
                    else if (this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                    {
                        // 既に%表記ならそのまま返す
                        returnVal = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text;
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToPercentForUriageShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }

        /// <summary>
        /// UIFormの売上消費税率を小数点表記で取得する
        /// </summary>
        /// <returns>小数点表記の売上消費税率(DBへ格納できる値)</returns>
        internal decimal ToDecimalForUriageShouhizeiRate()
        {
            decimal returnVal = 0;

            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
            {
                string tempUriageShouhizeiRate = string.Empty;

                if (!this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                {
                    tempUriageShouhizeiRate = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text;
                }
                else
                {
                    tempUriageShouhizeiRate = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Replace("%", "");
                }

                decimal shouhizeiRate = 0;
                if (decimal.TryParse(tempUriageShouhizeiRate, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate / 100m;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// UIFormの支払消費税率をパーセント表記で取得する
        /// </summary>
        /// <returns>パーセント表示の売上消費税率</returns>
        internal string ToPercentForShiharaiShouhizeiRate(out bool catchErr)
        {
            string returnVal = string.Empty;
            catchErr = false;
            try
            {
                if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
                {
                    decimal shouhizeiRate = 0;
                    if (!this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Contains("%")
                        && decimal.TryParse(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text, out shouhizeiRate))
                    {
                        returnVal = shouhizeiRate.ToString("P");
                    }
                    else if (this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                    {
                        // 既に%表記ならそのまま返す
                        returnVal = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text;
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToPercentForShiharaiShouhizeiRate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return returnVal;
            }
        }

        /// <summary>
        /// UIFormの支払消費税率を小数点表記で取得する
        /// </summary>
        /// <returns>小数点表記の売上消費税率(DBへ格納できる値)</returns>
        internal decimal ToDecimalForShiharaiShouhizeiRate()
        {
            decimal returnVal = 0;

            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
            {
                string tempUriageShouhizeiRate = string.Empty;

                if (!this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                {
                    tempUriageShouhizeiRate = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text;
                }
                else
                {
                    tempUriageShouhizeiRate = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Replace("%", "");
                }

                decimal shouhizeiRate = 0;
                if (decimal.TryParse(tempUriageShouhizeiRate, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate / 100m;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// 検収データ情報による日付等の操作
        /// </summary>
        private void SetKenshuDateStatus()
        {
            if (this.dto.kenshuNyuuryokuDto.kenshuDetailList.Count > 0)
            {
                // 検収明細データありの場合は、要検収、売上/支払日付を無効とする
                this.form.KENSHU_MUST_KBN.Enabled = false;
                this.form.URIAGE_DATE.Enabled = false;
                this.form.SHIHARAI_DATE.Enabled = false;

                // 検収売上/支払日付を売上/支払日付とする
                this.form.URIAGE_DATE.Value = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_DATE.Value;
                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = Convert.ToString(this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE);
                this.form.SHIHARAI_DATE.Value = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE.Value;
                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = Convert.ToString(this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE);
            }
            else
            {
                // 検収明細データなしの場合は、要検収、売上/支払日付を有効とする
                this.form.KENSHU_MUST_KBN.Enabled = true;
                this.form.URIAGE_DATE.Enabled = true;
                this.form.SHIHARAI_DATE.Enabled = true;
            }
        }
        #endregion

        #region 締状況チェック処理
        /// <summary>
        /// 締状況チェック処理
        /// 請求明細、精算明細、在庫明細を確認して、対象の伝票に締済のデータが存在するか確認する。
        /// </summary>
        internal bool CheckAllShimeStatus()
        {
            bool retval = false;

            long systemId = -1;
            int seq = -1;

            if (!this.dto.entryEntity.SYSTEM_ID.IsNull) systemId = (long)this.dto.entryEntity.SYSTEM_ID;
            if (!this.dto.entryEntity.SEQ.IsNull) seq = (int)this.dto.entryEntity.SEQ;
            if (systemId != -1 && seq != -1)
            {
                // 締処理状況判定用データ取得
                DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);
                DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);
                T_ZAIKO_SHUKKA_DETAIL zaikoData = this.accessor.GetZaikoShukkaData(systemId, seq);

                // 締処理状況(請求明細)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    retval = true;
                }

                // 締処理状況(精算明細)
                if (retval == false && seisanData != null && 0 < seisanData.Rows.Count)
                {
                    retval = true;
                }

                if (retval == false && zaikoData != null)
                {
                    retval = true;
                }
            }

            return retval;
        }
        #endregion 締状況チェック処理

        #region 配車状況更新処理
        /// <summary>
        /// 出荷受付入力の配車状況を更新します
        /// </summary>
        /// <param name="haishaJokyoCd">配車状況CD</param>
        /// <param name="haishaJokyoName">配車状況</param>
        private void UpdateHaishaJokyo(string haishaJokyoCd, string haishaJokyoName)
        {
            LogUtility.DebugMethodStart(haishaJokyoCd, haishaJokyoName);

            if (Int16.Parse(SalesPaymentConstans.HAISHA_JOKYO_CD_CANCEL) != this.tUketsukeSkEntry.HAISHA_JOKYO_CD
                                           && Int16.Parse(haishaJokyoCd) != this.tUketsukeSkEntry.HAISHA_JOKYO_CD)
            {
                // 配車状況が変更されているときだけ更新する
                var newEntryEntity = this.CreateTUketsukeSkEntry(this.tUketsukeSkEntry);
                newEntryEntity.HAISHA_JOKYO_CD = Int16.Parse(haishaJokyoCd);
                newEntryEntity.HAISHA_JOKYO_NAME = haishaJokyoName;
                this.accessor.InsertUketsukeSkEntry(newEntryEntity);

                // もとのエンティティを削除する
                this.DeleteTUketsukeSkEntry(this.tUketsukeSkEntry);

                // 子の収集受付詳細を更新する
                var tUketsukeSsDetailList = this.accessor.GetUketsukeSkDetail(this.tUketsukeSkEntry.SYSTEM_ID.ToString(), this.tUketsukeSkEntry.SEQ.ToString());
                foreach (var tUketsukeSsDetail in tUketsukeSsDetailList)
                {
                    var newDetailEntity = this.CreateTUketsukeSkDetail(newEntryEntity, tUketsukeSsDetail);
                    this.accessor.InsertUketsukeSkDetail(newDetailEntity);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された出荷受付入力エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entity">元になるエンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_SK_ENTRY CreateTUketsukeSkEntry(T_UKETSUKE_SK_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            // 配車状況を変更したエンティティを作成
            var newEntity = new T_UKETSUKE_SK_ENTRY();
            Shougun.Core.Common.BusinessCommon.Utility.MasterUtility.CopyProperties(entity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entity.SEQ + 1;
            newEntity.CREATE_USER = entity.CREATE_USER;
            newEntity.CREATE_DATE = entity.CREATE_DATE;
            newEntity.CREATE_PC = entity.CREATE_PC;

            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 渡された出荷受付入力エンティティを削除します
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        private void DeleteTUketsukeSkEntry(T_UKETSUKE_SK_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var dbLogic = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(entity);
            dbLogic.SetSystemProperty(entity, true);
            entity.DELETE_FLG = true;
            this.accessor.UpdateUketsukeSkEntry(entity);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された出荷受付詳細エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entryEntity">親になる出荷受付入力エンティティ</param>
        /// <param name="detailEntity">元になる出荷受付詳細エンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_SK_DETAIL CreateTUketsukeSkDetail(T_UKETSUKE_SK_ENTRY entryEntity, T_UKETSUKE_SK_DETAIL detailEntity)
        {
            LogUtility.DebugMethodStart(entryEntity, detailEntity);

            var newEntity = new T_UKETSUKE_SK_DETAIL();
            Shougun.Core.Common.BusinessCommon.Utility.MasterUtility.CopyProperties(detailEntity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entryEntity.SEQ;
            newEntity.CREATE_USER = entryEntity.CREATE_USER;
            newEntity.CREATE_DATE = entryEntity.CREATE_DATE;
            newEntity.CREATE_PC = entryEntity.CREATE_PC;

            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 取得済みの収集受付詳細エンティティをクリアします
        /// </summary>
        internal void ClearTUketsukeSkEntry()
        {
            this.tUketsukeSkEntry = null;
        }
        #endregion

        // No.2613-->
        /// <summary>文字列の指定位置に改行挿入する</summary>
        internal string InsertReturn(string str, int num)
        {
            string retstr = "";
            if (false == string.IsNullOrEmpty(str))
            {
                string s = str;
                int numberOfs = s.Count();
                while (numberOfs > num)
                {
                    retstr = retstr + s.Substring(0, num) + "\n";
                    s = s.Substring(num, numberOfs - num);
                    numberOfs -= num;
                }
                if (numberOfs > 0)
                {
                    retstr = retstr + s;
                }
            }
            return retstr;
        }
        // No.2613<--

        /// <summary>
        /// 明細欄の品名をセットします
        /// </summary>
        /// <param name="row">現在のセルを含む行（CurrentRow）</param>
        internal bool SetHinmeiName(Row row)
        {
            try
            {
                if (row == null)
                {
                    return true;
                }
                bool catchErr = false;
                bool retChousei = this.CheckHinmeiCd(row, out catchErr);
                if (catchErr)
                {
                    return false;
                }

                if (retChousei)    // 品名コードの存在チェック（伝種区分が受入、または共通）
                {
                    // 入力された品名コードが存在するとき
                    if (row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value != null)
                    {
                        if (string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value.ToString()))
                        {
                            // 品名が空の場合再セット
                            //row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = this.SearchHinmei(row.Cells["HINMEI_CD"].Value.ToString());
                            this.GetHinmei(row, out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // 品名が空の場合再セット
                        //row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = this.SearchHinmei(row.Cells["HINMEI_CD"].Value.ToString());
                        this.GetHinmei(row, out catchErr);
                        if (catchErr)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetHinmeiName", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHinmeiName", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        #region 単位kg-品名数量制御処理

        /// <summary>
        /// 単位kg-品名数量制御処理
        /// </summary>
        /// <param name="cellName">対象Cell名称</param>
        /// <param name="row">対象行</param>
        public bool SetHinmeiSuuryou(string cellName, Row row, bool isKingakuNotCalc)
        {
            try
            {
                /*
                 * 以下のセルが変更された場合は正味重量、品名数量、マニ数量の同期をとる
                 *    総重量
                 *    空車重量
                 *    割振重量(kg)
                 *    割振(%)
                 *    調整重量(kg)
                 *    調整(%)
                 *    容器CD
                 *    容器数量
                 *    容器重量(kg)
                 *    品名CD
                 *    単位CD
                 */
                if (cellName.Equals(LogicClass.CELL_NAME_STAK_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_EMPTY_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_WARIFURI_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_WARIFURI_PERCENT)
                    || cellName.Equals(LogicClass.CELL_NAME_CHOUSEI_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_CHOUSEI_PERCENT)
                    || cellName.Equals(LogicClass.CELL_NAME_YOUKI_CD)
                    || cellName.Equals(LogicClass.CELL_NAME_YOUKI_SUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_YOUKI_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_HINMEI_CD)
                    || cellName.Equals(LogicClass.CELL_NAME_UNIT_CD))
                {
                    object jyuuryou = row.Cells[LogicClass.CELL_NAME_NET_JYUURYOU].Value;
                    object unitcd = row.Cells[LogicClass.CELL_NAME_UNIT_CD].Value;

                    decimal value = 0;
                    if (jyuuryou != null && decimal.TryParse(Convert.ToString(jyuuryou), out value))
                    {
                        // 正味重量あり
                        if ("3".Equals(unitcd))
                        {
                            // 正味重量＝品名数量とする
                            row.Cells[LogicClass.CELL_NAME_SUURYOU].Value = value;
                        }
                        else
                        {
                            if (unitcd != null && unitcd.Equals("1"))
                            {
                                decimal ton = value / 1000;
                                // 単位tの場合は正味重量/1000＝品名数量とする
                                row.Cells[LogicClass.CELL_NAME_SUURYOU].Value = ton;
                            }
                        }

                        if (unitcd != null && (unitcd.ToString().Equals("1") || unitcd.ToString().Equals("3")))
                        {
                            // 正味重量ありかつ単位がkg,tの場合は品名数量変更不可
                            row.Cells[LogicClass.CELL_NAME_SUURYOU].ReadOnly = true;
                        }
                        else
                        {
                            // その他の場合は品名数量変更可
                            row.Cells[LogicClass.CELL_NAME_SUURYOU].ReadOnly = false;
                        }
                    }
                    else
                    {
                        // 正味重量なしの場合は品名数量手入力可（単位は何でもOK)
                        row.Cells[LogicClass.CELL_NAME_SUURYOU].ReadOnly = false;
                    }
                    row.Cells[LogicClass.CELL_NAME_SUURYOU].UpdateBackColor(false);

                    // 金額の再計算を行う
                    if (!isKingakuNotCalc)
                    {
                        if (!this.CalcDetaiKingaku(row))
                        {
                            throw new Exception("");
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetHinmeiSuuryou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
        }

        #endregion

        /// <summary>
        /// データ移動処理
        /// </summary>
        internal void SetMoveData()
        {
            try
            {
                if (this.form.moveData_flg)
                {
                    this.form.TORIHIKISAKI_CD.Text = this.form.moveData_torihikisakiCd;
                    bool catchErr = false;
                    this.CheckTorihikisaki(out catchErr);
                    if (catchErr) { return; }
                    this.form.GYOUSHA_CD.Text = this.form.moveData_gyousyaCd;
                    this.CheckGyousha(out catchErr);
                    if (catchErr) { return; }
                    this.form.GENBA_CD.Text = this.form.moveData_genbaCd;
                    this.CheckGenba(out catchErr);
                    if (catchErr) { return; }
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    this.hasShow = false;
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("SetMoveData", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetMoveData", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
        }

        #region 検収入力画面用データセット
        /// <summary>
        /// 画面の情報から検収入力用データをセット
        /// </summary>
        private void SetKenshuNyuuryokuDTOClass()
        {
            // KenshuNyuryokuListは設定されているはずなのでここではセットしない。

            if (!this.dto.entryEntity.SYSTEM_ID.IsNull)
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
            }

            if (!this.dto.entryEntity.KENSHU_DATE.IsNull)
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_DATE = this.dto.entryEntity.KENSHU_DATE.Value.Date;
            }

            if (!this.dto.entryEntity.KENSHU_URIAGE_DATE.IsNull)
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_DATE = this.dto.entryEntity.KENSHU_URIAGE_DATE.Value.Date;
            }

            if (!this.dto.entryEntity.KENSHU_SHIHARAI_DATE.IsNull)
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE = this.dto.entryEntity.KENSHU_SHIHARAI_DATE.Value.Date;
            }

            decimal kenshuUriageShouhizeiRate = 0;
            if (!this.dto.entryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE.IsNull
                && decimal.TryParse(this.dto.entryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE.Value.ToString(), out kenshuUriageShouhizeiRate))
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE = kenshuUriageShouhizeiRate;
            }

            decimal kenshuShiharaiShouhizeiRate = 0;
            if (!this.dto.entryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE.IsNull
                && decimal.TryParse(this.dto.entryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Value.ToString(), out kenshuShiharaiShouhizeiRate))
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE = kenshuShiharaiShouhizeiRate;
            }

            decimal netTotal = 0;
            if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text)
                && decimal.TryParse(this.form.NET_TOTAL.Text, out netTotal))
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.NET_TOTAL = (decimal)netTotal;
            }
            else
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.NET_TOTAL = SqlDecimal.Null;
            }

            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }

            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            else
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.GYOUSHA_CD = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.GENBA_CD = this.form.GENBA_CD.Text;
            }
            else
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.GENBA_CD = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }

            // 合計系
            decimal uriageAmountTotal = 0;
            decimal shiharaiAmountTotal = 0;
            decimal hinmeiUriageKingakuTotal = 0;
            decimal hinmeiShiharaiKingakuTotal = 0;

            if (this.form.gcMultiRow1.Rows != null)
            {
                foreach (var tempRow in this.form.gcMultiRow1.Rows)
                {
                    if (tempRow.IsNewRow)
                    {
                        continue;
                    }

                    /**
                     * 必要データチェック
                     */
                    if (tempRow[CELL_NAME_HINMEI_CD].Value == null
                        || string.IsNullOrEmpty(tempRow[CELL_NAME_HINMEI_CD].Value.ToString()))
                    {
                        continue;
                    }
                    var targetHinmei = this.accessor.GetHinmeiDataByCd(tempRow[CELL_NAME_HINMEI_CD].Value.ToString());
                    if (targetHinmei == null)
                    {
                        continue;
                    }

                    short denpyouKbnCd = -1;
                    if (tempRow[CELL_NAME_DENPYOU_KBN_CD].Value == null
                        || string.IsNullOrEmpty(tempRow[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())
                        || !short.TryParse(tempRow[CELL_NAME_DENPYOU_KBN_CD].Value.ToString(), out denpyouKbnCd))
                    {
                        continue;
                    }

                    decimal kingaku = 0;
                    if (tempRow[CELL_NAME_KINGAKU].Value == null
                        || string.IsNullOrEmpty(tempRow[CELL_NAME_KINGAKU].Value.ToString())
                        || !decimal.TryParse(tempRow[CELL_NAME_KINGAKU].Value.ToString(), out kingaku))
                    {
                        continue;
                    }

                    /**
                     * 金額計上
                     */
                    if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == denpyouKbnCd)
                    {
                        // 品名から税区分CD取得(分岐)
                        if (targetHinmei.ZEI_KBN_CD.IsNull
                            || targetHinmei.ZEI_KBN_CD == 0)
                        {
                            uriageAmountTotal += kingaku;
                        }
                        else
                        {
                            hinmeiUriageKingakuTotal += kingaku;
                        }
                    }
                    else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == denpyouKbnCd)
                    {
                        // 品名から税区分CD取得(分岐)
                        if (targetHinmei.ZEI_KBN_CD.IsNull
                            || targetHinmei.ZEI_KBN_CD == 0)
                        {
                            shiharaiAmountTotal += kingaku;
                        }
                        else
                        {
                            hinmeiShiharaiKingakuTotal += kingaku;
                        }
                    }
                }
            }

            this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.URIAGE_AMOUNT_TOTAL = uriageAmountTotal;
            this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL = shiharaiAmountTotal;
            this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = hinmeiUriageKingakuTotal;
            this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = hinmeiShiharaiKingakuTotal;

            // T_SHUKKA_DETAIL
            List<T_SHUKKA_DETAIL> detailList = new List<T_SHUKKA_DETAIL>();
            if (this.form.gcMultiRow1 != null)
            {
                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    T_SHUKKA_DETAIL tempDetail = new T_SHUKKA_DETAIL();

                    // SYSTEM_ID
                    if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SYSTEM_ID.IsNull)
                    {
                        tempDetail.SYSTEM_ID = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SYSTEM_ID;
                    }

                    // SEQ
                    if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SEQ.IsNull)
                    {
                        tempDetail.SEQ = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SEQ;
                    }

                    // DETAIL_SYSTEM_ID
                    long detailSystemId = 0;
                    if (row[CELL_NAME_DETAIL_SYSTEM_ID].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_DETAIL_SYSTEM_ID].Value.ToString())
                        && long.TryParse(row[CELL_NAME_DETAIL_SYSTEM_ID].Value.ToString(), out detailSystemId))
                    {
                        tempDetail.DETAIL_SYSTEM_ID = detailSystemId;
                    }

                    // SHUKKA_NUMBER
                    if (!this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SHUKKA_NUMBER.IsNull)
                    {
                        tempDetail.SHUKKA_NUMBER = this.dto.kenshuNyuuryokuDto.shukkaEntryEntity.SHUKKA_NUMBER;
                    }

                    // ROW_NO
                    short rowNo = 0;
                    if (row[CELL_NAME_ROW_NO].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_ROW_NO].Value.ToString())
                        && short.TryParse(row[CELL_NAME_ROW_NO].Value.ToString(), out rowNo))
                    {
                        tempDetail.ROW_NO = rowNo;
                    }

                    // HINMEI_CD
                    if (row[CELL_NAME_HINMEI_CD].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_HINMEI_CD].Value.ToString()))
                    {
                        tempDetail.HINMEI_CD = row[CELL_NAME_HINMEI_CD].Value.ToString();

                        // HINMEI_ZEI_KBN_CD
                        var targetHinmei = this.accessor.GetHinmeiDataByCd(row[CELL_NAME_HINMEI_CD].Value.ToString());
                        if (targetHinmei != null && !targetHinmei.ZEI_KBN_CD.IsNull)
                        {
                            tempDetail.HINMEI_ZEI_KBN_CD = targetHinmei.ZEI_KBN_CD;
                        }
                    }

                    // HINMEI_NAME
                    if (row[CELL_NAME_HINMEI_NAME].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_HINMEI_NAME].Value.ToString()))
                    {
                        tempDetail.HINMEI_NAME = row[CELL_NAME_HINMEI_NAME].Value.ToString();
                    }

                    // UNIT_CD
                    short unitCd = 0;
                    if (row[CELL_NAME_UNIT_CD].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_UNIT_CD].Value.ToString())
                        && short.TryParse(row[CELL_NAME_UNIT_CD].Value.ToString(), out unitCd))
                    {
                        tempDetail.UNIT_CD = unitCd;
                    }

                    // NET_JYUURYOU
                    decimal netJyuuryou = 0;
                    if (row[CELL_NAME_NET_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_NET_JYUURYOU].Value.ToString())
                        && decimal.TryParse(row[CELL_NAME_NET_JYUURYOU].Value.ToString(), out netJyuuryou))
                    {
                        tempDetail.NET_JYUURYOU = (decimal)netJyuuryou;
                    }

                    // DENPYOU_KBN
                    short denpyouKbn = 0;
                    if (row[CELL_NAME_DENPYOU_KBN_CD].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())
                        && short.TryParse(row[CELL_NAME_DENPYOU_KBN_CD].Value.ToString(), out denpyouKbn))
                    {
                        tempDetail.DENPYOU_KBN_CD = denpyouKbn;
                    }

                    // SUURYOU
                    decimal suuryou = 0;
                    if (row[CELL_NAME_SUURYOU].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_SUURYOU].Value.ToString())
                        && decimal.TryParse(row[CELL_NAME_SUURYOU].Value.ToString(), out suuryou))
                    {
                        tempDetail.SUURYOU = (decimal)suuryou;
                    }

                    // TANKA
                    decimal tanka = 0;
                    if (row[CELL_NAME_TANKA].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_TANKA].Value.ToString())
                        && decimal.TryParse(row[CELL_NAME_TANKA].Value.ToString(), out tanka))
                    {
                        tempDetail.TANKA = tanka;
                    }

                    // KINGAKU
                    decimal kingaku = 0;
                    if (row[CELL_NAME_KINGAKU].Value != null
                        && !string.IsNullOrEmpty(row[CELL_NAME_KINGAKU].Value.ToString())
                        && decimal.TryParse(row[CELL_NAME_KINGAKU].Value.ToString(), out kingaku))
                    {
                        if (tempDetail.HINMEI_ZEI_KBN_CD != SqlInt16.Null
                            && !string.IsNullOrEmpty(tempDetail.HINMEI_ZEI_KBN_CD.ToString()))
                        {
                            tempDetail.KINGAKU = 0;
                            tempDetail.HINMEI_KINGAKU = kingaku;
                        }
                        else
                        {
                            tempDetail.KINGAKU = kingaku;
                            tempDetail.HINMEI_KINGAKU = 0;
                        }
                    }

                    detailList.Add(tempDetail);
                }
            }

            this.dto.kenshuNyuuryokuDto.shukkaDetailList = detailList;
        }
        #endregion

        // No.3822-->
        #region タブオーダー設定
        /// <summary>タブオーダー伝票データ格納</summary>
        internal void TabDataSetDenpyou()
        {
            try
            {
                int count = 0;
                if (DenpyouCtrl.Count > 0)
                {
                    DenpyouCtrl.Clear();
                }

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabUiFormControlNames);
                foreach (var controlName in formControlNameList)
                {
                    Control control = controlUtil.FindControl(this.form, controlName);

                    if (control == null)
                    {
                        // headerを検索
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    var enabledProperty = control.GetType().GetProperty("Enabled");
                    var readOnlyProperty = control.GetType().GetProperty("ReadOnly");
                    var tabStopProperty = control.GetType().GetProperty("TabStop");
                    var tabOrderProperty = control.GetType().GetProperty("TabIndex");
                    var textProperty = control.GetType().GetProperty("DisplayItemName");

                    if (enabledProperty != null)
                    {
                        bool readOnlyValue = false;
                        if (readOnlyProperty != null)
                        {
                            readOnlyValue = (bool)readOnlyProperty.GetValue(control, null);
                        }

                        //if (readOnlyValue == false && textProperty != null)
                        if (textProperty != null)   // ReadOnlyはチェックしないようにする
                        {
                            string text = (string)textProperty.GetValue(control, null);
                            bool tabStopValue = (bool)tabStopProperty.GetValue(control, null);
                            int tabOrderValue = (int)tabOrderProperty.GetValue(control, null);
                            if (!string.IsNullOrEmpty(text))
                            {
                                string liststring = string.Format("{0}:{1}:{2}:{3}", controlName, text, tabStopValue.ToString(), tabOrderValue.ToString());
                                DenpyouCtrl.Add(liststring);   // 有効なコントロール名のリスト作成
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
        }

        /// <summary>タブオーダー詳細データ格納</summary>
        internal void TabDataSetDetail()
        {
            try
            {
                int count = 0;
                if (DetailCtrl.Count > 0)
                {
                    DetailCtrl.Clear();
                }

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabDetailControlNames);
                var row = this.form.gcMultiRow1.Template.Row;
                foreach (var controlName in formControlNameList)
                {
                    GrapeCity.Win.MultiRow.Cell control = row.Cells[controlName];
                    if (control == null)
                    {
                        continue;
                    }

                    var enabledProperty = control.GetType().GetProperty("Enabled");
                    var readOnlyProperty = control.GetType().GetProperty("ReadOnly");
                    var tabStopProperty = control.GetType().GetProperty("TabStop");
                    var tabOrderProperty = control.GetType().GetProperty("TabIndex");
                    var textProperty = control.GetType().GetProperty("DisplayItemName");

                    if (enabledProperty != null)
                    {
                        bool readOnlyValue = false;
                        if (readOnlyProperty != null)
                        {
                            readOnlyValue = (bool)readOnlyProperty.GetValue(control, null);
                        }

                        //if (readOnlyValue == false && textProperty != null)
                        if (textProperty != null)   // ReadOnlyはチェックしないようにする
                        {
                            string text = (string)textProperty.GetValue(control, null);
                            bool tabStopValue = (bool)tabStopProperty.GetValue(control, null);
                            int tabOrderValue = (int)tabOrderProperty.GetValue(control, null);
                            if (!string.IsNullOrEmpty(text))
                            {
                                string liststring = string.Format("{0}:{1}:{2}:{3}", controlName, text, tabStopValue.ToString(), tabOrderValue.ToString());
                                DetailCtrl.Add(liststring);   // 有効なコントロール名のリスト作成
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
        }

        /// <summary>
        /// ステータス取得
        /// </summary>
        public void GetStatus()
        {
            bool dataUpdate = false;

            // タブオーダー伝票データ取得
            if (Properties.Settings.Default.DenpyouCtrl != null && Properties.Settings.Default.DenpyouCtrl.Count > 0)
            {
                if (DenpyouCtrl.Count > 0)
                {
                    DenpyouCtrl.Clear();
                }
                for (var i = 0; i < Properties.Settings.Default.DenpyouCtrl.Count; i++)
                {
                    DenpyouCtrl.Add(Properties.Settings.Default.DenpyouCtrl[i]);
                }

                // 設定に従いタブストップを変更
                for (var i = 0; i < DenpyouCtrl.Count; i++)
                {
                    // string分解
                    string str = DenpyouCtrl[i];
                    int ctpos = str.IndexOf(':');
                    string controlName = str.Substring(0, ctpos);
                    int nmpos = str.IndexOf(':', ctpos + 1);
                    int tspos = str.IndexOf(':', nmpos + 1);
                    string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

                    Control control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        // headerを検索
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }
                    if (control == null)
                    {
                        continue;
                    }
                    if (tbstop.Equals("True"))
                    {
                        //20151026 hoanghm #13404 start
                        //control.TabStop = true;
                        Type type = control.GetType();
                        if (type.Name == "CustomTextBox" || type.BaseType.Name == "CustomTextBox")
                        {
                            if (!((CustomTextBox)control).ReadOnly)
                            {
                                control.TabStop = true;
                            }
                            else
                            {
                                control.TabStop = false;
                            }
                        }
                        else
                        {
                            control.TabStop = true;
                        }
                        //20151026 hoanghm #13404 end
                    }
                    else
                    {
                        control.TabStop = false;
                    }
                }
            }
            else
            {   // データが存在しない場合作成
                TabDataSetDenpyou();
                dataUpdate = true;
            }

            // タブオーダー詳細データ取得
            if (Properties.Settings.Default.DetailCtrl != null && Properties.Settings.Default.DetailCtrl.Count > 0)
            {
                if (DetailCtrl.Count > 0)
                {
                    DetailCtrl.Clear();
                }
                for (var i = 0; i < Properties.Settings.Default.DetailCtrl.Count; i++)
                {
                    DetailCtrl.Add(Properties.Settings.Default.DetailCtrl[i]);
                }

                // 設定に従いタブストップを変更
                bool isSeted = false;
                var row = this.form.gcMultiRow1.Template.Row;
                for (var i = 0; i < DetailCtrl.Count; i++)
                {
                    // string分解
                    string str = DetailCtrl[i];
                    int ctpos = str.IndexOf(':');
                    string controlName = str.Substring(0, ctpos);
                    int nmpos = str.IndexOf(':', ctpos + 1);
                    int tspos = str.IndexOf(':', nmpos + 1);
                    string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

                    GrapeCity.Win.MultiRow.Cell control = row.Cells[controlName];
                    if (control == null)
                    {
                        continue;
                    }

                    if (tbstop.Equals("True"))
                    {
                        control.TabStop = true;
                        if (!isSeted)
                        {
                            this.firstIndexDetailCellName = controlName;
                            isSeted = true;
                        }
                    }
                    else
                    {
                        control.TabStop = false;
                    }
                }
            }
            else
            {   // データが存在しない場合作成
                TabDataSetDetail();
                dataUpdate = true;
            }

            if (dataUpdate == true)
            {
                //データ保存
                SetStatus();
            }
        }

        /// <summary>
        /// ステータス保存
        /// </summary>
        public void SetStatus()
        {
            // タブオーダー伝票データ格納
            if (DenpyouCtrl.Count > 0 && DenpyouCtrl != Properties.Settings.Default.DenpyouCtrl)
            {
                if (Properties.Settings.Default.DenpyouCtrl == null)
                {
                    Properties.Settings.Default.DenpyouCtrl = new System.Collections.Specialized.StringCollection();
                }
                if (Properties.Settings.Default.DenpyouCtrl != null)
                {
                    if (Properties.Settings.Default.DenpyouCtrl.Count > 0)
                    {
                        Properties.Settings.Default.DenpyouCtrl.Clear();
                    }
                    for (var i = 0; i < DenpyouCtrl.Count; i++)
                    {
                        Properties.Settings.Default.DenpyouCtrl.Add(DenpyouCtrl[i]);
                    }
                }
            }

            // タブオーダー詳細データ格納
            if (DetailCtrl.Count > 0 && DetailCtrl != Properties.Settings.Default.DetailCtrl)
            {
                if (Properties.Settings.Default.DetailCtrl == null)
                {
                    Properties.Settings.Default.DetailCtrl = new System.Collections.Specialized.StringCollection();
                }
                if (Properties.Settings.Default.DetailCtrl != null)
                {
                    if (Properties.Settings.Default.DetailCtrl.Count > 0)
                    {
                        Properties.Settings.Default.DetailCtrl.Clear();
                    }
                    for (var i = 0; i < DetailCtrl.Count; i++)
                    {
                        Properties.Settings.Default.DetailCtrl.Add(DetailCtrl[i]);
                    }
                }
            }

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 初期時フォーカス設定
        /// </summary>
        public bool SetTopControlFocus()
        {
            try
            {
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabUiFormControlNames);
                foreach (var controlName in formControlNameList)
                {
                    Control control = controlUtil.FindControl(this.headerForm, controlName);
                    ICustomAutoChangeBackColor autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.headerForm, controlName);
                    if (control != null)
                    {
                        if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                        {
                            control.Focus();
                            return true;
                        }
                    }
                    else
                    {
                        control = controlUtil.FindControl(this.form, controlName);
                        autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                        if (control != null)
                        {
                            if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                            {
                                control.Focus();
                                return true;
                            }
                        }
                    }
                }

                if (!this.form.gcMultiRow1.IsDisposed)
                {
                    // 最後までみつからなかった場合
                    // 詳細で最初を探す
                    GrapeCity.Win.MultiRow.Cell gcontrol = NextDetailContorl(true);
                    if (gcontrol != null)
                    {
                        gcontrol.Selected = true;
                        if (gcontrol.GcMultiRow != null)
                        {
                            gcontrol.GcMultiRow.Focus();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTopControlFocus", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }

        /// <summary>
        /// 次のタブストップのコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public void GotoNextControl(bool foward)
        {
            Control control = NextFormControl(foward);
            if (control != null)
            {
                control.Focus();
            }
        }

        /// <summary>
        /// 現在のコントロールの次のタブストップコントールを探す
        /// </summary>
        /// <param name="foward"></param>
        /// <returns></returns>
        public Control NextFormControl(bool foward)
        {
            Control control = null;
            ICustomAutoChangeBackColor autochange = null;
            bool startflg = false;
            List<string> formControlNameList = new List<string>();

            formControlNameList.AddRange(tabUiFormControlNames);
            if (foward == false)
            {
                formControlNameList.Reverse();
            }
            foreach (var controlName in formControlNameList)
            {
                control = controlUtil.FindControl(this.headerForm, controlName);
                autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.headerForm, controlName);
                if (control != null)
                {
                    if (startflg)
                    {
                        // 次のコントロール
                        if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                        {
                            return control;
                        }
                    }
                    else if (this.headerForm.ActiveControl != null && this.headerForm.ActiveControl.Equals(control))
                    {   // 現在のactiveコントロ－ル
                        startflg = true;
                    }
                }
                else
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                    if (control != null)
                    {
                        if (startflg)
                        {
                            // 次のコントロール
                            if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                            {
                                return control;
                            }
                        }
                        else if (this.form.ActiveControl != null && this.form.ActiveControl.Equals(control))
                        {   // 現在のactiveコントロ－ル
                            startflg = true;
                        }
                    }
                }
            }

            // 最後までみつからなかった場合
            // 詳細で最初を探す
            GrapeCity.Win.MultiRow.Cell gcontrol = NextDetailContorl(foward);
            if (gcontrol != null)
            {
                this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(gcontrol.RowIndex, gcontrol.Name);
                if (gcontrol.GcMultiRow != null)
                {
                    gcontrol.GcMultiRow.Focus();
                }
                return null;
            }

            // 詳細でタブストップが無い場合最初から検索
            foreach (var controlName in formControlNameList)
            {
                control = controlUtil.FindControl(this.headerForm, controlName);
                autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.headerForm, controlName);
                if (control != null)
                {
                    if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                    {
                        return control;
                    }
                }
                else
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                    if (control != null)
                    {
                        if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                        {
                            return control;
                        }
                    }
                }
            }
            return control;
        }

        /// <summary>
        /// 詳細の最初のコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public Cell NextDetailContorl(bool foward)
        {
            List<string> sformControlNameList = new List<string>();
            sformControlNameList.AddRange(tabDetailControlNames);
            if (foward == false)
            {
                sformControlNameList.Reverse();
            }

            GrapeCity.Win.MultiRow.Cell control = null;
            foreach (var controlName in sformControlNameList)
            {
                var tmprow = this.form.gcMultiRow1.Template.Row;
                GrapeCity.Win.MultiRow.Cell tmpcell = tmprow.Cells[controlName];
                if (tmpcell != null)
                {
                    if (tmpcell.TabStop == true && tmpcell.Visible == true && tmpcell.ReadOnly == false)    // テンプレートのタブストップで判断
                    {
                        var currentrow = this.form.gcMultiRow1.Rows[0];
                        if (foward == false)
                        {   // 最後の場合、最後の行の最後のセル
                            var last = this.form.gcMultiRow1.RowCount - 1;
                            currentrow = this.form.gcMultiRow1.Rows[last];
                        }
                        if (currentrow != null)
                        {
                            control = currentrow.Cells[controlName];
                        }
                        return control;
                    }
                }
            }
            return control;
        }

        /// <summary>
        /// タブストップ情報取得(詳細含まず)
        /// </summary>
        /// <returns></returns>
        private bool GetTabStop(string cname)
        {
            bool tabstop = false;
            for (var i = 0; i < DenpyouCtrl.Count; i++)
            {
                string str = DenpyouCtrl[i];
                int ctpos = str.IndexOf(':');
                string controlName = str.Substring(0, ctpos);

                if (cname.Equals(controlName))
                {
                    int nmpos = str.IndexOf(':', ctpos + 1);
                    int tspos = str.IndexOf(':', nmpos + 1);
                    string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

                    Control control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }
                    if (control == null)
                    {
                        continue;
                    }
                    if (tbstop.Equals("True"))
                    {
                        tabstop = true;
                    }
                    break;
                }
            }
            return tabstop;
        }

        #endregion タブオーダー設定
        // No.3822<--

        /// <summary>
        /// 単価未入力検収伝票出力判定
        /// </summary>
        /// <returns name="bool">TRUE:「単価」「金額」を空欄とした検収伝票を出力する</returns>
        /// <remarks>
        /// 要検収かつ、検収入力確定が行われていない場合、「単価」「金額」を空欄とした検収伝票を出力する
        /// </remarks>
        private bool BlankKenshuDetailOutput()
        {
            bool ret = false;

            if ((this.form.KENSHU_MUST_KBN.Checked == true) &&
              ((this.dto.kenshuNyuuryokuDto.kenshuDetailList == null) || (this.dto.kenshuNyuuryokuDto.kenshuDetailList.Count <= 0)))
            {
                // 要検収かつ、検収入力確定が行われていない場合、「単価」「金額」を空欄とした検収伝票を出力する
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 現在入力されている情報から諸口状態の車輌CDかチェック
        /// 諸口状態だった場合は、車輌CD、車輌名のデザインを諸口状態用に変更する
        /// </summary>
        internal void CheckShokuchiSharyou()
        {
            // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
            var sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
            // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END
            if (sharyouEntitys == null || sharyouEntitys.Length < 1)
            {
                this.ChangeShokuchiSharyouDesign();
            }
        }

        /// <summary>
        /// 車輌CD、車輌名を諸口状態のデザインへ変更する
        /// </summary>
        internal void ChangeShokuchiSharyouDesign()
        {
            this.form.oldSharyouShokuchiKbn = true;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
            this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");
            this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
            // 自由入力可能であるため車輌名の色を変更
            this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
            this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
        }

        /// 20141112 Houkakou 「出荷入力」の締済期間チェックの追加　start
        #region 請求日付チェック
        /// <summary>
        /// 請求日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SeikyuuDateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
                List<ReturnDate> returnDate = new List<ReturnDate>();
                List<CheckDate> checkDate = new List<CheckDate>();
                ReturnDate rd = new ReturnDate();
                CheckDate cd = new CheckDate();

                bool bDenpyouKbnCheck = false;

                var denpyouKbnSelect = (from temp in this.form.gcMultiRow1.Rows
                                        where Convert.ToString(temp.Cells["DENPYOU_KBN_NAME"].Value) == "売上"
                                        select temp).ToArray();

                bDenpyouKbnCheck = denpyouKbnSelect != null && denpyouKbnSelect.Length > 0;

                if (bDenpyouKbnCheck == false)
                {
                    return true;
                }

                //nullチェック
                if (string.IsNullOrEmpty(this.form.URIAGE_DATE.Text))
                {
                    return true;
                }
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    return true;
                }

                string strSeikyuuDate = this.form.URIAGE_DATE.Text;
                DateTime seikyuudate = Convert.ToDateTime(strSeikyuuDate);

                cd.CHECK_DATE = seikyuudate;
                cd.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                cd.KYOTEN_CD = this.headerForm.KYOTEN_CD.Text;
                checkDate.Add(cd);
                returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 1);

                if (returnDate.Count == 0)
                {
                    return true;
                }
                else if (returnDate.Count == 1)
                {
                    //例外日付が含まれる
                    if (returnDate[0].dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                    else
                    {
                        if (msgLogic.MessageBoxShow("C084", returnDate[0].dtDATE.ToString("yyyy/MM/dd"), "請求") == DialogResult.Yes)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //例外日付が含まれる
                    foreach (ReturnDate rdDate in returnDate)
                    {
                        if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                        {
                            msgLogic.MessageBoxShow("E214");
                            return false;
                        }
                    }
                    if (msgLogic.MessageBoxShow("C085", "請求") == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SeikyuuDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeikyuuDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }

            return true;
        }
        #endregion

        #region 精算日付チェック
        /// <summary>
        /// 精算日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SeisanDateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
                List<ReturnDate> returnDate = new List<ReturnDate>();
                List<CheckDate> checkDate = new List<CheckDate>();
                ReturnDate rd = new ReturnDate();
                CheckDate cd = new CheckDate();

                bool bDenpyouKbnCheck = false;

                var denpyouKbnSelect = (from temp in this.form.gcMultiRow1.Rows
                                        where Convert.ToString(temp.Cells["DENPYOU_KBN_NAME"].Value) == "支払"
                                        select temp).ToArray();

                bDenpyouKbnCheck = denpyouKbnSelect != null && denpyouKbnSelect.Length > 0;

                if (bDenpyouKbnCheck == false)
                {
                    return true;
                }

                //nullチェック
                if (string.IsNullOrEmpty(this.form.SHIHARAI_DATE.Text))
                {
                    return true;
                }
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    return true;
                }

                string strShiharaiDate = this.form.SHIHARAI_DATE.Text;
                DateTime shiharaidate = Convert.ToDateTime(strShiharaiDate);

                cd.CHECK_DATE = shiharaidate;
                cd.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                cd.KYOTEN_CD = this.headerForm.KYOTEN_CD.Text;
                checkDate.Add(cd);
                returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 2);

                if (returnDate.Count == 0)
                {
                    return true;
                }
                else if (returnDate.Count == 1)
                {
                    //例外日付が含まれる
                    if (returnDate[0].dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                    else
                    {
                        if (msgLogic.MessageBoxShow("C084", returnDate[0].dtDATE.ToString("yyyy/MM/dd"), "支払") == DialogResult.Yes)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //例外日付が含まれる
                    foreach (ReturnDate rdDate in returnDate)
                    {
                        if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                        {
                            msgLogic.MessageBoxShow("E214");
                            return false;
                        }
                    }
                    if (msgLogic.MessageBoxShow("C085", "支払") == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SeisanDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeisanDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion
        /// 20141112 Houkakou 「出荷入力」の締済期間チェックの追加　end

        // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
        #region 車輌休動チェック
        internal bool SharyouDateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
                //運搬業者CD
                workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
                //車輌CD取得
                workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
                //伝票日付取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "車輌", "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SharyouDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion

        #region 運転者休動チェック
        internal bool UntenshaDateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                //取得テータ
                if (workcloseduntenshaList.Count() >= 1)
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "運転者", "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UntenshaDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion

        #region 受付番号チェック
        internal bool UketukeBangoCheck(out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                // 車輌休動
                M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
                //運搬業者CD
                workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
                //車輌CD取得
                workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
                //伝票日付取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                // 運転者休動
                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "車輌",
                                                    "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.SHARYOU_CD.Focus();
                    return false;
                }
                else if (workcloseduntenshaList.Count() >= 1)
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "運転者",
                                                    "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.UNTENSHA_CD.Focus();
                    return false;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UketukeBangoCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UketukeBangoCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 計量番号チェック
        internal bool KeiryouBangoCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                // 車輌休動
                M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
                //運搬業者CD
                workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
                //車輌CD取得
                workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
                //伝票日付取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                // 運転者休動
                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E208", "計量番号", ":" + this.form.KEIRYOU_NUMBER.Text, "車輌",
                                                    "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.SHARYOU_CD.Focus();
                    return false;
                }
                else if (workcloseduntenshaList.Count() >= 1)
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E208", "計量番号", ":" + this.form.KEIRYOU_NUMBER.Text, "運転者",
                                                    "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.UNTENSHA_CD.Focus();
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("KeiryouBangoCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("KeiryouBangoCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion
        // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end

        #region 現金取引チェック
        /// <summary>
        /// 現金取引チェック
        /// </summary>
        /// <returns>
        /// true  = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定以外の場合
        /// false = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定の場合
        /// </returns>
        internal bool GenkinTorihikiCheck(out bool catchErr)
        {
            catchErr = false;
            var ren = true;
            try
            {
                var uriageTorihikiKbn = this.form.txtUri.Text;
                var shiharaiTorihikiKbn = this.form.txtShi.Text;
                short kakuteiFlg = 0;
                if (!string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                    short.TryParse(this.form.KAKUTEI_KBN.Text, out kakuteiFlg);

                var genkin = SalesPaymentConstans.STR_TORIHIKI_KBN_1;
                var uriageRowCount = 0;
                var siharaiRowCount = 0;
                var denpyouKbnCuloumnIndex = this.form.gcMultiRow1.Columns["DENPYOU_KBN_CD"].Index;

                // 売上
                if (uriageTorihikiKbn == genkin)
                {
                    // 明細の売上行数
                    uriageRowCount = this.form.gcMultiRow1.Rows.Cast<GrapeCity.Win.MultiRow.Row>().ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value != null).ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value.ToString() == "1").Count();
                }

                // 支払
                if (shiharaiTorihikiKbn == genkin)
                {
                    // 明細の支払行数
                    siharaiRowCount = this.form.gcMultiRow1.Rows.Cast<GrapeCity.Win.MultiRow.Row>().ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value != null).ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value.ToString() == "2").Count();
                }

                // 確定フラグが2：未確定の場合
                if ((uriageRowCount != 0 || siharaiRowCount != 0) && (kakuteiFlg == SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E236");
                    ren = false;
                }

                return ren;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            return ren;
        }
        #endregion

        #region キャッシャ連動
        /// <summary>
        /// キャッシャ情報送信
        /// </summary>
        private void SendCasher()
        {
            // 売上金額算出※現金の場合のみ
            decimal uriKin = 0;
            if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD == CommonConst.TORIHIKI_KBN_GENKIN)
            {
                // 金額
                decimal kin = (this.dto.entryEntity.URIAGE_AMOUNT_TOTAL.Value + this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.Value);

                // 税
                // 税計算区分が伝票毎の場合は伝票毎消費税を用いる
                decimal tax = 0;
                if (this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD == CommonConst.ZEI_KEISAN_KBN_DENPYOU)
                {
                    // 伝票毎消費税
                    tax = (this.dto.entryEntity.URIAGE_TAX_SOTO.Value + this.dto.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL.Value);
                }
                else
                {
                    // 明細毎消費税合計
                    tax = (this.dto.entryEntity.URIAGE_TAX_SOTO_TOTAL.Value + this.dto.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL.Value);
                }

                // 合計
                uriKin = (kin + tax);
            }

            // 支払金額算出※現金の場合のみ
            decimal shiKin = 0;
            if (this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD == CommonConst.TORIHIKI_KBN_GENKIN)
            {
                // 金額
                decimal kin = (this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL.Value + this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value);

                // 税
                // 税計算区分が伝票毎の場合は伝票毎消費税を用いる
                decimal tax = 0;
                if (this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD == CommonConst.ZEI_KEISAN_KBN_DENPYOU)
                {
                    // 伝票毎消費税
                    tax = (this.dto.entryEntity.SHIHARAI_TAX_SOTO.Value + this.dto.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL.Value);
                }
                else
                {
                    // 明細毎消費税合計
                    tax = (this.dto.entryEntity.SHIHARAI_TAX_SOTO_TOTAL.Value + this.dto.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL.Value);
                }

                // 合計
                shiKin = (kin + tax);
            }

            // 差引０円の場合はキャッシャ情報の送信を行わない
            var kingaku = (uriKin - shiKin);
            if (kingaku != 0)
            {
                // キャッシャ用DTO生成
                var casherDto = new CasherDTOClass();
                casherDto.DENPYOU_DATE = this.dto.entryEntity.DENPYOU_DATE.Value;
                casherDto.NYUURYOKU_TANTOUSHA_CD = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD;
                casherDto.DENPYOU_NUMBER = this.dto.entryEntity.SHUKKA_NUMBER.Value;
                casherDto.KINGAKU = kingaku;
                casherDto.BIKOU = (string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_BIKOU) ? string.Empty : this.dto.entryEntity.DENPYOU_BIKOU);
                casherDto.DENSHU_KBN_CD = CommonConst.DENSHU_KBN_SHUKKA;
                casherDto.KYOTEN_CD = this.dto.entryEntity.KYOTEN_CD.Value;

                // キャッシャ共通処理に情報セット
                var casherAccessor = new CasherAccessor();
                casherAccessor.setCasherData(casherDto);
            }
        }

        #endregion キャッシャ連動

        //ThangNguyen [Add] 20150826 #10907 Start
        private void CheckTorihikisakiShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                bool catchErr = false;
                var torihikisakiEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { throw new Exception(""); }

                if (null != torihikisakiEntity)
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = !(bool)torihikisakiEntity.SHOKUCHI_KBN;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Tag = (bool)torihikisakiEntity.SHOKUCHI_KBN ? this.torihikisakiHintText : string.Empty;
                    if (!this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");
                    }
                }
            }
        }

        private void CheckGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                bool catchErr = false;
                var gyoushaEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { throw new Exception(""); }
                if (null != gyoushaEntity)
                {
                    this.form.GYOUSHA_NAME_RYAKU.ReadOnly = !(bool)gyoushaEntity.SHOKUCHI_KBN;
                    this.form.GYOUSHA_NAME_RYAKU.Tag = (bool)gyoushaEntity.SHOKUCHI_KBN ? this.gyoushaHintText : string.Empty;
                    if (!this.form.GYOUSHA_NAME_RYAKU.ReadOnly)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");
                    }
                }
            }
        }

        private void CheckGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                bool catchErr = false;
                var genbaEntity = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { throw new Exception(""); }
                if (null != genbaEntity)
                {
                    this.form.GENBA_NAME_RYAKU.ReadOnly = !(bool)genbaEntity.SHOKUCHI_KBN;
                    this.form.GENBA_NAME_RYAKU.Tag = (bool)genbaEntity.SHOKUCHI_KBN ? this.genbaHintText : string.Empty;
                    if (!this.form.GENBA_NAME_RYAKU.ReadOnly)
                    {
                        this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");
                    }
                }
            }
        }

        private void CheckNizumiGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                bool catchErr = false;
                var nizumiGyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { throw new Exception(""); }
                if (nizumiGyousha != null)
                {
                    // 20151104 BUNN #12040 STR
                    if (nizumiGyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || nizumiGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151104 BUNN #12040 END
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = !(bool)nizumiGyousha.SHOKUCHI_KBN;
                        this.form.NIZUMI_GYOUSHA_NAME.Tag = (bool)nizumiGyousha.SHOKUCHI_KBN ? this.nizumiGyoushaHintText : string.Empty;
                        if (!this.form.NIZUMI_GYOUSHA_NAME.ReadOnly)
                        {
                            this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");
                        }
                    }
                }
            }
        }

        private void CheckNizumiGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                var genbaEntityList = this.accessor.GetGenbaList(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                M_GENBA genba = new M_GENBA();
                if (genbaEntityList != null && genbaEntityList.Length > 0)
                {
                    genba = genbaEntityList[0];
                    // 事業場区分、現場区分チェック
                    // 20151104 BUNN #12040 STR
                    if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                    // 20151104 BUNN #12040 END
                    {
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = !(bool)genba.SHOKUCHI_KBN;
                        this.form.NIZUMI_GENBA_NAME.Tag = (bool)genba.SHOKUCHI_KBN ? this.nizumiGenbaHintText : string.Empty;
                        if (!this.form.NIZUMI_GENBA_NAME.ReadOnly)
                        {
                            this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");
                        }
                    }
                }
            }
        }

        private void CheckUpanGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                bool catchErr = false;
                var gyousha = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr) { throw new Exception(""); }

                if (gyousha != null)
                {
                    // 20151104 BUNN #12040 STR
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151104 BUNN #12040 END
                    {
                        this.form.UNPAN_GYOUSHA_NAME.ReadOnly = !(bool)gyousha.SHOKUCHI_KBN;
                        this.form.UNPAN_GYOUSHA_NAME.Tag = (bool)gyousha.SHOKUCHI_KBN ? this.unpanGyoushaHintText : string.Empty;
                        if (!this.form.UNPAN_GYOUSHA_NAME.ReadOnly)
                        {
                            this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");
                        }
                    }
                }
            }
        }
        //ThangNguyen [Add] 20150826 #10907 End
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
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
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 更新用受付入力データ取得
        /// </summary>
        /// <remarks>
        /// GetUketsukeNumberメソッドを参考に、受付データ保持部分のみ抽出
        /// </remarks>
        /// <returns></returns>
        internal bool GetUketsukeData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                {
                    return true;
                }

                // 受付（出荷）からデータ取得
                DataTable dt = this.accessor.GetUketsukeSK(this.form.UKETSUKE_NUMBER.Text);
                if (dt.Rows.Count == 0)
                {
                    // データなし
                    // 処理終了
                    return true;
                }

                var haishaJokyoCd = dt.Rows[0]["HAISHA_JOKYO_CD"].ToString();

                // 配車状況が「1:受注」「2:配車」「3:計上」以外は遷移できない
                if (SalesPaymentConstans.HAISHA_JOKYO_CD_CANCEL.Equals(haishaJokyoCd) || SalesPaymentConstans.HAISHA_JOKYO_CD_NASHI.Equals(haishaJokyoCd))
                {
                    // 処理終了
                    return true;
                }

                // 登録時に配車状況を変更するためにエンティティを保存
                var systemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                var seq = dt.Rows[0]["SEQ"].ToString();
                this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("GetUketsukeData", ex1);
                    msgLogic.MessageBoxShow("E093", "");
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("GetUketsukeData", ex);
                    msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 税区分、税計算区分、取引区分をセット

        /// <summary>
        /// 税区分、税計算区分、取引区分をセット
        /// </summary>
        public bool zeiKbnChanged()
        {
            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.dto.entryEntity != null)
            {
                this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn = Convert.ToString(this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn = Convert.ToString(this.dto.entryEntity.URIAGE_ZEI_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn = Convert.ToString(this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn = Convert.ToString(this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn = Convert.ToString(this.dto.entryEntity.SHIHARAI_ZEI_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn = Convert.ToString(this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD);

                if (!this.form.TORIHIKISAKI_CD.Text.Equals(this.dto.entryEntity.TORIHIKISAKI_CD.ToString()))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult dr = msgLogic.MessageBoxShow("C105", "取引先CD", "税計算区分", "税区分", "取引区分");
                    if (dr == DialogResult.OK || dr == DialogResult.Yes)
                    {
                        // 取引先請求情報
                        var torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);

                        if (torihikisakiSeikyuuEntity != null)
                        {
                            this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn = torihikisakiSeikyuuEntity.ZEI_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn = torihikisakiSeikyuuEntity.ZEI_KEISAN_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn = torihikisakiSeikyuuEntity.TORIHIKI_KBN_CD.ToString();
                        }
                        // 取引先支払情報
                        var torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);

                        if (torihikisakiShiharaiEntity != null)
                        {
                            this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn = torihikisakiShiharaiEntity.ZEI_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn = torihikisakiShiharaiEntity.ZEI_KEISAN_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn = torihikisakiShiharaiEntity.TORIHIKI_KBN_CD.ToString();
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region 連携チェック
        internal bool RenkeiCheck(string uketsukeNum)
        {
            try
            {
                if (string.IsNullOrEmpty(uketsukeNum))
                {
                    return true;
                }

                DataTable dt = this.mobisyoRtDao.GetRenkeiData("1", uketsukeNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.msgLogic.MessageBoxShow("E262", "現在運搬中", "完了後、実績取込にて、売上/支払データを作成");
                    return false;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 2"  // 2：出荷受付
                    + " and LDD.REF_DENPYOU_NO = " + uketsukeNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.dao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.msgLogic.MessageBoxShow("E261", "ロジこんぱす連携中", "呼出し");
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RenkeiCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RenkeiCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 複写モードチェック
        /// <summary>
        /// 複写モードで開かれたかチェックする
        /// </summary>
        /// <returns></returns>
        private bool copyModeCheck()
        {
            if ((WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.ShukkaNumber != -1) && !this.dto.entryEntity.KAKUTEI_KBN.IsNull)
            {
                // 複写モード
                return true;
            }
            else
            {
                // それ以外
                return false;
            }
        }
        #endregion

        // MAILAN #158992 START
        internal void ResetTankaCheck()
        {
            this.isTankaMessageShown = false;
            this.isCheckTankaFromChild = false;
        }
        // MAILAN #158992 END

        //20210825 Thanh 154360 s
        /// <summary>
        /// CheckDetailShukkaAndKenshyuu
        /// </summary>
        /// <returns></returns>
        internal bool CheckDetailShukkaAndKenshu()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            int RowCountShukka = this.form.gcMultiRow1.Rows.Count - 1;
            if (this.dto.kenshuNyuuryokuDto != null)
            {
                int RowCountKenshu = this.dto.kenshuNyuuryokuDto.kenshuDetailList.Count;
                if (RowCountKenshu > 0)
                {
                    if (RowCountKenshu != RowCountShukka)
                    {
                        msgLogic.MessageBoxShowError("出荷明細と検収明細の数が等しくありません。\n検収入力をやり直してから登録してください。");
                        ret = false;
                    }
                }
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }
        //20210825 Thanh 154360 e
    }
}