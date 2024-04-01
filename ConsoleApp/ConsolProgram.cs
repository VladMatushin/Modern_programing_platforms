using ConsoleApp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;
using Tracer.Serialize.ClassSerialize;

namespace Consol
{
    public class ConsolProgram
    {
        public static ConsolFunc func = new ConsolFunc();
        public static ConcurrentDictionary<int, Thread> threads = new ConcurrentDictionary<int, Thread>();
        public static void Main()
        {
            var thread1 = new Thread(func.Func1);
            var thread2 = new Thread(func.Func2);

            threads.TryAdd(0, thread1);
            threads.TryAdd(1, thread2);

            threads[0].Start();
            threads[1].Start();

            threads[0].Join();
            threads[1].Join();

            func.Func3(3);
            func.Func1();
            func.Func2();
            var traceResult = func.tracer.GetTraceResult();

            xmlSerializer xmlSerializ = new xmlSerializer();
            string messageXml = xmlSerializ.serialize(traceResult);
            Console.WriteLine(messageXml);
            File.WriteAllText("trace.xml", messageXml);

            Console.WriteLine("_____________________________________________________________________________________________");

            JsonSerialize jsonSerializ = new JsonSerialize();
            string messageJson = jsonSerializ.serialize(traceResult);
            Console.WriteLine(messageJson);
            File.WriteAllText("trace.json", messageJson);
        }

      
    }
}
