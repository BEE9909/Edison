﻿namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaIchiran
{
    partial class HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.lab_HidukeNyuuryoku = new System.Windows.Forms.Label();
            this.radbtnDenpyouHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnNyuuryokuHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_HidukeSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomTextBox();
            this.ReadDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.radbtnKenshuHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(8, 8);
            this.windowTypeLabel.TabIndex = 0;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(247, 34);
            this.lb_title.TabIndex = 1;
            this.lb_title.Text = "外部連携現場一覧";
            // 
            // HIDUKE_TO
            // 
            this.HIDUKE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_TO.Checked = false;
            this.HIDUKE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_TO.DateTimeNowYear = "";
            this.HIDUKE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_TO.DisplayItemName = "終了日付";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(834, 24);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_TO.TabIndex = 80;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Text = "2013/10/31(木)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.HIDUKE_TO.DoubleClick += new System.EventHandler(this.HIDUKE_TO_DoubleClick);
            this.HIDUKE_TO.Leave += new System.EventHandler(this.HIDUKE_TO_Leave);
            // 
            // lab_HidukeNyuuryoku
            // 
            this.lab_HidukeNyuuryoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lab_HidukeNyuuryoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_HidukeNyuuryoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_HidukeNyuuryoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lab_HidukeNyuuryoku.ForeColor = System.Drawing.Color.White;
            this.lab_HidukeNyuuryoku.Location = new System.Drawing.Point(561, 24);
            this.lab_HidukeNyuuryoku.Name = "lab_HidukeNyuuryoku";
            this.lab_HidukeNyuuryoku.Size = new System.Drawing.Size(110, 20);
            this.lab_HidukeNyuuryoku.TabIndex = 392;
            this.lab_HidukeNyuuryoku.Text = "伝票日付";
            this.lab_HidukeNyuuryoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtnDenpyouHiduke
            // 
            this.radbtnDenpyouHiduke.AutoSize = true;
            this.radbtnDenpyouHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnDenpyouHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.FocusOutCheckMethod")));
            this.radbtnDenpyouHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnDenpyouHiduke.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnDenpyouHiduke.Location = new System.Drawing.Point(35, 0);
            this.radbtnDenpyouHiduke.Name = "radbtnDenpyouHiduke";
            this.radbtnDenpyouHiduke.PopupAfterExecute = null;
            this.radbtnDenpyouHiduke.PopupBeforeExecute = null;
            this.radbtnDenpyouHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnDenpyouHiduke.PopupSearchSendParams")));
            this.radbtnDenpyouHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnDenpyouHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnDenpyouHiduke.popupWindowSetting")));
            this.radbtnDenpyouHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.RegistCheckMethod")));
            this.radbtnDenpyouHiduke.Size = new System.Drawing.Size(95, 17);
            this.radbtnDenpyouHiduke.TabIndex = 40;
            this.radbtnDenpyouHiduke.Tag = "日付種類が「1.登録日付」の場合にはチェックを付けてください";
            this.radbtnDenpyouHiduke.Text = "1.登録日付";
            this.radbtnDenpyouHiduke.UseVisualStyleBackColor = true;
            this.radbtnDenpyouHiduke.Value = "1";
            this.radbtnDenpyouHiduke.CheckedChanged += new System.EventHandler(this.radbtnDenpyouHiduke_CheckedChanged);
            // 
            // radbtnNyuuryokuHiduke
            // 
            this.radbtnNyuuryokuHiduke.AutoSize = true;
            this.radbtnNyuuryokuHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnNyuuryokuHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.FocusOutCheckMethod")));
            this.radbtnNyuuryokuHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnNyuuryokuHiduke.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnNyuuryokuHiduke.Location = new System.Drawing.Point(136, 0);
            this.radbtnNyuuryokuHiduke.Name = "radbtnNyuuryokuHiduke";
            this.radbtnNyuuryokuHiduke.PopupAfterExecute = null;
            this.radbtnNyuuryokuHiduke.PopupBeforeExecute = null;
            this.radbtnNyuuryokuHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnNyuuryokuHiduke.PopupSearchSendParams")));
            this.radbtnNyuuryokuHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnNyuuryokuHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnNyuuryokuHiduke.popupWindowSetting")));
            this.radbtnNyuuryokuHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.RegistCheckMethod")));
            this.radbtnNyuuryokuHiduke.Size = new System.Drawing.Size(95, 17);
            this.radbtnNyuuryokuHiduke.TabIndex = 50;
            this.radbtnNyuuryokuHiduke.Tag = "日付種類が「2.更新日付」の場合にはチェックを付けてください";
            this.radbtnNyuuryokuHiduke.Text = "2.更新日付";
            this.radbtnNyuuryokuHiduke.UseVisualStyleBackColor = true;
            this.radbtnNyuuryokuHiduke.Value = "2";
            this.radbtnNyuuryokuHiduke.CheckedChanged += new System.EventHandler(this.radbtnNyuuryokuHiduke_CheckedChanged);
            // 
            // txtNum_HidukeSentaku
            // 
            this.txtNum_HidukeSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_HidukeSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_HidukeSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_HidukeSentaku.DisplayItemName = "日付選択";
            this.txtNum_HidukeSentaku.DisplayPopUp = null;
            this.txtNum_HidukeSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.FocusOutCheckMethod")));
            this.txtNum_HidukeSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_HidukeSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_HidukeSentaku.IsInputErrorOccured = false;
            this.txtNum_HidukeSentaku.LinkedRadioButtonArray = new string[] {
        "radbtnDenpyouHiduke",
        "radbtnNyuuryokuHiduke",
        "radbtnKenshuHiduke"};
            this.txtNum_HidukeSentaku.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_HidukeSentaku.Name = "txtNum_HidukeSentaku";
            this.txtNum_HidukeSentaku.PopupAfterExecute = null;
            this.txtNum_HidukeSentaku.PopupBeforeExecute = null;
            this.txtNum_HidukeSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HidukeSentaku.PopupSearchSendParams")));
            this.txtNum_HidukeSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HidukeSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HidukeSentaku.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_HidukeSentaku.RangeSetting = rangeSettingDto1;
            this.txtNum_HidukeSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.RegistCheckMethod")));
            this.txtNum_HidukeSentaku.Size = new System.Drawing.Size(20, 20);
            this.txtNum_HidukeSentaku.TabIndex = 30;
            this.txtNum_HidukeSentaku.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_HidukeSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_HidukeSentaku.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(814, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 395;
            this.label2.Text = "～";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(249, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "拠点※";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点CD";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(364, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto2;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 10;
            this.KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.Visible = false;
            this.KYOTEN_CD.WordWrap = false;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.GetCodeMasterField = "";
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(393, 2);
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.PopupWindowName = "";
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.SetFormField = "";
            this.KYOTEN_NAME_RYAKU.ShortItemName = "";
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 20;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "検索する文字を入力してください";
            this.KYOTEN_NAME_RYAKU.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(981, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(981, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.Location = new System.Drawing.Point(1096, 2);
            this.alertNumber.MaxLength = 5;
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            this.alertNumber.ReadOnly = true;
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 90;
            this.alertNumber.TabStop = false;
            this.alertNumber.Tag = "";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.Visible = false;
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.Location = new System.Drawing.Point(1096, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 100;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.Visible = false;
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_FROM.Checked = false;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_FROM.DateTimeNowYear = "";
            this.HIDUKE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_FROM.DisplayItemName = "開始日付";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(676, 24);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_FROM.TabIndex = 70;
            this.HIDUKE_FROM.Tag = "日付を選択してください";
            this.HIDUKE_FROM.Text = "2013/10/31(木)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.HIDUKE_FROM.DoubleClick += new System.EventHandler(this.HIDUKE_FROM_DoubleClick);
            this.HIDUKE_FROM.Leave += new System.EventHandler(this.HIDUKE_FROM_Leave);
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.txtNum_HidukeSentaku);
            this.customPanel1.Controls.Add(this.radbtnDenpyouHiduke);
            this.customPanel1.Controls.Add(this.radbtnKenshuHiduke);
            this.customPanel1.Controls.Add(this.radbtnNyuuryokuHiduke);
            this.customPanel1.Location = new System.Drawing.Point(561, 2);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(411, 20);
            this.customPanel1.TabIndex = 5;
            // 
            // radbtnKenshuHiduke
            // 
            this.radbtnKenshuHiduke.AutoSize = true;
            this.radbtnKenshuHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnKenshuHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnKenshuHiduke.FocusOutCheckMethod")));
            this.radbtnKenshuHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnKenshuHiduke.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnKenshuHiduke.Location = new System.Drawing.Point(237, 0);
            this.radbtnKenshuHiduke.Name = "radbtnKenshuHiduke";
            this.radbtnKenshuHiduke.PopupAfterExecute = null;
            this.radbtnKenshuHiduke.PopupBeforeExecute = null;
            this.radbtnKenshuHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnKenshuHiduke.PopupSearchSendParams")));
            this.radbtnKenshuHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnKenshuHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnKenshuHiduke.popupWindowSetting")));
            this.radbtnKenshuHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnKenshuHiduke.RegistCheckMethod")));
            this.radbtnKenshuHiduke.Size = new System.Drawing.Size(95, 17);
            this.radbtnKenshuHiduke.TabIndex = 60;
            this.radbtnKenshuHiduke.Tag = "日付種類が「3.連携日付」の場合にはチェックを付けてください";
            this.radbtnKenshuHiduke.Text = "3.連携日付";
            this.radbtnKenshuHiduke.UseVisualStyleBackColor = true;
            this.radbtnKenshuHiduke.Value = "3";
            this.radbtnKenshuHiduke.CheckedChanged += new System.EventHandler(this.radbtnKenshuHiduke_CheckedChanged);
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(507, 23);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 670;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.lab_HidukeNyuuryoku);
            this.Controls.Add(this.label2);
            this.Name = "HeaderForm";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lab_HidukeNyuuryoku, 0);
            this.Controls.SetChildIndex(this.HIDUKE_TO, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.HIDUKE_FROM, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        internal System.Windows.Forms.Label lab_HidukeNyuuryoku;
        public r_framework.CustomControl.CustomRadioButton radbtnDenpyouHiduke;
        public r_framework.CustomControl.CustomRadioButton radbtnNyuuryokuHiduke;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_HidukeSentaku;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label4;
        public r_framework.CustomControl.CustomTextBox alertNumber;
        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomTextBox ReadDataNumber;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton radbtnKenshuHiduke;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}