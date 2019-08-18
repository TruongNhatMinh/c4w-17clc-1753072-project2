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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Student_Management.BUS;

namespace Student_Management
{
    /// <summary>
    /// Interaction logic for addStudent.xaml
    /// </summary>
    public partial class addStudent : Page
    {
        ServiceInterface handle = new ServiceInterface();
        public addStudent()
        {
            InitializeComponent();
        }

        private List<string> setValue()
        {
            List<string> information = new List<string>();
            information.Add(mssvtxtBox.Text);
            information.Add(hotentxtBox.Text);
            information.Add(gioitinhtxtBox.Text);
            information.Add(cmndtxtBox.Text);
            information.Add(datetxtBox.Text);
            information.Add(diachitxtBox.Text);
            information.Add(loptxtBox.Text);

            return information;
        }

        private void newStudent()
        {
            List<string> information = setValue();
            handle.addStudent(information);
            this.Content = null;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            newStudent();
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                newStudent();
            }

        }
    }
}
