using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BlurWindowLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        List<MainWindow> windowList = new List<MainWindow>();
        protected override async void OnStartup(StartupEventArgs e)
        {
            var flag = false;

            while (true)
            {
                Debug.WriteLine(DateTime.Now);

                if (flag) BlurTopmostScreen.Stop();
                else BlurTopmostScreen.Start();

                flag = !flag;
                await Task.Delay(5000);
            }

        }
    }
}
