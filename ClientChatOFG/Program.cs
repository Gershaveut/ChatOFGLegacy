using OFGmCoreCS.Util;

namespace ClientChatOFG
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            ChatOFGForm chatOFGForm = new();

            try
            {
                Application.Run(new ChatOFGForm());
            }
            catch (Exception ex)
            {
                new CrashReport(CrashReporter.CreateReport(ex)).ShowDialog();
            }

            chatOFGForm.chatOFG.logger.fileLogger.SaveFile();
        }
    }
}
