﻿// $Id: M216.cs 17036 2014-03-06 12:44:54Z y-sato $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace GyoushaIchiran.APP
{
    /// <summary>
    /// M216 業者一覧
    /// </summary>
    class M216 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            DENSHU_KBN denshuKbn = DENSHU_KBN.GYOUSHA;
            if (args.Length > 0)
            {
                denshuKbn = (DENSHU_KBN)args[0];
            }

            var callForm = new GyoushaIchiranForm(denshuKbn);
            return new IchiranBaseForm(callForm, denshuKbn);
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新処理
        /// </summary>
        /// <param name="form"></param>
        public void UpdateForm(Form form)
        {
        }
    }
}
