﻿namespace TorihikisakiHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class NyuukinsakiIchiranDetail
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
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

        #region MultiRow Template Designer generated code

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyuukinsakiIchiranDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.ITAKU_KEIYAKU_NO = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.NYUUKINSAKI_CD = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.NYUUKINSAKI_NAME1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.NYUUKINSAKI_ADDRESS1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.NYUUKINSAKI_NAME2 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.NYUUKINSAKI_ADDRESS2 = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.NYUUKINSAKI_CD);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_NO);
            this.Row.Cells.Add(this.NYUUKINSAKI_NAME1);
            this.Row.Cells.Add(this.NYUUKINSAKI_ADDRESS1);
            this.Row.Cells.Add(this.NYUUKINSAKI_NAME2);
            this.Row.Cells.Add(this.NYUUKINSAKI_ADDRESS2);
            this.Row.Height = 42;
            this.Row.Width = 635;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader5);
            this.columnHeaderSection1.Height = 42;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 635;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(65, 42);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle6.Border = border1;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader1.Style = cellStyle6;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.Value = "入金先CD";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FilterCellIndex = 1;
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(65, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(290, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle7.Border = border2;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle7;
            this.gcCustomColumnHeader2.TabIndex = 7;
            this.gcCustomColumnHeader2.Value = "入金先名1";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FilterCellIndex = 3;
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(65, 21);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(290, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle8.Border = border3;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle8;
            this.gcCustomColumnHeader3.TabIndex = 4;
            this.gcCustomColumnHeader3.Value = "入金先名2";
            this.gcCustomColumnHeader3.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FilterCellIndex = 2;
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(355, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(290, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border4;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle9;
            this.gcCustomColumnHeader4.TabIndex = 2;
            this.gcCustomColumnHeader4.Value = "入金先住所1";
            this.gcCustomColumnHeader4.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FilterCellIndex = 4;
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(355, 21);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(290, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle10.Border = border5;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader5.Style = cellStyle10;
            this.gcCustomColumnHeader5.TabIndex = 5;
            this.gcCustomColumnHeader5.Value = "入金先住所2";
            this.gcCustomColumnHeader5.ViewSearchItem = false;
            // 
            // ITAKU_KEIYAKU_NO
            // 
            this.ITAKU_KEIYAKU_NO.Location = new System.Drawing.Point(0, 0);
            this.ITAKU_KEIYAKU_NO.Name = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.ReadOnly = true;
            this.ITAKU_KEIYAKU_NO.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Vertical;
            this.ITAKU_KEIYAKU_NO.Size = new System.Drawing.Size(0, 0);
            this.ITAKU_KEIYAKU_NO.TabIndex = 6;
            this.ITAKU_KEIYAKU_NO.Tag = "";
            // 
            // NYUUKINSAKI_CD
            // 
            this.NYUUKINSAKI_CD.DataField = "NYUUKINSAKI_CD";
            this.NYUUKINSAKI_CD.DBFieldsName = "NYUUKINSAKI_CD";
            this.NYUUKINSAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_CD.DisplayItemName = "入金先CD";
            this.NYUUKINSAKI_CD.DisplayPopUp = null;
            this.NYUUKINSAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_CD.FocusOutCheckMethod")));
            this.NYUUKINSAKI_CD.IsInputErrorOccured = false;
            this.NYUUKINSAKI_CD.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_CD.Location = new System.Drawing.Point(0, 0);
            this.NYUUKINSAKI_CD.Name = "NYUUKINSAKI_CD";
            this.NYUUKINSAKI_CD.PopupAfterExecute = null;
            this.NYUUKINSAKI_CD.PopupBeforeExecute = null;
            this.NYUUKINSAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_CD.PopupSearchSendParams")));
            this.NYUUKINSAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_CD.PopupWindowName = "";
            this.NYUUKINSAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_CD.popupWindowSetting")));
            this.NYUUKINSAKI_CD.ReadOnly = true;
            this.NYUUKINSAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_CD.RegistCheckMethod")));
            this.NYUUKINSAKI_CD.ShortItemName = "";
            this.NYUUKINSAKI_CD.Size = new System.Drawing.Size(65, 42);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.NYUUKINSAKI_CD.Style = cellStyle1;
            this.NYUUKINSAKI_CD.TabIndex = 0;
            this.NYUUKINSAKI_CD.Tag = "入金先CDが表示されます";
            // 
            // NYUUKINSAKI_NAME1
            // 
            this.NYUUKINSAKI_NAME1.DataField = "NYUUKINSAKI_NAME1";
            this.NYUUKINSAKI_NAME1.DBFieldsName = "NYUUKINSAKI_NAME1";
            this.NYUUKINSAKI_NAME1.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_NAME1.DisplayItemName = "入金先名1";
            this.NYUUKINSAKI_NAME1.DisplayPopUp = null;
            this.NYUUKINSAKI_NAME1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME1.FocusOutCheckMethod")));
            this.NYUUKINSAKI_NAME1.IsInputErrorOccured = false;
            this.NYUUKINSAKI_NAME1.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_NAME1.Location = new System.Drawing.Point(65, 0);
            this.NYUUKINSAKI_NAME1.Name = "NYUUKINSAKI_NAME1";
            this.NYUUKINSAKI_NAME1.PopupAfterExecute = null;
            this.NYUUKINSAKI_NAME1.PopupBeforeExecute = null;
            this.NYUUKINSAKI_NAME1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_NAME1.PopupSearchSendParams")));
            this.NYUUKINSAKI_NAME1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_NAME1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_NAME1.popupWindowSetting")));
            this.NYUUKINSAKI_NAME1.ReadOnly = true;
            this.NYUUKINSAKI_NAME1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME1.RegistCheckMethod")));
            this.NYUUKINSAKI_NAME1.ShortItemName = "";
            this.NYUUKINSAKI_NAME1.Size = new System.Drawing.Size(290, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.NYUUKINSAKI_NAME1.Style = cellStyle2;
            this.NYUUKINSAKI_NAME1.TabIndex = 1;
            this.NYUUKINSAKI_NAME1.Tag = "入金先名1が表示されます";
            // 
            // NYUUKINSAKI_ADDRESS1
            // 
            this.NYUUKINSAKI_ADDRESS1.DataField = "NYUUKINSAKI_ADDRESS1";
            this.NYUUKINSAKI_ADDRESS1.DBFieldsName = "NYUUKINSAKI_ADDRESS1";
            this.NYUUKINSAKI_ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_ADDRESS1.DisplayItemName = "入金先住所1";
            this.NYUUKINSAKI_ADDRESS1.DisplayPopUp = null;
            this.NYUUKINSAKI_ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.FocusOutCheckMethod")));
            this.NYUUKINSAKI_ADDRESS1.IsInputErrorOccured = false;
            this.NYUUKINSAKI_ADDRESS1.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_ADDRESS1.Location = new System.Drawing.Point(355, 0);
            this.NYUUKINSAKI_ADDRESS1.Name = "NYUUKINSAKI_ADDRESS1";
            this.NYUUKINSAKI_ADDRESS1.PopupAfterExecute = null;
            this.NYUUKINSAKI_ADDRESS1.PopupBeforeExecute = null;
            this.NYUUKINSAKI_ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.PopupSearchSendParams")));
            this.NYUUKINSAKI_ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.popupWindowSetting")));
            this.NYUUKINSAKI_ADDRESS1.ReadOnly = true;
            this.NYUUKINSAKI_ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.RegistCheckMethod")));
            this.NYUUKINSAKI_ADDRESS1.ShortItemName = "";
            this.NYUUKINSAKI_ADDRESS1.Size = new System.Drawing.Size(290, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.NYUUKINSAKI_ADDRESS1.Style = cellStyle3;
            this.NYUUKINSAKI_ADDRESS1.TabIndex = 2;
            this.NYUUKINSAKI_ADDRESS1.Tag = "入金先住所1が表示されます";
            // 
            // NYUUKINSAKI_NAME2
            // 
            this.NYUUKINSAKI_NAME2.DataField = "NYUUKINSAKI_NAME2";
            this.NYUUKINSAKI_NAME2.DBFieldsName = "NYUUKINSAKI_NAME2";
            this.NYUUKINSAKI_NAME2.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_NAME2.DisplayItemName = "入金先名2";
            this.NYUUKINSAKI_NAME2.DisplayPopUp = null;
            this.NYUUKINSAKI_NAME2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME2.FocusOutCheckMethod")));
            this.NYUUKINSAKI_NAME2.IsInputErrorOccured = false;
            this.NYUUKINSAKI_NAME2.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_NAME2.Location = new System.Drawing.Point(65, 21);
            this.NYUUKINSAKI_NAME2.Name = "NYUUKINSAKI_NAME2";
            this.NYUUKINSAKI_NAME2.PopupAfterExecute = null;
            this.NYUUKINSAKI_NAME2.PopupBeforeExecute = null;
            this.NYUUKINSAKI_NAME2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_NAME2.PopupSearchSendParams")));
            this.NYUUKINSAKI_NAME2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_NAME2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_NAME2.popupWindowSetting")));
            this.NYUUKINSAKI_NAME2.ReadOnly = true;
            this.NYUUKINSAKI_NAME2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME2.RegistCheckMethod")));
            this.NYUUKINSAKI_NAME2.ShortItemName = "";
            this.NYUUKINSAKI_NAME2.Size = new System.Drawing.Size(290, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.NYUUKINSAKI_NAME2.Style = cellStyle4;
            this.NYUUKINSAKI_NAME2.TabIndex = 3;
            this.NYUUKINSAKI_NAME2.Tag = "入金先名2が表示されます";
            // 
            // NYUUKINSAKI_ADDRESS2
            // 
            this.NYUUKINSAKI_ADDRESS2.DataField = "NYUUKINSAKI_ADDRESS2";
            this.NYUUKINSAKI_ADDRESS2.DBFieldsName = "NYUUKINSAKI_ADDRESS2";
            this.NYUUKINSAKI_ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_ADDRESS2.DisplayItemName = "入金先住所2";
            this.NYUUKINSAKI_ADDRESS2.DisplayPopUp = null;
            this.NYUUKINSAKI_ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.FocusOutCheckMethod")));
            this.NYUUKINSAKI_ADDRESS2.IsInputErrorOccured = false;
            this.NYUUKINSAKI_ADDRESS2.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_ADDRESS2.Location = new System.Drawing.Point(355, 21);
            this.NYUUKINSAKI_ADDRESS2.Name = "NYUUKINSAKI_ADDRESS2";
            this.NYUUKINSAKI_ADDRESS2.PopupAfterExecute = null;
            this.NYUUKINSAKI_ADDRESS2.PopupBeforeExecute = null;
            this.NYUUKINSAKI_ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.PopupSearchSendParams")));
            this.NYUUKINSAKI_ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.popupWindowSetting")));
            this.NYUUKINSAKI_ADDRESS2.ReadOnly = true;
            this.NYUUKINSAKI_ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.RegistCheckMethod")));
            this.NYUUKINSAKI_ADDRESS2.ShortItemName = "";
            this.NYUUKINSAKI_ADDRESS2.Size = new System.Drawing.Size(290, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.NYUUKINSAKI_ADDRESS2.Style = cellStyle5;
            this.NYUUKINSAKI_ADDRESS2.TabIndex = 4;
            this.NYUUKINSAKI_ADDRESS2.Tag = "入金先住所2が表示されます";
            // 
            // NyuukinsakiIchiranDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle11.BackColor = System.Drawing.Color.Transparent;
            cellStyle11.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle11.Border = border6;
            cellStyle11.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle11.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle11.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle11.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle11.Format = "";
            cellStyle11.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle11.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle11.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle11.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle11.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle11.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle11.Margin = new System.Windows.Forms.Padding(0);
            cellStyle11.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle11.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle11.Padding = new System.Windows.Forms.Padding(0);
            cellStyle11.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle11.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle11.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle11.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle11.TextAngle = 0F;
            cellStyle11.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle11.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle11.TextIndent = 0;
            cellStyle11.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle11.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle11.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle11;
            this.Height = 84;
            this.Width = 635;

        }


        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        private GrapeCity.Win.MultiRow.TextBoxCell ITAKU_KEIYAKU_NO;
        internal r_framework.CustomControl.GcCustomTextBoxCell NYUUKINSAKI_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell NYUUKINSAKI_NAME1;
        internal r_framework.CustomControl.GcCustomTextBoxCell NYUUKINSAKI_ADDRESS1;
        internal r_framework.CustomControl.GcCustomTextBoxCell NYUUKINSAKI_NAME2;
        internal r_framework.CustomControl.GcCustomTextBoxCell NYUUKINSAKI_ADDRESS2;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;

    }
}
