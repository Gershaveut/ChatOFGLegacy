using OFGmCoreCS.ConsoleSimple;
using OFGmCoreCS.LoggerSimple;
using OFGmCoreCS.Util;
using ServerChatOFG.Command;
using System.IO;
using System.Net.Sockets;
using System.Xml.Linq;

namespace ServerChatOFG
{
    public class Server
    {
        public readonly TcpListener tcpListener;
        public readonly Logger logger = new(new Logger.Properties(), new FileLogger());
        public readonly List<Client> clients = new();

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
                        clients.Add(client);
                        _ = Task.Run(client.ProcessAsync);
                    }
                    else
                    {
                        await client.SendMessageAsync($"KICK:Пользователь с именем {client.name} уже вошёл", LoggerLevel.Error);
                        client.Close();
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

            while (true)
            {
                var feedback = commandHandler.ExecuteCommand(System.Console.ReadLine());

                if (feedback.message != "")
                    logger.Write(feedback.message, feedback.loggerLevel);
            }
        }

        public async Task BroadcastMessageAsync(string message, string argument)
        {
            logger.Write(message, LoggerLevel.Info);

            foreach (var client in clients)
            {
                await client.writer.WriteLineAsync(argument + message);
                await client.writer.FlushAsync();
            }
        }

        public async Task BroadcastMessageAsync(string message)
        {
            await BroadcastMessageAsync(message, "MESSAGE:");
        }

        public void RemoveConnection(string name)
        {
            Client? client = clients.FirstOrDefault(c => c.name == name);

            if (client != null)
            {
                clients.Remove(client);
                client.Close();
            }
        }

        public async Task SendMessageAsync(string name, string message)
        {
            Client? client = clients.FirstOrDefault(c => c.name == name);

            if (client != null)
                await client.SendMessageAsync("MESSAGE:" + message);
        }

        public async Task KickClientAsync(string name, string cause)
        {
            await SendMessageAsync(name, "KICK:" + cause);
            RemoveConnection(name);
        }

        public void Stop()
        {
            foreach (var client in clients)
                client.Close();

            tcpListener.Stop();
        }
    }
}
