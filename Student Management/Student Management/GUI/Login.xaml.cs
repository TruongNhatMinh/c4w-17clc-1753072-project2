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

namespace Student_Management.GUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        ServiceInterface handle = new ServiceInterface();
        public Login(Components cP)
        {
            DataContext = cP;
            InitializeComponent();
        }

        private void accountAccess()
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(usernameTextBox.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordBox.Password);

            if (!isUsernameEmpty && !isPasswordEmpty)
            {
                state resultState = handle.checkStateAccess(usernameTextBox.Text, passwordBox.Password);
                if (resultState == state.accountIsNotExist)
                    MessageBox.Show("Tài khoản không tồn tại!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                else if (resultState == state.incorrectPassword)
                    MessageBox.Show("Mật khẩu không đúng!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                else
                {
                    BindingExpression bindingExpression = usernameTextBox.GetBindingExpression(TextBox.TextProperty);
                    bindingExpression.UpdateSource();
                    if (resultState == state.isManager)
                    {
                        MessageBox.Show("Đăng nhập thành công", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.NavigationService.Navigate(new menuManager(DataContext as Components));
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thành công", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                if (isPasswordEmpty && isUsernameEmpty)
                    MessageBox.Show("Bạn chưa nhập Tài Khoản và Mật khẩu", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (isUsernameEmpty)
                    MessageBox.Show("Bạn chưa nhập Tài khoản!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (isPasswordEmpty)
                    MessageBox.Show("Bạn chưa nhập Mật khẩu!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                accountAccess();

        }

        private void DangnhapButton_Click(object sender, RoutedEventArgs e)
        {
            accountAccess();
        }

    }
}
