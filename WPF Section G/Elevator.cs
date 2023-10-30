using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Linq;
using WPF_Section_G;

namespace Section_G_Lab_Elevator
{
    internal class ElevatorDLL : Section_G_Lab_Elevator.Elevator
    {
        public Border Border { get; set; }
        public Grid Grid { get; set; }
        public Label Name { get; set; }
        public Label Floor { get; set; }
        public Label Capacity { get; set; }
        protected int ElevatorSpeed { get; set; }
        protected enum Floors { Ground, Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10, Penthouse };
        protected enum Doors { Open, Closing, Closed }

        protected Floors MAXFLOOR;
        protected Floors Location;
        protected Floors Destination;
        protected Doors DoorState = Doors.Open;
        protected bool hasDestination = false;
        const int MAXCAPACITY = 10;
        protected int occupants = 0;
        protected int occupantsWaiting = 0;
        protected bool loadingOccupants = false;
        protected bool unLoadingOccupants = false;
        protected int waitTimeForNextDestination = 1000;

        internal ElevatorDLL(Label name, Label floorLabel, Label capacityLabel, Border border, Grid grid, int x, int y, int lobby, int penthouse, int distance, int interval) : base(x,y, lobby, penthouse, distance, interval)
        {
            Border = border;
            Grid = grid;
            Name = name;
            Floor = floorLabel;
            Location = Floors.Ground;
            MainWindow.Heartbeat += CheckState;
            MainWindow.Heartbeat += UpdateFloorLabel;
            MainWindow.Heartbeat += UpdateCapacity;
        }

        protected void FillElevator()
        {

        }

        protected void EmptyElevator()
        {

        }

        protected void CheckState()
        {
            if (loadingOccupants)
            {
                FillElevator();
            }
            else if (Location != Destination)
            {
                MoveToDestinationFloor();
            }
            else if (unLoadingOccupants)
            {
                if (occupants != 0) EmptyElevator();
                else unLoadingOccupants = false;
            }
        }

        protected void MoveToDestinationFloor()
        {
            if (Destination < Location) Location--;
            else if (Destination > Location) Location++;
            else unLoadingOccupants = true;
        }

        protected void UpdateFloorLabel()
        {
            switch (Location)
            {
                case Floors.Ground:
                    Floor.Content = Floors.Ground.ToString(); break;

                case Floors.Level1:
                    Floor.Content = Floors.Level1.ToString(); break;

                case Floors.Level2:
                    Floor.Content = Floors.Level2.ToString(); break;

                case Floors.Level3:
                    Floor.Content = Floors.Level3.ToString(); break;

                case Floors.Level4:
                    Floor.Content = Floors.Level4.ToString(); break;

                case Floors.Level5:
                    Floor.Content = Floors.Level5.ToString(); break;

                case Floors.Level6:
                    Floor.Content = Floors.Level6.ToString(); break;

                case Floors.Level7:
                    Floor.Content = Floors.Level7.ToString(); break;

                case Floors.Level8:
                    Floor.Content = Floors.Level8.ToString(); break;

                case Floors.Level9:
                    Floor.Content = Floors.Level9.ToString(); break;

                case Floors.Level10:
                    Floor.Content = Floors.Level10.ToString(); break;

                case Floors.Penthouse:
                    Floor.Content = Floors.Penthouse.ToString(); break;
            }
        }

        protected int SetOccupantsWaiting()
        {
            Random rWaiting = new Random();
            occupantsWaiting = rWaiting.Next(MAXCAPACITY);
            if (occupantsWaiting == 0) loadingOccupants = false;
            return occupantsWaiting;
        }

        protected void AddOccupants()
        {
            int peopleWaiting = 0;

            Random rWaiting = new Random();

            rWaiting.Next(MAXCAPACITY);
        }

        protected Floors GenerateRandomFloor()
        {
            Random rnd = new Random();

            return (Floors)rnd.Next(11);
        }

        protected void UpdateCapacity()
        {
            Capacity.Content = $"Capacity: {occupants}/{MAXCAPACITY}";
        }
    }

    internal class LowElevatorDLL : ElevatorDLL
    {
        public LowElevatorDLL(Label name, Label floorLabel, Label capacityLabel, Border border, Grid grid, int x, int y, int lobby, int penthouse, int distance, int interval) : base(name, floorLabel, capacityLabel, border, grid, x, y, lobby, penthouse, distance, interval)
        {
            MAXFLOOR = Floors.Level3;
            ElevatorSpeed = 5;
        }
    }

    internal class MidElevatorDLL : ElevatorDLL
    {
        public MidElevatorDLL(Label name, Label floorLabel, Label capacityLabel, Border border, Grid grid, int x, int y, int lobby, int penthouse, int distance, int interval) : base(name, floorLabel, capacityLabel, border, grid, x, y, lobby, penthouse, distance, interval)
        {
            MAXFLOOR = Floors.Level6;
            ElevatorSpeed = 4;
        }
    }

    internal class PenthouseElevatorDLL : ElevatorDLL
    {
        public PenthouseElevatorDLL(Label name, Label floorLabel, Label capacityLabel, Border border, Grid grid, int x, int y, int lobby, int penthouse, int distance, int interval) : base(name, floorLabel, capacityLabel, border, grid, x, y, lobby, penthouse, distance, interval)
        {
            MAXFLOOR = Floors.Penthouse;
            ElevatorSpeed = 3;
        }
    }
}
