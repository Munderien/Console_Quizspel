using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class DeleteQuestion
    {
        public void askDeleteQuestion(Question question)
        {
            Console.WriteLine("Weet je zeker dat je de vraag en antwoorden wilt verwijderen?");

            string response = Console.ReadLine();

            response.ToLower();

            if (response == "ja")
            {

                deleteQuestion(question);

            }
        }
        public void deleteQuestion(Question question)
        {
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();
            //Ruben: deze conn.open heeft geen effect op het werken van de code zodra je db.getconnection aanroept krijg je al een open connectie.
            //conn.Open();
            string query = "DELETE FROM vraag WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", question.id);
                cmd.ExecuteNonQuery();
                Console.WriteLine(question.id.ToString());
            }


        }
    }
}
