using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Airplane.Components.Pages.CFiles
{
    internal class Flight
    {
        private string flightCode;
        public string FlightCode
        {
            get { return flightCode; }
            set { flightCode = value; }
        }

        private string airline;
        public string Airline
        {
            get { return airline; }
            set { airline = value; }
        }
        private string origin;
        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        private string destination;
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        private string weekday;
        public string Weekday
        {
            get { return weekday; }
            set { weekday = value; }
        }
        private string time;
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        private int seats;
        public int Seats
        {
            get { return seats; }
            set { seats = value; }
        }
        private double seatCost;
        public double SeatCost
        {
            get { return seatCost; }
            set { seatCost = value; }
        }
        public Flight(string flightCode, string airline, string origin, string destination, string weekday, string time, int seats, double seatCost)
        {
            this.flightCode = flightCode;
            this.airline = airline;
            this.origin = origin;
            this.destination = destination;
            this.weekday = weekday;
            this.time = time;
            this.seats = seats;
            this.seatCost = seatCost;
        }
    }
}
