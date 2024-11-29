using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class ConnectDB
    {
        //Ruben: deze classe is er voor gemaakt om te zorgen dat je de database maar 1 keer hoeft te openen en 1 keer hoeft te sluiten als het programma afsluit.
        private static ConnectDB _databaseConnect = null;

        private readonly string mySqlCon = "server=localhost; user=root; database=quizspeldb; password=";

        private MySqlConnection mySqlConnection;

        private ConnectDB()
        {
            try
            {
                mySqlConnection = new MySqlConnection(mySqlCon);
                mySqlConnection.Open();
                //Console.WriteLine("Database connection established");//Ruben: misschien dit verwijderen zodat je als user niet ziet
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public MySqlConnection GetConnection()
        {
            return mySqlConnection;
        }

        public static ConnectDB GetInstance()
        {
            if (_databaseConnect == null)
            {
                _databaseConnect = new ConnectDB();
            }
            return _databaseConnect;
        }
        
        //Ruben: je hoeft alleenmaar de database te sluiten op het eind van het programma. de connectie blijft altijd open
        public void CloseConnection()
        {
            if (mySqlConnection != null && mySqlConnection.State == System.Data.ConnectionState.Open)
            {
                mySqlConnection.Close();
                //Console.WriteLine("close connection");//Ruben: misschien dit verwijderen zodat je als user niet ziet
            }
        }
    }

}
