using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Airplane.Components.Pages.CFiles
{
    /// <summary>
    /// Airports is an instance of each airport, the instance has the shorthand and full name of the airport
    /// has a ToString overried to show short name and long name
    /// </summary>
    internal class Airports
    {
        private string shortName;
        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }
        private string airportName;
        public string AirportName
        {
            get { return airportName; }
            set { airportName = value; }
        }
        public Airports(string shortName, string airportName)
        {
            ShortName = shortName;
            AirportName = airportName;
        }
        public override string ToString()
        {
            return $"Short name: {shortName} Long name: {AirportName}";
        }
    }
}
