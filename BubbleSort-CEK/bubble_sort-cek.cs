using System;

/***********************************************************************
*
*  Filename : bubble_sort-cek.cs
*  Author : Caden Kriese
*  Purpose : The bubble sort takes a input of numbers and outputs them
*      back to the user sorted smallest to largest, or bubble sorted. 
*
***********************************************************************/
namespace BubbleSort_CEK
{
    internal static class BubbleSortCek
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Input a sequence of numbers all on one line: ");
            String input = Console.In.ReadLine();
            
            //Ensure they actually input something.
            if (input == null) return;
            
            //Erase any commas from their input to be more user friendly.
            input = input.Replace(",", "");
            //Split the inputs and convert them all to numbers.
            int[] splitNumbers = Array.ConvertAll(input.Split(" "), int.Parse);
            
            //Bubble sort.
            splitNumbers = Sort(splitNumbers);

            //Output
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sorted:");
            Console.WriteLine(string.Join(", ", splitNumbers));

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Sorts the input from smallest to largest.
        /// </summary>
        /// <param name="arrayToSort">The array of integers that should be sorted.</param>
        /// <returns>The given array with the numbers sorted where int[0] is the smallest
        /// and int[int.length-1] is the largest</returns>
        private static int[] Sort(int[] arrayToSort)
        {
            //  Sort array (Bubble Sort)
            for (int i = 0; i < arrayToSort.Length-1; i++) {
                for (int j = i+1; j < arrayToSort.Length; j++) {
                    if (arrayToSort[i] > arrayToSort[j]) {
                        int temp = arrayToSort[i];      // Swap the values
                        arrayToSort[i] = arrayToSort[j];
                        arrayToSort[j] = temp;
                    }
                }
            }

            return arrayToSort;
        }
    }
}