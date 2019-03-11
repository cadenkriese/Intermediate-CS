using System;
using CheckWriter;

/***********************************************************************
*
*  Filename : CheckWriter_CEK.cs
*  Author : Caden Kriese
*  Purpose : The CheckWriter is program is designed to format the given 
*  input (Name and amount) into a check format. This program utilizes 
*  another program also named CheckWriter to retrieve the data, such as
*  check number, from the bankning database. However, we override the
*  output method to support modern dates.
*
***********************************************************************/
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
            
            //Generate formatted date string, EX: March 5, 2019
            string date = DateTime.Today.ToString("MMMM dd, yyyy");
            string amountString = "$ " + Amount;

            Console.Out.WriteLine(new String(' ', width-Number.ToString().Length) + Number);
            Console.Out.WriteLine(new string(' ', width-date.Length)+date);
            Console.Out.WriteLine("Pay to the ");
            Console.Out.WriteLine("Order of: "+Payee);
            Console.Out.WriteLine(new String(' ', width-amountString.Length)+amountString);
            //Additional -2 due to 2 spaces on either side of the ~~~~~~~~.
            String dollar = Amount == 1 ? "Dollar" : "Dollars";
            Console.Out.WriteLine(strAmount + " " + new String('~', width-strAmount.Length-dollar.Length-2) + " "+dollar);
        }
    }
}
