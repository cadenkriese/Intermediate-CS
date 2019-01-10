using System;
using System.Text.RegularExpressions;

namespace Calculator_CEK
{
    internal static class Program
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

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
            Console.WriteLine("Valid operators, +, -, *, /, %, abs, sin, cos, tan, end");

            //Ask for operation first so we know the amount of inputs required.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter an operation.");
            var operation = Console.In.ReadLine();

            if (operation.ToLower().Equals("end"))
            {
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            //Perform calculations that only require one number first.
            if (operation.ToLower().Equals("sin") || operation.ToLower().Equals("cos") ||
                operation.ToLower().Equals("tan") || operation.ToLower().Equals("abs") ||
                operation.ToLower().Equals("end"))
            {
                var input = RetrieveNumber("Enter a number.");
                operation = operation.ToLower();

                switch (operation)
                {
                    case "sin":
                        Console.WriteLine("sin(" + input + ") = " + Math.Sin(input));
                        break;
                    case "cos":
                        Console.WriteLine("cos(" + input + ") = " + Math.Cos(input));
                        break;
                    case "tan":
                        Console.WriteLine("tan(" + input + ") = " + Math.Tan(input));
                        break;
                    case "abs":
                        Console.WriteLine("abs(" + input + ") = " + Math.Abs(input));
                        break;
                    default:
                        Console.WriteLine("Invalid argument, type END to exit.");
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            //Run operations that require 2 numbers.
            var firstNumber = RetrieveNumber("Please enter a number.");
            var secondNumber = RetrieveNumber("Please enter a second number.");

            var rgx = new Regex("[a-zA-Z0-9]");
            operation = rgx.Replace(operation, "");

            switch (operation)
            {
                case "+":
                    Console.WriteLine(firstNumber + " + " + secondNumber + " = " + (firstNumber + secondNumber));
                    break;
                case "-":
                    Console.WriteLine(firstNumber + " - " + secondNumber + " = " + (firstNumber - secondNumber));
                    break;
                case "*":
                    Console.WriteLine(firstNumber + " * " + secondNumber + " = " + firstNumber * secondNumber);
                    break;
                case "/":
                    Console.WriteLine(firstNumber + " / " + secondNumber + " = " + firstNumber / secondNumber);
                    break;
                case "%":
                    Console.WriteLine(firstNumber + " % " + secondNumber + " = " + firstNumber % secondNumber);
                    break;
                default:
                    Console.WriteLine("Invalid operation!");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.White;
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
    }
}