﻿namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomi
{
    partial class UIForm
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.TorihikiPopupButton = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.Nyuukin_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.SEIKYUU_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.searchString.Location = new System.Drawing.Point(0, 1);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(1000, 95);
            this.searchString.TabIndex = 1;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(2, 441);
            this.bt_ptn1.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn1.TabIndex = 4;
            this.bt_ptn1.Text = "パターン１";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(203, 441);
            this.bt_ptn2.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn2.TabIndex = 5;
            this.bt_ptn2.Text = "パターン２";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(405, 441);
            this.bt_ptn3.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn3.TabIndex = 6;
            this.bt_ptn3.Text = "パターン３";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(607, 441);
            this.bt_ptn4.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn4.TabIndex = 7;
            this.bt_ptn4.Text = "パターン４";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(808, 441);
            this.bt_ptn5.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn5.TabIndex = 8;
            this.bt_ptn5.Text = "パターン５";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Location = new System.Drawing.Point(3, 118);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 24);
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(3, 95);
            // 
            // TorihikiPopupButton
            // 
            this.TorihikiPopupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TorihikiPopupButton.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TorihikiPopupButton.DBFieldsName = null;
            this.TorihikiPopupButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.TorihikiPopupButton.DisplayItemName = "取引先CD";
            this.TorihikiPopupButton.DisplayPopUp = null;
            this.TorihikiPopupButton.ErrorMessage = null;
            this.TorihikiPopupButton.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TorihikiPopupButton.FocusOutCheckMethod")));
            this.TorihikiPopupButton.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.TorihikiPopupButton.GetCodeMasterField = null;
            this.TorihikiPopupButton.Image = ((System.Drawing.Image)(resources.GetObject("TorihikiPopupButton.Image")));
            this.TorihikiPopupButton.ItemDefinedTypes = null;
            this.TorihikiPopupButton.LinkedSettingTextBox = null;
            this.TorihikiPopupButton.LinkedTextBoxs = null;
            this.TorihikiPopupButton.Location = new System.Drawing.Point(468, 24);
            this.TorihikiPopupButton.Name = "TorihikiPopupButton";
            this.TorihikiPopupButton.PopupAfterExecute = null;
            this.TorihikiPopupButton.PopupBeforeExecute = null;
            this.TorihikiPopupButton.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TorihikiPopupButton.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TorihikiPopupButton.PopupSearchSendParams")));
            this.TorihikiPopupButton.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TorihikiPopupButton.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TorihikiPopupButton.PopupWindowName = "検索共通ポップアップ";
            this.TorihikiPopupButton.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TorihikiPopupButton.popupWindowSetting")));
            this.TorihikiPopupButton.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TorihikiPopupButton.RegistCheckMethod")));
            this.TorihikiPopupButton.SearchDisplayFlag = 0;
            this.TorihikiPopupButton.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TorihikiPopupButton.ShortItemName = "取引先CD";
            this.TorihikiPopupButton.Size = new System.Drawing.Size(22, 22);
            this.TorihikiPopupButton.TabIndex = 371;
            this.TorihikiPopupButton.TabStop = false;
            this.TorihikiPopupButton.UseVisualStyleBackColor = false;
            this.TorihikiPopupButton.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先CD";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(116, 25);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ShortItemName = "取引先CD";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(60, 20);
            this.TORIHIKISAKI_CD.TabIndex = 1;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "";
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.ErrorMessage = "";
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.GetCodeMasterField = "";
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(175, 25);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 20;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupGetMasterField = "";
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupSetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.SetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 373;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "　";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(3, 25);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 372;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Nyuukin_CD
            // 
            this.Nyuukin_CD.BackColor = System.Drawing.SystemColors.Window;
            this.Nyuukin_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukin_CD.DBFieldsName = "";
            this.Nyuukin_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukin_CD.DisplayItemName = "";
            this.Nyuukin_CD.DisplayPopUp = null;
            this.Nyuukin_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukin_CD.FocusOutCheckMethod")));
            this.Nyuukin_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.Nyuukin_CD.ForeColor = System.Drawing.Color.Black;
            this.Nyuukin_CD.GetCodeMasterField = "";
            this.Nyuukin_CD.IsInputErrorOccured = false;
            this.Nyuukin_CD.ItemDefinedTypes = "";
            this.Nyuukin_CD.Location = new System.Drawing.Point(116, 47);
            this.Nyuukin_CD.Name = "Nyuukin_CD";
            this.Nyuukin_CD.PopupAfterExecute = null;
            this.Nyuukin_CD.PopupBeforeExecute = null;
            this.Nyuukin_CD.PopupGetMasterField = "";
            this.Nyuukin_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukin_CD.PopupSearchSendParams")));
            this.Nyuukin_CD.PopupSetFormField = "";
            this.Nyuukin_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Nyuukin_CD.PopupWindowName = "";
            this.Nyuukin_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukin_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.Nyuukin_CD.RangeSetting = rangeSettingDto1;
            this.Nyuukin_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukin_CD.RegistCheckMethod")));
            this.Nyuukin_CD.SetFormField = "";
            this.Nyuukin_CD.ShortItemName = "";
            this.Nyuukin_CD.Size = new System.Drawing.Size(60, 20);
            this.Nyuukin_CD.TabIndex = 2;
            this.Nyuukin_CD.Tag = "半角10桁以内で入力してください";
            this.Nyuukin_CD.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 375;
            this.label1.Text = "入金番号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEIKYUU_NUMBER
            // 
            this.SEIKYUU_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.SEIKYUU_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEIKYUU_NUMBER.DBFieldsName = "";
            this.SEIKYUU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEIKYUU_NUMBER.DisplayItemName = "";
            this.SEIKYUU_NUMBER.DisplayPopUp = null;
            this.SEIKYUU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_NUMBER.FocusOutCheckMethod")));
            this.SEIKYUU_NUMBER.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEIKYUU_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_NUMBER.GetCodeMasterField = "";
            this.SEIKYUU_NUMBER.IsInputErrorOccured = false;
            this.SEIKYUU_NUMBER.ItemDefinedTypes = "";
            this.SEIKYUU_NUMBER.Location = new System.Drawing.Point(116, 70);
            this.SEIKYUU_NUMBER.Name = "SEIKYUU_NUMBER";
            this.SEIKYUU_NUMBER.PopupAfterExecute = null;
            this.SEIKYUU_NUMBER.PopupBeforeExecute = null;
            this.SEIKYUU_NUMBER.PopupGetMasterField = "";
            this.SEIKYUU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_NUMBER.PopupSearchSendParams")));
            this.SEIKYUU_NUMBER.PopupSetFormField = "";
            this.SEIKYUU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_NUMBER.PopupWindowName = "";
            this.SEIKYUU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_NUMBER.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.SEIKYUU_NUMBER.RangeSetting = rangeSettingDto2;
            this.SEIKYUU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_NUMBER.RegistCheckMethod")));
            this.SEIKYUU_NUMBER.SetFormField = "";
            this.SEIKYUU_NUMBER.ShortItemName = "";
            this.SEIKYUU_NUMBER.Size = new System.Drawing.Size(60, 20);
            this.SEIKYUU_NUMBER.TabIndex = 376;
            this.SEIKYUU_NUMBER.Tag = "半角10桁以内で入力してください";
            this.SEIKYUU_NUMBER.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 377;
            this.label2.Text = "請求番号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(527, 24);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 672;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1004, 490);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.SEIKYUU_NUMBER);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Nyuukin_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TorihikiPopupButton);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Name = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_CD, 0);
            this.Controls.SetChildIndex(this.TorihikiPopupButton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.Nyuukin_CD, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.SEIKYUU_NUMBER, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomAlphaNumTextBox txt_TorihikisakiCD;
        private System.Windows.Forms.Label lbl_Torohikisaki;
        private r_framework.CustomControl.CustomTextBox txt_TorihikisakiName;
        internal r_framework.CustomControl.CustomPopupOpenButton TorihikiPopupButton;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomNumericTextBox2 Nyuukin_CD;
        internal System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomNumericTextBox2 SEIKYUU_NUMBER;
        internal System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}