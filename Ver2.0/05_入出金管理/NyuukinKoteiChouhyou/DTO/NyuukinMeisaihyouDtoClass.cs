﻿namespace Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou
{
    /// <summary>
    /// 入金明細表出力で使用するDto（主に検索条件として使用）
    /// </summary>
    public class NyuukinMeisaihyouDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public NyuukinMeisaihyouDtoClass()
        {
            KyotenCd = 99;
            KyotenName = null;
            DateFrom = null;
            DateTo = null;
            NyuukinsakiCdFrom = null;
            NyuukinsakiFrom = null;
            NyuukinsakiCdTo = null;
            NyuukinsakiTo = null;
            TorihikisakiCdFrom = null;
            TorihikisakiFrom = null;
            TorihikisakiCdTo = null;
            TorihikisakiTo = null;
            BankCdFrom = null;
            BankFrom = null;
            BankCdTo = null;
            BankTo = null;
            BankShitenCdFrom = null;
            BankShitenFrom = null;
            BankShitenCdTo = null;
            BankShitenTo = null;
            Sort1 = 1;
            Sort2 = 1;
            IsGroupDenpyouNumber = true;
            IsGroupTorihikisaki = true;
            IsGroupNyuukinsaki = true;
            IsGroupNyuushukkinKbn = true;
        }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public int KyotenCd { get; set; }

        /// <summary>
        /// 拠点名称を取得・設定します
        /// </summary>
        public string KyotenName { get; set; }

        /// <summary>
        /// 日付種類CDを取得・設定します
        /// </summary>
        public int DateShuruiCd { get; set; }

        /// <summary>
        /// 日付Fromを取得・設定します
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// 日付Toを取得・設定します
        /// </summary>
        public string DateTo { get; set; }

        /// <summary>
        /// 入金先CDFromを取得・設定します
        /// </summary>
        public string NyuukinsakiCdFrom { get; set; }

        /// <summary>
        /// 入金先Fromを取得・設定します
        /// </summary>
        public string NyuukinsakiFrom { get; set; }

        /// <summary>
        /// 入金先CDToを取得・設定します
        /// </summary>
        public string NyuukinsakiCdTo { get; set; }

        /// <summary>
        /// 入金先Toを取得・設定します
        /// </summary>
        public string NyuukinsakiTo { get; set; }

        /// <summary>
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 取引先Fromを取得・設定します
        /// </summary>
        public string TorihikisakiFrom { get; set; }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 取引先Toを取得・設定します
        /// </summary>
        public string TorihikisakiTo { get; set; }

        /// <summary>
        /// 銀行CDFromを取得・設定します
        /// </summary>
        public string BankCdFrom { get; set; }

        /// <summary>
        /// 銀行Fromを取得・設定します
        /// </summary>
        public string BankFrom { get; set; }

        /// <summary>
        /// 銀行CDToを取得・設定します
        /// </summary>
        public string BankCdTo { get; set; }

        /// <summary>
        /// 銀行Toを取得・設定します
        /// </summary>
        public string BankTo { get; set; }

        /// <summary>
        /// 銀行支店CDFromを取得・設定します
        /// </summary>
        public string BankShitenCdFrom { get; set; }

        /// <summary>
        /// 銀行支店Fromを取得・設定します
        /// </summary>
        public string BankShitenFrom { get; set; }

        /// <summary>
        /// 銀行支店CDToを取得・設定します
        /// </summary>
        public string BankShitenCdTo { get; set; }

        /// <summary>
        /// 銀行支店Toを取得・設定します
        /// </summary>
        public string BankShitenTo { get; set; }

        /// <summary>
        /// 並び順１を取得・設定します
        /// </summary>
        public int Sort1 { get; set; }

        /// <summary>
        /// 並び順２を取得・設定します
        /// </summary>
        public int Sort2 { get; set; }

        /// <summary>
        /// 伝票番号単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupDenpyouNumber { get; set; }

        /// <summary>
        /// 取引先単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupTorihikisaki { get; set; }

        /// <summary>
        /// 入金先単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupNyuukinsaki { get; set; }

        /// <summary>
        /// 入出金区分で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupNyuushukkinKbn { get; set; }
    }
}
