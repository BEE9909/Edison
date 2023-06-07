﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Billing.SeikyuShimeShori
{
    public partial class UIHeader : HeaderBaseForm
    {

        internal string headerTittle = string.Empty;

        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = headerTittle;
        }

        internal void SetHeaderTittle()
        {
            base.lb_title.Text = headerTittle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HOGE");
        }
    }
}
