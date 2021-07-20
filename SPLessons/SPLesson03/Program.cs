using System;
using System.Threading;

namespace SPLesson03
{
    class Program
    {
        //public static void HelloT(object t)
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        Console.WriteLine("Hello " + i);
        //        Thread.Sleep(100);
        //    }
        //    ((Timer)t).Dispose();
        //}
        
        public static void HelloTH(object obj)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Hello " + obj + ' ' + i);
            }
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Timer t = new Timer(Hello);
            //t.Change(1000, 2000);
            //Thread.Sleep(5000);
            //Console.WriteLine("Hello World 2!");
            Thread thread1 = new Thread(HelloTH);
            thread1.IsBackground = true;
            thread1.Start("H");
            thread1.Join();
            //while (thread1.IsAlive) { }
            Console.WriteLine("Hello World!");
        }
    }
}
