using System;

namespace LoopExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            int index1 = 0;
            int index2 = 0;
            string output = "";

            Console.Write("First string: ");
            string first = Console.ReadLine();

            Console.Write("Second string: ");
            string second = Console.ReadLine();

            //while loop that procedes while where are still characters available
            //i.e. first.length > index1 or second.length > index2
            while(index1 < first.Length || index2 < second.Length)
            {
                //if there is a char at the current index of first, add it
                if(index1 < first.Length)
                {
                        output += first[index1];
                }

                //if there is a char at the current index of second, add it
                if(index2 < second.Length)
                {
                        output += second[index2];
                }

                //increment indexes
                index1++;
                index2++;
            }

            //print result
            Console.WriteLine(output);
        }
    }
}