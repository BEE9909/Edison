﻿using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.HaikibutuTyoubo
{
    /// <summary>
    /// G133 廃棄物帳簿
    /// </summary>
    public class G133 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var headerForm = new Shougun.Core.PaperManifest.HaikibutuTyoubo.UIHeader();
            var callForm = new Shougun.Core.PaperManifest.HaikibutuTyoubo.UIForm(headerForm);
            return new BusinessBaseForm(callForm, headerForm);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 一覧画面のためtrueを返却
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
