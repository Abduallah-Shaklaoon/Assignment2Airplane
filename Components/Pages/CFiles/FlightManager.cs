using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Airplane.Components.Pages.CFiles
{
    /// <summary>
    /// Flight Manager holds all flight objects and airport objects, deals with loading and saving the data for them from files.
    /// It also deals with finding flights by flight code whenever necessary to do so.
    /// </summary>
    internal class FlightManager
    {
        private static List<Flight> flights = new List<Flight>();
        public static List<Flight> Flights { get { return flights; } }
        public static List<Airports> airport = new List<Airports>();
        public static List<Airports> Airport { get { return airport; } }
        public static string FlightFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Files\\flights.csv");
        public static string AirportFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Files\\airports.csv");
        //Whan the instance of th eclass is created, it loads airports and flights
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
                if((flight.Origin == origin || origin == "Any") && (flight.Destination == destination || destination == "Any") && (flight.Weekday == weekday || weekday == "Any"))
                    {
                        foundFlights.Add(flight);
                    }
            }
            return foundFlights;

        }
        //Fills Flights list with all flights from the CSV file, catches any null values
        private List<Flight> GenerateFlights()
        {
            List<Flight> ReadFlights = new List<Flight>();
            string[] lineParts;
            foreach(string line in File.ReadLines(FlightFilePath))
            {
                lineParts = line.Split(",");
                try
                {
                    if (lineParts[0] == null || lineParts[1] == null || lineParts[2] == null || lineParts[3] == null || lineParts[4] == null || lineParts[5] == null || lineParts[6] == null || lineParts[7] == null)
                    {
                        throw new ArgumentNullException();
                    }
                    ReadFlights.Add(new Flight(lineParts[0], lineParts[1], lineParts[2], lineParts[3], lineParts[4], lineParts[5], int.Parse(lineParts[6]), double.Parse(lineParts[7])));
                }
                catch(ArgumentNullException e) //Exception if any of the parts of the flight are null
                {
                }
            }
            return ReadFlights;
        }
        //Saves flights to the file
        public void SaveFlights()
        {
            StreamWriter fileStream = File.CreateText(FlightFilePath);

            foreach (Flight flight in Flights)
            {
                fileStream.WriteLine(flight.ToString().Trim());
            }

            fileStream.Close();
        }
        //Loads all airports from the file
        private List<Airports> GenerateAirports()
        {
            List<Airports> ReadAirports = new List<Airports>();
            string[] lineParts;
            foreach (string line in File.ReadLines(AirportFilePath))
            {
                lineParts = line.Split(",");
                ReadAirports.Add(new Airports(lineParts[0], lineParts[1]));
            }
            return ReadAirports;
        }
        //Finds flight by code
        public static Flight FindFlightCode(string flightCode)
        {
            foreach(Flight flight in flights)
            {
                if (flight.FlightCode == flightCode)
                { 
                    return flight; 
                }

            }
            return null;
        }
    }
}
