using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App5.Controls
{
    public sealed partial class FlyoutableToggleButton : UserControl
    {
        private static int InstanceCount = 0;


        public MenuFlyout MyMenuFlyout
        {
            get { return (MenuFlyout)GetValue(MyMenuFlyoutProperty); }
            set { SetValue(MyMenuFlyoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyMenuFlyout.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyMenuFlyoutProperty =
            DependencyProperty.Register("MyMenuFlyout", typeof(MenuFlyout), typeof(FlyoutableToggleButton), new PropertyMetadata(null, new PropertyChangedCallback(ValueChanged)));



        //public object MyContent
        //{
        //    get { return (object)GetValue(MyContentProperty); }
        //    set { SetValue(MyContentProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyContent.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MyContentProperty =
        //    DependencyProperty.Register("MyContent", typeof(object), typeof(FlyoutableToggleButton), new PropertyMetadata(null, new PropertyChangedCallback(ContentChanged)));


        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(FlyoutableToggleButton), new PropertyMetadata(null, new PropertyChangedCallback(ContentChanged)));



        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(FlyoutableToggleButton), new PropertyMetadata(0, new PropertyChangedCallback(SelectedIndexChanged)));


        private List<RadioMenuFlyoutItem> flyoutItems = new List<RadioMenuFlyoutItem>();

        private int _flyoutCount = 0;

        public FlyoutableToggleButton()
        {
            this.InitializeComponent();

            InstanceCount++;
        }

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name + " old : " + e.OldValue + " new : " + e.NewValue);

            if (d is FlyoutableToggleButton ctrl)
            {
                //ctrl.コントロールの名前.Text = "あいうえお";

                var items = (ctrl.MyMenuFlyout).Items;
                ctrl._flyoutCount = items.Count;
                var radioMenuFlyouts = items.Select(x => (RadioMenuFlyoutItem)x);

                int ctr = 0;
                foreach (var f in items)
                {
                    if (f is RadioMenuFlyoutItem rmfi)
                    {
                        rmfi.GroupName = "GroupName" + InstanceCount;
                        rmfi.Name = "rmfi" + ctr.ToString();
                        rmfi.CommandParameter = ctr;
                        ctr++;
                        rmfi.Click += ctrl.Rmfi_Click;

                        ctrl.flyoutItems.Add(rmfi);
                    }
                }


                FlyoutBase.SetAttachedFlyout(ctrl.toggle, ctrl.MyMenuFlyout);
            }

        }
        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name + " old : " + e.OldValue + " new : " + e.NewValue);

            if (d is FlyoutableToggleButton ctrl)
            {

            }

        }

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name + " old : " + e.OldValue + " new : " + e.NewValue);

            if (d is FlyoutableToggleButton ctrl)
            {
                var index = (int)e.NewValue;
                //var index = ctrl.SelectedIndex;
                ctrl.flyoutItems[index].IsChecked = true;

                if (index < ctrl._flyoutCount - 1)
                {
                    ctrl.toggle.Background = new SolidColorBrush(Color.FromArgb(0xff, 0, 0, 0xff));
                }
                else
                {
                    ctrl.toggle.Background = new SolidColorBrush(Color.FromArgb(0xff, 0, 0, 0));
                }
            }

        }
        private void Rmfi_Click(object sender, RoutedEventArgs e)
        {
            var rmfi = ((RadioMenuFlyoutItem)sender);
            var name = rmfi.Name;
            var number = (int)rmfi.CommandParameter;

            if (number < _flyoutCount - 1)
            {
                toggle.Background = new SolidColorBrush(Color.FromArgb(0xff, 0, 0, 0xff));
            }
            else
            {
                toggle.Background = new SolidColorBrush(Color.FromArgb(0xff, 0, 0, 0));
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }


    public class FlyoutToggleButton : ToggleButton
    {
        protected override void OnToggle()
        {
            //base.OnToggle();
        }
    }
}
