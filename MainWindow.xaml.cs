using System;
using System.Windows;
using EggplantsActivies.Data;
using EggplantsActivies.Roles;
using EggplantsActivies.Users; // Для ProfileWindow

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
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (_dbHelper.AuthenticateUser(login, password))
            {
                Console.WriteLine($"User {login} logged in at {DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")}");
                ProfileWindow profileWindow = new ProfileWindow();
                profileWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Свяжитесь с менеджером для восстановления пароля.");
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Регистрация доступна только через менеджера.");
        }

        protected override void OnClosed(EventArgs e)
        {
            _dbHelper?.Dispose();
            base.OnClosed(e);
        }
    }
}