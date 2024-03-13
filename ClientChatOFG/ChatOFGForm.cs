using ChatOFGAPI;
using OFGmCoreCS.LoggerSimple;
using OFGmCoreCS.Util;
using Message = ChatOFGAPI.Message;

namespace ClientChatOFG
{
    public partial class ChatOFGForm : Form
    {
        public ChatOFGClient chatOFGClient = new ChatOFGClient(new Logger(new Logger.Properties(), new FileLogger()));
        public Log log;

        public ChatOFGForm()
        {
            InitializeComponent();

            chatOFGClient.ReceiveMessage += ReceiveMessage;
            chatOFGClient.ConnectionLost += ConnectionLost;
            log = new Log(chatOFGClient.logger);
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
                            Disconnect("�� ���� ��������� �� �������: " + message);
                            break;
                        case MessageType.Join:
                            usersListBox.Items.Add(message.text.Split(' ')[0]);
                            break;
                        case MessageType.Leave:
                            usersListBox.Items.Remove(message.text.Split(' ')[0]);
                            break;
                        case MessageType.Error:
                            ShowMessageBoxWithLog(message.text, "������", MessageBoxButtons.OK, MessageBoxIcon.Error, LoggerLevel.Error);
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
            if (messageTextBox.Text != "")
            {
                chatOFGClient.SendMessage("message:" + messageTextBox.Text);
                messageTextBox.Text = "";
            }
        }

        public void Disconnect(string cause)
        {
            chatOFGClient.Disconnect();
            ShowMessageBoxWithLog(cause, "���������� ��������", MessageBoxButtons.OK, MessageBoxIcon.Warning, LoggerLevel.Warn);
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
            chatOFGClient.logger.Write(text, logLevel);
            return MessageBox.Show(text, caption, buttons, icon);
        }

        private void ConnectionLost()
        {
            Disconnect("��������� ���� ������������� �������� ������������ �����������..");
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

        private void sendCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chatOFGClient.SendMessage(messageTextBox.Text);
            messageTextBox.Text = "";
        }

        private void kickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var textInputDialog = new TextInputDialog("������� ����������"))
            {
                textInputDialog.ShowDialog();
                if (usersListBox.SelectedItem is not null && textInputDialog.textWriten)
                    chatOFGClient.SendMessage(new Message($"{textInputDialog.textBox.Text}:{usersListBox.SelectedItem}", MessageType.Kick).ToFullText());
            }
        }
    }
}
