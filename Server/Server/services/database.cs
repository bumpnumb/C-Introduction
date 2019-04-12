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
        private const string connectionString = "Server=aass.oru.se; database=c5_dt117g_vt19; UID=c5_dt117g_vt19; password=dt117g_vt19";

        private static MySqlConnection db = null;


        public static void ExecuteServer()
        {
            db = new MySqlConnection(connectionString);
        }

        public static void query(string query)
        {
            db.Open();
            var cdm = new MySqlCommand(query, db);

            using (MySqlDataReader reader = cdm.ExecuteReader())
            {
                while (reader.Read())
                {
                    String level = reader["Level"].ToString();
                    String code = reader["Code"].ToString();
                    String message = reader["Message"].ToString();
                    Console.WriteLine(level + ' ' + code + ' ' + message);
                }
            }
            db.Close();
        }

        public static void TestConnection()
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



        //Here we put the sql commands


        public static bool UserExists(string UID)
        {
            //INSERT INTO `c5_dt117g_vt19`.`DanielUsers` (`ID`, `Name`, `Salt`, `Password`) VALUES ();
            //SELECT Name FROM `DanielUsers` WHERE ID=1
            //string query = string.Format("SELECT Name FROM 'DanielUsers' WHERE Name = {0};", UID);
            //string query = string.Format("SELECT EXISTS(SELECT * FROM DanielUsers WHERE Name={0});", UID);
            string query = string.Format("SELECT EXISTS(SELECT * FROM `DanielUsers` WHERE(Name = '{0}'));", UID);
            database.query(query);
            return false;
        }
    }
}

