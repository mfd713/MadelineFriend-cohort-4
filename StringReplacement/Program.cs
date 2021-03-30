using System;

namespace StringReplacement
{
    class Program
    {
        static void Main(string[] args)
        {
            string originalString, searchString, replaceString, result;

            //get OG string input from user
            Console.Write("Enter a string: ");
            originalString = Console.ReadLine();
            //get search string from user
            Console.Write("Enter a search string: ");
            searchString = Console.ReadLine();
            //get replacement string from user
            Console.Write("Enter a replacement string: ");
            replaceString = Console.ReadLine();

            //use replace() to replace
            result = originalString.Replace(searchString, replaceString);
            //print final result
            Console.WriteLine($"The result is: {result}");
        }
    }
}
