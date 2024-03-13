using ChatOFGAPI;
using OFGmCoreCS.ConsoleSimple;
using OFGmCoreCS.LoggerSimple;
using OFGmCoreCS.Util;
using ServerChatOFG.Command;
using System.Net.Sockets;

namespace ServerChatOFG
{
    public class Server
    {
        public readonly TcpListener tcpListener;
        public readonly Logger logger = new(new Logger.Properties(), new FileLogger());
        public readonly List<Client> clients = new();
        public readonly List<string> admins = new();

        public Server(TcpListener tcpListener)
        {
            this.tcpListener = tcpListener;
        }

        public async Task Start()
        {
            try
            {
                tcpListener.Start();
                logger.Write("Сервер запущен", LoggerLevel.Info);
                _ = Task.Run(CommandHandlerAsync);

                while (true)
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                    string name = new StreamReader(tcpClient.GetStream()).ReadLine() ?? "";

                    Client client = new Client(tcpClient, this, name);

                    if (!clients.Any((c) => c.name == name))
                    {
                        foreach (Client join in clients)
                            await client.SendMessageAsync(join.name, MessageType.Join);

                        clients.Add(client);

                        _ = Task.Run(client.ProcessAsync);
                    }
                    else
                    {
                        await client.KickClientAsync($"Пользователь с именем {client.name} уже вошёл");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Write(ex.Message, LoggerLevel.Error);
            }
        }

        public Task CommandHandlerAsync()
        {
            CommandHandler commandHandler = new CommandHandler();

            commandHandler.Register(new CommandKick(this));
            commandHandler.Register(new CommandSend(this));
            commandHandler.Register(new CommandAdmin(this));

            while (true)
            {
                var feedback = commandHandler.ExecuteCommand(System.Console.ReadLine());

                if (feedback.message != "")
                    logger.Write(feedback.message, feedback.loggerLevel);
            }
        }

        public async Task BroadcastMessageAsync(string message, MessageType messageType = MessageType.Message)
        {
            logger.Write(message, LoggerLevel.Info);

            foreach (var client in clients)
                await client.SendMessageAsync(message, messageType);
        }

        public bool TryRemoveConnection(string name)
        {
            Client? client = clients.FirstOrDefault(c => c.name == name);

            if (client != null)
            {
                clients.Remove(client);
                client.Close();
                return true;
            }

            return false;
        }

        public void RemoveConnection(string name)
        {
            TryRemoveConnection(name);
        }

        public async Task SendMessageAsync(string name, string message, MessageType messageType = MessageType.Message)
        {
            Client? client = clients.FirstOrDefault(c => c.name == name);
            
            if (client != null)
                await client.SendMessageAsync(message, messageType);
        }

        public async Task KickClientAsync(string name, string cause)
        {
            Client? client = clients.FirstOrDefault(c => c.name == name);

            if (client != null)
                await client.KickClientAsync(cause);
        }

        public void Stop()
        {
            foreach (var client in clients)
                client.Close();

            tcpListener.Stop();
        }
    }
}
