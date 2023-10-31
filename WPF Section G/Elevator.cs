using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        protected int WarmupTime { get; set; }
        protected enum Floors { Ground, Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10, Penthouse };
        protected enum Doors { Open, Opening, Closing, Closed }

        protected Floors MAXFLOOR;
        protected Floors Location;
        protected Floors Destination;
        protected Doors DoorState = Doors.Open;
        
        const int MAXCAPACITY = 10;

        protected int currentWaitTime = 0;
        protected int occupants = 0;
        protected int occupantsWaiting = 0;

        protected bool loadingOccupants = false;

        internal ElevatorDLL(Label name, Label floorLabel, Label capacityLabel, Border border, Grid grid, int x, int y, int lobby, int penthouse, int distance, int interval) : base(x,y, lobby, penthouse, distance, interval)
        {
            Border = border;
            Grid = grid;
            Name = name;
            Floor = floorLabel;
            Location = Floors.Ground;
            Capacity = capacityLabel;
            MainWindow.Heartbeat += CheckState;
            MainWindow.Heartbeat += UpdateFloorLabel;
            MainWindow.Heartbeat += UpdateCapacity;
            MainWindow.Heartbeat += UpdateVisualDoors;
        }

        protected void FillElevator()
        {
            occupantsWaiting--;
            occupants++;
        }

        protected void UpdateVisualDoors()
        {
            double maxMargin = 150;

            double newMargin;

            switch (DoorState)
            {
                case Doors.Opening:
                    newMargin = maxMargin - ((double)currentWaitTime / (double)WarmupTime * maxMargin);
                    Grid.Margin = new Thickness(newMargin, 0, newMargin, 0);
                    break;

                case Doors.Closing:
                    newMargin = ((double)currentWaitTime / (double)WarmupTime * maxMargin);
                    Grid.Margin = new Thickness(newMargin, 0, newMargin, 0);
                    break;
            }
        }

        protected void EmptyElevator()
        {
            Random rnd = new Random();
            
            occupants -= rnd.Next(4);

            if(occupants < 0) occupants = 0;
        }

        protected void UpdateVisualLocation(int direction)
        {
            Grid.SetRow(Border, Grid.GetRow(Border) - direction);
        }

        protected void CheckState()
        {
            //Check doors are open or closed

            //If doors are open

            //Check current occupants against waiting occupants

            //If waiting occupants is 0, close doors.

            //Once doors are closed, Get assigned a floor to drop occupants at.

            //If elevator isn't at the destination floor, move to the next floor

            //If elevator is at destination, begin opening doors.

            //once doors are open, drop occupants

            switch (DoorState)
            {
                case Doors.Open:
                    if (loadingOccupants)
                    {
                        if(occupants != MAXCAPACITY && occupantsWaiting > 0)
                        {
                            FillElevator();
                        }
                        else
                        {
                            loadingOccupants = false;
                            DoorState = Doors.Closing;
                        }
                    }
                    else
                    {
                        if (occupants > 0) EmptyElevator();
                        else
                        {
                            if (currentWaitTime != WarmupTime) 
                            { 
                                currentWaitTime++;
                                break;
                            }
                            
                            currentWaitTime = 0;
                            occupants = SetRandomOccupantsWaiting();
                            Destination = GenerateRandomFloor();
                            loadingOccupants = true;
                        }
                    }
                    //Check if unloading or loading, then check capacity.
                    //Once the elevator is either full, or empty. Then begin unloading or loading elevator.
                    //Then start closing doors
                    break;

                case Doors.Closing:
                    if (currentWaitTime != WarmupTime)
                    {
                        //UpdateVisualDoors();
                        currentWaitTime++;
                    }
                    else
                    {
                        DoorState = Doors.Closed;
                        currentWaitTime = 0;
                    }
                    break;

                case Doors.Closed:
                    if (Location == Destination) DoorState = Doors.Opening;
                    else MoveToDestinationFloor();
                    break;

                case Doors.Opening:
                    if (currentWaitTime != WarmupTime)
                    {
                        //UpdateVisualDoors();
                        currentWaitTime++;
                    }
                    else
                    {
                        DoorState = Doors.Open;
                        currentWaitTime = 0;
                    }
                    //Once doors are starting to open, keep opening.
                    //Once doors are completely open, start emptying elevator setting unloading to true.
                    break;
            }
        }

        protected void MoveToDestinationFloor()
        {
            if (Destination < Location)
            {
                Location--;
                UpdateVisualLocation(-1);
            }
            else if (Destination > Location)
            {
                Location++;
                UpdateVisualLocation(1);
            }
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

        protected int SetRandomOccupantsWaiting()
        {
            Random rWaiting = new Random();

            if(Location != Floors.Ground)
            {
                Random coinFlip = new Random();

                int flip = coinFlip.Next(3);

                if (flip <= 1)
                {
                    return 0;
                }
            }
            
            return rWaiting.Next(1, MAXCAPACITY+1); ;
        }

        protected Floors GenerateRandomFloor()
        {
            if(Location == Floors.Ground)
            {
                Random rnd = new Random();
                return (Floors)rnd.Next(1,(int)MAXFLOOR + 1);
            }
            else
            {
                return Floors.Ground;
            }
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
            WarmupTime = 50;
        }
    }

    internal class MidElevatorDLL : ElevatorDLL
    {
        public MidElevatorDLL(Label name, Label floorLabel, Label capacityLabel, Border border, Grid grid, int x, int y, int lobby, int penthouse, int distance, int interval) : base(name, floorLabel, capacityLabel, border, grid, x, y, lobby, penthouse, distance, interval)
        {
            MAXFLOOR = Floors.Level6;
            WarmupTime = 40;
        }
    }

    internal class PenthouseElevatorDLL : ElevatorDLL
    {
        public PenthouseElevatorDLL(Label name, Label floorLabel, Label capacityLabel, Border border, Grid grid, int x, int y, int lobby, int penthouse, int distance, int interval) : base(name, floorLabel, capacityLabel, border, grid, x, y, lobby, penthouse, distance, interval)
        {
            MAXFLOOR = Floors.Penthouse;
            WarmupTime = 30;
        }
    }
}
