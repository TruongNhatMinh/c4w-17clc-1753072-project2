using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;


namespace Student_Management.DAL
{
    class DataAccess
    {
        string conectionString = "Provider=SQLNCLI11;Server=DESKTOP-9MGEK4O\\SQLEXPRESS;Database=QLSV;Trusted_Connection=yes;";
        OleDbConnection cnn;
        public DataAccess()
        {
            cnn = new OleDbConnection();
            cnn.ConnectionString = conectionString;
        }

        public bool isAccountExist(string username)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"Select Acc.Password FROM Account Acc WHERE Acc.Username= ?";
            cmd.Parameters.AddWithValue("@Username", username);
            var rd = cmd.ExecuteReader();
            bool res = rd.Read();
            cnn.Close();
            return res;
        }

        public int comparePassword(string username, string password)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"Select Acc.Password, Acc.Type FROM Account Acc WHERE Acc.Username = ?";
            cmd.Parameters.AddWithValue("@Username", username);
            var rd = cmd.ExecuteReader();
            int state = 0;
            if (rd.Read())
            {
                if (password.Equals(rd.GetString(0)))
                {
                    if (rd.GetString(1).Contains("gv"))
                        state = 3;
                    else
                        state = 2;
                }
            }
            cnn.Close();
            return state;
        }
    }
}
