using System;
using System.Collections.Generic;
using System.Text;

namespace CapsuleHotel
{
    public class Capsule
    {
        public Guest guest { get; set; }
        public bool isOccupied { get {
                return isOccupied;
            }

            private set
            {
                if(guest == null)
                {
                    isOccupied = false;
                }
                else
                {
                    isOccupied = true;
                }
            } }

        //WHEN U COME BACK FROM LUNCH START HERE, REFACTORING CHECKIN METHOD TO WORK WITH NEW SYSTEM
        /// <summary>
        /// Promps the user for a guest name & capsule number and adds the guest to the list if both entries are valid
        /// </summary>
        /// <param name="arr">string[] guestList</param>
        public void CheckIn(Guest g)
        {
            int capsuleNumber = 0;
            //confirm at least one open spot
            if (CountNullOrEmpties(arr) == 0)
            {
                Console.WriteLine("The guest list is full. Please remove a guest before adding a new one.");
                AnyKeyToContinue();
                return;
            }
            //promt user for guest name
            guestName = ReadString("Enter the guest name");
            //prompt user for spot number
            capsuleNumber = ReadInt($"Enter the capsule number (1 - {arr.Length})", 1, arr.Length);
            //re-prompt if spot is occupied
            while (IsCapsuleOccupied(arr, capsuleNumber - 1))
            {
                capsuleNumber = ReadInt("Oops! That capsule is occupied. Please enter another");
            }

            //set if spot is open
            arr[capsuleNumber - 1] = guestName;
            Console.WriteLine($"Success! {guestName} was added to Capusle # {capsuleNumber}");
            AnyKeyToContinue();
        }

        /// <summary>
        /// Counts the number of null/empty strings in an array of strings
        /// </summary>
        /// <param name="arr">array of strings</param>
        /// <returns>int count of empties</returns>
        private int CountNullOrEmpties()
        {
            int count = 0;
            foreach (string item in arr)
            {
                if (String.IsNullOrEmpty(item))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
