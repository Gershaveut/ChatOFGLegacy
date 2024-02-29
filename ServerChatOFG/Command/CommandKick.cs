using OFGmCoreCS.ConsoleSimple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatOFG.Command
{
    public class CommandKick : AbstractCommand
    {
        public Server server;

        public CommandKick(Server server) : base("kick", "Kicks a user from the server.", new ArgsInput("name<string>", "User name"), new ArgsInput("cause<string>", "Reason for exclusion"))
        {
            this.server = server;
        }

        public override Feedback Execute(string[] args)
        {
            base.Execute(args);

            _ = server.KickClientAsync(args[0], args[1]);

            return Feedback.empty;
        }
    }
}
