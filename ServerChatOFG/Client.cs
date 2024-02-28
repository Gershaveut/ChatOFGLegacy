using OFGmCoreCS.LoggerSimple;
using System.Net.Sockets;

namespace ServerChatOFG
{
    public class Client
    {
        public string name;
        public StreamWriter writer;
        public StreamReader reader;

        public TcpClient client;
        public Server server;

        public Client(TcpClient tcpClient, Server server, string name)
        {
            client = tcpClient;
            this.server = server;
            this.name = name;

            var stream = client.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }

        public async Task ProcessAsync()
        {
            try
            {
                await server.BroadcastMessageAsync($"{name} вошел в чат");

                while (true)
                {
                    try
                    {
                        string? message = await reader.ReadLineAsync();

                        if (message == null)
                            continue;

                        await server.BroadcastMessageAsync($"{name}: {message}");
                    }
                    catch
                    {
                        server.RemoveConnection(name);
                        await server.BroadcastMessageAsync($"{name} покинул чат");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                server.RemoveConnection(name);
                server.logger.Write(e.Message, LoggerLevel.Error);
            }
        }

        public async Task SendMessageAsync(string message, LoggerLevel loggerLevel)
        {
            server.logger.Write(message, loggerLevel);

            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        public async Task<string?> ReceiveMessageAsync()
        {
            string? message = await reader.ReadLineAsync();

            if (message != null)
                server.logger.Write(message, LoggerLevel.Info);

            return message;
        }

        public void Close()
        {
            writer.Close();
            reader.Close();
            client.Close();
        }
    }
}
