using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Airplane.Components.Pages.CFiles
{
    internal class ReservationManager
    {
        public static Random rand = new Random();
        public static readonly string PossibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static List<Reservation> reservations = new List<Reservation>();
        public List<Reservation> Reservations { get {  return reservations; } }
        public ReservationManager()
        {
            reservations = GenerateReservations();
        }
        public List<Reservation> GenerateReservations()
        {
            List<Reservation> ReadReservations = new List<Reservation>();
            string[] resParts;
            foreach(string line in File.ReadLines("reservation.csv"))
            {
                resParts = line.Split(",");
                ReadReservations.Add(new Reservation(resParts[0], resParts[1], resParts[2], double.Parse(resParts[3]), resParts[4], resParts[5], bool.Parse(resParts[6])));
            }
            return ReadReservations;
        }

        public List<Reservation> FindReservation(string resCode, string airline, string name)
        {
            List<Reservation> foundReservations = new List<Reservation>();
            if (resCode == null && airline == null && name == null)
            {
                return reservations;
            }
            foreach (Reservation reservation in reservations)
            {
                if (reservation.ReservationCode == resCode && reservation.Airline == airline && reservation.Name == name)
                {
                    foundReservations.Add(reservation);
                }
                else if (reservation.ReservationCode == resCode && reservation.Airline == airline)
                {
                    foundReservations.Add(reservation);
                }
                else if (reservation.ReservationCode == resCode && reservation.Name == name)
                {
                    foundReservations.Add(reservation);
                }
                else if (reservation.Airline == airline && reservation.Name == name)
                {
                    foundReservations.Add(reservation);
                }
                else if (reservation.Name == name)
                {
                    foundReservations.Add(reservation);
                }
                else if (reservation.ReservationCode == resCode)
                {
                    foundReservations.Add(reservation);
                }
                else if (reservation.Airline == airline)
                {
                    foundReservations.Add(reservation);
                }
            }

            
            return foundReservations;
        }
        public void SaveReservations()
        {
            StreamWriter fileStream = File.CreateText("reservations.csv");

            foreach (Reservation reservation in Reservations)
            {
                fileStream.WriteLine(reservation.ToString());
            }

            fileStream.Close();
        }
        public void UpdateReservation(Reservation resToUpdate,string name, string citizenship, bool status)
        {
            if(name != null)
            {
                resToUpdate.Name = name;
            }
            if(citizenship != null)
            {
                resToUpdate.Citizenship = citizenship;
            }
            resToUpdate.Status = status;
            SaveReservations();
        }
        public void AddReservation(Reservation reservation)
        {
            reservations.Add(reservation);
            SaveReservations();
        }
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
        public bool IsUnique(string code)
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
