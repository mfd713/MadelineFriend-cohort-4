using System;
using System.Collections.Generic;
using System.Text;

namespace CapsuleHotel
{
    public class Capsule
    {
        public Guest Occupant { get; set; }
        public bool isOccupied { get {
                return Occupant != null;
            }
            private set
            {
                if (Occupant == null)
                {
                    isOccupied = false;
                }
                else
                {
                    isOccupied = true;
                }
            } }

        /// <summary>
        /// Sets the guest in this capsule if it is not occupied. Acts like 'push' to an array
        /// </summary>
        /// <param name="g">The guest to check in</param>
        /// <returns>true if checkin was successfull, false if the spot is occupied</returns>
        public bool CheckIn(Guest g)
        {
            //Check if capsule is occupied
            if(isOccupied)
            {
                Console.WriteLine("Oops! That capsule is occupied. Please enter another");
                return false;
            }

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
            if (!isOccupied)
            {
                Console.WriteLine("Oops! That capsule isn't occupied. Please enter another");
                ConsoleIO.AnyKeyToContinue();
                return null;
            }

            Guest holder = Occupant;
            Occupant = null;
            return holder;
            
        }
    }
}
