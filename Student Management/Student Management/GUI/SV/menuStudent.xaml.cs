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
    /// Interaction logic for menuStudent.xaml
    /// </summary>
    public partial class menuStudent : Page
    {
        ServiceInterface handle = new ServiceInterface();
        string _class = "";
        public menuStudent(Components _components)
        {
            DataContext = _components;
            List<string> nClass = handle.nameClass(_components.CurrentAccount);
            _class = nClass[0];
            List<string> nCourses = handle.nameCourses(nClass[0]);

            InitializeComponent();

            foreach (string _courses in nCourses)
            {
                viewScheduleStuCB.Items.Add(_courses);
            }
        }

        private void ViewScheduleStuCB_DropDownClosed(object sender, EventArgs e)
        {
            viewMarkBtn.IsEnabled = true;
        }

        private void viewMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            Components _components = DataContext as Components;
            _components.NewScoreboard = handle.viewScoreboard(_class, _components.CurrentAccount, viewScheduleStuCB.SelectedItem.ToString());
            viewFrame.Navigate(new viewScoreboard(DataContext as Components, null));
        }

        private void ModifyPassword_Click(object sender, RoutedEventArgs e)
        {
            viewFrame.Navigate(new changePassword(DataContext as Components));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
            //viewScheduleStuCB.Items.Clear();
        }
    }
}
