using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace SPLesson07
{
    class Program
    {
        static void Main(string[] args)
        {
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //cancellationTokenSource.Cancel();
            //int i1 = 0;
            //int i2 = 0;
            //Task task = Task.Run(() => i1 + i2)
            //    .ContinueWith((Task<int> t) => (double)t.Result * t.Result)
            //    .ContinueWith((Task<double> t) => t.Result / 3)
            //    .ContinueWith((Task<double> t) => Console.WriteLine(t.Result));

            //int sum = 0;
            //Task task = Task.Run(() => {
            //    for (int i = 0; i < 10000000; i++)
            //    {
            //        Thread.Sleep(100);
            //        sum += i;
            //        if (cancellationTokenSource.Token.IsCancellationRequested) return;
            //        //lock (lck) {
            //        //    while (tokenStop)
            //        //    {
            //        //        Thread.Sleep(10);
            //        //    }
            //        //}
            //    }
            //});
            //Console.ReadKey();
            //cancellationTokenSource.Cancel();
            //Console.WriteLine(sum);
            //Parallel.Invoke(new ParallelOptions { CancellationToken = cancellationTokenSource.Token }
            //, () => Console.WriteLine("H")
            //, () => Console.WriteLine("E")
            //, () => Console.WriteLine("L")
            //, () => Console.WriteLine("L")
            //, () => Console.WriteLine("O"));

            //int[] arr = new int[10];
            //Parallel.For(1, 10, (int i) => arr[i] = i * 2);

            //ParallelLoopResult r = Parallel.ForEach(new List<double> { 0.1, 0.2, 0.3, 0.4 }, (double i, ParallelLoopState state) => { 
            //    if (i == 0.3) state.Break();
            //    Console.WriteLine(i * 2); 
            //});
            //Console.WriteLine(r.LowestBreakIteration);

            //List<double> lst = new List<double> { 0.1, 0.2, 0.3, 0.4, 1,2,3,4,4,5,5,6,7,4,54,54,534,5,435,4,5,45,34,534,5,34,53,45,34,5,5,12,4,6756,7,648,2 };
            //var _lst1 = from l in lst.AsParallel()
            //            select l * 2;

            //var __lst1 = from l in _lst1.AsParallel()
            //             select l * 4;
            //List<double> lst1 = __lst1.ToList();

            //List<double> lst2 = lst.AsParallel().WithCancellation(cancellationTokenSource.Token).AsOrdered().Select((double l) => l * 2).ToList();
            //foreach (var item in lst2)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.ReadKey();
            Task t = DoSomething(2,3);
            t.Wait();
        }
        public static async Task DoSomething(int i1, int i2) {
            Console.WriteLine("Hello");
            await Task.Run(() => Console.WriteLine(i1 + i2));
            await Task.Run(() => Console.WriteLine(i1 * i2));

            //Task.Run(() => Console.WriteLine(i1 + i2))
            //    .ContinueWith((Task t) => Console.WriteLine(i1 * i2));
            await DoSomething2(i1, i2);
        }

        public static async Task DoSomething2(int i1, int i2)
        {
            await Task.Run(() => Console.WriteLine(i1 - i2));
        }
    }
}
