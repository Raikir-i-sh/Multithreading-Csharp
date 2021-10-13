using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlocks
{
    class Program
    {
        //Deadlocks can occur when you use nested locks in several places.
        static void Main(string[] args)
        {
            object caztonLock = new object();
            object chanderLock = new object();
			//this below is not main thread but another thread.
			
            new Thread(() =>
            {
				//in this case, caztonLock was started in 2nd thread and goes to sleep 
				//while main thread tries to access it But it can't as it's occupied.
				//and same is for chanderlock , hence deadlock occurs.
                lock (caztonLock)
                {
                    Console.WriteLine("Cazton Lock obtained");
                    Thread.Sleep(2000);
                    lock (chanderLock)
                    {
                        Console.WriteLine("Chander Lock obtained");
                    }
                }
            }).Start();
            lock (chanderLock)
            {
                Console.WriteLine("Main Thread obtained Chander Lock");
                Thread.Sleep(1000);
                lock (caztonLock)
                {
                    Console.WriteLine("Main Thread obtained Chander Lock");
                }
            }
        }
    }
}
