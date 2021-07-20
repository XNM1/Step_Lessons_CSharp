using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChatProtocol
{
    public interface Data {}

    [Serializable]
    public class DataMessage : Data
    {
        public string Message { set; get; }
    }

    public class Transfer {
        private static BinaryFormatter formatter = new BinaryFormatter();
        public static void SendTCP(TcpClient client, Data data) {
            //client.GetStream().Flush();
            formatter.Serialize(client.GetStream(), data);
        }

        public static Data ReceiveTCP(TcpClient client)
        {
            return (Data)formatter.Deserialize(client.GetStream());
        }

        public static void SendUDP(UdpClient client, Data data)
        {
            using MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, data);
            byte[] buff = ms.ToArray();
            client.Send(buff, buff.Length);
        }

        public static Data ReceiveUDP(UdpClient client)
        {
            IPEndPoint endPoint = null;
            byte[] buff = client.Receive(ref endPoint);
            using MemoryStream ms = new MemoryStream();
            ms.Write(buff);
            return (Data)formatter.Deserialize(ms);
        }

        public static void SendUDP(UdpClient client, IPEndPoint removeEnd, Data data)
        {
            using MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, data);
            byte[] buff = ms.ToArray();
            client.Send(buff, buff.Length, removeEnd);
        }

        public static Data ReceiveUDP(UdpClient client, ref IPEndPoint removeEnd)
        {
            byte[] buff = client.Receive(ref removeEnd);
            using MemoryStream ms = new MemoryStream();
            ms.Write(buff);
            return (Data)formatter.Deserialize(ms);
        }

    }
}
