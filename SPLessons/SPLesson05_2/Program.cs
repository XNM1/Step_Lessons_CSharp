using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLesson05_2
{
    class Program
    {
        delegate int AsyncFact(int start, int end);

        static int Fact(int start, int end) {
                int sum = 1;
                for (int i = start; i <= end; i++)
                {
                    sum *= i;
                }
                return sum;
        }

        static void Main(string[] args)
        {
            int n = 12;
            int factRes = 1;
            int fNum = 0;
            int workingFunc = 0;
            for (int i = 0; i < n || (i / 2) != 1; i/=2)
            {
                fNum++;
            }

            AsyncFact[] afact = new AsyncFact[fNum];
            IAsyncResult[] ar = new IAsyncResult[fNum];
            bool[] isComplete = new bool[fNum];
            for (int i = 0; i < fNum; i++)
            {
                afact[i] = Fact;
                isComplete[i] = false;
            }
            int shift = n / fNum;
            for (int i = 0, j = 0; i < n; i+=shift, j++)
            {
                ar[j] = afact[j].BeginInvoke(i + 1, i + shift, null, null);
            }
            workingFunc = fNum;
            for (int i = 0;; i++)
            {
                if (i == fNum)          i = 0;
                if (workingFunc == 0)   break;
                if (isComplete[i])      continue;
                if (ar[i].IsCompleted && !isComplete[i]) {
                    workingFunc--;
                    factRes *= afact[i].EndInvoke(ar[i]);
                    isComplete[i] = true;
                }
            }
            Console.WriteLine(factRes);
            Console.ReadKey();
        }
    }
}
