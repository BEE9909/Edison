﻿using System;
using System.Windows.Forms;
using r_framework.Const;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Logic;
using Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku
{
    class G077 : r_framework.FormManager.IShougunForm // 受入入力
    {
        public Form CreateForm(params object[] args)
        {
            WINDOW_TYPE winType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                winType = (WINDOW_TYPE)args[0];             
            }

            String No = "";
            if (args.Length > 1)
            {
                No = (String)args[1];
            }

            var HeaderForm = new Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.HeaderSample();
            var callForm = new Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.UIForm(winType, No, HeaderForm);

            return new BusinessBaseForm(callForm, HeaderForm);
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (args.Length > 1)
            {
                WINDOW_TYPE Window_type = (WINDOW_TYPE)args[0];
                String Nyuukin_CD = (String)args[1];
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.UIForm;
                return (uiForm.Nyuukin_CD == Nyuukin_CD && uiForm.Window_type == Window_type);
            }
            return false;
        }

        public void UpdateForm(Form form)
        {
        }

    }
}
