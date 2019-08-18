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
        public editMark()
        {
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
            handle.editMark(mark);
            this.Content = null;
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
