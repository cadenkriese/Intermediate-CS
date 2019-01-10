using System;

namespace Hello_CEK
{
    internal static class Program
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Please enter your name:");
            var name = Console.In.ReadLine();
            Console.WriteLine("Hello, " + name + "!");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}