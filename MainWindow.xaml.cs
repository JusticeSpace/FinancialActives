using System;
using System.Windows;
using System.Linq;
using EggplantsActivies.Roles;
using EggplantsActivies.Roles.Admin;
using EggplantsActivies.Users;
using EggplantsActivies.Data;

namespace EggplantsActivies
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (AuthenticateUser(login, password))
            {
                App.CurrentUserLogin = login;
                Console.WriteLine($"Пользователь {login} вошел в систему в {DateTime.Now:HH:mm:ss dd/MM/yyyy}");
                Window nextWindow = DetermineRoleWindow(login);
                nextWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AuthenticateUser(string login, string password)
        {
            using (var db = new ApplicationContext())
            {
                var user = db.UserManagers.FirstOrDefault(u => u.Login == login && u.Password == password);
                return user != null;
            }
        }

        private Window DetermineRoleWindow(string login)
        {
            // Простая логика ролей, как в Castle_IS
            if (login.ToLower() == "admin")
            {
                return new AdminWindow();
            }
            else
            {
                return new ProfileWindow();
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Свяжитесь с администратором для восстановления пароля.", "Восстановление пароля", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Регистрация доступна только через администратора.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}