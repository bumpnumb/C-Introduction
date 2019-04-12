using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //MySqlConnection db = new MySqlConnection();
            MySqlConnection db = ExecuteServer();
            TestConnection(db);

            Console.ReadLine();
        }
        public static MySqlConnection ExecuteServer()
        { 
            string connectionString = string.Format("Server=aass.oru.se; database=c5_dt117g_vt19; UID=c5_dt117g_vt19; password=dt117g_vt19");
            MySqlConnection con = new MySqlConnection(connectionString);
            return con;
        }

        public static void TestConnection(MySqlConnection db)
        {
            try
            {
                db.Open();
                string query = "SELECT * FROM Daniel";
                var cdm = new MySqlCommand(query, db);
                var reader = cdm.ExecuteReader();
                Console.WriteLine(query);

                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(1));
                }

                db.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
