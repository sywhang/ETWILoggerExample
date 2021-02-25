# ETWILoggerExample

This is just a simple sample code for enabling ILogger events using ETW.

## Instructions

1. Clone this repo and build. 

2. Launch an app that uses ILogger (i.e. ASP.NET Core MVC app)

3. Launch this app from an admin console and specify a duration you want to collect for.

For example, this collects ILogger logs for 10 seconds:

```
dotnet run 10
```

> NOTE: You *must* run as administrator, since you need admin privilege to start an ETW session.

4. You will see something like this:
```
ILogger Event: <Event MSec= "17240.5387" PID="36384" PName=        "" TID="31808" EventName="Message" ProviderName="Microsoft-Extensions-Logging" Level="1" FactoryID="1" LoggerName="Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker" EventId="1" EventName="ControllerFactoryExecuting" Exception="{ TypeName:&quot;&quot;, Message:&quot;&quot;, HResult:0, VerboseMessage:&quot;&quot; }" Arguments="[Controller-&gt;&quot;mvc.Controllers.HomeController&quot;,AssemblyName-&gt;&quot;mvc&quot;,{OriginalFormat}-&gt;&quot;Executing controller factory for controller {Controller} ({AssemblyName})&quot;]"/>
ILogger Event: <Event MSec= "17240.5478" PID="36384" PName=        "" TID="31808" EventName="MessageJson" ProviderName="Microsoft-Extensions-Logging" Level="1" FactoryID="1" LoggerName="Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker" EventId="1" EventName="ControllerFactoryExecuting" ExceptionJson="{}" ArgumentsJson="{&quot;Controller&quot;:&quot;mvc.Controllers.HomeController&quot;,&quot;AssemblyName&quot;:&quot;mvc&quot;,&quot;{OriginalFormat}&quot;:&quot;Executing controller factory for controller {Controller} ({AssemblyName})&quot;}" _FormattedMessage="Executing controller factory for controller mvc.Controllers.HomeController (mvc)"/>
ILogger Event: <Event MSec= "17240.5705" PID="36384" PName=        "" TID="31808" EventName="FormattedMessage" ProviderName="Microsoft-Extensions-Logging" Level="1" FactoryID="1" LoggerName="Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker" EventId="2" EventName="ControllerFactoryExecuted" _FormattedMessage="Executed controller factory for controller mvc.Controllers.HomeController (mvc)"/>
ILogger Event: <Event MSec= "17240.5758" PID="36384" PName=        "" TID="31808" EventName="Message" ProviderName="Microsoft-Extensions-Logging" Level="1" FactoryID="1" LoggerName="Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker" EventId="2" EventName="ControllerFactoryExecuted" Exception="{ TypeName:&quot;&quot;, Message:&quot;&quot;, HResult:0, VerboseMessage:&quot;&quot; }" Arguments="[Controller-&gt;&quot;mvc.Controllers.HomeController&quot;,AssemblyName-&gt;&quot;mvc&quot;,{OriginalFormat}-&gt;&quot;Executed controller factory for controller {Controller} ({AssemblyName})&quot;]"/>
ILogger Event: <Event MSec= "17240.5826" PID="36384" PName=        "" TID="31808" EventName="MessageJson" ProviderName="Microsoft-Extensions-Logging" Level="1" FactoryID="1" LoggerName="Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker" EventId="2" EventName="ControllerFactoryExecuted" ExceptionJson="{}" ArgumentsJson="{&quot;Controller&quot;:&quot;mvc.Controllers.HomeController&quot;,&quot;AssemblyName&quot;:&quot;mvc&quot;,&quot;{OriginalFormat}&quot;:&quot;Executed controller factory for controller {Controller} ({AssemblyName})&quot;}" _FormattedMessage="Executed controller factory for controller mvc.Controllers.HomeController (mvc)"/>
ILogger Event: <Event MSec= "17240.6084" PID="36384" PName=        "" TID="31808" EventName="FormattedMessage" ProviderName="Microsoft-Extensions-Logging" Level="1" FactoryID="1" LoggerName="Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine" EventId="2" EventName="ViewLookupCacheHit" _FormattedMessage="View lookup cache hit for view &apos;Privacy&apos; in controller &apos;Home&apos;."/>
```