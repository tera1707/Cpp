using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlurWindowLauncher
{
    internal static class BlurTopmostScreen
    {
        private static List<MainWindow> windowList = new List<MainWindow>();

        public static void Start()
        {
            windowList.Clear();
            Screen.AllScreens.ToList().ForEach(x => windowList.Add(new MainWindow()));

            int i = 0;
            foreach (var w in Screen.AllScreens)
            {
                var window = windowList[i];
                window.Top = w.Bounds.Y;
                window.Left = w.Bounds.X;
                window.Width = w.WorkingArea.Width;
                window.Height = w.WorkingArea.Height;
                window.Show();
                i++;
            }
        }

        public static void Stop()
        {
            foreach (var w in windowList)
            {
                w.Hide();
            }
            windowList.Clear();
        }
    }
}
