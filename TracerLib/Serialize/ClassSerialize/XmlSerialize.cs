using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Serialize.ClassSerialize
{
    public class xmlSerializer : ITraceSerializer
    {
        public string serialize(TraceResult traceResult)
        {
            string result = "";
            result += "<root>\n";

            foreach (TracerThread thred in traceResult.Threads)
            {
                result += addThreads(thred);
            }

            result += "</root>";
            return result;
        }

        //PadRight(result.Length + 8); Добавление пробелов

        //Добавление потока
        public string addThreads(TracerThread trace)
        {
            string result = "";
            result = result.PadRight(result.Length + 4);
            result += $"<thread id=\"{trace.threadId}\" time=\"{trace.time}ms\">\n";
            foreach (TracerNode method in trace.Methods)
                result += addMethod(method, 8);

            result = result.PadRight(result.Length + 4);
            result += "</thread>\n";

            return result;
        }

        //Добавление метода
        public string addMethod(TracerNode method, int step)
        {
            string result = "";
            result = result.PadRight(result.Length + step);
            if (method.TChilds.Count != 0)
            {
                result += $"<method name=\"{method.methodName}\" time=\"{method.GetTime()}ms\" class=\"{method.className}\">\n";

                foreach (TracerNode child in method.TChilds)
                {
                    result += addMethod(child, step + 4);
                }

                result = result.PadRight(result.Length + step);
                result += "</method>\n";
            }
            else
            {
                result += $"<method name=\"{method.methodName}\" time=\"{method.GetTime()}ms\" class=\"{method.className}\"/>\n";
            }
            return result;
        }
    }
}
