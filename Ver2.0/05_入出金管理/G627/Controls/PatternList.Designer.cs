﻿namespace Shougun.Core.ReceiptPayManagement.NyuukinShuukeiChouhyou.Controls
{
    partial class PatternList
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatternList));
            this.label1 = new System.Windows.Forms.Label();
            this.PATTERN_LIST_BOX = new r_framework.CustomControl.CustomListBox();
            this.SHUUKEI_FLAG_4 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_FLAG_3 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_FLAG_2 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_FLAG_1 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUTSURYOKU_KOUMOKU_4 = new r_framework.CustomControl.CustomComboBox();
            this.SHUTSURYOKU_KOUMOKU_3 = new r_framework.CustomControl.CustomComboBox();
            this.SHUTSURYOKU_KOUMOKU_2 = new r_framework.CustomControl.CustomComboBox();
            this.SHUTSURYOKU_KOUMOKU_1 = new r_framework.CustomControl.CustomComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SHUTSURYOKU_KOUMOKU_5 = new r_framework.CustomControl.CustomComboBox();
            this.SHUUKEI_FLAG_5 = new r_framework.CustomControl.CustomCheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 9999;
            this.label1.Text = "出力帳票を選択してください。";
            // 
            // PATTERN_LIST_BOX
            // 
            this.PATTERN_LIST_BOX.BackColor = System.Drawing.SystemColors.Window;
            this.PATTERN_LIST_BOX.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_LIST_BOX.DisplayMember = "PATTERN_NAME";
            this.PATTERN_LIST_BOX.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_LIST_BOX.FocusOutCheckMethod")));
            this.PATTERN_LIST_BOX.FormattingEnabled = true;
            this.PATTERN_LIST_BOX.ItemHeight = 12;
            this.PATTERN_LIST_BOX.Location = new System.Drawing.Point(5, 25);
            this.PATTERN_LIST_BOX.Name = "PATTERN_LIST_BOX";
            this.PATTERN_LIST_BOX.PopupAfterExecute = null;
            this.PATTERN_LIST_BOX.PopupBeforeExecute = null;
            this.PATTERN_LIST_BOX.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_LIST_BOX.PopupSearchSendParams")));
            this.PATTERN_LIST_BOX.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_LIST_BOX.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_LIST_BOX.popupWindowSetting")));
            this.PATTERN_LIST_BOX.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_LIST_BOX.RegistCheckMethod")));
            this.PATTERN_LIST_BOX.Size = new System.Drawing.Size(290, 76);
            this.PATTERN_LIST_BOX.TabIndex = 0;
            this.PATTERN_LIST_BOX.SelectedIndexChanged += new System.EventHandler(this.PATTERN_LIST_BOX_SelectedIndexChanged);
            this.PATTERN_LIST_BOX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PATTERN_LIST_BOX_MouseDoubleClick);
            // 
            // SHUUKEI_FLAG_4
            // 
            this.SHUUKEI_FLAG_4.AutoSize = true;
            this.SHUUKEI_FLAG_4.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_4.Enabled = false;
            this.SHUUKEI_FLAG_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_4.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_4.Location = new System.Drawing.Point(530, 85);
            this.SHUUKEI_FLAG_4.Name = "SHUUKEI_FLAG_4";
            this.SHUUKEI_FLAG_4.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_4.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_4.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_4.popupWindowSetting")));
            this.SHUUKEI_FLAG_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_4.RegistCheckMethod")));
            this.SHUUKEI_FLAG_4.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_4.TabIndex = 10007;
            this.SHUUKEI_FLAG_4.Text = "集計";
            this.SHUUKEI_FLAG_4.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_FLAG_3
            // 
            this.SHUUKEI_FLAG_3.AutoSize = true;
            this.SHUUKEI_FLAG_3.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_3.Enabled = false;
            this.SHUUKEI_FLAG_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_3.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_3.Location = new System.Drawing.Point(530, 60);
            this.SHUUKEI_FLAG_3.Name = "SHUUKEI_FLAG_3";
            this.SHUUKEI_FLAG_3.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_3.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_3.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_3.popupWindowSetting")));
            this.SHUUKEI_FLAG_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_3.RegistCheckMethod")));
            this.SHUUKEI_FLAG_3.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_3.TabIndex = 10006;
            this.SHUUKEI_FLAG_3.Text = "集計";
            this.SHUUKEI_FLAG_3.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_FLAG_2
            // 
            this.SHUUKEI_FLAG_2.AutoSize = true;
            this.SHUUKEI_FLAG_2.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_2.Enabled = false;
            this.SHUUKEI_FLAG_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_2.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_2.Location = new System.Drawing.Point(530, 35);
            this.SHUUKEI_FLAG_2.Name = "SHUUKEI_FLAG_2";
            this.SHUUKEI_FLAG_2.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_2.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_2.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_2.popupWindowSetting")));
            this.SHUUKEI_FLAG_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_2.RegistCheckMethod")));
            this.SHUUKEI_FLAG_2.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_2.TabIndex = 10005;
            this.SHUUKEI_FLAG_2.Text = "集計";
            this.SHUUKEI_FLAG_2.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_FLAG_1
            // 
            this.SHUUKEI_FLAG_1.AutoSize = true;
            this.SHUUKEI_FLAG_1.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_1.Enabled = false;
            this.SHUUKEI_FLAG_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_1.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_1.Location = new System.Drawing.Point(530, 9);
            this.SHUUKEI_FLAG_1.Name = "SHUUKEI_FLAG_1";
            this.SHUUKEI_FLAG_1.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_1.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_1.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_1.popupWindowSetting")));
            this.SHUUKEI_FLAG_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_1.RegistCheckMethod")));
            this.SHUUKEI_FLAG_1.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_1.TabIndex = 10004;
            this.SHUUKEI_FLAG_1.Text = "集計";
            this.SHUUKEI_FLAG_1.UseVisualStyleBackColor = false;
            // 
            // SHUTSURYOKU_KOUMOKU_4
            // 
            this.SHUTSURYOKU_KOUMOKU_4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUTSURYOKU_KOUMOKU_4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUTSURYOKU_KOUMOKU_4.BackColor = System.Drawing.SystemColors.Window;
            this.SHUTSURYOKU_KOUMOKU_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_KOUMOKU_4.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUTSURYOKU_KOUMOKU_4.DisplayPopUp = null;
            this.SHUTSURYOKU_KOUMOKU_4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUTSURYOKU_KOUMOKU_4.Enabled = false;
            this.SHUTSURYOKU_KOUMOKU_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_4.FocusOutCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUTSURYOKU_KOUMOKU_4.FormattingEnabled = true;
            this.SHUTSURYOKU_KOUMOKU_4.IsInputErrorOccured = false;
            this.SHUTSURYOKU_KOUMOKU_4.Location = new System.Drawing.Point(416, 82);
            this.SHUTSURYOKU_KOUMOKU_4.Name = "SHUTSURYOKU_KOUMOKU_4";
            this.SHUTSURYOKU_KOUMOKU_4.PopupAfterExecute = null;
            this.SHUTSURYOKU_KOUMOKU_4.PopupBeforeExecute = null;
            this.SHUTSURYOKU_KOUMOKU_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_4.PopupSearchSendParams")));
            this.SHUTSURYOKU_KOUMOKU_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_KOUMOKU_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_4.popupWindowSetting")));
            this.SHUTSURYOKU_KOUMOKU_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_4.RegistCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_4.Size = new System.Drawing.Size(104, 20);
            this.SHUTSURYOKU_KOUMOKU_4.TabIndex = 10003;
            this.SHUTSURYOKU_KOUMOKU_4.Tag = "";
            this.SHUTSURYOKU_KOUMOKU_4.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // SHUTSURYOKU_KOUMOKU_3
            // 
            this.SHUTSURYOKU_KOUMOKU_3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUTSURYOKU_KOUMOKU_3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUTSURYOKU_KOUMOKU_3.BackColor = System.Drawing.SystemColors.Window;
            this.SHUTSURYOKU_KOUMOKU_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_KOUMOKU_3.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUTSURYOKU_KOUMOKU_3.DisplayPopUp = null;
            this.SHUTSURYOKU_KOUMOKU_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUTSURYOKU_KOUMOKU_3.Enabled = false;
            this.SHUTSURYOKU_KOUMOKU_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_3.FocusOutCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUTSURYOKU_KOUMOKU_3.FormattingEnabled = true;
            this.SHUTSURYOKU_KOUMOKU_3.IsInputErrorOccured = false;
            this.SHUTSURYOKU_KOUMOKU_3.Location = new System.Drawing.Point(416, 57);
            this.SHUTSURYOKU_KOUMOKU_3.Name = "SHUTSURYOKU_KOUMOKU_3";
            this.SHUTSURYOKU_KOUMOKU_3.PopupAfterExecute = null;
            this.SHUTSURYOKU_KOUMOKU_3.PopupBeforeExecute = null;
            this.SHUTSURYOKU_KOUMOKU_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_3.PopupSearchSendParams")));
            this.SHUTSURYOKU_KOUMOKU_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_KOUMOKU_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_3.popupWindowSetting")));
            this.SHUTSURYOKU_KOUMOKU_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_3.RegistCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_3.Size = new System.Drawing.Size(104, 20);
            this.SHUTSURYOKU_KOUMOKU_3.TabIndex = 10002;
            this.SHUTSURYOKU_KOUMOKU_3.Tag = "";
            this.SHUTSURYOKU_KOUMOKU_3.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // SHUTSURYOKU_KOUMOKU_2
            // 
            this.SHUTSURYOKU_KOUMOKU_2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUTSURYOKU_KOUMOKU_2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUTSURYOKU_KOUMOKU_2.BackColor = System.Drawing.SystemColors.Window;
            this.SHUTSURYOKU_KOUMOKU_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_KOUMOKU_2.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUTSURYOKU_KOUMOKU_2.DisplayPopUp = null;
            this.SHUTSURYOKU_KOUMOKU_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUTSURYOKU_KOUMOKU_2.Enabled = false;
            this.SHUTSURYOKU_KOUMOKU_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_2.FocusOutCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUTSURYOKU_KOUMOKU_2.FormattingEnabled = true;
            this.SHUTSURYOKU_KOUMOKU_2.IsInputErrorOccured = false;
            this.SHUTSURYOKU_KOUMOKU_2.Location = new System.Drawing.Point(416, 32);
            this.SHUTSURYOKU_KOUMOKU_2.Name = "SHUTSURYOKU_KOUMOKU_2";
            this.SHUTSURYOKU_KOUMOKU_2.PopupAfterExecute = null;
            this.SHUTSURYOKU_KOUMOKU_2.PopupBeforeExecute = null;
            this.SHUTSURYOKU_KOUMOKU_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_2.PopupSearchSendParams")));
            this.SHUTSURYOKU_KOUMOKU_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_KOUMOKU_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_2.popupWindowSetting")));
            this.SHUTSURYOKU_KOUMOKU_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_2.RegistCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_2.Size = new System.Drawing.Size(104, 20);
            this.SHUTSURYOKU_KOUMOKU_2.TabIndex = 10001;
            this.SHUTSURYOKU_KOUMOKU_2.Tag = "";
            this.SHUTSURYOKU_KOUMOKU_2.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // SHUTSURYOKU_KOUMOKU_1
            // 
            this.SHUTSURYOKU_KOUMOKU_1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUTSURYOKU_KOUMOKU_1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUTSURYOKU_KOUMOKU_1.BackColor = System.Drawing.SystemColors.Window;
            this.SHUTSURYOKU_KOUMOKU_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_KOUMOKU_1.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUTSURYOKU_KOUMOKU_1.DisplayPopUp = null;
            this.SHUTSURYOKU_KOUMOKU_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUTSURYOKU_KOUMOKU_1.Enabled = false;
            this.SHUTSURYOKU_KOUMOKU_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_1.FocusOutCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUTSURYOKU_KOUMOKU_1.FormattingEnabled = true;
            this.SHUTSURYOKU_KOUMOKU_1.IsInputErrorOccured = false;
            this.SHUTSURYOKU_KOUMOKU_1.Location = new System.Drawing.Point(416, 7);
            this.SHUTSURYOKU_KOUMOKU_1.Name = "SHUTSURYOKU_KOUMOKU_1";
            this.SHUTSURYOKU_KOUMOKU_1.PopupAfterExecute = null;
            this.SHUTSURYOKU_KOUMOKU_1.PopupBeforeExecute = null;
            this.SHUTSURYOKU_KOUMOKU_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_1.PopupSearchSendParams")));
            this.SHUTSURYOKU_KOUMOKU_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_KOUMOKU_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_1.popupWindowSetting")));
            this.SHUTSURYOKU_KOUMOKU_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_1.RegistCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_1.Size = new System.Drawing.Size(104, 20);
            this.SHUTSURYOKU_KOUMOKU_1.TabIndex = 10000;
            this.SHUTSURYOKU_KOUMOKU_1.Tag = "";
            this.SHUTSURYOKU_KOUMOKU_1.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(307, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 10008;
            this.label2.Text = "集計項目";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHUTSURYOKU_KOUMOKU_5
            // 
            this.SHUTSURYOKU_KOUMOKU_5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUTSURYOKU_KOUMOKU_5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUTSURYOKU_KOUMOKU_5.BackColor = System.Drawing.SystemColors.Window;
            this.SHUTSURYOKU_KOUMOKU_5.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_KOUMOKU_5.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUTSURYOKU_KOUMOKU_5.DisplayPopUp = null;
            this.SHUTSURYOKU_KOUMOKU_5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUTSURYOKU_KOUMOKU_5.Enabled = false;
            this.SHUTSURYOKU_KOUMOKU_5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_5.FocusOutCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUTSURYOKU_KOUMOKU_5.FormattingEnabled = true;
            this.SHUTSURYOKU_KOUMOKU_5.IsInputErrorOccured = false;
            this.SHUTSURYOKU_KOUMOKU_5.Location = new System.Drawing.Point(416, 108);
            this.SHUTSURYOKU_KOUMOKU_5.Name = "SHUTSURYOKU_KOUMOKU_5";
            this.SHUTSURYOKU_KOUMOKU_5.PopupAfterExecute = null;
            this.SHUTSURYOKU_KOUMOKU_5.PopupBeforeExecute = null;
            this.SHUTSURYOKU_KOUMOKU_5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_5.PopupSearchSendParams")));
            this.SHUTSURYOKU_KOUMOKU_5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_KOUMOKU_5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_5.popupWindowSetting")));
            this.SHUTSURYOKU_KOUMOKU_5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_KOUMOKU_5.RegistCheckMethod")));
            this.SHUTSURYOKU_KOUMOKU_5.Size = new System.Drawing.Size(104, 20);
            this.SHUTSURYOKU_KOUMOKU_5.TabIndex = 10009;
            this.SHUTSURYOKU_KOUMOKU_5.Tag = "";
            this.SHUTSURYOKU_KOUMOKU_5.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // SHUUKEI_FLAG_5
            // 
            this.SHUUKEI_FLAG_5.AutoSize = true;
            this.SHUUKEI_FLAG_5.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_5.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_5.Enabled = false;
            this.SHUUKEI_FLAG_5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_5.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_5.Location = new System.Drawing.Point(530, 110);
            this.SHUUKEI_FLAG_5.Name = "SHUUKEI_FLAG_5";
            this.SHUUKEI_FLAG_5.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_5.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_5.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_5.popupWindowSetting")));
            this.SHUUKEI_FLAG_5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_5.RegistCheckMethod")));
            this.SHUUKEI_FLAG_5.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_5.TabIndex = 10010;
            this.SHUUKEI_FLAG_5.Text = "集計";
            this.SHUUKEI_FLAG_5.UseVisualStyleBackColor = false;
            // 
            // PatternList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SHUUKEI_FLAG_5);
            this.Controls.Add(this.SHUTSURYOKU_KOUMOKU_5);
            this.Controls.Add(this.SHUUKEI_FLAG_4);
            this.Controls.Add(this.SHUUKEI_FLAG_3);
            this.Controls.Add(this.SHUUKEI_FLAG_2);
            this.Controls.Add(this.SHUUKEI_FLAG_1);
            this.Controls.Add(this.SHUTSURYOKU_KOUMOKU_4);
            this.Controls.Add(this.SHUTSURYOKU_KOUMOKU_3);
            this.Controls.Add(this.SHUTSURYOKU_KOUMOKU_2);
            this.Controls.Add(this.SHUTSURYOKU_KOUMOKU_1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PATTERN_LIST_BOX);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PatternList";
            this.Size = new System.Drawing.Size(588, 133);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomListBox PATTERN_LIST_BOX;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_4;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_3;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_2;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_1;
        private r_framework.CustomControl.CustomComboBox SHUTSURYOKU_KOUMOKU_4;
        private r_framework.CustomControl.CustomComboBox SHUTSURYOKU_KOUMOKU_3;
        private r_framework.CustomControl.CustomComboBox SHUTSURYOKU_KOUMOKU_2;
        private r_framework.CustomControl.CustomComboBox SHUTSURYOKU_KOUMOKU_1;
        private System.Windows.Forms.Label label2;
        private r_framework.CustomControl.CustomComboBox SHUTSURYOKU_KOUMOKU_5;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_5;

    }
}
