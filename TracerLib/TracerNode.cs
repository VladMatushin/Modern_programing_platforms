using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TracerNode
    {
        public TracerNode TParent = null;

        public List<TracerNode> TChilds = new List<TracerNode>();

        public int threadId;
        public string className { get; set; }

        public string methodName { get; set; }
 
        public Stopwatch stopwatch = new Stopwatch();

        public bool isActive;

        public TracerNode(int threadId, string methodName, string className)
        {
            this.threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            this.className = className;
            this.methodName = methodName;
            isActive = true;
            stopwatch.Start();
        }

        public void AddNode (TracerNode node)
        {
            if ((TChilds.Count > 0) && (TChilds.Last().isActive == true))
            {
                TChilds.Last().AddNode(node);    
            }
            else
            {
                TChilds.Add(node);
                node.TParent = this;
            }
        }

        public void StopTimer()
        {
            if ((TChilds.Count > 0) && (TChilds.Last().isActive == true))
            {
                TChilds.Last().StopTimer();
            }
            else
            {
                isActive = false;
                stopwatch.Stop();
            }
        }

        public long GetTime()
        {
            return stopwatch.ElapsedMilliseconds;
        }

    }
}
