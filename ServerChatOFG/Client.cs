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
                await server.BroadcastMessageAsync(new Message($"{name} вошел в чат", MessageType.Join));

                while (true)
                {
                    try
                    {
                        string? text = await reader.ReadLineAsync();

                        if (string.IsNullOrEmpty(text))
                            continue;

                        Message message = text;

                        if (message.messageType == MessageType.Message)
                            await server.BroadcastMessageAsync($"{name}: {message.text}");
                        else
                        {
                            if (server.admins.Contains(name))
                                switch (message.messageType)
                                {
                                    default:
                                        await server.BroadcastMessageAsync(message);
                                        server.logger.Write($"{name} отправил команду {message}", LoggerLevel.Info);
                                        break;
                                    case MessageType.Kick:
                                        string kickedName = message.text.Split(":")[1];
                                        string cause = message.text.Split(":")[0];

                                        await server.SendMessageAsync(kickedName, new Message(cause, MessageType.Kick));
                                        server.logger.Write($"{name} исключил {kickedName} по причине {cause}", LoggerLevel.Info);
                                        break;
                                }
                            else
                                await SendMessageAsync(new Message("У вас недостаточно прав для использования этого", MessageType.Error));
                        }
                    }
                    catch
                    {
                        Close();
                        await server.BroadcastMessageAsync(new Message($"{name} покинул чат", MessageType.Leave));
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

        public async Task SendMessageAsync(Message message, LoggerLevel loggerLevel)
        {
            server.logger.Write(message, loggerLevel);

            await SendMessageAsync(message);
        }
        public async Task SendMessageAsync(Message message)
        {
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        public async Task KickClientAsync(string cause)
        {
            await SendMessageAsync(new Message(cause, MessageType.Kick), LoggerLevel.Info);
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
