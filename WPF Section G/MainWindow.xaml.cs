using Section_G_Lab_Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Section_G
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ElevatorHeartbeat();
        public static event ElevatorHeartbeat Heartbeat;

        public MainWindow()
        {
            InitializeComponent();

            ElevatorDLL E1 = new LowElevatorDLL(lblE1, lblFloorE1, lblCapacityE1, borderE1, gridE1, 0, 0, 0, 0, 0, 0);
            ElevatorDLL E2 = new MidElevatorDLL(lblE2, lblFloorE2, lblCapacityE2, borderE2, gridE2, 0, 0, 0, 0, 0, 0);
            ElevatorDLL E3 = new PenthouseElevatorDLL(lblE3, lblFloorE3, lblCapacityE3, borderE3, gridE3, 0, 0, 0, 0, 0, 0);
            ElevatorDLL E4 = new PenthouseElevatorDLL(lblE4, lblFloorE4, lblCapacityE4, borderE4, gridE4, 0, 0, 0, 0, 0, 0);
            ElevatorDLL E5 = new MidElevatorDLL(lblE5, lblFloorE5, lblCapacityE5, borderE5, gridE5, 0, 0, 0, 0, 0, 0);
            ElevatorDLL E6 = new LowElevatorDLL(lblE6, lblFloorE6, lblCapacityE6, borderE6, gridE6, 0, 0, 0, 0, 0, 0);

            //Grid.SetRow(E1.Border, Grid.GetRow(E1.Border));
            //E1.Floor.Content = "Floor 1";

            Timer HeartbeatTimer = new Timer();
            HeartbeatTimer.Interval = 300;
            HeartbeatTimer.Enabled = true;
            HeartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;
        }

        private void HeartbeatTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if(Heartbeat != null)
            {
                Application.Current.Dispatcher.Invoke(Heartbeat);    //Had to use this way of invoking to use the UI thread in WPF.
            }
        }
    }
}
