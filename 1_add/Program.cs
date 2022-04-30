using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdditionTask
{
    class MyClass
    {
        static int counter = 0; //отлично!
        static object locker = new object();
        public async static Task CreateCounter()
        {
            await Task.Run(() =>
            {
                lock (locker)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        counter++;
                        Console.WriteLine($"Number {counter} / Thread {Thread.CurrentThread.ManagedThreadId}");
                    }
                }
            });
        }
    }

    class Program: MyClass
    {
        static void Main(string[] args)
        {
            Task task1 = CreateCounter();
            Task task2 = CreateCounter();
            Task task3 = CreateCounter();

            Task.WaitAll(task1, task2, task3);
            Console.ReadKey();
        }
    }
}
