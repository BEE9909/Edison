﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Const;

namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        #region イベント

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        #endregion
    }
}
