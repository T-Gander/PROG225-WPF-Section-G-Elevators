using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Section_G
{
    internal class Elevator
    {
        private Border Border { get; set; }
        private Grid Grid { get; set; }
        private Label Name { get; set; }
        private Label Floor { get; set; }

        public Elevator(Border border, Grid grid, Label name, Label floorLabel) 
        {
            Border = border;
            Grid = grid;
            Name = name;
            Floor = floorLabel;
        }
    }
}
