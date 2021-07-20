using System;
using System.Threading;
using System.Diagnostics;

namespace SPLesson06
{
    class Program
    {
        public static int requiredInstances = 3;
        public static string semaphoreName = "My_S";
        static void Main(string[] args)
        {
            Semaphore s = null;
            try
            {
                s = Semaphore.OpenExisting(semaphoreName);
            }
            catch (Exception) { }
            if (s == null) {
                s = new Semaphore(requiredInstances, requiredInstances, semaphoreName);
            }

            if (!s.WaitOne(10)) {
                Console.WriteLine("The required amount of instances has already been launched");
                Console.ReadKey();
                Process.GetCurrentProcess().Kill();
            }
            AppDomain.CurrentDomain.ProcessExit += (object sender, EventArgs e) => s.Release();

            ManualResetEvent sync = new ManualResetEvent(false);
            Counter counter = new Counter();
            Thread[] thread = new Thread[5];
            for (int i = 0; i < thread.Length; i++)
            {
                thread[i] = new Thread(counter.Increment);
                thread[i].Start(sync);
            }
            sync.Set();
            for (int i = 0; i < thread.Length; i++)
            {
                thread[i].Join();
            }
            sync.Reset();
            Console.WriteLine(counter.count);
            Console.ReadKey();
        }
    }

    class Counter {
        public int count = 0;
        //object lck = new object();
        //Mutex m = new Mutex(false, "My_M");
        //Semaphore s = new Semaphore(3, 3, "My_S");
        //AutoResetEvent e = new AutoResetEvent(true);
        ManualResetEvent e = new ManualResetEvent(true);
        public void Increment(object state) {
            ManualResetEvent sync = (ManualResetEvent)state;
            sync.WaitOne();
            for (int i = 0; i < 10000; i++)
            {
                //Interlocked.Increment(ref count);
                //while (!Monitor.TryEnter(this)) { 

                //}
                //Monitor.Enter(lck);
                //try
                //{
                //    count++;
                //}
                //finally {
                //    Monitor.Exit(lck);
                //}
                //lock (lck) {
                //count++;
                //}
                //m.WaitOne();
                //count++;
                //m.ReleaseMutex();
                //s.WaitOne();
                //count++;
                //s.Release();
                //e.WaitOne();
                //count++;
                //e.Set();
                e.WaitOne();
                e.Reset();
                count++;
                e.Set();
            }
        }
    }
}
