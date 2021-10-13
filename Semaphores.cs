using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphores
{
    class Program
    {
		//they can be source intensive.
		//they ensure that not more than specified no. of concurrent threads
		//can access a particular resource.
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3); //capacity =3
        static void Main(string[] args)
        {
			//we're trying for 10 threads but capacity is 3.
			//so, 10 threads will be made.
            for (int i = 0; i < 10; i++)
            {
                new Thread(EnterSemaphore).Start(i+1);
            }
        }

        private static void EnterSemaphore(object id)
        {
            Console.WriteLine(id + " is waiting to be part of the club");
            //But, only 3 of them will pass through below line.
			//other will wait to access or may never get access.
			semaphoreSlim.Wait(); 
            Console.WriteLine(id + " part of the club");
            Thread.Sleep(1000 / (int)id);
            Console.WriteLine(id + " left the club");
        }
    }
}
