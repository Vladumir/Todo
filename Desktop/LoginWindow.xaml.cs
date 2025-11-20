using System.Windows;
using System.Windows.Controls;

namespace Desktop
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox1.Password.Trim();


            if (!InputValidator.ValidateEmail(email))
            {
                MessageBox.Show("Введите корректную почту (пример: example@mail.ru)",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!InputValidator.ValidatePassword(password))
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MainEmpty mainWindow = new MainEmpty();
            mainWindow.Show();
            this.Close();
        }
    }
}