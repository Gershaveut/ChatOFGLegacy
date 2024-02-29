﻿namespace ClientChatOFG
{
    partial class CrashReport
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
            ExitButton = new Button();
            CopyButton = new Button();
            crashReportTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ExitButton.Location = new Point(885, 466);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(88, 23);
            ExitButton.TabIndex = 1;
            ExitButton.Text = "Выход";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // CopyButton
            // 
            CopyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CopyButton.Location = new Point(791, 466);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(88, 23);
            CopyButton.TabIndex = 2;
            CopyButton.Text = "Копировать";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += CopyButton_Click;
            // 
            // crashReportTextBox
            // 
            crashReportTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            crashReportTextBox.BackColor = SystemColors.ControlLightLight;
            crashReportTextBox.Location = new Point(12, 12);
            crashReportTextBox.Name = "crashReportTextBox";
            crashReportTextBox.ReadOnly = true;
            crashReportTextBox.Size = new Size(961, 448);
            crashReportTextBox.TabIndex = 3;
            crashReportTextBox.Text = "";
            // 
            // CrashReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 501);
            Controls.Add(crashReportTextBox);
            Controls.Add(CopyButton);
            Controls.Add(ExitButton);
            Name = "CrashReport";
            Text = "Критическая ошибка";
            ResumeLayout(false);
        }

        #endregion
        private Button ExitButton;
        private Button CopyButton;
        private RichTextBox crashReportTextBox;
    }
}