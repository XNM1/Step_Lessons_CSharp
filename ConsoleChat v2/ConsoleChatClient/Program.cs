using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ChatProtocol;

namespace ConsoleChatClient
{
    class Program
    {
        private static TcpClient client;
        private static UdpClient server;
        private static IPEndPoint iPEndPoint;

        private static IPAddress ip = IPAddress.Parse("127.0.0.1");
        private static int port = 8081;
        private static string message = "";
        private static string name;
        static void Main(string[] args)
        {
            client = new TcpClient();
            server = new UdpClient(8082) { EnableBroadcast = true };
            iPEndPoint = new IPEndPoint(ip, port);
            client.Connect(iPEndPoint);

            Console.Write("Enter name: ");
            name = Console.ReadLine();

            Transfer.SendTCP(client, new DataMessage() { Message = name });

            ReciveChatAsync();
            ReciveServerAsync();

            while (!message.Equals("exit")) {
                Console.Write($"[{iPEndPoint.Address}] {name}: ");
                message = Console.ReadLine();

                Transfer.SendTCP(client, new DataMessage() { Message = message });
            }
        }

        private static async void ReciveChatAsync() {
            await Task.Run(() =>
            {
                while (true)
                {
                    string message = ((DataMessage)Transfer.ReceiveTCP(client)).Message;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine(message);
                    Console.Write($"[{iPEndPoint.Address}] {name}: ");
                }
            });
        }

        private static async void ReciveServerAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    string message = ((DataMessage)Transfer.ReceiveUDP(server)).Message;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine(message);
                    Console.Write($"[{iPEndPoint.Address}] {name}: ");
                }
            });
        }
    }
}
