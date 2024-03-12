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

            #if !DEBUG
            try
            {
            #endif
                Application.Run(new ChatOFGForm());
            #if !DEBUG
            }
            catch (Exception ex)
            {
                new CrashReport(CrashReporter.CreateReport(ex)).ShowDialog();
            }
            #endif

            chatOFGForm.chatOFG.logger.fileLogger.SaveFile();
        }
    }
}
