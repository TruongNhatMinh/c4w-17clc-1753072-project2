using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Student_Management.BUS;
using System.Data.OleDb;

namespace Student_Management.DAL
{
    /// <summary>
    /// Interaction logic for DB.xaml
    /// </summary>
    public partial class DB : Window
    {
        public DB()
        {
            InitializeComponent();
        }

        private List<User> getUser()
        {          
            var results = new List<User>();
            OleDbConnection cnn = new OleDbConnection();
            cnn.ConnectionString = "Server=DESKTOP-9MGEK4O\\SQLEXPRESS;Database=LoginDB;Trusted_Connection=True;";
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "Select * from listOfStudent";
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                var User = new User();
                User.Id = rd.GetInt32(0);
                User.Username = rd.GetString(1);
                User.Password = rd.GetString(2);
                results.Add(User);
            }
            cnn.Close();
            return results;
        }

        private User addUser(User newUser)
        {
            OleDbConnection cnn = new OleDbConnection();
            cnn.ConnectionString = "Server=DESKTOP-9MGEK4O\\SQLEXPRESS;Database=LoginDB;Trusted_Connection=True;";
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"insert into MYPRODUCT (Name) values ('{newUser.Username}'); SELECT SCOPE_IDENTITY()";
            var id = int.Parse(cmd.ExecuteScalar().ToString());
            newUser.Id = id;
            cnn.Close();
            return newUser;
        }
    }
}
