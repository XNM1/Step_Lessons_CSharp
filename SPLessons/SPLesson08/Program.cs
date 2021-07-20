using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SPLesson08
{

    class WinAPI {
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
    class Program
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static WinAPI.HookProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public static void Main()
        {
            _hookID = SetHook(_proc);
            while (true) { }
            //UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(WinAPI.HookProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return WinAPI.SetWindowsHookEx(WH_KEYBOARD_LL, proc, IntPtr.Zero, 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Console.WriteLine(vkCode);
            return WinAPI.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}
