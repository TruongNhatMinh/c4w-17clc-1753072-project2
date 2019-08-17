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
    /// Interaction logic for changePassword.xaml
    /// </summary>
    public partial class changePassword : Page
    {
        public changePassword(Components _components)
        {
            DataContext = _components;
            InitializeComponent();
        }

        private void _changePassword()
        {
            if (string.IsNullOrEmpty(oldPasswordBox.Password) || string.IsNullOrEmpty(newPasswordBox.Password) || string.IsNullOrEmpty(confirmBox.Password))
            {
                
            }
            else if (newPasswordBox.Password.Equals(oldPasswordBox.Password))
            {

            }
            else if (!newPasswordBox.Password.Equals(confirmBox.Password))
            {

            }
            else
            {
                ServiceInterface handle = new ServiceInterface();
                Components _components = DataContext as Components;
                if (handle.checkStateAccess(_components.CurrentAccount, oldPasswordBox.Password) == state.incorrectPassword)
                {

                }
                else
                {
                    if (handle.modifyPassword(_components.CurrentAccount, oldPasswordBox.Password, newPasswordBox.Password))
                    {
                        this.Content = null;
                    }
                    else
                    {

                    }
                }
            }
        }

        private void DoimatkhauButton_Click(object sender, RoutedEventArgs e)
        {
            _changePassword();
        }

        private void ConfirmBox_KeyDown(object sender, KeyEventArgs e)
        {
            _changePassword();
        }
    }
}
