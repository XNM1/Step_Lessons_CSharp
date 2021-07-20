using System;

namespace SPLesson09
{
    class Program
    {
        static void Main(string[] args)
        {
            unsafe {
                int c = 1;
                int* p = &c;
                (*p)++;
                Console.WriteLine(*p);
                int c1 = 5;
                Square(&c1);
                Console.WriteLine(c1);
            }
            Console.WriteLine("Hello World!");
        }

        unsafe static void Square(int *v) {
            *v *= *v; 
        }
    }
}
