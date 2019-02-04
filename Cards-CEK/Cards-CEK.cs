using System;

/***********************************************************************
*
*  Filename : Dice-CEK.cs
*  Author : Caden Kriese
*  Purpose : The program takes input from the user asking for the amount
*  of cards they would like to deal and then prints the output in console
*  using a random number generator to calculate the different values. 
*
***********************************************************************/
namespace Cards_CEK
{
    class CardsCek
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            //int cardCount = RequestNumericalInput("Enter a number of cards: ");
            for (int i = 0; i < int.MaxValue; i++)
            {
                new Card().print();
            }
        }
        
        /// <summary>
        /// Retrieves user input.
        /// </summary>
        /// <param name="prompt">The prompt to provide the user.</param>
        /// <returns>The users input.</returns>
        private static int RequestNumericalInput(string prompt)
        {
            try
            {
                Console.Out.WriteLine(prompt);
                return Convert.ToInt32(Console.In.ReadLine());
            }
            catch
            {
                ConsoleColor previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input, try again.");
                Console.ForegroundColor = previousColor;
                
                return RequestNumericalInput(prompt);
            }
        }
    }

    class Card
    {
        private static Random rand;
        private static string[] faces = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
        //Order = Clubs Spades, Hearts, Diamonds
        private static string[] suits = {"\u2667", "\u2664", "\u2661", "\u2662"};
        
        public Card()
        {
            //Create a new rng only once.
            if (rand == null)
                rand = new Random();
        }

        public void print()
        {
            int value = rand.Next(52);
            //random # 0 - 12
            int face = value % 13;
            //random #  0-4
            int suit = value / 13;

            //Match output color to suit
            Console.ForegroundColor = suit > 1 ? ConsoleColor.Red : ConsoleColor.Black;
            //Print output.
            Console.Write(faces[face] + suits[suit] + "  ");
        }
    }
}