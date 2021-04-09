using System;

namespace ArrayExercises
{
    public class Program
    {

        public static void Main(string[] args)
        {
            int[] one = MakeRandomAscendingArray();
            int[] two = MakeRandomAscendingArray();
            int[] holder = new int[one.Length + two.Length];

            /* Pseudocode:
         Create an integer index for `one`, `two`, and the result array, all starting at 0.
         Loop resultIndex from 0 to result.length - 1:
           if one[oneIndex] is less than two[twoIndex], add it to the result and increment oneIndex.
           if two[twoIndex] is less than one[oneIndex], add it to the result and increment twoIndex.
           determine how to settle ties
           if oneIndex >= one.length, there are no `one` elements remaining so use elements from two
           if twoIndex >= two.length, there are no `two` elements remaining so use elements from one
          */
            int oneIndex = 0;
            int twoIndex = 0;
            for (int i = 0; i < (holder.Length); i++)
            {
                if(oneIndex >= one.Length)
                {
                    holder[i] = two[twoIndex];
                    twoIndex++;
                }
                else if(twoIndex >= two.Length)
                {
                    holder[i] = oneIndex;
                    oneIndex++;
                }
                else if(one[oneIndex] <= two[twoIndex])
                {
                    holder[i] = one[oneIndex];
                    oneIndex++;
                }
                else
                {
                    holder[i] = two[twoIndex];
                    twoIndex++;

                }
            }
            foreach(int i in holder)
            {
                Console.WriteLine(i);
            }
        }

        public static int[] MakeRandomAscendingArray()
        {
            Random random = new Random();
            int[] result = new int[random.Next(100) + 50];
            int current = random.Next(11);
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = current;
                current += random.Next(4);
            }
            return result;
        }
    }
}
