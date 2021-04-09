using System;

namespace TaskManager
{
    class Program
    {

        static void Main(string[] args)
        {
            string[][] stringArray = new string[5][];
            //string[] menuArray = new string[4];
            //menuArray[0] = "View List";
            //menuArray[1] = "Add a Task";
            //menuArray[2] = "Remove a Task";
            //menuArray[3] = "Press Q to quit";

            do
            {
                int choice = UserChoice("Task List \n ======================== \n 1. View List \n 2. Add a Task \n 3. Remove a Task \n 4. Edit a Task \n \n Press Q to quit " +
                "\n===================== \n \n Enter Choice: ");

                //int choice = UserChoice("Enter Choice: ");

                switch (choice)
                {
                    case -1:
                        return;
                    case 1:
                        ViewTasks(stringArray);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        if (stringArray[stringArray.Length - 1] != null)
                        {
                            Console.WriteLine("You have too many tasks!");
                        }
                        else
                        {
                            AddTask(ReadString("Enter Task: "), stringArray);
                            Console.WriteLine("It's on the list!");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        if (stringArray[0] == null)
                        {
                            Console.WriteLine("You don't have any tasks!");
                        }
                        else
                        {
                            ViewTasks(stringArray);
                            RemoveTask(ReadInt("Enter the parent Task to delete: "), stringArray);
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        break;

                }
            } while (true);
        }


        static int UserChoice(string message)
        {
            string userInput;
            do
            {
                userInput = ReadString(message);
            } while (userInput != "q" && userInput != "1" && userInput != "2" && userInput != "3");

            int choice = -1;
            if (userInput.ToLower() == "q")
            {
                return choice;
            }
            else
            {
                return int.Parse(userInput);
            }
        }
        //Create a edit method that will edit a task
        static void EditTask(string[][] stringArray)
        {

        }

        //Create a add method that will add a task to the array
        //Parameters: string, string[]
        //Return: void
        static void AddTask(string task, string[][] stringArray)
        {

            for (int index = 0; index < stringArray.Length; index++)
            {

                if (stringArray[index] == null)
                {

                    int subtaskCount = ReadInt("How many subtasks would you like to add (Please press 0 for none)? ");
                    //stringArray[index][subtasks];
                    stringArray[index] = new string[subtaskCount + 1];
                    stringArray[index][0] = task;
                    if (subtaskCount != 0)
                    {
                        for (int i = 0; i < subtaskCount; i++)
                        {
                            string newSubtask = ReadString("Enter subtask: ");
                            stringArray[index][i + 1] = newSubtask;
                        }
                    }

                    return;
                }
            }
            ViewTasks(stringArray);
        }

        //Create a view method that will display the tasks in the array to the user
        //Print out the array with the proper formating
        // Parameters: string[]
        // return void
        static void ViewTasks(string[][] stringArray)
        {
            string border = "=====================================";


            Console.WriteLine($"{border}\n");
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (stringArray[i] == null)
                {
                    Console.WriteLine($"{i + 1}.");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {stringArray[i][0]}");
                    if (!(stringArray[i] == null))
                    {
                        for (int k = 1; k < stringArray[i].Length; k++)
                        {
                            Console.WriteLine($"\n\t{k}. {stringArray[i][k]}");

                        }

                    }
                }

            }

            Console.WriteLine($"\n{border}");


        }

        //Create a remove method that will remove a task from the array.
        //Parameters: int, string[][]
        //Return: void

        static void RemoveTask(int task, string[][] stringArray)
        {
            int taskChoice = task - 1;
            int parentOrSubt = ReadInt("Enter 1 to delete the whole Task or 2 to delete a Subtask: ");
            switch (parentOrSubt)
            {
                case 1:
                    stringArray[taskChoice] = null;
                    for (int i = taskChoice; i < stringArray.Length; i++)
                    {
                        if (i == stringArray.Length - 1)
                        {
                            stringArray[i] = null;
                        }
                        else
                        {
                            stringArray[i] = stringArray[i + 1];

                        }
                    }
                    Console.WriteLine("Deleted!");
                    break;
                case 2:
                    int subChoice = ReadInt("Enter the number of the Subtask you want to delete: ");
                    if(subChoice > stringArray[taskChoice].Length || stringArray[taskChoice][subChoice] == null)
                    {
                        Console.WriteLine("That subtask doesn't exist!");
                        Console.ReadKey();
                        break;
                    }
                    stringArray[taskChoice][subChoice] = null;
                    for (int i = subChoice; i < stringArray[taskChoice].Length; i++)
                    {
                        if (i == stringArray[taskChoice].Length - 1)
                        {
                            stringArray[taskChoice][i] = null;
                        }
                        else
                        {
                            stringArray[taskChoice][i] = stringArray[taskChoice][i + 1];

                        }
                    }
                    Console.WriteLine("Deleted!");
                    if (stringArray[taskChoice]== null)
                    {
                        Console.WriteLine("This task is now empty.");
                    }
                    break;
            }
            

        }



        //obtain a string from the user
        static string ReadString(string prompt)
        {
            string result = "";
            do
            {
                Console.Write($"{prompt} ");
                result = Console.ReadLine();
            } while (result == null || result.Trim().Length == 0);
            return result;
        }

        //obtain a int from the user
        static int ReadInt(string prompt)
        {
            int result;
            bool valid;
            do
            {
                valid = int.TryParse(ReadString(prompt), out result);
            } while (!valid);
            return result;
        }
    }

}
