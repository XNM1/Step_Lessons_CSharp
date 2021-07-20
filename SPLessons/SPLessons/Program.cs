using System;
using System.Runtime.InteropServices;

namespace SPLessons
{
    public static class WinApi {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);
    }
    class Program
    {
        static void Main()
        {
            WinApi.MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0x00000001 | 0x00000030);
            IntPtr ptr = WinApi.FindWindow(null, "Untitled - Notepad");
            Console.WriteLine(ptr);
            WinApi.SetWindowText(ptr, "hello");
        }
    }
}
