using System;

namespace KnockKnock
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Knock Knock! (press enter to continue)");
            getUserResponse();

            Console.WriteLine("Who's There?");
            getUserResponse();

            Console.WriteLine("Voodoo");
            getUserResponse();

            Console.WriteLine("Voodoo who?");
            getUserResponse();

            Console.WriteLine("Voodoo you think you are asking me all these questions???");
        }
        public static void getUserResponse()
        {
            ConsoleKeyInfo keypress;
            do
            {
                keypress = Console.ReadKey();
            } while (keypress.Key != ConsoleKey.Enter);
        } 
    }   
}
