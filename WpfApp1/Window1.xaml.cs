using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using inputValidation;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += new EventHandler(_timerTick);
        }

        DispatcherTimer timer = new DispatcherTimer();
        private static string _lineKey = "employee";
        private static int _counter = 0;
        MainWindow mainWindow = new MainWindow(); 
        Window window = new Window();
        DateTime Time;
        long seconds = 0;  

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textLogin = Login.Text;
            string textPassword = Password.Password;

            if (_toTriger() == true)
                _checkLoginPassword();
        }

        private void _timerTick(object sender, EventArgs e)
        {
            seconds++;

            if (seconds == 60)
            {
                var message = MessageBox.Show($"Уже прошла минута, а вы не ввели данные.\nЗакрыть программу?", "Привышение ожидания", MessageBoxButton.OKCancel);
                if (message == MessageBoxResult.OK)
                    Close();
                seconds = 0;
            }
        }

        private bool _checkLoginPassword()
        {
            if (Login.Text == _lineKey && Password.Password == _lineKey)
            {
                mainWindow.Show();
                window.Close();
                Close();
                timer.Stop();
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
