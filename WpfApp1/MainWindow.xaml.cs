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

        // button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            Regex regex = new Regex(@"(@)");
            MatchCollection email = regex.Matches(Email.Text);

            foreach (Match match in email)
                count++;

            if (count > 1 || count < 1)
                MessageBox.Show("Неверный адрес", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                File.AppendAllText("employee.txt", Email.Text);
        }
    }
}
