using System;

namespace NumberGuess_CEK
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generate random number between 1 and 100 (This is the static goal number they're trying to guess)
            var randomGoal = new Random().Next(99)+1;

            int minValue = 1;
            int maxValue = 100;

            int remainingGuesses = 10;
           
            while (true)
            {
                if (remainingGuesses == 0)
                {
                    //They have guessed the max amount of times and failed.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("You are out of guesses! Would you like to go again?");

                    if (Console.In.ReadLine().Equals("yes", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Main(args);
                    }
                    else
                    {
                        //Set console color to white and exit.
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    
                    continue;
                }
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                int input = RetrieveNumber("Guess a number between " + minValue + " and " + maxValue + ":");

                if (input == int.MinValue)
                {
                    Console.Out.WriteLine("Invalid input!");
                    continue;
                }
                
                if (input == randomGoal)
                {
                    //They guessed the correct number
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Out.WriteLine("Congratulations! The number was "+randomGoal+".");
                    Console.Out.WriteLine("Would you like to go again?");

                    //Ask if they want to go again.
                    if (Console.In.ReadLine().Equals("yes", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Main(args);
                    }
                    else
                    {
                        //Set console color to white and exit.
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    
                    break;
                }

                //If the number is 80 and they guess 90 80-90 = -10 which is < 0
                bool wasAbove = randomGoal - input < 0;
                //Define this here to avoid duplicating all this code below.
                string highLow = wasAbove ? "high" : "low";

                if (wasAbove)
                    maxValue = input;
                else
                    minValue = input;
                    
                Console.ForegroundColor = ConsoleColor.Red;
                int difference = Math.Abs(randomGoal - input);
                if (difference < 20)
                    //Range 0-19
                {
                    Console.Out.WriteLine("Your guess was slightly too "+highLow+".");
                } else if (difference < 40)
                    //Range 20-39
                {
                    Console.Out.WriteLine("Your guess was too "+highLow+".");
                }
                else if (difference  < 60)
                    //Range 40-59
                {
                    Console.Out.WriteLine("Your guess was slightly too "+highLow+".");
                }
                else
                    //Range 60-99
                {
                    Console.Out.WriteLine("Your guess was way too "+highLow+".");
                }

                remainingGuesses--;
                //Print their remaining guesses (using proper grammar of course).
                Console.Out.WriteLine("You have "+remainingGuesses+" guess"+(remainingGuesses == 1 ? "" : "es")+" remaining.");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        
        private static int RetrieveNumber(string request)
        {
            Console.WriteLine(request);
            String numberInput = Console.In.ReadLine();
            return int.TryParse(numberInput, out var intInput) ? intInput : int.MinValue;
        }
    }
}