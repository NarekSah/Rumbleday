using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rumbleday.Core
{
    public interface IRumbledayLogger 
    {
        public void Log(string message);

        public void LogError(Exception exception);
    }
}
