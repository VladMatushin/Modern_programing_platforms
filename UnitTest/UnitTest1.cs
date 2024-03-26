using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using Tracer;
using Consol;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        TracerThread TestTracerThread = new TracerThread(1);
        TracerNode TestMethod1 = new TracerNode(1, "MethodName1", "ClassName1");
        TracerNode TestMethod2 = new TracerNode(1, "MethodName2", "ClassName2");

        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
            TestTracerThread.addNode(TestMethod1);

            Thread.Sleep(100);
            TestTracerThread.addNode(TestMethod2);


            Thread.Sleep(200);
            TestTracerThread.stopNode();

            Thread.Sleep(100);
            TestTracerThread.stopNode();

            TestTracerThread.TimeCount();
        }


        [TestMethod]
        public void TestTimeCount()
        {
            TestTracerThread.time.Should().BeInRange(400, 500);
        }

        [TestMethod]
        public void TestGetResult()
        {
            TraceResult result = new TraceResult();
            result.addNode(TestMethod1);
            result.addNode(TestMethod2);
            result.Threads.Count.Should().Be(1);
        }
    }
}

