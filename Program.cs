using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace ETWILoggerExample
{
    class Program
    {
        static void TraceUsingFile(int traceDuration)
        {
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
        static void TraceRealTime(int traceDuration)
        {
            TraceEventSession session = new TraceEventSession("ILoggerSession");
            Task ParserTask = Task.Run(() => {
                session.Source.Dynamic.All += delegate(TraceEvent data) {
                    Console.WriteLine("ILogger Event: {0}", data);
                };
                session.EnableProvider("Microsoft-Extensions-Logging", TraceEventLevel.Verbose, ulong.MaxValue, new TraceEventProviderOptions(new String[] { "FilterSpecs", "App*:Information;*" }));
                session.Source.Process();
            });

            Thread.Sleep(traceDuration * 1000);
            session.Stop();
        }
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet run [file|rt] [traceDurationInSec]");
                return;
            }
            string mode = args[0];
            int traceDuration = Int32.Parse(args[1]);

            if (mode == "file")
            {
                TraceUsingFile(traceDuration);
            }
            else if (mode == "rt")
            {
                TraceRealTime(traceDuration);
            }
            else
            {
                Console.WriteLine("Invalid mode. Use either file or rt mode");
            }

        }
    }
}
