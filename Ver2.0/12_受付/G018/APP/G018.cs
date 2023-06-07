﻿using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.Reception.UketsukeMochikomiNyuuryoku
{
    /// <summary>
    /// G018 受付（持込）入力
    /// </summary>
    class G018 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : 受付番号
            string uketsukeNumber = string.Empty;
            if (args.Length > 1 && args[1] != null)
            {
                uketsukeNumber = (string)args[1];
            }

            // 引数 arg[2] :取引先CD
            // 引数 arg[3] :業者CD
            // 引数 arg[4] :現場CD
            UIForm callForm = null;
            //データ移動モード判定
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                args.Length > 4 &&
                args[1] == null)
            {
                callForm = new UIForm(windowType, (string)args[2], (string)args[3], (string)args[4]);
            }
            else
            {
                string SEQ = string.Empty;
                // 引数 arg[5] :SQE
                if (windowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG &&
                    args.Length > 5)
                {
                    SEQ = (string)args[5];
                }

                callForm = new UIForm(windowType, uketsukeNumber, SEQ);
                // 受付番号のデータが存在するかチェック
                var isExistUkeireData = callForm.IsExistData();
                if (!isExistUkeireData)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    callForm.Dispose();
                    return null;
                }
            }



            return new BusinessBaseForm(callForm, new UIHeaderForm());
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ受入番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1)
            {
                string uketsukeNumber = (string)args[1];
                var baseForm = form as BusinessBaseForm;
                var uiForm = baseForm.inForm as UIForm;
                return (uiForm.UketsukeNumber == uketsukeNumber);
            }
            return false;
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
