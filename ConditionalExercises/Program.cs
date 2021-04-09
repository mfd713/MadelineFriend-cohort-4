using System;

namespace ConditionalExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the secret word: ");
            string secret = Console.ReadLine();

            switch (secret)
            {
                case "tahini":
                    Console.WriteLine("The secret word is \"tahini\"");
                    break;
                default:
                    Console.WriteLine("That's not quite right. Try again.");
                    break;
            }

        }
    }
}
