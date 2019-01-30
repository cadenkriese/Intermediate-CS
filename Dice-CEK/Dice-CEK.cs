using System;
/***********************************************************************
*
*  Filename : Dice-CEK.cs
*  Author : Caden Kriese
*  Purpose : The program takes input from the user asking for the amount
*  of dice they would like to roll and then prints the output in console
*  using a random number generator to calculate the different values. 
*
***********************************************************************/
namespace Dice_CEK
{
    static class Program
    {
        static void Main(string[] args)
        {
            int diceAmount = Convert.ToInt32(RequestInput("Enter the number of dice to roll: "));

            //Roll dice i amount of times
            for (int i = 0; i < diceAmount; i++)
            {
                new Dice().roll();
            }
        }

        /// <summary>
        /// Retrieves user input.
        /// </summary>
        /// <param name="prompt">The prompt to provide the user.</param>
        /// <returns>The users input.</returns>
        private static String RequestInput(String prompt)
        {
            Console.Out.WriteLine(prompt);
            return Console.In.ReadLine();
        }
    }
}