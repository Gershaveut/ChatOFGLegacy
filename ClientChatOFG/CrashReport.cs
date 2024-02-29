using System;
using System.Windows.Forms;

namespace ClientChatOFG
{
    public partial class CrashReport : Form
    {
        public CrashReport(string crashReport)
        {
            InitializeComponent();

            crashReportTextBox.Text = crashReport;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(crashReportTextBox.Text);
        }
    }
}
