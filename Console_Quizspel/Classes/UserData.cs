using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class UserData
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public UserData(string name, int score) 
        {
            this.Name = name;
            this.Score = score;
        }

        public void insertUserIntoDB()
        {
            ConnectDB db = ConnectDB.GetInstance();
            MySqlConnection conn = db.GetConnection();

            string query = "INSERT into user(userName, userScore) VALUES(@userName, @userScore)";
            //Ruben: dit heeft geen effect de db.getconnection zorgt er voor dat je de open connectie krijgt die die open zet als de instance voor het eerst word aangeroepen
            // Marijn: Hij is weggehaald

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", Name);
                    cmd.Parameters.AddWithValue("@userScore", Score);

                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine($"Jouw score is: {Score} punten");
                Console.WriteLine("userName en userScore toegevoegd aan de database!");
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
