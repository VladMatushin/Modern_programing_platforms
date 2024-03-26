using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        public List<TracerThread> Threads;


        public TraceResult()
        {
            this.Threads = new List<TracerThread>();
        }

        public void addNode(TracerNode node)
        {
            bool flag = true;
            foreach (TracerThread thread in Threads)
            {
                if (node.threadId == thread.threadId)
                {
                    thread.addNode (node);
                    flag = false;
                }
            }
            if (flag == true)
            {
                var thread = new TracerThread(node.threadId);
                Threads.Add(thread);
                thread.addNode(node);
            }
        }

        public void stopNode(int threadId) 
        {
            var stopThread = Threads.FirstOrDefault(t => t.threadId == threadId);
            stopThread.stopNode();
        }

        public void SetTreadTime()
        {
            foreach(TracerThread thread in Threads)
            {
                thread.TimeCount();
            }
        }
    }
}
