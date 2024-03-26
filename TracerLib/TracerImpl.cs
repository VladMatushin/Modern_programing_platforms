using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TracerImpl : ITracer
    {
        public TraceResult traceResult = new TraceResult();

        public void StartTrace()
        { 
            //id
            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            var stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod();

            //Класс
            var callingClassName = callingMethod.DeclaringType.Name;

            //Метод
            var callingMethodName = callingMethod.Name;

            //Создание класса обрабатываемого метода
            var newNode = new TracerNode(threadId, callingMethodName, callingClassName);

            traceResult.addNode(newNode);
        }

        public void StopTrace()
        {
            //id
            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            traceResult.stopNode(threadId);
        }

        public TraceResult GetTraceResult()
        {
            traceResult.SetTreadTime();
            return traceResult;
        }
    }
}
