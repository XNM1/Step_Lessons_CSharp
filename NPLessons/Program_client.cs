using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NPLesson01_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress iP = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(iP, 1337);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(endPoint);
                socket.Send(System.Text.Encoding.ASCII.GetBytes("hello"));

                byte[] buff = new byte[1024];
                socket.Receive(buff);
                Console.WriteLine(System.Text.Encoding.ASCII.GetString(buff));
                Console.ReadKey();
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
