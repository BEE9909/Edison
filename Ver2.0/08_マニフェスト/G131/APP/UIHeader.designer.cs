﻿using r_framework.CustomControl;
namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup
{
    partial class UIHeader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 1);
            this.lb_title.Size = new System.Drawing.Size(486, 43);
            this.lb_title.Text = "条件指定ポップアップ";
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(800, 50);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIHeader";
            this.ResumeLayout(false);

        }

        #endregion

    }
}