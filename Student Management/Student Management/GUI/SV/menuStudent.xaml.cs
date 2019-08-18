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
        public menuStudent(Components _components)
        {
            DataContext = _components;
            InitializeComponent();
        }

        private void ViewScheduleCB_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void viewMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            Components _components = DataContext as Components;
            _components.NewScoreboard = handle.viewScoreboard(null, viewScheduleCB.SelectedItem.ToString());
            viewFrame.Navigate(new viewScoreboard(DataContext as Components));
        }

        private void ModifyPassword_Click(object sender, RoutedEventArgs e)
        {
            viewFrame.Navigate(new changePassword(DataContext as Components));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
