using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedagoška_sveska.Resursi
{
    class DataBase
    {
        static string server = "localhost";
        static string database = "Maturski rad";
        static string username = "root";
        static string password = "";
        static string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID = " + username + ";" + "PASSWORD = " + password + ";";
        public MySqlConnection conn = new MySqlConnection(constring);
        
        public bool Connect_db()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool Close_db()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
