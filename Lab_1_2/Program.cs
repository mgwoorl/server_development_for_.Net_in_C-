using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread firstThread = new Thread(PrintNumbers);
        Thread secondThread = new Thread(PrintNumbers);

        firstThread.Start();
        firstThread.Join();

        Thread.Sleep(1000);

        secondThread.Start();
        secondThread.Join();
    }

    static void PrintNumbers()
    {
        for (int i = 1; i <= 100; i++)
        {
            Console.WriteLine(i);
            Thread.Sleep(100);
        }
    }
}
