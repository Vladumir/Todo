using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;
using TodoDesktop;

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
            RegistrationWindow reg = new RegistrationWindow();
            reg.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox1.Password.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Введите Email!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool success = UserRepository.Login(email, password);

            if (!success)
            {
                MessageBox.Show("Неверный Email или пароль!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var userId = UserRepository.CurrentUser!.Id;
            bool hasTasks = TaskRepository.GetTasksForUser(userId).Any();

            if (!hasTasks)
            {

                MainEmpty empty = new MainEmpty();
                empty.Show();
                this.Close();
                return;
            }
            else
            {

                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
    }
}