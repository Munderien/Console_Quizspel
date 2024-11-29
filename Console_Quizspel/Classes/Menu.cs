using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class Menu
    {
        public void menu()
        {
            Console.WriteLine("Welkom! \n 1: Speel de quiz \n 2: Bewerk de quiz \n 3: Exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (choice)
            {
                case 1:
                    PlayQuiz();
                    break;
                case 2:
                    AdminMenu();
                    break;
                case 3:
                    Exit();
                    break;
                default:
                    Console.WriteLine("Verkeerde waarde, Kies een geldige Waarde!");
                    menu();
                    break;
            }
        }

        static void PlayQuiz()
        {
            Console.Clear();
            GetData getData = new GetData();
            List<Question> questions = getData.getRandomQuestions();
            PlayQuiz playQuiz = new PlayQuiz();
            Menu menu = new Menu();

            if (questions.Count > 0)
            {
                playQuiz.inputAnswer(questions);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Er Zijn geen vragen gevonden");
                menu.menu();
            }
        }
        public void AdminMenu()
        {
            // ter info: ik wou hiervoor een switch case gebruiken, maar dat deed raar i.v.m de classes (geen idee waarom wil ik wel ter evaluatie op terig komen)
            Console.WriteLine("Wat voor aanpassingen wil je doen aan de Quiz? \n 1: Bekijk alle vragen \n 2: maak een vraag aan \n 3: pas een vraag aan \n 4: verwijder een vraag");
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            //Ruben: voorbeeld van hoe je hier een switch case kan gebruiken
            //Ruben: de rede waarom het denk ik niet werkte was omdat je meerdere keren een nieuwe class aanriep
            //Ruben: hij kijkt namelijk { hier tussen } als daartussen 2 keer GetData getData = new GetData(); staat dan werkt die dus niet omdat die er al in staat

            // Marijn: Dit komt vanuit ruben's voorbeeld
            GetData getData = new GetData();
            Question question = new Question();
            switch (choice)
            {
                case 1:
                    getData.getDataFromDatabase();
                    break;
                case 2:
                    MakeQuestion makeQuestion = new MakeQuestion();
                    makeQuestion.makeQuestion();
                    break;
                case 3:
                    getData.getDatabyID();
                    UpdateQuestion updateQuestion = new UpdateQuestion();
                    updateQuestion.askUpdateQuestion(question);
                    break;
                case 4:
                    getData.getDatabyID();
                    DeleteQuestion deleteQuestion = new DeleteQuestion();
                    deleteQuestion.askDeleteQuestion(question);
                    break;
            }
        }

        // Marijn: Dit is wat ik eerst had
            /*
            if (choice == 1)
            {
                GetData getData = new GetData();
                getData.getDataFromDatabase();
            }
            else if (choice == 2)
            {
                MakeQuestion makeQuestion = new MakeQuestion();
                makeQuestion.makeQuestion();
            }
            else if (choice == 3)
            {
                GetData getData = new GetData();
                getData.getDatabyID();
                UpdateQuestion updateQuestion = new UpdateQuestion();
                Question question = new Question();
                updateQuestion.askUpdateQuestion(question);
            }
            else if(choice == 4)
            {
                GetData getData = new GetData();
                getData.getDatabyID();
                DeleteQuestion deleteQuestion = new DeleteQuestion();
                Question question = new Question();
                deleteQuestion.askDeleteQuestion(question);
            }
            Console.ReadKey();
            Console.Clear();
            menu();
        }*/

        public void Exit()
        {
            //Ruben: hier zou je bijvoorbeeld de database kunnen sluiten
            ConnectDB db = ConnectDB.GetInstance();
            db.CloseConnection();
            Console.WriteLine("See you soon!");
            Environment.Exit(0);
        }
    }
}
