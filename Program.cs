using System;
using System.Threading;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace ETWILoggerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: dotnet run [traceDurationInSec]");
                return;
            }

            int traceDuration = Int32.Parse(args[0]);
            using (var session = new TraceEventSession("ILoggerSession", "MyEventData.etl"))
            {
                session.EnableProvider("Microsoft-Extensions-Logging", TraceEventLevel.Verbose, ulong.MaxValue, new TraceEventProviderOptions(new String[] { "FilterSpecs", "App*:Information;*" }));

                Thread.Sleep(traceDuration * 1000);
            }
            using (var source = new ETWTraceEventSource("MyEventData.etl")) {
                source.Dynamic.All += delegate(TraceEvent data) {
                    Console.WriteLine("ILogger Event: {0}", data);
                };
                source.Process();
            }
        }
    }
}
