/***********************************************************************
*
*  Filename : inputoutput-cek.cs
*  Author : Caden Kriese
*  Purpose : Request and reprint user information utilizing a separate
* class to handle their data.
*
***********************************************************************/

using System;

namespace IO_CEK
{
    internal static class Program
    {   
        private static void Main(string[] args)
        {
            InfoCard infoCard = new InfoCard();
            Console.ForegroundColor = ConsoleColor.Cyan;
            infoCard.Input();
            Console.ForegroundColor = ConsoleColor.Green;
            infoCard.Output();
            //Change color back to white.
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}