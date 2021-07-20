using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace StreamProtocol
{
    public interface Data { }

    [Serializable]
    public enum ScreenInfo { 
        OK,
        LastScreen,
        GetScreen
    }

    [Serializable]
    public class ScreenPartData : Data
    {
        public byte[] PartScreen { set; get; }
    }

    [Serializable]
    public class ScreenInfoData : Data
    {
        public ScreenInfo Message { set; get; }
    }

    [Serializable]
    public class CountPartsScreen : Data
    {
        public int CountParts { set; get; }
    }

    public class Transfer
    {
        private static BinaryFormatter formatter = new BinaryFormatter();
        public static void SendTCP(TcpClient client, Data data)
        {
            formatter.Serialize(client.GetStream(), data);
        }

        public static async void SendTCPAsync(TcpClient client, Data data)
        {
            await Task.Run(() => formatter.Serialize(client.GetStream(), data));
        }

        public static Data ReceiveTCP(TcpClient client)
        {
            return (Data)formatter.Deserialize(client.GetStream());
        }

        public static async void SendUDPAsync(UdpClient client, IPEndPoint removeEnd, Data data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, data);
                byte[] buff = ms.ToArray();
                await client.SendAsync(buff, buff.Length, removeEnd);
            }
        }

        public static void SendUDP(UdpClient client, IPEndPoint removeEnd, Data data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, data);
                byte[] buff = ms.ToArray();
                client.Send(buff, buff.Length, removeEnd);
            }
        }

        public static Data ReceiveUDP(UdpClient client, ref IPEndPoint removeEnd)
        {
            byte[] buff = client.Receive(ref removeEnd);
            using (MemoryStream ms = new MemoryStream()) {
                ms.Write(buff, 0, buff.Length);
                ms.Position = 0;
                return (Data)formatter.Deserialize(ms);
            }
        }

    }
}
