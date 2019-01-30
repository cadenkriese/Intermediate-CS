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

        public void roll()
        {
            //Random int 1-6
            int next = random.Next(5) + 1;
            
            //Preset lines
            String lineOfZero = "\u007c            \u007c";
            String lineOfOne = "\u007c      \u2022      \u007c";
            String lineOfTwo = "\u007c   \u2022     \u2022    \u007c";

            //Lines that will be displayed
            String diceLineOne = "";
            String diceLineTwo = "";
            String diceLineThree = "";

            //Define layouts for each dice version.          
            switch (next)
            {
                case 1:
                    diceLineOne = new String(lineOfZero);
                    diceLineTwo = new String(lineOfOne);
                    diceLineThree = new String(lineOfZero);
                    break;
                case 2 :
                    diceLineOne = new String(lineOfZero);
                    diceLineTwo = new String(lineOfTwo);
                    diceLineThree = new String(lineOfZero);
                    break;
                case 3 :
                    diceLineOne = new String(lineOfOne);
                    diceLineTwo = new String(lineOfOne);
                    diceLineThree = new String(lineOfOne);
                    break;
                case 4 :
                    diceLineOne = new String(lineOfTwo);
                    diceLineTwo = new String(lineOfZero);
                    diceLineThree = new String(lineOfTwo);
                    break;
                case 5 :
                    diceLineOne = new String(lineOfTwo);
                    diceLineTwo = new String(lineOfOne);
                    diceLineThree = new String(lineOfTwo);
                    break;
                case 6 :
                    diceLineOne = new String(lineOfTwo);
                    diceLineTwo = new String(lineOfTwo);
                    diceLineThree = new String(lineOfTwo);
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