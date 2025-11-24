using System;
using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;
using Todo.Entities;
using TodoDesktop;

namespace Desktop
{
    public partial class AddTaskWindow : Window
    {
        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(CategoryBox.Text))
            {
                MessageBox.Show("Введите категорию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DatePickerTask.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (TimeBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите время!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string timeStr = ((ComboBoxItem)TimeBox.SelectedItem).Content.ToString()!;
            DateTime date = DatePickerTask.SelectedDate.Value;

            if (!TimeSpan.TryParse(timeStr, out TimeSpan time))
            {
                MessageBox.Show("Ошибка времени!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fullDateTime = date.Date + time;


            TaskModel task = new TaskModel
            {
                Title = TitleBox.Text.Trim(),
                Category = CategoryBox.Text.Trim(),
                Description = DescriptionBox.Text.Trim(),
                Date = fullDateTime,
                IsDone = false,
                OwnerId = UserRepository.CurrentUser!.Id
            };

            TaskRepository.AddTask(task);

            MessageBox.Show("Задача успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);



            MainWindow? main = null;


            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainWindow m)
                {
                    main = m;
                    break;
                }
            }

            if (main == null)
            {
                main = new MainWindow();
                main.Show();
            }

            if (main.Tasks != null)
            {
                main.RefreshTasks();
                main.SelectedTask = task;
            }


            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainEmpty)
                {
                    w.Close();
                    break;
                }
            }

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
