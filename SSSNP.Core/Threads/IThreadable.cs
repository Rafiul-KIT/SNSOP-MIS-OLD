using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Threads
{
    public interface IThreadable
    {
        string GetName();
        int GetSleepTime();
        AutoResetEvent GetWaitHandle();
        AutoResetEvent GetAbortHandle();

        /// <summary>
        /// Your implementation of task() must handle ThreadAbortException.
        /// 
        /// If you want to be able to wake a thread from sleep state. Do not use Thread.Sleep for waiting a
        /// long duration. Instead use "getWaitHandle().WaitOne(getSleepTime())".
        /// 
        /// For safe checkpoints in your code where it is safe to abort use "getAbortHandle().WaitOne(1)", and
        /// check for a TRUE return value.
        /// 
        /// Here is a code example:
        /// <code>
        ///private AutoResetEvent waitHandle = new AutoResetEvent(false);
        ///private AutoResetEvent abortHandle = new AutoResetEvent(false);
        /// 
        ///public void task()
        ///{
        ///    try
        ///    {
        ///        while (true)
        ///        {
        ///            // Do your work here
        ///
        ///            // Going into sleep mode
        ///            getWaitHandle().WaitOne(getSleepTime());
        ///            // Woke up from sleep after timeout or when the resumed
        ///            if (getAbortHandle().WaitOne(1))
        ///            {
        ///                // Abort request received. Getting out
        ///                break;
        ///            }
        ///        }
        ///    }
        ///    catch (ThreadAbortException e)
        ///    {
        ///        // Force abort during application shutdown.
        ///        // Incase the thread didn't stop for a long time
        ///    }
        ///}
        /// </code>
        /// 
        /// </summary>
        void Task();
    }
}
