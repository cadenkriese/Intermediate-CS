using System;

/***********************************************************************
*
*  Filename : hello-cek.cs
*  Author : Caden Kriese
*  Purpose : A very simple greeting program, the console requests an input
*     (the users name), and proceeds to say hello to them.
*
***********************************************************************/
namespace Hello_CEK
{
    internal static class HelloCek
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