namespace DotnetPatching {
   partial class Custom {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.lnkToMyHome = new System.Windows.Forms.LinkLabel();
         this.cmdStart = new System.Windows.Forms.Button();
         this.cmdOpenDefaultConfig = new System.Windows.Forms.Button();
         this.cmdOpenOmake = new System.Windows.Forms.Button();
         this.cmdAbout = new System.Windows.Forms.Button();
         this.chkAlterText = new System.Windows.Forms.CheckBox();
         this.chkTachieOnTop = new System.Windows.Forms.CheckBox();
         this.chkUseSkip = new System.Windows.Forms.CheckBox();
         this.chkUseBorder = new System.Windows.Forms.CheckBox();
         this.chkCustomFont = new System.Windows.Forms.CheckBox();
         this.chkAlterFPSFont = new System.Windows.Forms.CheckBox();
         this.chkAlterSpellNameFont = new System.Windows.Forms.CheckBox();
         this.chkAlterSpellStatisticsFont = new System.Windows.Forms.CheckBox();
         this.chkAlterRestFont = new System.Windows.Forms.CheckBox();
         this.lblFPSFont = new System.Windows.Forms.Label();
         this.lblSpellFont = new System.Windows.Forms.Label();
         this.lblSpellStatisticsFont = new System.Windows.Forms.Label();
         this.lblRestFont = new System.Windows.Forms.Label();
         this.grpAlterText = new System.Windows.Forms.GroupBox();
         this.grpAlterFont = new System.Windows.Forms.GroupBox();
         this.lblAntagonistLineColor = new System.Windows.Forms.Label();
         this.lblProtagonistLineColor = new System.Windows.Forms.Label();
         this.chkAntagonistLineColor = new System.Windows.Forms.CheckBox();
         this.chkProtagonistLineColor = new System.Windows.Forms.CheckBox();
         this.colorDlg = new System.Windows.Forms.ColorDialog();
         this.cmdGlossary = new System.Windows.Forms.Button();
         this.grpAlterText.SuspendLayout();
         this.grpAlterFont.SuspendLayout();
         this.SuspendLayout();
         // 
         // lnkToMyHome
         // 
         this.lnkToMyHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.lnkToMyHome.LinkArea = new System.Windows.Forms.LinkArea(28, 13);
         this.lnkToMyHome.Location = new System.Drawing.Point(420, 460);
         this.lnkToMyHome.Name = "lnkToMyHome";
         this.lnkToMyHome.Size = new System.Drawing.Size(284, 21);
         this.lnkToMyHome.TabIndex = 11;
         this.lnkToMyHome.TabStop = true;
         this.lnkToMyHome.Text = "Phiên dịch và lập trình bởi Meigyoku Thmn";
         this.lnkToMyHome.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         this.lnkToMyHome.UseCompatibleTextRendering = true;
         this.lnkToMyHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkToMyHome_LinkClicked);
         // 
         // cmdStart
         // 
         this.cmdStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdStart.Location = new System.Drawing.Point(614, 427);
         this.cmdStart.Name = "cmdStart";
         this.cmdStart.Size = new System.Drawing.Size(90, 30);
         this.cmdStart.TabIndex = 10;
         this.cmdStart.Text = "Bắt đầu";
         this.cmdStart.UseVisualStyleBackColor = true;
         this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
         // 
         // cmdOpenDefaultConfig
         // 
         this.cmdOpenDefaultConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.cmdOpenDefaultConfig.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdOpenDefaultConfig.Location = new System.Drawing.Point(435, 427);
         this.cmdOpenDefaultConfig.Name = "cmdOpenDefaultConfig";
         this.cmdOpenDefaultConfig.Size = new System.Drawing.Size(173, 30);
         this.cmdOpenDefaultConfig.TabIndex = 9;
         this.cmdOpenDefaultConfig.Text = "Mở Config mặc định";
         this.cmdOpenDefaultConfig.UseVisualStyleBackColor = true;
         this.cmdOpenDefaultConfig.Click += new System.EventHandler(this.cmdOpenDefaultConfig_Click);
         // 
         // cmdOpenOmake
         // 
         this.cmdOpenOmake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.cmdOpenOmake.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdOpenOmake.Location = new System.Drawing.Point(12, 351);
         this.cmdOpenOmake.Name = "cmdOpenOmake";
         this.cmdOpenOmake.Size = new System.Drawing.Size(146, 30);
         this.cmdOpenOmake.TabIndex = 5;
         this.cmdOpenOmake.Text = "Phiên dịch Omake";
         this.cmdOpenOmake.UseVisualStyleBackColor = true;
         this.cmdOpenOmake.Click += new System.EventHandler(this.cmdOpenOmake_Click);
         // 
         // cmdAbout
         // 
         this.cmdAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.cmdAbout.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdAbout.Location = new System.Drawing.Point(12, 427);
         this.cmdAbout.Name = "cmdAbout";
         this.cmdAbout.Size = new System.Drawing.Size(172, 30);
         this.cmdAbout.TabIndex = 8;
         this.cmdAbout.Text = "Về tác giả và bản dịch";
         this.cmdAbout.UseVisualStyleBackColor = true;
         this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
         // 
         // chkAlterText
         // 
         this.chkAlterText.AutoSize = true;
         this.chkAlterText.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkAlterText.Location = new System.Drawing.Point(15, 12);
         this.chkAlterText.Name = "chkAlterText";
         this.chkAlterText.Size = new System.Drawing.Size(186, 24);
         this.chkAlterText.TabIndex = 0;
         this.chkAlterText.Text = "Thay đổi kiểu vẽ văn bản";
         this.chkAlterText.UseVisualStyleBackColor = true;
         // 
         // chkTachieOnTop
         // 
         this.chkTachieOnTop.AutoSize = true;
         this.chkTachieOnTop.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkTachieOnTop.Location = new System.Drawing.Point(15, 288);
         this.chkTachieOnTop.Name = "chkTachieOnTop";
         this.chkTachieOnTop.Size = new System.Drawing.Size(368, 24);
         this.chkTachieOnTop.TabIndex = 2;
         this.chkTachieOnTop.Text = "Đưa tachie nhân vật đè lên trên khung giao diện game";
         this.chkTachieOnTop.UseVisualStyleBackColor = true;
         // 
         // chkUseSkip
         // 
         this.chkUseSkip.AutoSize = true;
         this.chkUseSkip.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkUseSkip.Location = new System.Drawing.Point(15, 318);
         this.chkUseSkip.Name = "chkUseSkip";
         this.chkUseSkip.Size = new System.Drawing.Size(490, 24);
         this.chkUseSkip.TabIndex = 3;
         this.chkUseSkip.Text = "Sử dụng patch skipgame (Không còn kẻ địch, non-spell 2 giây, spell 4 giây)";
         this.chkUseSkip.UseVisualStyleBackColor = true;
         // 
         // chkUseBorder
         // 
         this.chkUseBorder.AutoSize = true;
         this.chkUseBorder.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkUseBorder.Location = new System.Drawing.Point(15, 24);
         this.chkUseBorder.Name = "chkUseBorder";
         this.chkUseBorder.Size = new System.Drawing.Size(304, 24);
         this.chkUseBorder.TabIndex = 0;
         this.chkUseBorder.Text = "Dùng viền đen thay vì bóng đổ cho văn bản";
         this.chkUseBorder.UseVisualStyleBackColor = true;
         // 
         // chkCustomFont
         // 
         this.chkCustomFont.AutoSize = true;
         this.chkCustomFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkCustomFont.Location = new System.Drawing.Point(15, 52);
         this.chkCustomFont.Name = "chkCustomFont";
         this.chkCustomFont.Size = new System.Drawing.Size(186, 24);
         this.chkCustomFont.TabIndex = 1;
         this.chkCustomFont.Text = "Đổi font và màu văn bản";
         this.chkCustomFont.UseVisualStyleBackColor = true;
         // 
         // chkAlterFPSFont
         // 
         this.chkAlterFPSFont.AutoSize = true;
         this.chkAlterFPSFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkAlterFPSFont.Location = new System.Drawing.Point(14, 24);
         this.chkAlterFPSFont.Name = "chkAlterFPSFont";
         this.chkAlterFPSFont.Size = new System.Drawing.Size(100, 24);
         this.chkAlterFPSFont.TabIndex = 0;
         this.chkAlterFPSFont.Text = "Chỉ thị FPS";
         this.chkAlterFPSFont.UseVisualStyleBackColor = true;
         // 
         // chkAlterSpellNameFont
         // 
         this.chkAlterSpellNameFont.AutoSize = true;
         this.chkAlterSpellNameFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkAlterSpellNameFont.Location = new System.Drawing.Point(14, 54);
         this.chkAlterSpellNameFont.Name = "chkAlterSpellNameFont";
         this.chkAlterSpellNameFont.Size = new System.Drawing.Size(139, 24);
         this.chkAlterSpellNameFont.TabIndex = 2;
         this.chkAlterSpellNameFont.Text = "Tên của Spellcard";
         this.chkAlterSpellNameFont.UseVisualStyleBackColor = true;
         // 
         // chkAlterSpellStatisticsFont
         // 
         this.chkAlterSpellStatisticsFont.AutoSize = true;
         this.chkAlterSpellStatisticsFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkAlterSpellStatisticsFont.Location = new System.Drawing.Point(14, 86);
         this.chkAlterSpellStatisticsFont.Name = "chkAlterSpellStatisticsFont";
         this.chkAlterSpellStatisticsFont.Size = new System.Drawing.Size(259, 24);
         this.chkAlterSpellStatisticsFont.TabIndex = 4;
         this.chkAlterSpellStatisticsFont.Text = "Thống kê nhỏ bên dưới tên Spellcard";
         this.chkAlterSpellStatisticsFont.UseVisualStyleBackColor = true;
         // 
         // chkAlterRestFont
         // 
         this.chkAlterRestFont.AutoSize = true;
         this.chkAlterRestFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkAlterRestFont.Location = new System.Drawing.Point(14, 116);
         this.chkAlterRestFont.Name = "chkAlterRestFont";
         this.chkAlterRestFont.Size = new System.Drawing.Size(144, 24);
         this.chkAlterRestFont.TabIndex = 6;
         this.chkAlterRestFont.Text = "Những thứ còn lại";
         this.chkAlterRestFont.UseVisualStyleBackColor = true;
         // 
         // lblFPSFont
         // 
         this.lblFPSFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblFPSFont.AutoEllipsis = true;
         this.lblFPSFont.Cursor = System.Windows.Forms.Cursors.Hand;
         this.lblFPSFont.Location = new System.Drawing.Point(309, 26);
         this.lblFPSFont.Name = "lblFPSFont";
         this.lblFPSFont.Size = new System.Drawing.Size(371, 24);
         this.lblFPSFont.TabIndex = 1;
         this.lblFPSFont.Tag = "fps";
         this.lblFPSFont.Text = "Arial";
         this.lblFPSFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblFPSFont.Click += new System.EventHandler(this.envChangeFont);
         this.lblFPSFont.MouseEnter += new System.EventHandler(this.FontNameLabel_MouseEnter);
         this.lblFPSFont.MouseLeave += new System.EventHandler(this.FontNameLabel_MouseLeave);
         // 
         // lblSpellFont
         // 
         this.lblSpellFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblSpellFont.AutoEllipsis = true;
         this.lblSpellFont.Cursor = System.Windows.Forms.Cursors.Hand;
         this.lblSpellFont.Location = new System.Drawing.Point(309, 56);
         this.lblSpellFont.Name = "lblSpellFont";
         this.lblSpellFont.Size = new System.Drawing.Size(371, 24);
         this.lblSpellFont.TabIndex = 3;
         this.lblSpellFont.Tag = "spellcard";
         this.lblSpellFont.Text = "Arial";
         this.lblSpellFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblSpellFont.Click += new System.EventHandler(this.envChangeFont);
         this.lblSpellFont.MouseEnter += new System.EventHandler(this.FontNameLabel_MouseEnter);
         this.lblSpellFont.MouseLeave += new System.EventHandler(this.FontNameLabel_MouseLeave);
         // 
         // lblSpellStatisticsFont
         // 
         this.lblSpellStatisticsFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblSpellStatisticsFont.AutoEllipsis = true;
         this.lblSpellStatisticsFont.Cursor = System.Windows.Forms.Cursors.Hand;
         this.lblSpellStatisticsFont.Location = new System.Drawing.Point(309, 86);
         this.lblSpellStatisticsFont.Name = "lblSpellStatisticsFont";
         this.lblSpellStatisticsFont.Size = new System.Drawing.Size(371, 24);
         this.lblSpellStatisticsFont.TabIndex = 5;
         this.lblSpellStatisticsFont.Tag = "statistics";
         this.lblSpellStatisticsFont.Text = "Arial";
         this.lblSpellStatisticsFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblSpellStatisticsFont.Click += new System.EventHandler(this.envChangeFont);
         this.lblSpellStatisticsFont.MouseEnter += new System.EventHandler(this.FontNameLabel_MouseEnter);
         this.lblSpellStatisticsFont.MouseLeave += new System.EventHandler(this.FontNameLabel_MouseLeave);
         // 
         // lblRestFont
         // 
         this.lblRestFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblRestFont.AutoEllipsis = true;
         this.lblRestFont.Cursor = System.Windows.Forms.Cursors.Hand;
         this.lblRestFont.Location = new System.Drawing.Point(309, 116);
         this.lblRestFont.Name = "lblRestFont";
         this.lblRestFont.Size = new System.Drawing.Size(371, 24);
         this.lblRestFont.TabIndex = 7;
         this.lblRestFont.Tag = "rest";
         this.lblRestFont.Text = "Arial";
         this.lblRestFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblRestFont.Click += new System.EventHandler(this.envChangeFont);
         this.lblRestFont.MouseEnter += new System.EventHandler(this.FontNameLabel_MouseEnter);
         this.lblRestFont.MouseLeave += new System.EventHandler(this.FontNameLabel_MouseLeave);
         // 
         // grpAlterText
         // 
         this.grpAlterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.grpAlterText.Controls.Add(this.chkCustomFont);
         this.grpAlterText.Controls.Add(this.grpAlterFont);
         this.grpAlterText.Controls.Add(this.chkUseBorder);
         this.grpAlterText.Location = new System.Drawing.Point(6, 13);
         this.grpAlterText.Name = "grpAlterText";
         this.grpAlterText.Size = new System.Drawing.Size(698, 269);
         this.grpAlterText.TabIndex = 1;
         this.grpAlterText.TabStop = false;
         // 
         // grpAlterFont
         // 
         this.grpAlterFont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.grpAlterFont.Controls.Add(this.lblAntagonistLineColor);
         this.grpAlterFont.Controls.Add(this.lblProtagonistLineColor);
         this.grpAlterFont.Controls.Add(this.chkAntagonistLineColor);
         this.grpAlterFont.Controls.Add(this.chkProtagonistLineColor);
         this.grpAlterFont.Controls.Add(this.chkAlterFPSFont);
         this.grpAlterFont.Controls.Add(this.chkAlterSpellStatisticsFont);
         this.grpAlterFont.Controls.Add(this.chkAlterSpellNameFont);
         this.grpAlterFont.Controls.Add(this.chkAlterRestFont);
         this.grpAlterFont.Controls.Add(this.lblFPSFont);
         this.grpAlterFont.Controls.Add(this.lblRestFont);
         this.grpAlterFont.Controls.Add(this.lblSpellStatisticsFont);
         this.grpAlterFont.Controls.Add(this.lblSpellFont);
         this.grpAlterFont.Enabled = false;
         this.grpAlterFont.Location = new System.Drawing.Point(6, 54);
         this.grpAlterFont.Name = "grpAlterFont";
         this.grpAlterFont.Size = new System.Drawing.Size(686, 209);
         this.grpAlterFont.TabIndex = 2;
         this.grpAlterFont.TabStop = false;
         // 
         // lblAntagonistLineColor
         // 
         this.lblAntagonistLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblAntagonistLineColor.AutoEllipsis = true;
         this.lblAntagonistLineColor.BackColor = System.Drawing.SystemColors.Control;
         this.lblAntagonistLineColor.Cursor = System.Windows.Forms.Cursors.Hand;
         this.lblAntagonistLineColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblAntagonistLineColor.Location = new System.Drawing.Point(309, 176);
         this.lblAntagonistLineColor.Name = "lblAntagonistLineColor";
         this.lblAntagonistLineColor.Size = new System.Drawing.Size(371, 24);
         this.lblAntagonistLineColor.TabIndex = 11;
         this.lblAntagonistLineColor.Tag = "";
         this.lblAntagonistLineColor.Text = "Arial";
         this.lblAntagonistLineColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblAntagonistLineColor.Click += new System.EventHandler(this.lblAntagonistLineColor_Click);
         this.lblAntagonistLineColor.MouseEnter += new System.EventHandler(this.FontNameLabel_MouseEnter);
         this.lblAntagonistLineColor.MouseLeave += new System.EventHandler(this.FontNameLabel_MouseLeave);
         // 
         // lblProtagonistLineColor
         // 
         this.lblProtagonistLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblProtagonistLineColor.AutoEllipsis = true;
         this.lblProtagonistLineColor.Cursor = System.Windows.Forms.Cursors.Hand;
         this.lblProtagonistLineColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblProtagonistLineColor.Location = new System.Drawing.Point(309, 146);
         this.lblProtagonistLineColor.Name = "lblProtagonistLineColor";
         this.lblProtagonistLineColor.Size = new System.Drawing.Size(371, 24);
         this.lblProtagonistLineColor.TabIndex = 9;
         this.lblProtagonistLineColor.Tag = "";
         this.lblProtagonistLineColor.Text = "Arial";
         this.lblProtagonistLineColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblProtagonistLineColor.Click += new System.EventHandler(this.lblProtagonistLineColor_Click);
         this.lblProtagonistLineColor.MouseEnter += new System.EventHandler(this.FontNameLabel_MouseEnter);
         this.lblProtagonistLineColor.MouseLeave += new System.EventHandler(this.FontNameLabel_MouseLeave);
         // 
         // chkAntagonistLineColor
         // 
         this.chkAntagonistLineColor.AutoSize = true;
         this.chkAntagonistLineColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkAntagonistLineColor.Location = new System.Drawing.Point(14, 176);
         this.chkAntagonistLineColor.Name = "chkAntagonistLineColor";
         this.chkAntagonistLineColor.Size = new System.Drawing.Size(191, 24);
         this.chkAntagonistLineColor.TabIndex = 10;
         this.chkAntagonistLineColor.Text = "Màu thoại của antagonist";
         this.chkAntagonistLineColor.UseVisualStyleBackColor = true;
         // 
         // chkProtagonistLineColor
         // 
         this.chkProtagonistLineColor.AutoSize = true;
         this.chkProtagonistLineColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.chkProtagonistLineColor.Location = new System.Drawing.Point(14, 146);
         this.chkProtagonistLineColor.Name = "chkProtagonistLineColor";
         this.chkProtagonistLineColor.Size = new System.Drawing.Size(197, 24);
         this.chkProtagonistLineColor.TabIndex = 8;
         this.chkProtagonistLineColor.Text = "Màu thoại của protagonist";
         this.chkProtagonistLineColor.UseVisualStyleBackColor = true;
         // 
         // cmdGlossary
         // 
         this.cmdGlossary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.cmdGlossary.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cmdGlossary.Location = new System.Drawing.Point(164, 351);
         this.cmdGlossary.Name = "cmdGlossary";
         this.cmdGlossary.Size = new System.Drawing.Size(146, 30);
         this.cmdGlossary.TabIndex = 6;
         this.cmdGlossary.Text = "Chú giải Phiên dịch";
         this.cmdGlossary.UseVisualStyleBackColor = true;
         this.cmdGlossary.Click += new System.EventHandler(this.cmdGlossary_Click);
         // 
         // Custom
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(716, 490);
         this.Controls.Add(this.cmdGlossary);
         this.Controls.Add(this.chkAlterText);
         this.Controls.Add(this.grpAlterText);
         this.Controls.Add(this.chkUseSkip);
         this.Controls.Add(this.chkTachieOnTop);
         this.Controls.Add(this.cmdAbout);
         this.Controls.Add(this.cmdOpenOmake);
         this.Controls.Add(this.cmdOpenDefaultConfig);
         this.Controls.Add(this.cmdStart);
         this.Controls.Add(this.lnkToMyHome);
         this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.MinimumSize = new System.Drawing.Size(528, 528);
         this.Name = "Custom";
         this.Text = "Bảng tùy chỉnh - Đông Phương Mạc Hoa Tế";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Custom_FormClosing);
         this.grpAlterText.ResumeLayout(false);
         this.grpAlterText.PerformLayout();
         this.grpAlterFont.ResumeLayout(false);
         this.grpAlterFont.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.LinkLabel lnkToMyHome;
      private System.Windows.Forms.Button cmdStart;
      private System.Windows.Forms.Button cmdOpenDefaultConfig;
      private System.Windows.Forms.Button cmdOpenOmake;
      private System.Windows.Forms.Button cmdAbout;
      private System.Windows.Forms.CheckBox chkAlterText;
      private System.Windows.Forms.CheckBox chkTachieOnTop;
      private System.Windows.Forms.CheckBox chkUseSkip;
      private System.Windows.Forms.CheckBox chkUseBorder;
      private System.Windows.Forms.CheckBox chkCustomFont;
      private System.Windows.Forms.CheckBox chkAlterFPSFont;
      private System.Windows.Forms.CheckBox chkAlterSpellNameFont;
      private System.Windows.Forms.CheckBox chkAlterSpellStatisticsFont;
      private System.Windows.Forms.CheckBox chkAlterRestFont;
      private System.Windows.Forms.Label lblFPSFont;
      private System.Windows.Forms.Label lblSpellFont;
      private System.Windows.Forms.Label lblSpellStatisticsFont;
      private System.Windows.Forms.Label lblRestFont;
      private System.Windows.Forms.GroupBox grpAlterText;
      private System.Windows.Forms.GroupBox grpAlterFont;
      private System.Windows.Forms.CheckBox chkAntagonistLineColor;
      private System.Windows.Forms.CheckBox chkProtagonistLineColor;
      private System.Windows.Forms.Label lblAntagonistLineColor;
      private System.Windows.Forms.Label lblProtagonistLineColor;
      private System.Windows.Forms.ColorDialog colorDlg;
      private System.Windows.Forms.Button cmdGlossary;
   }
}