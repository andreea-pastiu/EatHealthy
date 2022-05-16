using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy
{
    public static class Logger
    {
        private static readonly ILogger _errorLogger;

        static Logger()
        {
            _errorLogger = new LoggerConfiguration()
            .WriteTo.File(HttpContext.Current.Server.MapPath("~/logs/log-.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }

        public static void LogInformation(string information)
        {
            _errorLogger.Information(information);
        }
    }
}