namespace ClientChatOFG
{
    partial class Log
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
            logTextBox = new RichTextBox();
            BackButton = new Button();
            CopyButton = new Button();
            ClearButton = new Button();
            SuspendLayout();
            // 
            // logTextBox
            // 
            logTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logTextBox.BackColor = SystemColors.ControlLightLight;
            logTextBox.Location = new Point(14, 14);
            logTextBox.Margin = new Padding(4, 3, 4, 3);
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.Size = new Size(536, 412);
            logTextBox.TabIndex = 0;
            logTextBox.Text = "";
            logTextBox.VisibleChanged += logTextBox_VisibleChanged;
            // 
            // BackButton
            // 
            BackButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BackButton.Location = new Point(463, 434);
            BackButton.Margin = new Padding(4, 3, 4, 3);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(88, 27);
            BackButton.TabIndex = 1;
            BackButton.Text = "Назад";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += backButton_Click;
            // 
            // CopyButton
            // 
            CopyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CopyButton.Location = new Point(369, 434);
            CopyButton.Margin = new Padding(4, 3, 4, 3);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(88, 27);
            CopyButton.TabIndex = 2;
            CopyButton.Text = "Копировать";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += copyButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ClearButton.Location = new Point(14, 434);
            ClearButton.Margin = new Padding(4, 3, 4, 3);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(88, 27);
            ClearButton.TabIndex = 3;
            ClearButton.Text = "Очистить";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += clearButton_Click;
            // 
            // Log
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 474);
            Controls.Add(ClearButton);
            Controls.Add(CopyButton);
            Controls.Add(BackButton);
            Controls.Add(logTextBox);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Log";
            Text = "Журнал";
            FormClosing += log_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        public RichTextBox logTextBox;
        private Button BackButton;
        private Button CopyButton;
        private Button ClearButton;
    }
}