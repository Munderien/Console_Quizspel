using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class PlayQuiz
    {
        // hier is chatgpt voor gebruikt i.v.m vragen checken of het goed was of niet 
        public void inputAnswer(List<Question> questions)
        {
            Console.WriteLine("Voer je gebruikersnaam in:");
            string name = Console.ReadLine();
            Console.Clear();

            int score = 0;
            foreach (var question in questions)
            {
                Console.WriteLine($"Vraag: {question.text}");

                
                // laat alle vragen zien en het bijbehorende nummer
                for (int i = 0; i < question.answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.answers[i].Text}");
                }

                // kijkt naar jouw keuze op basis van het nummer ipv de content van de vraag
                Console.WriteLine("Jouw Antwoord? (Type het nummer naar jouw keuze)");
                if (int.TryParse(Console.ReadLine(), out int userChoice) &&
                    userChoice > 0 &&
                    userChoice <= question.answers.Count)
                {
                    // Check if the selected answer is correct
                    if (question.answers[userChoice - 1].Correct)
                    {
                        Console.Clear();
                        Console.WriteLine("Correct! \n");
                        score++;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Incorrect! \n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Moving to the next question. \n");
                }
            }
            UserData userdata = new UserData(name, score);
            userdata.insertUserIntoDB();
        }
    }
}
