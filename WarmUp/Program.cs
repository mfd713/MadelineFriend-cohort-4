using System;

namespace WarmUp
{
    class Program
    {
        static void Main(string[] args)
        {
            //show what we have to offer
            //Console.Write("The year is 2007. You're at the mall with your friends. What would you like to purchase from one of the fine establishments?\n" +
            //    "1. A Panic! At the Disco T-shirt from Hot Topic\n" +
            //    "2. A set of earrings from Claire's\n" +
            //    "3. A pretzle from Auntie Anne's\n" +
            //    "Choose a number: ");
            ////user chooses which they want
            //int choice = int.Parse(Console.ReadLine());

            ////print out price
            //switch(choice)
            //{
            //    case 1:
            //        Console.WriteLine("That will be $15");
            //        break;
            //    case 2:
            //        Console.WriteLine("That will be $10");
            //        break;
            //    case 3:
            //        Console.WriteLine("That will be $2");
            //        break;

            //int[][] numberArray = new int[3][]; // don't need to explicitly set the number of columns

            ////these lines are creating implicit lengths for each column. Similar to setting an array like string[] stringArray = {"first", "second", "last"} and now we know stringArray.Length = 3
            //numberArray[0] = new int[]{ 2,1,10,2,5};
            //numberArray[1] = new int[] { 4, 10, 4, 7 };
            //numberArray[2] = new int[] { 9, 8, 7, 4, 3 };

            //for(int i = 0; i < numberArray.Length; i++)
            //{
            //    Console.WriteLine($"Element {i}");
            //    for (int j = 0; j < numberArray[i].Length; j++)
            //    {
            //        Console.WriteLine($"{j}: {numberArray[i][j]}");
            //    }
            //}
            string[] toDoList = new string[5];
            int menuChoice = 0;

            while (menuChoice != 3)
            {
                menuChoice = menu();

                switch (menuChoice)
                {
                    case 1:
                        break;
                    case 2:
                        add(toDoList);
                        break;
                    case 3:
                        break;
                }
                for(int i = 0; i < toDoList.Length; i++)
                {
                    Console.WriteLine(toDoList[i]);
                }
            }

        }
        static void add(string[] arr)
        {
            //check if there is space to add, end if not or keep going if there is
            if (!String.IsNullOrEmpty(arr[4]))
            {
                Console.WriteLine("The ToDo list is full. Please remove an item before adding a new one.");
                return;
            }
            //prompt user for task
            Console.Write("Enter Task: ");
            string input = Console.ReadLine();

            //find next available place in the array
            for (int i = 0; i < arr.Length; i++)
            {
                if (String.IsNullOrEmpty(arr[i]))
                {
                    arr[i] = input; //add task at that point
                    Console.WriteLine($"{input} was added to the list!");
                    break;
                }

            }


        }
        static int menu()
        {
            //display menu
            Console.WriteLine("Please select a menu item:\n1. View Items\n2. Add Items\n3. Quit\n");
            //prompt user for menu item
            string input = Console.ReadLine();
            //validate entry
            //execute applicable method
            return int.Parse(input);

        }

    }
}
