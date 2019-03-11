using System;
using System.Linq;

namespace FractionCalculator_CEK
{
    class FractionCalculator
    {
        static void Main()
        {
            bool validInput = false;

            while (!validInput)
            {

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Enter an operation: ");
                String operation = Console.ReadLine();

                //Check if their input is a valid operation.
                validInput = new[] {"+", "-", "*", "/"}.Contains(operation);

                Fraction fractionOne = new Fraction().Input();
                Fraction fractionTwo = new Fraction().Input();

                switch (operation)
                {
                    case "+":
                        Console.WriteLine((fractionOne + fractionTwo).ToString());
                        break;
                    case "-":
                        Console.WriteLine((fractionOne - fractionTwo).ToString());
                        break;
                    case "*":
                        Console.WriteLine((fractionOne * fractionTwo).ToString());
                        break;
                    case "/":
                        Console.WriteLine((fractionOne / fractionTwo).ToString());
                        break;
                    default:
                        Console.WriteLine("Invalid input! Try again.");
                        continue;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        class Fraction
        {
            public int Whole;
            public int Num;
            public int Denom;

            public Fraction Input()
            {
                //Request input.
                Whole = RequestNumericalInput("Enter a whole number: ");
                Num = RequestNumericalInput("Enter a numerator: ");
                Denom = RequestNumericalInput("Enter a denominator: ");

                //Allow chainable usage.
                return this;
            }

            public static Fraction operator *(Fraction lhs, Fraction rhs)
            {
                Fraction result;
                int lhsNum, rhsNum;
                result = new Fraction();

                result.Denom = lhs.Denom * rhs.Denom;
                lhsNum = lhs.Whole * lhs.Denom + lhs.Num;
                rhsNum = rhs.Whole * rhs.Denom + rhs.Num;
                result.Num = lhsNum * rhsNum;

                result.Whole = result.Num / result.Denom;
                result.Num = result.Num % result.Denom;

                return result;
            }

            public static Fraction operator /(Fraction lhs, Fraction rhs)
            {
                Fraction result;
                int lhsNum, rhsNum;
                result = new Fraction();

                //Swap rhs to its reciprocal
                rhsNum = rhs.Whole * rhs.Denom + rhs.Num;
                rhs.Num = rhs.Denom;
                rhs.Denom = rhsNum;
                rhsNum = rhs.Num;

                result.Denom = lhs.Denom * rhs.Denom;
                lhsNum = lhs.Whole * lhs.Denom + lhs.Num;
                result.Num = lhsNum * rhsNum;

                result.Whole = result.Num / result.Denom;
                result.Num = result.Num % result.Denom;

                return result;
            }

            public static Fraction operator +(Fraction lhs, Fraction rhs)
            {
                Fraction result = new Fraction();

                //Convert to improper fraction
                int lhsNum, rhsNum;
                lhsNum = lhs.Whole * lhs.Denom + lhs.Num;
                rhsNum = rhs.Whole * rhs.Denom + rhs.Num;

                int commonDenom = lhs.Denom * rhs.Denom;
                result.Denom = commonDenom;

                lhsNum *= rhs.Denom;
                rhsNum *= lhs.Denom;

                result.Num = lhsNum + rhsNum;

                //Create mixed number.
                result.Whole = result.Num / result.Denom;
                result.Num = result.Num % result.Denom;

                return result;
            }

            public static Fraction operator -(Fraction lhs, Fraction rhs)
            {
                Fraction result = new Fraction();

                int lhsNum, rhsNum;
                lhsNum = lhs.Whole * lhs.Denom + lhs.Num;
                rhsNum = rhs.Whole * rhs.Denom + rhs.Num;

                int commonDenom = lhs.Denom * rhs.Denom;
                result.Denom = commonDenom;

                lhsNum *= rhs.Denom;
                rhsNum *= lhs.Denom;

                result.Num = lhsNum - rhsNum;

                //Create mixed number.
                result.Whole = result.Num / result.Denom;
                result.Whole = result.Num % result.Denom;

                return result;
            }

            public override string ToString()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return Whole + " " + Num + "/" + Denom;
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
        }
    }
}