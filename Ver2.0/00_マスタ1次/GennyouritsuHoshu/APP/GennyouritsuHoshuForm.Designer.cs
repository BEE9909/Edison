﻿
namespace GennyouritsuHoshu.APP
{
    partial class GennyouritsuHoshuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GennyouritsuHoshuForm));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.HOUKOKUSHO_BUNRUI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.Ichiran = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.gennyouritsuHoshuDetail1 = new GennyouritsuHoshu.MultiRowTemplate.GennyouritsuHoshuDetail();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(655, 32);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(145, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 5;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除済も含めて全て表示";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(556, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 415;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(12, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 20);
            this.label16.TabIndex = 414;
            this.label16.Text = "検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 414;
            this.label2.Text = "報告書分類";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HOUKOKUSHO_BUNRUI_NAME_RYAKU
            // 
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.DBFieldsName = "";
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.DisplayItemName = "報告書分類";
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.DisplayPopUp = null;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.IsInputErrorOccured = false;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Location = new System.Drawing.Point(165, 4);
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.MaxLength = 0;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Name = "HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.PopupAfterExecute = null;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.PopupBeforeExecute = null;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME_RYAKU.PopupSearchSendParams")));
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.PopupSetFormField = "";
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.PopupWindowName = "";
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME_RYAKU.popupWindowSetting")));
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.prevText = null;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.PrevText = null;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.ReadOnly = true;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME_RYAKU.RegistCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.SetFormField = "";
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.ShortItemName = "報告書分類";
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Size = new System.Drawing.Size(150, 20);
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.TabIndex = 1;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.TabStop = false;
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Tag = " ";
            // 
            // CONDITION_ITEM
            // 
            this.CONDITION_ITEM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.CONDITION_ITEM.DBFieldsName = "";
            this.CONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM.DisplayItemName = "検索条件";
            this.CONDITION_ITEM.DisplayPopUp = null;
            this.CONDITION_ITEM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.FocusOutCheckMethod")));
            this.CONDITION_ITEM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM.IsInputErrorOccured = false;
            this.CONDITION_ITEM.Location = new System.Drawing.Point(111, 30);
            this.CONDITION_ITEM.MaxLength = 0;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupAfterExecute = null;
            this.CONDITION_ITEM.PopupBeforeExecute = null;
            this.CONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM.PopupSearchSendParams")));
            this.CONDITION_ITEM.PopupSendParams = new string[] {
        "Ichiran"};
            this.CONDITION_ITEM.PopupSetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM.popupWindowSetting")));
            this.CONDITION_ITEM.prevText = null;
            this.CONDITION_ITEM.PrevText = null;
            this.CONDITION_ITEM.ReadOnly = true;
            this.CONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.RegistCheckMethod")));
            this.CONDITION_ITEM.SetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.ShortItemName = "検索条件";
            this.CONDITION_ITEM.Size = new System.Drawing.Size(150, 20);
            this.CONDITION_ITEM.TabIndex = 2;
            this.CONDITION_ITEM.Tag = "検索条件を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.CONDITION_VALUE.DBFieldsName = "";
            this.CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE.DisplayItemName = "検索条件";
            this.CONDITION_VALUE.DisplayPopUp = null;
            this.CONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.FocusOutCheckMethod")));
            this.CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE.IsInputErrorOccured = false;
            this.CONDITION_VALUE.ItemDefinedTypes = "";
            this.CONDITION_VALUE.Location = new System.Drawing.Point(260, 30);
            this.CONDITION_VALUE.MaxLength = 0;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupAfterExecute = null;
            this.CONDITION_VALUE.PopupBeforeExecute = null;
            this.CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE.PopupSearchSendParams")));
            this.CONDITION_VALUE.PopupSetFormField = "";
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.PopupWindowName = "";
            this.CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE.popupWindowSetting")));
            this.CONDITION_VALUE.prevText = null;
            this.CONDITION_VALUE.PrevText = null;
            this.CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.RegistCheckMethod")));
            this.CONDITION_VALUE.SetFormField = "";
            this.CONDITION_VALUE.ShortItemName = "検索条件";
            this.CONDITION_VALUE.Size = new System.Drawing.Size(290, 20);
            this.CONDITION_VALUE.TabIndex = 3;
            this.CONDITION_VALUE.Tag = "検索する文字を入力してください";
            // 
            // HOUKOKUSHO_BUNRUI_CD
            // 
            this.HOUKOKUSHO_BUNRUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HOUKOKUSHO_BUNRUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKUSHO_BUNRUI_CD.ChangeUpperCase = true;
            this.HOUKOKUSHO_BUNRUI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HOUKOKUSHO_BUNRUI_CD.DBFieldsName = "HOUKOKUSHO_BUNRUI_CD";
            this.HOUKOKUSHO_BUNRUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKUSHO_BUNRUI_CD.DisplayItemName = "報告書分類";
            this.HOUKOKUSHO_BUNRUI_CD.DisplayPopUp = null;
            this.HOUKOKUSHO_BUNRUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.FocusOutCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HOUKOKUSHO_BUNRUI_CD.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKUSHO_BUNRUI_CD.GetCodeMasterField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKUSHO_BUNRUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HOUKOKUSHO_BUNRUI_CD.IsInputErrorOccured = false;
            this.HOUKOKUSHO_BUNRUI_CD.ItemDefinedTypes = "varchar";
            this.HOUKOKUSHO_BUNRUI_CD.Location = new System.Drawing.Point(111, 4);
            this.HOUKOKUSHO_BUNRUI_CD.MaxLength = 6;
            this.HOUKOKUSHO_BUNRUI_CD.Name = "HOUKOKUSHO_BUNRUI_CD";
            this.HOUKOKUSHO_BUNRUI_CD.PopupAfterExecute = null;
            this.HOUKOKUSHO_BUNRUI_CD.PopupBeforeExecute = null;
            this.HOUKOKUSHO_BUNRUI_CD.PopupGetMasterField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKUSHO_BUNRUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.PopupSearchSendParams")));
            this.HOUKOKUSHO_BUNRUI_CD.PopupSendParams = new string[0];
            this.HOUKOKUSHO_BUNRUI_CD.PopupSetFormField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKUSHO_BUNRUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HOUKOKUSHO_BUNRUI;
            this.HOUKOKUSHO_BUNRUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HOUKOKUSHO_BUNRUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.popupWindowSetting")));
            this.HOUKOKUSHO_BUNRUI_CD.prevText = null;
            this.HOUKOKUSHO_BUNRUI_CD.PrevText = null;
            this.HOUKOKUSHO_BUNRUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.RegistCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_CD.SetFormField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKUSHO_BUNRUI_CD.ShortItemName = "報告書分類";
            this.HOUKOKUSHO_BUNRUI_CD.Size = new System.Drawing.Size(55, 20);
            this.HOUKOKUSHO_BUNRUI_CD.TabIndex = 0;
            this.HOUKOKUSHO_BUNRUI_CD.Tag = "報告書分類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HOUKOKUSHO_BUNRUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HOUKOKUSHO_BUNRUI_CD.ZeroPaddengFlag = true;
            this.HOUKOKUSHO_BUNRUI_CD.TextChanged += new System.EventHandler(this.HOUKOKUSHO_BUNRUI_CD_TextChanged);
            this.HOUKOKUSHO_BUNRUI_CD.ImeModeChanged += new System.EventHandler(this.HOUKOKUSHO_BUNRUI_CD_ImeModeChanged);
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.Ichiran.ColumnHeadersDefaultHeaderCellStyle = cellStyle1;
            this.Ichiran.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            cellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.Ichiran.DefaultCellStyle = cellStyle2;
            this.Ichiran.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.Ichiran.Location = new System.Drawing.Point(12, 58);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            this.Ichiran.ShortcutKeyManager = shortcutKeyManager1;
            this.Ichiran.Size = new System.Drawing.Size(975, 424);
            this.Ichiran.TabIndex = 7;
            this.Ichiran.Template = this.gennyouritsuHoshuDetail1;
            this.Ichiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.Ichiran.CellEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_CellEnter);
            // 
            // GennyouritsuHoshuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(999, 486);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.HOUKOKUSHO_BUNRUI_CD);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.HOUKOKUSHO_BUNRUI_NAME_RYAKU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label16);
            this.Name = "GennyouritsuHoshuForm";
            this.Text = "GennyouritsuHoshuForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomTextBox HOUKOKUSHO_BUNRUI_NAME_RYAKU;
        public r_framework.CustomControl.GcCustomMultiRow Ichiran;
        public MultiRowTemplate.GennyouritsuHoshuDetail gennyouritsuHoshuDetail1;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        public r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        public r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        public r_framework.CustomControl.CustomAlphaNumTextBox HOUKOKUSHO_BUNRUI_CD;
    }
}
