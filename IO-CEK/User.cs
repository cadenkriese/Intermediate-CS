using System;

namespace IO_CEK
{
    public class User
    {
        private string name;
        private string address;
        private string city;
        private string state;
        private string zipCode;
        
        public void Input()
        {
            //Request inputs using private requestInput method.
            name = RequestInput("Enter your name: ");
            address = RequestInput("Enter your street address: ");
            city = RequestInput("Enter your city: ");
            state = RequestInput("Enter your state: ");
            zipCode = RequestInput("Enter your ZIP: ");
        }

        public void Output()
        {
            Console.Out.WriteLine(name);
            Console.Out.WriteLine(address);
            Console.Out.WriteLine(city + ", "+state + " " + zipCode);
        }
        
        private static String RequestInput(String prompt)
        {
            //Print line prompting user for input.
            Console.Out.WriteLine(prompt);
            //Return the input given.
            return Console.ReadLine();
        }
    }
}