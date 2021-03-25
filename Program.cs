using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace ETWILoggerExample
{
    class Program
    {
        static void ParserDelegate(TraceEvent data) {
            switch (data.EventName)
            {
                case "FormattedMessage":
                    Console.WriteLine($"[FormattedMessage] {data.PayloadStringByName("LoggerName")} : {data.PayloadStringByName("FormattedMessage")}");
                    break;
                case "Message":
                    Console.WriteLine($"[Message] {data.PayloadStringByName("LoggerName")} : {data.PayloadStringByName("EventName")}");
                    break;
                // There are other types of events that are logged by ILogger but won't print them since
                // they're irrelevant (i.e. ActivityStart, ActivityEnd, etc.) or are dups 
                // (ex. "MessageJson" which is just a Json formatted string of "Message")
                // You can add more stuff here as needed.
                default:
                    break;
            }
        }

        static void TraceUsingFile(int traceDuration)
        {
            string tempFile = "MyEventData.etl";
            using (var session = new TraceEventSession("ILoggerSession", tempFile))
            {
                session.EnableProvider("Microsoft-Extensions-Logging", TraceEventLevel.Verbose, ulong.MaxValue, new TraceEventProviderOptions(new String[] { "FilterSpecs", "App*:Information;*" }));

                Thread.Sleep(traceDuration * 1000);
            }
            ParseFile(tempFile);
        }
        static void TraceRealTime(int traceDuration)
        {
            TraceEventSession session = new TraceEventSession("ILoggerSession");
            Task ParserTask = Task.Run(() => {
                session.Source.Dynamic.All += ParserDelegate;
                session.EnableProvider("Microsoft-Extensions-Logging", TraceEventLevel.Verbose, ulong.MaxValue, new TraceEventProviderOptions(new String[] { "FilterSpecs", "App*:Information;*" }));
                session.Source.Process();
            });

            Thread.Sleep(traceDuration * 1000);
            session.Stop();
        }

        static void ParseFile(string filePath)
        {
            using (var source = new ETWTraceEventSource(filePath)) {
                source.Dynamic.All += ParserDelegate;
                source.Process();
            }
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
