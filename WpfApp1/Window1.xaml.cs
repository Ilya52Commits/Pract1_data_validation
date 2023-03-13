using inputValidation;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        public Window1() => InitializeComponent();

        private static string _lineKey = "employee";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); 
            Window window = new Window();

            string textLogin = Login.Text;
            string textPassword = Password.Password;

            int numberAttempts = 0; 
            while (true)
            {
                if (numberAttempts < 3)
                {
                    if (textLogin == _lineKey && textPassword == _lineKey)
                    {
                        mainWindow.Show();
                        window.Close();
                        break;
                    }
                    else
                    {
                        if (textLogin != _lineKey)
                            Login.BorderBrush = Brushes.Red; 
                        if (textPassword != _lineKey)
                            Password.BorderBrush = Brushes.Red;
                    }
                }
                else
                {
                    MessageBox.Show("Введено слишном много попыток", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                    
                numberAttempts++;
            }
        }
    }
}
