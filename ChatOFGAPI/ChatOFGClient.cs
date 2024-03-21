using OFGmCoreCS.LoggerSimple;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatOFGAPI
{
    public class ChatOFGClient
    {
        public TcpClient client;
        public Logger logger;
        public string name;

        protected StreamReader reader;
        protected StreamWriter writer;

        public StreamReader Reader { get; }
        public StreamWriter Writer { get; }

        public delegate void ReceiveMessageHandler(Message message);
        public delegate void ConnectionLostHandler();

        public event ReceiveMessageHandler ReceiveMessage;
        public event ConnectionLostHandler ConnectionLost;

        public ChatOFGClient(Logger logger)
        {
            this.logger = logger;
        }

        public async Task Connect(TcpClient tcpClient, IPEndPoint remote, string name)
        {
            this.name = name;

            Disconnect();

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

        public void Disconnect()
        {
            if (GetConnected())
            {
                reader.Close();
                writer.Close();
                client.Close();
            }
        }

        public void SendMessage(Message message)
        {
            if (GetConnected())
            {
                writer.WriteLine(message);
                writer.Flush();
            }
        }

        public bool GetConnected()
        {
            return client != null ? client.Connected : false;
        }

        protected async Task ReceiveMessageHandlerAsync()
        {
            while (GetConnected())
            {
                try
                {
                    string text = await reader.ReadLineAsync();

                    if (string.IsNullOrEmpty(text))
                        continue;

                    Message message = text;

                    if (message == MessageType.Kick)
                        logger.Write("Вы были исключены по причине: " + message, LoggerLevel.Warn);
                    else
                        logger.Write(message.text, LoggerLevel.Info);

                    ReceiveMessage?.Invoke(message);
                }
                catch (Exception ex)
                {
                    logger.Write(ex.Message, LoggerLevel.Error);
                }
            }

            ConnectionLost?.Invoke();
            logger.Write("Соединение потерянно", LoggerLevel.Warn);
        }
    }
}
