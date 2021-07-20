using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;
using StreamProtocol;

namespace Client
{
    public partial class Form1 : Form
    {
        string address;
        int port;
        bool isWorking = false;
        int interval = 16;
        MemoryStream stream;
        UdpClient client;
        public Form1()
        {
            InitializeComponent();
            buttonStop.Enabled = false;
        }

        private void buttonRecive_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(textBoxServer.Text, "^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$"))
                    throw new Exception("Incorrect server ip");
                address = textBoxServer.Text;
                port = int.Parse(textBoxPort.Text);
                interval = int.Parse(textBoxInterval.Text);
                buttonRecive.Enabled = false;
                buttonStop.Enabled = true;
                client = new UdpClient(8001);
                client.Client.SendTimeout = 1000;
                client.Client.ReceiveTimeout = 1000;
                stream = new MemoryStream();
                isWorking = true;
                Task.Run(() =>
                {
                    while (isWorking)
                    {
                        try
                        {
                            RequsetScreen();
                            ReceiveScreen();
                            Thread.Sleep(interval);
                        }
                        catch { }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RequsetScreen()
        {
            Transfer.SendUDP(client, new IPEndPoint(IPAddress.Parse(address), port), new ScreenInfoData() { Message = ScreenInfo.GetScreen });
        }

        private void ReceiveScreen()
        {
            IPEndPoint endPoint = null;
            while (isWorking) {
                Data data = Transfer.ReceiveUDP(client, ref endPoint);
                if (data is ScreenPartData && ((ScreenPartData)data).PartScreen != null)
                {
                    stream.Write(((ScreenPartData)data).PartScreen, 0, ((ScreenPartData)data).PartScreen.Length);
                }
                else if (data is ScreenInfoData && ((ScreenInfoData)data).Message == ScreenInfo.LastScreen)
                {
                    Image screen = Image.FromStream(stream);
                    stream.Flush();
                    stream.Position = 0;
                    pictureBoxFrame.Image = screen;
                    Transfer.SendUDP(client, new IPEndPoint(IPAddress.Parse(address), port), new ScreenInfoData() { Message = ScreenInfo.OK });
                    break;
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                isWorking = false;
                stream.Close();
                client.Close();
                buttonRecive.Enabled = true;
                buttonStop.Enabled = false;
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}