using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NPLesson01
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress iP = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(iP, 1337);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(endPoint);
            socket.Listen(100);
            try
            {
                Semaphore s = new Semaphore(20, 20);
                while (true)
                {
                    socket.AcceptAsync().ContinueWith((Task<Socket> t) =>
                    {
                        s.WaitOne();
                        if (t is null)
                        {
                            throw new ArgumentNullException(nameof(t));
                        }
                        Socket socketClient = t.Result;
                        Console.WriteLine("connect {0}", ((IPEndPoint)socketClient.LocalEndPoint).Address);

                        byte[] buff = new byte[1024];
                        socketClient.Receive(buff);
                        Console.WriteLine(System.Text.Encoding.ASCII.GetString(buff));

                        socketClient.Send(System.Text.Encoding.ASCII.GetBytes("world"));

                        socketClient.Disconnect(true);
                        socketClient.Close();
                        Console.WriteLine("close");
                        s.Release();
                    });
                    Thread.Sleep(10);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                socket.Close();
            }
        }
    }
}
