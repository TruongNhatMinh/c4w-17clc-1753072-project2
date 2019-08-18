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
    /// Interaction logic for deleteStudent.xaml
    /// </summary>
    public partial class deleteStudent : Page
    {

        ServiceInterface handle = new ServiceInterface();
        public deleteStudent(bool del)
        {
            InitializeComponent();
            mamonCB.Items.Clear();
            if (!del)
            {
                lSign.Content = "ĐĂNG KÝ MÔN HỌC";
            }            
        }

        private void MamonCB_DropDownOpened(object sender, EventArgs e)
        {
            List<string> nCourses = handle.nameCourses(loptxtBox.Text);


            foreach (string _courses in nCourses)
            {
                mamonCB.Items.Add(_courses);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (lSign.Content.ToString() == "ĐĂNG KÝ MÔN HỌC")
            {
                handle.signCourses(mssvtxtBox.Text, loptxtBox.Text, mamonCB.SelectedItem.ToString());
                this.Content = null;
            }
            else
            {
                handle.deleteStudent(mssvtxtBox.Text, loptxtBox.Text, mamonCB.SelectedItem.ToString());
                this.Content = null;
            }
        }
    }
}
