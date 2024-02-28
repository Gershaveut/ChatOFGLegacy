namespace ClientChatOFG
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            ChatOFGForm chatOFGForm = new ChatOFGForm();

            Application.Run(new ChatOFGForm());

            chatOFGForm.chatOFG.logger.fileLogger.SaveFile();
        }
    }
}
