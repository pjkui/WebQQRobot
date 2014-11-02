using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace CoreComponent
{
    public class SQLHelper
    {
        public static string SqlConnectionStr
        {
            get
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                return "Data Source=" + path + "Data.db";
            }
        }

        public static int ExecSql(string sql)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqlConnectionStr))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(conn);
                    cmd.CommandText = sql;
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch
            {
                return -1;
            }
            
        }

        public static DataSet QuerySql(string sql)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqlConnectionStr))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(conn);
                    cmd.CommandText = sql;
                    SQLiteDataAdapter adpt = new SQLiteDataAdapter(cmd);
                    DataSet set = new DataSet();
                    adpt.Fill(set);
                    return set;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
