namespace ClientChatOFG
{
    partial class ChatOFGForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chatRichTextBox = new RichTextBox();
            messageTextBox = new TextBox();
            sendButton = new Button();
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            logToolStripMenuItem = new ToolStripMenuItem();
            reconnectToolStripMenuItem = new ToolStripMenuItem();
            usersListBox = new ListBox();
            label1 = new Label();
            debugToolStripMenuItem = new ToolStripMenuItem();
            sendCustomToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // chatRichTextBox
            // 
            chatRichTextBox.BackColor = SystemColors.Window;
            chatRichTextBox.Location = new Point(12, 27);
            chatRichTextBox.Name = "chatRichTextBox";
            chatRichTextBox.ReadOnly = true;
            chatRichTextBox.Size = new Size(695, 397);
            chatRichTextBox.TabIndex = 0;
            chatRichTextBox.Text = "Вход в чат выполнен";
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(12, 430);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(695, 23);
            messageTextBox.TabIndex = 1;
            messageTextBox.KeyDown += messageTextBox_KeyDown;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(713, 430);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 2;
            sendButton.Text = "Отправить";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Window;
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem, debugToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(799, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.BackColor = SystemColors.Window;
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { logToolStripMenuItem, reconnectToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(53, 20);
            menuToolStripMenuItem.Text = "Меню";
            // 
            // logToolStripMenuItem
            // 
            logToolStripMenuItem.Name = "logToolStripMenuItem";
            logToolStripMenuItem.Size = new Size(182, 22);
            logToolStripMenuItem.Text = "Журнал";
            logToolStripMenuItem.Click += logToolStripMenuItem_Click;
            // 
            // reconnectToolStripMenuItem
            // 
            reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
            reconnectToolStripMenuItem.Size = new Size(182, 22);
            reconnectToolStripMenuItem.Text = "Переподключиться";
            reconnectToolStripMenuItem.Click += reconnectToolStripMenuItem_Click;
            // 
            // usersListBox
            // 
            usersListBox.FormattingEnabled = true;
            usersListBox.ItemHeight = 15;
            usersListBox.Location = new Point(713, 45);
            usersListBox.Name = "usersListBox";
            usersListBox.Size = new Size(75, 379);
            usersListBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(713, 27);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 5;
            label1.Text = "Пользователи";
            // 
            // debugToolStripMenuItem
            // 
            debugToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sendCustomToolStripMenuItem });
            debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            debugToolStripMenuItem.Size = new Size(51, 20);
            debugToolStripMenuItem.Text = "Дебаг";
            // 
            // sendCustomToolStripMenuItem
            // 
            sendCustomToolStripMenuItem.Name = "sendCustomToolStripMenuItem";
            sendCustomToolStripMenuItem.Size = new Size(227, 22);
            sendCustomToolStripMenuItem.Text = "Отправить свое сообщение";
            sendCustomToolStripMenuItem.Click += sendCustomToolStripMenuItem_Click;
            // 
            // ChatOFGForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 461);
            Controls.Add(label1);
            Controls.Add(usersListBox);
            Controls.Add(sendButton);
            Controls.Add(messageTextBox);
            Controls.Add(chatRichTextBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ChatOFGForm";
            Tag = "ChatOFG";
            Text = "ChatOFG";
            Load += ChatOFGForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox chatRichTextBox;
        private TextBox messageTextBox;
        private Button sendButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem logToolStripMenuItem;
        private ToolStripMenuItem reconnectToolStripMenuItem;
        private ListBox usersListBox;
        private Label label1;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem sendCustomToolStripMenuItem;
    }
}