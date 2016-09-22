using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using coalesce.UI.MainWindow;
using CoalesceInputPlugin;

namespace coalesce
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CoreSolids.UpdateInputPlugs();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).Init();
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(250),
                IsEnabled = true,
            };

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private MainWindowViewModel vm = null;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (vm == null)
            {
                if (DataContext is MainWindowViewModel)
                {
                    vm = this.DataContext as MainWindowViewModel;
                }
            }

            if (vm == null)
            {
                return;
            }

            CurrentSensorPositionX.Text = vm.CurrentSensorX.ToUIString();
            CurrentSensorPositionY.Text = vm.CurrentSensorY.ToUIString();
            CurrentSensorPositionZ.Text = vm.CurrentSensorZ.ToUIString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).AddInput();
            this.UpdateLayout();
            ActiveInputs.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var doop = ((Button)sender).DataContext;

            InputList woop = (InputList)doop;

            woop.Plugin.ShowMonitor();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var doop = ((Button)sender).DataContext;

            InputList woop = (InputList)doop;

            var t = woop.Plugin.GetDetails();

            string about = $@"Pluging: {woop.Name}
Author: {t.Author}
Support: {t.SupportLink}
Version: {t.Version}";

            MessageBox.Show(this, about);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string selectedDestJoin = ((RadioButton)sender).Content.ToString();
            Debug.WriteLine(selectedDestJoin);
            string t = selectedDestJoin.Replace("(", "_").Replace(")", "");

            DestinationJoints petType = (DestinationJoints)Enum.Parse(typeof(DestinationJoints), t);
            
            ((MainWindowViewModel)this.DataContext).Assign(petType);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).BindPosition();
        }
        
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Grid grid = button.Parent as Grid;
            InputList dc = grid.DataContext as InputList;
            Debug.WriteLine(dc.Name);
            ((MainWindowViewModel)this.DataContext).AddInput(dc);
            this.UpdateLayout();
            ((MainWindowViewModel)this.DataContext).AddedInputPlugins = ((MainWindowViewModel)this.DataContext).AddedInputPlugins;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
        }
    }
}
