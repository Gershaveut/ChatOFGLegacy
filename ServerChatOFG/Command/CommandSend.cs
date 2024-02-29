using OFGmCoreCS.ConsoleSimple;

namespace ServerChatOFG.Command
{
    public class CommandSend : AbstractCommand
    {
        public Server server;

        public CommandSend(Server server) : base("send", "Sends a message to a user or everyone.", new ArgsInput("message<string>", "Message to half the users"), new ArgsInput("name?<string>", "User name"))
        {
            needArgs -= 1;

            this.server = server;
        }

        public override Feedback Execute(string[] args)
        {
            base.Execute(args);

            args[0] = "Server: " + args[0];

            if (args.Length > 1)
                _ = server.SendMessageAsync(args[1], args[0]);
            else
                _ = server.BroadcastMessageAsync(args[0]);

            return Feedback.empty;
        }
    }
}
