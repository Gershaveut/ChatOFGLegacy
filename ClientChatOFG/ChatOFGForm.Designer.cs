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
            SuspendLayout();
            // 
            // chatRichTextBox
            // 
            chatRichTextBox.BackColor = SystemColors.Window;
            chatRichTextBox.Location = new Point(12, 12);
            chatRichTextBox.Name = "chatRichTextBox";
            chatRichTextBox.ReadOnly = true;
            chatRichTextBox.Size = new Size(776, 397);
            chatRichTextBox.TabIndex = 0;
            chatRichTextBox.Text = "Вход в чат выполнен";
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(12, 415);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(695, 23);
            messageTextBox.TabIndex = 1;
            messageTextBox.KeyDown += messageTextBox_KeyDown;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(713, 415);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 2;
            sendButton.Text = "Отправить";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // ChatOFGForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(sendButton);
            Controls.Add(messageTextBox);
            Controls.Add(chatRichTextBox);
            Name = "ChatOFGForm";
            Text = "ChatOFG";
            Load += ChatOFGForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox chatRichTextBox;
        private TextBox messageTextBox;
        private Button sendButton;
    }
}