using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Worker : BackgroundService
    {
        private readonly IMessageWriter _messageWriter;

        public Worker(IMessageWriter messageWriter) =>
            _messageWriter = messageWriter;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _messageWriter.Write($"Worker running at: {DateTimeOffset.Now}");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
