using System;

/***********************************************************************
*
*  Filename : Dice.cs
*  Author : Caden Kriese
*  Purpose : The object that represents a die. This object can be rolled
* via the roll method which will print the output of the roll in console.
*
***********************************************************************/
namespace Dice_CEK
{
    public class Dice
    {
        static Random random;
        static int count;
        private int id;
        
        public Dice()
        {
            //Only initialize random if it is not already set.
            if (count == 0)
            {
                random = new Random();
            }

            count++;
            id = count;
        }

        public void Roll()
        {
            //Random int 1-6
            int next = random.Next(6) + 1;
            
            //Preset lines
            const string lineOfZero = "\u007c            \u007c";
            const string lineOfOne = "\u007c     \u2022      \u007c";
            const string lineOfTwo = "\u007c  \u2022     \u2022   \u007c";

            //Lines that will be displayed
            string diceLineOne = "";
            string diceLineTwo = "";
            string diceLineThree = "";

            switch (next)
            {
                //Define layouts for each dice version.          
                case 1:
                    diceLineOne = new string(lineOfZero);
                    diceLineTwo = new string(lineOfOne);
                    diceLineThree = new string(lineOfZero);
                    break;
                case 2:
                    diceLineOne = new string(lineOfZero);
                    diceLineTwo = new string(lineOfTwo);
                    diceLineThree = new string(lineOfZero);
                    break;
                case 3:
                    diceLineOne = new string(lineOfOne);
                    diceLineTwo = new string(lineOfOne);
                    diceLineThree = new string(lineOfOne);
                    break;
                case 4:
                    diceLineOne = new string(lineOfTwo);
                    diceLineTwo = new string(lineOfZero);
                    diceLineThree = new string(lineOfTwo);
                    break;
                case 5:
                    diceLineOne = new string(lineOfTwo);
                    diceLineTwo = new string(lineOfOne);
                    diceLineThree = new string(lineOfTwo);
                    break;
                case 6:
                    diceLineOne = new string(lineOfTwo);
                    diceLineTwo = new string(lineOfTwo);
                    diceLineThree = new string(lineOfTwo);
                    break;
            }

            Console.Out.WriteLine(
                //corner ------------ corner
                "\u231C\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u231D\n" +
                diceLineOne + "\n" +
                diceLineTwo + "\n" +
                diceLineThree + "\n" +
                "\u231E\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u231F\n"
                );
        }
    }
}