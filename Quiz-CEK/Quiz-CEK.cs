using System;

namespace Quiz_CEK
{
    class QuizCEK
    {
        static void Main(string[] args)
        {
            new Question("Whats 2+2?", "4", new [] {"3", "5", "1.321x10^24"}).print();
        }
    }

    class Question
    {
        private static Random rand;
        private String question;
        private String correctAnswer;
        private String[] wrongAnswers;

        public Question(String question, String correctAnswer, String[] wrongAnswers)
        {
            if (rand == null)
                rand = new Random();
            
            this.question = question;
            this.correctAnswer = correctAnswer;
            this.wrongAnswers = wrongAnswers;
        }

        public void print()
        {
            int correctAnswerPos = rand.Next(wrongAnswers.Length);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(question);
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < wrongAnswers.Length; i++)
            {
                if (i == correctAnswerPos) {
                    Console.WriteLine(NumberToString(i) + ") "+correctAnswer);
                }
                else
                {
                    Console.WriteLine(NumberToString(i+1) + ") "+wrongAnswers[i]);
                }
                
                Console.WriteLine(NumberToString(i) + ") "+wrongAnswers[i]);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Answer: ");
            char input;
            try
            {
                input = Convert.ToChar(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!");
                print();
                return;
            }

            if (NumberFromString(Convert.ToChar(input)) == correctAnswerPos)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect! Correct answer was "+NumberToString(correctAnswerPos)+".");
            }
        }
        
        private static string NumberToString(int number)
        {
            char c = (char)(65 + number);
            return c.ToString();
        }
        
        private int NumberFromString(char character)
        {
            return char.ToUpper(character) - 65;
        }
    }
}