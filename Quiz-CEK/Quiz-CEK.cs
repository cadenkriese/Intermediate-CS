using System;

/***********************************************************************
*
*  Filename : quiz-cek.cs
*  Author : Caden Kriese
*  Purpose : Second only to the SAT, the CEK quiz is one of the most challenging
*  pop quizzes in the world. Utilizing revolutionary technology such as
*  Object Oriented Programing the quiz is one of the most difficult to overcome
*  in any computer science teachers career.
*
***********************************************************************/

namespace Quiz_CEK
{
    class QuizCek
    {
        private static int correctAnswers;

        private static Question[] questions = {
            new Question("How many teams are in the NFL?", "32",
                new[] {"42", "26", "50"}),
            
            new Question("What was the US population in 1920?", "107M",
                new[] {"64M", "37M", "49M", "100M"}),
            
            new Question("How many moles of carbon in 1.204x10^24 particles of carbon?", "2.0*10^0",
                new[] {"3.42*10^-12", "4.72*10^3", "4", "I refuse to revisit my dark past with moles."}),
            
            new Question("Who's your favorite student?", "C.J.", 
                new []{"See Jay", "CJ", "KJ", "Caden", "Colton"}),
            
            new Question("What does GNU stand for?", "I give up with recursive acronyms", 
                new []{"GNU is not unix.", "GNU NU is not unix.", "GNU NU NU is not unix", "GNU NU NU NU is not unix."}) 
        };
        
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("");

            //Ask all the questions.
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].Ask();
                if (questions[i].Check())
                {
                    correctAnswers++;
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            double correctAnswerPercent = correctAnswers / (double) questions.Length * 100;
            Console.WriteLine("You got "+correctAnswers+" / "+questions.Length+" questions correct. ("+correctAnswerPercent+"%)");
            
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Question
    {
        private static Random rand;
        private int correctAnswerPos;
        private string question;
        private string correctAnswer;
        private string[] wrongAnswers;

        public Question(string question, string correctAnswer, string[] wrongAnswers)
        {
            if (rand == null)
                rand = new Random();
            
            this.question = question;
            this.correctAnswer = correctAnswer;
            this.wrongAnswers = wrongAnswers;
        }

        public void Ask()
        {
            //Space out console
            Console.WriteLine("");
            Console.WriteLine("");
            
            correctAnswerPos = rand.Next(wrongAnswers.Length);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(question);
            Console.ForegroundColor = ConsoleColor.Green;

            //Loop through all the wrong answers and put in the correct answer at a random spot.
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
        }
        
        public bool Check()
        {
            bool answerProvided = false;

            while (!answerProvided)
            {
                //Prompt for answer
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Answer: ");
                string rawInput = Console.ReadLine();
                int numericalInput = -1;

                try
                {
                    numericalInput = NumberFromString(Convert.ToChar(rawInput));
                }
                catch
                {
                    if (!rawInput.ToLower().Equals(correctAnswer))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input!");
                        //You let me do this.
                        continue;
                    }
                }
                
                if (rawInput.ToLower().Equals(correctAnswer) || numericalInput == correctAnswerPos)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                    return true;
                }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect! Correct answer was " + NumberToString(correctAnswerPos) + ".");
                    return false;
            }

            //Unreachable but necessary for building.
            return false;
        }
        
        private string NumberToString(int number)
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