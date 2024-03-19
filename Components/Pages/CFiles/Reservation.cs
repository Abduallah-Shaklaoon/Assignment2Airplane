using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Airplane.Components.Pages.CFiles
{
    /// <summary>
    /// Reservation is the class that descibes each reservation object, holds the data that correlates to each reservation
    /// Has a ToString override to display each item of the reservation
    /// </summary>
    internal class Reservation
    {
        private string reservationCode;
        public string ReservationCode 
        {
            get {  return reservationCode; } 
            set {  reservationCode = value; } 
        }
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
        private double cost;
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string citizenship;
        public string Citizenship
        {
            get { return citizenship; }
            set { citizenship = value; }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public Reservation(string reservationCode, string flightCode, string airline, double cost, string name, string citizenship,string status)
        {
            ReservationCode = reservationCode;
            FlightCode = flightCode;
            Airline = airline;
            Cost = cost;
            Name = name;
            Citizenship = citizenship;
            Status = status;
        }
        public override string ToString()
        {
            return $"{ReservationCode},{FlightCode},{Airline},{Cost},{Name},{Citizenship},{Status}";
        }
    }
}
