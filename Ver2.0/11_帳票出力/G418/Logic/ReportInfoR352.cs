﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - ReportInfoR352 -

    /// <summary>計量集計表(R352)のレポート情報を表すクラス・コントロール</summary>
    internal class ReportInfoR352 : ReportInfoCommon
    {
        #region - Constructors -

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfoR352" /> class.
        /// </summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="commonChouhyouBase">共通帳票</param>
        public ReportInfoR352(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
            : base(windowID, dataTable, commonChouhyouBase)
        {
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            try
            {
                // フィールド状態の更新処理
                base.UpdateFieldsStatus();

                int itemColumnIndex = 0;
                int index;

                this.SetFieldName("PHN_TOTAL_KINGAKU_LBL_VLB", "総合計");

                #region - 全てのタイトルカラムテキスト初期化 -

                // 集計項目領域初期化
                this.SetFieldName("PHY_KAHEN1_1_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_2_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_3_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_4_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_5_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_6_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_7_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN1_8_VLB", string.Empty);

                // 帳票出力項目領域（伝票部又は明細部の内容部）初期化
                this.SetFieldName("PHY_KAHEN2_1_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_2_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_3_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_4_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_5_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_6_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_7_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN2_8_VLB", string.Empty);

                // 帳票出力項目領域（伝票部又は明細部のコード部）初期化
                this.SetFieldName("PHY_KAHEN3_1_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN3_2_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN3_3_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN3_4_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN3_5_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN3_6_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN3_7_VLB", string.Empty);
                this.SetFieldName("PHY_KAHEN3_8_VLB", string.Empty);

                #endregion - 全てのタイトルカラムテキスト初期化 -

                #region - 集計項目用タイトルカラムテキスト -

                // 有効な集計項目グループ数
                int syuukeiKoumokuEnableGroup = this.ComChouhyouBase.SelectEnableSyuukeiKoumokuGroupCount;
                for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroup; itemColumnIndex++)
                {
                    int itemIndex = this.ComChouhyouBase.SelectSyuukeiKoumokuList[itemColumnIndex];
                    SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemIndex];

                    // 別という文字を削除
                    string syuukeiKoumokuCD = string.Empty;
                    string syuukeiKoumokuCDName = string.Empty;
                    if (syuukeiKoumoku.Name != string.Empty)
                    {
                        string syuukeiKoumokuName = syuukeiKoumoku.Name.Replace("別", string.Empty);
                        syuukeiKoumokuCD = syuukeiKoumokuName + "CD";
                        syuukeiKoumokuCDName = syuukeiKoumokuName + "名";
                    }

                    switch (itemColumnIndex + 1)
                    {
                        case 1: // 集計項目１
                            this.SetFieldName("PHY_KAHEN1_1_VLB", syuukeiKoumokuCD);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_CD_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCD;

                            this.SetFieldName("PHY_KAHEN1_2_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                        case 2: // 集計項目２
                            this.SetFieldName("PHY_KAHEN1_3_VLB", syuukeiKoumokuCD);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_CD_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCD;

                            this.SetFieldName("PHY_KAHEN1_4_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                        case 3: // 集計項目３
                            this.SetFieldName("PHY_KAHEN1_5_VLB", syuukeiKoumokuCD);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_CD_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCD;

                            this.SetFieldName("PHY_KAHEN1_6_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                        case 4: // 集計項目４
                            this.SetFieldName("PHY_KAHEN1_7_VLB", syuukeiKoumokuCD);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_CD_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCD;

                            this.SetFieldName("PHY_KAHEN1_8_VLB", syuukeiKoumokuCDName);
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_NAME_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = syuukeiKoumokuCDName;

                            break;
                    }
                }

                #endregion - 集計項目用タイトルカラムテキスト -

                #region - 帳票出力項目（伝票又は明細）用タイトルカラムテキスト -

                itemColumnIndex = 0;
                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup;
                ChouhyouOutKoumoku chouhyouOutKoumoku;

                int denpyouCount = this.ComChouhyouBase.SelectChouhyouOutKoumokuDepyouList.Count;
                int meisaiCount = this.ComChouhyouBase.SelectChouhyouOutKoumokuMeisaiList.Count;
                int maxCount = denpyouCount + meisaiCount;
                bool isDenpyou = true;

                for (int i = 0; i < maxCount; i++, itemColumnIndex++)
                {
                    if (i < denpyouCount)
                    {   // 伝票
                        chouhyouOutKoumokuGroup = this.ComChouhyouBase.SelectChouhyouOutKoumokuDepyouList[i];
                        isDenpyou = true;
                    }
                    else if (i - denpyouCount < maxCount)
                    {   // 明細
                        chouhyouOutKoumokuGroup = this.ComChouhyouBase.SelectChouhyouOutKoumokuMeisaiList[i - denpyouCount];
                        isDenpyou = false;
                    }
                    else
                    {
                        break;
                    }

                    chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

                    ALIGN_TYPE alignment = (ALIGN_TYPE)chouhyouOutKoumoku.OutputAlignment;

                    switch (itemColumnIndex)
                    {
                        case 0: // 帳票出力可能項目１番目
                            this.SetFieldName("PHY_KAHEN2_1_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_1_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_1_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_1_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_1_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_1_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                        case 1: // 帳票出力可能項目２番目
                            this.SetFieldName("PHY_KAHEN2_2_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_2_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_2_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_2_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_2_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_2_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                        case 2: // 帳票出力可能項目３番目
                            this.SetFieldName("PHY_KAHEN2_3_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_3_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_3_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_3_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_3_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_3_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                        case 3: // 帳票出力可能項目４番目
                            this.SetFieldName("PHY_KAHEN2_4_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_4_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_4_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_4_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_4_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_4_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                        case 4: // 帳票出力可能項目５番目
                            this.SetFieldName("PHY_KAHEN2_5_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_5_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_5_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_5_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_5_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_5_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                        case 5: // 帳票出力可能項目６番目
                            this.SetFieldName("PHY_KAHEN2_6_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_6_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_6_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_6_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_6_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_6_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                        case 6: // 帳票出力可能項目７番目
                            this.SetFieldName("PHY_KAHEN2_7_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_7_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_7_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_7_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_7_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_7_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                        case 7: // 帳票出力可能項目８番目
                            this.SetFieldName("PHY_KAHEN2_8_VLB", chouhyouOutKoumoku.Name);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_8_LABEL") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_8_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = chouhyouOutKoumoku.Name;

                            this.SetFieldAlign("DTL_KAHEN2_8_CTL", alignment);
                            index = isDenpyou ? this.DataTableList["Header"].Columns.IndexOf("OUTPUT_DENPYOU_8_ALIGN") : this.DataTableList["Header"].Columns.IndexOf("OUTPUT_MEISAI_8_ALIGN");
                            this.DataTableList["Header"].Rows[0][index] = ((int)alignment).ToString();

                            break;
                    }
                }

                #endregion - 帳票出力項目（伝票又は明細）用タイトルカラムテキスト -

                for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroup; itemColumnIndex++)
                {
                    int itemIndex = this.ComChouhyouBase.SelectSyuukeiKoumokuList[itemColumnIndex];
                    SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemIndex];

                    if (syuukeiKoumoku.Name == string.Empty)
                    {
                        continue;
                    }

                    // 別文字を削除
                    string name = syuukeiKoumoku.Name.Replace("別", string.Empty);

                    switch (itemColumnIndex)
                    {
                        case 0: // 集計項目１が有効
                            this.SetGroupVisible("GROUP2", false, true);
                            this.SetFieldName("PHN_FILL_COND_ID_1_TOTAL_LBL_VLB", name + "合計");
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_ID_1_TOTAL_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = name + "合計";

                            break;
                        case 1: // 集計項目２が有効
                            this.SetGroupVisible("GROUP3", false, true);
                            this.SetFieldName("PHN_FILL_COND_ID_2_TOTAL_LBL_VLB", name + "合計");
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_ID_2_TOTAL_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = name + "合計";

                            break;
                        case 2: // 集計項目３が有効
                            this.SetGroupVisible("GROUP4", false, true);
                            this.SetFieldName("PHN_FILL_COND_ID_3_TOTAL_LBL_VLB", name + "合計");
                            index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_ID_3_TOTAL_LABEL");
                            this.DataTableList["Header"].Rows[0][index] = name + "合計";

                            break;
                        case 3: // 集計項目４が有効
                            //this.SetGroupVisible("GROUP4", false, true);
                            //this.SetFieldName("PHN_FILL_COND_ID_4_TOTAL_LBL_VLB", name + "合計");
                            //index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_ID_4_TOTAL_LABEL");
                            //this.DataTableList["Header"].Rows[0][index] = name + "合計";

                            break;
                    }
                }

                // 総合計
                index = this.DataTableList["Header"].Columns.IndexOf("ALL_TOTAL_LABEL");
                this.DataTableList["Header"].Rows[0][index] = "総合計";

                // データテーブルリストに明細を追加
                this.DataTableList.Add("Detail", this.ComChouhyouBase.DataTableUkewatashi);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - ReportInfoR352 -

    #endregion - Classes -
}
