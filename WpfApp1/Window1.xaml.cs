using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading; // погуглить, что это за блиблиотека 
using inputValidation;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        private CancellationTokenSource _isAuthorizationWindowOpened = new CancellationTokenSource(); // погуглить тип данных 
        private DispatcherTimer _timer = new DispatcherTimer();
        private MainWindow _mainWindow = new MainWindow();
        private DateTime _time;
        private string _lineKey = "employee";
        private int _counter = 0;
        private int _seconds = 0;

        public Window1()
        {
            InitializeComponent();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
            _timer.Tick += new EventHandler(TimerTick);
        }

        // function of the button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textLogin = Login.Text;
            string textPassword = Password.Password;

            if (ShowTimeMessage() == true)
            {
                _timer.Stop();
                CheckLoginPassword();
            }

        }

        // function of timer
        private void TimerTick(object sender, EventArgs e)
        {
            _seconds++;
            if (_seconds == 10)
            {
                var message = MessageBox.Show($"Уже прошла минута, а вы не ввели данные.\nЗакрыть программу?", "Привышение ожидания", MessageBoxButton.OKCancel);
                if (message == MessageBoxResult.OK)
                {
                    _timer.Stop();
                    Close();
                }
                _seconds = 0;
            }
        }

        // function of chek login and password 
        private void CheckLoginPassword()
        {
            if (Login.Text == _lineKey && Password.Password == _lineKey)
            {
                _mainWindow.Show();
                Close();
            }
            else
            {
                if (Login.Text != _lineKey)
                    Login.BorderBrush = Brushes.Red;
                if (Password.Password != _lineKey)
                    Password.BorderBrush = Brushes.Red;
            }
        }

        // displays a message about the expiration of time
        public bool ShowTimeMessage()
        {
            if (_counter == 3)
            {
                TimeSpan time = DateTime.Now - _time;
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
                _time = DateTime.Now;
                MessageBox.Show("Неправильные данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        //private void ClosingForm(Object sender, EventArgs e)
        //{

        //}
    }
}
// перенести все переменные вначало проекта 
// настроить кнопку крестик