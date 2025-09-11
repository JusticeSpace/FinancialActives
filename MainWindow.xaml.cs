using System;
using System.Windows;
using System.Windows.Controls;
using EggplantsActivies.Data;
using EggplantsActivies.Roles;
using EggplantsActivies.Users;  // Для ProfileWindow

namespace EggplantsActivies
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseHelper _dbHelper;

        public MainWindow()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;  // Убедитесь, что TextBox имеет x:Name="LoginTextBox"
            string password = PasswordBox.Password;  // Убедитесь, что PasswordBox имеет x:Name="PasswordBox"

            if (_dbHelper.AuthenticateUser(login, password))
            {
                ProfileWindow profileWindow = new ProfileWindow();
                profileWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Свяжитесь с менеджером для восстановления пароля");
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Регистрация доступна только через менеджера");
        }
    }
}