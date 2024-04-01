using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;

namespace ConsoleApp
{
    public class ConsolFunc
    {
        public readonly TracerImpl tracer = new TracerImpl();        
        public void Func1()
        {
            Console.WriteLine("Func1 start");
            tracer.StartTrace();

            Thread.Sleep(150);

            Console.WriteLine("Func1 stop");
            tracer.StopTrace();
        }

        public void Func2()
        {
            Console.WriteLine("Func2 start");
            tracer.StartTrace();

            Thread.Sleep(250);
            Func1();

            Console.WriteLine("Func2 stop");
            tracer.StopTrace();
        }

        public void Func3(int n)
        {
            tracer.StartTrace();

            Thread.Sleep(50);
            if (n == 1)
                Func1();
            if (n == 2)
                Func2();
            if (n != 0)
                Func3(--n);

            tracer.StopTrace();
        }
    }
}
