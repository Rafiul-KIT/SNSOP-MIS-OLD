using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Threads
{
    public class ThreadHandler
    {
        private static Dictionary<string, ThreadHandler> threads = new Dictionary<string, ThreadHandler>();
        private static Object handlerListLock = new Object();
        private static volatile bool _shutdown = false;
        private Thread thread = null;
        private Object handlerLock = null;
        private AutoResetEvent waitHandle = null;
        private AutoResetEvent abortHandle = null;
        private ThreadStart task = null;
        private int MAX_WAIT_TIME = 5 * 1000; // 5 seconds

        public static ThreadHandler GetInstance(IThreadable threadableObject)
        {
            lock (handlerListLock)
            {
                // It's better not to return null if shutdown already initiated
                // because most callers may not handle null, and that can result in an Exception.
                // The strategy is to return a new handler to the caller, but not let them start
                // a new thread

                string componentName = threadableObject.GetName();
                if (!threads.ContainsKey(componentName))
                {
                    ThreadHandler handler = new ThreadHandler();
                    handler.task = threadableObject.Task;
                    handler.waitHandle = threadableObject.GetWaitHandle();
                    handler.abortHandle = threadableObject.GetAbortHandle();
                    threads.Add(componentName, handler);
                }
                return threads[componentName];
            }
        }

        public ThreadHandler()
        {
            handlerLock = new Object();
        }

        /// <summary>
        /// Start/wake thread
        /// </summary>
        public void StartThread()
        {
            if (_shutdown) return;

            lock (handlerLock)
            {
                if (thread == null || !thread.IsAlive)
                {
                    thread = new Thread(task);
                    thread.Start();
                }
                else if (thread.IsAlive && waitHandle != null)
                {
                    waitHandle.Set();
                }
            }
        }


        /// <summary>
        /// Tries to stop the thread when it reaches a sleep state.
        /// </summary>
        public void StopThread()
        {
            if (_shutdown) return; // Since shutdown initiated, that will stop all threads

            lock (handlerLock)
            {
                if (thread != null && thread.IsAlive)
                {
                    abortHandle.Set();
                    waitHandle.Set();
                    thread.Join();
                }
            }
        }

        /// <summary>
        /// Call only when shutting down application.
        /// Tries to gracefully stop all threads, but if times out, then
        /// force stops those threads.
        /// </summary>
        public static void Shutdown()
        {
            lock (handlerListLock)
            {
                if (_shutdown) return;
                _shutdown = true;

                // Attempt to stop all running threads
                foreach (KeyValuePair<string, ThreadHandler> pair in threads)
                {
                    ThreadHandler handler = pair.Value;
                    lock (handler.handlerLock)
                    {
                        if (handler.thread != null && handler.thread.IsAlive)
                        {
                            handler.abortHandle.Set();
                            handler.waitHandle.Set();
                        }
                    }
                }

                // Wait for threads to stop
                foreach (KeyValuePair<string, ThreadHandler> pair in threads)
                {
                    ThreadHandler handler = pair.Value;
                    lock (handler.handlerLock)
                    {
                        if (handler.thread != null && !handler.thread.Join(handler.MAX_WAIT_TIME))
                        {
                            // Graceful stop failed. So do force stop.
                            handler.thread.Abort();
                            handler.thread.Join();
                        }
                    }
                }
            }
        }
    }
}
