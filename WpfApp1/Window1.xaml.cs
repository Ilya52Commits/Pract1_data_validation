using System;
using System.Windows;
using System.Windows.Media;
using inputValidation;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        public Window1() => InitializeComponent();

        private static string _lineKey = "employee";
        private static int _counter = 0;
        MainWindow mainWindow = new MainWindow(); 
        Window window = new Window();
        DateTime Time;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textLogin = Login.Text;
            string textPassword = Password.Password;

            if (_toTriger() == true)
                _checkLoginPassword();
        }

        private bool _checkLoginPassword()
        {
            if (Login.Text == _lineKey && Password.Password == _lineKey)
            {
                mainWindow.Show();
                window.Close();
                return true;
            }
            else
            {
                if (Login.Text != _lineKey)
                    Login.BorderBrush = Brushes.Red;
                if (Password.Password != _lineKey)
                    Password.BorderBrush = Brushes.Red;
                return false; 
            } 
        }

        public bool _toTriger()
        {
            if (_counter == 3)
            {
                TimeSpan time = DateTime.Now - Time;

                if (time.TotalSeconds < 60)
                {
                    MessageBox.Show($"Осталось {60 - (int)time.TotalSeconds}");
                    return false;
                }

                _counter = 0;
            }
            if (Login.Text.Trim() == _lineKey || Password.Password.Trim() == _lineKey) 
            {
                _counter = 0;
                return true; 
            }

            _counter = (_counter + 1) % 4;

            if (_counter == 3)
            {
                Time = DateTime.Now;
                MessageBox.Show("Вы - долбоёб!", "Хватит вводить хуйню!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; 
            }
            return true; 
        }
    }
}
