using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class UpdateQuestion
    {
        public void askUpdateQuestion(Question question)
        {
            Console.WriteLine("Wat wordt de nieuwe vraag?");

            string response = Console.ReadLine();

            question.text = response;

            UpdateQuestionQuiz(question);
        }
        public void UpdateQuestionQuiz(Question question)
        {
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();

            string query = "UPDATE vraag SET content = @content WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", question.id);
                cmd.Parameters.AddWithValue("@content", question.text);

                cmd.ExecuteNonQuery();
                //Ruben: eigenlijk is dit niet nodig want je zou hier niet moeten kunnen komen als de vraag niet bestaad.
                //dit zegt eigenlijk dat je een vraag kan kiezen om aan te passen die niet bestaad.

                // Marijn: Hij is weggehaald

                Console.WriteLine($"Question met ID {question.id} is geupdate");

            }
        }
    }
}
