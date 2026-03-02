using iTextSharp.text.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Threads
{
    public class SimpleThreadable : IThreadable
    {
        private string NAME = System.Guid.NewGuid().ToString();
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);

        public delegate void ThreadedTask();
        private ThreadedTask methodToCall;

        public string GetName()
        {
            return NAME;
        }

        public int GetSleepTime()
        {
            return 0;
        }

        public System.Threading.AutoResetEvent GetWaitHandle()
        {
            return waitHandle;
        }

        public System.Threading.AutoResetEvent GetAbortHandle()
        {
            return abortHandle;
        }

        public void Task()
        {
            try
            {
                //logger.Debug(NAME + "...Thread started");
                methodToCall();
                //logger.Debug(NAME + "...Exiting thread");
            }
            catch (ThreadAbortException e)
            {
                //logger.Debug(NAME + "...Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        public void StartThread(ThreadedTask t)
        {
            methodToCall = t;
            ThreadHandler.GetInstance(this).StartThread();
        }

        public void StopThread()
        {
            ThreadHandler.GetInstance(this).StopThread();
        }
    }
}
