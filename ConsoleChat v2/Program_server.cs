using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using ChatProtocol;

namespace ConsoleChatServer
{
    class Program
    {
        private static List<Client> clients;
        private static TcpListener server;
        private static IPEndPoint iPEndPoint;

        private static string ip = "127.0.0.1";
        private static int port = 8081;

        private static object lck;
        static void Main(string[] args)
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            server = new TcpListener(iPEndPoint);
            clients = new List<Client>();
            lck = new object();

            server.Start(50);
            Console.WriteLine($"Server started: {iPEndPoint.Address}:{iPEndPoint.Port}");

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        TcpClient socket = server.AcceptTcpClient();
                        Task.Run(() =>
                        {
                            string name = ((DataMessage)Transfer.ReceiveTCP(socket)).Message;
                            Client client = new Client()
                            {
                                ClientSocket = socket,
                                ClientIPEP = (IPEndPoint)socket.Client.LocalEndPoint,
                                Name = name
                            };
                            clients.Add(client);
                            Console.WriteLine($"Client connected with name: {client.Name} and ip: {client.ClientIPEP.Address}");
                            SendToEveryone(client, $"{client.Name} joined to chat");

                            while (true)
                            {
                                string message = "[" + client.ClientIPEP.Address + "] "
                                                     + client.Name + ": " 
                                                     + ((DataMessage)Transfer.ReceiveTCP(socket)).Message;
                                Console.WriteLine($"Message has been recived: {client.Name} and ip: {client.ClientIPEP.Address}");

                                SendToEveryone(client, message);
                            }
                        });
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            });
            Console.ReadKey();
            Console.WriteLine("Server closing...");
            server.Stop();
        }

        private static void SendToEveryone(Client client, string message) {
            lock (lck)
            {
                foreach (var c in clients)
                {
                    if (c != client)
                    {
                        Transfer.SendTCP(c.ClientSocket, new DataMessage() { Message = message });
                    }
                }
                Console.WriteLine($"Message has been sent: {client.Name} and ip: {client.ClientIPEP.Address}");
            }
        }
    }

    class Client {
        public TcpClient ClientSocket { get; set; }
        public IPEndPoint ClientIPEP { get; set; }
        public string Name { get; set; }
    }
}
