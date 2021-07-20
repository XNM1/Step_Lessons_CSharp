using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using StreamProtocol;
using System.Runtime.InteropServices;

namespace Server
{

    class Program
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }

        private static float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor;
        }

        static UdpClient client;
        static int localPort;
        static Bitmap screen;
        static byte[] screenByte;
        static object lck;
        static bool fastMode = false;
        static Size resolution;
        static int partSize = 256;
        static Rectangle screenSize = Screen.PrimaryScreen.Bounds;
        static float scaleFactor = getScalingFactor();
        static void Main(string[] args)
        {
            lck = new object();
            do
            {
                Console.Write("Enter port: ");
            } while (!int.TryParse(Console.ReadLine(), out localPort));
            Console.Write("Do you want enable fast mode?(y/n): ");
            string answ = Console.ReadLine();
            if (answ.ToLower().Equals("y") || answ.ToLower().Equals("yes"))
            {
                fastMode = true;
                Console.WriteLine("Fast mode has been enabled");
            }
            else Console.WriteLine("Fast mode has been disabled");
            int rChose = 0;
            Console.WriteLine("Chose resolution:");
            Console.WriteLine("1 - 640  × 360");
            Console.WriteLine("2 - 848  × 480");
            Console.WriteLine("3 - 1024 × 600");
            Console.WriteLine("4 - 1280 × 720");
            Console.WriteLine("5 - 1366 × 768");
            Console.WriteLine("6 - 1600 × 900");
            Console.WriteLine("7 - 1920 × 1080");
            while (!int.TryParse(Console.ReadLine(), out rChose))
            {
                Console.WriteLine("Incorrect answer");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            switch (rChose)
            {
                case 1:
                    resolution = new Size(640, 360);
                    Console.WriteLine($"Chosen resolution: 640  × 360");
                    break;
                case 2:
                    resolution = new Size(848, 480);
                    Console.WriteLine($"Chosen resolution: 848  × 480");
                    break;
                case 3:
                    resolution = new Size(1024, 600);
                    Console.WriteLine($"Chosen resolution: 1024 × 600");
                    break;
                case 4:
                    resolution = new Size(1280, 720);
                    Console.WriteLine($"Chosen resolution: 1280 × 720");
                    break;
                case 5:
                    resolution = new Size(1366, 768);
                    Console.WriteLine($"Chosen resolution: 1366 × 768");
                    break;
                case 6:
                    resolution = new Size(1600, 900);
                    Console.WriteLine($"Chosen resolution: 1600 × 900");
                    break;
                case 7:
                    resolution = new Size(1920, 1080);
                    Console.WriteLine($"Chosen resolution: 1920 × 1080");
                    break;
                default:
                    resolution = new Size(1920, 1080);
                    Console.WriteLine($"Default resolution: 1920 × 1080");
                    break;
            }
            client = new UdpClient(localPort);
            client.Client.SendTimeout = 1000;
            Task.Run(Listen);
            Console.WriteLine($"Server started on port {localPort}");
            Console.ReadKey();
            Console.WriteLine("Server closing...");
            client.Close();
        }

        private static void Listen()
        {
            IPEndPoint endPoint = null;
            while (true)
            {
                try
                {
                    client.Client.ReceiveTimeout = 0;
                    Data data = Transfer.ReceiveUDP(client, ref endPoint);
                    if (data is ScreenInfoData && ((ScreenInfoData)data).Message == ScreenInfo.GetScreen)
                        SendScreen(endPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void SendScreen(IPEndPoint endPoint)
        {
            GetScreen();
            SendScreenParts(endPoint);
            SendLastScreen(endPoint);
            if (!fastMode) lock (lck) Console.WriteLine("{0}: Screen has been sent", DateTime.Now);
        }

        public static void GetScreen() {
            screen = ImageFromScreen();
            screenByte = ImageToByteArray(screen);
        }

        public static void SendScreenParts(IPEndPoint endPoint) {
            for (int i = 0; i < screenByte.Length;)
            {
                byte[] data = new byte[partSize];
                for (int j = 0; j < partSize && j < (screenByte.Length - i); j++, i++)
                {
                    data[j] = screenByte[i];
                }
                Transfer.SendUDPAsync(client, endPoint, new ScreenPartData() { PartScreen = data });
            }
        }

        public static void SendLastScreen(IPEndPoint endPoint) {
            client.Client.ReceiveTimeout = 1000;
            IPEndPoint point = null;
            Data data = null;
            do
            {
                Transfer.SendUDP(client, endPoint, new ScreenInfoData() { Message = ScreenInfo.LastScreen });
                data = Transfer.ReceiveUDP(client, ref point);
            } while (data is ScreenInfoData && ((ScreenInfoData)data).Message == ScreenInfo.OK);
        }

        public static Bitmap ImageFromScreen()
        {
            Bitmap bmp = new Bitmap((int)(screenSize.Width * scaleFactor), (int)(screenSize.Height * scaleFactor), PixelFormat.Format32bppRgb);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            }
            bmp = new Bitmap(bmp, resolution);
            return bmp;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
