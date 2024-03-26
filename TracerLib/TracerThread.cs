using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TracerThread
    {
        public int threadId { get; }

        public long time;

        public List<TracerNode> Methods = new List<TracerNode>();

        public TracerThread() 
        { 
            threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }
        public TracerThread(int threadId)
        {
            this.threadId = threadId;
        }

        public void addNode (TracerNode node)
        {

            if ((Methods.Count > 0) && (Methods.Last().isActive == true))
            {
                Methods.Last().AddNode(node);
            }
            else
            {
                Methods.Add(node);
            }
        }

        public void stopNode()
        {
            Methods.Last().StopTimer();
        }

        public void TimeCount()
        {
            long time = 0;
            foreach (TracerNode node in Methods)
            {
                time += node.GetTime();
            }
            this.time = time;
        }
    }
}
