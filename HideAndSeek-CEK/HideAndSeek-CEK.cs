using System;
using System.Collections.Generic;
using System.Linq;

/***********************************************************************
*
*  Filename : HideAndSeek-CEK.cs
*  Author : Caden Kriese
*  Purpose : The program stores a list of hiders with randomly generated
*  x and y coordinates within bounds specified by the user. The user is 
*  then required to move themselves around using inputs W, A, S, and D
*  for up, down, left and right respectively. The Users end goal is to
*  locate the hiders based on the distance they are from each hider which
*  is provided to them each time they move. 
*
***********************************************************************/
namespace HideAndSeek_CEK
{
    class HideAndSeekCEK
    {
        private static List<Location> hiders = new List<Location>();
        private static Location user = new Location(5, 5);
        private static int mapSize;
        private static int[,] board;
    
        static void Main()
        {
            mapSize = RequestNumericalInput("How large would you like the arena to be?");
            int hidersAmount = RequestNumericalInput("How many hiders would you like to play against?");

            int[] xValues = CalculateRandoms(hidersAmount, 0, mapSize);
            int[] yValues = CalculateRandoms(hidersAmount, 0, mapSize);

            board = new int[mapSize, mapSize];

            for (int i = 0; i < hidersAmount; i++)
            {
                //Board values in y, x because of how it's printed.
                board[xValues[i], yValues[i]] = 1;
                hiders.Add(new Location(xValues[i], yValues[i]));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You are playing against "+hidersAmount+" hiders on a "+mapSize+"x"+mapSize+ " map.");
            
            
            //While at least one hasn't been found.
            while (!hiders.TrueForAll(hider => hider.Found))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
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

                    if (distance <= 3 || hider.Found)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (distance <= 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

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
                
                PrintBoard();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations you have found all of the hiders!");
            
            Console.ForegroundColor = ConsoleColor.White;
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

        private static void PrintBoard()
        {
            for (int x = mapSize-1; x >= 0; x--)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    if (x == user.X && y == user.Y)
                    {
                        if (board[x, y] == 1)
                            board[x, y] = 2;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                    }
                    else if (board[x, y] == 2)
                    {
                        board[x, y] = 2;
                        
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\u2713 ");
                    }
                    else if (board[x, y] == 1 || new Random().Next((int) Math.Pow(mapSize, 2)) < mapSize)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("? ");
                    } 
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("# ");
                    }
                }
                    Console.WriteLine();
            }
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
    
            return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
        }
    }
}