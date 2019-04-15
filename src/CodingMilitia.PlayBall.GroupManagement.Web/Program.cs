using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;

namespace CodingMilitia.PlayBall.GroupManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureNLog();
            CreateWebHostBuilder(args).Build().Run();
        }

        // TODO: replace with nlog.config
        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget("coloredConsole")
            {
                Layout = @"${date: format=HH\:mm\:ss} ${logger} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);
            //var fileTarget = new FileTarget("file")
            //{
            //    FileName = @"${basedir}\file.log",
            //    Layout = @"${date: format=HH\:mm\:ss} ${level} ${message} ${exception} ${ndlc}"
            //};
            //config.AddTarget(fileTarget);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Info, consoleTarget, "CodingMilitia.*");
            config.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, consoleTarget);
            //config.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(builder => {
                    builder.ClearProviders(); // clear default logging providers
                    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog() // use NLog logging
                .UseStartup<Startup>();
    }
}
