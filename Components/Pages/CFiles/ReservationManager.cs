using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Airplane.Components.Pages.CFiles
{
    /// <summary>
    /// Reservation manager holds all active and inactive reservations.
    /// It can return a list of all of them, and it can find reservations just off of the reservation code.
    /// It handles saving and reading reservations from a csv file 
    /// It also handles generating reservation codes and making sure they arent duplicates
    /// </summary>
    internal class ReservationManager
    {
        private static Random rand = new Random();
        public static readonly string PossibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string ResFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Files\\reservations.csv");
        private static List<Reservation> reservations = new List<Reservation>();
        public List<Reservation> Reservations { get {  return reservations; } }
        
        //When an instance of reservation manager is created it loads all reservations from the file
        public ReservationManager()
        {
            reservations = LoadReservations();
        }
        //Reads all reservations from the file
        public List<Reservation> LoadReservations()
        {
            List<Reservation> ReadReservations = new List<Reservation>();
            string[] resParts;

                foreach (string line in File.ReadLines(ResFilePath))
                {
                try
                {
                    resParts = line.Split(",");
                    if(resParts[0] == null || resParts[1] == null || resParts[2] == null || resParts[3] == null || resParts[4] == null || resParts[5] == null || resParts[6] == null)
                    {
                        throw new ArgumentNullException();
                    }
                    ReadReservations.Add(new Reservation(resParts[0], resParts[1], resParts[2], double.Parse(resParts[3]), resParts[4], resParts[5], resParts[6]));
                }
                catch (ArgumentNullException e)
                {

                }
            }

            return ReadReservations;
        }
        //Returns reservation with matching reservation code
        public Reservation GetResByCode(string code)
        {
            foreach(Reservation reservation in reservations)
            {
                if(reservation.ReservationCode == code) return reservation;
            }
            return null;
        }
        //returns reservations with matching fields
        public List<Reservation> GetReservations(string resCode, string airline, string name)
        {
            List<Reservation> foundReservations = new List<Reservation>();
            if (resCode == "ANY" && airline == "ANY" && name == "ANY")
            {
                return reservations;
            }
            foreach (Reservation reservation in reservations)
            {
                if (((reservation.ReservationCode).ToUpper() == resCode || resCode == "ANY") && ((reservation.Airline).ToUpper() == airline || airline == "ANY") && ((reservation.Name).ToUpper() == name || name == "ANY"))
                {
                    foundReservations.Add(reservation);
                }
               
            }

            
            return foundReservations;
        }
        //saves reservations to a file
        private void SaveReservations()
        {
            StreamWriter fileStream = File.CreateText(ResFilePath);

            foreach (Reservation reservation in Reservations)
            {
                fileStream.WriteLine(reservation.ToString());
            }
            fileStream.Close();
        }
        //updates a specific reservation with proper details
        public void ReservationUpdate(Reservation reservation,string name, string citizenship, string status)
        {
            if(name != null)
            {
                reservation.Name = name;
            }
            if(citizenship != null)
            {
                reservation.Citizenship = citizenship;
            }
            
            if (reservation.Status != status)
            {
                Flight resFlight = FlightManager.FindFlightCode(reservation.FlightCode);
                if (reservation.Status == "Active")
                {
                    reservation.Status = "Inactive";
                    resFlight.Seats = resFlight.Seats + 1;
                }
                else
                {
                    status = "Active";
                    resFlight.Seats = resFlight.Seats - 1;
                }
            }

            reservation.Status = status;
            SaveReservations();
        }
        //adds a reservation to the file and list of all active reservations in the ReservationManager instance
        public void AddReservation(Reservation reservation)
        {
            reservations.Add(reservation);
            SaveReservations();
        }
        //Generates a random code for a reservation
        public string GenerateReservationCode()
        {

            string reservationCode;
            char resCharacter;
            string resDigits;
            while(true)
            {
                resCharacter = PossibleChars[rand.Next(0, 25)];
                resDigits = rand.Next(1000, 10000).ToString();
                reservationCode = resCharacter + resDigits;
                if (IsUnique(reservationCode))
                {
                    break;
                }
            }
            return reservationCode;
        }
        //checks if a reservation code hasnt been used before.
        private bool IsUnique(string code)
        {
            foreach(Reservation reservation in reservations)
            {
                if(reservation.ReservationCode == code)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
