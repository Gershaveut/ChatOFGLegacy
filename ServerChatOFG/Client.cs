using ChatOFGAPI;
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
                        string? text = await reader.ReadLineAsync();

                        if (text == null)
                            continue;

                        Message message = text;

                        if (message.messageType == MessageType.Message)
                            await server.BroadcastMessageAsync($"{name}: {message.text}");
                        else
                        {
                            if (server.admins.Contains(name))
                                await server.BroadcastMessageAsync(message.text, message.messageType);
                            else
                                await SendMessageAsync("У вас недостаточно прав для использования этого");
                        }
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
