using MyUtilily;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CreateProcessAsUserOnCSharp.CreateProcessAsUser(@"C:\Windows\System32\notepad.exe C:\git\Cpp\UserProcessFromServiceJikken\WorkerService1\bin\Debug\aakill.bat");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}