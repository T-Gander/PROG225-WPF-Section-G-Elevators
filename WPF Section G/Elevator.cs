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
        public Border Border { get; set; }
        public Grid Grid { get; set; }
        public Label Name { get; set; }
        public Label Floor { get; set; }
        protected int ElevatorSpeed { get; set; }
        protected enum Floors { Ground, Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10, Penthouse };
        protected enum Doors { Open, Closing, Closed }

        protected Floors MAXFLOOR;
        protected Doors DoorState = Doors.Open;

        public Elevator(Label name, Label floorLabel, Border border, Grid grid) 
        {
            Border = border;
            Grid = grid;
            Name = name;
            Floor = floorLabel;
        }

        protected void FillElevator()
        {

        }

        protected void EmptyElevator()
        {

        }

        protected void MoveToDestinationFloor()
        {

        }

        protected void UpdateFloorLabel()
        {

        }
    }

    internal class LowElevator : Elevator
    {
        public LowElevator(Label name, Label floorLabel, Border border, Grid grid) : base(name, floorLabel, border, grid)
        {
            MAXFLOOR = Floors.Level3;
            ElevatorSpeed = 5;
        }
    }

    internal class MidElevator : Elevator
    {
        public MidElevator(Label name, Label floorLabel, Border border, Grid grid) : base(name, floorLabel, border, grid)
        {
            MAXFLOOR = Floors.Level6;
            ElevatorSpeed = 4;
        }
    }

    internal class PenthouseElevator : Elevator
    {
        public PenthouseElevator(Label name, Label floorLabel, Border border, Grid grid) : base(name, floorLabel, border, grid)
        {
            MAXFLOOR = Floors.Penthouse;
            ElevatorSpeed = 3;
        }
    }
}
