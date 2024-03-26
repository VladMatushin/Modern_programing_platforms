using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Serialize
{
    public interface ITraceSerializer
    {
        string serialize(TraceResult traceResult);
    }
}
