using System;

namespace Quiz_CEK
{
    class QuizCek
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("");
            
            //TODO track correct answers.
            
            new Question("How many teams are in the NFL?", "32", 
                new [] {"42", "26", "50"}).Print();
            new Question("What was the US population in 1920?", "107M", 
                new [] {"64M", "37M", "49M", "100M"}).Print();
            new Question("How many moles of carbon in 1.204x10^24 particles of carbon?", "2.0*10^0", 
                new [] {"3.42*10^-12", "4.72*10^3", "2", "I refuse to revisit my dark past with moles."}).Print();
            
            
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Question
    {
        private static Random rand;
        private string question;
        private string correctAnswer;
        private string[] wrongAnswers;

        public Question(String question, String correctAnswer, String[] wrongAnswers)
        {
            if (rand == null)
                rand = new Random();
            
            this.question = question;
            this.correctAnswer = correctAnswer;
            this.wrongAnswers = wrongAnswers;
        }

        public void Print()
        {
            //Space out console
            Console.WriteLine("");
            Console.WriteLine("");
            
            int correctAnswerPos = rand.Next(wrongAnswers.Length);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(question);
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < wrongAnswers.Length; i++)
            {
                if (i == correctAnswerPos) {
                    Console.WriteLine(NumberToString(i) + ") "+correctAnswer);
                    Console.WriteLine(NumberToString(i+1) + ") "+wrongAnswers[i]);
                }
                else if (i > correctAnswerPos)
                {
                    Console.WriteLine(NumberToString(i+1) + ") "+wrongAnswers[i]);
                }
                else
                {
                    Console.WriteLine(NumberToString(i) + ") "+wrongAnswers[i]);
                }
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
                Print();
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