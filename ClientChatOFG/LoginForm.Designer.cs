namespace ClientChatOFG
{
    partial class LoginForm
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
            IPLabel = new Label();
            IPTextBox = new TextBox();
            portLabel = new Label();
            portTextBox = new TextBox();
            loginButton = new Button();
            NameLabel = new Label();
            nameTextBox = new TextBox();
            SuspendLayout();
            // 
            // IPLabel
            // 
            IPLabel.AutoSize = true;
            IPLabel.Location = new Point(12, 15);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new Size(17, 15);
            IPLabel.TabIndex = 0;
            IPLabel.Text = "IP";
            // 
            // IPTextBox
            // 
            IPTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            IPTextBox.Location = new Point(56, 12);
            IPTextBox.Name = "IPTextBox";
            IPTextBox.Size = new Size(100, 23);
            IPTextBox.TabIndex = 1;
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Location = new Point(12, 44);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(35, 15);
            portLabel.TabIndex = 2;
            portLabel.Text = "Порт";
            // 
            // portTextBox
            // 
            portTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            portTextBox.Location = new Point(56, 41);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(100, 23);
            portTextBox.TabIndex = 3;
            // 
            // loginButton
            // 
            loginButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            loginButton.Location = new Point(12, 151);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(160, 23);
            loginButton.TabIndex = 4;
            loginButton.Text = "Подключиться";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(12, 102);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(31, 15);
            NameLabel.TabIndex = 5;
            NameLabel.Text = "Имя";
            // 
            // nameTextBox
            // 
            nameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameTextBox.Location = new Point(56, 99);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(100, 23);
            nameTextBox.TabIndex = 6;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(184, 186);
            Controls.Add(nameTextBox);
            Controls.Add(NameLabel);
            Controls.Add(loginButton);
            Controls.Add(portTextBox);
            Controls.Add(portLabel);
            Controls.Add(IPTextBox);
            Controls.Add(IPLabel);
            MinimumSize = new Size(200, 225);
            Name = "LoginForm";
            Text = "Вход";
            FormClosing += LoginForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label IPLabel;
        private TextBox IPTextBox;
        private Label portLabel;
        private TextBox portTextBox;
        private Button loginButton;
        private Label NameLabel;
        private TextBox nameTextBox;
    }
}