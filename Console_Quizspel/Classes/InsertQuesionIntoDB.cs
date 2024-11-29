using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class InsertQuesionIntoDB
    {
        public void insertQuestionIntoDB(Question question)
        {
            // maak nieuwe question aan in de database op basis van 'question'
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();

            string query = "INSERT INTO vraag (content) VALUES (@content); SELECT LAST_INSERT_ID();";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@content", question.text);

            // execute de command and gebruikt de SELECT LAST_INSERT_ID(); query
            // ook gooit hij de id wat is gebruikt in de int new id en daarna in de question.ID
            int newId = Convert.ToInt32(cmd.ExecuteScalar());
            question.id = newId;

            // haal het id op van de zojuist aangemaakt question in de database
            // loop over alle answers van de question en roep dan insertAnswer(questionid, answer object)

            foreach (var answer in question.answers)
            {
                insertAnswer(question.id, answer);
            }
            db.CloseConnection();
        }
        public void insertAnswer(int question_id, Answer answer)
        {
            // insert statement voor het antwoord
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();
            string query = "INSERT INTO answer (question_id, content, correct) VALUES (@question_id, @content, @correct)";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@question_id", question_id);
                    cmd.Parameters.AddWithValue("@content", answer.Text);
                    cmd.Parameters.AddWithValue("@correct", answer.Correct);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        
    }
}
