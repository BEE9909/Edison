﻿using System;
using System.Data.SqlTypes;

namespace Shougun.Core.SalesPayment.UriageShukeiHyo
{
    /// <summary>
    /// 売上データエンティティ
    /// </summary>
    public class UriageData
    {
        /// <summary>
        /// 取引区分CDを取得・設定します
        /// </summary>
        public String URIAGE_TORIHIKI_KBN_CD { get; set; }

        /// <summary>
        /// 取引区分を取得・設定します
        /// </summary>
        public String URIAGE_TORIHIKI_KBN_NAME { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public String KYOTEN_CD { get; set; }

        /// <summary>
        /// 拠点を取得・設定します
        /// </summary>
        public String KYOTEN_NAME { get; set; }

        /// <summary>
        /// 取引先CDを取得・設定します
        /// </summary>
        public String TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 取引先を取得・設定します
        /// </summary>
        public String TORIHIKISAKI_NAME { get; set; }

        /// <summary>
        /// 業者CDを取得・設定します
        /// </summary>
        public String GYOUSHA_CD { get; set; }

        /// <summary>
        /// 業者を取得・設定します
        /// </summary>
        public String GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 現場CDを取得・設定します
        /// </summary>
        public String GENBA_CD { get; set; }

        /// <summary>
        /// 現場を取得・設定します
        /// </summary>
        public String GENBA_NAME { get; set; }

        /// <summary>
        /// 荷降業者CDを取得・設定します
        /// </summary>
        public String NIOROSHI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷降業者を取得・設定します
        /// </summary>
        public String NIOROSHI_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 荷降現場CDを取得・設定します
        /// </summary>
        public String NIOROSHI_GENBA_CD { get; set; }

        /// <summary>
        /// 荷降現場を取得・設定します
        /// </summary>
        public String NIOROSHI_GENBA_NAME { get; set; }

        /// <summary>
        /// 荷積業者CDを取得・設定します
        /// </summary>
        public String NIZUMI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷積業者を取得・設定します
        /// </summary>
        public String NIZUMI_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 荷積現場CDを取得・設定します
        /// </summary>
        public String NIZUMI_GENBA_CD { get; set; }

        /// <summary>
        /// 荷積現場を取得・設定します
        /// </summary>
        public String NIZUMI_GENBA_NAME { get; set; }

        /// <summary>
        /// 営業担当者CDを取得・設定します
        /// </summary>
        public String EIGYOU_TANTOUSHA_CD { get; set; }

        /// <summary>
        /// 営業担当者を取得・設定します
        /// </summary>
        public String EIGYOU_TANTOUSHA_NAME { get; set; }

        /// <summary>
        /// 入力担当者CDを取得・設定します
        /// </summary>
        public String NYUURYOKU_TANTOUSHA_CD { get; set; }

        /// <summary>
        /// 入力担当者を取得・設定します
        /// </summary>
        public String NYUURYOKU_TANTOUSHA_NAME { get; set; }

        /// <summary>
        /// 車輌CDを取得・設定します
        /// </summary>
        public String SHARYOU_CD { get; set; }

        /// <summary>
        /// 車輌を取得・設定します
        /// </summary>
        public String SHARYOU_NAME { get; set; }

        /// <summary>
        /// 車種CDを取得・設定します
        /// </summary>
        public String SHASHU_CD { get; set; }

        /// <summary>
        /// 車種を取得・設定します
        /// </summary>
        public String SHASHU_NAME { get; set; }

        /// <summary>
        /// 運搬業者CDを取得・設定します
        /// </summary>
        public String UNPAN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 運搬業者を取得・設定します
        /// </summary>
        public String UNPAN_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 運転者CDを取得・設定します
        /// </summary>
        public String UNTENSHA_CD { get; set; }

        /// <summary>
        /// 運転者を取得・設定します
        /// </summary>
        public String UNTENSHA_NAME { get; set; }

        /// <summary>
        /// 形態区分CDを取得・設定します
        /// </summary>
        public String KEITAI_KBN_CD { get; set; }

        /// <summary>
        /// 形態区分を取得・設定します
        /// </summary>
        public String KEITAI_KBN_NAME { get; set; }

        /// <summary>
        /// 台貫区分CDを取得・設定します
        /// </summary>
        public String DAIKAN_KBN_CD { get; set; }

        /// <summary>
        /// 台貫区分を取得・設定します
        /// </summary>
        public String DAIKAN_KBN_NAME { get; set; }

        /// <summary>
        /// 品名CDを取得・設定します
        /// </summary>
        public String HINMEI_CD { get; set; }

        /// <summary>
        /// 品名を取得・設定します
        /// </summary>
        public String HINMEI_NAME { get; set; }

        /// <summary>
        /// 正味重量を取得・設定します
        /// </summary>
        public SqlDecimal NET_JYUURYOU { get; set; }

        /// <summary>
        /// 数量を取得・設定します
        /// </summary>
        public SqlDecimal SUURYOU { get; set; }

        /// <summary>
        /// 単位CDを取得・設定します
        /// </summary>
        public String UNIT_CD { get; set; }

        /// <summary>
        /// 単位を取得・設定します
        /// </summary>
        public String UNIT_NAME { get; set; }

        /// <summary>
        /// 金額を取得・設定します
        /// </summary>
        public SqlDecimal KINGAKU { get; set; }

        /// <summary>
        /// 種類CDを取得・設定します
        /// </summary>
        public String SHURUI_CD { get; set; }

        /// <summary>
        /// 種類を取得・設定します
        /// </summary>
        public String SHURUI_NAME { get; set; }

        /// <summary>
        /// 分類CDを取得・設定します
        /// </summary>
        public String BUNRUI_CD { get; set; }

        /// <summary>
        /// 分類を取得・設定します
        /// </summary>
        public String BUNRUI_NAME { get; set; }

        //MOD VAN 20200323 #133167 S
        /// <summary>
        /// 伝票日付を取得・設定します
        /// </summary>
        public String DENPYOU_DATE { get; set; }

        /// <summary>
        /// 売上日付を取得・設定します
        /// </summary>
        public String URIAGE_DATE { get; set; }

        /// <summary>
        /// 入力日付を取得・設定します
        /// </summary>
        public String UPDATE_DATE { get; set; }
        //MOD VAN 20200323 #133167 E

        //PhuocLoc 2020/12/08 #136223 -Start
        /// <summary>
        /// 集計項目CDを取得・設定します
        /// </summary>
        public String MOD_SHUUKEI_KOUMOKU_CD { get; set; }

        /// <summary>
        /// 集計項目を取得・設定します
        /// </summary>
        public String MOD_SHUUKEI_KOUMOKU_NAME { get; set; }
        //PhuocLoc 2020/12/08 #136223 -End
    }
}
