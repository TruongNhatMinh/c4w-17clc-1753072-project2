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
using System.IO;
using Microsoft.Win32;
using Student_Management.BUS;

namespace Student_Management
{
    /// <summary>
    /// Interaction logic for menuManager.xaml
    /// </summary>
    public partial class menuManager : Page
    {
        ServiceInterface handle = new ServiceInterface();
        public menuManager(Components _components)
        {
            DataContext = _components;
            InitializeComponent();
        }

        private string getImportFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog() { Filter = "csv files (*.csv)|*.csv" };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        private void addClass_Click(object sender, RoutedEventArgs e)
        {
            string filePath = getImportFilePath();
            if (string.IsNullOrEmpty(filePath)) return;
            bool add = true;
            if (handle.isClassExist(filePath))
            {
                if (MessageBox.Show($"Lớp {System.IO.Path.GetFileNameWithoutExtension(filePath)} đã tồn tại. Bạn có muốn ghi đè ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    add = true;
            }
            if (add)
            {
                Components _components = DataContext as Components;
                _components.NewClass = handle.addClass(filePath);
                managerFrame2.Navigate(new viewClass(DataContext as Components));
            }
        }

        private void addStudent_Click(object sender, RoutedEventArgs e)
        {
            managerFrame2.Navigate(new addStudent());
        }

        private void addSchedule_Click(object sender, RoutedEventArgs e)
        {
            string filePath = getImportFilePath();
            if (string.IsNullOrEmpty(filePath)) return;

            Components _components = DataContext as Components;
            _components.NewSchedule = handle.addSchedule(filePath);
            managerFrame2.Navigate(new viewSchedule(DataContext as Components));
        }

        private void viewSchedule_Click(object sender, RoutedEventArgs e)
        {
        }

        private void addScoreboard_Click(object sender, RoutedEventArgs e)
        {
            string filePath = getImportFilePath();
            if (string.IsNullOrEmpty(filePath)) return;

            Components _components = DataContext as Components;
            _components.NewScoreboard = handle.addScoreboard(filePath);
            managerFrame2.Navigate(new viewScoreboard(DataContext as Components));
        }


        private void viewClass_Click(object sender, RoutedEventArgs e)
        {
            List<string> nClass = handle.nameClass();
            managerFrame1.Visibility = Visibility.Hidden;
            managerFrame2.Content = null;
            viewFrame.Visibility = Visibility.Visible;

            foreach (string _class in nClass)
            {
                viewClassCB.Items.Add(_class);
            }
        }

        private void ViewClassCB_DropDownClosed(object sender, EventArgs e)
        {
            List<string> nCourses = handle.nameCourses(viewClassCB.SelectedItem.ToString());
            viewScheduleCB.Items.Clear();

            foreach (string _courses in nCourses)
            {
                viewScheduleCB.Items.Add(_courses);
            }

            viewClassBtn.IsEnabled = true;
            viewScheduleBtn.IsEnabled = true;
            viewScheduleCB.IsEnabled = true;
        }

        private void viewClassBtn_Click(object sender, RoutedEventArgs e)
        {
            Components _components = DataContext as Components;
            _components.NewClass = handle.viewClass(viewClassCB.SelectedItem.ToString());
            managerFrame2.Navigate(new viewClass(DataContext as Components));
        }

        private void viewScheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            Components _components = DataContext as Components;
            _components.NewSchedule = handle.viewSchedule(viewClassCB.SelectedItem.ToString());
            managerFrame2.Navigate(new viewSchedule(DataContext as Components));
        }

        private void ViewScheduleCB_DropDownClosed(object sender, EventArgs e)
        {
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            managerFrame1.Visibility = Visibility.Visible;
            viewFrame.Visibility = Visibility.Hidden;
            managerFrame2.Content = null;
            viewClassCB.Items.Clear();
            viewScheduleCB.Items.Clear();
            viewClassBtn.IsEnabled = false;
            viewScheduleBtn.IsEnabled = false;
            viewScheduleCB.IsEnabled = false;
            viewMarkBtn.IsEnabled = false;
            editMarkBtn.IsEnabled = false;
        }

        private void viewMarkBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void editMarkBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void signObject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyPassword_Click(object sender, RoutedEventArgs e)
        {
            managerFrame2.Navigate(new changePassword(DataContext as Components));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
