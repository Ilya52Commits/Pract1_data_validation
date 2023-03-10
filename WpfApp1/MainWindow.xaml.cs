using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.IO;


namespace inputValidation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        // function of the button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textIdentiti = Identifier.Text;
            string textName = Name.Text;
            string textSurname = Surname.Text;
            string textPasport = Passport.Text;
            string textEmail = Email.Text;

            if (string.IsNullOrWhiteSpace(textIdentiti) ||
                string.IsNullOrWhiteSpace(textName) ||
                string.IsNullOrWhiteSpace(textSurname) ||
                string.IsNullOrWhiteSpace(textPasport) ||
                string.IsNullOrWhiteSpace(textEmail))
            {
                MessageBox.Show("Введены некоректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // mail check 
            if (!textEmail.Contains("@"))
            {
                MessageBox.Show("Неверный адрес1", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] delimiter = textEmail.Split('@');

            if (delimiter.Length != 2)
            {
                MessageBox.Show("Неверный адрес2", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string login = delimiter[0];
            string domain = delimiter[1];

            char firstSymbol = login[0];

            if (!Regex.IsMatch(firstSymbol.ToString(), @"[a-z]"))
            {
                MessageBox.Show("Введены некоректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] arrDobain = domain.Split('.');

            if (arrDobain.Length != 2)
            {
                MessageBox.Show("Неверный адрес3", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            File.AppendAllText("employee.txt", textEmail);
            MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
