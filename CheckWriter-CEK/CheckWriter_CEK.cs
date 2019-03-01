using System;
using CheckWriter;

namespace CheckWriter_CEK
{
    class CheckWriter_CEK
    {
        static void Main(string[] args)
        {
            CheckUpdated check = new CheckUpdated();
            check.Display();
        }
    }

    class CheckUpdated : Check
    {
        private String date;
        
        public override void Display()
        {
            int width = 65;
            
            date = DateTime.Today.ToString("MMMM dd, yyyy");
            Console.Out.WriteLine(new string(' ', width-date.Length)+date);
        }
    }
}