**NOTE** i had to fix something in the NLog package for the wrapper situation to work; when i get a chance I'll double check they've not fixed it and/or pull request the fix

# C# Core Utilities
The core utilities you always tote around from one project to another
Simple, easy to use, explicit libraries that are adaptable, fexible, and sassy


## Settings Helper
  At its most explicit, specify a "type" of setting, the setting name, and a default value to return if the setting retrieval fails (think about production deployments that fail b/c a app.config setting didn't make it to the LIVE server)

### Usage
```csharp
  DefaultLogLevel = SettingsHelper.Get(SettingsHelper.SettingsType.System, "Logging:DefaultLevel", LoggingEventType.Info);
```

## Logger
Instead of Debug.WriteLine() for your tracing, how about Trace() 
Convenience methods for using a powerful logging package like NLog
Use Log(..) for a custom experience, or straight up Warn("text"); usage
    
### Levels 
Trace => Debug => Info => Warn => Error => Fatal
    
### Logger.cs
This is the central wire-up for the logger package you're using; here we use NLog
You could change just one method to use Log4NET or some other logger

### Usage
#### Statically
```csharp
      public static class SettingsHelper
      {
          private static readonly ILogger Logger = new Logger(typeof(SettingsHelper));
  
          public static T Get<T>(string key)
          {
              Logger.Trace(string.Format("Retrieving a key called {0}", key));
```
#### Object Instance
```csharp
      public class YourObj
      {
          ILogger specificTypeLogger = new Logger(GetType());
          specificTypeLogger.Log("Specific Type: Logger.Log() entry via extension, default log level");
          specificTypeLogger.Trace("a trace message via extension method - which is likely stripped due to logging config level default");
      }
```
