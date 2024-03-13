namespace ClientChatOFG
{
    public partial class TextInputDialog : Form
    {
        public bool textWriten;

        public TextInputDialog(string inputType)
        {
            InitializeComponent();

            inpntTypeLabel.Text += inputType;
        }

        public TextInputDialog() : this("")
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "")
                textWriten = true;

            Close();
        }
    }
}
