﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.SalesPayment.ShiharaiJunihyo
{
    /// <summary>
    /// 帳票パターン登録ポップアップ 画面クラス
    /// </summary>
    public partial class ChouhyouPatternTourokuPopupForm : SuperPopupForm
    {
        /// <summary>
        /// 画面区分
        /// </summary>
        private WINDOW_TYPE windowType;

        /// <summary>
        /// ロジッククラス
        /// </summary>
        private ChouhyouPatternTourokuPopupLogic logic;

        /// <summary>
        /// パターンDTOクラス
        /// </summary>
        private PatternDto dto;

        /// <summary>
        /// 集計項目のコンボボックスに表示するリスト
        /// </summary>
        private List<S_LIST_COLUMN_SELECT> shuukeiKoumokuList;

        /// <summary>
        /// 集計項目１の前回値
        /// </summary>
        private S_LIST_COLUMN_SELECT shuukeiKoumoku1ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();

        /// <summary>
        /// 集計項目２の前回値
        /// </summary>
        private S_LIST_COLUMN_SELECT shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ChouhyouPatternTourokuPopupForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="patternDto">パターンDTO</param>
        public ChouhyouPatternTourokuPopupForm(WINDOW_TYPE windowType, PatternDto patternDto)
        {
            LogUtility.DebugMethodStart(windowType, patternDto);

            InitializeComponent();

            this.windowType = windowType;
            this.WindowId = (WINDOW_ID)patternDto.Pattern.WINDOW_ID.Value;

            this.logic = new ChouhyouPatternTourokuPopupLogic();
            this.dto = patternDto;

            this.Text = "支払順位表パターン登録";
            this.TITLE_LABEL.Text = "支払順位表パターン登録";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダイアログを表示します
        /// </summary>
        /// <returns>ダイアログボックスの戻り値</returns>
        public new DialogResult ShowDialog()
        {
            LogUtility.DebugMethodStart();

            var ret = DialogResult.OK;

            this.shuukeiKoumokuList = this.logic.GetShuukeiKoumokuList(this.WindowId);
            this.shuukeiKoumokuList.Insert(0, new S_LIST_COLUMN_SELECT());

            this.SetShuukeiKoumokuListToComboBox1();

            ret = base.ShowDialog();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 画面がロードされたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.GetDtoData();

            switch (this.windowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.PATTERN_NAME.ReadOnly = true;
                    this.PATTERN_NAME.TabStop = false;
                    this.SHUUKEI_KOUMOKU_1.Enabled = false;
                    this.SHUUKEI_KOUMOKU_2.Enabled = false;
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面にパターンDTOからデータを取得します
        /// </summary>
        private void GetDtoData()
        {
            LogUtility.DebugMethodStart();

            if (this.windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.PATTERN_NAME.Text = this.dto.PATTERN_NAME;
                this.SHUUKEI_KOUMOKU_1.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(1).KOUMOKU_ID) == 0).FirstOrDefault();
                if (this.SHUUKEI_KOUMOKU_1.SelectedItem != null)
                {
                    this.SetShuukeiKoumokuListToComboBox2(true);
                }
                this.SHUUKEI_KOUMOKU_2.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(2).KOUMOKU_ID) == 0).FirstOrDefault();

            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターンDTOに画面からデータをセットします
        /// </summary>
        private void SetDtoData()
        {
            LogUtility.DebugMethodStart();

            if (this.windowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                this.dto.Pattern.PATTERN_NAME = this.PATTERN_NAME.Text;
                this.dto.PatternColumnList = new List<M_LIST_PATTERN_COLUMN>();
                var shuukeiKoumoku1 = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
                if (shuukeiKoumoku1.KOUMOKU_ID.IsNull == false)
                {
                    this.dto.PatternColumnList.Add(new M_LIST_PATTERN_COLUMN()
                    {
                        // テーブル定義を変更したくないため、集計のフラグはDETAIL_KBNを使用する
                        DETAIL_KBN = false,
                        ROW_NO = 1,
                        WINDOW_ID = (int)this.WindowId,
                        KOUMOKU_ID = shuukeiKoumoku1.KOUMOKU_ID
                    });
                }
                var shuukeiKoumoku2 = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
                if (shuukeiKoumoku2 != null && shuukeiKoumoku2.KOUMOKU_ID.IsNull == false)
                {
                    this.dto.PatternColumnList.Add(new M_LIST_PATTERN_COLUMN()
                    {
                        // テーブル定義を変更したくないため、集計のフラグはDETAIL_KBNを使用する
                        DETAIL_KBN = false,
                        ROW_NO = 2,
                        WINDOW_ID = (int)this.WindowId,
                        KOUMOKU_ID = shuukeiKoumoku2.KOUMOKU_ID
                    });
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンが押されたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChouhyouPatternTourokuPopupForm_KeyUp(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            switch (e.KeyData)
            {
                case Keys.F9:
                    this.btnF9.Focus();
                    this.Regist();
                    break;
                case Keys.F12:
                    this.FormClose(DialogResult.Cancel);
                    break;
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// [F9]登録ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnF9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Regist();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnF12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.FormClose(DialogResult.Cancel);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録処理を行います
        /// </summary>
        private void Regist()
        {
            LogUtility.DebugMethodStart();

            var res = false;

            if (this.InputError() == false)
            {
                //20151020 hoanghm #12019 start
                //check exists pattern by pattern name
                if (this.windowType == WINDOW_TYPE.NEW_WINDOW_FLAG || this.windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    bool isError = false;
                    var patternDao = DaoInitUtility.GetComponent<IM_LIST_PATTERNDao>();
                    var entity = new M_LIST_PATTERN();
                    entity.PATTERN_NAME = this.PATTERN_NAME.Text;
                    entity.WINDOW_ID = (int)this.WindowId;
                    entity.DELETE_FLG = false;
                    var patternList = patternDao.GetListPatternList(entity);
                    if (patternList != null && patternList.Count > 0)
                    {
                        if (this.windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            foreach (M_LIST_PATTERN obj in patternList)
                            {
                                if (obj.SYSTEM_ID != this.dto.Pattern.SYSTEM_ID)
                                {
                                    isError = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }
                    }
                    if (isError)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E257");
                        return;
                    }
                }
                //20151020 hoanghm #12019 end

                this.SetDtoData();

                res = this.logic.Regist(this.windowType, this.dto);
                if (true == res)
                {
                    this.FormClose(DialogResult.OK);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力チェックを行います
        /// </summary>
        /// <returns>入力チェックがある場合、True</returns>
        private bool InputError()
        {
            LogUtility.DebugMethodStart();

            var ret = false;
            var koumoku = String.Empty;
            if (String.IsNullOrEmpty(this.PATTERN_NAME.Text))
            {
                this.PATTERN_NAME.IsInputErrorOccured = true;
                koumoku = koumoku + "帳票名";
                ret = true;
            }
            if (((S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem).KOUMOKU_ID.IsNull == true)
            {
                this.SHUUKEI_KOUMOKU_1.IsInputErrorOccured = true;
                if (String.IsNullOrEmpty(koumoku) == false)
                {
                    koumoku = koumoku + "、";
                }
                koumoku = koumoku + "集計項目";
                ret = true;
            }

            if (ret)
            {
                new MessageBoxShowLogic().MessageBoxShow("E001", koumoku);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// ポップアップを閉じます
        /// </summary>
        /// <param name="result">ダイアログの戻り値</param>
        private void FormClose(DialogResult result)
        {
            LogUtility.DebugMethodStart(result);

            this.DialogResult = result;
            this.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目１にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_1_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku1ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目２にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_2_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku2ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目１のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetShuukeiKoumokuListToComboBox2(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目２のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private void SetShuukeiKoumokuListToComboBox2(bool isForceSet)
        {
            LogUtility.DebugMethodStart(isForceSet);

            // 集計項目１が変更されたら
            var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
            if (shuukeiKoumoku1SelectedItem == null)
            {
                shuukeiKoumoku1SelectedItem = new S_LIST_COLUMN_SELECT();
            }
            if (isForceSet || this.shuukeiKoumoku1ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) != 0)
            {
                // 集計項目２以降のリストをクリア
                this.SHUUKEI_KOUMOKU_2.DataSource = null;
                this.SHUUKEI_KOUMOKU_2.Enabled = false;

                // 集計項目１が選択されていたら
                if (shuukeiKoumoku1SelectedItem.KOUMOKU_ID.IsNull == false)
                {
                    // 集計項目１で選択されている項目を除いたリストを作成
                    var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                    newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());

                    // 親子関係項目の親が上位項目で選択されているかチェックし、選択されていない場合は子項目を取り除いたリストを作成
                    // 業者 - 現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.GYOUSHA_CD_KOUMOKU_ID, 1))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.GENBA_CD_CD_KOUMOKU_ID) == 0).FirstOrDefault());
                    }

                    // 選択できる項目が残っていたら
                    if (0 < newList.Count())
                    {
                        // 集計項目２にセット
                        this.SHUUKEI_KOUMOKU_2.DataSource = newList;
                        this.SHUUKEI_KOUMOKU_2.DisplayMember = ConstClass.DISPLAY_MEMBER;
                        this.SHUUKEI_KOUMOKU_2.ValueMember = ConstClass.VALUE_MEMBER;
                        this.SHUUKEI_KOUMOKU_2.Enabled = true;
                    }
                    this.shuukeiKoumoku1ComboBoxSelectedItem = shuukeiKoumoku1SelectedItem;
                }
                else
                {
                    this.shuukeiKoumoku1ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                    this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目１のリストボックスに集計項目リストをセットします
        /// </summary>
        private void SetShuukeiKoumokuListToComboBox1()
        {
            LogUtility.DebugMethodStart();

            if (this.shuukeiKoumokuList.Count() > 0)
            {
                // 親子関係項目の子項目を取り除いたリストを作成
                var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                // 現場
                newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.GENBA_CD_CD_KOUMOKU_ID) == 0).FirstOrDefault());

                this.SHUUKEI_KOUMOKU_1.DataSource = newList;
                this.SHUUKEI_KOUMOKU_1.DisplayMember = ConstClass.DISPLAY_MEMBER;
                this.SHUUKEI_KOUMOKU_1.ValueMember = ConstClass.VALUE_MEMBER;
                this.SHUUKEI_KOUMOKU_1.SelectedIndex = 0;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 親子関係に属する項目のうち親に当たる項目が既に使用されているかをチェックします
        /// </summary>
        /// <param name="parentId">親に当たる項目の項目ID</param>
        /// <param name="checkIndex">チェック対象位置</param>
        /// <returns>使用されている：True</returns>
        private bool CheckParentKoumokuUsed(int parentId, int checkIndex)
        {
            // 使用例
            // checkIndexが3の場合、第一 ～ 第三項目までで指定されたparentIdに該当する項目が存在するかチェックします

            bool result = false;

            for (int i = 1; i <= checkIndex; i++)
            {
                r_framework.CustomControl.CustomComboBox control = (r_framework.CustomControl.CustomComboBox)this.Controls["SHUUKEI_KOUMOKU_" + i];
                S_LIST_COLUMN_SELECT selectedItem = (S_LIST_COLUMN_SELECT)control.SelectedItem;
                if (selectedItem.KOUMOKU_ID == parentId)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}
