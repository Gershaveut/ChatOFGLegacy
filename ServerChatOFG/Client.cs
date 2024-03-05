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
                await server.BroadcastMessageAsync($"{name} вошел в чат", MessageType.Join);

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
                        Close();
                        await server.BroadcastMessageAsync($"{name} покинул чат", MessageType.Leave);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Close();
                server.logger.Write(e.Message, LoggerLevel.Error);
            }
        }

        public async Task SendMessageAsync(string message, LoggerLevel loggerLevel, MessageType messageType = MessageType.Message)
        {
            server.logger.Write(message, loggerLevel);

            await SendMessageAsync(message, messageType);
        }
        public async Task SendMessageAsync(string message, MessageType messageType = MessageType.Message)
        {
            await writer.WriteLineAsync($"{messageType}:{message}");
            await writer.FlushAsync();
        }

        public async Task KickClientAsync(string cause)
        {
            await SendMessageAsync(cause, LoggerLevel.Info, MessageType.Kick);
            Close();
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
            if (!server.TryRemoveConnection(name))
            {
                writer.Close();
                reader.Close();
                client.Close();
            }
        }
    }
}
