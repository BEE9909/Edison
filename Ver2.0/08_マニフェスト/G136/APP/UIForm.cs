﻿// $Id: UIForm.cs 28506 2014-08-25 08:36:52Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon;
using System.Data;
using System.Collections.Generic;
using GrapeCity.Win.MultiRow;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyusei
{

    public partial class JissekiHokokuSyuseiForm : SuperForm
    {
        internal HeaderForm header;
        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private JissekiHokokuSyuseiLogic logic;

        /// <summary>
        /// コントロールindex
        /// </summary>
        private int index;

        /// <summary>
        /// SYSTEM_ID
        /// </summary>
        internal String systemid;

        /// <summary>
        /// 前回値チェック用変数(明細用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// エラーセル
        /// </summary>
        private Cell errorCell;

        /// <summary>
        /// 明細でエラーが起きたかどうか判断するためのフラグ
        /// </summary>
        internal bool bErrOrPopFlag = false;

        /// <summary>
        /// Validatedを行うかどうか判断するためのフラグ
        /// </summary>
        internal bool validatedFlag = true;

        /// <summary>
        /// 画面はロード中かどうか判断するためのフラグ
        /// </summary>
        internal bool isOnloadFlag = false;

        #region UIForm
        // <summary>
        /// UIForm
        /// </summary>
        public JissekiHokokuSyuseiForm(HeaderForm headerForm, WINDOW_ID windowId, WINDOW_TYPE windowType, string systemid)
            : base(windowId, windowType)
        {
            this.systemid = systemid;

            this.InitializeComponent();

            this.ControlEnabledSet(windowType == WINDOW_TYPE.DELETE_WINDOW_FLAG);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new JissekiHokokuSyuseiLogic(this);

            //ヘッダ
            this.header = headerForm;
            this.logic.SetHeaderInfo(this.header);
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);

                this.logic.WindowInit();        // 画面情報の初期化
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
        }
        #endregion

        /// <summary>
        /// コントロールが入力できるかどうか設定
        /// </summary>
        /// <param name="enabled">入力できるかどうか</param>
        private void ControlEnabledSet(bool enabled)
        {
            this.grdIchiran.ReadOnly = enabled;
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UpdateMode(object sender, EventArgs e)
        {
            // 処理モード変更
            base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            base.HeaderFormInit();
            this.ControlEnabledSet(false);
            this.logic.UpdateMode(sender, e);
        }

        /// <summary>
        /// 委託先パップアップ後
        /// </summary>
        public void Itakusaki_PopupAfter()
        {
            this.logic.Itakusaki_PopupAfter(this.index);
        }

        /// <summary>
        /// 現場パップアップ後
        /// </summary>
        public void GetGenba_PopupAfter()
        {
            this.logic.GetGenba_PopupAfter(this.index);
        }

        /// <summary>
        /// 業者パップアップ後
        /// </summary>
        public void GetGyosha_PopupAfter()
        {
            this.logic.SbnGensha_PopupAfter(this.index);
        }

        /// <summary>
        /// グリッドのダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdIchiran_CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            try
            {
                if (!this.logic.cellDoubleClick(sender, e)) { return; }
                switch (e.CellName)
                {
                    case "gcCustomPopupOpenButtonCell2":
                    case "gcCustomPopupOpenButtonCell3":
                    case "gcCustomPopupOpenButtonCell6":
                    case "gcCustomPopupOpenButtonCell7":
                    case "gcCustomPopupOpenButtonCell8":
                        this.index = e.RowIndex;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        /// <summary>
        /// グリッドのLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdIchiran_CellLeave(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            string format = mSysInfo.SYS_SUURYOU_FORMAT.ToString();
            //switch (e.CellName)
            //{
            //    case "HoukokushoBunruiCd":
            //        this.logic.HaikiShuruiCD_PopupAfter(e.RowIndex);
            //        break;
            //    //case "JyutakuRyou":
            //    //    if (!string.IsNullOrWhiteSpace(this.grdIchiran.Rows[e.RowIndex].Cells["JyutakuRyou"].Value.ToString()))
            //    //    {
            //    //        this.grdIchiran.Rows[e.RowIndex]["JyutakuRyou"].Value = string.Format("{0:" + format + "}",
            //    //                   Double.Parse(this.grdIchiran.Rows[e.RowIndex].Cells["JyutakuRyou"].Value.ToString()));
            //    //    }
            //    //    break;
            //    //case "ItakuRyou":
            //    //    if (!string.IsNullOrWhiteSpace(this.grdIchiran.Rows[e.RowIndex].Cells["ItakuRyou"].Value.ToString()))
            //    //    {
            //    //        this.grdIchiran.Rows[e.RowIndex]["ItakuRyou"].Value = string.Format("{0:" + format + "}",
            //    //                   Double.Parse(this.grdIchiran.Rows[e.RowIndex].Cells["ItakuRyou"].Value.ToString()));
            //    //    }
            //    //    break;
            //    default:
            //        break;
            //}
        }

        /// <summary>
        /// グリッドのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdIchiran_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            switch (e.CellName)
            {
                case "gcCustomPopupOpenButtonCell2":
                case "gcCustomPopupOpenButtonCell3":
                case "gcCustomPopupOpenButtonCell6":
                case "gcCustomPopupOpenButtonCell7":
                case "gcCustomPopupOpenButtonCell8":
                    this.index = e.RowIndex;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 明細のCellValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void grdIchiran_CellValidated(object sender, CellEventArgs e)
        {
            if (!this.validatedFlag) { return; }
            if (Convert.ToString(this.grdIchiran.Rows[e.RowIndex].Cells[e.CellName].Value) == beforeValuesForDetail[e.CellName])
            {
                return;
            }
            //switch (e.CellName)
            //{
            //    case "HoukokushoBunruiCd":
            //        // 廃棄物CD
            //        this.grdIchiran.Rows[e.RowIndex].Cells["HoukokushoBunruiName"].Value = "";
            //        if (string.IsNullOrEmpty(Convert.ToString(this.grdIchiran.Rows[e.RowIndex].Cells[e.CellName].Value))) { return; }
            //        M_CHIIKIBETSU_BUNRUI[] bunruiResult = this.logic.GetBunrui(this.TeishutuSakiCd.Text, Convert.ToString(this.grdIchiran.Rows[e.RowIndex].Cells[e.CellName].Value));
            //        if (bunruiResult == null || bunruiResult.Length == 0)
            //        {
            //            var messageShowLogic = new MessageBoxShowLogic();
            //            messageShowLogic.MessageBoxShow("E020", "地域別分類");
            //            GcMultiRow gc = sender as GcMultiRow;
            //            if (gc != null && gc.EditingControl != null)
            //            {
            //                ((TextBoxEditingControl)gc.EditingControl).SelectAll();
            //            }
            //            this.bErrOrPopFlag = true;
            //            this.errorCell = this.grdIchiran.Rows[e.RowIndex].Cells[e.CellName];
            //            return;
            //        }
            //        else
            //        {
            //            this.grdIchiran.Rows[e.RowIndex].Cells["HoukokushoBunruiName"].Value = bunruiResult[0].HOUKOKU_BUNRUI_NAME;
            //        }
            //        break;
            //}
        }

        /// <summary>
        /// 明細のCellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void grdIchiran_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Row row = this.grdIchiran.CurrentRow;

            if (row == null)
            {
                return;
            }

            //ポップアップキャンセル時、値が再保存されないように制御する。
            if (!bErrOrPopFlag)
            {
                // 前回値チェック用データをセット
                if (beforeValuesForDetail.ContainsKey(e.CellName))
                {
                    beforeValuesForDetail[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
                }
                else
                {
                    beforeValuesForDetail.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
                }
            }
            else
            {
                if (this.errorCell != null)
                {
                    this.validatedFlag = false;
                    this.grdIchiran.CurrentCell = this.errorCell;
                    this.errorCell = null;
                    if (beforeValuesForDetail.ContainsKey(e.CellName))
                    {
                        beforeValuesForDetail[e.CellName] = string.Empty;
                    }
                    this.validatedFlag = true;
                }
                bErrOrPopFlag = false;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void grdIchiran_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.errorCell != null)
            {
                e.Cancel = true;
            }
        }
    }
}