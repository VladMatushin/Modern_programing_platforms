using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Serialize.ClassSerialize
{
    public class JsonSerialize : ITraceSerializer
    {
        public string serialize(TraceResult traceResult)
        {
            string result = "";
            result += "{\n    \"threads\": [\n";

            foreach (TracerThread thred in traceResult.Threads)
            {
                result = result.PadRight(result.Length + 8);
                result += "{\n";

                result += addThreads(thred);

                result = result.PadRight(result.Length + 8);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result += "    ]\n}";
            return result;
        }

        //PadRight(result.Length + 8); Добавление пробелов
        //TrimEnd(',', '\n') + "\n"; Удаление последней запятой

        //Добавления потока
        public string addThreads(TracerThread trace)
        {
            string result = "";

            result = result.PadRight(result.Length + 12);
            result += $"\"id\": \"{trace.threadId}\",\n";

            result = result.PadRight(result.Length + 12);
            result += $"\"time\": \"{trace.time}\",\n";

            result = result.PadRight(result.Length + 12);
            result += $"\"methods\": [\n";

            foreach (TracerNode method in trace.Methods)
            {
                result = result.PadRight(result.Length + 16);
                result += "{\n";

                result += addMethod(method, 20);

                result = result.PadRight(result.Length + 16);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result = result.PadRight(result.Length + 12);
            result += $"]\n";

            return result;
        }

        //Добавление метода
        public string addMethod(TracerNode method, int step)
        {
            string result = "";

            result = result.PadRight(result.Length + step);
            result += $"\"name\": \"{method.methodName}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"class\": \"{method.className}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"time\": \"{method.GetTime()}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"methods\": [\n";

            foreach (TracerNode child in method.TChilds)
            {
                result = result.PadRight(result.Length + step + 4);
                result += "{\n";

                result += addMethod(child, step + 8);

                result = result.PadRight(result.Length + step + 4);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result = result.PadRight(result.Length + step);
            result += "]\n";

            return result;
        }
    }
}
