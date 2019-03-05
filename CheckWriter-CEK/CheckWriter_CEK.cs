using System;
using CheckWriter;

namespace CheckWriter_CEK
{
    class CheckWriter_CEK
    {
        static void Main(string[] args)
        {
            new CheckUpdated().Display();
        }
    }

    class CheckUpdated : Check
    {   
        public override void Display()
        {
            int width = 65;
            
            string date = DateTime.Today.ToString("MMMM dd, yyyy");
            string amountString = "$ " + Amount;
                
            Console.Out.WriteLine(new string(' ', width-date.Length)+date);
            Console.Out.WriteLine("Pay to the ");
            Console.Out.WriteLine("Order of: "+Payee);
            Console.Out.WriteLine(new String(' ', width-amountString.Length)+amountString);
            Console.Out.WriteLine(strAmount + " " + new String('~', width-strAmount.Length-9)+ " Dollars");
        }
        
        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";
            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));
            string words = "";
            
            if (number / 1000000 > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", 
                    "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen",
                    "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if (number % 10 > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }
            return words;
        }
    }
}