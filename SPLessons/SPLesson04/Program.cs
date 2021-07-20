using System;
using System.Threading;

namespace SPLesson04
{
    class Program
    {
        public static void Sum(object state) {
            int sum = 0;
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                sum += r.Next(1,100);
            }
            Console.WriteLine("Sum: " + sum);
        }

        public static void Mul(object state)
        {
            int sum = 1;
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                sum *= r.Next(1, 5);
            }
            Console.WriteLine("Mul: " + sum);
        }

        public static void Diff(object state)
        {
            int sum = 0;
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                sum -= r.Next(1,100);
            }
            Console.WriteLine("Diff: " + sum);
        }
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(3, 6);
            ThreadPool.QueueUserWorkItem(Sum);
            ThreadPool.QueueUserWorkItem(Mul);
            ThreadPool.QueueUserWorkItem(Diff);

            //int w1, w2;
            //ThreadPool.GetMaxThreads(out w1, out w2);
            //Console.WriteLine("W1: " + w1);
            //Console.WriteLine("W2: " + w2);
            //Timer t = new Timer(Sum);
            //t.Change(0, 2000);
            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
