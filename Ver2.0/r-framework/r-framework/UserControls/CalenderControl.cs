﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.UserControls
{
    /// <summary>
    /// ６か月分のカレンダー情報を表示するユーザコントロール
    /// </summary>
    public partial class CalendarControl : UserControl
    {
        /// <summary>
        /// 開始日フィールド
        /// </summary>
        private DateTime startDateTime { get; set; }

        /// <summary>
        /// チェックを行う日付の格納フィールド
        /// </summary>
        private List<string> CheckDate { get; set; }
        /// <summary>
        /// コントロールユーティリティ
        /// </summary>
        private ControlUtility controlUtil;
        /// <summary>
        /// チェッククラス
        /// </summary>
        private Validator validator;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CalendarControl()
        {
            InitializeComponent();

            CheckDate = new List<string>();

            controlUtil = new ControlUtility();
            validator = new Validator();

            this.DateReset();
        }
        /// <summary>
        /// 日付の初期化を行う
        /// </summary>
        public void DateReset()
        {

            //当日日付にて初期化を行っているが
            //マスタの期首日から生成を行うように変更する
            startDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);

            startDateTime = startDateTime.AddMonths(-1);
        }

        /// <summary>
        /// カレンダー情報を動的に生成する
        /// </summary>
        public void viewCalendar()
        {
            this.CreateButtonText();

            for (int i = 1; i <= 6; i++)
            {
                DateTime calendarTime = startDateTime.AddMonths(i);

                Panel calendarPanel = (Panel)controlUtil.FindControl(this, "calendarPanel" + i);

                this.createLabel(i, calendarPanel);
                Label calendarTitle = (Label)controlUtil.FindControl(this, "calendarTitle" + i);

                calendarTitle.Text = calendarTime.Year + "/" + calendarTime.Month;

                this.createCalender(calendarTime.Year, calendarTime.Month, calendarTime.Day, calendarPanel);
            }
        }

        /// <summary>
        /// 曜日ラベルの生成を行う
        /// </summary>
        public void createLabel(int calendarNo, Panel calendarPanel)
        {

            calendarPanel.Controls.Clear();

            Label SundayLabel = new Label();
            Label MondayLabel = new Label();
            Label TuesdayLabel = new Label();
            Label WednesdayLabel = new Label();
            Label ThursdayLabel = new Label();
            Label FridayLabel = new Label();
            Label SaturdayLabel = new Label();
            // 
            // SaturdayLabel
            // 
            SaturdayLabel.BackColor = System.Drawing.Color.Blue;
            SaturdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            SaturdayLabel.Location = new System.Drawing.Point(233, 26);
            SaturdayLabel.Name = "SaturdayLabel" + calendarNo;
            SaturdayLabel.Size = new System.Drawing.Size(36, 22);
            SaturdayLabel.TabIndex = 8;
            SaturdayLabel.Text = "土";
            SaturdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(SaturdayLabel);
            // 
            // FridayLabel
            // 
            FridayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            FridayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            FridayLabel.Location = new System.Drawing.Point(195, 26);
            FridayLabel.Name = "FridayLabel" + calendarNo;
            FridayLabel.Size = new System.Drawing.Size(36, 22);
            FridayLabel.TabIndex = 6;
            FridayLabel.Text = "金";
            FridayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(FridayLabel);
            // 
            // ThursdayLabel
            // 
            ThursdayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            ThursdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            ThursdayLabel.Location = new System.Drawing.Point(157, 26);
            ThursdayLabel.Name = "ThursdayLabel" + calendarNo;
            ThursdayLabel.Size = new System.Drawing.Size(36, 22);
            ThursdayLabel.TabIndex = 5;
            ThursdayLabel.Text = "木";
            ThursdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(ThursdayLabel);
            // 
            // WednesdayLabel
            // 
            WednesdayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            WednesdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            WednesdayLabel.Location = new System.Drawing.Point(119, 26);
            WednesdayLabel.Name = "WednesdayLabel" + calendarNo;
            WednesdayLabel.Size = new System.Drawing.Size(36, 22);
            WednesdayLabel.TabIndex = 4;
            WednesdayLabel.Text = "水";
            WednesdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(WednesdayLabel);
            // 
            // TuesdayLabel
            // 
            TuesdayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            TuesdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            TuesdayLabel.Location = new System.Drawing.Point(81, 26);
            TuesdayLabel.Name = "TuesdayLabel" + calendarNo;
            TuesdayLabel.Size = new System.Drawing.Size(36, 22);
            TuesdayLabel.TabIndex = 3;
            TuesdayLabel.Text = "火";
            TuesdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(TuesdayLabel);
            // 
            // MondayLabel
            // 
            MondayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            MondayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            MondayLabel.Location = new System.Drawing.Point(43, 26);
            MondayLabel.Name = "MondayLabel" + calendarNo;
            MondayLabel.Size = new System.Drawing.Size(36, 22);
            MondayLabel.TabIndex = 2;
            MondayLabel.Text = "月";
            MondayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(MondayLabel);
            // 
            // SundayLabel
            // 
            SundayLabel.BackColor = System.Drawing.Color.Red;
            SundayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            SundayLabel.Location = new System.Drawing.Point(5, 26);
            SundayLabel.Name = "SundayLabel" + calendarNo;
            SundayLabel.Size = new System.Drawing.Size(36, 22);
            SundayLabel.TabIndex = 1;
            SundayLabel.Text = "日";
            SundayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(SundayLabel);

            Label calendarTitle = new Label();
            // 
            // calendarTitle2
            // 
            calendarTitle.AutoSize = true;
            calendarTitle.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            calendarTitle.ForeColor = System.Drawing.Color.White;
            calendarTitle.Location = new System.Drawing.Point(104, 6);
            calendarTitle.Name = "calendarTitle" + calendarNo;
            calendarTitle.Size = new System.Drawing.Size(64, 16);
            calendarTitle.TabIndex = 0;
            calendarPanel.Controls.Add(calendarTitle);
        }

        /// <summary>
        /// カレンダーに表示する日付チェックボックスの生成を行う
        /// </summary>
        public void createCalender(int year, int Month, int day, Panel addPanel)
        {

            DateTime thisDate = new DateTime(year, Month, day);

            var location = this.CreateNewLocation(year, Month, day);

            int xline = Convert.ToInt32(thisDate.DayOfWeek);

            int yline = 1;

            var day1 = 1;

            while (true)
            {
                location.Y = location.Y + 24;

                while ((xline + 1) % 8 != 0)
                {
                    if (!validator.IsDate(year, Month, day1))
                    {
                        return;
                    }
                    var DayCheckBox = new System.Windows.Forms.CheckBox();

                    var thisDays = new DateTime(year, Month, day1);

                    var holidayInfo = HolidayCheckLogic.Holiday(thisDays);

                    if (holidayInfo.holiday != HolidayCheckLogic.HolidayInfo.HOLIDAY.WEEKDAY)
                    {
                        DayCheckBox.ForeColor = System.Drawing.Color.Red;
                    }
                    DayCheckBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
                    DayCheckBox.Location = location;
                    DayCheckBox.Name = year + "/" + Month.ToString("00") + "/" + day1.ToString("00");
                    DayCheckBox.Size = new System.Drawing.Size(36, 22);
                    DayCheckBox.TabIndex = day1;
                    DayCheckBox.Text = Convert.ToString(day1);
                    DayCheckBox.UseVisualStyleBackColor = false;
                    DayCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    DayCheckBox.PreviewKeyDown += new PreviewKeyDownEventHandler(DayCheckBox_PreviewKeyDown);
                    DayCheckBox.Enter += new EventHandler(DayCheckBox_Enter);
                    DayCheckBox.Leave += new EventHandler(DayCheckBox_Leave);

                    DayCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
                    addPanel.Controls.Add(DayCheckBox);

                    location.X = location.X + 38;
                    xline++;
                    day1++;
                }
                location.X = 5;


                xline++;
                yline++;
            }

        }

        /// <summary>
        /// 日付チェックボックスの開始ロケーションを設定する
        /// </summary>
        public Point CreateNewLocation(int iYear, int iMonth, int iDay)
        {
            DateTime thisDate = new DateTime(iYear, iMonth, iDay);

            var week = thisDate.DayOfWeek;
            if (DayOfWeek.Sunday == week)
            {
                return new Point(5, 26);
            }
            else if (DayOfWeek.Monday == week)
            {
                return new Point(43, 26);
            }
            else if (DayOfWeek.Tuesday == week)
            {
                return new Point(81, 26);
            }
            else if (DayOfWeek.Wednesday == week)
            {
                return new Point(119, 26);
            }
            else if (DayOfWeek.Thursday == week)
            {
                return new Point(157, 26);
            }
            else if (DayOfWeek.Friday == week)
            {
                return new Point(195, 26);
            }
            else if (DayOfWeek.Saturday == week)
            {
                return new Point(233, 26);
            }
            throw new Exception();

        }

        /// <summary>
        /// 次の六ヶ月表示用ボタン
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            startDateTime = startDateTime.AddMonths(6);

            this.viewCalendar();
        }
        /// <summary>
        /// 前の六ヶ月表示用ボタン
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            startDateTime = startDateTime.AddMonths(-6);

            this.viewCalendar();
        }

        /// <summary>
        /// 初期表示に戻るボタン（未使用）
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            this.DateReset();
            this.viewCalendar();
        }

        /// <summary>
        /// コントロールの初期表示設定
        /// </summary>
        private void CalendarControl_Load_1(object sender, EventArgs e)
        {
            this.DateReset();
            this.viewCalendar();
        }
        /// <summary>
        /// 前後ボタンの表示テキストの設定を行う
        /// </summary>
        private void CreateButtonText()
        {

            var beforeTextStart = startDateTime.AddMonths(-5);
            var beforeTextEnd = startDateTime;

            this.beforeButton.Text = beforeTextStart.Year + "/" + beforeTextStart.Month + " ～ " + beforeTextEnd.Year + "/" + beforeTextEnd.Month;

            var afterTextStart = startDateTime.AddMonths(7);
            var afterTextEnd = startDateTime.AddMonths(12);

            this.afterButton.Text = afterTextStart.Year + "/" + afterTextStart.Month + " ～ " + afterTextEnd.Year + "/" + afterTextEnd.Month;
        }

        /// <summary>
        /// フォーカス入時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayCheckBox_Enter(object sender, EventArgs e)
        {
            CheckBox activeControl = (CheckBox)sender;
            activeControl.BackColor = System.Drawing.SystemColors.Highlight;
        }

        /// <summary>
        /// フォーカス離時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayCheckBox_Leave(object sender, EventArgs e)
        {
            CheckBox activeControl = (CheckBox)sender;
            activeControl.BackColor = System.Drawing.SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayCheckBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            CheckBox activeControl = (CheckBox)sender;
            DateTime activeDate = DateTime.ParseExact(activeControl.Name, "yyyy/MM/dd", null);
            if (activeDate == null)
            {
                throw new Exception();
            }

            DateTime targetDate = activeDate;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    targetDate = activeDate.AddDays(-7);
                    break;
                case Keys.Down:
                    targetDate = activeDate.AddDays(7);
                    break;
                case Keys.Left:
                    targetDate = activeDate.AddDays(-1);
                    break;
                case Keys.Right:
                    targetDate = activeDate.AddDays(1);
                    break;
                default:
                    return;
            }

            // 特別なケースの処理
            if (IsPageTopmost(activeDate) && e.KeyCode == Keys.Up)
            {
                // 画面上で最上
                targetDate = activeDate.AddMonths(3);
                targetDate = GetBottomDay(targetDate.Year, targetDate.Month, activeDate.DayOfWeek);
            }
            else if (IsPageBottommost(activeDate) && e.KeyCode == Keys.Down)
            {
                // 画面上で最下
                targetDate = activeDate.AddMonths(-3);
                targetDate = GetTopDay(targetDate.Year, targetDate.Month, activeDate.DayOfWeek);
            }
            else if (IsPageRightmost(activeDate) && e.KeyCode == Keys.Right)
            {
                // 画面上で最右
                targetDate = activeDate.AddMonths(4);
                targetDate = GetLeftDay(targetDate.Year, targetDate.Month, NthWeek(activeDate));
            }
            else if (IsPageLeftmost(activeDate) && e.KeyCode == Keys.Left)
            {
                // 画面上で最左
                targetDate = activeDate.AddMonths(-4);
                targetDate = GetRightDay(targetDate.Year, targetDate.Month, NthWeek(activeDate));
            }
            else if (IsTopmost(activeDate) && e.KeyCode == Keys.Up)
            {
                // カレンダー上で最上
                targetDate = activeDate.AddMonths(-3);
                targetDate = GetBottomDay(targetDate.Year, targetDate.Month, activeDate.DayOfWeek);
            }
            else if (IsBottommost(activeDate) && e.KeyCode == Keys.Down)
            {
                // カレンダー上で最下
                targetDate = activeDate.AddMonths(3);
                targetDate = GetTopDay(targetDate.Year, targetDate.Month, activeDate.DayOfWeek);
            }
            else if (IsRightmost(activeDate) && e.KeyCode == Keys.Right)
            {
                // カレンダー上で最右
                targetDate = activeDate.AddMonths(1);
                targetDate = GetLeftDay(targetDate.Year, targetDate.Month, NthWeek(activeDate));
            }
            else if (IsLeftmost(activeDate) && e.KeyCode == Keys.Left)
            {
                // カレンダー上で最左
                targetDate = activeDate.AddMonths(-1);
                targetDate = GetRightDay(targetDate.Year, targetDate.Month, NthWeek(activeDate));
            }

            string targetName = targetDate.ToString("yyyy/MM/dd", null);
            Control[] targets = this.Controls.Find(targetName, true);
            if (targets.Length == 0)
            {
                if (activeControl.Name.CompareTo(targetName) < 0)
                {
                    // 次6ヵ月へ
                    button2_Click(null, new EventArgs());
                }
                else
                {
                    // 前6ヵ月へ
                    button1_Click(null, new EventArgs());
                }
            }

            foreach (Control target in this.Controls.Find(targetName, true))
            {
                target.Focus();
            }

            e.IsInputKey = true;
        }

        /// <summary>
        /// カレンダー上で最上、かつ指定曜日の日付を取得
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        private static DateTime GetTopDay(int year, int month, DayOfWeek week)
        {
            DateTime checkDate = new DateTime(year, month, 1);
            for (int i = 0; i < 7; i++)
            {
                if (checkDate.DayOfWeek.Equals(week))
                {
                    return checkDate;
                }
                checkDate = checkDate.AddDays(1);
            }

            throw new Exception();
        }

        /// <summary>
        /// カレンダー上で最下、かつ指定曜日の日付を取得
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        private static DateTime GetBottomDay(int year, int month, DayOfWeek week)
        {
            DateTime checkDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            for (int i = 0; i < 7; i++)
            {
                if (checkDate.DayOfWeek.Equals(week))
                {
                    return checkDate;
                }
                checkDate = checkDate.AddDays(-1);
            }

            throw new Exception();
        }

        /// <summary>
        /// カレンダー上で最左、かつ指定週の日付を取得
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="nthWeek"></param>
        /// <returns></returns>
        private static DateTime GetLeftDay(int year, int month, int nthWeek)
        {
            int startDay = (nthWeek - 1) * 7 - 5;
            int maxDay = DateTime.DaysInMonth(year, month);
            if (startDay <= 0)
            {
                startDay = 1;
            }
            else if (maxDay < startDay)
            {
                startDay = maxDay;
            }

            DateTime checkDate = new DateTime(year, month, startDay);

            for (int i = 0; i < 7; i++)
            {
                if (NthWeek(checkDate) == nthWeek || checkDate.Day == maxDay)
                {
                    return checkDate;
                }
                checkDate = checkDate.AddDays(1);
            }

            throw new Exception();
        }

        /// <summary>
        /// カレンダー上で最右、かつ指定週の日付を取得
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="nthWeek"></param>
        /// <returns></returns>
        private static DateTime GetRightDay(int year, int month, int nthWeek)
        {
            int startDay = nthWeek * 7;
            int maxDay = DateTime.DaysInMonth(year, month);
            if (maxDay < startDay)
            {
                startDay = maxDay;
            }

            DateTime checkDate = new DateTime(year, month, startDay);

            for (int i = 0; i < 7; i++)
            {
                if (NthWeek(checkDate) <= nthWeek || checkDate.Day == 1)
                {
                    return checkDate;
                }
                checkDate = checkDate.AddDays(-1);
            }

            throw new Exception();
        }

        /// <summary>
        /// カレンダー上で最上かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static bool IsTopmost(DateTime date)
        {
            return (date.Month != date.AddDays(-7).Month);
        }

        /// <summary>
        /// カレンダー上で最下かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static bool IsBottommost(DateTime date)
        {
            return (date.Month != date.AddDays(7).Month);
        }

        /// <summary>
        /// カレンダー上で最左かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static bool IsLeftmost(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Sunday || date.Day == 1);
        }

        /// <summary>
        /// カレンダー上で最右かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static bool IsRightmost(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday || date.Day == DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// 画面上で最上かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsPageTopmost(DateTime date)
        {
            return ((date.Month == this.startDateTime.AddMonths(1).Month ||
                     date.Month == this.startDateTime.AddMonths(2).Month ||
                     date.Month == this.startDateTime.AddMonths(3).Month) &&
                date.Month != date.AddDays(-7).Month);
        }

        /// <summary>
        /// 画面上で最下かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsPageBottommost(DateTime date)
        {
            return ((date.Month == this.startDateTime.AddMonths(4).Month ||
                     date.Month == this.startDateTime.AddMonths(5).Month ||
                     date.Month == this.startDateTime.AddMonths(6).Month) &&
                date.Month != date.AddDays(7).Month);
        }

        /// <summary>
        /// 画面上で最左かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsPageLeftmost(DateTime date)
        {
            return ((date.Month == this.startDateTime.AddMonths(1).Month || date.Month == this.startDateTime.AddMonths(4).Month) &&
                IsLeftmost(date));
        }

        /// <summary>
        /// 画面上で最右かどうか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsPageRightmost(DateTime date)
        {
            return ((date.Month == this.startDateTime.AddMonths(3).Month || date.Month == this.startDateTime.AddMonths(6).Month) &&
                IsRightmost(date));
        }

        /// <summary>
        /// 第何週目かを取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static int NthWeek(DateTime date)
        {
            int w = (int)(new DateTime(date.Year, date.Month, 1).DayOfWeek);
            return (date.Day + w - 1) / 7 + 1;
        }
    }
}
