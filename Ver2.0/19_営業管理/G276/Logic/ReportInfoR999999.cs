using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Utility;
namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
{
    #region - Class -

    /// <summary>(R425・R508・R547・R548)見積書を表すクラス・コントロール</summary>
    public class ReportInfoR999999 : ReportInfoBase
    {
        #region - Fields -

        private const int ConstMaxDispDetailHRowCount = 10;           // 金額見積もり横のDetail1の最大表示行数
        private const int ConstMaxDispDetailVRowCount = 16;           // 金額見積もり縦のDetail1の最大表示行数
        private const int ConstMaxDispTankaDetailHRowCount = 11;      // 単価見積もり横のDetail1の最大表示行数
        private const int ConstMaxDispTankaDetailVRowCount = 19;      // 単価見積もり縦のDetail1の最大表示行数
        //quoc-begin
        string denpyou_kbn = "";
        //quoc-end
        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR425_R508_R547_R548"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR999999(WINDOW_ID windowID)
        {
            this.windowID = windowID;

            this.OutputType = OutputTypeDef.KingakuMitsumoriH;      // 金額見積り（横、R508）

            this.SetRecord(this.dataTable);
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>出力タイプに関する列挙型</summary>
        public enum OutputTypeDef
        {
            /// <summary>金額見積り（縦)</summary>
            /// kingaku mitsumori ( tate )
            /// Ước tính số tiền (Dọc)
            KingakuMitsumoriV,

            /// <summary>金額見積り（横）</summary>
            /// kingaku mitsumori ( yoko )
            /// Công cụ ước tính số tiền (ngang)
            KingakuMitsumoriH,

            /// <summary>単価見積り（縦）</summary>
            /// tanka mitsumori ( tate )
            /// Công cụ ước tính đơn giá (Dọc)
            TankaMitsumoriV,

            /// <summary>単価見積り（縦）</summary>
            /// tanka mitsumori ( tate )
            /// Công cụ ước tính đơn giá (Dọc)
            TankaMitsumoriH,
        }

        #endregion - Enums -

        #region - Properties -

        /// <summary>出力タイプを保持するプロパティ</summary>
        /// <remarks>
        /// 金額見積り（縦）: KingakuMitsumoriV,
        /// 金額見積り（横）: KingakuMitsumoriH,
        /// 単価見積り（縦）: TankaMitsumoriV,
        /// 単価見積り（縦）: TankaMitsumoriH,
        /// </remarks>
        public OutputTypeDef OutputType { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>サンプルデータの作成処理を実行する(Thực hiện quy trình tạo dữ liệu mẫu)</summary>
        public void CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;

            bool isPrint = true;
            bool isPrintH = true;

            for (int pageNo = 1; pageNo <= 1; pageNo++)
            {
                this.DataTablePageList[pageNo.ToString()] = new Dictionary<string, DataTable>();

                switch (this.OutputType)
                {
                    case OutputTypeDef.KingakuMitsumoriV:   // 金額見積り（縦）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE_FLB");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");

                        //quoc-begin
                        dataTableTmp.Columns.Add("MOD_KAKUIN_INJI");
                        //quoc-end


                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");
                        // 業者名1(＋業者敬称1)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME1");
                        // 業者名2(＋業者敬称2)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME2");
                        // 現場名1(＋現場敬称1)
                        dataTableTmp.Columns.Add("GENBA_NAME1");
                        // 現場名2(＋現場敬称2)
                        dataTableTmp.Columns.Add("GENBA_NAME2");
                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");
                        // 201400708 syunrei ＃947　№13　　start
                        // 部署名ラベル
                        dataTableTmp.Columns.Add("BUSHO_NAME_LABEL");
                        // 201400708 syunrei ＃947　№13　　end
                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");
                        // 合計金額
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU");

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 税区分
                        dataTableTmp.Columns.Add("ZEI_KBN_CD");
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                        if (isPrintH)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // タイトル
                            rowTmp["TITLE_FLB"] = "a";
                            // 見積書番号
                            rowTmp["MITSUMORI_NUMBER"] = "1234567890";
                            // 見積日付
                            rowTmp["MITSUMORI_DATE"] = "2013/12/10 12:00:00";
                            // 取引先名1(＋取引先敬称1)
                            rowTmp["TORIHIKISAKI_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 取引先名2(＋取引先敬称2)
                            rowTmp["TORIHIKISAKI_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 業者名1(＋業者敬称1)
                            rowTmp["GYOUSHA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 業者名2(＋業者敬称2)
                            rowTmp["GYOUSHA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 現場名1(＋現場敬称1)
                            rowTmp["GENBA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 現場名2(＋現場敬称2)
                            rowTmp["GENBA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 件名
                            rowTmp["KENMEI"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 見積項目名称1
                            rowTmp["MITSUMORI_KOUMOKU1"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目1
                            rowTmp["MITSUMORI_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 見積項目名称2
                            rowTmp["MITSUMORI_KOUMOKU2"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目2
                            rowTmp["MITSUMORI_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 見積項目名称3
                            rowTmp["MITSUMORI_KOUMOKU3"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目3
                            rowTmp["MITSUMORI_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 見積項目名称4
                            rowTmp["MITSUMORI_KOUMOKU4"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目4
                            rowTmp["MITSUMORI_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 会社名
                            rowTmp["CORP_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 代表者
                            rowTmp["CORP_DAIHYOU"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 印字拠点名1
                            rowTmp["KYOTEN_NAME_1"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 印字拠点郵便番号1
                            rowTmp["KYOTEN_POST_1"] = "123456789012345";
                            // 印字拠点住所1_1
                            rowTmp["KYOTEN_ADDRESS1_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点住所2_1
                            rowTmp["KYOTEN_ADDRESS2_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点TEL1
                            rowTmp["KYOTEN_TEL_1"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_1"] = "123456789012345";
                            // 印字拠点名2
                            rowTmp["KYOTEN_NAME_2"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 印字拠点郵便番号2
                            rowTmp["KYOTEN_POST_2"] = "123456789012345";
                            // 印字拠点住所1_2
                            rowTmp["KYOTEN_ADDRESS1_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点住所2_2
                            rowTmp["KYOTEN_ADDRESS2_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点TEL2
                            rowTmp["KYOTEN_TEL_2"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_2"] = "123456789012345";
                            // 部署名
                            rowTmp["BUSHO_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 営業担当者名
                            rowTmp["EIGYO_TANTOUSHA_NAME"] = "あいうえおかきくけこさしすせそ";
                            // 見積書文言
                            rowTmp["MITSUMORI_SENTENSE"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 合計金額
                            rowTmp["GOUKEI_KINGAKU"] = "123,456,789,000";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                            // 税区分
                            rowTmp["ZEI_KBN_CD"] = "1234567890";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Header", dataTableTmp);

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 金額
                        dataTableTmp.Columns.Add("KINGAKU");
                        // 品名別税区分
                        dataTableTmp.Columns.Add("HINMEI_ZEI_KBN_CD");
                        // 消費税
                        dataTableTmp.Columns.Add("TAX");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");

                        if (isPrint)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // №
                                rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}", pageNo, i + 1);
                                // 品名
                                rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                // 数量
                                rowTmp["SUURYOU"] = "123,456,789,000,123,456";
                                // 単位
                                rowTmp["UNIT_NAME"] = "あいうえお";
                                // 単価
                                rowTmp["TANKA"] = "123,456,789,000";
                                // 金額
                                rowTmp["KINGAKU"] = "123456789000";
                                // 品名別税区分
                                rowTmp["HINMEI_ZEI_KBN_CD"] = "あいうえお";
                                // 消費税
                                rowTmp["TAX"] = "123456789000";
                                // 備考
                                rowTmp["MEISAI_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";

                                dataTableTmp.Rows.Add(rowTmp);
                            }
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);

                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 合計(内税込)
                        dataTableTmp.Columns.Add("KINGAKU_TOTAL");
                        // 消費税(外税)
                        dataTableTmp.Columns.Add("TAX_SOTO");
                        // 課税対象額
                        dataTableTmp.Columns.Add("PRICE_PROPER");
                        // 総合計
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU_TOTAL");
                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        if (isPrint)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 合計(内税込)
                            rowTmp["KINGAKU_TOTAL"] = "123456789000";
                            // 消費税(外税)
                            rowTmp["TAX_SOTO"] = "123456789000";
                            // 課税対象額
                            rowTmp["PRICE_PROPER"] = "123456789000";
                            // 総合計
                            rowTmp["GOUKEI_KINGAKU_TOTAL"] = "123456789000";
                            // 備考1
                            rowTmp["BIKOU_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考2
                            rowTmp["BIKOU_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考3
                            rowTmp["BIKOU_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考4
                            rowTmp["BIKOU_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考5
                            rowTmp["BIKOU_5"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                    case OutputTypeDef.KingakuMitsumoriH:   // 金額見積り（横）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");
                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");
                        // 業者名1(＋業者敬称1)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME1");
                        // 業者名2(＋業者敬称2)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME2");
                        // 現場名1(＋現場敬称1)
                        dataTableTmp.Columns.Add("GENBA_NAME1");
                        // 現場名2(＋現場敬称2)
                        dataTableTmp.Columns.Add("GENBA_NAME2");
                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");

                        // 201400709 syunrei #947 №13　start
                        dataTableTmp.Columns.Add("BUSHO_NAME_LABEL");
                        // 201400709 syunrei #947 №13　end
                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");
                        // 合計金額
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU");

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 税区分
                        dataTableTmp.Columns.Add("ZEI_KBN_CD");
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                        if (isPrintH)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // タイトル
                            rowTmp["TITLE"] = "a";
                            // 見積書番号
                            rowTmp["MITSUMORI_NUMBER"] = "1234567890";
                            // 見積日付
                            rowTmp["MITSUMORI_DATE"] = "2013/12/10 12:00:00";
                            // 取引先名1(＋取引先敬称1)
                            rowTmp["TORIHIKISAKI_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 取引先名2(＋取引先敬称2)
                            rowTmp["TORIHIKISAKI_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 業者名1(＋業者敬称1)
                            rowTmp["GYOUSHA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 業者名2(＋業者敬称2)
                            rowTmp["GYOUSHA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 現場名1(＋現場敬称1)
                            rowTmp["GENBA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 現場名2(＋現場敬称2)
                            rowTmp["GENBA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                            // 件名
                            rowTmp["KENMEI"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 見積項目名称1
                            rowTmp["MITSUMORI_KOUMOKU1"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目1
                            rowTmp["MITSUMORI_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 見積項目名称2
                            rowTmp["MITSUMORI_KOUMOKU2"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目2
                            rowTmp["MITSUMORI_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 見積項目名称3
                            rowTmp["MITSUMORI_KOUMOKU3"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目3
                            rowTmp["MITSUMORI_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 見積項目名称4
                            rowTmp["MITSUMORI_KOUMOKU4"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目4
                            rowTmp["MITSUMORI_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 会社名
                            rowTmp["CORP_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 代表者
                            rowTmp["CORP_DAIHYOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点名1
                            rowTmp["KYOTEN_NAME_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点郵便番号1
                            rowTmp["KYOTEN_POST_1"] = "123456789012345";
                            // 印字拠点住所1_1
                            rowTmp["KYOTEN_ADDRESS1_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点住所2_1
                            rowTmp["KYOTEN_ADDRESS2_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点TEL1
                            rowTmp["KYOTEN_TEL_1"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_1"] = "123456789012345";
                            // 印字拠点名2
                            rowTmp["KYOTEN_NAME_2"] = "あいうえおかきくけこさしすせそ";
                            // 印字拠点郵便番号2
                            rowTmp["KYOTEN_POST_2"] = "123456789012345";
                            // 印字拠点住所1_2
                            rowTmp["KYOTEN_ADDRESS1_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点住所2_2
                            rowTmp["KYOTEN_ADDRESS2_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 印字拠点TEL2
                            rowTmp["KYOTEN_TEL_2"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_2"] = "123456789012345";
                            // 部署名
                            rowTmp["BUSHO_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 営業担当者名
                            rowTmp["EIGYO_TANTOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 見積書文言
                            rowTmp["MITSUMORI_SENTENSE"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 合計金額
                            rowTmp["GOUKEI_KINGAKU"] = "123,456,789,000,123,456";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                            // 税区分
                            rowTmp["ZEI_KBN_CD"] = "1234567890";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()]["Header"] = dataTableTmp;

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 金額
                        dataTableTmp.Columns.Add("KINGAKU");
                        // 品名別税区分
                        dataTableTmp.Columns.Add("HINMEI_ZEI_KBN_CD");
                        // 消費税
                        dataTableTmp.Columns.Add("TAX");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");

                        if (isPrint)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // №
                                rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}", pageNo, i + 1);
                                // 品名
                                rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                // 数量
                                rowTmp["SUURYOU"] = "123,456,789,000,123,456";
                                // 単位
                                rowTmp["UNIT_NAME"] = "あいうえお";
                                // 単価
                                rowTmp["TANKA"] = "123,456,789,000,123,456";
                                // 金額
                                rowTmp["KINGAKU"] = "123456789000";
                                // 品名別税区分
                                rowTmp["HINMEI_ZEI_KBN_CD"] = "あいうえお";
                                // 消費税
                                rowTmp["TAX"] = "123456789000";
                                // 備考
                                rowTmp["MEISAI_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";

                                dataTableTmp.Rows.Add(rowTmp);
                            }
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);

                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 合計(内税込)
                        dataTableTmp.Columns.Add("KINGAKU_TOTAL");
                        // 消費税(外税)
                        dataTableTmp.Columns.Add("TAX_SOTO");
                        // 課税対象額
                        dataTableTmp.Columns.Add("PRICE_PROPER");
                        // 総合計
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU_TOTAL");
                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        if (isPrint)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 合計(内税込)
                            rowTmp["KINGAKU_TOTAL"] = "123456789000";
                            // 消費税(外税)
                            rowTmp["TAX_SOTO"] = "123456789000";
                            // 課税対象額
                            rowTmp["PRICE_PROPER"] = "123456789000";
                            // 総合計
                            rowTmp["GOUKEI_KINGAKU_TOTAL"] = "123456789000";
                            // 備考1
                            rowTmp["BIKOU_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考2
                            rowTmp["BIKOU_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考3
                            rowTmp["BIKOU_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考4
                            rowTmp["BIKOU_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 備考5
                            rowTmp["BIKOU_5"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                    case OutputTypeDef.TankaMitsumoriV:     // 単価見積り（縦）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");
                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");
                        // 業者名1(＋業者敬称1)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME1");
                        // 業者名2(＋業者敬称2)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME2");
                        // 現場名1(＋現場敬称1)
                        dataTableTmp.Columns.Add("GENBA_NAME1");
                        // 現場名2(＋現場敬称2)
                        dataTableTmp.Columns.Add("GENBA_NAME2");
                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");

                        // 201400709 syunrei #947 №13　start
                        dataTableTmp.Columns.Add("BUSHO_NAME_LABEL");
                        // 201400709 syunrei #947 №13　end

                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 合計金額
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU");

                        // 税区分
                        dataTableTmp.Columns.Add("ZEI_KBN_CD");
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                        if (isPrintH)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // タイトル
                            rowTmp["TITLE"] = "a";
                            // 見積書番号
                            rowTmp["MITSUMORI_NUMBER"] = "1234567890";
                            // 見積日付
                            rowTmp["MITSUMORI_DATE"] = "2013/12/10 12:00:00";
                            // 取引先名1(＋取引先敬称1)
                            rowTmp["TORIHIKISAKI_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 取引先名2(＋取引先敬称2)
                            rowTmp["TORIHIKISAKI_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 業者名1(＋業者敬称1)
                            rowTmp["GYOUSHA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 業者名2(＋業者敬称2)
                            rowTmp["GYOUSHA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 現場名1(＋現場敬称1)
                            rowTmp["GENBA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 現場名2(＋現場敬称2)
                            rowTmp["GENBA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 件名
                            rowTmp["KENMEI"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 見積項目名称1
                            rowTmp["MITSUMORI_KOUMOKU1"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目1
                            rowTmp["MITSUMORI_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積項目名称2
                            rowTmp["MITSUMORI_KOUMOKU2"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目2
                            rowTmp["MITSUMORI_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積項目名称3
                            rowTmp["MITSUMORI_KOUMOKU3"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目3
                            rowTmp["MITSUMORI_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積項目名称4
                            rowTmp["MITSUMORI_KOUMOKU4"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目4
                            rowTmp["MITSUMORI_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 会社名
                            rowTmp["CORP_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 代表者
                            rowTmp["CORP_DAIHYOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点名1
                            rowTmp["KYOTEN_NAME_1"] = "あいうえおかきくけこさしすせそ";
                            // 印字拠点郵便番号1
                            rowTmp["KYOTEN_POST_1"] = "123456789012345";
                            // 印字拠点住所1_1
                            rowTmp["KYOTEN_ADDRESS1_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点住所2_1
                            rowTmp["KYOTEN_ADDRESS2_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点TEL1
                            rowTmp["KYOTEN_TEL_1"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_1"] = "123456789012345";
                            // 印字拠点名2
                            rowTmp["KYOTEN_NAME_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点郵便番号2
                            rowTmp["KYOTEN_POST_2"] = "123456789012345";
                            // 印字拠点住所1_2
                            rowTmp["KYOTEN_ADDRESS1_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点住所2_2
                            rowTmp["KYOTEN_ADDRESS2_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点TEL2
                            rowTmp["KYOTEN_TEL_2"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_2"] = "123456789012345";
                            // 部署名
                            rowTmp["BUSHO_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 営業担当者名
                            rowTmp["EIGYO_TANTOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積書文言
                            rowTmp["MITSUMORI_SENTENSE"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start

                            // 合計金額
                            rowTmp["GOUKEI_KINGAKU"] = "123,456,789,000,123,456";

                            // 税区分
                            rowTmp["ZEI_KBN_CD"] = "1234567890";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Header", dataTableTmp);

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");

                        if (isPrint)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // №
                                rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}", pageNo, i + 1);
                                // 品名
                                rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                                // 数量
                                rowTmp["SUURYOU"] = "123,456,789,000,123,456";
                                // 単位
                                rowTmp["UNIT_NAME"] = "あいうえお";
                                // 単価
                                rowTmp["TANKA"] = "123,456,789,000,123,456";
                                // 備考
                                rowTmp["MEISAI_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";

                                dataTableTmp.Rows.Add(rowTmp);
                            }
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);

                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        if (isPrint)
                        {
                            rowTmp = dataTableTmp.NewRow();
                            // 備考1
                            rowTmp["BIKOU_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考2
                            rowTmp["BIKOU_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考3
                            rowTmp["BIKOU_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考4
                            rowTmp["BIKOU_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考5
                            rowTmp["BIKOU_5"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                    case OutputTypeDef.TankaMitsumoriH:     // 単価見積り（横）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");
                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");
                        // 業者名1(＋業者敬称1)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME1");
                        // 業者名2(＋業者敬称2)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME2");
                        // 現場名1(＋現場敬称1)
                        dataTableTmp.Columns.Add("GENBA_NAME1");
                        // 現場名2(＋現場敬称2)
                        dataTableTmp.Columns.Add("GENBA_NAME2");
                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");
                        // 201400709 syunrei #947 №13　start
                        dataTableTmp.Columns.Add("BUSHO_NAME_LABEL");
                        // 201400709 syunrei #947 №13　end
                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");


                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 合計金額
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU");

                        // 税区分
                        dataTableTmp.Columns.Add("ZEI_KBN_CD");
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                        if (isPrintH)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // タイトル
                            rowTmp["TITLE"] = "a";
                            // 見積書番号
                            rowTmp["MITSUMORI_NUMBER"] = "1234567890";
                            // 見積日付
                            rowTmp["MITSUMORI_DATE"] = "2013/12/10 12:00:00";
                            // 取引先名1(＋取引先敬称1)
                            rowTmp["TORIHIKISAKI_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 取引先名2(＋取引先敬称2)
                            rowTmp["TORIHIKISAKI_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 業者名1(＋業者敬称1)
                            rowTmp["GYOUSHA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 業者名2(＋業者敬称2)
                            rowTmp["GYOUSHA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 現場名1(＋現場敬称1)
                            rowTmp["GENBA_NAME1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 現場名2(＋現場敬称2)
                            rowTmp["GENBA_NAME2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 件名
                            rowTmp["KENMEI"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積項目名称1
                            rowTmp["MITSUMORI_KOUMOKU1"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目1
                            rowTmp["MITSUMORI_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積項目名称2
                            rowTmp["MITSUMORI_KOUMOKU2"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目2
                            rowTmp["MITSUMORI_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積項目名称3
                            rowTmp["MITSUMORI_KOUMOKU3"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目3
                            rowTmp["MITSUMORI_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積項目名称4
                            rowTmp["MITSUMORI_KOUMOKU4"] = "あいうえおかきくけこさしすせそ";
                            // 見積項目4
                            rowTmp["MITSUMORI_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 会社名
                            rowTmp["CORP_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 代表者
                            rowTmp["CORP_DAIHYOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点名1
                            rowTmp["KYOTEN_NAME_1"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 印字拠点郵便番号1
                            rowTmp["KYOTEN_POST_1"] = "123456789012345";
                            // 印字拠点住所1_1
                            rowTmp["KYOTEN_ADDRESS1_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点住所2_1
                            rowTmp["KYOTEN_ADDRESS2_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点TEL1
                            rowTmp["KYOTEN_TEL_1"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_1"] = "123456789012345";
                            // 印字拠点名2
                            rowTmp["KYOTEN_NAME_2"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 印字拠点郵便番号2
                            rowTmp["KYOTEN_POST_2"] = "123456789012345";
                            // 印字拠点住所1_2
                            rowTmp["KYOTEN_ADDRESS1_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点住所2_2
                            rowTmp["KYOTEN_ADDRESS2_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 印字拠点TEL2
                            rowTmp["KYOTEN_TEL_2"] = "123456789012345";
                            // 印字拠点FAX2
                            rowTmp["KYOTEN_FAXL_2"] = "123456789012345";
                            // 部署名
                            rowTmp["BUSHO_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 営業担当者名
                            rowTmp["EIGYO_TANTOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 見積書文言
                            rowTmp["MITSUMORI_SENTENSE"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                            // 合計金額
                            rowTmp["GOUKEI_KINGAKU"] = "123,456,789,000,123,456";

                            // 税区分
                            rowTmp["ZEI_KBN_CD"] = "1234567890";

                            // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Header", dataTableTmp);

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");

                        if (isPrint)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // №
                                rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}", pageNo, i + 1);
                                // 品名
                                rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                                // 数量
                                rowTmp["SUURYOU"] = "123,456,789,000,123,456";
                                // 単位
                                rowTmp["UNIT_NAME"] = "あいうえお";
                                // 単価
                                rowTmp["TANKA"] = "123,456,789,000,123,456";
                                // 備考
                                rowTmp["MEISAI_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";

                                dataTableTmp.Rows.Add(rowTmp);
                            }
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);

                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        if (isPrint)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 備考1
                            rowTmp["BIKOU_1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考2
                            rowTmp["BIKOU_2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考3
                            rowTmp["BIKOU_3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考4
                            rowTmp["BIKOU_4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";
                            // 備考5
                            rowTmp["BIKOU_5"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめも";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                }
            }
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int rowNo = 1;
            int i;
            DataRow row = null;
            string ctrlName = string.Empty;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            for (int pageNo = 1; pageNo <= this.DataTablePageList.Count; pageNo++)
            {
                int maxPage;
                bool detailComp = false;
                DataTable dataTableTmp = this.DataTablePageList[pageNo.ToString()]["Detail"];
                int detailMaxCount = dataTableTmp.Rows.Count;
                int detailStart = 0;
                //quoc-begin
                DataTable dataTableHeaderTmp = this.DataTablePageList[pageNo.ToString()]["Header"];
                DataTable dataTableFooterTmp = this.DataTablePageList[pageNo.ToString()]["Footer"];
                //quoc-end
                int maxRow = 0;

                // 帳票出力用データの設定処理
                this.SetChouhyouInfo(pageNo);

                #region - Detail -

                switch (this.OutputType)
                {
                    case OutputTypeDef.KingakuMitsumoriV:       // 金額見積もり縦

                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 金額
                            ctrlName = "PHY_KINGAKU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 税区分
                            ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 消費税
                            ctrlName = "PHY_TAX_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispDetailVRowCount);

                        if (maxPage == 0)
                        {
                            maxPage = 1;
                            detailComp = true;
                        }

                        maxRow = maxPage * ConstMaxDispDetailVRowCount;
                        rowNo = 1;

                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();

                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                { // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        //quoc-begin
                                        if (!string.IsNullOrEmpty(dataTableTmp.Rows[i].ItemArray[index].ToString()))
                                        {

                                            row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                        }
                                        else
                                        {
                                            //row[ctrlName] = this.SetFieldVisible(ctrlName,false);
                                            row["PHY_DENPYOU_NUMBER_FLB"] = string.Empty;
                                            row[ctrlName] = string.Empty;
                                        }
                                        //quoc-end
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 数量
                                //quoc-begin
                                decimal quocSoluong = 0;
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    decimal.TryParse(Convert.ToString(dataTableTmp.Rows[i].ItemArray[index]), out quocSoluong);
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                //伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN");
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {

                                    denpyou_kbn = Convert.ToString(dataTableTmp.Rows[i].ItemArray[index]);
                                    if (!string.IsNullOrEmpty(denpyou_kbn))
                                    {
                                        if (denpyou_kbn.Equals("支払"))
                                        {
                                            index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                                            string sinput = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                                            {
                                                sinput = sinput.Replace("-", "▲");
                                                this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", "\\" + sinput);
                                            }

                                            index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_TOTAL");
                                            string sinput2 = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                            {
                                                sinput2 = sinput.Replace("-", "▲");
                                                this.SetFieldName("PF_KINGAKU_TOTAL_CTL", sinput2);
                                            }

                                        }
                                    }
                                }
                                //quoc-end
                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 6)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }



                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    //quoc-begin
                                    if (denpyou_kbn.Equals("支払"))
                                    {
                                        row[ctrlName] = "▲" + dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                    //quoc-end
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 金額
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHY_KINGAKU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    //quoc-begin
                                    if (quocSoluong == 0)
                                    {
                                        row[ctrlName] = "";
                                    }
                                    else
                                    {
                                        //row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                        if (denpyou_kbn.Equals("支払"))
                                        {
                                            row[ctrlName] = "▲" + dataTableTmp.Rows[i].ItemArray[index];
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                        }
                                    }
                                    //quoc-end
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN_CD");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 4)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 4);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 消費税
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHY_TAX_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }


                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 金額
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = string.Empty;

                                // 税区分
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = string.Empty;

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);

                            rowNo++;
                        }

                        break;
                    case OutputTypeDef.KingakuMitsumoriH:       // 金額見積もり横
                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 金額
                            ctrlName = "PHY_KINGAKU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 税区分
                            ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 消費税
                            ctrlName = "PHY_TAX_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispDetailHRowCount);

                        if (maxPage == 0)
                        {
                            maxPage = 1;
                            detailComp = true;
                        }

                        maxRow = maxPage * ConstMaxDispDetailHRowCount;
                        rowNo = 1;

                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();

                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 6)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 金額
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHY_KINGAKU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN_CD");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 4)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 4);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 消費税
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHY_TAX_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }
                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 金額
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = string.Empty;

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;

                                // 消費税
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = string.Empty;

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);

                            rowNo++;
                        }

                        break;
                    case OutputTypeDef.TankaMitsumoriV:         // 単価見積もり縦

                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                            // 金額
                            ctrlName = "PHY_KINGAKU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 税区分
                            ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 消費税
                            ctrlName = "PHY_TAX_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                            // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispTankaDetailVRowCount);

                        if (maxPage == 0)
                        {
                            maxPage = 1;
                            detailComp = true;
                        }

                        maxRow = maxPage * ConstMaxDispTankaDetailVRowCount;
                        rowNo = 1;
                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();

                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 6)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                // 金額
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN_CD");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 2)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 2);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                ctrlName = "PHY_TAX_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }
                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                // 金額
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = string.Empty;

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = string.Empty;
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);
                            rowNo++;
                        }

                        break;
                    case OutputTypeDef.TankaMitsumoriH:         // 単価見積もり横

                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                            // 金額
                            ctrlName = "PHY_KINGAKU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 税区分
                            ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 消費税
                            ctrlName = "PHY_TAX_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                            // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispTankaDetailHRowCount);

                        if (maxPage == 0)
                        {
                            maxPage = 1;
                            detailComp = true;
                        }

                        maxRow = maxPage * ConstMaxDispTankaDetailHRowCount;
                        rowNo = 1;
                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();
                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 6)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                // 金額
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN_CD");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 4)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 4);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                ctrlName = "PHY_TAX_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                if (!this.IsDBNull(dataTableTmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableTmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {   // NULL
                                    row[ctrlName] = string.Empty;
                                }

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }
                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 金額
                                index = dataTableTmp.Columns.IndexOf("HINMEI_KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = string.Empty;

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("HINMEI_TAX_SOTO");
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = string.Empty;

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);
                            rowNo++;
                        }

                        break;
                }

                this.SetRecord(this.dataTable);

                #endregion - Detail -
            }
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
        }

        /// <summary>帳票出力用データテーブル作成処理を実行する</summary>
        /// Thực hiện xử lý tạo bảng dữ liệu cho kết xuất biểu mẫu
        private void SetChouhyouInfo(int pageNo)
        {
            int index;
            int indexquoc;
            int indexquoc2;
            DataTable dataTableHeaderTmp = this.DataTablePageList[pageNo.ToString()]["Header"];
            DataTable dataTableFooterTmp = this.DataTablePageList[pageNo.ToString()]["Footer"];
            string ctrlName = string.Empty;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            switch (this.OutputType)
            {
                case OutputTypeDef.KingakuMitsumoriV:       // 金額見積り（縦）
                    #region - Header -

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        //// タイトル
                        //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                        //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 15)
                            {
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", encoding.GetString(byteArray, 0, 15));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                //quoc-begin
                                this.SetFieldVisible("PHY_MITSUMORI_NUMBER_FLB", false);
                                this.SetFieldVisible("PHY_MITSUMORI_NUMBER_VLB", false);
                                //quoc-end
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);
                        }

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);
                        }

                        //quoc-begin
                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_KAKUIN_INJI");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            //this.SetFieldName("MOD_PHY_KAKUIN1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            Int16 quocval = Convert.ToInt16(dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            if (quocval == 1)
                            {
                                this.SetFieldVisible("MOD_PHY_KAKUIN1_VLB", true);
                                this.SetFieldVisible("Field2", true);
                                this.SetFieldVisible("MOD_PHY_KAKUIN2_VLB", true);
                                
                            }
                            else
                            {
                                this.SetFieldVisible("MOD_PHY_KAKUIN1_VLB", false);
                                this.SetFieldVisible("Field2", false);
                                this.SetFieldVisible("MOD_PHY_KAKUIN2_VLB", true);
                            }

                        }
                        else
                        {   // NULL
                            this.SetFieldName("MOD_PHY_KAKUIN1_VLB", string.Empty);
                            this.SetFieldName("Field2", string.Empty);
                            this.SetFieldVisible("MOD_PHY_KAKUIN2_VLB", true);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_SHAIN_TEL");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("MOD_PHY_SHAIN_TEL_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("MOD_PHY_SHAIN_TEL_VLB", string.Empty);
                        }
                        // quoc - end

                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);
                        }

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);
                        }

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);
                        }

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);
                        }

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);
                        }

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);
                        }

                        // 件名

                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            string quocchuoi = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KENMEI_VLB", encoding.GetString(byteArray, 0, 40));

                            }
                            else
                            {
                                //quoc-begin
                                if (!string.IsNullOrEmpty(quocchuoi))
                                {
                                    this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                                else
                                {
                                    this.SetFieldVisible("PHY_KENMEI_FLB", false);
                                    this.SetFieldVisible("PHY_KENMEI_VLB", false);
                                }
                                //quoc-end
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KENMEI_VLB", string.Empty);
                        }

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);
                        }

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);
                        }

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);
                        }

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);
                        }

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);
                        }

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);
                        }

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);
                        }

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);
                        }

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);
                        }

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 30)
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", encoding.GetString(byteArray, 0, 30));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);
                        }

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);
                        }

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);
                        }

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);
                        }

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", string.Empty);
                        }

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);
                        }

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", string.Empty);
                        }

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", string.Empty);
                        }

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);
                        }

                        // 見積書文言
                        //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                        //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {

                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", "\\" + (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);
                        }

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            switch (dataTableHeaderTmp.Rows[0].ItemArray[index].ToString())
                            {
                                //quoc-begin

                                //bản-chuẩn
                                //case "1": // 「1.外税」の場合
                                //    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（外税）");
                                //    break;
                                //case "2": // 「2.内税」の場合
                                //    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（内税）");
                                //    break;
                                //case "3": // 「3.非課税」の場合
                                //    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（税抜）");
                                //    break;
                                //default:
                                //    this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                                //    break;
                                //end

                                case "1": // 「1.外税」の場合
                                case "2": // 「2.内税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（税込）");
                                    break;
                                case "3": // 「3.非課税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（税抜）");
                                    break;
                                default:
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                                    break;
                                    //quoc-end
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        }
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }
                    else
                    {
                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);

                        //quoc-begin
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_KAKUIN_INJI");
                        this.SetFieldName("MOD_PHY_KAKUIN1_VLB", string.Empty);
                        //quoc-end

                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);

                        // 件名
                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        this.SetFieldName("PHY_KENMEI_VLB", string.Empty);

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", string.Empty);

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", string.Empty);

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", string.Empty);

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);

                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }

                    #endregion - Header -

                    #region - Footer -

                    if (dataTableFooterTmp.Rows.Count > 0)
                    {
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                        // 合計(内税込)   小計
                        index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_TOTAL");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_KINGAKU_TOTAL_CTL", string.Empty);
                        }

                        // 消費税(外税)   消費税
                        //quoc-begin

                        index = dataTableFooterTmp.Columns.IndexOf("TAX_SOTO");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("MOD_PF_SHOUHIZEI_KEI_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("MOD_PF_SHOUHIZEI_KEI_CTL", string.Empty);
                        }
                        //quoc-end

                        index = dataTableFooterTmp.Columns.IndexOf("TAX_SOTO");
                        indexquoc = dataTableHeaderTmp.Columns.IndexOf("MOD_GOUKEI_KINGAKU_INJI");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            Int16 quocval = Convert.ToInt16(dataTableHeaderTmp.Rows[0].ItemArray[indexquoc]);
                            if (quocval == 1)
                            {
                                this.SetFieldName("PF_TAX_SOTO_CTL", "(内消費税 \\" + (string)dataTableFooterTmp.Rows[0].ItemArray[index] + ")");
                            }
                            else
                            {
                                this.SetFieldName("PF_TAX_SOTO_CTL", "(消費税は別途頂きます。)");
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_TAX_SOTO_CTL", string.Empty);
                        }

                        // 課税対象額
                        /*index = dataTableFooterTmp.Columns.IndexOf("PRICE_PROPER");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_PRICE_PROPER_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_PRICE_PROPER_CTL", string.Empty);
                        }*/

                        // 総合計   合計
                        index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", string.Empty);
                        }
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 76)
                            {
                                //this.SetFieldName("PF_BIKOU_1_CTL", encoding.GetString(byteArray, 0, 50));
                                string sinput = Convert.ToString(dataTableFooterTmp.Rows[0].ItemArray[index]);
                                //string sTest = Convert.ToString(dataTableFooterTmp.Rows[0].ItemArray[index]).SubStringByByte(76, 8);
                                //this.SetFieldName("PF_BIKOU_1_CTL", Convert.ToString(dataTableFooterTmp.Rows[0].ItemArray[index]).SubStringByByte(76,8));
                                //cach 1
                                this.SetFieldName("PF_BIKOU_1_CTL", sinput);
                                //cach 2
                                //sinput = sinput.Replace("\r\n", "");
                                //sinput = sinput.SubStringByByte(76, 8);
                                //this.SetFieldName("PF_BIKOU_1_CTL", sinput);
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);
                        }

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);
                        }

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);
                        }

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);
                        }

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);
                        }

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        if (!string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName("PF_BUSHO_NAME_VLB", "部署　" + encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    // 201400709 syunrei #947 №13　start
                                    //this.SetFieldName("PHY_BUSHO_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                    string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                    temp = "部署　" + temp;
                                    this.SetFieldName("PF_BUSHO_NAME_VLB", temp);
                                    // 201400709 syunrei #947 №13　end
                                }
                            }
                            else
                            {   // NULL
                                this.SetFieldName("PF_BUSHO_NAME_VLB", string.Empty);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BUSHO_NAME_VLB", string.Empty);
                        }

                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) && !string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 16)
                            {
                                this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", "営業担当者　" + encoding.GetString(byteArray, 0, 16));
                            }
                            else
                            {
                                string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                temp = "営業担当者　" + temp;
                                this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", temp);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);
                            //quoc-begin
                            this.SetFieldName("MOD_PHY_SHAIN_TEL_VLB", string.Empty);
                            //quoc-end
                        }
                    }
                    else
                    {
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                        // 合計(内税込)  小計
                        index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_TOTAL");
                        this.SetFieldName("PF_KINGAKU_TOTAL_CTL", string.Empty);

                        // 消費税(外税)  消費税
                        index = dataTableFooterTmp.Columns.IndexOf("TAX_SOTO");
                        this.SetFieldName("PF_TAX_SOTO_CTL", string.Empty);

                        // 課税対象額
                        //index = dataTableFooterTmp.Columns.IndexOf("PRICE_PROPER");
                        //this.SetFieldName("PF_PRICE_PROPER_CTL", string.Empty);

                        // 総合計   合計
                        index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                        this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", string.Empty);
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                        this.SetFieldName("PF_BUSHO_NAME_VLB", string.Empty);

                        // 20140709 syunrei #947 №13　start
                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        this.SetFieldName("PHY_BUSHO_NAME_FLB", string.Empty);
                        // 20140709 syunrei #947 №13　end
                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);
                    }
                    #endregion - Footer -

                    break;
                case OutputTypeDef.KingakuMitsumoriH:       // 金額見積り（横）
                    #region - Header -

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        // タイトル
                        //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                        //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 15)
                            {
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", encoding.GetString(byteArray, 0, 15));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);
                        }

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);
                        }

                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);
                        }

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);
                        }

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);
                        }

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);
                        }

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);
                        }

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);
                        }

                        // 件名
                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KENMEI_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KENMEI_VLB", string.Empty);
                        }

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);
                        }

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);
                        }

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);
                        }

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);
                        }

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);
                        }

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);
                        }

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);
                        }

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);
                        }

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);
                        }

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 30)
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", encoding.GetString(byteArray, 0, 30));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);
                        }

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);
                        }

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);
                        }

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);
                        }

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);
                        }

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);
                        }

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", string.Empty);
                        }

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", string.Empty);
                        }

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);
                        }

                        // 見積書文言
                        //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                        //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        if (!string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName("PHY_BUSHO_NAME_VLB", "部署　" + encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                    temp = "部署　" + temp;
                                    this.SetFieldName("PHY_BUSHO_NAME_VLB", temp);
                                }
                            }
                            else
                            {   // NULL
                                this.SetFieldName("PHY_BUSHO_NAME_VLB", string.Empty);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_BUSHO_NAME_VLB", string.Empty);
                        }

                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) && !string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 16)
                            {
                                this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", "営業担当者　" + encoding.GetString(byteArray, 0, 16));
                            }
                            else
                            {
                                string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                temp = "営業担当者　" + temp;
                                this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", temp);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);
                        }

                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", "\\" + (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);
                        }

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            switch (dataTableHeaderTmp.Rows[0].ItemArray[index].ToString())
                            {
                                case "1": // 「1.外税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（外税）");
                                    break;
                                case "2": // 「2.内税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（内税）");
                                    break;
                                case "3": // 「3.非課税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（税抜）");
                                    break;
                                default:
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                                    break;
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        }
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }
                    else
                    {
                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);

                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);

                        // 件名
                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        this.SetFieldName("PHY_KENMEI_VLB", string.Empty);

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", string.Empty);

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", string.Empty);

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", string.Empty);

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                        this.SetFieldName("PHY_BUSHO_NAME_VLB", string.Empty);
                        // 20140709 syunrei #947 №13　start
                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        this.SetFieldName("PHY_BUSHO_NAME_FLB", string.Empty);
                        // 20140709 syunrei #947 №13　end
                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);

                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }
                    #endregion - Header -

                    #region - Footer -

                    if (dataTableFooterTmp.Rows.Count > 0)
                    {
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                        // 合計(内税込)  小計
                        index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_TOTAL");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_KINGAKU_TOTAL_CTL", string.Empty);
                        }

                        // 消費税(外税)  消費税
                        index = dataTableFooterTmp.Columns.IndexOf("TAX_SOTO");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_TAX_SOTO_CTL", "(内消費税 \\" + (string)dataTableFooterTmp.Rows[0].ItemArray[index] + ")");
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_TAX_SOTO_CTL", string.Empty);
                        }

                        // 課税対象額
                        /*index = dataTableFooterTmp.Columns.IndexOf("PRICE_PROPER");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_PRICE_PROPER_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_PRICE_PROPER_CTL", string.Empty);
                        }*/

                        // 総合計  合計
                        index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", string.Empty);
                        }
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_1_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);
                        }

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);
                        }

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);
                        }

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);
                        }

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);
                        }
                    }
                    else
                    {
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                        // 合計(内税込)  小計
                        index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_TOTAL");
                        this.SetFieldName("PF_KINGAKU_TOTAL_CTL", string.Empty);

                        // 消費税(外税)  消費税
                        index = dataTableFooterTmp.Columns.IndexOf("TAX_SOTO");
                        this.SetFieldName("PF_TAX_SOTO_CTL", string.Empty);

                        // 課税対象額
                        //index = dataTableFooterTmp.Columns.IndexOf("PRICE_PROPER");
                        //this.SetFieldName("PF_PRICE_PROPER_CTL", string.Empty);

                        // 総合計  合計
                        index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                        this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", string.Empty);
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);
                    }
                    #endregion - Footer -

                    break;
                case OutputTypeDef.TankaMitsumoriV:         // 単価見積り（縦）
                    #region - Header -
                    //quoc-sheet5
                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        //// タイトル
                        //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                        //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                        //quoc-sheet15-begin
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_TEL");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            string chuoi = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]).SubStringByByte(13, 1);
                            this.SetFieldName("PHY_SOUFU_SAKI_DENWA_BANGO_VLB", chuoi);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_SOUFU_SAKI_DENWA_BANGO_VLB", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        indexquoc = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) || !this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[indexquoc]))
                        {
                            string chuoi1 = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            string chuoi2 = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[indexquoc]);
                            this.SetFieldName("PHY_KAISHA_MEI_VLB", chuoi1 + " " + chuoi2);
                        }
                        else
                        {
                            this.SetFieldName("PHY_KAISHA_MEI_VLB", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("TODOUFUKEN_NAME");
                        indexquoc = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_ADDRESS1");
                        indexquoc2 = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_ADDRESS2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) || !this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[indexquoc]) || !this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[indexquoc2]))
                        {
                            string chuoi1 = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            string chuoi2 = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[indexquoc]);
                            string chuoi3 = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[indexquoc2]);
                            string chuoi4 = (chuoi1 + " " + chuoi2 + " " + chuoi3).SubStringByByte(88, 1);
                            this.SetFieldName("PHY_JUUSHO1_VLB", chuoi4);
                        }
                        else
                        {
                            this.SetFieldName("PHY_JUUSHO1_VLB", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_BUSHO_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            string chuoi = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]).SubStringByByte(20, 1);
                            this.SetFieldName("PHY_BUSHO_MEI_VLB", chuoi);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_BUSHO_MEI_VLB", string.Empty);
                        }


                        
                        index = dataTableHeaderTmp.Columns.IndexOf("TANTOUSHA");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            string chuoi = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]).SubStringByByte(16, 1);
                            this.SetFieldName("PHY_O_TANTOU_MONO_MEI_VLB", chuoi);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_O_TANTOU_MONO_MEI_VLBs", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("TENWABANGOU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            string chuoi = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]).SubStringByByte(13, 1);
                            this.SetFieldName("PHY_KEITAI_BANGO_VLB", chuoi);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KEITAI_BANGO_VLB", string.Empty);
                        }



                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        { 
                            this.SetFieldName("PHY_SOUSHIN_SHA_VLB",(string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_SOUSHIN_SHA_VLB", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MAIL_ADDRESS");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_MAILADORES_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MAILADORES_VLB", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PF_KENMEI_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_KENMEI_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_1_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_1_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_1_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_2_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_2_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_2_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_3_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_3_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_3_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_4_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_4_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_4_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU5");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_5_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_5_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_5_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU6");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_6_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_6_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_6_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU7");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_7_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_7_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_7_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU8");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_8_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_8_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_8_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU9");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_9_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_9_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_9_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU10");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_10_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_10_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_10_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU11");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_11_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_11_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_11_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU12");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_12_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_12_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_12_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU13");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_13_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_13_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_13_CTL", string.Empty);
                        }


                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU14");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_14_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_14_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_14_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU15");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_15_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_15_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_15_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU16");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_16_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_16_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_16_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU17");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_17_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_17_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_17_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU18");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_18_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_18_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_18_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU19");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_19_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_19_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_19_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU20");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_20_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_20_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_20_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU21");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_21_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_21_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_21_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU22");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_22_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_22_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_22_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU23");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_23_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_23_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_23_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU24");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_24_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_24_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_24_CTL", string.Empty);
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("MOD_BIKOU25");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 90)
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_25_CTL", encoding.GetString(byteArray, 0, 90));
                            }
                            else
                            {
                                this.SetFieldName("PF_SOUSHIN_BIKOU_25_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_SOUSHIN_BIKOU_25_CTL", string.Empty);
                        }

                        //quoc-sheet15-end


                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 15)
                            {
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", encoding.GetString(byteArray, 0, 15));
                            }
                            else
                            {
                                //ban chuan
                                //this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                //quoc-begin
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);
                                //quoc-end
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);
                        }

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);
                        }



                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);
                        }

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);
                        }

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);
                        }

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);
                        }

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);
                        }

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);
                        }

                        // 件名
                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        string quocchuoi = "";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            quocchuoi = Convert.ToString(dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KENMEI_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                //quoc-begin
                                if (!string.IsNullOrEmpty(quocchuoi))
                                {
                                    this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                                else
                                {
                                    this.SetFieldVisible("PHY_KENMEI_FLB", false);
                                    this.SetFieldVisible("PHY_KENMEI_VLB", false);
                                }
                                //quoc-end
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KENMEI_VLB", string.Empty);
                            //quoc-begin
                            this.SetFieldVisible("PHY_KENMEI_FLB", false);
                            //quoc-end
                        }

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);
                        }

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);
                        }

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);
                        }

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);
                        }

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);
                        }

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);
                        }

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);
                        }

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);
                        }

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);
                        }

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 30)
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", encoding.GetString(byteArray, 0, 30));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);
                        }

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);
                        }

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);
                        }

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);
                        }

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", string.Empty);
                        }

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);
                        }

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);
                        }

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);
                        }

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);
                        }

                        // 見積書文言
                        //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                        //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", "\\" + (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);
                        }

                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            switch (dataTableHeaderTmp.Rows[0].ItemArray[index].ToString())
                            {
                                case "1": // 「1.外税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（外税）");
                                    break;
                                case "2": // 「2.内税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（内税）");
                                    break;
                                case "3": // 「3.非課税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（税抜）");
                                    break;
                                default:
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                                    break;
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        }
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }
                    else
                    {
                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);

                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);

                        // 件名
                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        this.SetFieldName("PHY_KENMEI_VLB", string.Empty);

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", string.Empty);

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", string.Empty);

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", string.Empty);

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);

                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }
                    #endregion - Header -

                    #region - Footer -

                    if (dataTableFooterTmp.Rows.Count > 0)
                    {
                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_1_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);
                        }

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);
                        }

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);
                        }

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);
                        }

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);
                        }

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        if (!string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName("PF_BUSHO_NAME_VLB", "部署　" + encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    // 201400709 syunrei #947 №13　start
                                    //this.SetFieldName("PHY_BUSHO_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                    string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                    temp = "部署　" + temp;
                                    this.SetFieldName("PF_BUSHO_NAME_VLB", temp);
                                    // 201400709 syunrei #947 №13　end
                                }
                            }
                            else
                            {   // NULL
                                this.SetFieldName("PF_BUSHO_NAME_VLB", string.Empty);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BUSHO_NAME_VLB", string.Empty);
                        }

                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) && !string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 16)
                            {
                                this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", "営業担当者　" + encoding.GetString(byteArray, 0, 16));
                            }
                            else
                            {
                                string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                temp = "営業担当者　" + temp;
                                this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", temp);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);
                        }
                    }
                    else
                    {
                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                        this.SetFieldName("PF_BUSHO_NAME_VLB", string.Empty);

                        // 201400709 syunrei #947 №13　start
                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        this.SetFieldName("PHY_BUSHO_NAME_FLB", string.Empty);
                        // 201400709 syunrei #947 №13　end

                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);
                    }
                    #endregion - Footer -

                    break;
                case OutputTypeDef.TankaMitsumoriH:         // 単価見積り（横）
                    #region - Header -

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        // タイトル
                        //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                        //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 15)
                            {
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", encoding.GetString(byteArray, 0, 15));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);
                        }

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);
                        }

                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);
                        }

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);
                        }

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);
                        }

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);
                        }

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);
                        }

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 52)
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", encoding.GetString(byteArray, 0, 52));
                            }
                            else
                            {
                                this.SetFieldName("PHY_GENBA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);
                        }

                        // 件名
                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KENMEI_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KENMEI_VLB", string.Empty);
                        }

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);
                        }

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);
                        }

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);
                        }

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);
                        }

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);
                        }

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);
                        }

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 12)
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", encoding.GetString(byteArray, 0, 12));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);
                        }

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);
                        }

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);
                        }

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 30)
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", encoding.GetString(byteArray, 0, 30));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);
                        }

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);
                        }

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);
                        }

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);
                        }

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", string.Empty);
                        }

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);
                        }

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);
                        }

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", string.Empty);
                        }

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", string.Empty);
                        }

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);
                        }

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);
                        }

                        // 見積書文言
                        //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                        //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        if (!string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName("PHY_BUSHO_NAME_VLB", "部署　" + encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                    temp = "部署　" + temp;
                                    this.SetFieldName("PHY_BUSHO_NAME_VLB", temp);
                                }
                            }
                            else
                            {   // NULL
                                this.SetFieldName("PHY_BUSHO_NAME_VLB", string.Empty);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_BUSHO_NAME_VLB", string.Empty);
                        }

                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) && !string.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 16)
                            {
                                this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", "営業担当者　" + encoding.GetString(byteArray, 0, 16));
                            }
                            else
                            {
                                string temp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                temp = "営業担当者　" + temp;
                                this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", temp);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);
                        }

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", "\\" + (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);
                        }

                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            switch (dataTableHeaderTmp.Rows[0].ItemArray[index].ToString())
                            {
                                case "1": // 「1.外税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（外税）");
                                    break;
                                case "2": // 「2.内税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（内税）");
                                    break;
                                case "3": // 「3.非課税」の場合
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", "（税抜）");
                                    break;
                                default:
                                    this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                                    break;
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        }
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }
                    else
                    {
                        // 見積書番号
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                        this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", string.Empty);

                        // 見積日付
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                        this.SetFieldName("PHY_MITSUMORI_DATE_VLB", string.Empty);

                        // 取引先名1(＋取引先敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", string.Empty);

                        // 取引先名2(＋取引先敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                        this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", string.Empty);

                        // 業者名1(＋業者敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                        this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", string.Empty);

                        // 業者名2(＋業者敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                        this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", string.Empty);

                        // 現場名1(＋現場敬称1)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                        this.SetFieldName("PHY_GENBA_NAME1_VLB", string.Empty);

                        // 現場名2(＋現場敬称2)
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                        this.SetFieldName("PHY_GENBA_NAME2_VLB", string.Empty);

                        // 件名
                        index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                        this.SetFieldName("PHY_KENMEI_VLB", string.Empty);

                        // 見積項目名称1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", string.Empty);

                        // 見積項目1
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                        this.SetFieldName("PHY_MITSUMORI_1_VLB", string.Empty);

                        // 見積項目名称2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", string.Empty);

                        // 見積項目2
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                        this.SetFieldName("PHY_MITSUMORI_2_VLB", string.Empty);

                        // 見積項目名称3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", string.Empty);

                        // 見積項目3
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                        this.SetFieldName("PHY_MITSUMORI_3_VLB", string.Empty);

                        // 見積項目名称4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                        this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", string.Empty);

                        // 見積項目4
                        index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                        this.SetFieldName("PHY_MITSUMORI_4_VLB", string.Empty);

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                        this.SetFieldName("PHY_CORP_NAME_VLB", string.Empty);

                        // 代表者
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                        this.SetFieldName("PHY_CORP_DAIHYOU_VLB", string.Empty);

                        // 印字拠点名1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                        this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", string.Empty);

                        // 印字拠点郵便番号1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                        this.SetFieldName("PHY_KYOTEN_POST_1_VLB", string.Empty);

                        // 印字拠点住所1_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", string.Empty);

                        // 印字拠点住所2_1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", string.Empty);

                        // 印字拠点TEL1
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                        this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                        this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", string.Empty);

                        // 印字拠点名2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                        this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", string.Empty);

                        // 印字拠点郵便番号2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                        this.SetFieldName("PHY_KYOTEN_POST_2_VLB", string.Empty);

                        // 印字拠点住所1_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", string.Empty);

                        // 印字拠点住所2_2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                        this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", string.Empty);

                        // 印字拠点TEL2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                        this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", string.Empty);

                        // 印字拠点FAX2
                        index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                        this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", string.Empty);

                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                        this.SetFieldName("PHY_BUSHO_NAME_VLB", string.Empty);
                        // 20140709 syunrei #947 №13　start
                        // 部署名
                        index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME_LABEL");
                        this.SetFieldName("PHY_BUSHO_NAME_FLB", string.Empty);
                        // 20140709 syunrei #947 №13　end
                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                        this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", string.Empty);

                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 start
                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", string.Empty);

                        // 税区分
                        index = dataTableHeaderTmp.Columns.IndexOf("ZEI_KBN_CD");
                        this.SetFieldName("PHY_ZEIKUBUN_VLB", string.Empty);
                        // 20140709 chinchisi [環境将軍R 標準版 - 開発 #947]_№17 end
                    }
                    #endregion - Header -

                    #region - Footer -

                    if (dataTableFooterTmp.Rows.Count > 0)
                    {
                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_1_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);
                        }

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);
                        }

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);
                        }

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);
                        }

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 50)
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", encoding.GetString(byteArray, 0, 50));
                            }
                            else
                            {
                                this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {   // NULL
                            this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);
                        }
                    }
                    else
                    {
                        // 備考1
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                        this.SetFieldName("PF_BIKOU_1_CTL", string.Empty);

                        // 備考2
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                        this.SetFieldName("PF_BIKOU_2_CTL", string.Empty);

                        // 備考3
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                        this.SetFieldName("PF_BIKOU_3_CTL", string.Empty);

                        // 備考4
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                        this.SetFieldName("PF_BIKOU_4_CTL", string.Empty);

                        // 備考5
                        index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                        this.SetFieldName("PF_BIKOU_5_CTL", string.Empty);
                    }
                    #endregion - Footer -

                    break;
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
