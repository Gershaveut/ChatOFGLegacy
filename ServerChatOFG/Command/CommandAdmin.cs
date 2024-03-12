using OFGmCoreCS.ConsoleSimple;

namespace ServerChatOFG.Command
{
    public class CommandAdmin : AbstractCommand
    {
        public Server server;

        public CommandAdmin(Server server) : base("admin", "Assigns administrator status to the user", new ArgsInput("name<string>", "User name"), new ArgsInput("admin<bool>", "Admin State"))
        {
            this.server = server;
        }

        public override Feedback Execute(string[] args)
        {
            base.Execute(args);

            string name = args[0];

            if (Convert.ToBoolean(args[1]))
            {
                server.admins.Add(name);

                return new Feedback(name + " стал админом");
            }
            else
            {
                server.admins.Remove(name);

                return new Feedback(name + " больше не админом");
            }
        }
    }
}
