namespace ClientChatOFG
{
    partial class TextInputDialog
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
            ok = new Button();
            textBox = new TextBox();
            inpntTypeLabel = new Label();
            SuspendLayout();
            // 
            // ok
            // 
            ok.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ok.Location = new Point(250, 58);
            ok.Name = "ok";
            ok.Size = new Size(75, 23);
            ok.TabIndex = 0;
            ok.Text = "ОК";
            ok.UseVisualStyleBackColor = true;
            ok.Click += ok_Click;
            // 
            // textBox
            // 
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Location = new Point(12, 27);
            textBox.Name = "textBox";
            textBox.Size = new Size(313, 23);
            textBox.TabIndex = 1;
            // 
            // inpntTypeLabel
            // 
            inpntTypeLabel.AutoSize = true;
            inpntTypeLabel.Location = new Point(12, 9);
            inpntTypeLabel.Name = "inpntTypeLabel";
            inpntTypeLabel.Size = new Size(53, 15);
            inpntTypeLabel.TabIndex = 2;
            inpntTypeLabel.Text = "Введите ";
            // 
            // TextInputDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(337, 93);
            Controls.Add(inpntTypeLabel);
            Controls.Add(textBox);
            Controls.Add(ok);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TextInputDialog";
            ShowIcon = false;
            Text = "Введите текст";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ok;
        public TextBox textBox;
        private Label inpntTypeLabel;
    }
}