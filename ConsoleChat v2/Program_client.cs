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
        private static IPEndPoint iPEndPoint;

        private static string ip = "127.0.0.1";
        private static int port = 8081;
        private static string message = "";
        private static string name;
        static void Main(string[] args)
        {
            client = new TcpClient();
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            client.Connect(iPEndPoint);

            Console.Write("Enter name: ");
            name = Console.ReadLine();

            Transfer.SendTCP(client, new DataMessage() { Message = name });

            ReciveAsync();

            while (!message.Equals("exit")) {
                Console.Write($"[{iPEndPoint.Address}] {name}: ");
                message = Console.ReadLine();

                Transfer.SendTCP(client, new DataMessage() { Message = message });
            }
        }

        private static async void ReciveAsync() {
            await Task.Run(() =>
            {
                while (true)
                {
                    string message = ((DataMessage)Transfer.ReceiveTCP(cl)).Message;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine(message);
                    Console.Write($"[{iPEndPoint.Address}] {name}: ");
                }
            });
        }
    }
}
