using System;
using System.Collections.Generic;

namespace Calculator_CEK
{
    internal static class CalculatorCek
    {
        private static bool _goNext = true;

        private static void Main()
        {
            while (_goNext)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                for (var i = 0; i < 6; i++)
                {
                    Console.WriteLine("");
                }

                Console.WriteLine(
                    "  _________  ____  _________  / /__     _________ _/ /______  __/ /___ _/ /_____  _____");
                Console.WriteLine(
                    " / ___/ __ \\/ __ \\/ ___/ __ \\/ / _ \\   / ___/ __ `/ / ___/ / / / / __ `/ __/ __ \\/ ___/");
                Console.WriteLine(
                    "/ /__/ /_/ / / / (__  ) /_/ / /  __/  / /__/ /_/ / / /__/ /_/ / / /_/ / /_/ /_/ / /    ");
                Console.WriteLine(
                    "\\___/\\____/_/ /_/____/\\____/_/\\___/   \\___/\\__,_/_/\\___/\\__,_/_/\\__,_/\\__/\\____/_/     ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Welcome to ConsoleCalculator V1.0");
                Console.WriteLine("Valid operators, +, -, *, /, %, abs, sin, cos, tan");

                //Ask for operation first so we know the amount of inputs required.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please enter an operation.");
                var operation = Console.In.ReadLine();

                var multiNumOperations = new List<string> { "+", "-", "*", "/", "%" };
                var singleNumberOperations = new List<string> { "abs", "sin", "cos", "tan" };

                if (!multiNumOperations.Contains(operation) && !singleNumberOperations.Contains(operation))
                {
                    Error("Invalid operation!");
                    continue;
                }
                
                //Perform calculations that only require one number first.
                if (singleNumberOperations.Contains(operation))
                {
                    var input = RetrieveNumber("Enter a number.");
                    operation = operation.ToLower();

                    switch (operation)
                    {
                        case "sin":
                            Console.WriteLine("sin(" + input + ") = " + ConvertRadiansToDegrees(Math.Sin(input)) + "°");
                            break;
                        case "cos":
                            Console.WriteLine("cos(" + input + ") = " + ConvertRadiansToDegrees(Math.Cos(input)) + "°");
                            break;
                        case "tan":
                            Console.WriteLine("tan(" + input + ") = " + ConvertRadiansToDegrees(Math.Tan(input)) + "°");
                            break;
                        case "abs":
                            Console.WriteLine("abs(" + input + ") = " + ConvertRadiansToDegrees(Math.Abs(input)) + "°");
                            break;
                        default:
                            Console.WriteLine("Invalid argument, type END to exit.");
                            break;
                    }

                    _goNext = GoAgane();
                    continue;
                }

                //Run operations that require 2 numbers.
                var firstNumber = RetrieveNumber("Please enter a number.");
                if (firstNumber == double.MinValue)
                {
                    Error("Invalid number!");
                    continue;
                }
                
                var secondNumber = RetrieveNumber("Please enter a second number.");
                if (secondNumber == double.MinValue)
                {
                    Error("Invalid number!");
                    continue;
                }

                switch (operation)
                {
                    case "+":
                        Console.WriteLine(firstNumber + " + " + secondNumber + " = " + (firstNumber + secondNumber));
                        break;
                    case "-":
                        Console.WriteLine(firstNumber + " - " + secondNumber + " = " + (firstNumber - secondNumber));
                        break;
                    case "*":
                        Console.WriteLine(firstNumber + " * " + secondNumber + " = " + (firstNumber * secondNumber));
                        break;
                    case "/":
                        if (secondNumber == 0)
                        {
                            Error("Cannot divide by 0!");
                            continue;
                        }

                        var remainder = firstNumber % secondNumber != 0 && (int)firstNumber / (int)secondNumber != 0 ? " with a remainder of " + firstNumber % secondNumber : "";
                        Console.WriteLine(firstNumber + " / " + secondNumber + " = " + (int)firstNumber / (int)secondNumber + remainder);
                        break;
                    case "%":
                        Console.WriteLine(firstNumber + " % " + secondNumber + " = " + firstNumber % secondNumber);
                        break;
                    default:
                        Error("Invalid operation!");
                        break;
                }

                _goNext = GoAgane();
            }
        }

        /// <summary>
        ///     Retrieves a number from the user through the console.
        /// </summary>
        /// <param name="request">The string that should be used to prompt the user, e.g.
        ///     <value>"Enter a number:"</value>
        /// </param>
        /// <returns>The number given from the user, if they give an invalid number, double.minvalue is returned.</returns>
        private static double RetrieveNumber(string request)
        {
            Console.WriteLine(request);
            var numberInput = Console.In.ReadLine();
            return double.TryParse(numberInput, out var doubleInput) ? doubleInput : double.MinValue;
        }

        private static Boolean GoAgane()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Would you like to go again?");
            String input = Console.In.ReadLine();

            //If they say no the console color has to be set to white.
            Console.ForegroundColor = ConsoleColor.White;
            return input.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
        }

        private static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = 180 / Math.PI * radians;
            return degrees;
        }

        private static void Error(String error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            _goNext = GoAgane();
        }
    }
}