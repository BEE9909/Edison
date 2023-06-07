﻿using System;
using System.Windows.Forms;
using logic = r_framework.Logic.CustomPostalCodeTextBoxLogic;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public class DgvCustomPostalCodeTextBoxEditingControl : DataGridViewTextBoxEditingControl
    {
        public override int MaxLength
        {
            get { return base.MaxLength; }
            set { base.MaxLength = value; }
        }

        public new ImeMode ImeMode
        {
            get { return base.ImeMode; }
            set { base.ImeMode = ImeMode.Disable; }
        }
        protected override ImeMode ImeModeBase
        {
            get { return base.ImeModeBase; }
            set { base.ImeModeBase = ImeMode.Disable; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            // 勝手にIMEモードが有効になってしまう現象の対策
            this.ImeMode = ImeMode.Disable;

            //
            var cell = this.EditingControlDataGridView.CurrentCell as DgvCustomPostalCodeTextBoxCell;
            this.MaxLength = cell.MaxInputLength;

            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                if (!this.ReadOnly)
                {
                    // フォーカス取得でテキスト全選択させたいが、なぜかここでは選択されないので
                    // Enterイベント終わった後に実行させる。
                    this.BeginInvoke((Action)this.SelectAll);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            bool accept = logic.CanAcceptOnKeyPress(e.KeyChar);
            if (!accept)
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x302: /*WM_PASTE*/
                    if (!logic.CanAcceptClipboardText())
                        return;
                    break;

                default:
                    break;
            }

            base.WndProc(ref m);
        }
    }
}