using OFGmCoreCS.LoggerSimple;

namespace ClientChatOFG
{
    public partial class Log : Form
    {
        public Log(Logger logger)
        {
            InitializeComponent();

            logger.LogWritten += LogWritten;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            if (logTextBox.Text != "")
                Clipboard.SetText(logTextBox.Text);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            logTextBox.Clear();
        }

        private void logTextBox_VisibleChanged(object sender, EventArgs e)
        {
            if (logTextBox.Visible)
            {
                logTextBox.SelectionStart = logTextBox.TextLength;
                logTextBox.ScrollToCaret();
            }
        }

        private void log_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }

        private void LogWritten(string message, LoggerLevel loggerLevel)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    int startSelect = logTextBox.TextLength;
                    logTextBox.AppendText(message);

                    logTextBox.Select(startSelect, logTextBox.TextLength);
                    logTextBox.SelectionColor = LoggerLevelColor.GetColor(loggerLevel);
                    logTextBox.DeselectAll();
                });
                return;
            }
        }
    }
}
