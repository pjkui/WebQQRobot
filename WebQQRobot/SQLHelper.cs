using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebQQRobot
{
    public class SQLHelper
    {
        public static string ConnectionStr = "Server=localhost;Database=qq_robot; User=root;Password=;Use Procedure Bodies=false;Charset=utf8;Allow Zero Datetime=True; Pooling=false; Max Pool Size=50;";

        public static int ExecuteSQL(string sql, string question, string answer)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@question", question));
            cmd.Parameters.Add(new MySqlParameter("@answer", answer));
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }

        public static int ExecuteSQL(string sql, string question)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@question", question));
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }

        public static DataTable QuerySQL(string sql, string question)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@question", question));
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }
    }
}
