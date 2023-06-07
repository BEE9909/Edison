using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Exceptions;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DTO;
using Shougun.Core.BusinessManagement.Const.Common;
using r_framework.FormManager;
//using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
//using Shougun.Core.ExternalConnection.CommunicateLib.Logic;
//using Shougun.Core.FileUpload.FileUploadCommon.DTO;
//using Shougun.Core.FileUpload.FileUploadCommon.Logic;
//using SystemSetteiHoshu.APP;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
{
    public class InitialPopupFormLogic
    {
        internal SuperEntity[] entity { get; set; }

        private InitialPopupForm form;
        private MitsumoriNyuryokuForm formMitsu;

        internal Control[] popupViewControls { get; set; }





        private static readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Setting.PopupButtonSetting.xml";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InitialPopupFormLogic(InitialPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                // ボタンの初期化
                this.ButtonInit();

                // イベント初期化
                this.EventInit();

                CustomNumericTextBox2 initTextBox;





                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M261", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            //画面制御ボタン(F1)イベント生成
            //this.form.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

            this.form.bt_func5.Click += new EventHandler(this.form.Clear);

            //反映ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);



        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {


            // FunctionButton
            this.form.bt_func9.Enabled = false;
        }



        /// <summary>
        /// 反映ボタン押下時の入力チェック
        /// </summary>
        /// <returns>true:エラーあり、false:エラーなし</returns>


        /// <summary>
        /// コンボボックスの初期化
        /// </summary>
        /// <param name="dbConnectionString"></param>


        /// <summary>
        /// XMLからDB接続情報取得
        /// </summary>
        /// <returns></returns>


        /// <summary>
        /// コンボボックスの選択値チェック
        /// </summary>


        /// <summary>
        /// コンボボックス、閉じるボタン以外のコントロールの活性制御
        /// </summary>
        /// <param name="enabled"></param>
        private void SetAllControlsEnabled(bool enabled)
        {
            // 「接続先コンボボックス」と「F12 閉じる」ボタン以外のコントロールが作成されたら追加
            this.form.bt_func9.Enabled = enabled;
        }

        #region InxsSubapplication Database connection setting

        /// <summary>
        /// Set list database connection to combobox
        /// </summary>
        /// <param name="dbConnectionString"></param>


        /// <summary>
        /// Get list InxsSubapp DB connection list
        /// </summary>
        /// <returns></returns>




        #endregion





        private bool checkTableLog(DBConnectionDTOLOG dto)
        {
            try
            {
                var daoLog = (IS2Container)SingletonS2ContainerFactory.Container.GetComponent(Constans.DAO_LOG);
                var dataSourceFile = (Seasar.Extension.Tx.Impl.TxDataSource)daoLog.GetComponent("DataSource");
                dataSourceFile.ConnectionString = dto.ConnectionString;
                using (TransactionUtility tran = new TransactionUtility())
                {
                    r_framework.Dao.IT_OPERATE_LOGDao daoentry = DaoInitUtilityLOG.GetComponent<r_framework.Dao.IT_OPERATE_LOGDao>();
                    var incheck = daoentry.CheckTableConnect();
                    tran.Commit();
                }
                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("checkTableLog", ex1);
                this.form.errmessage.MessageBoxShow("E350", "");
                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("checkTableLog", ex1);
                this.form.errmessage.MessageBoxShow("E350", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkTableLog", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
        internal void ElementDecision()
        {

            this.form.value1 = this.form.MOD_SOUSHIN_HYOU_BIKOU1.Text;
            this.form.value2 = this.form.MOD_SOUSHIN_HYOU_BIKOU2.Text;
            this.form.value3 = this.form.MOD_SOUSHIN_HYOU_BIKOU3.Text;
            this.form.value4 = this.form.MOD_SOUSHIN_HYOU_BIKOU4.Text;
            this.form.value5 = this.form.MOD_SOUSHIN_HYOU_BIKOU5.Text;
            this.form.value6 = this.form.MOD_SOUSHIN_HYOU_BIKOU6.Text;
            this.form.value7 = this.form.MOD_SOUSHIN_HYOU_BIKOU7.Text;
            this.form.value8 = this.form.MOD_SOUSHIN_HYOU_BIKOU8.Text;
            this.form.value9 = this.form.MOD_SOUSHIN_HYOU_BIKOU9.Text;
            this.form.value10 = this.form.MOD_SOUSHIN_HYOU_BIKOU10.Text;
            this.form.value11 = this.form.MOD_SOUSHIN_HYOU_BIKOU11.Text;
            this.form.value12 = this.form.MOD_SOUSHIN_HYOU_BIKOU12.Text;
            this.form.value13 = this.form.MOD_SOUSHIN_HYOU_BIKOU13.Text;
            this.form.value14 = this.form.MOD_SOUSHIN_HYOU_BIKOU14.Text;
            this.form.value15 = this.form.MOD_SOUSHIN_HYOU_BIKOU15.Text;
            this.form.value16 = this.form.MOD_SOUSHIN_HYOU_BIKOU16.Text;
            this.form.value17 = this.form.MOD_SOUSHIN_HYOU_BIKOU17.Text;
            this.form.value18 = this.form.MOD_SOUSHIN_HYOU_BIKOU18.Text;
            this.form.value19 = this.form.MOD_SOUSHIN_HYOU_BIKOU19.Text;
            this.form.value20 = this.form.MOD_SOUSHIN_HYOU_BIKOU20.Text;
            this.form.value21 = this.form.MOD_SOUSHIN_HYOU_BIKOU21.Text;
            this.form.value22 = this.form.MOD_SOUSHIN_HYOU_BIKOU22.Text;
            this.form.value23 = this.form.MOD_SOUSHIN_HYOU_BIKOU23.Text;
            this.form.value24 = this.form.MOD_SOUSHIN_HYOU_BIKOU24.Text;
            this.form.value25 = this.form.MOD_SOUSHIN_HYOU_BIKOU25.Text;
            this.form.Close();
        }
    }

}