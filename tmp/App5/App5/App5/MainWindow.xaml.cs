using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

namespace App5
{
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public int SelectedIndex { 
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value; OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        private int _selectedIndex = 0;

        public MainWindow()
        {
            this.InitializeComponent();

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex >= 3)
            {
                SelectedIndex = 0;
            }
            else
            {
                SelectedIndex++;
            }
        }
    }
}
