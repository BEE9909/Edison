﻿namespace Shougun.Core.PaymentManagement.KaikakekinItiranHyo
{
    partial class UIHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lbl_YomikomiDataKensu = new System.Windows.Forms.Label();
            this.lbl_AlertKensu = new System.Windows.Forms.Label();
            this.txt_YomikomiDataKensu = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txt_AlertKensu = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_DenpyoDate = new System.Windows.Forms.Label();
            this.dtp_DenpyoDateFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.dtp_DenpyoDateTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(10, 5);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(304, 34);
            this.lb_title.Text = "買掛金一覧表";
            // 
            // lbl_YomikomiDataKensu
            // 
            this.lbl_YomikomiDataKensu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_YomikomiDataKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_YomikomiDataKensu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_YomikomiDataKensu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_YomikomiDataKensu.ForeColor = System.Drawing.Color.White;
            this.lbl_YomikomiDataKensu.Location = new System.Drawing.Point(973, 24);
            this.lbl_YomikomiDataKensu.Name = "lbl_YomikomiDataKensu";
            this.lbl_YomikomiDataKensu.Size = new System.Drawing.Size(110, 20);
            this.lbl_YomikomiDataKensu.TabIndex = 532;
            this.lbl_YomikomiDataKensu.Text = "読込データ件数";
            this.lbl_YomikomiDataKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AlertKensu
            // 
            this.lbl_AlertKensu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_AlertKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_AlertKensu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_AlertKensu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_AlertKensu.ForeColor = System.Drawing.Color.White;
            this.lbl_AlertKensu.Location = new System.Drawing.Point(973, 2);
            this.lbl_AlertKensu.Name = "lbl_AlertKensu";
            this.lbl_AlertKensu.Size = new System.Drawing.Size(110, 20);
            this.lbl_AlertKensu.TabIndex = 533;
            this.lbl_AlertKensu.Text = "アラート件数";
            this.lbl_AlertKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_AlertKensu.Visible = false;
            // 
            // txt_YomikomiDataKensu
            // 
            this.txt_YomikomiDataKensu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_YomikomiDataKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_YomikomiDataKensu.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_YomikomiDataKensu.DisplayPopUp = null;
            this.txt_YomikomiDataKensu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_YomikomiDataKensu.FocusOutCheckMethod")));
            this.txt_YomikomiDataKensu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_YomikomiDataKensu.ForeColor = System.Drawing.Color.Black;
            this.txt_YomikomiDataKensu.IsInputErrorOccured = false;
            this.txt_YomikomiDataKensu.Location = new System.Drawing.Point(1088, 24);
            this.txt_YomikomiDataKensu.Name = "txt_YomikomiDataKensu";
            this.txt_YomikomiDataKensu.PopupAfterExecute = null;
            this.txt_YomikomiDataKensu.PopupBeforeExecute = null;
            this.txt_YomikomiDataKensu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_YomikomiDataKensu.PopupSearchSendParams")));
            this.txt_YomikomiDataKensu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_YomikomiDataKensu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_YomikomiDataKensu.popupWindowSetting")));
            this.txt_YomikomiDataKensu.RangeSetting = rangeSettingDto1;
            this.txt_YomikomiDataKensu.ReadOnly = true;
            this.txt_YomikomiDataKensu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_YomikomiDataKensu.RegistCheckMethod")));
            this.txt_YomikomiDataKensu.Size = new System.Drawing.Size(80, 20);
            this.txt_YomikomiDataKensu.TabIndex = 534;
            this.txt_YomikomiDataKensu.TabStop = false;
            this.txt_YomikomiDataKensu.Tag = "検索結果の総件数が表示されます";
            this.txt_YomikomiDataKensu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_YomikomiDataKensu.WordWrap = false;
            // 
            // txt_AlertKensu
            // 
            this.txt_AlertKensu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_AlertKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_AlertKensu.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_AlertKensu.DisplayPopUp = null;
            this.txt_AlertKensu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_AlertKensu.FocusOutCheckMethod")));
            this.txt_AlertKensu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_AlertKensu.ForeColor = System.Drawing.Color.Black;
            this.txt_AlertKensu.IsInputErrorOccured = false;
            this.txt_AlertKensu.Location = new System.Drawing.Point(1088, 2);
            this.txt_AlertKensu.Name = "txt_AlertKensu";
            this.txt_AlertKensu.PopupAfterExecute = null;
            this.txt_AlertKensu.PopupBeforeExecute = null;
            this.txt_AlertKensu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_AlertKensu.PopupSearchSendParams")));
            this.txt_AlertKensu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_AlertKensu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_AlertKensu.popupWindowSetting")));
            this.txt_AlertKensu.RangeSetting = rangeSettingDto2;
            this.txt_AlertKensu.ReadOnly = true;
            this.txt_AlertKensu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_AlertKensu.RegistCheckMethod")));
            this.txt_AlertKensu.Size = new System.Drawing.Size(80, 20);
            this.txt_AlertKensu.TabIndex = 535;
            this.txt_AlertKensu.TabStop = false;
            this.txt_AlertKensu.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.txt_AlertKensu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_AlertKensu.Visible = false;
            this.txt_AlertKensu.WordWrap = false;
            // 
            // lbl_DenpyoDate
            // 
            this.lbl_DenpyoDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_DenpyoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_DenpyoDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_DenpyoDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_DenpyoDate.ForeColor = System.Drawing.Color.White;
            this.lbl_DenpyoDate.Location = new System.Drawing.Point(549, 24);
            this.lbl_DenpyoDate.Name = "lbl_DenpyoDate";
            this.lbl_DenpyoDate.Size = new System.Drawing.Size(110, 20);
            this.lbl_DenpyoDate.TabIndex = 541;
            this.lbl_DenpyoDate.Text = "伝票日付";
            this.lbl_DenpyoDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_DenpyoDateFrom
            // 
            this.dtp_DenpyoDateFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_DenpyoDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_DenpyoDateFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_DenpyoDateFrom.Checked = false;
            this.dtp_DenpyoDateFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_DenpyoDateFrom.DateTimeNowYear = "";
            this.dtp_DenpyoDateFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_DenpyoDateFrom.DisplayPopUp = null;
            this.dtp_DenpyoDateFrom.Enabled = false;
            this.dtp_DenpyoDateFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DenpyoDateFrom.FocusOutCheckMethod")));
            this.dtp_DenpyoDateFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_DenpyoDateFrom.ForeColor = System.Drawing.Color.Black;
            this.dtp_DenpyoDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DenpyoDateFrom.IsInputErrorOccured = false;
            this.dtp_DenpyoDateFrom.Location = new System.Drawing.Point(664, 24);
            this.dtp_DenpyoDateFrom.MaxLength = 10;
            this.dtp_DenpyoDateFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_DenpyoDateFrom.Name = "dtp_DenpyoDateFrom";
            this.dtp_DenpyoDateFrom.NullValue = "";
            this.dtp_DenpyoDateFrom.PopupAfterExecute = null;
            this.dtp_DenpyoDateFrom.PopupBeforeExecute = null;
            this.dtp_DenpyoDateFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_DenpyoDateFrom.PopupSearchSendParams")));
            this.dtp_DenpyoDateFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_DenpyoDateFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_DenpyoDateFrom.popupWindowSetting")));
            this.dtp_DenpyoDateFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DenpyoDateFrom.RegistCheckMethod")));
            this.dtp_DenpyoDateFrom.Size = new System.Drawing.Size(138, 20);
            this.dtp_DenpyoDateFrom.TabIndex = 542;
            this.dtp_DenpyoDateFrom.TabStop = false;
            this.dtp_DenpyoDateFrom.Tag = "";
            this.dtp_DenpyoDateFrom.Text = "2013/12/09(月)";
            this.dtp_DenpyoDateFrom.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(806, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(19, 20);
            this.label38.TabIndex = 543;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_DenpyoDateTo
            // 
            this.dtp_DenpyoDateTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_DenpyoDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_DenpyoDateTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_DenpyoDateTo.Checked = false;
            this.dtp_DenpyoDateTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_DenpyoDateTo.DateTimeNowYear = "";
            this.dtp_DenpyoDateTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_DenpyoDateTo.DisplayPopUp = null;
            this.dtp_DenpyoDateTo.Enabled = false;
            this.dtp_DenpyoDateTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DenpyoDateTo.FocusOutCheckMethod")));
            this.dtp_DenpyoDateTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_DenpyoDateTo.ForeColor = System.Drawing.Color.Black;
            this.dtp_DenpyoDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DenpyoDateTo.IsInputErrorOccured = false;
            this.dtp_DenpyoDateTo.Location = new System.Drawing.Point(829, 24);
            this.dtp_DenpyoDateTo.MaxLength = 10;
            this.dtp_DenpyoDateTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_DenpyoDateTo.Name = "dtp_DenpyoDateTo";
            this.dtp_DenpyoDateTo.NullValue = "";
            this.dtp_DenpyoDateTo.PopupAfterExecute = null;
            this.dtp_DenpyoDateTo.PopupBeforeExecute = null;
            this.dtp_DenpyoDateTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_DenpyoDateTo.PopupSearchSendParams")));
            this.dtp_DenpyoDateTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_DenpyoDateTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_DenpyoDateTo.popupWindowSetting")));
            this.dtp_DenpyoDateTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DenpyoDateTo.RegistCheckMethod")));
            this.dtp_DenpyoDateTo.Size = new System.Drawing.Size(138, 20);
            this.dtp_DenpyoDateTo.TabIndex = 544;
            this.dtp_DenpyoDateTo.TabStop = false;
            this.dtp_DenpyoDateTo.Tag = "";
            this.dtp_DenpyoDateTo.Text = "2013/12/09(月)";
            this.dtp_DenpyoDateTo.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.dtp_DenpyoDateTo);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.dtp_DenpyoDateFrom);
            this.Controls.Add(this.lbl_DenpyoDate);
            this.Controls.Add(this.lbl_YomikomiDataKensu);
            this.Controls.Add(this.lbl_AlertKensu);
            this.Controls.Add(this.txt_AlertKensu);
            this.Controls.Add(this.txt_YomikomiDataKensu);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.txt_YomikomiDataKensu, 0);
            this.Controls.SetChildIndex(this.txt_AlertKensu, 0);
            this.Controls.SetChildIndex(this.lbl_AlertKensu, 0);
            this.Controls.SetChildIndex(this.lbl_YomikomiDataKensu, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lbl_DenpyoDate, 0);
            this.Controls.SetChildIndex(this.dtp_DenpyoDateFrom, 0);
            this.Controls.SetChildIndex(this.label38, 0);
            this.Controls.SetChildIndex(this.dtp_DenpyoDateTo, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_YomikomiDataKensu;
        private System.Windows.Forms.Label lbl_AlertKensu;
        public r_framework.CustomControl.CustomNumericTextBox2 txt_YomikomiDataKensu;
        public r_framework.CustomControl.CustomNumericTextBox2 txt_AlertKensu;
        private System.Windows.Forms.Label lbl_DenpyoDate;
        public r_framework.CustomControl.CustomDateTimePicker dtp_DenpyoDateFrom;
        private System.Windows.Forms.Label label38;
        public r_framework.CustomControl.CustomDateTimePicker dtp_DenpyoDateTo;

    }
}