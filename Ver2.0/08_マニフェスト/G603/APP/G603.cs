﻿using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.JissekiHokokuSisetsu
{
    /// <summary>
    /// G603 実績報告書_処理施設_条件指定
    /// </summary>
    class G603 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var callForm = new Shougun.Core.PaperManifest.JissekiHokokuSisetsu.UIForm();
            var callHeader = new Shougun.Core.PaperManifest.JissekiHokokuSisetsu.UIHeader();
            
            if (args.Length > 0)
            {
                callForm.fromKbn = args[0].ToString();
            }
            
            if (args.Length > 1)
            {
                callForm.SYSTEM_ID = Convert.ToInt16(args[1]);
            }

            var businessForm = new BasePopForm(callForm, callHeader);
            return businessForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 常に前面表示
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name=form>表示を更新するフォーム</param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {
        }
    }
}
