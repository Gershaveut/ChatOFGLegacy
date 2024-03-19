using System.Net;
using System.Net.Sockets;

namespace ClientChatOFG
{
    public partial class Login : Form
    {
        public ChatOFGForm сhatOFGForm;

        public Login(ChatOFGForm сhatOFGForm)
        {
            InitializeComponent();

            this.сhatOFGForm = сhatOFGForm;
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            loginButton.Enabled = false;

            try
            {
                await Task.Run(() => сhatOFGForm.chatOFGClient.Connect(new TcpClient(), new IPEndPoint(IPAddress.Parse(IPTextBox.Text), Convert.ToInt32(portTextBox.Text)), nameTextBox.Text));
                сhatOFGForm.Text = $"{сhatOFGForm.Tag} - {nameTextBox.Text}";
                сhatOFGForm.Connect();
                Close();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Ошибка подключения", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    loginButton_Click(sender, e);
            }

            loginButton.Enabled = true;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(сhatOFGForm.chatOFGClient.client is not null && сhatOFGForm.chatOFGClient.client.Connected))
                Application.Exit();
        }
    }
}
