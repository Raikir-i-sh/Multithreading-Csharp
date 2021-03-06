using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedLocks
{
    class Program
    {
        static object caztonLock = new object();
        static void Main(string[] args)
        {
			// 3 Locks are being used here But what matters is
			// the parent lock below. 
            lock (caztonLock)
            {
                DoSth();
            }
        }

        private static void DoSth()
        {
            lock (caztonLock)
            {
                Task.Delay(2000);
                AnotherMethod();
            }
        }

        private static void AnotherMethod()
        {
            lock (caztonLock)
            {

            }
        }
    }
}
