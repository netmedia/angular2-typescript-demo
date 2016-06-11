using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Netmedia.Infrastructure
{
    public class PadLock
    {
        public static void Do(object usingLockObject, Action executeAnAction)
        {
            var monitorSucceded = false;
            object localLock = null;

            try
            {
                Monitor.Enter(localLock = usingLockObject, ref monitorSucceded);

                executeAnAction();
            }
            finally
            {
                if (monitorSucceded) Monitor.Exit(localLock);
            }            
        }
    }
}
