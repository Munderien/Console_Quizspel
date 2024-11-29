using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class GetData
    {
        //Ruben: deze class heet getdate de naam zecht eigenlijk dat je dus al het data krijgt. ik zie ook dat je hierin ook data gebruikt en laat zien
        //dit zou je misschien iets anders kunnen inrichten dat je de data ophaald en in een andere class de data gebruikt dat maakt het een stuk overzichtelijker

        //Ruben: deze methode heet getDataFromDatabase zou misschien een betere naam kunnen worden bijvoorbeeld getAllAwnsersByQuestion
        public void getDataFromDatabase()
        {            
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();

            //Ruben: deze query is nodig en lijkt mij prima voor de structuur die je hebt bij je programma omdat je ook in deze methode de vraag laat zien
            // Marijn: In vervolg wil ik dit gaan doen door middel van een list
            // pakt de vraag en de antwoorden wat gelinked is aan de vraag
            string query = "SELECT vraag.id AS vraag_id, vraag.content AS vraag_content, answer.content AS answer_content, answer.correct " +
               "FROM vraag " +
               "JOIN answer ON vraag.id = answer.question_id " +
               "ORDER BY vraag.id";


            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            string currentQuestionContent = null;

            while (dr.Read())
            {
                string vraagId = dr["vraag_id"].ToString();
                string vraagContent = dr["vraag_content"].ToString();
                string answerContent = dr["answer_content"].ToString();
                bool correct = Convert.ToBoolean(dr["correct"]);

                if (vraagContent != currentQuestionContent)
                {
                    Console.WriteLine($"Vraag: {vraagId} {vraagContent} \n");
                    currentQuestionContent = vraagContent;
                }
                Console.WriteLine($"Antwoord: {answerContent} | Correct: {correct} ");
            }

            db.CloseConnection();
        }

        public void getDatabyID()
        {
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();

            Console.WriteLine("Selecteer de vraag die je wilt aanpassen/verwijderen op basis van hun nummer:");
            int inputID = int.Parse(Console.ReadLine());

            Console.Clear();
            string query = "SELECT id, content FROM vraag WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", inputID);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) 
                    {
                       Question question = new Question
                        {
                            id = dr.GetInt32(0),
                            text = dr.GetString(1)
                        };
                        db.CloseConnection();
                        Console.WriteLine($"id: {question.id} vraag: {question.text}");

                    }
                    else
                    {
                        Console.WriteLine("Geen vraag gevonden op basis van de ID");
                    }
                }
            }

            //db.CloseConnection();
            Console.Clear();
        }

        public List<Question> getRandomQuestions()
        {
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();
            MakeQuestion makeQuestion = new MakeQuestion();
            try
            {
                // pakt alle vragen en bijbehorende antwoorden
                string query = @"SELECT vraag.id AS question_id, 
                vraag.content AS vraag_content, 
                answer.content AS answer_text, 
                answer.correct AS answer_correct 
         FROM vraag 
         LEFT JOIN answer ON vraag.id = answer.question_id;";


                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    return makeQuestion.CreateQuestionsFromReader(dr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fout bij het ophalen van de vraag: " + ex.Message);
                return new List<Question>();
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    db.CloseConnection();
                }
            }
        }  
    }  
}
