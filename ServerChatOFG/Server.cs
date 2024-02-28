using OFGmCoreCS.LoggerSimple;
using OFGmCoreCS.Util;
using System.IO;
using System.Net.Sockets;

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
                        await client.SendMessageAsync($"Пользователь с именем {client.name} уже вошёл", LoggerLevel.Error);
                        client.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Write(ex.Message, LoggerLevel.Error);
            }
        }

        public async Task BroadcastMessageAsync(string message)
        {
            logger.Write(message, LoggerLevel.Info);

            foreach (var client in clients)
            {
                await client.writer.WriteLineAsync(message);
                await client.writer.FlushAsync();
            }
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

        public void Stop()
        {
            foreach (var client in clients)
                client.Close();

            tcpListener.Stop();
        }
    }
}
