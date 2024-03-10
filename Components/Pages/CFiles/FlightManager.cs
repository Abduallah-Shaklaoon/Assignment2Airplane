using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Airplane.Components.Pages.CFiles
{
    internal class FlightManager
    {
        private static List<Flight> flights = new List<Flight>();
        public static List<Flight> Flights { get { return flights; } }
        public static List<Airports> airport = new List<Airports>();
        public static List<Airports> Airport { get { return airport; } }
        public FlightManager()
        {
            airport = GenerateAirports();
            flights = GenerateFlights();
        }
        //Function that finds all flights from a specific origin ,destination and week day
        public static List<Flight> FindFlights(string origin,  string destination, string weekday)
        {
            List<Flight> foundFlights = new List<Flight>();

            foreach (Flight flight in flights)
            {
                if(flight.Origin  == origin && flight.Destination == destination && (flight.Weekday == weekday || weekday == "Any"))
                    {
                        foundFlights.Add(flight);
                    }
            }
            return foundFlights;

        }
        //Fills Flights list with all flights from the CSV file
        private List<Flight> GenerateFlights()
        {
            List<Flight> ReadFlights = new List<Flight>();
            string[] lineParts;
            foreach(string line in File.ReadLines("C:\\Users\\a_sha\\source\\repos\\Assignment2Airplane\\Resources\\Files\\flights.csv"))
            {
                lineParts = line.Split(",");
                try
                {
                    ReadFlights.Add(new Flight(lineParts[0], lineParts[1], lineParts[2], lineParts[3], lineParts[4], lineParts[5], int.Parse(lineParts[6]), double.Parse(lineParts[7])));
                }
                catch(Exception e) //InvalidFlightCodeException
                {
                    Console.WriteLine(e.Message);
                }
            }
            return ReadFlights;
        }
        private List<Airports> GenerateAirports()
        {
            List<Airports> ReadAirports = new List<Airports>();
            string[] lineParts;
            foreach (string line in File.ReadLines("C:\\Users\\a_sha\\source\\repos\\Assignment2Airplane\\Resources\\Files\\airports.csv"))
            {
                lineParts = line.Split(",");
                ReadAirports.Add(new Airports(lineParts[0], lineParts[1]));
            }
            return ReadAirports;
        }
        public string ReturnFullAirportName(string shortName)
        {
            foreach(Airports airport in airport)
            {
                if(airport.ShortName == shortName)
                {
                    return airport.AirportName;
                }
            }
            return null;
        }
    }
}
