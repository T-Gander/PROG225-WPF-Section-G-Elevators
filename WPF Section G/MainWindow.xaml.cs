using System;
using System.Collections.Generic;
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

namespace WPF_Section_G
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Elevator E1 = new LowElevator(lblE1, lblFloorE1, borderE1, gridE1);
            Elevator E2 = new MidElevator(lblE2, lblFloorE2, borderE2, gridE2);
            Elevator E3 = new PenthouseElevator(lblE3, lblFloorE3, borderE3, gridE3);
            Elevator E4 = new PenthouseElevator(lblE4, lblFloorE4, borderE4, gridE4);
            Elevator E5 = new MidElevator(lblE5, lblFloorE5, borderE5, gridE5);
            Elevator E6 = new LowElevator(lblE6, lblFloorE6, borderE6, gridE6);

            Grid.SetRow(E1.Border, Grid.GetRow(E1.Border) - 1);
            E1.Floor.Content = "Floor 1";
        }
    }
}
