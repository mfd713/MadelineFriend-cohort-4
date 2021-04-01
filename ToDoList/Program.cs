using System;
using System.Text;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            //an array
            string[,] toDoList = new string[5,5];
            string[] fullList = { "one", "two", "three", "four", "five" }; // a full list if you want to use it for testing

            menu(toDoList);
        }

        // Any key to continue method
        static void AnyKeyToContinue()
        {
            ConsoleKeyInfo consoleKey;
            do
            {
                Console.WriteLine("Press any key to continue...");
                consoleKey = Console.ReadKey();
            } while (false);
        }

        // remove a task
        static void Remove(string[] arr)
        {
            //Display list using task

            //Prompt user for item to be deleted, save into variable +1 for index

            int indexToRemove = 0;
            do
            {
                indexToRemove = PromptUserForInt("Enter list number you wish to remove: ") - 1;
                if (arr[0] == null)
                {
                    Console.WriteLine("This task is empty. You cannot remove what does not exist");
                    AnyKeyToContinue();
                    return;
                }
                else if (indexToRemove >= arr.Length)
                {
                    Console.WriteLine($"Enter a number between 1 and {arr.Length}");
                }
            } while (!(indexToRemove < arr.Length));



            for (int i = indexToRemove; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                {
                    arr[i] = null;
                }
                else
                {
                    arr[i] = arr[i + 1];
                }
            }


        }
        //add method. Will add a string to the end of the array, if there is space
        //takes in a string array
        //does not return anything
        static void add(string[][] arr)
        {
            //Ask if user is adding a task or subtask
            int addTaskorSubtask = PromptUserForInt("Enter 1 to add a task or 2 to add a Subtask");
            switch (addTaskorSubtask)
            {
                case 1:
                    //check if there is space to add a task, end if not or keep going if there is
                    if (!String.IsNullOrEmpty(arr[arr.GetLength(0) - 1][0]))
                    {
                        Console.WriteLine("The ToDo list is full. Please remove an item before adding a new one.");
                        AnyKeyToContinue();
                        return;
                    }

                    //prompt user for task
                    //check that input is not null or empty
                    string input = "";
                    while (String.IsNullOrEmpty(input))
                    {
                        Console.Write("Enter Task: ");
                        input = Console.ReadLine();
                        if (String.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Please enter a valid string");
                        }
                    }

                    //find next available place in the task array
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        if (String.IsNullOrEmpty(arr[i][0]))
                        {
                            arr[i][0] = input; //add task at that point
                            Console.WriteLine($"\"{input}\" was added to the list!");
                            AnyKeyToContinue();
                            break;
                        }

                    }
                    break;
                case 2:
                    //ask user where they want to add this subtask
                    int whichTaskToSub = PromptUserForInt("Enter the number of the task you want to add a subtask to: ");
                    //check if there is space for the subtask
                    if (!String.IsNullOrEmpty(arr[whichTaskToSub][arr.GetLength(1)-1]))
                    {
                        Console.WriteLine("The task is full. Please remove a subtask before adding a new one.");
                        AnyKeyToContinue();
                        return;
                    }

                    //prompt user for subtask
                    //check that input is not null or empty
                    input = "";
                    while (String.IsNullOrEmpty(input))
                    {
                        Console.Write("Enter Subtask: ");
                        input = Console.ReadLine();
                        if (String.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Please enter a valid string");
                        }
                    }

                    //find next available place in the subtask array
                    for (int i = 0; i < arr[whichTaskToSub].Length; i++)
                    {
                        if (String.IsNullOrEmpty(arr[whichTaskToSub][i]))
                        {
                            arr[whichTaskToSub][i] = input; //add task at that point
                            Console.WriteLine($"\"{input}\" was added to the list!");
                            AnyKeyToContinue();
                            break;
                        }

                    }

                    break;
            }

        }

        //menu method
        static void menu(string[][] arr)
        {
            bool valid = false;
            int selection = 0;
            do
            {
                Console.Clear();
                // display menu
                Console.Write("Task List\n=====================================\n1. View List\n2. Add a Task or Subtask\n3. Remove a Task or Subtask\n\nPress Q to quit\n=====================================\n\nEnter Choice: ");

                // prompt user for menu item
                string input = Console.ReadLine();

                // validate response
                if (int.TryParse(input, out selection))
                {
                    switch (selection)
                    {
                        case 1:
                            // run view method
                            Console.Clear();
                            View(arr);
                            break;
                        case 2:
                            // run add method
                            Console.Clear();
                            add(arr);
                            break;
                        case 3:
                            // run remove method
                            Console.Clear();
                            Remove(arr);
                            break;
                        default:
                            Console.WriteLine("Invalid response. Please select a menu item of 1, 2, or 3, or press Q to quit.");
                            AnyKeyToContinue();
                            break;
                    }
                }
                else if (input.ToLower() == "q")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid response. Please select a menu item of 1, 2, or 3, or press Q to quit.");
                    AnyKeyToContinue();
                    continue;
                }

            } while (!valid);
        }

        //view method
        static void View(string[] arr)
        {
            StringBuilder stringBuilder = new StringBuilder("Task List\n" +
                "======================\n");

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null)
                {
                    stringBuilder.Append($"{i + 1}. {arr[i]}\n");
                }
            }
            stringBuilder.Append("======================");
            Console.WriteLine(stringBuilder.ToString());
            AnyKeyToContinue();
        }
        static int PromptUserForInt(string prompt)
        {
            int output = 0;
            Console.Write(prompt);
            int.TryParse(Console.ReadLine(), out output);
            return output;
        }
    }


}


