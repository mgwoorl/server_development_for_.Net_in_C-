using System;
using System.Threading;

namespace Lab_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(() => PrintNumbersInCertainRange(1, 10));
            Thread thread2 = new Thread(() => PrintNumbersInCertainRange(11, 20));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        static void PrintNumbersInCertainRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {i}");
                Thread.Sleep(100);
            }
        }
    }
}
