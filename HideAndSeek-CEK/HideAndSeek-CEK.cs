﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HideAndSeek_CEK
{
    class HideAndSeekCEK
    {
        private static List<Location> hiders = new List<Location>();
        private static Location user = new Location(0, 0);
    
        static void Main()
        {
            int mapSize = RequestNumericalInput("How large would you like the arena to be?");
            int hidersAmount = RequestNumericalInput("How many hiders would you like to play against?");

            int[] xValues = CalculateRandoms(hidersAmount, 0, mapSize);
            int[] yValues = CalculateRandoms(hidersAmount, 0, mapSize);

            for (int i = 0; i < hidersAmount; i++)
            {
                hiders.Add(new Location(xValues[i], yValues[i]));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You are playing against "+hidersAmount+" hiders on a "+mapSize+"x"+mapSize+ " map.");
            
            
            //While at least one hasn't been found.
            while (!hiders.TrueForAll(hider => hider.Found))
            {
                Console.WriteLine("Your current position is ("+user.X+","+user.Y+").");
                String move = RequestMovementInput();

                switch (move)
                {
                    case "W":
                        user.X++;
                        break;
                    case "S": 
                        user.X--;
                        break;
                    case "D": 
                        user.Y++;
                        break;
                    case "A": 
                        user.Y--;
                        break;
                }

                if (user.X > mapSize || user.X < 0 || user.Y > mapSize || user.Y < 0)
                {
                    //Revert their movement.
                    if (user.X > mapSize)
                        user.X--;
                    else if (user.X < 0)
                        user.X++;
                    else if (user.Y > mapSize)
                        user.Y--;
                    else if (user.Y < 0)
                        user.Y++;
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You hit the border of the map!");
                    continue;
                }

                int i = 1;
                foreach (var hider in hiders)
                {
                    double distance = user.CalculateDistance(hider);
                    Console.ForegroundColor = distance < 3 || hider.Found ? ConsoleColor.Green : ConsoleColor.DarkGreen;

                    if (distance == 0 || hider.Found)
                    {
                        Console.WriteLine("You have found Hider #"+i+"!");
                        hider.Found = true;
                    }
                    else
                    {
                        Console.WriteLine("Your distance from Hider #" + i + " is " + distance);
                    }
                    
                    i++;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations you have found all of the hiders!");
        }

        private static int RequestNumericalInput(string prompt)
        {
            bool validInput = false;
            int response = -1;
            while (!validInput)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(prompt);
                    response = Convert.ToInt32(Console.ReadLine());
                    validInput = true;
                }
                catch
                {
                    ConsoleColor previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, try again.");
                    Console.ForegroundColor = previousColor;
                }
            }

            return response;
        }
        
        private static string RequestMovementInput()
        {
            string[] validInputs = {"W", "A", "S", "D"};
            
            bool validInput = false;
            string response = "";
            while (!validInput)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Enter a movement key (W, A, S or D)");
                    response = Console.ReadLine();
                    if (response != null && validInputs.Contains(response.ToUpper()))
                    {
                        response = response.ToUpper();
                        validInput = true;
                    }
                    else
                    {
                        //Shortcut to run the catch block without duplicating code.
                        throw new Exception();
                    }
                }
                catch
                {
                    ConsoleColor previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, try again.");
                    Console.ForegroundColor = previousColor;
                }
            }

            return response;
        }

        private static int[] CalculateRandoms(int amount, int minInclusive, int maxExclusive)
        {
            Random rand = new Random();
            int[] randoms = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                randoms[i] = rand.Next(maxExclusive) + minInclusive;
            }
            
            return randoms;
        }
    }

    class Location
    {
        public int X;
        public int Y;

        public bool Found;
        
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double CalculateDistance(Location location)
        {
            int xDiff = X - location.X;
            int yDiff = Y - location.Y;
    
            return Math.Sqrt(xDiff*xDiff + yDiff*yDiff);
        }
    }
}