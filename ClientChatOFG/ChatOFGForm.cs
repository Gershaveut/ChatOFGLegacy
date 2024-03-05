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

        private void ReceiveMessage(Message message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    switch (message.messageType)
                    {
                        case MessageType.Message:
                            chatRichTextBox.AppendText(Environment.NewLine + message);
                            break;
                        case MessageType.Kick:
                            Disconnect("Вы были исключены по причине: " + message);
                            break;
                        case MessageType.Join:
                            usersListBox.Items.Add(message.text.Split(' ')[0]);
                            break;
                        case MessageType.Leave:
                            usersListBox.Items.Remove(message.text.Split(' ')[0]);
                            break;
                    }
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
            new Login(this).ShowDialog();
        }

        public void Connect()
        {
            chatRichTextBox.Clear();
            messageTextBox.Clear();
            usersListBox.Items.Clear();
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
            new Login(this).ShowDialog();
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Show();
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Login(this).ShowDialog();
        }
    }
}
