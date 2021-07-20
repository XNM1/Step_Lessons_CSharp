using System;
using System.Diagnostics;
using System.Reflection;

namespace SPLesson02
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo infop = new ProcessStartInfo();
            infop.FileName = "notepad.exe";
            infop.Arguments = "7 3 +";
            Process p = Process.Start(infop);
            //Console.ReadKey();
            //p.Kill();

            AppDomain appDomain = AppDomain.CreateDomain("new domain");
            Assembly asm = appDomain.Load(AssemblyName.GetAssemblyName("HelloClass.dll"));
            asm.GetModule("HelloClass.dll").GetType("HelloClass.HelloClass").GetMethod("HelloWorld").Invoke(null, null);
            AppDomain.Unload(appDomain);
        }
    }
}
