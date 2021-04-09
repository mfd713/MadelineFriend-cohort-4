using System;

namespace MethodExercises
{
    public class Program
    {
        //reads a string from the user and validates
        //input: message you want to prompt user with
        //output: the string given by the user
        static string ReadString(string prompt)
        {
            string input = "";
            Console.Write(prompt);
            while (String.IsNullOrEmpty(input))
            {
                input = Console.ReadLine();
                if (String.IsNullOrEmpty(input))
                {
                    Console.Write("Please enter a valid string: ");
                }
            }
            return input;
        }
        // Name: ReadInt
        // Inputs: string
        // Output: int
        // Description: prompts a user to enter a whole number and returns their input as an int.
        // The parameter is the message displayed to the user.
        static int ReadInt(string str)
        {
            string input = ReadString(str);
            int number = 0;
            while (!int.TryParse(input, out number))
            {
                Console.Write("Please enter a whole number: ");
                input = ReadString("");
            }
            return number;
        }
        public static void Main(string[] args)
        {
            String firstName = ReadString("What is your first name? ");
            String lastName = ReadString("What is your last name? ");
            int residences = ReadInt("How many towns/cities have you live in? ");
            Console.WriteLine($"First name: {firstName}\nLast Name: {lastName}\n" +
                $"Places lived: {residences}");
        }
    }
}
