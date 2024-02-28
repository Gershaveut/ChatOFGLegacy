using OFGmCoreCS.LoggerSimple;
using OFGmCoreCS.Util;

namespace ClientChatOFG
{
    public partial class ChatOFGForm : Form
    {
        public ChatOFG chatOFG = new ChatOFG(new Logger(new Logger.Properties(), new FileLogger()));

        public ChatOFGForm()
        {
            InitializeComponent();

            chatOFG.ReceiveMessage += ReceiveMessage;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void ReceiveMessage(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    chatRichTextBox.AppendText(Environment.NewLine + message);
                });
                return;
            }
        }

        private void messageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Send();
        }

        private void Send()
        {
            try
            {
                chatOFG.SendMessage(messageTextBox.Text);
            }
            catch 
            {
                chatOFG.Disconnect();
                MessageBox.Show("Соединение потеряно", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new LoginForm(this).ShowDialog();
            }

            messageTextBox.Text = "";
        }

        private void ChatOFGForm_Load(object sender, EventArgs e)
        {
            new LoginForm(this).ShowDialog();
        }
    }
}
