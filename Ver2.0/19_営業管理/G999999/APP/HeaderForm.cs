// $Id: HeaderForm.cs 3552 2013-10-11 10:16:21Z sys_dev_24 $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Shougun.Core.BusinessManagement.Soushinpyoubikou
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        public HeaderForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "送信票備考";
            base.windowTypeLabel.Text = "";
            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// 拠点コード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KYOTEN_CD_Leave(object sender, EventArgs e)
        {
            int i;
            if (!int.TryParse(this.KYOTEN_CD.Text, out i))
            {
                this.KYOTEN_CD.Text = string.Empty;
            }
        }
    }
}
