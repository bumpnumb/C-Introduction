using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Server.services
{
    class database
    {

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
                Console.WriteLine("Connection to db has been made!\n");
                string query = "SELECT * FROM Daniel";
                var cdm = new MySqlCommand(query, db);
                var reader = cdm.ExecuteReader();

                Console.WriteLine("Querrying:\n" + query);

                while (reader.Read())
                {
                    person temp = new person();
                    temp.Name = reader.GetString("Namn");
                    temp.Betyg = reader.GetString("Betyg");
                    temp.Yes = reader.GetInt32("Yes");
                    temp.Bobby = reader.GetString("Bobby_Tables");
                    temp.print();
                }

                db.Close();
                Console.WriteLine("\nClosed db connection!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

