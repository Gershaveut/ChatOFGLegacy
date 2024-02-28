using OFGmCoreCS.LoggerSimple;
using System.Net;
using System.Net.Sockets;

namespace ClientChatOFG
{
    public class ChatOFG
    {
        public TcpClient? client;
        public Logger logger;

        protected StreamReader? reader;
        protected StreamWriter? writer;

        public StreamReader? Reader { get; }
        public StreamWriter? Writer { get; }

        public delegate void ReceiveMessageHandler(string message);

        public event ReceiveMessageHandler? ReceiveMessage;

        public ChatOFG(Logger logger)
        {
            this.logger = logger;
        }

        public async Task Connect(TcpClient tcpClient, IPEndPoint remote, string name)
        {
            try
            {
                client = tcpClient;
                client.Connect(remote);

                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());

                if (reader is null || writer is null)
                    return;

                _ = Task.Run(ReceiveMessageHandlerAsync);

                await writer.WriteLineAsync(name);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                logger.Write(ex.Message, LoggerLevel.Error);
                throw ex;
            }
        }

        public void Disconnect()
        {
            reader?.Close();
            writer?.Close();
            client?.Close();
        }

        public void SendMessage(string message)
        {
            if (writer is not null)
            {
                writer.WriteLine(message);
                writer.Flush();
            }
            else
                logger.Write("Нет подключения", LoggerLevel.Error);
        }

        protected async Task ReceiveMessageHandlerAsync()
        {
            while (true)
            {
                try
                {
                    if (reader is not null)
                    {
                        string? message = await reader.ReadLineAsync();

                        if (string.IsNullOrEmpty(message))
                            continue;

                        logger.Write(message, LoggerLevel.Info);
                        ReceiveMessage?.Invoke(message);
                    }
                    else
                    {
                        logger.Write("Нет подключения", LoggerLevel.Error);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    logger.Write(ex.Message, LoggerLevel.Error);
                }
            }
        }
    }
}
