using OFGmCoreCS.LoggerSimple;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatOFGAPI
{
    public class ChatOFG
    {
        public TcpClient client;
        public Logger logger;

        protected StreamReader reader;
        protected StreamWriter writer;
        protected CancellationTokenSource receiveMessageToken;

        public StreamReader Reader { get; }
        public StreamWriter Writer { get; }

        public delegate void ReceiveMessageHandler(Message message);
        public delegate void ConnectionLostHandler();

        public event ReceiveMessageHandler ReceiveMessage;
        public event ConnectionLostHandler ConnectionLost;

        public ChatOFG(Logger logger)
        {
            this.logger = logger;
        }

        public async Task Connect(TcpClient tcpClient, IPEndPoint remote, string name)
        {
            Disconnect();

            receiveMessageToken = new CancellationTokenSource();

            client = tcpClient;
            client.Connect(remote);

            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());

            if (reader is null || writer is null)
                return;

            _ = Task.Run(ReceiveMessageHandlerAsync, receiveMessageToken.Token);
            
            await writer.WriteLineAsync(name);
            await writer.FlushAsync();
        }

        public void Disconnect()
        {
            receiveMessageToken?.Cancel();
            receiveMessageToken?.Dispose();
            receiveMessageToken = null;
            reader?.Close();
            writer?.Close();
            client?.Close();
        }

        public void SendMessage(string message)
        {
            writer?.WriteLine(message);
            writer?.Flush();
        }

        protected async Task ReceiveMessageHandlerAsync()
        {
            while (true)
            {
                try
                {
                    if (reader != null)
                    {
                        string text = await reader.ReadLineAsync();

                        if (string.IsNullOrEmpty(text))
                            continue;

                        Message message = text;

                        if (message == MessageType.Kick)
                            logger.Write("Вы были исключены по причине: " + message, LoggerLevel.Warn);
                        else
                            logger.Write(message, LoggerLevel.Info);

                        ReceiveMessage?.Invoke(message);
                    }
                }
                catch (IOException ex)
                {
                    if (!receiveMessageToken?.Token.IsCancellationRequested ?? true)
                    {
                        ConnectionLost?.Invoke();
                        logger.Write(ex.Message, LoggerLevel.Warn);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    logger.Write(ex.Message, LoggerLevel.Error);
                }
            }
        }
    }
}
