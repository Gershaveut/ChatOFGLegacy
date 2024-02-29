using OFGmCoreCS.LoggerSimple;
using OFGmCoreCS.Util;

namespace ClientChatOFG
{
    public partial class ChatOFGForm : Form
    {
        public ChatOFG chatOFG = new ChatOFG(new Logger(new Logger.Properties(), new FileLogger()));
        public Log log;

        public ChatOFGForm()
        {
            InitializeComponent();

            chatOFG.ReceiveMessage += ReceiveMessage;
            chatOFG.ConnectionLost += ConnectionLost;
            log = new Log(chatOFG.logger);
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
                    if (message.Split(':')[0] == "KICK")
                        Disconnect("Вы были исключены по причине: " + message.Split(':')[1]);
                    else
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
            chatOFG.SendMessage(messageTextBox.Text);
            messageTextBox.Text = "";
        }

        public void Disconnect(string cause)
        {
            chatOFG.Disconnect();
            ShowMessageBoxWithLog(cause, "Соединение потеряно", MessageBoxButtons.OK, MessageBoxIcon.Warning, LoggerLevel.Warn);
            new LoginForm(this).ShowDialog();
        }

        public void Connect()
        {
            chatRichTextBox.Clear();
            messageTextBox.Clear();
        }

        public DialogResult ShowMessageBoxWithLog(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, LoggerLevel logLevel)
        {
            chatOFG.logger.Write(text, logLevel);
            return MessageBox.Show(text, caption, buttons, icon);
        }

        private void ConnectionLost()
        {
            Disconnect("Удаленный хост принудительно разорвал существующее подключение..");
        }

        private void ChatOFGForm_Load(object sender, EventArgs e)
        {
            new LoginForm(this).ShowDialog();
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Show();
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LoginForm(this).ShowDialog();
        }
    }
}
