using System;
using System.Threading;

namespace Lab_1_3
{
    class Program
    {
        static double value = 0.5;
        static object locker = new object();
        static bool isCosCalculated = false;

        static void Main(string[] args)
        {
            Thread cosThread = new Thread(CalculateCos);
            Thread acosThread = new Thread(CalculateArcCos);

            cosThread.Start();
            acosThread.Start();

            cosThread.Join();
            acosThread.Join();

            Console.WriteLine("Работа потоков завершена.");
        }

        static void CalculateCos()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (locker)
                {
                    if (!isCosCalculated)
                    {
                        Monitor.Wait(locker);
                    }

                    double newValue = Math.Cos(value);
                    Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: Cos({value}) = {newValue}");
                    value = newValue;
                    isCosCalculated = false;

                    Monitor.PulseAll(locker);
                }
            }
        }

        static void CalculateArcCos()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (locker)
                {
                    if (isCosCalculated)
                    {
                        Monitor.Wait(locker);
                    }

                    double newValue = Math.Acos(value);
                    Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: Acos({value}) = {newValue}");
                    value = newValue;
                    isCosCalculated = true;

                    Monitor.PulseAll(locker);
                }
            }
        }
    }
}
