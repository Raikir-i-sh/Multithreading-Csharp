using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterlocks
{
    class Program
    {
		/*ReadLocks can be acquired by many threads at same time.
		But only one thread can acquire write lock.*/
        static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        static Dictionary<int, string> persons = new Dictionary<int, string>();
        static Random random = new Random();
        static void Main(string[] args)
        {
            var task1 = Task.Factory.StartNew(Read);
            var task2 = Task.Factory.StartNew(Write, "Cazton");
            var task3 = Task.Factory.StartNew(Write, "Chander");
            var task4 = Task.Factory.StartNew(Read);
            var task5 = Task.Factory.StartNew(Read);
            Task.WaitAll(task1, task2, task3, task4, task5);
        }

        static void Read()
        {
            for (int i = 0; i < 10; i++)
            {
                readerWriterLockSlim.EnterReadLock();
                Thread.Sleep(50);
                readerWriterLockSlim.ExitReadLock();
            }
        }

        static void Write(object user)
        {
            for (int i = 0; i < 10; i++)
            {
                //int id = random.Next(2000, 5000);
                int id = GetRandom();
                readerWriterLockSlim.EnterWriteLock();
                var person = "Person " + i;
                persons.Add(id, person);
                readerWriterLockSlim.ExitWriteLock();
                Console.WriteLine(user + " added " + person);
                Thread.Sleep(250);
            }
        }

        static int GetRandom()
        {
            lock (random)
            {
                return random.Next(2000, 5000);
            }
        }
    }
}
