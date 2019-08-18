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

namespace Student_Management
{
    /// <summary>
    /// Interaction logic for editMark.xaml
    /// </summary>
    public partial class editMark : Page
    {
        ServiceInterface handle = new ServiceInterface();
        string _class = "", _courses = "";
        public editMark(string nCLass, string nCourses)
        {
            _class = nCLass;
            _courses = nCourses;
            InitializeComponent();
        }

        private List<string> setValue()
        {
            List<string> mark = new List<string>();
            mark.Add(mssvtxtBox.Text);
            mark.Add(diemgktxtBox.Text);
            mark.Add(diemcktxtBox.Text);
            mark.Add(diemkhactxtBox.Text);
            mark.Add(diemtongtxtBox.Text);

            return mark;
        }

        private void editMarkStudent()
        {
            Components _components = DataContext as Components;
            List<string> mark = setValue();
            if (checkMark(mark[1]) && checkMark(mark[2]) && checkMark(mark[3]) && checkMark(mark[4]) && isStudentExist(_class, mssvtxtBox.Text, _courses))
            {
                handle.editMark(mark);
                this.Content = null;
            }
            else
            {
                MessageBox.Show("Điểm nhập không hợp lệ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                mssvtxtBox.Text = diemgktxtBox.Text = diemcktxtBox.Text = diemkhactxtBox.Text = diemtongtxtBox.Text = "";

                mssvtxtBox.Focus();
            }
        }

        private bool isStudentExist(string nClass, string mssv, string nCourses)
        {
            
            return handle.isStudentExist(nClass, mssv, nCourses);
        }

        private bool checkMark(string _mark)
        {
            float mark;
            bool success = float.TryParse(_mark, out mark);
            if (mark < 0 || mark > 10 || !success)
                return false;
            return true;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            editMarkStudent();
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                editMarkStudent();
            }
        }
    }
}
