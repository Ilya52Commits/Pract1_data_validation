using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using inputValidation;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        private CancellationTokenSource _isAuthorizationWindowOpened = new CancellationTokenSource(); // погуглить тип данных 
        private DispatcherTimer _timer = new DispatcherTimer();
        private string _lineKey = "employee";
        public MainWindow _mainWindow = new MainWindow();
        private DateTime _time;
        private int _counter = 0;
        private int _seconds = 0;

        public Window1()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
            _timer.Tick += new EventHandler(TimerTick);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textLogin = Login.Text;
            string textPassword = Password.Password;

            if (ToTriger() == true)
            {
                _timer.Stop();
                CheckLoginPassword();
            }
        }

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
                    _timer.Dispatcher.DisableProcessing();
                }
                _seconds = 0;
            }
        }

        private bool CheckLoginPassword()
        {
            if (Login.Text == _lineKey && Password.Password == _lineKey)
            {
                _mainWindow.Show();
                _timer.Stop();
                _timer.Dispatcher.DisableProcessing();
                Close();
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

        public bool ToTriger()
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
    }
}