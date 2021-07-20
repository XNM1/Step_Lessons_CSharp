using System;
using System.Threading;
using System.IO;

namespace SPLesson05
{
    class Program
    {
        delegate int AsyncSum(int size);
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("C:\\Users\\IamIllusion\\source\\repos\\SPLesson01\\SPLesson04\\Program.cs", FileMode.Open);
            byte[] b = new byte[256];
            //int br = fs.Read(b, 0, b.Length);
            //fs.BeginRead(b, 0, b.Length, (IAsyncResult ar) => {
            //    int br = fs.EndRead(ar);
            //    Console.WriteLine(br);
            //}, null);
            AsyncSum asum = Sum;
            asum.BeginInvoke(100, (IAsyncResult ar) => { Console.WriteLine(asum.EndInvoke(ar)); }, null);
            Console.ReadKey();
        }

        static int Sum(int size) {
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
