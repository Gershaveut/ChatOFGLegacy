using OFGmCoreCS.Util;
using System.Net;
using System.Net.Sockets;

namespace ServerChatOFG
{
    internal static class Program
    {
        public static Server? server;

        static async Task Main()
        {
            Console.Title = "Server ChatOFG";

            Console.Write("IP: ");
            string ip = Console.ReadLine() ?? IPAddress.Any.ToString();
            ip = ip == "" ? IPAddress.Any.ToString() : ip;

            Console.Write("Port: ");
            string defaultPort = "7500";
            string port = Console.ReadLine() ?? defaultPort;
            port = port == "" ? defaultPort : port;

            server = new Server(new TcpListener(IPAddress.Parse(ip), Convert.ToInt32(port)));

            ConsoleHelper.SetSignal((consoleSignal) => server.logger.fileLogger.SaveFile(), true);

            Console.CancelKeyPress += (sender, e) => server.logger.fileLogger.SaveFile();

            Console.WriteLine();

            await server.Start();
        }
    }
}
