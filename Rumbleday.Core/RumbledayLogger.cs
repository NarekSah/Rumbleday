using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rumbleday.Core
{
    public class RumbledayLogger : IRumbledayLogger
    {
        private readonly ILogger<RumbledayLogger> _logger;

        public RumbledayLogger(ILogger<RumbledayLogger> logger)
        {
            _logger = logger;
        }
        public void Log(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogError(Exception exception)
        {
            _logger.LogError(exception, "Տեղի ունեցավ սխալ։");
        }
    }
}
