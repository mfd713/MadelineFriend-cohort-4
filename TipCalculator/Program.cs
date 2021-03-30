using System;

namespace TipCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //get user dollar amount
            Console.Write("Enter the amount: ");
            string billString = Console.ReadLine();
            decimal bill = decimal.Parse(billString);

            //get tip percentage
            Console.Write("Enter the tip percentage: ");
            string tipString = Console.ReadLine();
            decimal tip = decimal.Parse(tipString)/100;


            //display original amount, the tip percent, the tip amount, and total amount
            Console.WriteLine($"For an amount of {bill:C} a {tip:0.00%} tip would be {bill * tip:C} totaling {bill * (tip +1):C}");
        }
    }
}
