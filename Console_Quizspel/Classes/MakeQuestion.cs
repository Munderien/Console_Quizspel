using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class MakeQuestion
    {
        public void makeQuestion()
        {
            // de ? zorgt ervoor als er wat fout is dat het antwoord null is.
            // en de trim haalt alle onnodige whitespace weg
            InsertQuesionIntoDB insertIntoDB = new InsertQuesionIntoDB();
            Console.WriteLine("Wat is de vraag voor de Quiz?");
            string questionText = Console.ReadLine()?.Trim();
            List<Answer> answers = new List<Answer>();
            string response;
            do
            {
                Console.WriteLine("Wilt u een antwoord toevoegen?");
                response = Console.ReadLine();
                response.ToLower();
                if (response == "ja")
                {
                    answers.Add(makeAnswer());
                }
            } while (response == "ja");

            Question question = new Question();
            question.text = questionText;
            question.answers = answers;
            insertIntoDB.insertQuestionIntoDB(question);
        }
        //Ruben: deze static is niet persee nodig en je weet hoe ik over onnodige static denk hahaha
        // Marijn: De static is weggehaald XD
        public Answer makeAnswer()
        {
            // de ? zorgt ervoor als er wat fout is dat het antwoord null is.
            // en de trim haalt alle onnodige whitespace weg
            Console.WriteLine("Wat voor antwoord wil je op deze vraag? ");
            string answerText = Console.ReadLine()?.Trim();
            Console.WriteLine("Is dit antwoord correct?");
            string respone = Console.ReadLine()?.Trim().ToLower();
            Answer answer = new Answer(answerText, respone == "ja");
            return answer;
        }

        //Ruben: heb nog beetje mijn vraagtekens op hoe deze methode werkt zover ik nu zie lijkt het dat er dubbele/onnodige dingen worden gedaan
        // Marijn: Hier checkt hij of de vraag is gesteld en dat hij niet opnieuw wordt gesteld achterelkaar, Daarom ook de if statements.
        // Sommige delen van dit zijn door chatgpt geschreven i.v.m wat errors die ik hier had zoals null exceptie en vragen die opnieuw worden gesteld terwijl dit niet de bedoeling is
        public List<Question> CreateQuestionsFromReader(MySqlDataReader dr)
        {
            List <Question> questions = new List<Question>();
            int currentQuestionId = -1;
            Question currentQuestion = null;


            // leest de antwoorden uit van de database 
            while (dr.Read())
            {
                int questionId = dr["question_id"] != DBNull.Value ? Convert.ToInt32(dr["question_id"]) : -1;
                string questionContent = dr["vraag_content"] != DBNull.Value ? dr["vraag_content"].ToString() : "Geen vraag";
                string answerText = dr["answer_text"] != DBNull.Value ? dr["answer_text"].ToString() : "geen antwoord";
                bool answerCorrect = dr["answer_correct"] != DBNull.Value && Convert.ToBoolean(dr["answer_correct"]);

                // hier voegt hij de vraag toe aan de list
                if (questionId != currentQuestionId)
                {
                    if (currentQuestion != null)
                    {
                        questions.Add(currentQuestion);
                    }

                    currentQuestion = new Question
                    {
                        id = questionId,
                        text = questionContent,
                        answers = new List<Answer>()
                    };

                    currentQuestionId = questionId;
                }
                currentQuestion.answers.Add(new Answer(answerText, answerCorrect));
            }
            // als de vraag nog niet is weergeven, voegt hij de vraag toe
            if (currentQuestion != null)
            {
                questions.Add(currentQuestion);
            }

            return questions;
        }
    }
}
