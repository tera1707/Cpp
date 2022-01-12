using Microsoft.Toolkit.Uwp.Notifications;
using MyUtilily;
using System.Diagnostics;
using System.Reflection;
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
            Assembly myAssembly = Assembly.GetEntryAssembly();
            string path = myAssembly.Location;
            var dir = Path.GetDirectoryName(path);

            try
            {
                // �������́A����exe���蓮�ŃT�[�r�X�Ɠ����t�H���_�ɃR�s�[�K�v�B
                CreateProcessAsUserOnCSharp.CreateProcessAsUser(dir + @"\BlurWindowLauncher.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                // �������́A����exe���蓮�ŃT�[�r�X�Ɠ����t�H���_�ɃR�s�[�K�v�B
                CreateProcessAsUserOnCSharp.CreateProcessAsUser(dir + @"\ToastLauncher.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}