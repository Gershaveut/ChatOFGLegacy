using System.Net;
using System.Net.Sockets;

namespace ClientChatOFG
{
    public partial class LoginForm : Form
    {
        public ChatOFGForm сhatOFGForm;

        public LoginForm(ChatOFGForm сhatOFGForm)
        {
            InitializeComponent();

            this.сhatOFGForm = сhatOFGForm;
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            loginButton.Enabled = false;

            try
            {
                await Task.Run(() => сhatOFGForm.chatOFG.Connect(new TcpClient(), new IPEndPoint(IPAddress.Parse(IPTextBox.Text), Convert.ToInt32(portTextBox.Text)), nameTextBox.Text));
                сhatOFGForm.Text = $"{сhatOFGForm.Text} - {nameTextBox.Text}";
                Close();
            }
            catch (Exception ex)
            {
                loginButton.Enabled = true;

                if (MessageBox.Show(ex.Message, "Ошибка подключения", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    loginButton_Click(sender, e);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!сhatOFGForm.chatOFG.client.Connected)
                Application.Exit();
        }
    }
}
