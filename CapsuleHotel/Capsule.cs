using System;
using System.Collections.Generic;
using System.Text;

namespace CapsuleHotel
{
    public class Capsule
    {
        public Guest Occupant { get; set; }
        public bool IsOccupied { get {
                return Occupant != null;
            }
            private set
            {
                if (Occupant == null)
                {
                    IsOccupied = false;
                }
                else
                {
                    IsOccupied = true;
                }
            } }

        public const decimal PER_NIGHT_PRICE = 33.33M;

        /// <summary>
        /// Sets the guest in this capsule if it is not occupied. Acts like 'push' to an array
        /// </summary>
        /// <param name="g">The guest to check in</param>
        /// <returns>true if checkin was successfull, false if the spot is occupied</returns>
        public bool CheckIn(Guest g)
        {
            //set if this capsule is open
            Occupant = g;
            return true;
        }

        /// <summary>
        /// If this capsule is occupied, removes the guest from it and sets the Occupant to null. Acts kind of like 'pop' from an array
        /// </summary>
        /// <returns>The Guest that was removed, or null if no Guest to remove</returns>
        public Guest CheckOut()
        {

            //validate occupied
            if (!IsOccupied)
            {
                Console.WriteLine("Oops! That capsule isn't occupied. Please enter another");
                ConsoleIO.AnyKeyToContinue();
                return null;
            }

            DateTime checkout = ConsoleIO.GetCheckoutDay();
            while (Occupant.CheckInTime.Day > checkout.Day || checkout.Day<0)
            {
                Console.WriteLine("Slow down there, Time Traveler. Please try again");
                checkout = ConsoleIO.GetCheckoutDay();
            }

            Guest holder = Occupant;
            Occupant = null;

            ConsoleIO.PrintTotalCost(holder, checkout, PER_NIGHT_PRICE);
            return holder; 
        }
    }
}
